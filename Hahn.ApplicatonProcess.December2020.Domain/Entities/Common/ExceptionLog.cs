using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace Hahn.ApplicatonProcess.December2020.Domain.Entities.Common
{
    /// <summary>
    /// Table used to store error of system
    /// </summary>
    public class ExceptionLog : BaseEntity
    {
        /// <summary>
        /// The method which had exception
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Index]
        public string Method { get; set; }

        /// <summary>
        /// Parameters used if any
        /// </summary>
        [Required]
        public string Data { get; set; }

        /// <summary>
        /// Exception Details
        /// </summary>
        [Required]
        public string Exception { get; set; }






    }
}
