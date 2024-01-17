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

        [HttpPost("{subjectId}/students")] //TODO change studentId to FromBody
        public async Task RegisterStudentToSubjectAsync(int studentId, int subjectId)
        {
            await _subjectService.RegisterStudentAsync(studentId, subjectId);
        }
        
        
        [HttpPost("{subjectId}/professors")] 
        public async Task RegisterProfessorToSubjectAsync(int professorId, int subjectId)
        {
            await _subjectService.RegisterProfessorAsync(professorId, subjectId);
        }
        
        [HttpPut]
        public async Task<SubjectDTO> UpdateAsync(int subjectId, [FromBody] SubjectDTO dto)
        {
            Subject subject;
            subject = await _subjectService.UpdateAsync(subjectId, dto.Name); 
            
            return new SubjectDTO 
            {
                Id = subject.Id,
                Name = subject.Name,
            };
        }

        [HttpDelete]
        public async Task DeleteAsync(int subjectId)
        {
            await _subjectService.DeleteAsync(subjectId);
        }

        [HttpGet("{id}")] // /subjects/{id}
        public async Task<SubjectDTO> GetByIdAsync(int id)
        {
            Subject subject;
            subject = await _subjectService.GetByIdAsync(id);

            return new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name,
            };
        }

        
        [HttpGet]
        public async Task<IEnumerable<SubjectDTO>> SelectAllAsync()
        {
            IEnumerable<Subject> subjects = new List<Subject>();
            subjects = await _subjectService.SelectAllAsync();

            return subjects.Select(s => new SubjectDTO{
                Id = s.Id,
                Name = s.Name,
            });
        }
    }
}