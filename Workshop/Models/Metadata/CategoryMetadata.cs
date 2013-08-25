using System;
using System.ComponentModel.DataAnnotations;

namespace Workshop.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
        private class CategoryMetadata
        {
            [UIHint("SystemUserName")]
            public Guid CreateUser { get; set; }
            [UIHint("SystemUserName")]
            public Nullable<Guid> UpdateUser { get; set; }
        }
    }
}