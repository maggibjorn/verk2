using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    public class ProjectListViewModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public string authorName { get; set; }
    }
    public class CreateProjectViewModel
    {
        [Required]
        [Display(Name = "Project Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Project Type")]
        public int type { get; set; }
    }
}