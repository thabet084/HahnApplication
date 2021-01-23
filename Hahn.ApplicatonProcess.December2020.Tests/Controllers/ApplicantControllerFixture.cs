using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Tests.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Hahn.ApplicatonProcess.Application.Controllers;
using Hahn.ApplicatonProcess.December2020.Services.Interfaces;
using Hahn.ApplicatonProcess.December2020.Services.Classes;
using Hahn.ApplicatonProcess.December2020.Data.Repositories.Interfaces;
using Hahn.ApplicatonProcess.December2020.Tests.InMemoryRepositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using Hahn.ApplicatonProcess.December2020.Shared.Resources;

namespace Hahn.ApplicatonProcess.December2020.Tests.Controllers
{
    [TestClass]
    public class ApplicantControllerFixture
    {
        IMapper _mapper;
        private ApplicantController _systemUnderTest;
        private IApplicantService _applicantService;
        private IApplicantRepository _applicantRepository;
        private ILogger<ApplicantController> _logger;

        [TestInitialize]
        public void OnInit()
        {

            _mapper = UnitTestUtility.CreateMapper();
            _applicantRepository = new InMemoryApplicantRepository(_mapper);
            _applicantService = new ApplicantService(_applicantRepository);
            _systemUnderTest = new ApplicantController(_applicantService, _logger);

        }

        /// <summary>
        /// Get all applicants
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task GetAllApplicants()
        {
            //Arrange


            //Act
            var okResult = (OkObjectResult)((await _systemUnderTest.GetAll()).Result);
            var result = (List<ApplicantViewModel>)okResult.Value;

            //Assert
            Assert.IsNotNull(result, "Error In getting data");
            Assert.IsTrue(result.Count == 1, "Count should be 1");
        }

        /// <summary>
        /// Get applicant with founded id
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task GetApplicant_IdFound()
        {
            //Arrange
            
            //Act
            var okResult =(OkObjectResult) ((await _systemUnderTest.Get(1)).Result);
            var result = (ApplicantViewModel)okResult.Value;
            
            //Assert
            Assert.IsNotNull(result, "Error In getting data");
            Assert.IsTrue(result.Id==1, "Id should be 1");
        }

        /// <summary>
        /// Get applicant with not founde id
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task GetApplicant_IdNotFound()
        {
            //Arrange

            //Act
            var notFoundResult = (NotFoundResult)((await _systemUnderTest.Get(10)).Result);

            //Assert
            Assert.IsNotNull(notFoundResult.StatusCode==404, "Should not found");
        }

        /// <summary>
        /// Add valid applicant 
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task AddApplicant_Valid()
        {
            //Arrange
            var applicantViewModel = UnitTestUtility.Get_ValidApplicant();

            //Act
            var result =(CreatedAtActionResult) await _systemUnderTest.Add(applicantViewModel);
           var model = (ApplicantViewModel)result.Value;
            
            //Assert
            Assert.IsNotNull(model, "Error In getting data");
            Assert.IsTrue(model.Name == "Johny", "Name should be Johny");
        }

        /// <summary>
        /// Add applicant with duplicated email
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task AddApplicant_InValid_DuplicatedEmail()
        {
            //Arrange
            var applicantViewModel = UnitTestUtility.Get_InValidApplicant_ExistedEmail();

            //Act
            var result = (BadRequestObjectResult)await _systemUnderTest.Add(applicantViewModel);

            //Assert
            Assert.IsNotNull(result.StatusCode==400, "Status code should be 400 bad request");
            Assert.IsNotNull(result.Value == Resource.Error_EmailDuplicated, "Emai is duplicated");
        }

        /// <summary>
        /// Add applicant with Name Length Less Than 5
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task AddApplicant_InValid_NameLengthLessThan5()
        {
            //Arrange
            var applicantViewModel = UnitTestUtility.Get_InValidApplicant_NameLengthLessThan5();

            //Act
            var result = (BadRequestObjectResult)await _systemUnderTest.Add(applicantViewModel);

            //Assert
            Assert.IsNotNull(result.StatusCode == 400, "Status code should be 400 bad request");
            Assert.IsNotNull(result.Value == Resource.Validation_MinLenght5, "Lenght is less than 5");
        }

        /// <summary>
        /// Delete applicant with valid id
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task AddApplicant_Delete_ValidId ()
        {
            //Arrange
  

            //Act
            var result = (NoContentResult)await _systemUnderTest.Delete(1);

            //Assert
            Assert.IsNotNull(result.StatusCode == 204, "Status code should be 201 no content request");
        }

        /// <summary>
        /// Delete applicant with in valid id
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task AddApplicant_Delete_InValidId()
        {
            //Arrange


            //Act
            var result = (NotFoundResult)await _systemUnderTest.Delete(50);

            //Assert
            Assert.IsNotNull(result.StatusCode == 404, "Status code should be 404 not found result");
        }

        /// <summary>
        /// Update applicant with  valid id
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task UpdateApplicant_ValidId()
        {
            //Arrange
            var applicantViewModel = UnitTestUtility.Get_ValidApplicantWithId();


            //Act
            var result =(NoContentResult)await _systemUnderTest.Update(1,applicantViewModel);

            //Assert
            Assert.IsNotNull(result.StatusCode == 204, "Status code should be 204 not found result");
        }

        /// <summary>
        /// Update applicant with in valid id
        /// </summary>
        /// <returns>void</returns>
        [TestMethod]
        public async Task UpdateApplicant_InValidId()
        {
            //Arrange
            var applicantViewModel = UnitTestUtility.Get_ValidApplicantWithId();


            //Act
            var result = (BadRequestResult)await _systemUnderTest.Update(30, applicantViewModel);

            //Assert
            Assert.IsNotNull(result.StatusCode == 400, "Status code should be 400");
        }

    }
}
