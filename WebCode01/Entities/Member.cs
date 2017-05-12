using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    /// <summary>
    /// Entity class for connection table in database.
    /// Used for keeping records of which users have access to certain projects.
    /// Also contains information about authors of projects.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Id of member row.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Project id of corresponding project.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// User id of corresponding user.
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// True if user is author of project.
        /// </summary>
        public bool isAuthor { get; set; }
    }
}