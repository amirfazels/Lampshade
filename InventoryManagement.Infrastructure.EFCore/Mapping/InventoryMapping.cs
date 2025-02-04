using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EFCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Inventory");

            builder.OwnsMany(x => x.Operations, modelBuilder =>
            {
                modelBuilder.ToTable("Operations");
                modelBuilder.HasKey(x => x.Id);
                modelBuilder.Property(x => x.Description).HasMaxLength(1000);
                modelBuilder.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);
            });
        }
    }
}
