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
    [Route("api/Car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarApiContext _carContext;
        private readonly UserManager<ApiUser> userManager;

        public CarController(CarApiContext carContext, UserManager<ApiUser> userManager)
        {
            _carContext = carContext;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get toute les voiture d'origine
        /// </summary>
        /// <returns>Liste des voitures</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OriginalCarDTO>>> GetOriginalCars()
        {
            List<Car>? cars = await _carContext.Cars
                .Include(c => c.Maker)
                .Take(30)
                .ToListAsync();

            return cars.Select(c => c.ToModel()).ToList();
        }

        /// <summary>
        /// Obtenir une liste de voiture en fonction de leurs ids
        /// </summary>
        /// <param name="guids">Liste des ids</param>
        /// <response code="400 + Message"></response>
        /// <returns>Liste des voittures</returns>
        [HttpPost]
        [Route("CarsIds")]
        public async Task<ActionResult<IEnumerable<OriginalCarDTO>>> GetOriginalCarsIds([FromBody] List<Guid> guids)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            List<Car>? cars = await _carContext.Cars
                .Include(c => c.Maker)
                .Where(c => guids.Contains(c.IdCar))
                .ToListAsync();

            return cars.Select(c => c.ToModel()).ToList();
        }

        /// <summary>
        /// Get une voiture par son ID
        /// </summary>
        /// <param name="id">id de la voiture </param>
        /// <response code="400 + Message"></response>
        /// <returns>Model de la voiture</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OriginalCarDTO>> GetOriginalCar(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Car? car = await _carContext.Cars
                .Include(c => c.Maker)
                .FirstOrDefaultAsync(c => c.IdCar == id);

            if (car == null)
            {
                return NotFound();
            }

            return car.ToModel();

        }
    }
}
