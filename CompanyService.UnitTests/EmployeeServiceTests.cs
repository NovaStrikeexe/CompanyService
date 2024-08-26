using CompanyService.Models;
using CompanyService.Services.Implementation;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace CompanyService.UnitTests;

public class EmployeeServiceTests
{
    private readonly Mock<IRepository<Employee>> _mockRepo;
    private readonly EmployeeService _service;

    public EmployeeServiceTests()
    {
        _mockRepo = new Mock<IRepository<Employee>>();
        _service = new EmployeeService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_ReturnsAllEmployees()
    {
        var employees = new List<Employee>
        {
            new() { Id = 1, FirstName = "John", LastName = "Doe" },
            new() { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };

        _mockRepo.Setup(repo => repo.GetAllAsync(default)).ReturnsAsync(employees);

        var result = await _service.GetAllEmployeesAsync(default);

        Assert.Equal(2, result.Count());
        Assert.Equal("John", result.First().FirstName);
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_ReturnsEmployee()
    {
        var employee = new Employee { Id = 1, FirstName = "John", LastName = "Doe" };

        _mockRepo.Setup(repo => repo.GetByIdAsync(1, default)).ReturnsAsync(employee);

        var result = await _service.GetEmployeeByIdAsync(1, default);

        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task AddEmployeeAsync_AddsEmployee()
    {
        var employee = new Employee { FirstName = "New", LastName = "Employee" };

        await _service.AddEmployeeAsync(employee, default);

        _mockRepo.Verify(repo => repo.AddAsync(employee, default), Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_UpdatesEmployee()
    {
        var employee = new Employee { Id = 1, FirstName = "Updated", LastName = "Employee" };

        await _service.UpdateEmployeeAsync(employee, default);

        _mockRepo.Verify(repo => repo.UpdateAsync(employee, default), Times.Once);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_DeletesEmployee()
    {
        await _service.DeleteEmployeeAsync(1, default);

        _mockRepo.Verify(repo => repo.DeleteAsync(1, default), Times.Once);
    }
}