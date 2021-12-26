using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Model.Common;
using Api.Model.Models;
using Api.Model.ViewModels;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PatientController : BaseController
    {
        private readonly IPatientService _patient;
        public PatientController(IPatientService patientService)
        {
            this._patient = patientService;
        }

        // GET: api/<controller>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientListPaged(Guid id)
        {
            var result = await _patient.GetPatientByIdAsync(id);
            return await GetResult(result);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetPatientListPaged([FromBody] PaginationSortAndSearchInfo paginationSortAndSearchInfo)
        {
            var result = await _patient.GetPatientsAsync(paginationSortAndSearchInfo);
            return await GetResult(result);
        }

        // POST api/<controller>
        [HttpPost("Add")]
        public async Task<IActionResult> AddPatient([FromBody] PatientDto dto)
        {
            var result = await _patient.AddPatientAsync(dto);
            return await GetResult(result);
        }

        // POST api/<controller>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] PatientDto dto)
        {
            var result = await _patient.UpdatePatientAsync(id, dto);
            return await GetResult(result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var result = await _patient.DeletePatientAsync(id);
            return await GetResult(result);
        }
    }
}
