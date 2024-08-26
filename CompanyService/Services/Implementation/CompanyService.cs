using CompanyService.Models;
using CompanyService.Services.Interfaces;

namespace CompanyService.Services.Implementation;

public class CompanyService(IRepository<Company> repository) : ICompanyService
{
    public async Task<IEnumerable<Company>> GetAllCompaniesAsync(CancellationToken cancellationToken) =>
        await repository.GetAllAsync(cancellationToken);

    public async Task<Company> GetCompanyByIdAsync(int id, CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(id, cancellationToken);

    public async Task AddCompanyAsync(Company company, CancellationToken cancellationToken) =>
        await repository.AddAsync(company, cancellationToken);

    public async Task UpdateCompanyAsync(Company company, CancellationToken cancellationToken) =>
        await repository.UpdateAsync(company, cancellationToken);

    public async Task DeleteCompanyAsync(int id, CancellationToken cancellationToken) =>
        await repository.DeleteAsync(id, cancellationToken);
}