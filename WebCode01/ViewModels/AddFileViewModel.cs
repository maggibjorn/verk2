using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    public class AddFileViewModel
    {
        public int projectId { get; set; }
        [Required]
        [Display(Name ="File")]
        public HttpPostedFileBase file { get; set; }
    }
}