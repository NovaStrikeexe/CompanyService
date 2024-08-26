using CompanyService.Models;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace CompanyService.UnitTests;

public class CompanyServiceTests
{
    private readonly Mock<IRepository<Company>> _mockRepo;
    private readonly CompanyService.Services.Implementation.CompanyService _service;

    public CompanyServiceTests()
    {
        _mockRepo = new Mock<IRepository<Company>>();
        _service = new CompanyService.Services.Implementation.CompanyService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllCompaniesAsync_ReturnsAllCompanies()
    {
        var companies = new List<Company>
        {
            new() { Id = 1, Name = "Company 1" },
            new() { Id = 2, Name = "Company 2" }
        };

        _mockRepo.Setup(repo => repo.GetAllAsync(default)).ReturnsAsync(companies);

        var result = await _service.GetAllCompaniesAsync(default);

        Assert.Equal(2, result.Count());
        Assert.Equal("Company 1", result.First().Name);
    }

    [Fact]
    public async Task GetCompanyByIdAsync_ReturnsCompany()
    {
        var company = new Company { Id = 1, Name = "Company 1" };

        _mockRepo.Setup(repo => repo.GetByIdAsync(1, default)).ReturnsAsync(company);

        var result = await _service.GetCompanyByIdAsync(1, default);

        Assert.Equal("Company 1", result.Name);
    }

    [Fact]
    public async Task AddCompanyAsync_AddsCompany()
    {
        var company = new Company { Name = "New Company" };

        await _service.AddCompanyAsync(company, default);

        _mockRepo.Verify(repo => repo.AddAsync(company, default), Times.Once);
    }

    [Fact]
    public async Task UpdateCompanyAsync_UpdatesCompany()
    {
        var company = new Company { Id = 1, Name = "Updated Company" };

        await _service.UpdateCompanyAsync(company, default);

        _mockRepo.Verify(repo => repo.UpdateAsync(company, default), Times.Once);
    }

    [Fact]
    public async Task DeleteCompanyAsync_DeletesCompany()
    {
        await _service.DeleteCompanyAsync(1, default);

        _mockRepo.Verify(repo => repo.DeleteAsync(1, default), Times.Once);
    }
}