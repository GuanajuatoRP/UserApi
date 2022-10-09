using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Mapper;
using UserApi.Models.Permis;
using UserApi.Settings;

namespace UserApi.Controllers
{
    /// <summary>
    /// Gère les permis et les stages des utilisateur
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PermisController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;

        public PermisController(UserApiContext context, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            _context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get les permis d'un user
        /// </summary>
        /// <param name="DiscordId">Discord Id de l'utilisateur</param>
        /// <response code="400 + Message"></response>
        /// <returns>Model permis</returns>
        [HttpGet]
        [Route("{DiscordId}")]
        public async Task<ActionResult<PermisDTO>> GetPermis([FromRoute] string DiscordId)
        {
            ApiUser? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == DiscordId);
            if (user == null) return BadRequest("USER_NOT_FOUND");


            return user.ToPermisDto();
        }

        /// <summary>
        /// Ajoute des points au permis d'un user
        /// </summary>
        /// <param name="DiscordId">discord id user</param>
        /// <param name="dto">Model changement de points</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model Permis</response>
        [HttpPost]
        [Route("addPoints/{DiscordId}")]
        public async Task<IActionResult> AddPoints([FromRoute] string DiscordId, [FromBody] ChangePointsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("USER_NOT_FOUND");

            //Max 12 pts
            if (entity.Points + dto.Value > 12 ) entity.Points = 12;
            else entity.Points += dto.Value;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        /// <summary>
        /// Retire des points a un user
        /// </summary>
        /// <param name="DiscordId">discord is user</param>
        /// <param name="dto">Model changement points</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model permis user</response>
        [HttpPost]
        [Route("removePoints/{DiscordId}")]
        public async Task<IActionResult> RemovePoints([FromRoute] string DiscordId, [FromBody] ChangePointsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("USER_NOT_FOUND");

            //if 0 pts pas de retrait possible

            if (entity.Points - dto.Value <= 0)
            {
                entity.Permis = PermisName.NA;
                entity.Points = 0;
                entity.NbSessionsPermis = 0;
                
            } 
            else entity.Points -= dto.Value;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        /// <summary>
        /// Défini un nombre de point a un user
        /// </summary>
        /// <param name="DiscordId">discord is user</param>
        /// <param name="dto">Model changement points</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model permis user</response>
        [HttpPut]
        [Route("setPoints/{DiscordId}")]
        public async Task<IActionResult> SetPoints([FromRoute] string DiscordId, [FromBody] ChangePointsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("USER_NOT_FOUND");

            if (dto.Value < 0 || dto.Value > 12) return BadRequest("Nombres de points invalide");

            entity.Points = dto.Value;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        /// <summary>
        /// Défini un permis
        /// </summary>
        /// <param name="DiscordId">discord is user</param>
        /// <param name="dto">Model changement permis</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model permis user</response>
        [HttpPut]
        [Route("setPermis/{DiscordId}")]
        public async Task<IActionResult> SetPermis([FromRoute] string DiscordId, [FromBody] SetPermisDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("USER_NOT_FOUND");
            
            if (dto.Points < 0 || dto.Points > 12) return BadRequest("Nombres de points invalide");
            
            entity.Permis = dto.Permis;
            entity.Points = dto.Points;
            entity.NbSessionsPermis = dto.NbSessionsPermis;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        /// <summary>
        /// Upgrade de permis
        /// </summary>
        /// <param name="DiscordId">discord is user</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model permis user</response>
        [HttpGet]
        [Route("upgradePermis/{DiscordId}")]
        public async Task<IActionResult> UpgradePermis([FromRoute] string DiscordId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("USER_NOT_FOUND");

            //Get length of enum and remove one 
            int enumLen = Enum.GetValues(typeof(PermisName)).Length -1;
            //test if actual permis of user is the last permis
            if ((int)entity.Permis == enumLen) return BadRequest("Vous avez déjà le dernier permis");

            //si upgrate proba -> def == pts *2 
            if (entity.Permis == PermisName.Probatoire) entity.Points += entity.Points;
            //Si upgrade no permis -> proba == 6pts et 6 nbsessions permis
            if (entity.Permis == PermisName.NA)
            {
                entity.Points = 6;
                entity.NbSessionsPermis = 6;
            }


            //Upgrade permis to the next level
            entity.Permis = (PermisName)((int)entity.Permis + 1);
            

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        /// <summary>
        /// retire un permis
        /// </summary>
        /// <param name="DiscordId">discord is user</param>
        /// <param name="dto">Model retait de permis</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model permis user</response>
        [HttpPost]
        [Route("retraitPermis/{DiscordId}")]
        public async Task<IActionResult> RemovePermis([FromRoute] string DiscordId, [FromBody] RetraitPermisDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("USER_NOT_FOUND");


            entity.Permis = PermisName.Retrait;
            entity.Points = 0;
            entity.NbSessionsPermis = dto.NbSessions;
            

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        /// <summary>
        /// Upgrade de stage
        /// </summary>
        /// <param name="DiscordId">discord is user</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model permis user</response>
        [HttpGet]
        [Route("upgradeStage/{DiscordId}")]
        public async Task<IActionResult> UpgradeStage([FromRoute] string DiscordId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == DiscordId);
            if (entity == null) return BadRequest("USER_NOT_FOUND");

            //Get length of enum and remove one 
            int enumLen = Enum.GetValues(typeof(StageName)).Length -1;
            
            //test if actual Stage of user is the last Stage
            if ((int)entity.Stage == enumLen) return BadRequest("Vous avez déjà le dernier stage");

            if (entity.Permis == PermisName.Retrait || entity.Permis == PermisName.NA)
                return BadRequest("Vous n'avez pas le permis. Vous ne pouvez pas améliorer votre stage");

            //Get le stage n+1
            entity.Stage = (StageName)((int)entity.Stage + 1);


            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        /// <summary>
        /// retait de stage
        /// </summary>
        /// <param name="DiscordId">discord is user</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Model permis user</response>
        [HttpGet]
        [Route("retraitStage/{DiscordId}")]
        public async Task<IActionResult> RemoveStage([FromRoute] string DiscordId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == DiscordId);
            if (entity == null) return BadRequest("USER_NOT_FOUND");

            if (entity.Stage == StageName.NA) return BadRequest("Il ny a plus de stage a remove ici :(");

            entity.Stage = (StageName)((int)entity.Stage - 1);


            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }
    }
}
