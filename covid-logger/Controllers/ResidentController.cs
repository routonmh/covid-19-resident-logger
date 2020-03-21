using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResidentLog.Models;
using ResidentLog.Models.Entities;

namespace ResidentLog.Controllers
{
    [ApiController]
    [Route("api/resident")]
    public class ResidentController
    {
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
        public async Task<ActionResult> Post([FromBody, Required] Resident resident)
        {
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

        [HttpDelete("{residentId}")]
        public async Task Delete([FromRoute, Required] int residentId)
        {
            await ResidentModel.DeleteResidentEntry(residentId);
        }
    }
}