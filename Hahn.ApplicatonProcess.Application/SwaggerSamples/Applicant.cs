using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.Application.SwaggerSamples
{
    public class ApplicantAddModelExample : IExamplesProvider<ApplicantViewModel>
    {
        public ApplicantViewModel GetExamples()
        {
            return new ApplicantViewModel
            {
                Address="Cairo",
                Age=30,
                CountryOfOrigin="Egypt",
                EmailAddress="thabet084@hotmail.com",
                FamilyName="Thabet",
                Hired=false,
                Name="Mohammed"
            };

        }
    }
}
