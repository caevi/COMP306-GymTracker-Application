using AutoMapper;
using GymTracker.Core.DTOs.Users;
using GymTracker.Core.Entities;
using GymTracker.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
        {
            var users = await _repo.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<UserReadDto>>(users);
            return Ok(dto);
        }

        // GET: api/users/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserReadDto>> GetById(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return NotFound();

            var dto = _mapper.Map<UserReadDto>(user);
            return Ok(dto);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> Create(UserCreateDto dto)
        {
            var user = _mapper.Map<User>(dto);

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();

            var readDto = _mapper.Map<UserReadDto>(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, readDto);
        }

        // PUT: api/users/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/users/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, UserUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Simple manual patch – only override if values are not null/empty
            if (!string.IsNullOrWhiteSpace(dto.Name))
                existing.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                existing.Email = dto.Email;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _repo.Remove(existing);
            await _repo.SaveChangesAsync();

            return NoContent();
        }
    }
}
