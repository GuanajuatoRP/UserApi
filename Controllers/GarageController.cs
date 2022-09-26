using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Mapper;
using UserApi.Models.Car;
using UserApi.Models.Garage;

namespace UserApi.Controllers
{
    /// <summary>
    /// Permet de contrôller les garrage des joueurs, intéragis avec la DB RôlePlay
    /// </summary>
    [Route("api/Garage")]
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly UserApiContext _userContext;
        private readonly UserManager<ApiUser> userManager;

        public GarageController(UserApiContext userContext, UserManager<ApiUser> userManager)
        {
            _userContext = userContext;
            this.userManager = userManager;
        }

        /// <summary>
        /// Ajoute une voiture a un utilisateur
        /// </summary>
        /// <param name="DiscordId">Discord id de l'utilisateur</param>
        /// <param name="dto">Voiture a ajouter au garage</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Confirmation ajout de la voiture</response>
        [HttpPost]
        [Route("add/{DiscordId}/{CarId}")]
        public async Task<IActionResult> AddCar([FromRoute] string DiscordId, [FromRoute] Guid CarId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
            if (user == null) return BadRequest("USER_NOT_FOUND");

            OriginalCar? originalCar = await _userContext.OriginalCars
                .FirstOrDefaultAsync(oc => oc.IdCar == CarId);

            if (originalCar == null) return BadRequest("Aucune voiture trouvé avec cette id");


            try
            {
                _userContext.Voitures.Add(originalCar.ToVoiture(user.Id));
                await _userContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest($"fail : {e.Message}");
                throw;
            }

            return Ok($"La voiture : {originalCar.Model}, viens d'être ajouté dans nos registre au nom de : {user.UserName}");
        }

        /// <summary>
        /// Obtenir une voiture d'un garage X par sa clé
        /// </summary>
        /// <param name="KeyCar">Clé de la voiture a trouvé</param>
        /// <response code="400 + Message"></response>
        /// <returns>Model de la voiture</returns>
        [HttpGet]
        [Route("{KeyCar}")]
        public async Task<ActionResult<CarDTO>> GetCar([FromRoute] Guid KeyCar)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Voitures? car = await _userContext.Voitures
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.KeyCar == KeyCar);

            OriginalCar? originalCar = await _userContext.OriginalCars
                                            .Include(c => c.Maker)
                                            .FirstOrDefaultAsync(c => c.IdCar == car.IdCar);


            return car.ToModel(originalCar.ToModel());
        }

        /// <summary>
        /// Get toute les voiture de tous les garrages
        /// </summary>
        /// <response code="400 + Message"></response>
        /// <returns>Liste des voiture de tous les garrages</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<CarDTO>>> GetAllCars()
        {
            List<Voitures> voitures = await _userContext.Voitures
                .Include(v => v.User)
                .ToListAsync();

            return voitures.Select(v =>
            {
                OriginalCar? originalCar = _userContext.OriginalCars
                                            .Include(c => c.Maker)
                                            .FirstOrDefault(c => c.IdCar == v.IdCar);
                return v.ToModel(originalCar.ToModel());
            }).ToList();
        }


        /// <summary>
        /// Get toute les voiture du garage d'un utilisateur
        /// </summary>
        /// <param name="DiscordId">Discord ID de l'utilisateur</param>
        /// <response code="400 + Message"></response>
        /// <returns>Liste des voiture du garage</returns>
        [HttpGet]
        [Route("all/{DiscordId}")]
        public async Task<ActionResult<List<CarDTO>>> GetCars([FromRoute] string DiscordId)
        {
            ApiUser? user = await _userContext.Users
                .Include(u => u.Voitures)
                .FirstOrDefaultAsync(u => u.Email == DiscordId);
            if (user == null) return BadRequest("USER_NOT_FOUND");

            List<CarDTO> carDto = new List<CarDTO>();


            foreach (Voitures car in user.Voitures)
            {
                var dto = await GetCar(car.KeyCar);
                if (dto == null) return BadRequest("Une erreur c'est produite");
                carDto.Add(dto.Value);
            }


            return carDto;


        }

        /// <summary>
        /// Edit une voiture d'un garage
        /// </summary>
        /// <param name="KeyCar">Clé de la voiture a edit</param>
        /// <param name="dto">Model de la voiture contenant les modifs</param>
        /// <response code="400 + Message"></response>
        /// <response code="404">La voiture n'est pas trouvé</response>
        /// <response code="200">La voiture a été modifier</response>
        [HttpPut]
        [Route("{KeyCar}")]
        public async Task<IActionResult> EditCar([FromRoute] Guid KeyCar, [FromBody] EditCarDTO dto)
        {
            if (KeyCar != dto.KeyCar) return BadRequest("L'id est != de celui du dto");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Voitures entity = await _userContext.Voitures.FirstOrDefaultAsync(e => e.KeyCar == KeyCar);

            if (entity == null) return BadRequest("Aucune voiture avec cette id");


            entity.PowerHp = dto.PowerHp;
            entity.WeightKG = dto.WeightKg;
            entity.DriveTrain = (DriveTrain)Enum.Parse(typeof(DriveTrain), dto.DriveTrain); ;
            entity.Class = (Class)Enum.Parse(typeof(Class), dto.Class);
            entity.Pi = dto.Pi;
            entity.OnRoad = dto.OnRoad;
            entity.Speed = dto.Speed;
            entity.Handling = dto.Handling;
            entity.Accelerate = dto.Accelerate;
            entity.Launch = dto.Launch;
            entity.Braking = dto.Braking;
            entity.Offroad = dto.Offroad;
            entity.Imatriculation = dto.Imatriculation ?? String.Empty;
            entity.PrixTotal = dto.TotalPrice;
            entity.PrixModif = dto.EditPrice ?? 0;


            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok($"Voiture Modifiée");
        }

        /// <summary>
        /// Suprime une voiture par sa clé
        /// </summary>
        /// <param name="KeyCar">Clé de la voiture a supprimer</param>
        /// <response code="204">La voiture a été supprimer</response>
        /// <response code="404">La voiture n'existe pas</response>
        [HttpDelete("{KeyCar}")]
        public async Task<IActionResult> DeleteCar([FromRoute] Guid KeyCar)
        {
            Voitures? car = await _userContext.Voitures.FirstOrDefaultAsync(v => v.KeyCar == KeyCar);
            if (car == null) return NotFound($"Aucune sessions trouvée avec l'id suivant {KeyCar}");

            _userContext.Voitures.Remove(car);
            await _userContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
