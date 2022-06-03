using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Mapper;
using UserApi.Models.Stages;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : ControllerBase
    {
        private readonly UserApiContext _context;

        public StagesController(UserApiContext context)
        {
            _context = context;
        }

        // GET: api/Stages
        [HttpGet]
        public async Task<ActionResult<List<ListStagesDTO>>> GetStage()
        {
            List<Stage> stages = await _context.Stage.ToListAsync();

            return stages.Select(s => s.ToModelList()).ToList();
        }

        // GET: api/Stages/5
        [HttpGet("{id}")]

        public async Task<ActionResult<StageDTO>> GetStage([FromRoute] Guid id, bool withUser = false)
        {
            Stage? stage = null;
            if (withUser) stage = await _context.Stage
                    .Include(s => s.Users)
                    .FirstOrDefaultAsync(s => s.StageId == id);
            else stage = await _context.Stage.FindAsync(id);
            

            if (stage == null) return NotFound();

            return stage.ToModel();
        }

        // PUT: api/Stages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStage([FromRoute] Guid id, [FromBody] EditStageDTO dto)
        {
            if (id != dto.StageId) return BadRequest("L'id est != de celui du dto");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stage? entity = await _context.Stage
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.StageId == id);

            if (entity == null) return NotFound("Aucun stage n'existe avec cet id");

            
            entity.Name = dto.Name;
            entity.PermisRequis = dto.PermisRequis;
            entity.StageRequis = dto.StageRequis;
            entity.NbSessionsRequis = dto.NbSessionsRequis;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExistsId(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Id du stage modifier : {id}");
        }

        // POST: api/Stages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostStage([FromBody] CreateStageDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stage stage = new Stage
            {
                Name = dto.Name,
                PermisRequis = dto.PermisRequis,
                StageRequis = dto.StageRequis,
                NbSessionsRequis = dto.NbSessionsRequis
            };

            try
            {
                _context.Stage.Add(stage);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StageExistsName(stage.Name.ToString())) Conflict("Le stage existe déjàs");
                throw;
            }

            return Ok($"Id du nouveau stage : {stage.StageId}");
        }

        // DELETE: api/Stages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStage([FromRoute] Guid id)
        {
            Stage? stage = await _context.Stage.FindAsync(id);

            if (stage == null) return NotFound($"Aucun stage trouvé avec l'id suivant {id}");
            

            _context.Stage.Remove(stage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StageExistsId(Guid id)
        {
            return (_context.Stage?.Any(e => e.StageId == id)).GetValueOrDefault();
        }
        private bool StageExistsName(string name)
        {
            return (_context.Stage?.Any(e => e.Name.ToString().ToLower() == name.ToLower())).GetValueOrDefault();
        }
    }
}
