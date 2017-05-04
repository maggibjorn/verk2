using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    public class Member
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string userId { get; set; }
        public bool isAuthor { get; set; }
    }
}