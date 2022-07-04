using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Access;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Mapper;
using UserApi.Models.Auth;
using UserApi.Models.Car;
using UserApi.Models.User;
using UserApi.Settings;

namespace UserApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTSettings jwtSettings;

        public UserController(UserApiContext context, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/{DiscordId}")]
        public async Task<ActionResult<UserDTO>> GetUSer([FromRoute] string DiscordId)
        {
            ApiUser? user = await _context.Users
                .Include(u => u.Voitures)
                .Include(u => u.Sessions)
                .Include(u => u.Stage)
                .FirstOrDefaultAsync(u => u.Email == DiscordId);

            if (user == null) return BadRequest("Aucun utilisateur trouver avec ce discordId");

            List<OriginalCarDTO> listOriginalCar = new();
            try
            {
                listOriginalCar = await CarApi.GetCarsByIds(user.Voitures.Select(v => v.IdCar).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return user.ToUserModel(listOriginalCar);
        }
    }
}
