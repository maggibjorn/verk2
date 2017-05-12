using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCode01.ViewModels
{
    /// <summary>
    /// View model for list of user projects.
    /// </summary>
    public class ProjectListViewModel
    {
        /// <summary>
        /// Id of project.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Name of project.
        /// </summary>
        public string name { get; set; }
        
    }
    
}