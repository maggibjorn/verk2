using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    public class CreateProjectViewModel
    {
        [Required]
        [Display(Name = "Project Name")]
        public string name { get; set; }       
       
    }
}