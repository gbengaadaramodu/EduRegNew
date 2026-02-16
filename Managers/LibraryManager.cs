using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class LibraryManager : IELibrary
    {
        private readonly IELibrary _eLibraryRepository;

        public LibraryManager(IELibrary eLibraryRepository)
        {
            _eLibraryRepository = eLibraryRepository;
        }

        public async Task<GeneralResponse> CreateELibraryAsync(CreateELibraryDto model)
        {
            return await _eLibraryRepository.CreateELibraryAsync( model);
        }

        public async Task<GeneralResponse> GetELibraryByIdAsync(long id)
        {
            return await _eLibraryRepository.GetELibraryByIdAsync(id);
        }

        public async Task<GeneralResponse> GetAllELibraryAsync(PagingParameters paging, ELibraryFilter filter)
        {
            return await _eLibraryRepository.GetAllELibraryAsync(paging, filter);
        }

        public async Task<GeneralResponse> UpdateELibraryAsync(long id, UpdateELibraryDto model)
        {
            return await _eLibraryRepository.UpdateELibraryAsync(id, model);
        }

        public async Task<GeneralResponse> DeleteELibraryAsync(long id)
        {
            return await _eLibraryRepository.DeleteELibraryAsync(id);
        }
    }
}