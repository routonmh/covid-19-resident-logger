using System;
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
    [Route("api/duty")]
    public class DutyController
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dutyAssignmentDto"></param>
        /// <returns></returns>
        [HttpPost("assign")]
        public async Task AssignDuty([Required, FromBody] DutyAssignmentDTO dutyAssignmentDto)
        {
            List<Duty> dutyTypes = await DutyModel.GetDutyTypes();
            dutyAssignmentDto.DateAssigned = DateTime.Now;
            DutyAssignment dutyAssignment = DutyAssignment.ConvertDTO(dutyAssignmentDto, dutyTypes);
            await DutyModel.AssignDuty(dutyAssignment);
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