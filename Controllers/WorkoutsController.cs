using AutoMapper;
using GymTracker.Core.DTOs.Workouts;
using GymTracker.Core.Entities;
using GymTracker.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutRepository _repo;
        private readonly IMapper _mapper;

        public WorkoutsController(IWorkoutRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutReadDto>>> GetAll()
        {
            var workouts = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<WorkoutReadDto>>(workouts));
        }

        // GET: api/workouts/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<WorkoutReadDto>> GetById(int id)
        {
            var workout = await _repo.GetByIdAsync(id);
            if (workout == null) return NotFound();
            return Ok(_mapper.Map<WorkoutReadDto>(workout));
        }

        // POST: api/workouts
        [HttpPost]
        public async Task<ActionResult<WorkoutReadDto>> Create(WorkoutCreateDto dto)
        {
            var workout = _mapper.Map<Workout>(dto);
            await _repo.AddAsync(workout);
            await _repo.SaveChangesAsync();

            var readDto = _mapper.Map<WorkoutReadDto>(workout);
            return CreatedAtAction(nameof(GetById), new { id = workout.Id }, readDto);
        }

        // PUT: api/workouts/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, WorkoutUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/workouts/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, WorkoutUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Simple patch: only override non-default values
            if (dto.Notes != null)
                existing.Notes = dto.Notes;
            if (dto.Date != default)
                existing.Date = dto.Date;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/workouts/5
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
