using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    /// <summary>
    /// View model for adding new member to project.
    /// </summary>
    public class AddMemberViewModel
    {
        /// <summary>
        /// Id of project the member is being added to.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Email of the member.
        /// </summary>
        [Required]
        [Display(Name ="User Email")]
        public string userEmail { get; set; }
    }
}