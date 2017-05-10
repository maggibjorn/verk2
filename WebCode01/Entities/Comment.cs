using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    public class Comment
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string userName { get; set; }
        public string text { get; set; }
        public string date { get; set; }

    }
}