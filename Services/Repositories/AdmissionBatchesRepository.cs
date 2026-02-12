using AutoMapper;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class AdmissionBatchesRepository : IAdmissionBatches
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;
        public AdmissionBatchesRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _requestContext.InstitutionShortName = requestContext.InstitutionShortName.ToUpper();
            _mapper = mapper;
        }
        public async Task<GeneralResponse> CreateAdmissionBatchAsync(AdmissionBatchesDto model)
        {
            if (model == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid admission batch data",
                    Data = null
                };
            }
            model.InstitutionShortName = _requestContext.InstitutionShortName;

            var admissionBatchExists = await _context.AdmissionBatches.FirstOrDefaultAsync(x => x.InstitutionShortName == model.InstitutionShortName && x.BatchShortName == model.BatchShortName);
            if(admissionBatchExists != null) 
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = $"Batch short name: {model.BatchShortName} already exists",
                    Data = null
                };
            }

            var entity = new AdmissionBatches
            {
                BatchShortName = model.BatchShortName,
                InstitutionShortName = model.InstitutionShortName,
                BatchName = model.BatchName,
                Description = model.Description,
                ActiveStatus = model.ActiveStatus,

            };

            await _context.AdmissionBatches.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Admission batch created successfully",
                Data = new AdmissionBatchesDto { 
                
                }
            };
        }

        public async Task<GeneralResponse> DeleteAdmissionBatchAsync(long Id)
        {
            var batch = await _context.AdmissionBatches.FirstOrDefaultAsync(x => x.Id == Id);

            if (batch == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Admission batch not found",
                    Data = null
                };
            }

            _context.AdmissionBatches.Remove(batch);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Admission batch deleted successfully",
                Data = null
            };
        }

        public async Task<GeneralResponse> GetAdmissionBatchByIdAsync(long Id)
        {
            if (Id <= 0)
            {
                return new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid ID",
                    Data = null
                };
            }

            var batch = await _context.AdmissionBatches.FirstOrDefaultAsync(x => x.Id == Id);
            if (batch == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Admission batch not found",
                    Data = null
                };
            }

            var batchDto = _mapper.Map<AdmissionBatchesDto>(batch);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Admission batch retrieved successfully",
                Data = batchDto//batch
            };
        }

        public async Task<GeneralResponse> GetAllAdmissionBatchAsync(PagingParameters paging)
        {
            var query = _context.AdmissionBatches.Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName).AsQueryable();

            var totalRecords = await query.CountAsync();

            var batches = await query
                .OrderBy(x => x.BatchName)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            var batchesDto = _mapper.Map<List<AdmissionBatchesDto>>(batches);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = totalRecords == 0
                    ? "No admission batches found"
                    : "Admission batches retrieved successfully",
                Data = batchesDto,//batches, // EMPTY LIST if none
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

        public async Task<GeneralResponse> UpdateAdmissionBatchAsync(long Id, UpdateAdmissionBatchesDto model)
        {
            var batch = await _context.AdmissionBatches.FirstOrDefaultAsync(x => x.Id == Id);

            if (batch == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Admission batch not found",
                    Data = null
                };
            }

            batch.BatchName = model.BatchName;
            batch.Description = model.Description;
            batch.ActiveStatus = model.ActiveStatus;

            _context.AdmissionBatches.Update(batch);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Admission batch updated successfully",
                Data = batch
            };
        }

        public async Task<GeneralResponse> UpdateAdmissionBatchByShortNameAsync(string shortName, UpdateAdmissionBatchesDto model)
        {
            var batch = await _context.AdmissionBatches.FirstOrDefaultAsync(x => x.BatchShortName == shortName);

            if (batch == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Admission batch not found",
                    Data = null
                };
            }

            batch.BatchName = model.BatchName;
            batch.Description = model.Description;

            _context.AdmissionBatches.Update(batch);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Admission batch updated successfully",
                Data = batch
            };
        }
    }
}
