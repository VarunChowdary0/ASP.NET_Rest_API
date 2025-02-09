using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlEFWebAPI.Data;
using MySqlEFWebAPI.Models;

namespace MySqlEFWebAPI.Controllers
{
    
    // initailize the route.
    [Route("api/")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDBContext2 _context;

        // set the database context taken from constructor.
        public StudentController(AppDBContext2 context)
        {
            this._context = context;
        }

        [HttpGet("getname/{id}")]
        public async Task<ActionResult<string>> GetName(int id)
        {
            Console.WriteLine($"Get Student name : {id}");
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                return student.Name;
            }
            return NotFound();
        }
            // Get method
            
            //
        [HttpGet("student")] 
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
            {
                Console.WriteLine("All GetStudents");

                var res = await _context.Students
                    .Select(student => new StudentDTO
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Courses = student.Courses.Select(course => new CourseDTO
                        {
                            Id = course.Id,
                            Title = course.Title,
                            StudentId = course.StudentId,
                            Name = student.Name
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(res); // Use Ok() to explicitly return a 200 status code
            }

        [HttpGet("student/{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            Console.WriteLine("Get Student by ID");

            var student = await _context.Students
                .Where(student => student.Id == id)
                .Select(student1 => new StudentDTO
                {
                    Id = student1.Id,
                    Name = student1.Name,
                    Courses = student1.Courses.Select(course => new CourseDTO
                    {
                        Id = course.Id,
                        Title = course.Title,
                        StudentId = course.StudentId
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            if (student == null)
                return NotFound();
            
            return Ok(student);
        }
        
        //Update method;
        [HttpPut("student/{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] Student studentUpdated)
        {
            Console.WriteLine("Updating student : "+studentUpdated.Id + studentUpdated.Name);
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();
            
            student.Name = studentUpdated.Name;
            
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync(); // use asynchornous
            return NoContent(); // status 204
        }

        // Post Method - takes student;
        [HttpPost("student")]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            Console.WriteLine("Post : "+student.Name);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }
    }
}