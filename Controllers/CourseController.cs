using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlEFWebAPI.Data;
using MySqlEFWebAPI.Models;

namespace MySqlEFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly AppDBContext2 _context;

        public CourseController(AppDBContext2 context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourse()
        {
            Console.WriteLine("All Courses");

            var res = await _context.Courses
                .Select(s => new
                {
                    Id = s.Id,
                    Title = s.Title,
                    StudentId = s.StudentId,
                    Name = s.Student.Name
                })
                .ToListAsync();
            
            var c = res.Select(c => new CourseDTO()
                {
                    Id = c.Id,
                    Title = c.Title,
                    StudentId = c.StudentId,
                    Name = c.Name
                }).ToList();
            
            return Ok(c);
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetUngradedCourses(int studentId)
        {
            return await _context.Courses.Where(c => c.StudentId == studentId && c.Grade == null).ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Post Course");
            Console.ResetColor();
            // Find the student in the database
            var student = await _context.Students.FindAsync(course.StudentId);
            if (student == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Student not found");
                Console.ResetColor();
                return BadRequest("Student does not exist.");
            }
            var existingCourse = await _context.Courses
                .AnyAsync(c => c.StudentId == course.StudentId && c.Title == course.Title);

            if (existingCourse)
            {
                return Conflict($"Course '{course.Title}' already exists for Student ID {course.StudentId}.");
            }

            Course newCourse = new Course
            {
                StudentId = course.StudentId,
                Title = course.Title,
                Student = student // ✅ Assign the fetched Student object
            };

            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUngradedCourses), new { studentId = course.StudentId }, newCourse);
        }

    }
}