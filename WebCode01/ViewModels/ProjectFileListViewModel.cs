using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.ViewModels
{
    /// <summary>
    /// View model for file in project, is part of a list of files.
    /// Returned to view as IEnumerable.
    /// </summary>
    public class ProjectFileListViewModel
    {
        /// <summary>
        /// Id of file.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Name of file.
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// Id of the project that the file is in.
        /// </summary>
        public int projectId { get; set; }
    }
}