using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Mapper;
using UserApi.Models.Money;
using UserApi.Settings;

namespace UserApi.Controllers
{
    /// <summary>
    /// Gestion de l'argent d'un user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly UserManager<ApiUser> userManager;

        public MoneyController(UserApiContext context, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Get la money d'un utilisateur
        /// </summary>
        /// <param name="DiscordId">Id de l'utilisateur</param>
        /// <response code="400 + Message"></response>
        /// <returns>Model money de l'utilisateur</returns>
        [HttpGet]
        [Route("{DiscordId}")]
        public async Task<ActionResult<MoneyDTO>> GetMoney([FromRoute] string DiscordId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
            if (user == null) return BadRequest("Aucun utilisateur existe avec cet Id");

            return user.ToMoneyDto();
        }

        /// <summary>
        /// Ajout d'une somme a un user
        /// </summary>
        /// <param name="DiscordId">Id de l'utilisateur</param>
        /// <param name="dto">Model d'ajout de money</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model money du user</response>
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

        /// <summary>
        /// retire la money d'un user
        /// </summary>
        /// <param name="DiscordId">user discord id</param>
        /// <param name="dto">Model changement de money</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model Argent user</response>
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

        /// <summary>
        /// Définis l'argent d'un utilisateur a une somme X
        /// </summary>
        /// <param name="DiscordId">discord id user</param>
        /// <param name="dto">Model changement money</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model money user</response>
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
