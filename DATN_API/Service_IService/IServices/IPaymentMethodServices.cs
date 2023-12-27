using DATN_API.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IPaymentMethodServices
    {
        public Task<PaymentMethod> PostPaymentMethod(PaymentMethod paymentMethod);
        public Task<PaymentMethod> PutPaymentMethod(PaymentMethod paymentMethod);
        public Task<PaymentMethod> DeletePaymentMethod(Guid Id);
        public Task<PaymentMethod> GetPaymentMethodById(Guid Id);
        public Task<IEnumerable<PaymentMethod>> GetPaymentMethodByName(string name);
        public Task<IEnumerable<PaymentMethod>> GetAllPaymentMethod();
    }
}
