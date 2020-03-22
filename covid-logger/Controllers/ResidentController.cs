using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResidentLog.Models;
using ResidentLog.Models.DTOs;
using ResidentLog.Models.Entities;

namespace ResidentLog.Controllers
{
    [ApiController]
    [Route("api/resident")]
    public class ResidentController
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="residentId"></param>
        /// <returns></returns>
        [HttpGet("{residentId}")]
        public async Task<Resident> Get(int residentId)
        {
            return await ResidentModel.GetResidentEntry(residentId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resident"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody, Required] ResidentDTO residentDto)
        {
            List<TestResult> testResultTypes = await TestResultModel.GetTestResultTypes();
            Resident resident = Resident.ConvertDTO(residentDto, testResultTypes);
            await ResidentModel.CreateResidentEntry(resident);
            return new OkResult();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPut("{residentId}")]
        public async Task<ActionResult> Put([FromRoute, Required] int residentId,
            [FromBody, Required] Resident resident)
        {
            await ResidentModel.UpdateResidentEntry(residentId, resident);
            return new OkResult();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="residentId"></param>
        /// <returns></returns>
        [HttpDelete("{residentId}")]
        public async Task Delete([FromRoute, Required] int residentId)
        {
            await ResidentModel.DeleteResidentEntry(residentId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet("test-result-types")]
        public async Task<List<TestResult>> GetTestResultTypes()
        {
            return await TestResultModel.GetTestResultTypes();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet("duty-types")]
        public async Task<List<Duty>> GetDutyTypes()
        {
            return await DutyModel.GetDutyTypes();
        }
    }
}