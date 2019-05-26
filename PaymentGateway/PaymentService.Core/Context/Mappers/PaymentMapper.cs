using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Core.Models;

namespace PaymentService.Core.Context.Mappers
{
    public class PaymentMapper : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            builder.HasKey(p => p.PaymentId);
            builder.Property(p => p.CardNumber).HasMaxLength(19).IsRequired();
            builder.Property(p => p.Ccv).HasColumnType("char(3)").IsRequired();
            builder.Property(p => p.ExpiryYear).IsRequired();
            builder.Property(p => p.ExpiryMonth).IsRequired();
            builder.Property(p => p.Currency).HasColumnType("char(3)").IsRequired();
            builder.Property(p => p.Amount).HasDefaultValue(0).HasColumnType("decimal(5, 2)");
            builder.Property(p => p.CreatedDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
        }
    }
}