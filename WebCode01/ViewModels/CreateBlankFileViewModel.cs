using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.ViewModels
{
    public class CreateBlankFileViewModel
    {
        public int projectId { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
    }
}