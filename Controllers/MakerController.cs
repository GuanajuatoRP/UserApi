using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Models.Makers;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakerController : ControllerBase
    {
        private readonly UserApiContext _userContext;
        public MakerController(UserApiContext userContext)
        {
            _userContext = userContext;
        }


        [HttpPost]
        [Route("updateAllMakers")]
        public async Task<IActionResult> addOriginalCar([FromBody] List<CreateMakerDTO> dtos)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            List<Maker> makers = await _userContext.Makers.ToListAsync();
            List<Maker> newMakers = new List<Maker>();

            foreach (CreateMakerDTO dto in dtos)
            {
                Maker maker = makers.FirstOrDefault(m => dto.Name.ToUpper().Contains(m.Name.ToUpper()));
                if (maker == null)
                {
                    maker = new Maker
                    {
                        Name = dto.Name,
                        Origin = "NA"
                    };
                    newMakers.Add(maker);
                }
                else
                {
                    maker.Name = dto.Name;
                    maker.Origin = maker.Origin;
                }
            }

            try
            {
                _userContext.Makers.AddRange(newMakers);
                await _userContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


            return Ok();
        }
    }
}
