using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500);
            builder.Property(x => x.Discription).HasMaxLength(2000);
            builder.Property(x => x.Picture).HasMaxLength(2000);
            builder.Property(x => x.Slug).HasMaxLength(500);
            builder.Property(x => x.Discription).HasMaxLength(600);
            builder.Property(x => x.Keyword).HasMaxLength(100);
            builder.Property(x => x.MetaDiscription).HasMaxLength(150);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(1000);

            builder.HasMany(x => x.Articles)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
