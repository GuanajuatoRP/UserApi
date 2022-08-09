using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Models.Enum;

namespace UserApi.Controllers
{
    /// <summary>
    /// Obtenir la liste des enums
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;

        public EnumController(UserApiContext context, UserManager<ApiUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Liste des permis
        /// </summary>
        [HttpGet]
        [Route("PermisName")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetPemisName()
        {
            return Enum.GetValues(typeof(PermisName)).Cast<PermisName>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }

        /// <summary>
        /// Liste des type de session
        /// </summary>
        [HttpGet]
        [Route("SessionType")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetSessionType()
        {
            return Enum.GetValues(typeof(SessionType)).Cast<SessionType>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }

        /// <summary>
        /// Liste des nom de stage
        /// </summary>
        [HttpGet]
        [Route("StageName")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetStageName()
        {
            return Enum.GetValues(typeof(StageName)).Cast<StageName>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }

        /// <summary>
        /// Liste des type d'aspiration (Turbo)
        /// </summary>
        [HttpGet]
        [Route("Aspiration")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetAspiration()
        {
            return Enum.GetValues(typeof(Aspiration)).Cast<Aspiration>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }

        /// <summary>
        /// Liste des class de voiture
        /// </summary>
        [HttpGet]
        [Route("Class")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetClass()
        {
            return Enum.GetValues(typeof(Class)).Cast<Class>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }

        /// <summary>
        /// Liste des type de voiture
        /// </summary>
        [HttpGet]
        [Route("CarType")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetCarType()
        {
            return Enum.GetValues(typeof(CarType)).Cast<CarType>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("EnginePosition")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetEnginePosition()
        {
            return Enum.GetValues(typeof(EnginePosition)).Cast<EnginePosition>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }
    }
}
