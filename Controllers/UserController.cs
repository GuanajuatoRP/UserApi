using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
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
        private readonly CarApiContext _carContext;
        private readonly UserManager<ApiUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTSettings jwtSettings;

        public UserController(UserApiContext userContext, CarApiContext carContext, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            _userContext = userContext;
            _carContext = carContext;
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
        [Route("/{DiscordId}")]
        public async Task<ActionResult<UserDTO>> GetUser([FromRoute] string DiscordId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? user = await _userContext.Users
                .Include(u => u.Voitures)
                .Include(u => u.Sessions)
                .Include(u => u.Stage)
                .FirstOrDefaultAsync(u => u.Email == DiscordId);

            if (user == null) return BadRequest("Aucun utilisateur trouver avec ce discordId");

            List<OriginalCarDTO> listOriginalCar = new();
            List<Guid> guids = user.Voitures.Select(v => v.IdCar).ToList();
            List<Car>? cars = await _carContext.Cars
                .Include(c => c.Maker)
                .Where(c => guids.Contains(c.IdCar))
                .ToListAsync();
            listOriginalCar = cars.Select(c => c.ToModel()).ToList();

            return user.ToUserModel(listOriginalCar);
        }
    }
}
