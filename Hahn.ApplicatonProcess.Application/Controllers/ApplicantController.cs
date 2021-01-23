using Hahn.ApplicatonProcess.Application.SwaggerSamples;
using Hahn.ApplicatonProcess.December2020.Services.Interfaces;
using Hahn.ApplicatonProcess.December2020.Shared.Validators;
using Hahn.ApplicatonProcess.December2020.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.Application.ActionFilters;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.Application.Controllers
{
    [Route("Applicant/[action]")]
    [ApiController]

    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly ILogger<ApplicantController> _logger;
        public ApplicantController(IApplicantService applicantService, ILogger<ApplicantController> logger)
        {
            _applicantService = applicantService;
            _logger = logger;
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(ApplicantViewModel), typeof(ApplicantAddModelExample))]
   
        public async Task<IActionResult> Add(ApplicantViewModel applicantViewModel)
        {
            try
            {
              

                var validator = new ApplicantValidator();
                var result = validator.Validate(applicantViewModel);
                if (!result.IsValid)
                    return BadRequest(result.Errors);

               var addTuple= await _applicantService.Add(applicantViewModel);

                if (string.IsNullOrEmpty(addTuple.errorMessage))
                    return CreatedAtAction(nameof(Get), new { id = addTuple.applicantViewModel.Id }, addTuple.applicantViewModel);
                else
                    return BadRequest(addTuple.errorMessage);


            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicantViewModel>> Get(int id)
        {
            var applicantViewModel = await _applicantService.Get(id);

            if (applicantViewModel == null)
            {
                return NotFound();
            }

            return Ok(applicantViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<ApplicantViewModel>> GetAll()
        {
            var applicantsViewModel = await _applicantService.GetAll();

            if (applicantsViewModel == null)
            {
                return NotFound();
            }

            return Ok(applicantsViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ApplicantViewModel applicantViewModel)
        {
            try
            {
                if (id != applicantViewModel.Id)
                {
                    return BadRequest();
                }

                var updateTuple = await _applicantService.Update(id, applicantViewModel);

                if (!updateTuple.isSuccess &&string.IsNullOrEmpty(updateTuple.errorMessage))
                {
                    return BadRequest(updateTuple.errorMessage);
                }
                else if (!updateTuple.isSuccess && !string.IsNullOrEmpty(updateTuple.errorMessage))
                    return NotFound();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex) 
            {
                return NotFound();
            }


           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _applicantService.Delete(id);

            if (!isSuccess)
            {
                return NotFound();
            }


            return NoContent();
        }

    }
}
