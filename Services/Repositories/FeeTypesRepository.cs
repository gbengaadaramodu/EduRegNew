using AutoMapper;
using EduReg.Common;
using EduReg.Data;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Models.Entities;
using EduReg.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduReg.Services.Repositories
{
    public class FeeTypeRepository : IFeeTypes
    {
        private readonly ApplicationDbContext _context;
        private readonly RequestContext _requestContext;
        private readonly IMapper _mapper;

        public FeeTypeRepository(ApplicationDbContext context, RequestContext requestContext, IMapper mapper)
        {
            _context = context;
            _requestContext = requestContext;
            _mapper = mapper;
        }

        public async Task<GeneralResponse> CreateFeeTypeAsync(FeeTypeDto model)
        {
            var exists = await _context.FeeTypes.AnyAsync(x => x.Name == model.Name &&
                                                              x.InstitutionShortName == _requestContext.InstitutionShortName);
            if (exists)
            {
                return new GeneralResponse { StatusCode = 400, Message = "Fee Type already exists." };
            }

            var feeType = new FeeType
            {
                Name = model.Name,
                Description = model.Description,
                InstitutionShortName = _requestContext.InstitutionShortName,
                IsSystemDefined = model.IsSystemDefined,
                ActiveStatus = model.ActiveStatus,
                CreatedBy = model.CreatedBy,
                Created = DateTime.Now
            };

            _context.FeeTypes.Add(feeType);
            await _context.SaveChangesAsync();

            var feeTypeDto = _mapper.Map<FeeTypeDto>(feeType);

            return new GeneralResponse { StatusCode = 200, Message = "Created successfully.", Data = feeTypeDto };
        }

        public async Task<GeneralResponse> GetAllFeeTypesAsync(PagingParameters paging, string? institutionShortName = null)
        {
            var query = _context.FeeTypes.AsQueryable();

            
            
                query = query.Where(x => x.InstitutionShortName == _requestContext.InstitutionShortName);
            

            var totalRecords = await query.CountAsync();
            var data = await query
                .OrderByDescending(x => x.Id)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            var dataDto = _mapper.Map<List<FeeTypeDto>>(data);

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Success",
                Data = dataDto,
                Meta = new
                {
                    paging.PageNumber,
                    paging.PageSize,
                    TotalRecords = totalRecords,
                    TotalPages = (int)Math.Ceiling(totalRecords / (double)paging.PageSize)
                }
            };
        }

        public async Task<GeneralResponse> GetFeeTypeByIdAsync(long id)
        {
            var item = await _context.FeeTypes.FindAsync(id);
            if (item == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            var itemDto = _mapper.Map<FeeTypeDto>(item);

            return new GeneralResponse { StatusCode = 200, Message = "Success", Data = itemDto };
        }

        public async Task<GeneralResponse> UpdateFeeTypeAsync(long id, FeeTypeDto model)
        {
            var existingItem = await _context.FeeTypes.FindAsync(id);
            if (existingItem == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            if (existingItem.IsSystemDefined)
            return new GeneralResponse { StatusCode = 403, Message = "System-defined items cannot be updated." };

            existingItem.Name = model.Name;
            existingItem.Description = model.Description;
            existingItem.ActiveStatus = model.ActiveStatus;

            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Updated successfully." };
        }

        public async Task<GeneralResponse> DeleteFeeTypeAsync(long id)
        {
            var item = await _context.FeeTypes.FindAsync(id);
            if (item == null) return new GeneralResponse { StatusCode = 404, Message = "Not found." };

            if (item.IsSystemDefined)
                return new GeneralResponse { StatusCode = 403, Message = "System-defined items cannot be deleted." };

            _context.FeeTypes.Remove(item);
            await _context.SaveChangesAsync();
            return new GeneralResponse { StatusCode = 200, Message = "Deleted successfully." };
        }
    }
}