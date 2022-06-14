﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Mapper;
using UserApi.Models.Sessions;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly UserApiContext _context;
        private readonly UserManager<ApiUser> userManager;

        public SessionsController(UserApiContext context, UserManager<ApiUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionsDTO>>> GetSessions(bool withUSers = false)
        {
            List<Sessions> sessions;

            if (withUSers) sessions = await _context.Sessions
                    .Include(s => s.Users)
                    .ToListAsync();
            else sessions = await _context.Sessions.ToListAsync();

            if (sessions == null) return NotFound("Aucunne sessions trouvée");

            sessions.Reverse();

            return sessions.Select(s => s.ToModelList()).ToList();
        }

        // GET: api/Sessions/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SessionDTO>> GetSessions([FromRoute] Guid id, bool withUSers = false)
        {
            Sessions? session = null;

            if (withUSers) session = await _context.Sessions
                    .Include(s => s.Users)
                    .FirstOrDefaultAsync(s => s.SessionId == id);
            else session = await _context.Sessions.FindAsync(id);


            if (session == null) return NotFound();

            return session.ToModel();
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutSessions([FromRoute] Guid id, [FromBody] EditSessionDTO dto)
        {
            if (id != dto.SessionId) return BadRequest("L'id est != de celui du DTO");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Sessions? entity = await _context.Sessions.FindAsync(id);

            if (entity == null) return NotFound("Aucune entité n'existe avec cet id");

            entity.Type = dto.Type;
            entity.Debut = dto.Debut;
            entity.Fin = dto.Debut;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Id de la sessions modifier : {id}");
        }
        
        [HttpPut]
        [Route("add/{id}")]
        public async Task<IActionResult> AddUserSessions([FromRoute] Guid id, [FromBody] AddUserDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Sessions? session = await _context.Sessions
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.SessionId == id);
            if (session == null) return NotFound("Aucune sessions n'existe avec cet id");

            ApiUser user = await userManager.FindByEmailAsync(dto.DiscordId);
            if (user == null) return NotFound("Aucun user n'existe avec cet id");

            if (session.Users.Contains(user)) return BadRequest("L'utilisateur spécifier est déja présent dans la session");
            session.Users.Add(user);
            session.NbParticipant++;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!SessionsExists(id))
                {
                    return NotFound(e.Message);
                }
                else
                {
                    throw;
                }
            }

            return Ok($"{user.UserName} a été ajouté a la session : {id}");
        }

        [HttpPut]
        [Route("remove/{id}")]
        public async Task<IActionResult> RemoveUserSessions([FromRoute] Guid id, [FromBody] AddUserDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Sessions? session = await _context.Sessions
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.SessionId == id);
            if (session == null) return NotFound("Aucune sessions n'existe avec cet id");

            ApiUser user = await userManager.FindByEmailAsync(dto.DiscordId);
            if (user == null) return NotFound("Aucun user n'existe avec cet id");

            if (!session.Users.Contains(user)) return BadRequest("L'utilisateur spécifier n'est pas présent dans la séssions");
            
            session.Users.Remove(user);
            session.NbParticipant--;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!SessionsExists(id))
                {
                    return NotFound(e.Message);
                }
                else
                {
                    throw;
                }
            }

            return Ok($"{user.UserName} a été retirer de la session : {id}");
        }

        // POST: api/Sessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sessions>> PostSessions([FromBody] CreateSessionDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Sessions session = dto.CreateClass();

            try
            {
                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();  
                throw;
            }

            return Ok($"Id de la nouvelle session: {session.SessionId}");
        }

        // DELETE: api/Sessions/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSessions([FromRoute] Guid id)
        {
            Sessions? sessions = await _context.Sessions.FindAsync(id);
            if (sessions == null) return NotFound($"Aucune sessions trouvée avec l'id suivant {id}");

            _context.Sessions.Remove(sessions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessionsExists(Guid id)
        {
            return (_context.Sessions?.Any(e => e.SessionId == id)).GetValueOrDefault();
        }
    }
}
