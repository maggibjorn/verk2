using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    /// <summary>
    /// View model for creating blank file on the website.
    /// </summary>
    public class CreateBlankFileViewModel
    {
        /// <summary>
        /// Id of the project the blank file is being added to.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Name of the blank file.
        /// </summary>
        [Required]
        public string fileName { get; set; }
        /// <summary>
        /// Type of the blank file.
        /// </summary>
        public string fileType { get; set; }
    }
}