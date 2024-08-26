using CompanyService.Models;
using CompanyService.Services.Interfaces;

namespace CompanyService.Services.Implementation;


public class DepartmentService(IRepository<Department> repository) : IDepartmentService
{
    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken) =>
        await repository.GetAllAsync(cancellationToken);

    public async Task<Department> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(id, cancellationToken);

    public async Task AddDepartmentAsync(Department department, CancellationToken cancellationToken) =>
        await repository.AddAsync(department, cancellationToken);

    public async Task UpdateDepartmentAsync(Department department, CancellationToken cancellationToken) =>
        await repository.UpdateAsync(department, cancellationToken);

    public async Task DeleteDepartmentAsync(int id, CancellationToken cancellationToken) =>
        await repository.DeleteAsync(id, cancellationToken);
}
