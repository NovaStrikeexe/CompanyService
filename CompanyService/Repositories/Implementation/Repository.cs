using CompanyService.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Repositories.Implementation;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    private readonly ILogger<Repository<T>> _logger;

    public Repository(ApplicationDbContext context, ILogger<Repository<T>> logger)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _logger = logger;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Запрос на получение всех сущностей типа {typeof(T).Name}.");
        var entities = await _dbSet.ToListAsync(cancellationToken);
        _logger.LogInformation($"Получено {entities.Count} сущностей типа {typeof(T).Name}.");
        return entities;
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Запрос на получение сущности типа {typeof(T).Name} с идентификатором {id}.");
        var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        if (entity == null)
        {
            _logger.LogWarning($"Сущность типа {typeof(T).Name} с идентификатором {id} не найдена.");
        }
        else
        {
            _logger.LogInformation($"Сущность типа {typeof(T).Name} с идентификатором {id} успешно получена.");
        }
        return entity;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Добавление новой сущности типа {typeof(T).Name}.");
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Сущность типа {typeof(T).Name} успешно добавлена.");
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Обновление сущности типа {typeof(T).Name}.");
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Сущность типа {typeof(T).Name} успешно обновлена.");
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Удаление сущности типа {typeof(T).Name} с идентификатором {id}.");
        var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Сущность типа {typeof(T).Name} с идентификатором {id} успешно удалена.");
        }
        else
        {
            _logger.LogWarning($"Сущность типа {typeof(T).Name} с идентификатором {id} не найдена.");
        }
    }
}