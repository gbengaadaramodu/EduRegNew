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

                // Validate file type
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

                // Validate file size (max 50MB)
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
                    InstitutionShortName = _requestContext.InstitutionShortName,
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
            var item = await _context.ELibraries.FindAsync(id);

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
            // Start with base query
            var query = _context.ELibraries.AsNoTracking().AsQueryable();

            // Filter by institution    
            if (!string.IsNullOrEmpty(filter?.InstitutionShortName))
                query = query.Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName);

            // Filter by course code
            if (!string.IsNullOrEmpty(filter?.CourseCode))
                query = query.Where(x => x.CourseCode.ToLower().Contains(filter.CourseCode.ToLower()));

            // Filter by category
            if (!string.IsNullOrEmpty(filter?.Category))
                query = query.Where(x => x.Category == filter.Category);

            // Filter by program
            if (filter?.ProgramId.HasValue == true)
                query = query.Where(x => x.ProgramId == filter.ProgramId);

            // Filter by author
            if (!string.IsNullOrEmpty(filter?.Author))
                query = query.Where(x => x.Author != null && x.Author.ToLower().Contains(filter.Author.ToLower()));

            // Search across Title, Author, Description
            if (!string.IsNullOrEmpty(filter?.Search))
            {
                string searchLower = filter.Search.ToLower();
                query = query.Where(x =>
                    x.Title.ToLower().Contains(searchLower) ||
                    (x.Author != null && x.Author.ToLower().Contains(searchLower)) ||
                    (x.Description != null && x.Description.ToLower().Contains(searchLower)));
            }

            // Get total count
            var totalRecords = await query.CountAsync();

            // Pagination
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
                var existing = await _context.ELibraries.FindAsync(id);

                if (existing == null)
                {
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Resource not found"
                    };
                }

                // Update metadata
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

                // If new file uploaded, replace the old one
                if (model.File != null && model.File.Length > 0)
                {
                    // Validate file type
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

                    // Upload new file
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
            var item = await _context.ELibraries.FindAsync(id);

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