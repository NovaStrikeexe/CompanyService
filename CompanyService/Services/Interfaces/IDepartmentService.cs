using CompanyService.Models;

namespace CompanyService.Services.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllDepartmentsAsync(CancellationToken cancellationToken);
    Task<Department> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken);
    Task AddDepartmentAsync(Department department, CancellationToken cancellationToken);
    Task UpdateDepartmentAsync(Department department, CancellationToken cancellationToken);
    Task DeleteDepartmentAsync(int id, CancellationToken cancellationToken);
}