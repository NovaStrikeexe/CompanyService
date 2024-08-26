namespace CompanyService.Models;

public class Employee
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}