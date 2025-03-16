using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .ToTable("Roles");


            builder
                .HasKey(x=> x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasMany(x => x.Account)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);

            builder
                .OwnsMany<Permission>(x => x.Permissions, navigationBuilder =>
                {

                    navigationBuilder
                        .ToTable("RolePermissions");

                    navigationBuilder
                        .HasKey(x => x.Id);

                    navigationBuilder
                        .Property(x => x.Code)
                        .IsRequired();

                    navigationBuilder
                        .Property(x => x.Name)
                        .HasMaxLength(100)
                        .IsRequired();

                    navigationBuilder
                        .WithOwner(x => x.Role);

                });

        }
    }
}
