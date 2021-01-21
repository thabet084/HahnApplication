using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Domain.Entities.Common
{
    /// <summary>
    /// Base Entity Structure for all system entities
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Record's Primary Key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Creation date time
        /// </summary>
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Modification date time
        /// </summary>
        public DateTime ModificationDateTime { get; set; } = DateTime.Now;

    }
}
