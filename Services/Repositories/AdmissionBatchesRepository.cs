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
        public AdmissionBatchesRepository(ApplicationDbContext context)
        {
            _context = context;
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

            var entity = new AdmissionBatches
            {
                BatchShortName = model.BatchShortName,
                InstitutionShortName = model.InstitutionShortName,
                BatchName = model.BatchName,
                Description = model.Description
            };

            await _context.AdmissionBatches.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new GeneralResponse
            {
                StatusCode = 201,
                Message = "Admission batch created successfully",
                Data = entity
            };
        }

        public async Task<GeneralResponse> DeleteAdmissionBatchAsync(int Id)
        {
            var batch = await _context.AdmissionBatches.FindAsync(Id);

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

        public async Task<GeneralResponse> GetAdmissionBatchByIdAsync(int Id)
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

            var batch = await _context.AdmissionBatches.FindAsync(Id);
            if (batch == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Admission batch not found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Admission batch retrieved successfully",
                Data = batch
            };
        }

        public async Task<GeneralResponse> GetAllAdmissionBatchAsync()
        {
            var batches = await _context.AdmissionBatches.ToListAsync();

            if (!batches.Any())
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "No admission batches found",
                    Data = null
                };
            }

            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Admission batches retrieved successfully",
                Data = batches
            };
        }

        public async Task<GeneralResponse> UpdateAdmissionBatchAsync(int Id, AdmissionBatchesDto model)
        {
            var batch = await _context.AdmissionBatches.FindAsync(Id);

            if (batch == null)
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "Admission batch not found",
                    Data = null
                };
            }

            batch.BatchShortName = model.BatchShortName;
            batch.InstitutionShortName = model.InstitutionShortName;
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
