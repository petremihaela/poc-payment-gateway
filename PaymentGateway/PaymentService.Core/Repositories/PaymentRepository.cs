using Microsoft.EntityFrameworkCore;
using PaymentService.Core.Context;
using PaymentService.Core.Models;
using System;
using System.Threading.Tasks;

namespace PaymentService.Core.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentServiceDbContext _context;

        public PaymentRepository(PaymentServiceDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(PaymentEntity payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<PaymentEntity> GetByIdAsync(Guid paymentId)
        {
            var payment = await _context.Payments.SingleOrDefaultAsync(r => r.PaymentId == paymentId);
            return payment;
        }
    }
}