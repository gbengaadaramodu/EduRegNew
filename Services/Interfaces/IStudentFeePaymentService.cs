using EduReg.Common;

namespace EduReg.Services.Interfaces
{
    public interface IStudentFeePaymentService
    {
        Task<GeneralResponse> UpdateStudentPaymentAsync(string matricNumber, List<long> paidItemIds);


    }

    
}
