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
    [Route("api/Garage")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;

        public CarController(UserApiContext context, UserManager<ApiUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

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
                _context.Voitures.Add(car);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

            return Ok($"La voiture : {model}, viens d'être ajouté dans nos registre au nom de : {user.UserName}");
        }

        [HttpGet]
        [Route("{KeyCar}")]
        public async Task<ActionResult<CarDTO>> GetCar([FromRoute] Guid KeyCar)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Voitures? car = await _context.Voitures
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.KeyCar == KeyCar);

            if (car == null) return BadRequest("Aucune voiture existe avec cet Id");

            string queryString = @$"SELECT * FROM Car WHERE Id_Car = '{car.IdCar}'";
            string connectionString = @"server=172.17.0.2,1433;database=Cars;User Id=sa;Password=*5273%0&Q9%8q!3@#^1#";

            OriginalCarDTO? originalCar = null;




            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        originalCar = new OriginalCarDTO
                        {
                            IdCar = Guid.Parse(reader["Id_Car"].ToString()),
                            CarId = Convert.ToInt32(reader["CarId"]),
                            CarOrdinal = Convert.ToInt32(reader["CarOrdinal"]),
                            Maker = reader["Maker_Id"].ToString(),
                            Model = reader["Model"].ToString(),
                            Year = Convert.ToInt32(reader["Year"]),
                            Transmission = reader["Transmission"].ToString(),
                            GearBox = Convert.ToInt32(reader["GearBox"]),
                            EngineConfiguration = reader["EngineConfiguration"].ToString(),
                            Type = reader["Type"].ToString(),
                            Rarity = reader["Rarity"].ToString(),
                            WikiLink = reader["WikiLink"].ToString(),
                            PictureLink = reader["PictureLink"].ToString(),

                            PowerBhp = Convert.ToInt32(reader["Power_BHP"]),
                            PowerKw = Convert.ToInt32(reader["Power_KW"]),
                            TorqueLbft = Convert.ToInt32(reader["Torque_LBFT"]),
                            TorqueNm = Convert.ToInt32(reader["Torque_NM"]),
                            WeightLbs = Convert.ToInt32(reader["Weight_LBS"]),
                            WeightKg = Convert.ToInt32(reader["Weight_KG"]),
                            EngineDisplacement = Convert.ToDecimal(reader["EngineDisplacement"]),
                            NbCylindre = Convert.ToInt32(reader["NbCylindre"]),
                            EnginePosition = reader["EnginePosition"].ToString(),
                            Aspiration = reader["Aspiration"].ToString(),
                            Speed = Convert.ToDecimal(reader["Speed"]),
                            Handling = Convert.ToDecimal(reader["Handling"]),
                            Accelerate = Convert.ToDecimal(reader["Accelerate"]),
                            Launch = Convert.ToDecimal(reader["Launch"]),
                            Braking = Convert.ToDecimal(reader["Braking"]),
                            Offroad = Convert.ToDecimal(reader["Offroad"]),
                            Pi = Convert.ToInt32(reader["PI"]),
                            Price = Convert.ToInt32(reader["Price"]),
                            Class = reader["Class"].ToString(),
                        };
                    }
                }
                finally
                {
                    reader.Close();
                }


                queryString = @$"SELECT * FROM Maker WHERE Id_Maker = '{originalCar.Maker}'";
                command = new SqlCommand(queryString, connection);
                reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        originalCar.Maker = reader["Name"].ToString();
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            if (originalCar == null) return BadRequest("Une erreur c'est produite");

            return car.ToModel(originalCar);
        }

        [HttpGet]
        [Route("all/{DiscordId}")]
        public async Task<ActionResult<List<CarDTO>>> GetCars([FromRoute] string DiscordId)
        {
            ApiUser? user = await _context.Users
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

        [HttpPut]
        [Route("{KeyCar}")]
        public async Task<IActionResult> EditCar([FromRoute] Guid KeyCar, [FromBody] EditCarDTO dto)
        {
            if (KeyCar != dto.KeyCar) return BadRequest("L'id est != de celui du dto");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Voitures entity = await _context.Voitures.FirstOrDefaultAsync(e => e.KeyCar == KeyCar);

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
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    return NotFound();
            }

            return Ok($"Voiture Modifiée");
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{KeyCar}")]
        public async Task<IActionResult> DeleteCar([FromRoute] Guid KeyCar)
        {
            Voitures? car = await _context.Voitures.FirstOrDefaultAsync(v => v.KeyCar == KeyCar);
            if (car == null) return NotFound($"Aucune sessions trouvée avec l'id suivant {KeyCar}");

            _context.Voitures.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
