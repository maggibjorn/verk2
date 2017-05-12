using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    /// <summary>
    /// Entity class for comments on project. 
    /// Not used in final solution but it has been set up in database.
    /// Decided not to delete it to end up in migration problems on final day.
    /// </summary>
    public class Comment
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string userName { get; set; }
        public string text { get; set; }
        public string date { get; set; }

    }
}