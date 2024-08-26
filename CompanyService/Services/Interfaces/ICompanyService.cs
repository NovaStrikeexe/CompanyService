using CompanyService.Models;

namespace CompanyService.Services.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<Company>> GetAllCompaniesAsync(CancellationToken cancellationToken);
    Task<Company> GetCompanyByIdAsync(int id, CancellationToken cancellationToken);
    Task AddCompanyAsync(Company company, CancellationToken cancellationToken);
    Task UpdateCompanyAsync(Company company, CancellationToken cancellationToken);
    Task DeleteCompanyAsync(int id, CancellationToken cancellationToken);
}