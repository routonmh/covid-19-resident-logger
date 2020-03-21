using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResidentLog.Models;
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
        /// <returns></returns>
        [HttpGet("types")]
        public async Task<List<Duty>> GetDutyTypes()
        {
            return await DutyModel.GetDutyTypes();
        }
    }
}