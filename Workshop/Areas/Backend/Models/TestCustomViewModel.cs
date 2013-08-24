using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workshop.Filters;

namespace Workshop.Areas.Backend.Models
{
    public class TestCustomViewModel
    {
        [NoIs("twmvc",ErrorMessage = "請用別的名稱")]
        public string Nottwmvc { get; set; }
    }
}