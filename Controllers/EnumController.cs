using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Models.Enum;

namespace UserApi.Controllers
{
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

        [HttpGet]
        [Route("PermisName")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetPemisName()
        {
            return Enum.GetValues(typeof(PermisName)).Cast<PermisName>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }
        [HttpGet]
        [Route("SessionType")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetSessionType()
        {
            return Enum.GetValues(typeof(SessionType)).Cast<SessionType>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }
        [HttpGet]
        [Route("StageName")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetStageName()
        {
            return Enum.GetValues(typeof(StageName)).Cast<StageName>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }
        [HttpGet]
        [Route("Aspiration")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetAspiration()
        {
            return Enum.GetValues(typeof(Aspiration)).Cast<Aspiration>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }
        [HttpGet]
        [Route("Class")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetClass()
        {
            return Enum.GetValues(typeof(Class)).Cast<Class>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }
        [HttpGet]
        [Route("CarType")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetCarType()
        {
            return Enum.GetValues(typeof(CarType)).Cast<CarType>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }
        [HttpGet]
        [Route("EnginePosition")]
        public async Task<ActionResult<List<EnumvalDTO>>> GetEnginePosition()
        {
            return Enum.GetValues(typeof(EnginePosition)).Cast<EnginePosition>()
                .Select(p => new EnumvalDTO { Index = (int)p, Name = p.ToString() }).ToList();
        }
    }
}
