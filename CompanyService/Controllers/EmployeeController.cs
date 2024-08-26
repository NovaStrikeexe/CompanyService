using CompanyService.Models;
using CompanyService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Получить всех сотрудников", Description = "Возвращает список всех сотрудников.")]
        [SwaggerResponse(200, "Список сотрудников успешно получен.", typeof(IEnumerable<Employee>))]
    public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на получение списка всех сотрудников.");
        var employees = await _employeeService.GetAllEmployeesAsync(cancellationToken);
        _logger.LogInformation($"Получено {employees.Count()} сотрудников.");
        return Ok(employees);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получить сотрудника по идентификатору", Description = "Возвращает информацию о сотруднике по его идентификатору.")]
    [SwaggerResponse(200, "Информация о сотруднике успешно получена.", typeof(Employee))]
    [SwaggerResponse(404, "Сотрудник с указанным идентификатором не найден.")]
    public async Task<IActionResult> GetEmployee(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Запрос на получение сотрудника с идентификатором {id}.");
        var employee = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
        if (employee == null)
        {
            _logger.LogWarning($"Сотрудник с идентификатором {id} не найден.");
            return NotFound();
        }
        _logger.LogInformation($"Сотрудник с идентификатором {id} успешно получен.");
        return Ok(employee);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Добавить нового сотрудника", Description = "Добавляет нового сотрудника в систему.")]
    [SwaggerResponse(201, "Сотрудник успешно создан.", typeof(Employee))]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на добавление нового сотрудника.");
        await _employeeService.AddEmployeeAsync(employee, cancellationToken);
        _logger.LogInformation($"Сотрудник с идентификатором {employee.Id} успешно создан.");
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Обновить существующего сотрудника", Description = "Обновляет информацию о существующем сотруднике.")]
    [SwaggerResponse(204, "Сотрудник успешно обновлен.")]
    [SwaggerResponse(400, "Неправильный запрос. Идентификатор сотрудника не совпадает с переданным в теле запроса.")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee, CancellationToken cancellationToken)
    {
        if (id != employee.Id)
        {
            _logger.LogWarning($"Неправильный запрос: идентификатор {id} не совпадает с идентификатором в теле запроса {employee.Id}.");
            return BadRequest();
        }
        _logger.LogInformation($"Запрос на обновление сотрудника с идентификатором {id}.");
        await _employeeService.UpdateEmployeeAsync(employee, cancellationToken);
        _logger.LogInformation($"Сотрудник с идентификатором {id} успешно обновлен.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удалить сотрудника по идентификатору", Description = "Удаляет сотрудника по его идентификатору.")]
    [SwaggerResponse(204, "Сотрудник успешно удален.")]
    [SwaggerResponse(404, "Сотрудник с указанным идентификатором не найден.")]
    public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Запрос на удаление сотрудника с идентификатором {id}.");
        await _employeeService.DeleteEmployeeAsync(id, cancellationToken);
        _logger.LogInformation($"Сотрудник с идентификатором {id} успешно удален.");
        return NoContent();
    }
}
