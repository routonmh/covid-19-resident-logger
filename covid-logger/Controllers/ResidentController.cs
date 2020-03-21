using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResidentLog.Models.Entities;

namespace ResidentLog.Controllers
{
    [ApiController]
    [Route("api/resident")]
    public class ResidentController
    {
        [HttpGet("{residentId}")]
        public async Task<Resident> Get()
        {
            Resident resident = null;


            return resident;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resident"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromBody, Required] Resident resident)
        {

        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task Put([FromBody, Required] Resident resident)
        {

        }

        [HttpDelete("{residentId}")]
        public async Task Delete([FromRoute, Required] int residentId)
        {

        }

    }
}