﻿using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contacts.ArticleCategory
{
    public class ArticleCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Discription { get; set; }
        public int ShowOrder { get; set; }
        public string CreationDate { get; set; }
        public long ArticlesCount { get; set; }
    }
}
