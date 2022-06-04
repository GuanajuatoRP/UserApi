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

<<<<<<< HEAD
            ApiUser userExists = await userManager.FindByNameAsync($"{dto.Prenom}{dto.Nom}");
            if (userExists != null) return BadRequest("L'utilisateur existe déjà");

=======
            ApiUser userExists = await userManager.FindByNameAsync($"{dto.Prenom} {dto.Nom}");
            if (userExists != null) return BadRequest("L'utilisateur existe déjà");
>>>>>>> AuthController
            Stage? stage = await _context.Stage.FirstOrDefaultAsync(s => s.Name == StageName.NA);
            ApiUser user = new ApiUser
            {
                UserName = $"{dto.Prenom}{dto.Nom}",
                Email = dto.DiscordId,
<<<<<<< HEAD
                EmailConfirmed = true,
=======
>>>>>>> AuthController
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

<<<<<<< HEAD
            IdentityResult? result = await userManager.CreateAsync(user, dto.Password);
=======
            IdentityResult? result = await userManager.CreateAsync(user);
>>>>>>> AuthController
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Visiteur);
            }
            else return BadRequest(result.Errors);

<<<<<<< HEAD
            return Ok($"Le compte a été crée avec le username : {user.UserName}");
            //string? registrationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //return Ok(registrationToken);
        }

        //[HttpPost]
        //[Route("ConfirmDiscord")]
        //public async Task<IActionResult> ConfirmDiscord([FromBody] ConfirmDiscordDTO dto)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    ApiUser user = await userManager.FindByEmailAsync(dto.DiscordId);
        //    if (user == null) return BadRequest("Nom d'utilisateur invalide");

        //    if (user.EmailConfirmed) return BadRequest("Le compte discord est déjà validé");

        //    IdentityResult? result = await userManager.ConfirmEmailAsync(user, dto.ConfirmationToken);
        //    if (!result.Succeeded) return BadRequest(result.Errors);

        //    result = await userManager.AddPasswordAsync(user, dto.Password);
        //    if (!result.Succeeded) return BadRequest(result.Errors);

        //    return Ok("Le compte discord a été validé avec succès");
        //}

        [HttpGet]
        [Route("UserExist/{DiscordId}")]
        public async Task<IActionResult> UserExist([FromRoute] string DiscordId)
        {
            ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
            if (user == null) return BadRequest("Aucun utilisateur existe avec cet id");
            else return Ok(user.UserName);
        }

        [HttpDelete]
        [Route("DeleteUser/{DiscordId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string DiscordId)
        {
            ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
            if (user != null) await userManager.DeleteAsync(user);
            else return NotFound("Aucun user avec cet id");

            return NoContent();
=======
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
>>>>>>> AuthController
        }
    }
}
