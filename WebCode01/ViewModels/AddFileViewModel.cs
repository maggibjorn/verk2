using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    /// <summary>
    /// View model for adding new file to project.
    /// </summary>
    public class AddFileViewModel
    {
        /// <summary>
        /// Id of the project the file is in.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// File content.
        /// </summary>
        [Required]
        [Display(Name ="File")]
        public HttpPostedFileBase file { get; set; }
    }
}