using CompanyService.Models;
using CompanyService.Services.Interfaces;

namespace CompanyService.Services.Implementation;

public class EmployeeService(IRepository<Employee> repository) : IEmployeeService
{
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken) =>
        await repository.GetAllAsync(cancellationToken);

    public async Task<Employee> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(id, cancellationToken);

    public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken) =>
        await repository.AddAsync(employee, cancellationToken);

    public async Task UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken) =>
        await repository.UpdateAsync(employee, cancellationToken);

    public async Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken) =>
        await repository.DeleteAsync(id, cancellationToken);
}