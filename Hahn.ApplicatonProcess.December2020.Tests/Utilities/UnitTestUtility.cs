using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Tests.Utilities
{
    public class UnitTestUtility
    {
        /// <summary>
        /// Create mapper configuration for unit testing
        /// </summary>
        /// <returns></returns>
        public static IMapper CreateMapper()
        {

            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Applicant, ApplicantViewModel>().ReverseMap();

            });

            var mapper = config.CreateMapper();

            return mapper;
        }

        public static ApplicantViewModel Get_ValidApplicant()
        {
            ApplicantViewModel applicantViewModel = new ApplicantViewModel();
            applicantViewModel.Address = "Cairo";
            applicantViewModel.Age = 40;
            applicantViewModel.CountryOfOrigin = "Germany";
            applicantViewModel.EmailAddress = "test@gmail.com";
            applicantViewModel.FamilyName = "Edward";
            applicantViewModel.Name = "Johny";
            applicantViewModel.Hired = true;
            return applicantViewModel;
        }

        public static ApplicantViewModel Get_ValidApplicantWithId()
        {
            ApplicantViewModel applicantViewModel = new ApplicantViewModel();
            applicantViewModel.Id = 1;
            applicantViewModel.Address = "Cairo";
            applicantViewModel.Age = 40;
            applicantViewModel.CountryOfOrigin = "Germany";
            applicantViewModel.EmailAddress = "testz@gmail.com";
            applicantViewModel.FamilyName = "Edwardz";
            applicantViewModel.Name = "Johnyz";
            applicantViewModel.Hired = true;
            return applicantViewModel;
        }

        public static ApplicantViewModel Get_InValidApplicant_ExistedEmail()
        {
            ApplicantViewModel applicantViewModel = new ApplicantViewModel();
            applicantViewModel.Address = "Cairo";
            applicantViewModel.Age = 40;
            applicantViewModel.CountryOfOrigin = "Egypt";
            applicantViewModel.EmailAddress = "thabet084@hotmail.com";
            applicantViewModel.FamilyName = "Thabet";
            applicantViewModel.Name = "Mohammed";
            applicantViewModel.Hired = true;
            return applicantViewModel;
        }

        public static ApplicantViewModel Get_InValidApplicant_NameLengthLessThan5()
        {
            ApplicantViewModel applicantViewModel = new ApplicantViewModel();
            applicantViewModel.Address = "Cairo";
            applicantViewModel.Age = 40;
            applicantViewModel.CountryOfOrigin = "Egypt";
            applicantViewModel.EmailAddress = "s@hotmail.com";
            applicantViewModel.FamilyName = "sayed";
            applicantViewModel.Name = "s";
            applicantViewModel.Hired = true;
            return applicantViewModel;
        }
    }
}
