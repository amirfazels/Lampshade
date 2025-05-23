﻿using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .ToTable("Accounts");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(x => x.Mobile)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(x => x.ProfilePhoto)
                .HasMaxLength(500);

            builder
                .HasOne(x => x.Role)
                .WithMany(x => x.Account)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade); ;


        }
    }
}
