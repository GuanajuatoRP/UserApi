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
    [Route("api/OriginalCar")]
    [ApiController]
    public class OriginalCarController : ControllerBase
    {
        private readonly UserApiContext _userContext;

        public OriginalCarController(UserApiContext userContext)
        {
            _userContext = userContext;
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<OriginalCarDTO>>> Search([FromQuery] OriginalCarSearchModel searchModel)
        {
            List<OriginalCar> originalCars = await _userContext.OriginalCars
                .Include(c => c.Maker)
                .Where(c => string.IsNullOrWhiteSpace(searchModel.marque) || c.Maker.Name == searchModel.marque)
                .Where(c => string.IsNullOrWhiteSpace(searchModel.pays) || c.Maker.Origin == searchModel.pays)
                .Where(c => string.IsNullOrWhiteSpace(searchModel.type) || c.Type == searchModel.type)
                .Where(c => string.IsNullOrWhiteSpace(searchModel.modele) || c.Model.Contains(searchModel.modele))
                .ToListAsync();

            return originalCars.Select(c => c.ToModel()).ToList();
        }

        [HttpGet("SearchDiscord")]
        public async Task<ActionResult<IEnumerable<OriginalCarDTO>>> SearchDiscord([FromQuery] string? searchModel, [FromQuery] int limit = 25)
        {
            if (searchModel == null) return Ok(new List<OriginalCarDTO>());
            
            var seachTerms = searchModel?.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var query = _userContext.OriginalCars
                .Include(c => c.Maker)
                .Where(c => c.OnRoad);

            foreach (var searchValue in seachTerms)
            {
                var searchValue2 = searchValue.Trim('(', ')');
                query = query.Where(c => c.Maker.Name.Contains(searchValue2) ||
                c.Model.Contains(searchValue2) ||
                c.Year.ToString().Contains(searchValue2) ||
                c.Class.Contains(searchValue2) ||
                c.Pi.ToString().Contains(searchValue2));
            }

            var originalCars = await query.Take(limit).ToListAsync();


            return originalCars.Select(c => c.ToModel()).ToList();
        }

        /// <summary>
        /// Get toute les voiture d'origine
        /// </summary>
        /// <returns>Liste des voitures</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OriginalCarDTO>>> GetOriginalCars([FromQuery] int limit = 10)
        {
            List<OriginalCar> originalCars = await _userContext.OriginalCars
                .Include(c => c.Maker)
                .Take(limit)
                .ToListAsync();

            return originalCars.Select(c => c.ToModel()).ToList();
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

            List<OriginalCar>? cars = await _userContext.OriginalCars
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

            OriginalCar? car = await _userContext.OriginalCars
                .Include(c => c.Maker)
                .FirstOrDefaultAsync(c => c.IdCar == id);

            if (car == null)
            {
                return NotFound();
            }

            return car.ToModel();

        }



        [HttpPost]
        [Route("addOriginalCar")]
        public async Task<IActionResult> addOriginalCar([FromBody] IEnumerable<CreateOriginalCarDTO> dtos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            foreach (CreateOriginalCarDTO dto in dtos)
            {
                OriginalCar? originalCar = await _userContext.OriginalCars
                 .FirstOrDefaultAsync(oc => oc.CarOrdinal == dto.CarOrdinal);

                if (originalCar != null) Console.WriteLine($"La voiture {dto.Maker}, {dto.Model}, {dto.Year}, {dto.CarOrdinal} existe déja");
                else 
                {
                    Maker? maker = await _userContext.Makers.FirstOrDefaultAsync(m => m.Name == dto.Maker);

                    if (maker == null) return BadRequest($"Maker {dto.Maker} not found");
                    else
                    {
                            originalCar = dto.ToEntity(maker);
                        try
                        {
                            _userContext.OriginalCars.Add(originalCar);
                            await _userContext.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            return BadRequest($"fail : {e.Message}");
                            throw;
                        }
                    }
                }               
            }


            return Ok();
        }
    }
    public record OriginalCarSearchModel(string? marque, string? pays, string? type, string? modele);
}
