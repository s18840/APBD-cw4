using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            var students = new List<Student>();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18840;Integrated Security=True"))
                using(var com=new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select IndexNumber, FirstName, LastName, BirthDate, Name, Semester from student as s Inner Join Enrollment as e On s.IdEnrollment=e.IdEnrollment Inner Join Studies as st On e.IdStudy=st.IdStudy;";
                
                client.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumer = dr["IndexNumber"].ToString();
                    //st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();

                    st.Name = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    students.Add(st);
                }
            }
                return Ok(students);
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
            student.IndexNumer = $"s{new Random().Next(1, 20000)}";
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