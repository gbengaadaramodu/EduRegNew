using EduReg.Common;
using EduReg.Models.Dto;
using EduReg.Services.Interfaces;
using Microsoft.OpenApi.Validations;

namespace EduReg.Managers
{
    
    public class FeePaymentManager : IStudentFeePaymentService
    {
        private readonly IStudentFeePaymentService _feepayment;
        public FeePaymentManager(IStudentFeePaymentService feepayment)
        {
            _feepayment = feepayment;
        }
        public async Task<GeneralResponse> UpdateStudentPaymentAsync(string matricNumber, List<long> paidItemIds)
        {
             var result = await _feepayment.UpdateStudentPaymentAsync(matricNumber, paidItemIds);
            return result;


        }
    }
}
