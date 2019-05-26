using Microsoft.EntityFrameworkCore;
using PaymentService.Core.Context.Mappers;
using PaymentService.Core.Models;

namespace PaymentService.Core.Context
{
    public class PaymentServiceDbContext : DbContext
    {
        public PaymentServiceDbContext()
        {
            Database.EnsureCreated();
        }

        public PaymentServiceDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<PaymentEntity> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentMapper());
        }
    }
}