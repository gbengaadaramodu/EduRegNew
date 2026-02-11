using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;

namespace EduReg.Managers
{
    public class LibraryManager : IELibrary
    {
        private readonly IELibrary _library;

        public LibraryManager(IELibrary library)
        {
            _library = library;
        }

        public async Task<GeneralResponse> CreateELibraryAsync(ELibraryDto model)
        {
            return await _library.CreateELibraryAsync(model);
        }

        public async Task<GeneralResponse> GetELibraryByIdAsync(long id)
        {
            return await _library.GetELibraryByIdAsync(id);
        }

        // Updated to accept the optional courseCode
        public async Task<GeneralResponse> GetAllELibraryAsync(PagingParameters paging, string? institutionShortName = null, string? courseCode = null)
        {
            return await _library.GetAllELibraryAsync(paging, institutionShortName, courseCode);
        }

        public async Task<GeneralResponse> UpdateELibraryAsync(long id, ELibraryDto model)
        {
            return await _library.UpdateELibraryAsync(id, model);
        }

        public async Task<GeneralResponse> DeleteELibraryAsync(long id)
        {
            return await _library.DeleteELibraryAsync(id);
        }
    }
}