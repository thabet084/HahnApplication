using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Shared.Validators
{
	public class ApplicantValidator : AbstractValidator<ApplicantViewModel>
	{
		public ApplicantValidator()
		{
			RuleFor(x => x.Id).NotNull();
			RuleFor(x => x.Name).MinimumLength(5);
			RuleFor(x => x.FamilyName).MinimumLength(5);
			RuleFor(x => x.Address).MinimumLength(5);
			RuleFor(x => x.EmailAddress).EmailAddress();
			RuleFor(x => x.Age).InclusiveBetween(20, 60);

			RuleFor(x => x.CountryOfOrigin).Custom((country,context)=>
			{
				if(! CountryValidation.Validate(country).Result)
                {
					context.AddFailure("Must be valid country");
                }
			});
		}


	}
}
