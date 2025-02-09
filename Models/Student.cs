using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySqlEFWebAPI.Models;

public class Student
{
    [Key]
    public int Id { get; set; } // Primary Key
    [Required]
    public string Name { get; set; } = string.Empty;
    public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
}