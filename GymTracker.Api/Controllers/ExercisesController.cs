using AutoMapper;
using GymTracker.Core.DTOs.Exercises;
using GymTracker.Core.Entities;
using GymTracker.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseRepository _repo;
        private readonly IMapper _mapper;

        public ExercisesController(IExerciseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseReadDto>>> GetAll()
        {
            var exercises = await _repo.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<ExerciseReadDto>>(exercises);
            return Ok(dto);
        }

        // GET: api/exercises/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExerciseReadDto>> GetById(int id)
        {
            var exercise = await _repo.GetByIdAsync(id);
            if (exercise == null) return NotFound();

            var dto = _mapper.Map<ExerciseReadDto>(exercise);
            return Ok(dto);
        }

        // POST: api/exercises
        [HttpPost]
        public async Task<ActionResult<ExerciseReadDto>> Create(ExerciseCreateDto dto)
        {
            var exercise = _mapper.Map<Exercise>(dto);

            await _repo.AddAsync(exercise);
            await _repo.SaveChangesAsync();

            var readDto = _mapper.Map<ExerciseReadDto>(exercise);
            return CreatedAtAction(nameof(GetById), new { id = exercise.Id }, readDto);
        }

        // PUT: api/exercises/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ExerciseUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/exercises/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, ExerciseUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(dto.Name))
                existing.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.MuscleGroup))
                existing.MuscleGroup = dto.MuscleGroup;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/exercises/5
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
