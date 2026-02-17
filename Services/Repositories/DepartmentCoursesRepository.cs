using AutoMapper;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using EduReg.Utilities.FileUtility;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EduReg.Services.Repositories
{
    public class DepartmentCoursesRepository : IDepartmentCourses
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _config;
        public DepartmentCoursesRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper, IWebHostEnvironment hostEnvironment, IConfiguration config)
        {
            _context = context;
            _requestContext = requestContext;
            _requestContext.InstitutionShortName = requestContext.InstitutionShortName.ToUpper();
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _config = config;
        }

        public async Task<GeneralResponse> CreateDepartmentCourseAsync(DepartmentCoursesDto model)
        {
            try
            {
                model.InstitutionShortName = _requestContext.InstitutionShortName;
                var departmentExists = await _context.Departments.AnyAsync(x => x.DepartmentCode == model.DepartmentCode && x.InstitutionShortName == model.InstitutionShortName);
                if (!departmentExists)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = $"Invalid department code: {model.DepartmentCode}",
                        Data = null
                    };
                }

                var courseExists = await _context.DepartmentCourses.AnyAsync(x => x.InstitutionShortName == model.InstitutionShortName && x.CourseCode == model.CourseCode);
                if (courseExists)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = $"Course code: {model.CourseCode} already exists",
                        Data = null
                    };
                }


                var entity = new DepartmentCourses
                {
                    InstitutionShortName = model.InstitutionShortName,
                    DepartmentCode = model.DepartmentCode,
                    CourseCode = model.CourseCode,
                    Title = model.Title,
                    Units = model.Units,
                    CourseType = model.CourseType,
                    Prerequisite = model.Prerequisite,
                    Description = model.Description,
                    CreatedBy = model.CreatedBy,
                    Created = model.Created,
                    ActiveStatus = model.ActiveStatus
                };

                await _context.DepartmentCourses.AddAsync(entity);
                await _context.SaveChangesAsync();

                var departmentCoursesDto = _mapper.Map<DepartmentCoursesDto>(entity);
                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Department course created successfully.",
                    Data = departmentCoursesDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> CreateDepartmentCourseAsync(List<DepartmentCoursesDto> models)
        {
            try
            {
                var entities = models.Select(model => new DepartmentCourses
                {
                    InstitutionShortName = model.InstitutionShortName,
                    DepartmentCode = model.DepartmentCode,
                    CourseCode = model.CourseCode,
                    Title = model.Title,
                    Units = model.Units,
                    CourseType = model.CourseType,
                    Prerequisite = model.Prerequisite,
                    Description = model.Description,
                    CreatedBy = model.CreatedBy,
                    Created = model.Created,
                    ActiveStatus = model.ActiveStatus
                }).ToList();

                await _context.DepartmentCourses.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                var departmentCoursesDtos = _mapper.Map<List<DepartmentCoursesDto>>(entities);

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Department courses created successfully.",
                    Data = departmentCoursesDtos
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> UploadDepartmentCourseAsync(byte[] model)
        {
            // TODO: Implement Excel file parsing using EPPlus or similar library
            // For now, return a placeholder response
            return new GeneralResponse
            {
                StatusCode = 501,
                Message = "Upload functionality not yet implemented. Please use individual course creation.",
                Data = null
            };
        }

        public async Task<GeneralResponse> UploadDepartmentCourseAsync(IFormFile fileUploaded)
        {
            // TODO: Implement Excel file parsing using EPPlus or similar library
            // For now, return a placeholder response
            try
            {
                if(fileUploaded != null)
                {
                    //where upload will be done.
                    var file = new AttachmentModel();
                    //reads a content of the file

                    string folderPath = Path.Combine(_hostEnvironment.ContentRootPath, "FilledDocuments");
                    //create folder if not existing
                    Directory.CreateDirectory(folderPath);

                    using (var memoryStream = new MemoryStream())
                    {
                        await fileUploaded.CopyToAsync(memoryStream);

                        // Upload the file if less than 2 MB
                        // if (memoryStream.Length < 2097152)
                        if (memoryStream.Length < Convert.ToInt32(_config["FileSize"]))
                        {
                            file = new AttachmentModel()
                            {
                                FileName = fileUploaded.FileName,
                                Format = fileUploaded.ContentType,
                                Content = memoryStream.ToArray()
                            };

                            var fullpath = Path.Combine(_hostEnvironment.ContentRootPath, "FilledDocuments", file.FileName);

                            //save the file
                            //System.IO.File.WriteAllBytes(fullpath, file.Content);
                            ByteToFile.SaveByteArrayToFile(file.Content, fullpath);

                            //read the excel
                            // ReadToExcel.ReadExcelFile(fullpath);
                            var dataTable = ReadToExcel.GetDataTableFromExcelFile(fullpath, "");

                            //Convert the datatable to list
                            var member = new List<DepartmentCoursesUploadModel>();
                            member = DataTableToList.ConvertDataTable<DepartmentCoursesUploadModel>(dataTable);
                            if (member.Count > 0)
                            {
                                var response = await CreateDepartmentCourseForFileAsync(member);
                                
                                if (response.StatusCode == 400)
                                {
                                    return new GeneralResponse()
                                    {
                                        Message = response.Message,
                                        StatusCode = 400
                                    };
                                }
                                //if (errors.Count() > 0)
                                if (response.StatusCode == 207)
                                {
                                    List<DepartmentCoursesErrorModel> errors = (List<DepartmentCoursesErrorModel>)response.Data;
                                    var filename = "DepartmentCoursesErrorMessages.xlsx";

                                    string folderPath2 = Path.Combine(_hostEnvironment.ContentRootPath, "Documents");

                                    //create folder if not existing
                                    Directory.CreateDirectory(folderPath2);

                                    var fullpath2 = Path.Combine(_hostEnvironment.ContentRootPath, "Documents", filename);

                                    WriteToExcel.WriteExcelFile(errors, fullpath2);

                                    var provider = new FileExtensionContentTypeProvider();
                                    if (!provider.TryGetContentType(fullpath2, out var contentType))
                                    {
                                        contentType = "application/octet-stream";
                                    }

                                    var bytes = await System.IO.File.ReadAllBytesAsync(fullpath2);

                                    return new GeneralResponse
                                    {
                                        StatusCode = 201,
                                        Data = new FileUploadErrorResponse
                                        {
                                            Bytes = bytes,
                                            ContentType = contentType,
                                            FilePath = fullpath2
                                        },
                                        Message = "Some deparment courses not uploaded"
                                    };
                                    //return File(bytes, contentType, Path.GetFileName(fullpath2));


                                }

                                return new GeneralResponse()
                                {
                                    //Data = response.member,
                                    Message = "Successfully uploaded department courses",
                                    StatusCode = 200
                                };
                            }
                            else
                            {
                                return new GeneralResponse()
                                {
                                    Message = "No department course uploaded.",
                                    StatusCode = 400
                                };
                            }
                        }
                        else
                        {
                            return new GeneralResponse()
                            {
                                Message = "The file upload is too large",
                                StatusCode = 400
                            };
                        }
                    }
                }
                else
                {

                    return new GeneralResponse()
                    {
                        Message = "Kindly upload Document to continue",
                        StatusCode = 400
                    };
                }
            }
            catch (Exception ex)
            {

                return new GeneralResponse
                {
                    StatusCode = 501,
                    Message = "Upload functionality not yet implemented. Please use individual course creation.",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> CreateDepartmentCourseForFileAsync(List<DepartmentCoursesUploadModel> models)
        {
            try
            {

                var existingDepartments = await _context.Departments.Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName).ToListAsync();
                var existingCourses = await _context.DepartmentCourses.Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName).ToListAsync();


                var departmentCoursesError = new List<DepartmentCoursesErrorModel>();
                var departmentCourses = new List<DepartmentCoursesUploadModel>();

                foreach (var item in models)
                {
                    var departmentExists = existingDepartments.Any(x => x.DepartmentCode == item.DepartmentCode);
                    if (!departmentExists)
                    {
                        departmentCoursesError.Add(CreateErrorModel(item, $"Department with code: {item.DepartmentCode} does not exist"));
                        continue;
                    }

                    var courseExists = existingCourses.Any(x => x.CourseCode == item.CourseCode);
                    if (courseExists)
                    {
                        departmentCoursesError.Add(CreateErrorModel(item, $"Department with code: {item.DepartmentCode} does not exist"));
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.Title))
                    {
                        departmentCoursesError.Add(CreateErrorModel(item, $"Title cannot be empty"));
                        continue;
                    }

                    if (Convert.ToInt32(item.Units) < 0)
                    {
                        departmentCoursesError.Add(CreateErrorModel(item, $"Units cannot be less than 0"));
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.CourseType))
                    {
                        departmentCoursesError.Add(CreateErrorModel(item, $"Course type is required"));
                        continue;
                    }

                    item.CourseType = item.CourseType.ToUpper();
                    if(item.CourseType != "C" || item.CourseType != "E")
                    {
                        departmentCoursesError.Add(CreateErrorModel(item, $"Course type has to be E or C ie Elective or Compulsory"));
                        continue;
                    }

                    departmentCourses.Add(item);
                }

                var entities = departmentCourses.Select(model => new DepartmentCourses
                {
                    InstitutionShortName = _requestContext.InstitutionShortName,
                    DepartmentCode = model.DepartmentCode,
                    CourseCode = model.CourseCode,
                    Title = model.Title,
                    Units = model.Units,
                    CourseType = model.CourseType,
                    //Prerequisite = model.Prerequisite,
                    Description = model.Description,
                    //CreatedBy = model.CreatedBy,
                    //Created = model.Created,
                    //ActiveStatus = model.ActiveStatus
                }).ToList();

                await _context.DepartmentCourses.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                var departmentCoursesDtos = _mapper.Map<List<DepartmentCoursesDto>>(entities);

                if(departmentCoursesError.Count > 0)
                {
                    return new GeneralResponse
                    {
                        Data = departmentCoursesError,
                        Message = "Processed with errors",
                        StatusCode = 207
                    };
                }

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "Department courses created successfully.",
                    Data = departmentCoursesDtos
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<GeneralResponse> UpdateDepartmentCourseAsync(long id, DepartmentCoursesDto model)
        {
            var course = await _context.DepartmentCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Course not found",
                    Data = null
                };
            }

            model.InstitutionShortName = _requestContext.InstitutionShortName;
            var departmentExists = await _context.Departments.AnyAsync(x => x.DepartmentCode == model.DepartmentCode && x.InstitutionShortName == model.InstitutionShortName);
            if (!departmentExists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Invalid department code: {model.DepartmentCode}",
                    Data = null
                };
            }

            var courseExists = await _context.DepartmentCourses.AnyAsync(x => x.InstitutionShortName == model.InstitutionShortName && x.CourseCode == model.CourseCode);
            if (!courseExists)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Course code: {model.CourseCode} already exists",
                    Data = null
                };
            }

            course.Title = model.Title;
            course.Units = model.Units;
            course.CourseType = model.CourseType;
            course.Prerequisite = model.Prerequisite;
            course.Description = model.Description;
            course.ActiveStatus = model.ActiveStatus;

            _context.DepartmentCourses.Update(course);
            await _context.SaveChangesAsync();

            var departmentCourseDto = _mapper.Map<DepartmentCoursesDto>(course);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Course updated successfully",
                Data = departmentCourseDto
            };
        }

        public async Task<GeneralResponse> DeleteDepartmentCourseAsync(long id)
        {
            var course = await _context.DepartmentCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Course not found",
                    Data = null
                };
            }

            _context.DepartmentCourses.Remove(course);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Course deleted successfully",
                Data = null
            };
        }

        public async Task<GeneralResponse> GetDepartmentCoursesByIdAsync(long id)
        {
            var course = await _context.DepartmentCourses.FindAsync(id);
            if (course == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Course not found",
                    Data = null
                };
            }

            var departmentCourseDto = _mapper.Map<DepartmentCoursesDto>(course);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = departmentCourseDto
            };
        }

        public async Task<GeneralResponse> GetDepartmentCoursesByDepartmentNameAsync(string shortname)
        {
            var courses = await _context.DepartmentCourses
                .Where(x => x.DepartmentCode == shortname)
                .ToListAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = courses
            };
        }

        public async Task<GeneralResponse> GetAllDepartmentsByCoursesAsync(PagingParameters paging,DepartmentCourseFilter filter)
        {
            var query = _context.DepartmentCourses.AsQueryable();
            filter.InstitutionShortName = _requestContext.InstitutionShortName;
            // Apply filters from the filter class
            if (!string.IsNullOrWhiteSpace(filter?.InstitutionShortName))
                query = query.Where(x => x.InstitutionShortName == filter.InstitutionShortName);

            if (!string.IsNullOrWhiteSpace(filter?.DepartmentCode))
                query = query.Where(x => x.DepartmentCode == filter.DepartmentCode);

            if (!string.IsNullOrWhiteSpace(filter?.CourseType))
                query = query.Where(x => x.CourseType == filter.CourseType);

            if (filter?.Units != null)
                query = query.Where(x => x.Units == filter.Units);

            if (!string.IsNullOrWhiteSpace(filter?.Search))
                query = query.Where(x =>
                    (x.Title != null && x.Title.Contains(filter.Search)) ||
                    (x.CourseCode != null && x.CourseCode.Contains(filter.Search)) ||
                    (x.Description != null && x.Description.Contains(filter.Search))
                );

            var totalRecords = await query.CountAsync();

            var courses = await query
                .OrderBy(x => x.Title)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            var departmentCoursesDtos = _mapper.Map<List<DepartmentCoursesDto>>(courses);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = totalRecords == 0
                    ? "No department courses found"
                    : "Department courses retrieved successfully",
                Data = departmentCoursesDtos ,
                Meta = new
                {
                    paging.PageNumber,
                    paging.PageSize,
                    TotalRecords = totalRecords,
                    TotalPages = totalRecords == 0
                        ? 0
                        : (int)Math.Ceiling(totalRecords / (double)paging.PageSize)
                }
            };
        }

        private DepartmentCoursesErrorModel CreateErrorModel(DepartmentCoursesUploadModel item, string errorMessage)
        {
            return new DepartmentCoursesErrorModel
            {
                CourseCode = item.CourseCode,
                Title = item.Title,
                DepartmentCode = item.DepartmentCode,
                ErrorMessage = errorMessage
            };
        }

    }
}
