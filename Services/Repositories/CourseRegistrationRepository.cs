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
        public CourseRegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateCourseRegistrationAsync(CreateCourseRegistrationDto model)
        {
            var response = new GeneralResponse();
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
                var minimumUnits = 15;
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
                if(totalUnitsRegistered < maximumUnits)
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
                var currentLevel = levels.FirstOrDefault(x => x.LevelId == studentExists.CurrentLevel && x.InstitutionShortName.ToLower() == model.InstitutionShortName.ToLower());
                var coursesToRegister = new List<CourseRegistrationDetail>();

                var courseRegistration = new CourseRegistration
                {
                    SemesterId = Convert.ToInt32(studentExists.CurrentSemesterId),
                    SessionId = Convert.ToInt32(studentExists.CurrentSessionId),
                    StudentsId = studentExists.Id,
                    DepartmentCode = studentExists.DepartmentCode,
                    ProgrammeCode = studentExists.ProgrammeCode

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
                var courseRegistrationQuery = _context.CourseRegistrations/*.Include(x => x.CourseSchedule)*/.AsQueryable();
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

                    var allCourseSchedulesForSessionSemester = await _context.CourseSchedules.Where(x => x.SessionId == currentSessionId && x.SemesterId == currentSemesterId && x.InstitutionShortName == model.InstitutionShortName).ToListAsync();

                    
                    
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
