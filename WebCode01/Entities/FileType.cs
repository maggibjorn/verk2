using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCode01.Entities
{
    /// <summary>
    /// Entity class for type of file.
    /// </summary>
    public class FileType
    {
        /// <summary>
        /// Id of file type.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Name of file type.
        /// </summary>
        public string name { get; set; }
    }
}