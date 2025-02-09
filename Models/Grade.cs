using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySqlEFWebAPI.Models;

namespace MySqlEFWebAPI.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 10)]
        public int Score { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}