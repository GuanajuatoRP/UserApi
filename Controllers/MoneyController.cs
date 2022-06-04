using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Mapper;
using UserApi.Models.Auth;
using UserApi.Models.Money;
using UserApi.Models.Response;
using UserApi.Settings;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTSettings jwtSettings;

        public MoneyController(UserApiContext context, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings;
        }

        [HttpGet]
        [Route("{DiscordId}")]
        public async Task<ActionResult<MoneyDTO>> GetMoney([FromRoute] string DiscordId)
        {
            ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
            if (user == null) return BadRequest("Aucun utilisateur existe avec cet Id");

            return user.ToMoneyDto();
        }

        [HttpPost]
        [Route("add/{DiscordId}")]
        public async Task<IActionResult> AddMoney([FromRoute] string DiscordId, [FromBody] ChangeAmountDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");

            entity.Argent += dto.Value;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToMoneyDto());
            else return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("remove/{DiscordId}")]
        public async Task<IActionResult> RemoveMoney([FromRoute] string DiscordId, [FromBody] ChangeAmountDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");

            entity.Argent -= dto.Value;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToMoneyDto());
            else return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("set/{DiscordId}")]
        public async Task<IActionResult> SetMoney([FromRoute] string DiscordId, [FromBody] ChangeAmountDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");

            entity.Argent = dto.Value;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToMoneyDto());
            else return BadRequest(result.Errors);
        }
    }
}
