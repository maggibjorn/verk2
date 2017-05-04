using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    public class Project
    {
        public int id { get; set; }
        public string name { get; set; }
        public int projectTypeId { get; set; }
    }
}