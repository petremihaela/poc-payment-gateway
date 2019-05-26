using Microsoft.EntityFrameworkCore;

namespace PaymentService.Core.Context
{
    public class PaymentServiceDbContext : DbContext
    {
        public PaymentServiceDbContext()
        {
        }

        public PaymentServiceDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}