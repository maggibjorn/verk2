using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.ViewModels
{
    /// <summary>
    /// View model for kicking member out of certain project.
    /// </summary>
    public class KickMemberViewModel
    {
        /// <summary>
        /// List of members in project.
        /// </summary>
        public KickMemberViewModel()
        {
            members = new List<AddMemberViewModel>();
        }
        /// <summary>
        /// Id of the project.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// Name of the project.
        /// </summary>
        public string name { get; set; }
        public List<AddMemberViewModel> members { get; set; }
    }
}