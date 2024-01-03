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

        

        

        /*
        [HttpGet("{id}")]
        public async Task<MateriaDTO> GetByIdAsync(int id)
        {
            Materia materia = await _subjectService.GetByIdAsync(id);
            return new MateriaDTO
            {
                ID = materia.Id,
                Nombre = materia.Nombre,
            };
        }
        */
    }
}