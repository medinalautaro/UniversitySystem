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
    public class ProfessorController : ControllerBase
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
        public async Task<ActionResult<ProfessorDTO>> UpdateAsync(int professorId, [FromBody] ProfessorCreateDTO dto)
        {
            Professor professor;
            try{
                professor = await _professorService.UpdateAsync(professorId, dto.Name, dto.LastName, dto.NationalId);
            } catch (KeyNotFoundException ex){
                return NotFound(ex.Message);
            }
            
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
            try{
                await _professorService.DeleteAsync(professorId);
            } catch (KeyNotFoundException ex){
               //return NotFound(ex.Message); TODO returning the not found causes an error
            }
        }

        [HttpGet("{id}")] // /subjects/{id}
        public async Task<ActionResult<ProfessorDTO>> GetByIdAsync(int id)
        {
            Professor professor;
            
            try{
                professor = await _professorService.GetByIdAsync(id);
            } catch (KeyNotFoundException ex){
               return NotFound(ex.Message);
            }

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
            
            try{
                professor = await _professorService.SelectAllAsync();
            } catch (KeyNotFoundException ex){
               //return NotFound(ex.Message); TODO returning the not found causes an error
            }

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