using Asp.Versioning;
using hotel_river.src.core.application.services;
using hotel_river.src.infrastructure.database.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace hotel_river.src.infrastructure.api.controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }        

        // GET: GetClient
        /// <summary>
        /// Get the client by the client
        /// </summary>        
        /// <returns>The list of clients</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]        
        [HttpGet("getall")]
        public async Task<IActionResult> GetClient()
        {
            var clients = await _context.Clients                
                .Include(c => c.Address)
                .ToListAsync();

            string jsonString = JsonSerializer.Serialize(clients);

            return Ok(jsonString);
        }

        // GET BY ID: GetClientById        
        /// <summary>
        /// Get client by Id
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns>The client entity</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetClientById(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Address)                
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            string jsonString = JsonSerializer.Serialize(client);

            return Ok(jsonString);
        }

        // POST: SetClient              
        /// <summary>
        /// Create client
        /// </summary>
        /// <param name="client">Client object</param>
        /// <returns>Created()</returns>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetClient(
            [Bind("HotelId,Id,Name,LastName,Birth,Gender,CPF,Email,Phone")]
            Client client)
        {
            if (ModelState.IsValid)
            {
                client.Id = Guid.NewGuid();
                _context.Add(client);
                await _context.SaveChangesAsync();

                return Created();
            }

            return NoContent();
        }

        // PATCH: EditClient
        /// <summary>
        /// Edit client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="client">Client object</param>
        /// <returns>The client entity</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClient(Guid id,
            [Bind("HotelId,Id,Name,LastName,Birth,Gender,CPF,Email,Phone")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(client);
            }            
            return NoContent();
        }

        // DELETE: Delete Client        
        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="id">Client id</param>        
        /// <returns>Ok()</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]        
        [HttpDelete("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            Console.WriteLine(client);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
