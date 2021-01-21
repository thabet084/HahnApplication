using Hahn.ApplicatonProcess.December2020.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Domain.Entities
{
    public class Applicant: BaseEntity
    {
      
        [MinLength(5)]
        public string Name { get; set; }

        [MinLength(5)]
        public string FamilyName { get; set; }

        [MinLength(10)]
        public string Address { get; set; }

        public string CountryOfOrigin { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Range(20,60)]
        public int Age { get; set; }

        public bool Hired { get; set; }
    }
}
