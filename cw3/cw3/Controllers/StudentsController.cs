using System;
using cw3.DAL;
using cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")]
    public IActionResult GetStudent (int id)
        {
            _dbService.GetStudents();
            return NotFound("Nie znaleziono studenta");
        }
        [HttpPost]
        public IActionResult CreateStudent (Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult updateStudent(int id)
        {
            return Ok("Aktualizacja dokończona");
        }

        [HttpPut("{id}")]
        public IActionResult deleteStudent(int id)
        {
            return Ok("Usuwanie zakończone");
        }


    }
}