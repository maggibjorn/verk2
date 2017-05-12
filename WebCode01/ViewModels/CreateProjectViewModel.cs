using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    /// <summary>
    /// View model for creating new user project.
    /// </summary>
    public class CreateProjectViewModel
    {
        /// <summary>
        /// Name of the project.
        /// </summary>
        [Required]
        [Display(Name = "Project Name")]
        public string name { get; set; }       
       
    }
}