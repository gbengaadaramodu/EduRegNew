using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Models.Dto.Request;

namespace EduReg.Interfaces
{
    public interface IStudent
    {
        Task<(StudentResponse item, string message, bool isSuccess)> StudentLogin(StudentLogin student);
    }
}