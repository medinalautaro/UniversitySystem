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
    [Route("subjects")]
    public class SubjectController : ControllerBase
    {
        private ISubjectService _subjectService;
        public SubjectController()
        {
            _subjectService = new SubjectService();
        }

        [HttpPost]
        public async Task<SubjectDTO> CreateAsync([FromBody] SubjectCreateDTO dto)
        {
            Subject subject = await _subjectService.CreateAsync(dto.Name);
            return new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name,
            };
        }

        [HttpPost("{id}")] //TODO preguntar a nico respecto a porque el remplzao de id por subjectId no funciona
        public async Task<ActionResult> RegisterStudentToSubjectAsync(int studentId, int subjectId)
        {
            try{
                await _subjectService.RegisterStudentAsync(studentId, subjectId);
            } catch (KeyNotFoundException ex){
                return NotFound(ex.Message);
            }
            return Ok();
        }
        
        /*
        [HttpPost("{id}")] //TODO el "id" hace o no hace referencia al subject? me cago en esta mierda
        public async Task<ActionResult> RegisterProfessorToSubjectAsync(int professorId, int subjectId)
        {
            try{
                await _subjectService.RegisterProfessorAsync(professorId, subjectId);
            } catch (KeyNotFoundException ex){
                return NotFound(ex.Message);
            }
            return Ok();
        }
        */
        [HttpPut]
        public async Task<ActionResult<SubjectDTO>> UpdateAsync(int subjectId, [FromBody] SubjectDTO dto)
        {
            Subject subject;
            try{
                subject = await _subjectService.UpdateAsync(subjectId, dto.Name);
            } catch (KeyNotFoundException ex){
                return NotFound(ex.Message);
            } catch (ArgumentNullException ex){  //Argument null exception inherits from ArgumentException so it has to be catched before.
                return BadRequest(ex.Message);
            } catch (ArgumentException ex){
                return BadRequest(ex.Message);
            } 
            
            return new SubjectDTO 
            {
                Id = subject.Id,
                Name = subject.Name,
            };
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int subjectId)
        {
            try{
                await _subjectService.DeleteAsync(subjectId);
            } catch (KeyNotFoundException ex){
                return NotFound(ex.Message);
            }
            return Ok();
        }

        [HttpGet("{id}")] // /subjects/{id}
        public async Task<ActionResult<SubjectDTO>> GetByIdAsync(int id)
        {
            Subject subject;
            
            try{
                subject = await _subjectService.GetByIdAsync(id);
            } catch (KeyNotFoundException ex){
               return NotFound(ex.Message);
            }

            return new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name,
            };
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> SelectAllAsync()
        {
            IEnumerable<Subject> subjects = new List<Subject>();
            
            try{
                subjects = await _subjectService.SelectAllAsync();
            } catch (KeyNotFoundException ex){
               return NotFound(ex.Message);
            }

            return Ok(subjects.Select(s => new SubjectDTO{
                Id = s.Id,
                Name = s.Name,
            }));
        }
    }
}