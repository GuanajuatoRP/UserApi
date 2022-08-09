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
    /// <summary>
    /// Obtenir l'ensemble des textes markdown
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MarkdownsController : ControllerBase
    {
        private readonly UserApiContext _context;

        public MarkdownsController(UserApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get l'ensemble des markdown
        /// </summary>
        /// <returns>Liste de model de md</returns>
        [HttpGet]
        [Route("web")]
        public async Task<IActionResult> GetMarkdown()
        {
            List<Markdown> mdList = await _context.Markdown.Where(m => m.FormatType == "Web").ToListAsync();
            return Ok(mdList.ToModelList());
        }

        /// <summary>
        /// Get un MD donner par sont id
        /// </summary>
        /// <param name="id">ID de MD</param>
        /// <response code="400 + Message"></response>
        /// <returns>Model de MD</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MarkdownDTO>> GetMarkdown([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Markdown? md = await _context.Markdown.FindAsync(id);

            if (md == null) return NotFound();
            

            return md.ToModel();
        }

        /// <summary>
        /// Edit un md
        /// </summary>
        /// <param name="id">id du texte markdown a edit</param>
        /// <param name="dto">Model de la modif</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Confirmation</response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutMarkdown([FromRoute] Guid id, [FromBody] MarkdownDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
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

        /// <summary>
        /// Creation d'un nouveau texte en markdown
        /// </summary>
        /// <param name="dto">Model du texte a ajouté</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Confirmation d'ajout + id</response>
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

        /// <summary>
        /// suprimer un texte markdown
        /// </summary>
        /// <param name="id">id du md a supprimer</param>
        /// <response code="400 + Message"></response>
        /// <response code="404">texte Md non trouver</response>
        /// <response code="204">Texte delete</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMarkdown(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
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
