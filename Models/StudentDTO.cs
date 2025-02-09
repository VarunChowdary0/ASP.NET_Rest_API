namespace MySqlEFWebAPI.Models;

public class GradeDTOGet
{
    public int Id { get; set; }
    public int Score { get; set; }
    public int CourseId {get; set;}
    public int StudentId {get; set;}

}
public class GradeDTO: GradeDTOGet
{
    public string CourseName { get; set; }
    public string StudentName { get; set; }
    
}
public class CourseDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Name {get; set;}
    
    public int StudentId { get; set; }
    
}
public class StudentDTO
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; } = string.Empty;
    public List<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
}