using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Workshop.Models
{
    [MetadataType(typeof(SystemUserMetada))]
    public partial class SystemUser 
    {
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}

        private class SystemUserMetada
        {
            //public string CWP { get; set; }

            public System.Guid ID { get; set; }

            [DisplayName("姓名")]
            [StringLength(20)]
            public string Name { get; set; }

            [DisplayName("帳號")]
            [StringLength(20)]
            public string Account { get; set; }

            [DisplayName("密碼")]
            public string Password { get; set; }

            public string Salt { get; set; }

            [DisplayName("電子郵件")]
            [EmailAddress]
            public string Email { get; set; }

            [UIHint("SystemUserName")]
            public System.Guid CreateUser { get; set; }

            public System.DateTime CreateDate { get; set; }

            [UIHint("SystemUserName")]
            public Nullable<System.Guid> UpdateUser { get; set; }
            
            public System.DateTime UpdateDate { get; set; }
        }
    }

    //public class SystemUserMetada
    //{
    //    //public string CWP { get; set; }
        
    //    public System.Guid ID { get; set; }

    //    [DisplayName("姓名")]
    //    [StringLength(20)]
    //    public string Name { get; set; }

    //    [DisplayName("帳號")]
    //    [StringLength(20)]
    //    public string Account { get; set; }

    //    [DisplayName("密碼")]
    //    public string Password { get; set; }

    //    public string Salt { get; set; }

    //    [DisplayName("電子郵件")]
    //    [EmailAddress]
    //    public string Email { get; set; }

    //    public System.Guid CreateUser { get; set; }
    //    public System.DateTime CreateDate { get; set; }
    //    public Nullable<System.Guid> UpdateUser { get; set; }
    //    public System.DateTime UpdateDate { get; set; }
    //}
}