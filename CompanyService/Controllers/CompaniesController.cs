using CompanyService.Models;
using CompanyService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    /// <summary>
    /// Получить список всех компаний.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список компаний.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Получить все компании", Description = "Возвращает список всех компаний.")]
    [SwaggerResponse(200, "Список компаний успешно получен.", typeof(IEnumerable<Company>))]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompanies(CancellationToken cancellationToken)
    {
        var companies = await companyService.GetAllCompaniesAsync(cancellationToken);
        return Ok(companies);
    }

    /// <summary>
    /// Получить компанию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Компания.</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получить компанию по идентификатору", Description = "Возвращает информацию о компании по её идентификатору.")]
    [SwaggerResponse(200, "Информация о компании успешно получена.", typeof(Company))]
    [SwaggerResponse(404, "Компания с указанным идентификатором не найдена.")]
    public async Task<ActionResult<Company>> GetCompany(int id, CancellationToken cancellationToken)
    {
        var company = await companyService.GetCompanyByIdAsync(id, cancellationToken);
        if (company == null) return NotFound();
        return Ok(company);
    }

    /// <summary>
    /// Добавить новую компанию.
    /// </summary>
    /// <param name="company">Модель компании.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданная компания.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Добавить новую компанию", Description = "Добавляет новую компанию в систему.")]
    [SwaggerResponse(201, "Компания успешно создана.", typeof(Company))]
    public async Task<ActionResult<Company>> PostCompany(Company company, CancellationToken cancellationToken)
    {
        await companyService.AddCompanyAsync(company, cancellationToken);
        return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
    }

    /// <summary>
    /// Обновить существующую компанию.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="company">Модель компании.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат обновления.</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Обновить существующую компанию", Description = "Обновляет информацию о существующей компании.")]
    [SwaggerResponse(204, "Компания успешно обновлена.")]
    [SwaggerResponse(400, "Неправильный запрос. Идентификатор компании не совпадает с переданным в теле запроса.")]
    public async Task<IActionResult> PutCompany(int id, Company company, CancellationToken cancellationToken)
    {
        if (id != company.Id) return BadRequest();
        await companyService.UpdateCompanyAsync(company, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Удалить компанию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор компании.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удалить компанию по идентификатору", Description = "Удаляет компанию по её идентификатору.")]
    [SwaggerResponse(204, "Компания успешно удалена.")]
    [SwaggerResponse(404, "Компания с указанным идентификатором не найдена.")]
    public async Task<IActionResult> DeleteCompany(int id, CancellationToken cancellationToken)
    {
        await companyService.DeleteCompanyAsync(id, cancellationToken);
        return NoContent();
    }
}