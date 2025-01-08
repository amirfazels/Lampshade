using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductCategoryMapping
    {
        public ProductCategoryMapping(EntityTypeBuilder<ProductCategory> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("ProductCategories");
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            entityTypeBuilder.Property(x => x.Keywords).IsRequired().HasMaxLength(80);
            entityTypeBuilder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(150);
            entityTypeBuilder.Property(x => x.Slug).HasMaxLength(300);
            entityTypeBuilder.Property(x => x.PictureAlt).HasMaxLength(250);
            entityTypeBuilder.Property(x => x.PictureTitle).HasMaxLength(500);
            entityTypeBuilder.Property(x => x.Description).HasMaxLength(500);
            entityTypeBuilder.Property(x => x.Picture).HasMaxLength(1000);
        }
    }
}
