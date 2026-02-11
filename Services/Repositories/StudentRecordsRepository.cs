using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class StudentRecordsRepository : IStudentRecords
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;

        public StudentRecordsRepository(ApplicationDbContext context, RequestContext requestContext)
        {
            _context = context;
            _requestContext = requestContext;
        }

        public async Task<GeneralResponse> GetAllStudentRecords(PagingParameters paging, StudentRecordsFilter filter)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter?.InstitutionShortName))
                query = query.Where(x => x.InstitutionShortName == filter.InstitutionShortName);

            if (!string.IsNullOrWhiteSpace(filter?.MatricNumber))
                query = query.Where(x => x.MatricNumber == filter.MatricNumber);

            if (!string.IsNullOrWhiteSpace(filter?.ProgrammeCode))
                query = query.Where(x => x.ProgrammeCode == filter.ProgrammeCode);

            if (!string.IsNullOrWhiteSpace(filter?.Search))
            {
                query = query.Where(x =>
                    (x.FirstName != null && x.FirstName.Contains(filter.Search)) ||
                    (x.LastName != null && x.LastName.Contains(filter.Search)) ||
                    (x.MatricNumber != null && x.MatricNumber.Contains(filter.Search)));
            }

            var totalRecords = await query.CountAsync();

            var data = await query
                .OrderBy(x => x.LastName)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = totalRecords == 0 ? "No student records found" : "Student records retrieved successfully",
                Data = data,
                Meta = new
                {
                    paging.PageNumber,
                    paging.PageSize,
                    TotalRecords = totalRecords,
                    TotalPages = totalRecords == 0 ? 0 : (int)Math.Ceiling(totalRecords / (double)paging.PageSize)
                }
            };
        }

        public async Task<GeneralResponse> GetStudentRecordsById(string id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Student record not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Student record retrieved successfully",
                Data = student
            };
        }

        public async Task<GeneralResponse> UpdateStudentRecords(string id, UpdateStudentRecordsDto model)
        {
            try
            {
                // 1. Fetch the student
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Student record not found",
                        Data = null
                    };
                }

                // 2. Check if ProgrammeCode is changing (TRIGGER for matric regeneration)
                bool programmeChanged =
                    !string.IsNullOrEmpty(model.ProgrammeCode) &&
                    !string.Equals(student.ProgrammeCode, model.ProgrammeCode, StringComparison.OrdinalIgnoreCase);

                // 3. Update all editable fields (only if provided)
                if (!string.IsNullOrEmpty(model.LastName))
                    student.LastName = model.LastName;

                if (!string.IsNullOrEmpty(model.FirstName))
                    student.FirstName = model.FirstName;

                if (!string.IsNullOrEmpty(model.MiddleName))
                    student.MiddleName = model.MiddleName;

                if (!string.IsNullOrEmpty(model.Email))
                {
                    student.Email = model.Email;
                    student.UserName = model.Email;
                    student.NormalizedEmail = model.Email.ToUpper();
                    student.NormalizedUserName = model.Email.ToUpper();
                }

                if (!string.IsNullOrEmpty(model.PhoneNumber))
                    student.PhoneNumber = model.PhoneNumber;

                if (!string.IsNullOrEmpty(model.InstitutionShortName))
                    student.InstitutionShortName = model.InstitutionShortName;

                if (!string.IsNullOrEmpty(model.BatchShortName))
                    student.BatchShortName = model.BatchShortName;

                if (!string.IsNullOrEmpty(model.DepartmentCode))
                    student.DepartmentCode = model.DepartmentCode;

                if (!string.IsNullOrEmpty(model.ProgrammeCode))
                    student.ProgrammeCode = model.ProgrammeCode;

                if (model.AdmittedSessionId > 0)
                    student.AdmittedSessionId = model.AdmittedSessionId;

                if (model.AdmittedLevelId > 0)
                    student.AdmittedLevelId = model.AdmittedLevelId;

                if (model.CurrentLevel > 0)
                    student.CurrentLevel = model.CurrentLevel;

                if (model.CurrentSessionId.HasValue)
                    student.CurrentSessionId = model.CurrentSessionId;

                if (model.CurrentSemesterId.HasValue)
                    student.CurrentSemesterId = model.CurrentSemesterId;

                if (!string.IsNullOrEmpty(model.ApplicantId))
                    student.ApplicantId = model.ApplicantId;

                // 4. REGENERATE MATRIC NUMBER if programme changed
                if (programmeChanged)
                {
                    // Validate programme exists
                    var programmeExists = await _context.Programmes
                        .AnyAsync(p => p.ProgrammeCode == student.ProgrammeCode);

                    if (!programmeExists)
                    {
                        return new GeneralResponse
                        {
                            StatusCode = 400,
                            Message = "Invalid ProgrammeCode. Cannot regenerate matric number.",
                            Data = null
                        };
                    }

                    // Validate session exists - USE ID (from CommonBase)
                    var sessionExists = await _context.AcademicSessions
                        .AnyAsync(s => s.Id == student.AdmittedSessionId);

                    if (!sessionExists)
                    {
                        return new GeneralResponse
                        {
                            StatusCode = 400,
                            Message = "Invalid AdmittedSessionId. Cannot regenerate matric number.",
                            Data = null
                        };
                    }

                    // Generate new matric number
                    student.MatricNumber = GenerateMatricNumber(
                        student.ProgrammeCode,
                        student.AdmittedSessionId
                    );
                }

                // 5. Save changes
                _context.Students.Update(student);
                await _context.SaveChangesAsync();

                // 6. Build enriched response
                var response = await BuildStudentRecordsResponse(student);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Student record updated successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Update failed: {ex.Message}",
                    Data = null
                };
            }
        }

        public string GenerateMatricNumber(string programmeCode, int sessionId)
        {
           
            var programme = _context.Programmes
                .FirstOrDefault(p => p.ProgrammeCode == programmeCode);

            if (programme == null)
                throw new Exception("Invalid ProgrammeCode");

         
            var session = _context.AcademicSessions
                .FirstOrDefault(s => s.Id == sessionId);

            if (session == null)
                throw new Exception("Invalid AdmittedSessionId");

          
            string[] years = session.SessionName.Split('/');

            if (years.Length != 2)
                throw new Exception("Invalid SessionName format. Expected format: YYYY/YYYY");

            string startYear = years[0].Substring(2, 2); 

            
            int serial = _context.Students.Count();
            int newSerial = serial + 1;

           
            string matricNumber = $"{programme.ProgrammeCode}/{startYear}/{newSerial.ToString().PadLeft(4, '0')}";

            return matricNumber;
        }

        private async Task<UpdateStudentRecordsResponse> BuildStudentRecordsResponse(Students student)
        {
            // Get admitted session using Id (from CommonBase)
            var admittedSession = await _context.AcademicSessions
                .FirstOrDefaultAsync(s => s.Id == student.AdmittedSessionId);

            var admittedLevel = await _context.AcademicLevels
                .FirstOrDefaultAsync(l => l.Id == student.AdmittedLevelId);

            string? currentSessionName = null;
            if (student.CurrentSessionId.HasValue)
            {
                var currentSession = await _context.AcademicSessions
                    .FirstOrDefaultAsync(s => s.Id == student.CurrentSessionId.Value);

                currentSessionName = currentSession?.SessionName;
            }

            string? currentSemesterName = null;
            if (student.CurrentSemesterId.HasValue)
            {
                // FIX: Query by Id (from CommonBase), not SemesterId
                var currentSemester = await _context.Semesters
                    .Where(s => s.Id == student.CurrentSemesterId.Value)
                    .Select(s => s.SemesterName)  // Only select the name
                    .FirstOrDefaultAsync();

                currentSemesterName = currentSemester;
            }

            var programme = await _context.Programmes
                .FirstOrDefaultAsync(p => p.ProgrammeCode == student.ProgrammeCode);

            return new UpdateStudentRecordsResponse
            {
                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                MatricNumber = student.MatricNumber,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                InstitutionShortName = student.InstitutionShortName,
                BatchShortName = student.BatchShortName,
                DepartmentCode = student.DepartmentCode,
                ProgrammeCode = student.ProgrammeCode,
                ProgrammeName = programme?.ProgrammeName,

                AdmittedSessionId = student.AdmittedSessionId,
                AdmittedSessionName = admittedSession?.SessionName,

                AdmittedLevelId = student.AdmittedLevelId,
                AdmittedLevelName = admittedLevel?.LevelName,

                CurrentLevel = student.CurrentLevel,
                CurrentSessionId = student.CurrentSessionId,
                CurrentSessionName = currentSessionName,
                CurrentSemesterId = student.CurrentSemesterId,
                CurrentSemesterName = currentSemesterName,

                ApplicantId = student.ApplicantId
            };
        }
    }
}