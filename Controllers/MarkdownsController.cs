using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Mapper;
using UserApi.Models.Markdown;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkdownsController : ControllerBase
    {
        private readonly UserApiContext _context;

        public MarkdownsController(UserApiContext context)
        {
            _context = context;
        }

        // GET: api/Markdowns
        [HttpGet]
        [Route("web")]
        public async Task<IActionResult> GetMarkdown()
        {
            List<Markdown> mdList = await _context.Markdown.Where(m => m.FormatType == "Web").ToListAsync();
            return Ok(mdList.ToModelList());
        }

        // GET: api/Markdowns/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MarkdownDTO>> GetMarkdown([FromRoute] Guid id)
        {
            Markdown? md = await _context.Markdown.FindAsync(id);

            if (md == null) return NotFound();
            

            return md.ToModel();
        }

        // PUT: api/Markdowns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutMarkdown([FromRoute] Guid id, [FromBody] MarkdownDTO dto)
        {
            if (id != dto.TextId) return BadRequest("Les deux ids sont !=");
            

            Markdown? entity = await _context.Markdown.FindAsync(id);
            if (entity == null) return BadRequest("Aucun texte n'as été trouvé avec cette id");

            entity.CatName = dto.CatName;
            entity.Title = dto.Title;
            entity.RawText = dto.RawText;
            entity.FormatType = dto.FormatType;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkdownExists(id)) return NotFound();
                else throw;
            }

            return Ok("Text Modifié");
        }

        // POST: api/Markdowns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostMarkdown(AddMdDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Markdown md = new Markdown
            {
                CatName = dto.CatName,
                Title = dto.Title,
                RawText = dto.RawText,
                FormatType = dto.FormatType
            };

            try
            {
                _context.Markdown.Add(md);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

            return Ok($"Id du nouveau text : {md.TextId}");
        }

        // DELETE: api/Markdowns/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMarkdown(Guid id)
        {
            Markdown? md = await _context.Markdown.FindAsync(id);
            if (md == null) return NotFound();

            _context.Markdown.Remove(md);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarkdownExists(Guid id)
        {
            return (_context.Markdown?.Any(e => e.TextId == id)).GetValueOrDefault();
        }
    }
}
