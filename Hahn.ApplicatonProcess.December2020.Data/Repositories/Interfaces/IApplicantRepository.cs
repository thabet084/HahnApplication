using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories.Interfaces
{
    public interface IApplicantRepository
    {
        Task<ApplicantViewModel> Add(ApplicantViewModel applicantViewModel);

        Task<ApplicantViewModel> Get(int id);
        Task<List<ApplicantViewModel>> GetAll();

        Task<bool> Update(int id, ApplicantViewModel applicantViewModel);

        Task<bool> Delete(int id);

        Task<bool> IsEmailExist(string email, int? excludedId);
    }
}
