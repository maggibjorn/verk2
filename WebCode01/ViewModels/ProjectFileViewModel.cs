using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.ViewModels
{
    public class ProjectFileViewModel
    {
        public string fileName { get; set; }
        public HttpPostedFileBase file { get; set; }
        
    }
}