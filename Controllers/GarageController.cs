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
        private readonly CarApiContext _carContext;
        private readonly UserManager<ApiUser> userManager;

        public GarageController(UserApiContext userContext, CarApiContext carContext, UserManager<ApiUser> userManager)
        {
            _userContext = userContext;
            _carContext = carContext;
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
        [Route("add/{DiscordId}")]
        public async Task<IActionResult> AddCar([FromRoute] string DiscordId, [FromBody] AddCarDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
            if (user == null) return BadRequest("Aucun utilisateur existe avec cet Id");
            string queryString = @$"SELECT Id_Car, Model, Power_BHP, Power_KW, Torque_LBFT,
                Torque_NM, Weight_LBS, Weight_KG, EngineDisplacement, NbCylindre,
                Aspiration, EnginePosition, Speed,
                Handling, Accelerate, Launch, Braking, Offroad, PI, Class, Price FROM Car
                WHERE Id_Car = '{dto.CarId}'";

            Voitures? car = null;
            string? model = null;
            string connectionString = @"server=172.17.0.2,1433;database=Cars;User Id=sa;Password=*5273%0&Q9%8q!3@#^1#";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        model = reader["Model"].ToString();
                        car = new Voitures
                        {
                            IdUser = user.Id,
                            IdCar = Guid.Parse(reader["Id_Car"].ToString()),
                            Power_BHP = Convert.ToInt32(reader["Power_BHP"]),
                            Power_KW = Convert.ToInt32(reader["Power_KW"]),
                            Torque_LBFT = Convert.ToInt32(reader["Torque_LBFT"]),
                            Torque_NM = Convert.ToInt32(reader["Torque_NM"]),
                            Weight_LBS = Convert.ToInt32(reader["Weight_LBS"]),
                            Weight_KG = Convert.ToInt32(reader["Weight_KG"]),
                            EngineDisplacement = Convert.ToDecimal(reader["EngineDisplacement"]),
                            NbCylindre = Convert.ToInt32(reader["NbCylindre"]),
                            EnginePosition = (EnginePosition)Enum.Parse(typeof(EnginePosition), reader["EnginePosition"].ToString()),
                            Aspiration = (Aspiration)Enum.Parse(typeof(Aspiration), reader["Aspiration"].ToString()),
                            PrixModif = 0,
                            PrixTotal = Convert.ToInt32(reader["Price"]),
                            Speed = Convert.ToDecimal(reader["Speed"]),
                            Handling = Convert.ToDecimal(reader["Handling"]),
                            Accelerate = Convert.ToDecimal(reader["Accelerate"]),
                            Launch = Convert.ToDecimal(reader["Launch"]),
                            Braking = Convert.ToDecimal(reader["Braking"]),
                            Offroad = Convert.ToDecimal(reader["Offroad"]),
                            Pi = Convert.ToInt32(reader["PI"]),
                            Class = (Class)Enum.Parse(typeof(Class), reader["Class"].ToString()),
                            Imatriculation = "",
                        };
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            if (car == null) return BadRequest("Aucune voiture trouvé avec cette id");
            

            try
            {
                _userContext.Voitures.Add(car);
                await _userContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

            return Ok($"La voiture : {model}, viens d'être ajouté dans nos registre au nom de : {user.UserName}");
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

            Car? originalCar = await _carContext.Cars
                                            .Include(c => c.Maker)
                                            .FirstOrDefaultAsync(c => c.IdCar == car.IdCar);


            return car.ToModel(originalCar.ToModel());
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
            if (user == null) return BadRequest("Aucun utilisateur existe avec cet id");

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

            
            entity.Power_BHP = dto.Power_BHP;
            entity.Power_KW = dto.Power_KW;
            entity.Torque_LBFT = dto.Torque_LBFT;
            entity.Torque_NM = dto.Torque_NM;
            entity.Weight_LBS = dto.Weight_LBS;
            entity.Weight_KG = dto.Weight_KG;
            entity.EngineDisplacement = dto.EngineDisplacement;
            entity.NbCylindre = dto.NbCylindre;
            entity.EnginePosition = dto.EnginePosition;
            entity.PrixTotal += dto.PrixModif;
            entity.PrixModif += dto.PrixModif;
            entity.Aspiration = dto.Aspiration;
            entity.Speed = dto.Speed;
            entity.Handling = dto.Handling;
            entity.Accelerate = dto.Accelerate;
            entity.Launch = dto.Launch;
            entity.Braking = dto.Braking;
            entity.Offroad = dto.Offroad;
            entity.Pi = dto.Pi;
            entity.Class = dto.Class;
            entity.Imatriculation = dto.Imatriculation;


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
