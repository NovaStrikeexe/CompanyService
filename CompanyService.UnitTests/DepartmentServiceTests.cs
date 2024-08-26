using CompanyService.Models;
using CompanyService.Services.Implementation;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace CompanyService.UnitTests;

public class DepartmentServiceTests
{
    private readonly Mock<IRepository<Department>> _mockRepo;
    private readonly DepartmentService _service;

    public DepartmentServiceTests()
    {
        _mockRepo = new Mock<IRepository<Department>>();
        _service = new DepartmentService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllDepartmentsAsync_ReturnsAllDepartments()
    {
        var departments = new List<Department>
        {
            new() { Id = 1, Name = "Department 1" },
            new() { Id = 2, Name = "Department 2" }
        };

        _mockRepo.Setup(repo => repo.GetAllAsync(default)).ReturnsAsync(departments);

        var result = await _service.GetAllDepartmentsAsync(default);

        Assert.Equal(2, result.Count());
        Assert.Equal("Department 1", result.First().Name);
    }

    [Fact]
    public async Task GetDepartmentByIdAsync_ReturnsDepartment()
    {
        var department = new Department { Id = 1, Name = "Department 1" };

        _mockRepo.Setup(repo => repo.GetByIdAsync(1, default)).ReturnsAsync(department);

        var result = await _service.GetDepartmentByIdAsync(1, default);

        Assert.Equal("Department 1", result.Name);
    }

    [Fact]
    public async Task AddDepartmentAsync_AddsDepartment()
    {
        var department = new Department { Name = "New Department" };

        await _service.AddDepartmentAsync(department, default);

        _mockRepo.Verify(repo => repo.AddAsync(department, default), Times.Once);
    }

    [Fact]
    public async Task UpdateDepartmentAsync_UpdatesDepartment()
    {
        var department = new Department { Id = 1, Name = "Updated Department" };

        await _service.UpdateDepartmentAsync(department, default);

        _mockRepo.Verify(repo => repo.UpdateAsync(department, default), Times.Once);
    }

    [Fact]
    public async Task DeleteDepartmentAsync_DeletesDepartment()
    {
        await _service.DeleteDepartmentAsync(1, default);

        _mockRepo.Verify(repo => repo.DeleteAsync(1, default), Times.Once);
    }
}