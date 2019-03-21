using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuceneSearch.AzureLucene
{
    /// <summary>
    /// An Entry in teh Lucene Index
    /// </summary>
    public class Entry
    {
        /// <summary>
        /// Unique Identifier for the Entry
        /// 
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// The Name of Person or Organisation
        /// </summary>
        [Required]
        public string Name { get; set; }

    }
}