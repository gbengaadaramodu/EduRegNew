using AutoMapper;
using EduReg.Common;
using EduReg.Common.FileUploadService;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace EduReg.Services.Repositories
{
    public class ELibraryRepository : IELibrary
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public ELibraryRepository(
            ApplicationDbContext context,
            RequestContext requestContext,
            IMapper mapper,
            IFileUploadService fileUploadService)
        {
            _context = context;
            _requestContext = requestContext;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }

        public async Task<GeneralResponse> CreateELibraryAsync(CreateELibraryDto model)
        {
            try
            {
                // Validate file
                if (model.File == null || model.File.Length == 0)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "No file uploaded"
                    };
                }

                model.InstitutionShortName = _requestContext.InstitutionShortName;

   
                var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx" };
                var fileExtension = Path.GetExtension(model.File.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "Invalid file type. Allowed: PDF, DOC, DOCX, PPT, PPTX, XLS, XLSX"
                    };
                }

            
                if (model.File.Length > 50 * 1024 * 1024)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 400,
                        Message = "File size exceeds 50MB limit"
                    };
                }

                // Upload file using the service
                string fileUrl = await _fileUploadService.UploadToServer(model.File,"elibrary", model.InstitutionShortName);

                if (string.IsNullOrEmpty(fileUrl))
                {
                    return new GeneralResponse
                    {
                        StatusCode = 500,
                        Message = "File upload failed"
                    };
                }

                // Create entity
                var library = new ELibrary
                {
                    Title = model.Title,
                    Description = model.Description,
                    Author = model.Author,
                    Category = model.Category,
                    CourseCode = model.CourseCode,
                    ProgramId = model.ProgramId,
                    FileUrl = fileUrl,
                    FileName = model.File.FileName,
                    FileType = model.File.ContentType,
                    FileSizeBytes = model.File.Length,
                    InstitutionShortName = model.InstitutionShortName,
                    Created = DateTime.Now,
                    CreatedBy = model.CreatedBy,
                    ActiveStatus = model.ActiveStatus
                };

                _context.ELibraries.Add(library);
                await _context.SaveChangesAsync();

                var eLibraryDto = _mapper.Map<ELibraryDto>(library);

                return new GeneralResponse
                {
                    StatusCode = 201,
                    Message = "E-Library resource uploaded successfully",
                    Data = eLibraryDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Upload failed: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> GetELibraryByIdAsync(long id)
        {
            var item =  _context.ELibraries.Where(a => a.Id == id && a.InstitutionShortName == _requestContext.InstitutionShortName).FirstOrDefault();

            if (item == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Resource not found"
                };
            }

            var eLibraryDto = _mapper.Map<ELibraryDto>(item);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = eLibraryDto
            };
        }

        public async Task<GeneralResponse> GetAllELibraryAsync(PagingParameters paging, ELibraryFilter filter)
        {
    
            var query = _context.ELibraries.Where(a => a.InstitutionShortName == _requestContext.InstitutionShortName).AsNoTracking().AsQueryable();
 
            if (!string.IsNullOrEmpty(filter?.InstitutionShortName))
                query = query.Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName);

            if (!string.IsNullOrEmpty(filter?.CourseCode))
                query = query.Where(x => x.CourseCode.ToLower().Contains(filter.CourseCode.ToLower()));

            if (!string.IsNullOrEmpty(filter?.Category))
                query = query.Where(x => x.Category == filter.Category);

            if (filter?.ProgramId.HasValue == true)
                query = query.Where(x => x.ProgramId == filter.ProgramId);
            
            if (!string.IsNullOrEmpty(filter?.Author))
                query = query.Where(x => x.Author != null && x.Author.ToLower().Contains(filter.Author.ToLower()));

            
            if (!string.IsNullOrEmpty(filter?.Search))
            {
                string searchLower = filter.Search.ToLower();
                query = query.Where(x =>
                    x.Title.ToLower().Contains(searchLower) ||
                    (x.Author != null && x.Author.ToLower().Contains(searchLower)) ||
                    (x.Description != null && x.Description.ToLower().Contains(searchLower)));
            }

           
            var totalRecords = await query.CountAsync();

           
            var data = await query
                .OrderByDescending(x => x.Created)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            var eLibrariesDto = _mapper.Map<List<ELibraryDto>>(data);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = totalRecords == 0 ? "No resources found" : "Resources retrieved successfully",
                Data = eLibrariesDto,
                Meta = new
                {
                    paging.PageNumber,
                    paging.PageSize,
                    TotalRecords = totalRecords,
                    TotalPages = totalRecords == 0 ? 0 : (int)Math.Ceiling(totalRecords / (double)paging.PageSize)
                }
            };
        }
        public async Task<GeneralResponse> UpdateELibraryAsync(long id, UpdateELibraryDto model)
        {
            try
            {
                var existing =  _context.ELibraries.Where(a  => a.Id == id && a.InstitutionShortName == _requestContext.InstitutionShortName).FirstOrDefault();

                if (existing == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Resource not found"
                    };
                }

           
                if (!string.IsNullOrEmpty(model.Title))
                    existing.Title = model.Title;

                if (!string.IsNullOrEmpty(model.Description))
                    existing.Description = model.Description;

                if (!string.IsNullOrEmpty(model.Author))
                    existing.Author = model.Author;

                if (!string.IsNullOrEmpty(model.Category))
                    existing.Category = model.Category;

                if (!string.IsNullOrEmpty(model.CourseCode))
                    existing.CourseCode = model.CourseCode;

                if (model.ProgramId.HasValue)
                    existing.ProgramId = model.ProgramId;

               
                if (model.File != null && model.File.Length > 0)
                {
                  
                    var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx" };
                    var fileExtension = Path.GetExtension(model.File.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return new GeneralResponse
                        {
                            StatusCode = 400,
                            Message = "Invalid file type"
                        };
                    }

    
                    string newFileUrl = await _fileUploadService.UploadToServer(
                        model.File,
                        "elibrary",
                        existing.InstitutionShortName
                    );

                    if (!string.IsNullOrEmpty(newFileUrl))
                    {
                        existing.FileUrl = newFileUrl;
                        existing.FileName = model.File.FileName;
                        existing.FileType = model.File.ContentType;
                        existing.FileSizeBytes = model.File.Length;
                    }
                }

                _context.ELibraries.Update(existing);
                await _context.SaveChangesAsync();

                var eLibraryDto = _mapper.Map<ELibraryDto>(existing);

                return new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "Updated successfully",
                    Data = eLibraryDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse
                {
                    StatusCode = 500,
                    Message = $"Update failed: {ex.Message}"
                };
            }
        }

        public async Task<GeneralResponse> DeleteELibraryAsync(long id)
        {
            var item = _context.ELibraries.Where(a => a.Id == id && a.InstitutionShortName == _requestContext.InstitutionShortName).FirstOrDefault();

            if (item == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Resource not found"
                };
            }

            _context.ELibraries.Remove(item);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Deleted successfully"
            };
        }
    }
}