using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Mapper;
using UserApi.Models.Money;
using UserApi.Settings;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly UserManager<ApiUser> userManager;

        public MoneyController(UserApiContext context, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            this.userManager = userManager;
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
