using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Mapper;
using UserApi.Models.Car;
using UserApi.Models.User;
using UserApi.Settings;

namespace UserApi.Controllers
{
    /// <summary>
    /// Gère 1 utilisateur
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserApiContext _userContext;
        private readonly UserManager<ApiUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTSettings jwtSettings;

        public UserController(UserApiContext userContext, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            _userContext = userContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Get un utilisateur avec son garage
        /// </summary>
        /// <param name="DiscordId">discord is user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("garrage/{DiscordId}")]
        public async Task<ActionResult<UserDTO>> GetUer([FromRoute] string DiscordId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? user = await _userContext.Users
                .Include(u => u.Voitures)
                .Include(u => u.Sessions)
                .FirstOrDefaultAsync(u => u.Email == DiscordId);

            if (user == null) return BadRequest("Aucun utilisateur trouver avec ce discordId");

            List<OriginalCarDTO> listOriginalCar = new();
            List<Guid> guids = user.Voitures.Select(v => v.IdCar).ToList();
            List<OriginalCar>? cars = await _userContext.OriginalCars
                .Include(c => c.Maker)
                .Where(c => guids.Contains(c.IdCar))
                .ToListAsync();
            listOriginalCar = cars.Select(c => c.ToModel()).ToList();

            return user.ToUserModel(listOriginalCar);
        }


        /// <summary>
        /// Get un user par son discord id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{DiscordId}")]
        public async Task<ActionResult<UserDTO>> GetUerByDiscordId([FromRoute] string DiscordId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? user = await _userContext.Users
                .Include(u => u.Voitures)
                .Include(u => u.Sessions)
                .FirstOrDefaultAsync(u => u.Email == DiscordId);

            if (user == null) return BadRequest("Aucun utilisateur trouver avec ce discordId");

            return user.ToUserDTO();
        }


        /// <summary>
        /// Get tous les users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUer()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            List<ApiUser>? users = await _userContext.Users
                .Include(u => u.Voitures)
                .Include(u => u.Sessions)
                .ToListAsync();


            return users.ToUserDTOList();
        }

        /// <summary>
        /// Edit un user avec l'admin pannel
        /// </summary>
        /// <param name="KeyCar">Clé de la voiture a edit</param>
        /// <param name="dto">Model de la voiture contenant les modifs</param>
        /// <response code="400 + Message"></response>
        /// <response code="404">La voiture n'est pas trouvé</response>
        /// <response code="200">La voiture a été modifier</response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditCar([FromRoute] string id, [FromBody] UserDTO dto)
        {
            if (id != dto.DiscordId) return BadRequest("L'id est != de celui du dto");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser entity = await _userContext.Users.FirstOrDefaultAsync(e => e.Email == id);

            if (entity == null) return BadRequest("Aucun user avec cet id");


            entity.Argent = dto.Argent;
            entity.Permis = (PermisName)Enum.Parse(typeof(PermisName), dto.Permis);
            entity.Stage = (StageName)Enum.Parse(typeof(StageName), dto.Stage);
            entity.Points = dto.Points;
            entity.NbSessionsPermis = dto.NbSessionsPermis;
            entity.NbSessionsPolice = dto.NbSessionsPolice;
            entity.NbSessions = dto.NbSessions;


            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok($"User Modifiée");
        }
    }
}
