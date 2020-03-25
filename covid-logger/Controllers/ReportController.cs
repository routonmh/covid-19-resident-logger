using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResidentLog.Models.Reports;
using ResidentLog.Models.Reports.Entities;

namespace ResidentLog.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController
    {
        [HttpGet("all-residents")]
        public async Task<List<ResidentReportItem>> GetAllResidents([Required, FromQuery] string sortByFieldName,
            [Required, FromQuery] bool asc)
        {
            return await ResidentReportModel.GetAllResidents(sortByFieldName, asc);
        }

        [HttpGet("assignments")]
        public async Task<List<ResidentReportItem>> GetAssignments([Required, FromQuery] string sortByFieldName,
            [Required, FromQuery] bool asc)
        {
            return await ResidentReportModel.GetAssignments(sortByFieldName, asc);
        }

        [HttpGet("available")]
        public async Task<List<ResidentReportItem>> GetAvailableResidents([Required, FromQuery] string sortByFieldName,
            [Required, FromQuery] bool asc)
        {
            return await ResidentReportModel.GetAvailableResidents(sortByFieldName, asc);
        }

        [HttpGet("on-duty")]
        public async Task<List<ResidentReportItem>> GetOnDutyResidents([Required, FromQuery] string sortByFieldName,
            [Required, FromQuery] bool asc)
        {
            return await ResidentReportModel.GetOnDutyResidents(sortByFieldName, asc);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sortByFieldName"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        [HttpGet("quarantined")]
        public async Task<List<ResidentReportItem>> GetQurantinedResidents([Required, FromQuery] string sortByFieldName,
            [Required, FromQuery] bool asc)
        {
            return await ResidentReportModel.GetQuarantinedResidents(sortByFieldName, asc);
        }
    }
}