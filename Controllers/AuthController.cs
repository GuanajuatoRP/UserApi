using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Web;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Models.Auth;
using UserApi.Settings;

namespace UserApi.Controllers
{
    /// <summary>
    /// Contrôlleur d'Auth et de gestion des utilisateurs
    /// </summary>
    //[Authorize]
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTSettings jwtSettings;
        private readonly RegistrationSettings registrationSettings;
        private readonly ApiToBotSettings apiToBotSettings;

        public AuthController(UserApiContext context,
                              UserManager<ApiUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              JWTSettings jwtSettings,
                              RegistrationSettings registrationSettings,
                              ApiToBotSettings apiToBotSettings)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings;
            this.registrationSettings = registrationSettings;
            this.apiToBotSettings = apiToBotSettings;
        }

        /// <summary>
        /// Initialise les table avec les rôles et l'utilisateur Admin
        /// </summary>
        /// <response code="200 + Message"></response>
        [AllowAnonymous]
        [HttpPost]
        [Route("Initialize")]
        public async Task<IActionResult> Initialize()
        {
            var result = await DBInitializer.Initialize(_context, userManager, roleManager);
            var resultMessage = $"Initialisation DB : {(result ? "Succès" : "DB existe déja")}";

            return Ok(resultMessage);
        }

        /// <summary>
        /// Permet de register un user dans la DB
        /// </summary>
        /// <param name="discordId">id discord de l'utilisateur</param>
        /// <param name="dto">Model de l'utilisateur</param>
        /// <response code="400 + Message"></response>
        /// <response code="200 + Message"></response>
        [AllowAnonymous]
        [HttpPost]
        [Route("register/{discordId}")]
        public async Task<IActionResult> Register([FromRoute] string discordId, [FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            ApiUser userExists = await userManager.FindByNameAsync($"{dto.Prenom}{dto.Nom}");
            if (userExists != null) return BadRequest("L'utilisateur existe déjà");

            Stage? stage = await _context.Stage.FirstOrDefaultAsync(s => s.Name == StageName.NA);
            ApiUser user = new ApiUser
            {
                UserName = $"{dto.Prenom}_{dto.Nom}",
                Email = dto.DiscordId,
                EmailConfirmed = false,
                Prenom = dto.Prenom,
                Nom = dto.Nom,
                Sexe = dto.Sexe,
                IdStage = stage.StageId,
                Argent = registrationSettings.defaultMoney,
                CreatedAt = DateTime.Now,
                Permis = PermisName.NA,
                Points = 0,
                NbSessions = 0,
                NbSessionsPermis = 0,
                NbSessionsPolice = 0,
            };

            IdentityResult? result = await userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded == false) return BadRequest(result.Errors); 



            string registrationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            registrationToken = HttpUtility.UrlEncode(registrationToken);

            EmailConfirmationTokenDTO tokenDTO = new() { token = registrationToken };

            //discord valide token stp :D
            //var url = $"{apiToBotSettings.baseURI}sendRegisterValidationButton/{user.Email}";
            var url = $"http://bot.guanajuato-roleplay.fr/sendRegisterValidationButton/{user.Email}";
            HttpClient client = new();
            string json = JsonSerializer.Serialize(tokenDTO);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, data);

            return Ok("L'utilisateur a été crée et est en attente de validation");
        }

        /// <summary>
        /// Permet de valider un compte user
        /// </summary>
        /// <param name="discordId">id discord de l'utilisateur</param>
        /// <param name="token">Token de validation du compte</param>
        [AllowAnonymous]
        [HttpPost]
        [Route("register/validation/{discordId}")]
        public async Task<IActionResult> ValidationRegister([FromRoute] string discordId, [FromBody] ValidationRegistrationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest("qqsdqsfsf"+ModelState);

            if (discordId != dto.discordId) return BadRequest("DiscordIds do not match");

            ApiUser user = await userManager.FindByEmailAsync(dto.discordId);
            if (user == null) return BadRequest("L'utilisateur n'existe pas");

            IdentityResult registrationToken = await userManager.ConfirmEmailAsync(user, dto.token);

            if (registrationToken.Succeeded) await userManager.AddToRoleAsync(user, Roles.User);
            else return BadRequest("aaaaa" + registrationToken.Errors);


            userValidatedOnDbDTO userValidatedOnDbDTO = new() { userId = user.Id, discordId = user.Email };

            var url = $"{apiToBotSettings.baseURI}userValidatedOnDB/{user.Id}";

            string json = JsonSerializer.Serialize(userValidatedOnDbDTO);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new();
            await client.PostAsync(url, data);

            return Ok();
        }



        /// <summary>
        /// Permet de login un user dans la DB
        /// </summary>
        /// <param name="dto">Model de login d'un user</param>
        /// <response code="400 + Message"></response>
        /// <response code="401">Erreur de mdp ou id</response>
        /// <response code="200">Token + date d'expiration</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? user = await userManager.FindByNameAsync(dto.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, dto.Password))
            {
                IList<string>? userRoles = await userManager.GetRolesAsync(user);

                List<Claim> authClaims = new List<Claim>
                {
                    new Claim("Name", user.UserName),
                    new Claim("DiscordId", user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim("Roles", userRole));
                }

                SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret));

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddMinutes(jwtSettings.DurationTime),
                    Issuer = jwtSettings.ValidIssuer,
                    Audience = jwtSettings.ValidAudience,
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                //JwtSecurityToken token = new JwtSecurityToken(
                //    issuer: JWTSettings.ValidIssuer,
                //    audience: JWTSettings.ValidAudience,
                //    expires: DateTime.Now.AddMinutes(jwtSettings.DurationTime),
                //    claims: authClaims,
                //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                //);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    exipration = token.ValidTo
                });
            }
            else return Unauthorized();
        }


         /// <summary>
         /// Teste la validiter d'un token
         /// </summary>
         /// <param name="token">token a check</param>
         /// <response code="401">Token non valide || Pas la permission d'acceder a cette endpoint</response>
         /// <response code="200">Token valide</response>
         [HttpPost]
         [Route("TokenTest")]
         public async Task<IActionResult> TokenTest([FromBody] string token)
         {
             SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret));

             var tokenHandler = new JwtSecurityTokenHandler();
             try
             {
                 tokenHandler.ValidateToken(token, new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidIssuer = jwtSettings.ValidIssuer,
                     ValidAudience = jwtSettings.ValidAudience,
                     IssuerSigningKey = authSigningKey
                 }, out SecurityToken validatedToken);
             }
             catch
             {
                 return Unauthorized();
             }
             return Ok();
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


         /// <summary>
         /// Check si l'utilisateur existe dans la DB
         /// </summary>
         /// <param name="DiscordId">discord id de l'utilisateur a check</param>
         /// <response code="204">l'utilisateur n'existe pas</response>
         /// <response code="200">Username de l'utilisateur</response>
         [HttpGet]
         [Route("UserExist/{DiscordId}")]
         public async Task<IActionResult> UserExist([FromRoute] string DiscordId)
         {
             ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
             if (user == null) return NoContent();
             else return Ok(user.UserName);
         }

         /// <summary>
         /// Permet de suprimer un utilisateur
         /// </summary>
         /// <param name="DiscordId">discord id de l'utilisateur a delete</param>
         /// <response code="404 + Message"></response>
         /// <response code="204">Utilisateur suprimer</response>
         [HttpDelete]
         [Route("DeleteUser/{DiscordId}")]
         public async Task<IActionResult> DeleteUser([FromRoute] string DiscordId)
         {
             ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
             if (user != null) await userManager.DeleteAsync(user);
             else return NotFound("Aucun user avec cet id");

             return NoContent();
         }

         /// <summary>
         /// Crée une requette pour avoir un token de changement de mot de pase
         /// </summary>
         /// <param name="dto">Model de changement de mot de passe</param>
         /// <response code="400 + Message"></response>
         /// <response code="200">Token permettant de changer de mot de passe</response>
         [HttpPost]
         [Route("FrogotPassword")]
         public async Task<IActionResult> FrogotPassword([FromBody] FrogotPasswordDTO dto)
         {
             if (!ModelState.IsValid) return BadRequest(ModelState);

             ApiUser? user = await userManager.FindByEmailAsync(dto.DiscordId);
             if (user == null || !(await userManager.IsEmailConfirmedAsync(user))) return BadRequest("Aucun utilisateur existe avec cet id");

             string? token = await userManager.GeneratePasswordResetTokenAsync(user);
             return Ok(token);
         }

         /// <summary>
         /// Reset un mot de passe avec un token de changement de mot de passe 
         /// </summary>
         /// <param name="dto">Model de changement de mot de passe</param>
         /// <response code="400 + Message"></response>
         /// <response code="200">Confirmation</response>
         [HttpPost]
         [Route("ResetPassword")]
         public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
         {
             if (!ModelState.IsValid) return BadRequest(ModelState);

             ApiUser? user = await userManager.FindByEmailAsync(dto.DiscordId);
             if (user == null) return BadRequest("Aucun utilisateur existe avec cet id");

             IdentityResult? result = await userManager.ResetPasswordAsync(user, dto.Token, dto.Password);
             if (!result.Succeeded) return BadRequest(result.Errors);

             return Ok("Le mot de passe a été modifié avec succès");
         }


     }

}
