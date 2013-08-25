using System.ComponentModel.DataAnnotations;

namespace Workshop.Areas.Backend.Models
{
    public class LogonViewModel
    {
        [Required]
        [Display(Name = "�b��")]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "�K�X")]
        public string Password { get; set; }

        [Display(Name = "�O���")]
        public bool Remember { get; set; }
    }
}