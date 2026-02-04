using EduReg.Common;
using EduReg.Models.Dto;

namespace EduReg.Services.Interfaces
{
    public interface IStudentFeeSchedule
    {
        Task<GeneralResponse> GenerateStudentFeeSchedulesAsync(string institutionShortName, AcademicContextDto model);


    }
}
