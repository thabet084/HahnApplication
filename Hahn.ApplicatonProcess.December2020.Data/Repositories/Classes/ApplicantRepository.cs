using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Repositories.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories.Classes
{
    public class ApplicantRepository: IApplicantRepository
    {
        private readonly HahnDBContext _context;
        private readonly IMapper _mapper;
        public ApplicantRepository(HahnDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplicantViewModel> Add(ApplicantViewModel applicantViewModel )
        {
            var applicant = _mapper.Map<Applicant>(applicantViewModel);

            await _context.Applicants.AddAsync(applicant);
           await _context.SaveChangesAsync();

           return _mapper.Map<ApplicantViewModel>(applicant);

           

        }

        public async Task<bool> IsEmailExist(string email,int? excludedId)
        {

            return await _context.Applicants.AnyAsync(a=>a.EmailAddress==email 
            &&( excludedId == null?true:a.Id!=excludedId));
            
        }
        public async Task<ApplicantViewModel> Get(int id)
        {
            var applicant = await _context.Applicants.FindAsync(id);

            if (applicant != null)
                return _mapper.Map<ApplicantViewModel>(applicant);

            return null;

        }

        public async Task<List<ApplicantViewModel>> GetAll()
        {
            var applicants = await _context.Applicants.ToListAsync();

            if(applicants!=null)
            return _mapper.Map<List<ApplicantViewModel>>(applicants);

            return null;
            
        }

        public async Task<bool> Update(int id,ApplicantViewModel applicantViewModel)
        {
            var applicant = await _context.Applicants.FindAsync(id);

            if (applicant != null)
            {
                applicant.Address = applicantViewModel.Address;
                applicant.Age = applicantViewModel.Age;
                applicant.CountryOfOrigin = applicantViewModel.CountryOfOrigin;
                applicant.Name = applicantViewModel.Name;
                applicant.FamilyName = applicantViewModel.FamilyName;
                applicant.EmailAddress = applicantViewModel.EmailAddress;

                applicant.ModificationDateTime = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;

            }
                

            return false;

        }

        public async Task<bool> Delete(int id)
        {
            var applicant = await _context.Applicants.FindAsync(id);

            if (applicant != null)
            {
                _context.Applicants.Remove(applicant);
                await _context.SaveChangesAsync();

                return true;

            }


            return false;

        }
    }
}
