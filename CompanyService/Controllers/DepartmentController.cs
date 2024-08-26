using CompanyService.Models;
using CompanyService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController(IDepartmentService departmentService) : ControllerBase
{
    /// <summary>
    /// Получить список всех отделов.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список всех отделов.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllDepartments(CancellationToken cancellationToken)
    {
        var departments = await departmentService.GetAllDepartmentsAsync(cancellationToken);
        return Ok(departments);
    }

    /// <summary>
    /// Получить отдел по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор отдела.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Информация о запрашиваемом отделе.</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получить отдел по идентификатору", Description = "Возвращает информацию о конкретном отделе по его идентификатору.")]
    [SwaggerResponse(200, "Успешно получена информация об отделе.", typeof(Department))]
    [SwaggerResponse(404, "Отдел с данным идентификатором не найден.")]
    public async Task<IActionResult> GetDepartment(int id, CancellationToken cancellationToken)
    {
        var department = await departmentService.GetDepartmentByIdAsync(id, cancellationToken);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    /// <summary>
    /// Добавить новый отдел.
    /// </summary>
    /// <param name="department">Информация о новом отделе.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Созданный отдел.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Добавить новый отдел", Description = "Добавляет новый отдел в систему.")]
    [SwaggerResponse(201, "Отдел успешно создан.", typeof(Department))]
    public async Task<IActionResult> AddDepartment([FromBody] Department department, CancellationToken cancellationToken)
    {
        await departmentService.AddDepartmentAsync(department, cancellationToken);
        return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
    }

    /// <summary>
    /// Обновить существующий отдел.
    /// </summary>
    /// <param name="id">Идентификатор отдела.</param>
    /// <param name="department">Информация об отделе для обновления.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Обновить существующий отдел", Description = "Обновляет информацию о существующем отделе.")]
    [SwaggerResponse(204, "Информация об отделе успешно обновлена.")]
    [SwaggerResponse(400, "Неправильный запрос. Идентификатор отдела не совпадает с переданным в теле запроса.")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department, CancellationToken cancellationToken)
    {
        if (id != department.Id)
        {
            return BadRequest();
        }

        await departmentService.UpdateDepartmentAsync(department, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Удалить отдел.
    /// </summary>
    /// <param name="id">Идентификатор отдела.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Результат выполнения операции.</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удалить отдел", Description = "Удаляет отдел по идентификатору.")]
    [SwaggerResponse(204, "Отдел успешно удален.")]
    public async Task<IActionResult> DeleteDepartment(int id, CancellationToken cancellationToken)
    {
        await departmentService.DeleteDepartmentAsync(id, cancellationToken);
        return NoContent();
    }
}
