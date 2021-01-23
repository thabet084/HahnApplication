using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Services.Interfaces
{
    public interface IApplicantService
    {
         Task<(ApplicantViewModel applicantViewModel, string errorMessage)> Add(ApplicantViewModel applicantViewModel);

        Task<ApplicantViewModel> Get(int id);
        Task<List<ApplicantViewModel>> GetAll();

        Task<(bool isSuccess, string errorMessage)> Update(int id, ApplicantViewModel applicantViewModel);

        Task<bool> Delete(int id);
    }
}
