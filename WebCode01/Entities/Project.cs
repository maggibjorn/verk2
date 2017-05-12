using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    /// <summary>
    /// Entity class for user project.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Id of prject.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Name of project.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Not used, decided not to take it away to end up in migration problems on last day.
        /// </summary>
        public int numMembers { get; set; }
        
    }
}