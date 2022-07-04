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
    /// Permet de contrôller tou
    /// </summary>
    [Route("api/Garage")]
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

        [HttpGet]
        [Route("Original")]
        public async Task<ActionResult<IEnumerable<GetOriginalCarListDTO>>> GetOriginalCars()
        {
            List<Car>? cars = await _carContext.Cars
                .Include(c => c.Maker)
                .Take(30)
                .ToListAsync();

            return cars.Select(c => c.ToModel()).ToList();
        }

        // GET: api/Cars
        [HttpPost]
        [Route("Original/CarsIds")]
        public async Task<ActionResult<IEnumerable<GetOriginalCarListDTO>>> GetOriginalCarsIds([FromBody] List<Guid> guids)
        {
            List<Car>? cars = await _carContext.Cars
                .Include(c => c.Maker)
                .Where(c => guids.Contains(c.IdCar))
                .ToListAsync();

            return cars.Select(c => c.ToModel()).ToList();
        }

        // GET: api/Cars/5
        [HttpGet]
        [Route("Original/{id}")]
        public async Task<ActionResult<GetOriginalCarListDTO>> GetOriginalCar(Guid id)
        {
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
