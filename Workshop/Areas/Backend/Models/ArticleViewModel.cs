using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Workshop.Models;

namespace Workshop.Areas.Backend.Models
{
    public class ArticleViewModel
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "Id 為必填")]
        public Guid ID { get; set; }

        [DisplayName("CategoryID")]
        [Required(ErrorMessage = "Category 為必填")]
        public Guid CategoryID { get; set; }

        [DisplayName("標題")]
        [Required(ErrorMessage = "名稱 為必填")]
        public string Subject { get; set; }

        [DisplayName("摘要")]
        [Required(ErrorMessage = "摘要 為必填")]
        public string Summary { get; set; }

        [DisplayName("內容")]
        [Required(ErrorMessage = "內容 為必填")]
        public string ContentText { get; set; }

        [DisplayName("是否發佈")]
        [Required(ErrorMessage = "是否發佈 為必填")]
        public bool IsPublish { get; set; }

        [DisplayName("發佈日期")]
        [Required(ErrorMessage = "發佈日期 為必填")]
        [UIHint("DateTimePicker")]
        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        [DisplayName("瀏覽次數")]
        public int ViewCount { get; set; }

        [DisplayName("建立者")]
        [Required(ErrorMessage = "CreateUser 為必填")]
        [UIHint("SystemUserName")]
        public Guid CreateUser { get; set; }

        [DisplayName("建立時間")]
        [Required(ErrorMessage = "CreateDate 為必填")]
        public DateTime CreateDate { get; set; }

        [DisplayName("修改者")]
        [UIHint("SystemUserName")]
        public Guid? UpdateUser { get; set; }

        [DisplayName("修改時間")]
        [Required(ErrorMessage = "UpdateDate 為必填")]
        public DateTime UpdateDate { get; set; }

        public Category Category { get; set; }

        public IEnumerable<Photo> Photo { get; set; }
    }
}