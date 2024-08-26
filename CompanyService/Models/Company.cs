﻿namespace CompanyService.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Department> Departments { get; set; }
}