using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Models.Auth;
using UserApi.Settings;

namespace UserApi.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTSettings jwtSettings;

        public AuthController(UserApiContext context, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("Initialize")]
        public async Task<IActionResult> Initialize()
        {
            var result = await DBInitializer.Initialize(_context, userManager, roleManager);
            var resultMessage = $"Initialisation DB : {(result ? "Succès" : "DB existe déja")}";

            return Ok(resultMessage);
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser userExists = await userManager.FindByNameAsync($"{dto.Prenom} {dto.Nom}");
            if (userExists != null) return BadRequest("L'utilisateur existe déjà");
            Stage? stage = await _context.Stage.FirstOrDefaultAsync(s => s.Name == StageName.NA);
            ApiUser user = new ApiUser
            {
                UserName = $"{dto.Prenom}{dto.Nom}",
                Email = dto.DiscordId,
                Prenom = dto.Prenom,
                Nom = dto.Nom,
                Sexe = dto.Sexe,
                CreatedAt = DateTime.Now,
                Argent = 0,
                Permis = PermisName.NA,
                IdStage = stage.StageId,
                Points = 0,
                NbSessions = 0,
                NbSessionsPermis = 0,
                NbSessionsPolice = 0,
            };

            IdentityResult? result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Visiteur);
            }
            else return BadRequest(result.Errors);

            string? registrationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            return Ok(registrationToken);
        }

        [HttpPost]
        [Route("ConfirmDiscord")]
        public async Task<IActionResult> ConfirmDiscord([FromBody] ConfirmDiscordDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser user = await userManager.FindByEmailAsync(dto.DiscordId);
            if (user == null) return BadRequest("Nom d'utilisateur invalide");

            IdentityResult? result = await userManager.AddPasswordAsync(user, dto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            if (user.EmailConfirmed) return BadRequest("Le compte discord est déjà validé");

            result = await userManager.ConfirmEmailAsync(user, dto.ConfirmationToken);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Le compte discord a été validé avec succès");
        }
    }
}
