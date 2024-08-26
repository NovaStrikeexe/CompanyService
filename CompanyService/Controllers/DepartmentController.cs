using CompanyService.Models;
using CompanyService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<DepartmentController> _logger;

    public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
    {
        _departmentService = departmentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartments(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на получение списка всех отделов.");
        var departments = await _departmentService.GetAllDepartmentsAsync(cancellationToken);
        _logger.LogInformation($"Получено {departments.Count()} отделов.");
        return Ok(departments);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получить отдел по идентификатору", Description = "Возвращает информацию о конкретном отделе по его идентификатору.")]
    [SwaggerResponse(200, "Успешно получена информация об отделе.", typeof(Department))]
    [SwaggerResponse(404, "Отдел с данным идентификатором не найден.")]
    public async Task<IActionResult> GetDepartment(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Запрос на получение отдела с идентификатором {id}.");
        var department = await _departmentService.GetDepartmentByIdAsync(id, cancellationToken);
        if (department == null)
        {
            _logger.LogWarning($"Отдел с идентификатором {id} не найден.");
            return NotFound();
        }
        _logger.LogInformation($"Отдел с идентификатором {id} успешно получен.");
        return Ok(department);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Добавить новый отдел", Description = "Добавляет новый отдел в систему.")]
    [SwaggerResponse(201, "Отдел успешно создан.", typeof(Department))]
    public async Task<IActionResult> AddDepartment([FromBody] Department department, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на добавление нового отдела.");
        await _departmentService.AddDepartmentAsync(department, cancellationToken);
        _logger.LogInformation($"Отдел с идентификатором {department.Id} успешно создан.");
        return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Обновить существующий отдел", Description = "Обновляет информацию о существующем отделе.")]
    [SwaggerResponse(204, "Информация об отделе успешно обновлена.")]
    [SwaggerResponse(400, "Неправильный запрос. Идентификатор отдела не совпадает с переданным в теле запроса.")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department, CancellationToken cancellationToken)
    {
        if (id != department.Id)
        {
            _logger.LogWarning($"Неправильный запрос: идентификатор {id} не совпадает с идентификатором в теле запроса {department.Id}.");
            return BadRequest();
        }

        _logger.LogInformation($"Запрос на обновление отдела с идентификатором {id}.");
        await _departmentService.UpdateDepartmentAsync(department, cancellationToken);
        _logger.LogInformation($"Отдел с идентификатором {id} успешно обновлен.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удалить отдел", Description = "Удаляет отдел по идентификатору.")]
    [SwaggerResponse(204, "Отдел успешно удален.")]
    public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Запрос на удаление отдела с идентификатором {id}.");
        await _departmentService.DeleteDepartmentAsync(id, cancellationToken);
        _logger.LogInformation($"Отдел с идентификатором {id} успешно удален.");
        return NoContent();
    }
}
