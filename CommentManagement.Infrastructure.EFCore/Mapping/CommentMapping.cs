﻿using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Email).HasMaxLength(500);
            builder.Property(x => x.Message).HasMaxLength(1000);
            builder
                .HasMany(builder => builder.Children)
                .WithOne(builder => builder.Parent)
                .HasForeignKey(builder => builder.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
