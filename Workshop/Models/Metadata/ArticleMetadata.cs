using System;
using System.ComponentModel.DataAnnotations;

namespace Workshop.Models
{
    [MetadataType(typeof(ArticleMetadata))]
    public partial class Article
    {
        private class ArticleMetadata
        {
            [UIHint("SystemUserName")]
            public Guid CreateUser { get; set; }
            [UIHint("SystemUserName")]
            public Nullable<Guid> UpdateUser { get; set; }
        }
    }
}