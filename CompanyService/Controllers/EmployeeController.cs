using CompanyService.Models;
using CompanyService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    /// <summary>
    /// Получить список всех сотрудников.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список сотрудников.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Получить всех сотрудников", Description = "Возвращает список всех сотрудников.")]
    [SwaggerResponse(200, "Список сотрудников успешно получен.", typeof(IEnumerable<Employee>))]
    public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
    {
        var employees = await employeeService.GetAllEmployeesAsync(cancellationToken);
        return Ok(employees);
    }

    /// <summary>
    /// Получить сотрудника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сотрудник.</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получить сотрудника по идентификатору", Description = "Возвращает информацию о сотруднике по его идентификатору.")]
    [SwaggerResponse(200, "Информация о сотруднике успешно получена.", typeof(Employee))]
    [SwaggerResponse(404, "Сотрудник с указанным идентификатором не найден.")]
    public async Task<IActionResult> GetEmployee(int id, CancellationToken cancellationToken)
    {
        var employee = await employeeService.GetEmployeeByIdAsync(id, cancellationToken);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    /// <summary>
    /// Добавить нового сотрудника.
    /// </summary>
    /// <param name="employee">Модель сотрудника.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданный сотрудник.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Добавить нового сотрудника", Description = "Добавляет нового сотрудника в систему.")]
    [SwaggerResponse(201, "Сотрудник успешно создан.", typeof(Employee))]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee, CancellationToken cancellationToken)
    {
        await employeeService.AddEmployeeAsync(employee, cancellationToken);
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    /// <summary>
    /// Обновить существующего сотрудника.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="employee">Модель сотрудника.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат обновления.</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Обновить существующего сотрудника", Description = "Обновляет информацию о существующем сотруднике.")]
    [SwaggerResponse(204, "Сотрудник успешно обновлен.")]
    [SwaggerResponse(400, "Неправильный запрос. Идентификатор сотрудника не совпадает с переданным в теле запроса.")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee, CancellationToken cancellationToken)
    {
        if (id != employee.Id) return BadRequest();
        await employeeService.UpdateEmployeeAsync(employee, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Удалить сотрудника по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сотрудника.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удалить сотрудника по идентификатору", Description = "Удаляет сотрудника по его идентификатору.")]
    [SwaggerResponse(204, "Сотрудник успешно удален.")]
    [SwaggerResponse(404, "Сотрудник с указанным идентификатором не найден.")]
    public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
    {
        await employeeService.DeleteEmployeeAsync(id, cancellationToken);
        return NoContent();
    }
}