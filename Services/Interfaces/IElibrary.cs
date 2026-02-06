using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IELibrary
    {
        Task<GeneralResponse> CreateELibraryAsync(ELibraryDto model);
        Task<GeneralResponse> GetELibraryByIdAsync(long id);
        Task<GeneralResponse> GetAllELibraryAsync(PagingParameters paging, string? institutionShortName = null, string? courseCode = null );
        Task<GeneralResponse> UpdateELibraryAsync(long id, ELibraryDto model);
        Task<GeneralResponse> DeleteELibraryAsync(long id);
    }
}