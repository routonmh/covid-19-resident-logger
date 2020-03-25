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
        public async Task<ResidentDTO> Get(int residentId)
        {
            Resident resident = await ResidentModel.GetResidentEntry(residentId);
            return Resident.ConvertToDTO(resident);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resident"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResidentDTO> Post([FromBody, Required] ResidentDTO resident)
        {
            List<TestResult> testResultTypes = await TestResultModel.GetTestResultTypes();
            Resident res = Resident.ConvertFromDTO(resident, testResultTypes);
            int residentId = await ResidentModel.CreateResidentEntry(res);
            resident.ResidentID = residentId;
            return resident;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPut("{residentId}")]
        public async Task<ActionResult> Put([FromRoute, Required] int residentId,
            [FromBody, Required] ResidentDTO resident)
        {
            List<TestResult> testResultTypes = await TestResultModel.GetTestResultTypes();
            Resident res = Resident.ConvertFromDTO(resident, testResultTypes);

            await ResidentModel.UpdateResidentEntry(residentId, res);
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

        [HttpGet("local-id-set")]
        public async Task<LocalIDSet<int>> LocalResidentIDs(int residentId)
        {
            return await ResidentModel.PreviousAndNextResidentIds(residentId);
        }
    }
}