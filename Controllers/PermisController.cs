using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Mapper;
using UserApi.Models.Permis;
using UserApi.Settings;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWTSettings jwtSettings;

        public PermisController(UserApiContext context, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager, JWTSettings jwtSettings)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings;
        }

        [HttpGet]
        [Route("{DiscordId}")]
        public async Task<ActionResult<PermisDTO>> GetPermis([FromRoute] string DiscordId)
        {
            ApiUser? user = await userManager.FindByEmailAsync(DiscordId);
            if (user == null) return BadRequest("Aucun utilisateur existe avec cet Id");

            return user.ToPermisDto();
        }

        [HttpPost]
        [Route("addPoints/{DiscordId}")]
        public async Task<IActionResult> AddPoints([FromRoute] string DiscordId, [FromBody] ChangePointsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");

            //MAx 12 pts
            if (entity.Points + dto.Value > 12 ) entity.Points = 12;
            else entity.Points += dto.Value;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("removePoints/{DiscordId}")]
        public async Task<IActionResult> RemovePoints([FromRoute] string DiscordId, [FromBody] ChangePointsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");

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

        [HttpPut]
        [Route("setPoints/{DiscordId}")]
        public async Task<IActionResult> SetPoints([FromRoute] string DiscordId, [FromBody] ChangePointsDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");

            if (dto.Value < 0 || dto.Value > 12) return BadRequest("Nombres de points invalide");

            entity.Points = dto.Value;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("setPermis/{DiscordId}")]
        public async Task<IActionResult> SetPermis([FromRoute] string DiscordId, [FromBody] SetPermisDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");
            
            if (dto.Points < 0 || dto.Points > 12) return BadRequest("Nombres de points invalide");
            
            entity.Permis = dto.Permis;
            entity.Points = dto.Points;
            entity.NbSessionsPermis = dto.NbSessionsPermis;

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("upgradePermis/{DiscordId}")]
        public async Task<IActionResult> UpgradePermis([FromRoute] string DiscordId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");

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

        [HttpPost]
        [Route("retraitPermis/{DiscordId}")]
        public async Task<IActionResult> RemovePermis([FromRoute] string DiscordId, [FromBody] RetraitPermisDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApiUser? entity = await userManager.FindByEmailAsync(DiscordId);
            if (entity == null) return BadRequest("Aucun utilisateur existe avec cet Id");


            entity.Permis = PermisName.Retrait;
            entity.Points = 0;
            entity.NbSessionsPermis = dto.NbSessions;
            

            IdentityResult result = await userManager.UpdateAsync(entity);
            if (result.Succeeded) return Ok(entity.ToPermisDto());
            else return BadRequest(result.Errors);
        }
    }
}
