

using EduReg.Common;

namespace EduReg.Services.Interfaces
{
    public interface IPaymentTransactionService
    {
        Task<GeneralResponse> RecordPaymentTransactionAsync(string matricNumber, decimal amountPaid, string paymentMethod, string transactionReference);
        Task<GeneralResponse> GetPaymentTransactionsByStudentAsync(string matricNumber);
        Task<GeneralResponse> GetAllPaymentTransactionsAsync();
        Task<GeneralResponse> GetPaymentTransactionByIdAsync(int transactionId);
        Task<GeneralResponse> RefundPaymentTransactionAsync(int transactionId, decimal amountToRefund, string reason);
    }
}
