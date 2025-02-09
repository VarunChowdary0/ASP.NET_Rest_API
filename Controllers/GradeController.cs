using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlEFWebAPI.Data;
using MySqlEFWebAPI.Models;

namespace MySqlEFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly AppDBContext2 _context;

        public GradeController(AppDBContext2 context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Grade>> AssignGrade(GradeDTOGet grade)
        {
            var course = await _context.Courses.FindAsync(grade.CourseId);
            var student = await _context.Students.FindAsync(grade.StudentId);

            if (course == null || student == null)
            {
                return BadRequest("Course or Student not found");
            }
            
            var existingCourse = await _context.Grades
                .AnyAsync(c => c.StudentId == grade.StudentId && c.CourseId == grade.CourseId);
            if (existingCourse)
            {
                return Conflict($"Course '{course.Title}' already Graded for Student ID {grade.StudentId}.");
            }
            Grade newGrade = new Grade
            {
                CourseId = course.Id,
                StudentId = student.Id,
                Score = grade.Score,
                
                Student = student,
                Course = course
            };
            
            // Add the new grade
            _context.Grades.Add(newGrade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrades), new { studentId = grade.StudentId }, grade);
        }


        [HttpGet("{studentId}")]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades(int studentId)
        {
            return await _context.Grades.Where(g => g.StudentId == studentId)
                .Include(g => g.Course)
                .ToListAsync();
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDTO>>> GetAllGrades()
        {
            List<GradeDTO> grades = await _context.Grades
                .Include(g => g.Course)
                .Include(S => S.Student)
                .Select(G => new GradeDTO
                {
                    Id = G.Id,
                    Score = G.Score,
                    CourseId = G.CourseId,
                    CourseName = G.Course.Title,
                    StudentId = G.StudentId,
                    StudentName = G.Student.Name
                }).ToListAsync();
            return grades;
        }
    }
}