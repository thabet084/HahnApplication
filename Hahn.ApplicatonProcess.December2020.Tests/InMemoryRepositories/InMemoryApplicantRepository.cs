using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Repositories.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Tests.InMemoryRepositories
{
    public class InMemoryApplicantRepository : IApplicantRepository
    {
        private List<Applicant> applicants = null;
        private readonly IMapper _mapper;
        public InMemoryApplicantRepository(IMapper mapper)
        {
            _mapper = mapper;

            applicants = new List<Applicant>();
            applicants.Add(new Applicant()
            {
                Id = 1,
                Address = "Cairo",
                Age = 30,
                CountryOfOrigin = "Egypt",
                EmailAddress = "thabet084@hotmail.com",
                FamilyName = "Thabet",
                Name = "Mohammed",
                Hired = true
            });

        }
        public async Task<ApplicantViewModel> Add(ApplicantViewModel applicantViewModel)
        {
            var applicant = _mapper.Map<Applicant>(applicantViewModel);

            applicants.Add(applicant);
            

            return _mapper.Map<ApplicantViewModel>(applicant);
        }

        public async Task<bool> Delete(int id)
        {
            var applicant = applicants.FirstOrDefault(a=>a.Id==id);

            if (applicant != null)
            {
                applicants.Remove(applicant);
               

                return true;

            }


            return false;
        }

        public async Task<ApplicantViewModel> Get(int id)
        {
            var applicant =  applicants.FirstOrDefault(a=>a.Id==id);

            if (applicant != null)
                return _mapper.Map<ApplicantViewModel>(applicant);

            return null;
        }

        public async Task<List<ApplicantViewModel>> GetAll()
        {
          
            if (applicants != null)
                return _mapper.Map<List<ApplicantViewModel>>(applicants);

            return null;
        }

        public async Task<bool> IsEmailExist(string email, int? excludedId)
        {
            return  applicants.Any(a => a.EmailAddress == email
              && (excludedId == null ? true : a.Id != excludedId));
        }

        public async Task<bool> Update(int id, ApplicantViewModel applicantViewModel)
        {
            var applicant = applicants.FirstOrDefault(a => a.Id == id);

            if (applicant != null)
            {
                applicant.Address = applicantViewModel.Address;
                applicant.Age = applicantViewModel.Age;
                applicant.CountryOfOrigin = applicantViewModel.CountryOfOrigin;
                applicant.Name = applicantViewModel.Name;
                applicant.FamilyName = applicantViewModel.FamilyName;
                applicant.EmailAddress = applicantViewModel.EmailAddress;

                applicant.ModificationDateTime = DateTime.Now;
               

                return true;

            }


            return false;
        }
    }
}
