using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    public class AddMemberViewModel
    {
        public int projectId { get; set; }
        [Required]
        [Display(Name ="User Email")]
        public string userEmail { get; set; }
    }
}