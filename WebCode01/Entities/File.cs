using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    /// <summary>
    /// Entity class for file of certain project.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Id of file.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Name of file.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Content of file.
        /// </summary>
        public string fileContent {get; set;}
        /// <summary>
        /// Project Id of project that the file is in.
        /// </summary>
        public int projectId { get; set; }
        /// <summary>
        /// The actual type of the file. C++, javascript etc.
        /// </summary>
        public int fileTypeId { get; set; }
    }
}