using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Models.Car;
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


    }
}
