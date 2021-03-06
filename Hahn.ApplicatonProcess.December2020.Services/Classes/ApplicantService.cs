﻿using Hahn.ApplicatonProcess.December2020.Data.Repositories.Interfaces;
using Hahn.ApplicatonProcess.December2020.Services.Interfaces;
using Hahn.ApplicatonProcess.December2020.Shared.Resources;
using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Services.Classes
{
    /// <summary>
    /// application service comment1
    /// </summary>
    public class ApplicantService: IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        public ApplicantService(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }
        public async Task<(ApplicantViewModel applicantViewModel, string errorMessage)> Add(ApplicantViewModel applicantViewModel)
        {
            if (await _applicantRepository.IsEmailExist(applicantViewModel.EmailAddress,null))
                return (null, Resource.Error_EmailDuplicated);
                
            var addedApplicantViewModel = await _applicantRepository.Add(applicantViewModel);
            return (addedApplicantViewModel, string.Empty);
        }

        public async Task<ApplicantViewModel> Get(int id)
        {
            return await _applicantRepository.Get(id);
        }
        public async Task<List<ApplicantViewModel>> GetAll()
        {
            return await _applicantRepository.GetAll();
        }

        public async  Task<(bool isSuccess,string errorMessage)> Update(int id, ApplicantViewModel applicantViewModel)
        {
            if (await _applicantRepository.IsEmailExist(applicantViewModel.EmailAddress,id))
                return (false, Resource.Error_EmailDuplicated);
            bool isSuccess= await _applicantRepository.Update(id, applicantViewModel);
            return (isSuccess, string.Empty);
        }

        public async Task<bool> Delete(int id)
        {
            return await _applicantRepository.Delete(id);
        }
    }
}
