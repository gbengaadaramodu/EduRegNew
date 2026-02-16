using AutoMapper;
using Azure;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class CourseRegistrationRepository: ICourseRegistration
    {
        private readonly ApplicationDbContext _context;
        private readonly Common.RequestContext _requestContext;
       
        public CourseRegistrationRepository(ApplicationDbContext context, Common.RequestContext requestContext)
        {
            _context = context;
            _requestContext = requestContext;
            _requestContext.InstitutionShortName = requestContext.InstitutionShortName.ToUpper();
            
        }

        public async Task<GeneralResponse> CreateCourseRegistrationAsync(CreateCourseRegistrationDto model)
        {
            var response = new GeneralResponse();
            model.InstitutionShortName = _requestContext.InstitutionShortName;
            try
            {
                if(model.CourseScheduleIds.Count <= 0)
                {
                    response.StatusCode = 400;
                    response.Message = "Courses were not selected";

                    return response;
                }

                var studentExists = await _context.Students.FirstOrDefaultAsync(x => x.MatricNumber.ToLower() == model.MatricNo.ToLower() && x.InstitutionShortName.ToLower() == model.InstitutionShortName.ToLower());
                if(studentExists == null)
                {
                    response.StatusCode = 400;
                    response.Message = $"Matric no: {model.MatricNo} in {model.InstitutionShortName} does not exist";
                    return response;
                }
                
                var result = await ValidateCourseSchedules(model.CourseScheduleIds, studentExists.CurrentSessionId, studentExists.CurrentSemesterId, studentExists.InstitutionShortName);
                if (result.success == false)
                {
                    response.StatusCode = 400;
                    response.Message = result.message;
                    
                    return response;
                }

                //These are placeholders. Come and modify once architecture is sorted
                var minimumUnits = 5;
                var maximumUnits = 21;

                var alreadyRegisteredCourses = await _context.CourseRegistrationDetails.Include(x => x.CourseSchedule).Where(x => x.CourseSchedule.SessionId == studentExists.CurrentSessionId && x.CourseSchedule.SemesterId == studentExists.CurrentSemesterId && x.StudentsId == studentExists.Id).ToListAsync();
                var alreadyRegisteredCoursesScheduleIds = alreadyRegisteredCourses.Select(x=> x.CourseSchedule.Id).ToList();

                var commonCourseScheduleIds = model.CourseScheduleIds
                                                .Intersect(alreadyRegisteredCoursesScheduleIds)
                                                .ToList();
                string errorMessage = "The following courses have already been registered: \n";
                var courses = await _context.CourseSchedules
                                        .AsNoTracking()
                                        .Where(cs => model.CourseScheduleIds.Contains(cs.Id))
                                        .ToListAsync();

                foreach (var item in commonCourseScheduleIds)
                {
                    var cs = alreadyRegisteredCourses.FirstOrDefault(x => x.Id == item);
                    var course = await _context.DepartmentCourses.FirstOrDefaultAsync(x => x.CourseCode == cs.CourseSchedule.CourseCode && x.InstitutionShortName.ToLower() == studentExists.InstitutionShortName.ToLower());
                    errorMessage += $"({cs.CourseSchedule.CourseCode}) {course?.Title}\n";
                }

                if(commonCourseScheduleIds.Count() > 0)
                {
                    response.StatusCode = 400;
                    response.Message = errorMessage;

                    return response;
                }


                int totalUnitsRegistered = 0;
                int totalUnitsAlreadyRegistered = alreadyRegisteredCourses.Sum(x => x.CourseSchedule.Units);
                int totalUnitsBeingRegistered = 0;

                totalUnitsBeingRegistered = result.courseSchedules.Sum(x => x.Units);

                totalUnitsRegistered = totalUnitsAlreadyRegistered + totalUnitsBeingRegistered;
                if(totalUnitsRegistered < minimumUnits)
                {
                    response.StatusCode = 400;
                    response.Message = $"{totalUnitsRegistered} units is less than the minimum required units {minimumUnits}";

                    return response;
                }

                if (totalUnitsRegistered > maximumUnits)
                {
                    response.StatusCode = 400;
                    response.Message = $"{totalUnitsRegistered} units is greater than the maximum required units {maximumUnits}";

                    return response;
                }

                var levels = await _context.AcademicLevels.ToListAsync();
                var currentLevel = levels.FirstOrDefault(x => x.ClassCode == studentExists.CurrentClassCode && x.InstitutionShortName.ToLower() == model.InstitutionShortName.ToLower() && x.ProgrammeCode == studentExists.ProgrammeCode);
                if (currentLevel == null)
                {
                    response.StatusCode = 400;
                    response.Message = "Academic level configuration error.";
                    return response;
                }
                var coursesToRegister = new List<CourseRegistrationDetail>();

                var courseRegistration = new CourseRegistration
                {
                    SemesterId = Convert.ToInt64(studentExists.CurrentSemesterId),
                    SessionId = Convert.ToInt64(studentExists.CurrentSessionId),
                    StudentsId = studentExists.Id,
                    DepartmentCode = studentExists.DepartmentCode,
                    ProgrammeCode = studentExists.ProgrammeCode,
                    Level = currentLevel.LevelName,
                    ClassCode = currentLevel.ClassCode

                };

                var courseRegistrationExists = await _context.CourseRegistrations.FirstOrDefaultAsync(x => x.StudentsId == studentExists.Id && x.SessionId == studentExists.CurrentSessionId && x.SemesterId == studentExists.CurrentSemesterId);
                if (courseRegistrationExists != null)
                {
                    courseRegistration.Id = courseRegistrationExists.Id;
                }
                else
                {

                    await _context.CourseRegistrations.AddAsync(courseRegistration);
                    await _context.SaveChangesAsync();
                }


                foreach (var item in model.CourseScheduleIds)
                {
                    var cs = courses.FirstOrDefault(x => x.Id == item);

                    var courseRegistrationDetail = new CourseRegistrationDetail
                    {
                        CourseScheduleId = item,
                        StudentsId =  studentExists.Id,
                        CourseRegistrationId = courseRegistration.Id,
                        CourseCode = cs?.CourseCode,
                        CourseTitle = cs?.Title,
                       
                    };

                    //var cs = await _context.CourseSchedules.FirstOrDefaultAsync(x => x.Id == item);
                    var courseLevel = levels.FirstOrDefault(x => x.ClassCode == cs.ClassCode  && x.InstitutionShortName.ToLower() == model.InstitutionShortName.ToLower());


                    if(courseLevel?.Order < currentLevel?.Order)
                    {
                        //courseRegistration.IsCarryOver = true;
                        //Work on the above
                        courseRegistrationDetail.IsCarryOver = true;
                    }

                    coursesToRegister.Add(courseRegistrationDetail);
                }


                await _context.CourseRegistrationDetails.AddRangeAsync(coursesToRegister);
                await _context.SaveChangesAsync();

                response.StatusCode = 200;
                response.Message = "Courses registered successfully";
                return response;


            }
            catch (Exception ex)
            {

                response.StatusCode = 500;
                response.Message = $"Sorry, an error occurred: {ex.Message}";

                return response;
            }

        }

        public async Task<GeneralResponse> GetCourseRegistration(CourseRegistrationRequestDto model)
        {
            var response = new GeneralResponse();
            try
            {
                model.InstitutionShortName = _requestContext.InstitutionShortName;
                var courseRegistrationQuery = _context.CourseRegistrations.AsNoTracking()/*.Include(x => x.CourseSchedule)*/.AsQueryable();
                if(!string.IsNullOrWhiteSpace(model.MatricNo))
                {
                    var studentExists = await _context.Students.FirstOrDefaultAsync(x => x.MatricNumber.ToLower() == model.MatricNo.ToLower() && x.InstitutionShortName.ToLower() == model.InstitutionShortName.ToLower());
                    if(studentExists == null) 
                    {
                        response.StatusCode = 400;
                        response.Message = $"Matric no: {model.MatricNo} in {model.InstitutionShortName} does not exist";
                        return response;
                    }
                    courseRegistrationQuery = courseRegistrationQuery.Where(x => x.StudentsId == studentExists.Id);
                }

                if(model.SessionId != null)
                {
                    //courseRegistrationQuery = courseRegistrationQuery.Where(x => x.CourseSchedule.SessionId == model.SessionId);
                    courseRegistrationQuery = courseRegistrationQuery.Where(x => x.SessionId == model.SessionId);
                }

                if (model.SemesterId != null)
                {
                    //courseRegistrationQuery = courseRegistrationQuery.Where(x => x.CourseSchedule.SemesterId == model.SemesterId);
                    courseRegistrationQuery = courseRegistrationQuery.Where(x => x.SemesterId == model.SemesterId);
                }

                var courseRegistrations = await courseRegistrationQuery.ToListAsync();
                var courseRegistrationsDto = new List<CourseRegistrationViewDto>();
                foreach (var courseRegistration in courseRegistrations)
                {
                    var item = MapEntityToDto(courseRegistration);
                    item.CourseRegistrationDetails = new List<CourseRegistrationDetailViewDto>();
                    var courseRegistrationDetails = await _context.CourseRegistrationDetails.AsNoTracking().Include(x => x.CourseSchedule).Where(x => x.CourseRegistrationId == courseRegistration.Id).ToListAsync();
                    foreach (var courseRegistrationDetailDto in courseRegistrationDetails)
                    {
                        var courseRegistrationDetail = MapEntityToDto(courseRegistrationDetailDto);
                        item.CourseRegistrationDetails.Add(courseRegistrationDetail);
                    }
                    courseRegistrationsDto.Add(item);

                }

                response.StatusCode = 200;
                response.Message = "Course registrations returned successfully";
                response.Data = courseRegistrationsDto;

                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = $"Sorry, an error occurred: {ex.Message}";

                return response;
            }
        }

        public async Task<GeneralResponse> GetCourseRegistrationById(long id)
        {
            var response = new GeneralResponse();
            try
            {
                var courseRegistration = await _context.CourseRegistrations/*.Include(x => x.CourseSchedule)*/.FirstOrDefaultAsync(x => x.Id == id);
                if (courseRegistration == null)
                {
                    response.StatusCode = 400;
                    response.Message = $"Course registrationId: {id} does not exist";
                    return response;
                }
                var courseRegistrationsDto = new CourseRegistrationViewDto();
                var item = MapEntityToDto(courseRegistration);
                item.CourseRegistrationDetails = new List<CourseRegistrationDetailViewDto>();
                var courseRegistrationDetails = await _context.CourseRegistrationDetails.AsNoTracking().Include(x => x.CourseSchedule).Where(x => x.CourseRegistrationId == courseRegistration.Id).ToListAsync();
                foreach (var courseRegistrationDetailDto in courseRegistrationDetails)
                {
                    var courseRegistrationDetail = MapEntityToDto(courseRegistrationDetailDto);
                    item.CourseRegistrationDetails.Add(courseRegistrationDetail);
                }
                courseRegistrationsDto = item;

                response.StatusCode = 200;
                response.Message = "Course registration returned successfully";
                response.Data = courseRegistrationsDto;

                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = $"Sorry, an error occurred: {ex.Message}";

                return response;
            }
        }

        public async Task<GeneralResponse> GetCoursesStudentCanRegister(CoursesStudentCanRegisterRequestDto model)
        {
            var response = new GeneralResponse();
            try
            {
                model.InstitutionShortName = _requestContext.InstitutionShortName;
                if (!string.IsNullOrWhiteSpace(model.MatricNo))
                {
                    var studentExists = await _context.Students.FirstOrDefaultAsync(x => x.MatricNumber.ToLower() == model.MatricNo.ToLower() && x.InstitutionShortName.ToLower() == model.InstitutionShortName.ToLower());
                    if (studentExists == null)
                    {
                        response.StatusCode = 400;
                        response.Message = $"Matric no: {model.MatricNo} in {model.InstitutionShortName} does not exist";
                        return response;
                    }

                    var currentSemesterId = studentExists.CurrentSemesterId;
                    var currentSessionId = studentExists.CurrentSessionId;

                    if (currentSessionId == null || currentSessionId <= 0)
                    {
                        response.StatusCode = 400;
                        response.Message = $"Matric no: {model.MatricNo} in {model.InstitutionShortName} does not have a current session";
                        return response;
                    }

                    if (currentSemesterId == null || currentSemesterId <= 0)
                    {
                        response.StatusCode = 400;
                        response.Message = $"Matric no: {model.MatricNo} in {model.InstitutionShortName} does not have a current semester";
                        return response;
                    }

                    var allCourseSchedulesForSessionSemester = await _context.CourseSchedules.Where(x => x.SessionId == currentSessionId && x.SemesterId == currentSemesterId
                                                                                                            && x.InstitutionShortName == model.InstitutionShortName
                                                                                                            && x.BatchShortName == studentExists.BatchShortName
                                                                                                            && x.ProgrammeCode == studentExists.ProgrammeCode).AsNoTracking().ToListAsync();
                    if (allCourseSchedulesForSessionSemester.Count == 0)
                    {
                        response.StatusCode = 200;
                        response.Message = $"No courses scheduled yet";
                        return response;
                    }

                    //List<CourseRegistrationDetailViewDto> coursesStudentCanRegister = new List<CourseRegistrationDetailViewDto>();
                    //var notPassedCourses = await _context.CourseRegistrationDetails
                    //                                .Where(x => x.StudentsId == studentExists.Id)
                    //                                .GroupBy(x => x.CourseCode)
                    //                                .Where(g => !g.Any(x => x.ExamStatus == "PASSED")) // Exclude if passed at least once
                    //                                .Select(g => g.First()) // Return once per course
                    //                                .AsNoTracking()
                    //                                .ToListAsync();
                    /*var passedCourseCodes = await _context.CourseRegistrationDetails
                                                        .Where(x => x.StudentsId == studentExists.Id
                                                                    && x.ExamStatus == "PASSED")
                                                        .Select(x => x.CourseCode)
                                                        .Distinct()
                                                        .ToListAsync();

                    var currentSemesterRegistrations = await _context.CourseRegistrationDetails
                                                                    .Where(x => x.StudentsId == studentExists.Id
                                                                                && x.CourseSchedule.SessionId == currentSessionId
                                                                                && x.CourseSchedule.SemesterId == currentSemesterId)
                                                                    .Select(x => new
                                                                    {
                                                                        x.CourseCode,
                                                                        x.CourseRegistrationDate,
                                                                        x.CourseScheduleId
                                                                    })
                                                                    .ToListAsync();
                   
                    var coursesStudentCanRegister = allCourseSchedulesForSessionSemester
                                                        .Where(s => !passedCourseCodes.Contains(s.CourseCode)) // exclude passed
                                                        .GroupBy(s => s.CourseCode)
                                                        .Select(g => g.First())
                                                        .Select(s =>
                                                        {
                                                            var existingRegistration = currentSemesterRegistrations
                                                                .FirstOrDefault(r => r.CourseCode == s.CourseCode);

                                                            return new CourseRegistrationDetailViewDto
                                                            {
                                                                CourseCode = s.CourseCode,
                                                                CourseTitle = s.Title,
                                                                CourseUnit = s.Units,
                                                                CourseCategory = s.CourseType,
                                                                CourseFee = s.CourseFee,
                                                                CourseScheduleId = s.Id,

                                                                // Only set registration date if registered this semester
                                                                RegistrationDate = existingRegistration != null
                                                                                    ? existingRegistration.CourseRegistrationDate
                                                                                    : default,

                                                                IsCarryOver = !passedCourseCodes.Contains(s.CourseCode) &&
                                                                              _context.CourseRegistrationDetails
                                                                                  .Any(cr => cr.StudentsId == studentExists.Id
                                                                                             && cr.CourseCode == s.CourseCode)
                                                            };
                                                        })
                                                        .ToList();*/


                    // Get student's start & current academic level orders
                    var studentLevels = await _context.AcademicLevels
                        .Where(x => x.ClassCode == studentExists.StartClassCode
                                 || x.ClassCode == studentExists.CurrentClassCode)
                        .Select(x => new
                        {
                            x.ClassCode,
                            x.Order
                        })
                        .ToListAsync();

                    var startLevel = studentLevels
                        .FirstOrDefault(x => x.ClassCode == studentExists.StartClassCode);

                    var currentLevel = studentLevels
                        .FirstOrDefault(x => x.ClassCode == studentExists.CurrentClassCode);

                    if (startLevel == null || currentLevel == null)
                    {
                        response.StatusCode = 400;
                        response.Message = "Academic level configuration error.";
                        return response;
                    }


                    //Load all academic levels once (to map course ClassCode → Order)
                    var academicLevels = await _context.AcademicLevels.Where(x => x.InstitutionShortName == model.InstitutionShortName && x.ProgrammeCode == studentExists.ProgrammeCode)
                        .Select(x => new
                        {
                            x.ClassCode,
                            x.Order
                        })
                        .ToListAsync();


                    //Get student course history
                    var studentCourseHistory = await _context.CourseRegistrationDetails
                        .Where(x => x.StudentsId == studentExists.Id)
                        .Select(x => new
                        {
                            x.CourseCode,
                            x.ExamStatus
                        })
                        .AsNoTracking()
                        .ToListAsync();

                    var passedCourseCodes = studentCourseHistory
                        .Where(x => x.ExamStatus == "PASSED")
                        .Select(x => x.CourseCode)
                        .Distinct()
                        .ToList();

                    var takenCourseCodes = studentCourseHistory
                        .Select(x => x.CourseCode)
                        .Distinct()
                        .ToList();


                    //Get current semester registrations
                    var currentSemesterRegistrations = await _context.CourseRegistrationDetails
                        .Where(x => x.StudentsId == studentExists.Id
                                    && x.CourseSchedule.SessionId == currentSessionId
                                    && x.CourseSchedule.SemesterId == currentSemesterId)
                        .Select(x => new
                        {
                            x.CourseCode,
                            x.CourseRegistrationDate,
                            x.CourseScheduleId
                        })
                        .AsNoTracking()
                        .ToListAsync();


                    ////FINAL FILTERING (ORDER-BASED)
                    //var coursesStudentCanRegister = allCourseSchedulesForSessionSemester
                    //    .Where(s =>
                    //    {
                    //        var courseLevel = academicLevels
                    //            .FirstOrDefault(l => l.ClassCode == s.ClassCode);

                    //        if (courseLevel == null)
                    //            return false;

                    //        // Rule 1: cannot register below starting level
                    //        if (courseLevel.Order < startLevel.Order)
                    //            return false;

                    //        // Rule 2: cannot register above current level
                    //        if (courseLevel.Order > currentLevel.Order)
                    //            return false;

                    //        // Rule 3: cannot register passed courses
                    //        if (passedCourseCodes.Contains(s.CourseCode))
                    //            return false;

                    //        return true;
                    //    })
                    //    .GroupBy(s => s.CourseCode)
                    //    .Select(g => g.First())
                    //    .Select(s =>
                    //    {
                    //        var existingRegistration = currentSemesterRegistrations
                    //            .FirstOrDefault(r => r.CourseCode == s.CourseCode);

                    //        bool hasTakenBefore = takenCourseCodes.Contains(s.CourseCode);
                    //        bool isPassed = passedCourseCodes.Contains(s.CourseCode);

                    //        return new CourseRegistrationDetailViewDto
                    //        {
                    //            CourseCode = s.CourseCode,
                    //            CourseTitle = s.Title,
                    //            CourseUnit = s.Units,
                    //            CourseCategory = s.CourseType,
                    //            CourseFee = s.CourseFee,
                    //            CourseScheduleId = s.Id,

                    //            RegistrationDate = existingRegistration != null
                    //                               ? existingRegistration.CourseRegistrationDate
                    //                               : default,

                    //            IsCarryOver = hasTakenBefore && !isPassed
                    //        };
                    //    })
                    //    .OrderBy(x => x.CourseCode)
                    //    .ToList();

                    var coursesStudentCanRegister = allCourseSchedulesForSessionSemester
    .Where(s =>
    {
        var courseLevel = academicLevels
            .FirstOrDefault(l => l.ClassCode == s.ClassCode);

        if (courseLevel == null)
            return false;

        // Rule 1: cannot register below starting level
        if (courseLevel.Order < startLevel.Order)
            return false;

        // Rule 2: cannot register above current level
        if (courseLevel.Order > currentLevel.Order)
            return false;

        // Rule 3: cannot register passed courses
        if (passedCourseCodes.Contains(s.CourseCode))
            return false;

        return true;
    })
    .GroupBy(s => s.CourseCode)
    .Select(g => g.First())
    .Select(s =>
    {
        var existingRegistration = currentSemesterRegistrations
            .FirstOrDefault(r => r.CourseCode == s.CourseCode);

        bool hasTakenBefore = takenCourseCodes.Contains(s.CourseCode);
        bool isPassed = passedCourseCodes.Contains(s.CourseCode);

        return new
        {
            Schedule = s,
            CourseRegistration = existingRegistration,
            IsCarryOver = hasTakenBefore && !isPassed,
            LevelOrder = academicLevels
                            .First(l => l.ClassCode == s.ClassCode)
                            .Order
        };
    })
    // ORDER: descending level first, then carryovers last, then course code for tie-breaker
    .OrderByDescending(x => x.LevelOrder)            // Current level courses first
    .ThenBy(x => x.IsCarryOver ? 1 : 0)            // Non-carryovers first
    .ThenBy(x => x.Schedule.CourseCode)            // Tie-breaker
    .Select(x => new CourseRegistrationDetailViewDto
    {
        CourseCode = x.Schedule.CourseCode,
        CourseTitle = x.Schedule.Title,
        CourseUnit = x.Schedule.Units,
        CourseCategory = x.Schedule.CourseType,
        CourseFee = x.Schedule.CourseFee,
        CourseScheduleId = x.Schedule.Id,

        RegistrationDate = x.CourseRegistration != null
                           ? x.CourseRegistration.CourseRegistrationDate
                           : default,

        IsCarryOver = x.IsCarryOver
    })
    .ToList();





                    response.StatusCode = 200;
                    response.Message = "Courses retrieved successfully";
                    response.Data = coursesStudentCanRegister;

                    return response;
                }
                else
                {
                    response.StatusCode = 400;
                    response.Message = $"Matric no is required";
                    return response;
                }

            }
            catch (Exception ex)
            {

                response.StatusCode = 500;
                response.Message = $"Sorry, an error occurred: {ex.Message}";

                return response;
            }
        }


        private async Task<(List<CourseSchedule> courseSchedules, bool success, string message)> ValidateCourseSchedules(List<long> courseScheduleIds, int? currentSessionId, int? currentSemesterId, string institutionShortName)
        {
            var courseSchedules = new List<CourseSchedule>();
            string message = "";
            foreach (var item in courseScheduleIds)
            {
                var courseScheduleExists = await _context.CourseSchedule.FirstOrDefaultAsync(x => x.Id == item && x.SessionId == currentSessionId && x.SemesterId == currentSemesterId && x.InstitutionShortName.ToLower() == institutionShortName.ToLower());
                if(courseScheduleExists == null)
                {
                    var currentSession = await _context.AcademicSessions.FirstOrDefaultAsync(x => x.Id == currentSessionId && x.InstitutionShortName.ToLower() == institutionShortName.ToLower());
                    if(currentSession == null)
                    {
                        message += $"No session with Id: {currentSessionId} in {institutionShortName}\n";
                    }
                    else
                    {
                        var currentSemester = await _context.Semesters.FirstOrDefaultAsync(x => x.Id == currentSemesterId && x.InstitutionShortName.ToLower() == institutionShortName.ToLower());
                        if(currentSemester != null)
                        {
                            message += $"No course schedule for {currentSession.SessionName} in {institutionShortName} in semester: {currentSemester.SemesterName}\n";
                        }
                        else
                        {

                             message += $"No semester with Id {currentSemesterId} in {institutionShortName}\n";
                        }
                    }
                }
                else
                {
                    courseSchedules.Add(courseScheduleExists);
                }
            }

            if(!string.IsNullOrWhiteSpace(message))
            {
                return (courseSchedules, false, message);
            }

            return (courseSchedules, true, message);
        }


        // Private helper to map DTO → Entity
        private CourseRegistrationDetailViewDto MapEntityToDto(CourseRegistrationDetail entity)
        {
            return new CourseRegistrationDetailViewDto
            {
                CourseCategory = entity.CourseSchedule?.CourseType,
                CourseCode = entity.CourseSchedule?.CourseCode,
                CourseFee = entity.CourseSchedule?.CourseFee,
                CourseScheduleId = entity.CourseSchedule.Id,
                CourseTitle = entity.CourseSchedule?.Title,
                CourseUnit = entity.CourseSchedule?.Units,
                RegistrationDate = entity.Created,
                Id = entity.Id,
                

                
                // If you have Created, CreatedBy, ActiveStatus in entity
                //Created = dto.Created,
                //CreatedBy = dto.CreatedBy,
                //ActiveStatus = dto.ActiveStatus
            };
        }

        private CourseRegistrationViewDto MapEntityToDto(CourseRegistration entity)
        {
            return new CourseRegistrationViewDto
            {
                DepartmentCode = entity.DepartmentCode,
                Level = entity.Level,
                ProgrammeCode = entity.ProgrammeCode,
                SemesterId = entity.SemesterId,
                SessionId = entity.SessionId,
                StudentsId = entity.StudentsId,
                RegistrationDate = entity.Created,
                Id = entity.Id,



                // If you have Created, CreatedBy, ActiveStatus in entity
                //Created = dto.Created,
                //CreatedBy = dto.CreatedBy,
                //ActiveStatus = dto.ActiveStatus
            };
        }
    }
}
