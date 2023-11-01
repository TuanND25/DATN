using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class PaymentMethodServices : IPaymentMethodServices
    {
        private readonly ApplicationDbContext _context;
        public PaymentMethodServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaymentMethod> DeletePaymentMethod(Guid Id)
        {
            try
            {
                var a = await _context.PaymentMethods.FindAsync(Id);
                a.Status = 0;
                _context.PaymentMethods.Update(a);
                await _context.SaveChangesAsync();
                return a;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethod()
        {
            var a = await _context.PaymentMethods.ToListAsync();
            return a;
        }

        public async Task<PaymentMethod> GetPaymentMethodById(Guid Id)
        {
            var a = await _context.PaymentMethods.FindAsync(Id);
            return a;
        }

        public Task<IEnumerable<PaymentMethod>> GetPaymentMethodByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentMethod> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                await _context.PaymentMethods.AddAsync(paymentMethod);
                await _context.SaveChangesAsync();
                return paymentMethod;   
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PaymentMethod> PutPaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                var a = await _context.PaymentMethods.FindAsync(paymentMethod.Id);
                a.Status = paymentMethod.Status;
                a.Name= paymentMethod.Name;
                _context.PaymentMethods.Update(a);
                await _context.SaveChangesAsync();
                return a;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
