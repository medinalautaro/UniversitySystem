using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using SystemUniversity.API.DTOs.Requests;
using SystemUniversity.API.DTOs.Responses;
using SystemUniversity.Contracts.Models;
using SystemUniversity.Contracts.Services;
using SystemUniversity.Services;

namespace SystemUniversity.API.Controllers
{
    [ApiController]
    [Route("professors")]
    public class ProfessorController : ControllerBase  //TODO sacar trycatch de los controladores
    {
        private IProfessorService _professorService;
        public ProfessorController()
        {
            _professorService = new ProfessorService();
        }

        [HttpPost]
        public async Task<ProfessorDTO> CreateAsync([FromBody] ProfessorCreateDTO dto)
        {
            Professor professor = await _professorService.CreateAsync(dto.Name, dto.LastName, dto.NationalId);
            
            return new ProfessorDTO
            {
                Id = professor.Id,
                Name = professor.Name,
                LastName = professor.LastName,
                NationalId = professor.NationalId
            };
        }

        [HttpPut]
        public async Task<ProfessorDTO> UpdateAsync(int professorId, [FromBody] ProfessorCreateDTO dto)
        {
            Professor professor;
            professor = await _professorService.UpdateAsync(professorId, dto.Name, dto.LastName, dto.NationalId);
            
            return new ProfessorDTO
            {
                Id = professor.Id,
                Name = professor.Name,
                LastName = professor.Name,
                NationalId = professor.NationalId
            };
        }

        [HttpDelete]
        public async Task DeleteAsync(int professorId)
        {
            await _professorService.DeleteAsync(professorId);

        }

        [HttpGet("{id}")] // /subjects/{id}
        public async Task<ProfessorDTO> GetByIdAsync(int id)
        {
            Professor professor;
            professor = await _professorService.GetByIdAsync(id);

            return new ProfessorDTO
            {
                Id = professor.Id,
                Name = professor.Name,
                LastName = professor.Name,
                NationalId = professor.NationalId
            };
        }

        
        [HttpGet]
        public async Task<IEnumerable<ProfessorDTO>> SelectAllAsync()
        {
            IEnumerable<Professor> professor = new List<Professor>();
            professor = await _professorService.SelectAllAsync();

            return professor.Select(s => new ProfessorDTO
            {
                Id = s.Id,
                Name = s.Name,
                LastName = s.LastName,
                NationalId = s.NationalId
            });
        }
    }
}