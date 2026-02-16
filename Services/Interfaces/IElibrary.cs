using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Services.Interfaces
{
    public interface IELibrary
    {
        Task<GeneralResponse> CreateELibraryAsync(CreateELibraryDto model);
        Task<GeneralResponse> GetELibraryByIdAsync(long id);
        Task<GeneralResponse> GetAllELibraryAsync(PagingParameters paging, ELibraryFilter filter);
        Task<GeneralResponse> UpdateELibraryAsync(long id, UpdateELibraryDto model);
        Task<GeneralResponse> DeleteELibraryAsync(long id);
    }
}