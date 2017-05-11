using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    public class CreateBlankFileViewModel
    {
        public int projectId { get; set; }
        [Required]
        [StringLength(30,ErrorMessage = "The file must have a name")]
        public string fileName { get; set; }
        public string fileType { get; set; }
    }
}