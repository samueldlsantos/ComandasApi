using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ComandasAPI.Data;
using ComandasAPI.DTO;
using ComandasAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComandasAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();

            var usersDTO = _mapper.Map<List<UserDTO>>(users);
            return Ok(usersDTO);
        }

        // Obtener un usuario por ID con su rol
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (user == null) return NotFound();
            var userDTO = _mapper.Map<UserDTO>(user);


            return Ok(userDTO);
        }

        // Crear un usuario
        [HttpPost]
        public async Task<ActionResult<Product>> CreateUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
             // Obtener el id del usuario logueado desde el token
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("Usuario no autenticado o token inválido.");
            }

            // Asignar CreatedBy con el usuario actual
            user.CreatedBy = userId;
            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<UserDTO>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, resultDto);
        }
        // Actualizar un usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDTO)
        {

            if (id != userDTO.Id)
            return BadRequest();

            // Buscar el usuario existente en la base de datos
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            // Mapear los datos recibidos al objeto role existente (sin sobreescribir UpdatedBy aún)
            _mapper.Map(userDTO, user);

            // Obtener el id del usuario logueado desde el token
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("Usuario no autenticado o token inválido.");
            }

            // Asignar UpdatedBy con el usuario actual
            user.UpdatedBy = userId;
            user.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Roles.Any(r => r.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }
        // Eliminar un usuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}