using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.ViewModels
{
    /// <summary>
    /// View model for specific file.
    /// Used in editor.
    /// </summary>
    public class ProjectFileViewModel
    {
        /// <summary>
        /// Id of file.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// File name.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// File content.
        /// </summary>
        public string fileContent { get; set; }
    }
    
}