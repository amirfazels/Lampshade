using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore
{
    public class BlogContext : DbContext
    {
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogContext).Assembly);
        }
    }
}
