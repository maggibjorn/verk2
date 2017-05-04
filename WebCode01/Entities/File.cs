using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    public class File
    {
        public int id { get; set; }
        public string name { get; set; }
        public byte[] fileBinary {get; set;}
        public int projectId { get; set; }
    }
}