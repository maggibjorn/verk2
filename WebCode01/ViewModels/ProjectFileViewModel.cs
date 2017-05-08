using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.ViewModels
{
    public class ProjectFileViewModel
    {
        public int id { get; set; }
        public string fileContent { get; set; }
    }
    public class AddFileViewModel
    {
        public int projectId { get; set; }
        public HttpPostedFileBase file { get; set; }
    }
}