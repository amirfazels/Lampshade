using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.Comment;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public ArticleQueryModel? GetArticleDetails(string slug)
        {
            var article =  _blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Description = x.Description,
                }).FirstOrDefault(x => x.Slug == slug);
            article.KeywordList = article.Keywords?.Split(",").ToList();

            article.Comments = _commentContext.Comments
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.IsConfirmed)
                .Where(x => !x.IsCanceled)
                .Where(x => x.OwnerRecordId == article.Id)
                .Where(x => x.ParentId == 0)
                .Include(x => x.Children)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Children = MapComment(x.Children, article.Id)
                })
                .OrderByDescending(x => x.Id)
                .ToList();
            return article;
        }

        private static List<CommentQueryModel> MapComment(List<Comment> commentList, long OwnerRecordId)
        {
            return commentList
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.IsConfirmed)
                .Where(x => !x.IsCanceled)
                .Where(x => x.OwnerRecordId == OwnerRecordId)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi(),
                })
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                }).ToList();
        }

        public List<ArticleQueryModel> Search(string value)
        {
            throw new NotImplementedException();
        }
    }
}
