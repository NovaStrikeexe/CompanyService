using CompanyService.Models;
using CompanyService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ILogger<CompaniesController> _logger;

    public CompaniesController(ICompanyService companyService, ILogger<CompaniesController> logger)
    {
        _companyService = companyService;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Получить все компании", Description = "Возвращает список всех компаний.")]
    [SwaggerResponse(200, "Список компаний успешно получен.", typeof(IEnumerable<Company>))]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompanies(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на получение списка всех компаний.");
        var companies = await _companyService.GetAllCompaniesAsync(cancellationToken);
        _logger.LogInformation($"Получено {companies.Count()} компаний.");
        return Ok(companies);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получить компанию по идентификатору", Description = "Возвращает информацию о компании по её идентификатору.")]
    [SwaggerResponse(200, "Информация о компании успешно получена.", typeof(Company))]
    [SwaggerResponse(404, "Компания с указанным идентификатором не найдена.")]
    public async Task<ActionResult<Company>> GetCompany(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Запрос на получение компании с идентификатором {id}.");
        var company = await _companyService.GetCompanyByIdAsync(id, cancellationToken);
        if (company == null)
        {
            _logger.LogWarning($"Компания с идентификатором {id} не найдена.");
            return NotFound();
        }
        _logger.LogInformation($"Компания с идентификатором {id} успешно получена.");
        return Ok(company);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Добавить новую компанию", Description = "Добавляет новую компанию в систему.")]
    [SwaggerResponse(201, "Компания успешно создана.", typeof(Company))]
    public async Task<ActionResult<Company>> PostCompany(Company company, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на добавление новой компании.");
        await _companyService.AddCompanyAsync(company, cancellationToken);
        _logger.LogInformation($"Компания с идентификатором {company.Id} успешно создана.");
        return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Обновить существующую компанию", Description = "Обновляет информацию о существующей компании.")]
    [SwaggerResponse(204, "Компания успешно обновлена.")]
    [SwaggerResponse(400, "Неправильный запрос. Идентификатор компании не совпадает с переданным в теле запроса.")]
    public async Task<IActionResult> PutCompany(int id, Company company, CancellationToken cancellationToken)
    {
        if (id != company.Id)
        {
            _logger.LogWarning($"Неправильный запрос: идентификатор {id} не совпадает с идентификатором в теле запроса {company.Id}.");
            return BadRequest();
        }
        _logger.LogInformation($"Запрос на обновление компании с идентификатором {id}.");
        await _companyService.UpdateCompanyAsync(company, cancellationToken);
        _logger.LogInformation($"Компания с идентификатором {id} успешно обновлена.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удалить компанию по идентификатору", Description = "Удаляет компанию по её идентификатору.")]
    [SwaggerResponse(204, "Компания успешно удалена.")]
    [SwaggerResponse(404, "Компания с указанным идентификатором не найдена.")]
    public async Task<IActionResult> DeleteCompany(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Запрос на удаление компании с идентификатором {id}.");
        await _companyService.DeleteCompanyAsync(id, cancellationToken);
        _logger.LogInformation($"Компания с идентификатором {id} успешно удалена.");
        return NoContent();
    }
}
