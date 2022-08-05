using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Mapper;
using UserApi.Models.Car;
using UserApi.Models.Garage;
using UserApi.Settings;

namespace UserApi.Controllers
{
    /// <summary>
    /// Permet de contrôller les voiture Original
    /// </summary>
    [Route("api/Infos")]
    [ApiController]
    public class InfosController : ControllerBase
    {
        private readonly UserApiContext _userContext;

        public InfosController(UserApiContext userContext)
        {
            _userContext = userContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet("Makers")]
        public async Task<ActionResult<IEnumerable<string>>> GetListMakers()
        {
            return await _userContext.Makers.Select(m => m.Name).Distinct().ToListAsync();
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<string>>> GetListTypes()
        {
            return await _userContext.OriginalCars.Select(m => m.Type).Distinct().ToListAsync();
        }

        [HttpGet("Pays")]
        public async Task<ActionResult<IEnumerable<string>>> GetListPays()
        {
            return await _userContext.Makers.Where(p => !string.IsNullOrEmpty(p.Origin)).Select(m => m.Origin!).Distinct().ToListAsync();
        }
    }
}
