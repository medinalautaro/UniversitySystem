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
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentController()
        {
            _studentService = new StudentService();
        }

        [HttpPost]
        public async Task<StudentDTO> CreateAsync([FromBody] StudentCreateDTO dto)
        {
            Student student = await _studentService.CreateAsync(dto.Name, dto.LastName, dto.NationalId);
            
            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                LastName = student.LastName,
                NationalId = student.NationalId
            };
        }

        [HttpPut]
        public async Task<StudentDTO> UpdateAsync(int studentId, [FromBody] StudentCreateDTO dto)
        {
            Student student;
            student = await _studentService.UpdateAsync(studentId, dto.Name, dto.LastName, dto.NationalId);

            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                LastName = student.Name,
                NationalId = student.NationalId
            };
        }

        [HttpDelete]
        public async Task DeleteAsync(int studentId)
        {
            await _studentService.DeleteAsync(studentId);
        }

        [HttpGet("{id}")] // /subjects/{id}
        public async Task<StudentDTO> GetByIdAsync(int id)
        {
            Student student;
            student = await _studentService.GetByIdAsync(id);

            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                LastName = student.Name,
                NationalId = student.NationalId
            };
        }

        
        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> SelectAllAsync()
        {
            IEnumerable<Student> student = new List<Student>();
            student = await _studentService.SelectAllAsync();

            return student.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                LastName = s.LastName,
                NationalId = s.NationalId
            });
        }
    }
}