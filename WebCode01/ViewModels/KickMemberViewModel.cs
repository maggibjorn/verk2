using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.ViewModels
{
    public class KickMemberViewModel
    {
        public KickMemberViewModel()
        {
            members = new List<AddMemberViewModel>();
        }
        public int projectId { get; set; }
        public string name { get; set; }
        public List<AddMemberViewModel> members { get; set; }
    }
}