using System;
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
using UserApi.Models.User;

namespace UserApi.Controllers
{
    /// <summary>
    /// Gère les sessions
    /// </summary>
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

        /// <summary>
        /// Get la liste des sessions
        /// </summary>
        /// <param name="withUSers">Bool, inclure ou non les user aux sessiosn</param>
        /// <response code="400 + Message"></response>
        /// <returns>Liste de sessions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionsDTO>>> GetSessions(bool withUSers = false)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            List<Sessions> sessions;

            if (withUSers) sessions = await _context.Sessions
                    .Include(s => s.Users)
                    .ToListAsync();
            else sessions = await _context.Sessions.ToListAsync();

            if (sessions == null) return NotFound("Aucunne sessions trouvée");

            sessions.Reverse();

            return sessions.Select(s => s.ToModelList()).ToList();
        }

        /// <summary>
        /// Get une session donner
        /// </summary>
        /// <param name="id">id de sessions</param>
        /// <param name="withUSers">Inclure les user aux sessions</param>
        /// <response code="400 + Message"></response>
        /// <returns>Model sessions</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SessionDTO>> GetSessions([FromRoute] Guid id, bool withUSers = false)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Sessions? session = null;

            if (withUSers) session = await _context.Sessions
                    .Include(s => s.Users)
                    .FirstOrDefaultAsync(s => s.SessionId == id);
            else session = await _context.Sessions.FindAsync(id);


            if (session == null) return NotFound();

            return session.ToModel();
        }

        /// <summary>
        /// Get l'ensemble de tous le utilisateur qui ne sont pas présent dans la session
        /// </summary>
        /// <param name="id">id de sessions</param>
        /// <response code="200">Liste d'utilisateur </response>
        /// <response code="400 + Message"></response>
        /// <response code="404">Session not found</response>
        /// <returns>Model sessions</returns>
        [HttpGet]
        [Route("{id}/users")]
        public async Task<ActionResult<List<UserDTO>>> getUserAreNotInSessions([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Sessions? session = _context.Sessions
                .Include(s => s.Users).
                FirstOrDefault(s => s.SessionId == id);

            if (session == null) return NotFound();

            List<ApiUser> users = await _context.Users
                .Where(u => !session.Users.Contains(u))
                .ToListAsync();

            return users.ToUserDTOList();
        }

        /// <summary>
        /// Edit une sessions
        /// </summary>
        /// <param name="id">Id de la sessions</param>
        /// <param name="dto">model de modif de sessions</param>
        /// <response code="400 + Message"></response>
        /// <response code="200">Confirmation + id sessions</response>
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

        /// <summary>
        /// Ajoute des users a une sessions
        /// </summary>
        /// <param name="id">id de sessions</param>
        /// <param name="usernames">liste de username</param>
        /// <response code="404"></response>
        /// <response code="200">tous les utilisateur on été ajouté</response>
        [HttpPost]
        [Route("add/{id}/users")]
        public async Task<IActionResult> AddUserSessions([FromRoute] Guid id, [FromBody] List<string> usernames)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Sessions? session = await _context.Sessions
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.SessionId == id);
            if (session == null) return NotFound("Aucune sessions n'existe avec cet id");

            foreach(var username in usernames)
            {
                ApiUser user = await userManager.FindByNameAsync(username);
                if (user == null) return NotFound($"Aucun user n'existe avec cet id: {username}");

                if (!session.Users.Contains(user))
                {
                    session.Users.Add(user);
                    user.NbSessions++;
                    session.NbParticipant++;
                };
            }


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

            return Ok($"les utilisateurs ont été ajouté a la session : {id}");
        }

        /// <summary>
        /// retire des users d'une sessions
        /// </summary>
        /// <param name="id">id de sessions</param>
        /// <param name="usernames">Liste de username</param>
        /// <response code="404"></response>
        /// <response code="200">tous les utilisateur on été ajouté</response>
        [HttpPost]
        [Route("remove/{id}/users")]
        public async Task<IActionResult> RemoveUserSessions([FromRoute] Guid id, [FromBody] List<string> usernames)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Sessions? session = await _context.Sessions
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.SessionId == id);
            if (session == null) return NotFound("Aucune sessions n'existe avec cet id");

            foreach(var username in usernames)
            {
                ApiUser user = await userManager.FindByNameAsync(username);
                if (user == null) return NotFound($"Aucun user n'existe avec cet username: {username}");

                if (session.Users.Contains(user))
                {
                    session.Users.Remove(user);
                    user.NbSessions--;
                    session.NbParticipant--;
                };
            }


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

            return Ok($"les utilisateurs ont été ajouté a la session : {id}");
        }

        /// <summary>
        /// Remove un user a une sessions
        /// </summary>
        /// <param name="id">id sessions</param>
        /// <param name="dto">model remove user</param>
        /// <reponse code="400 + Message"></reponse>
        /// <reponse code="200">Confirmation + username + id sessions</reponse>
        [HttpPut]
        [Route("remove/{id}")]
        public async Task<IActionResult> RemoveUserSessions([FromRoute] Guid id, [FromBody] RemoveUserDTO dto)
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

        /// <summary>
        /// Crée une sessions
        /// </summary>
        /// <param name="dto">Model de création</param>
        /// <response code="400 + Model"></response>
        /// <response code="200">Confirmation + id sessions</response>
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

        /// <summary>
        /// Delete une sessions
        /// </summary>
        /// <param name="id">sessions id</param>
        /// <response code="400 + Message"></response>
        /// <response code="404">Session non trouvé</response>
        /// <response code="204">Session suprimée</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSessions([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
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
