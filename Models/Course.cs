using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MySqlEFWebAPI.Models;

namespace MySqlEFWebAPI.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student? Student { get; set; }

        public Grade? Grade { get; set; }
    }
}