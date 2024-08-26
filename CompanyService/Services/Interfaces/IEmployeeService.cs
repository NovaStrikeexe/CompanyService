using CompanyService.Models;

namespace CompanyService.Services.Interfaces;


public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken);
    Task<Employee> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken);
    Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    Task UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken);
}
