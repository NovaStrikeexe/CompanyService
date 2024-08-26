# CompanyService API

## Описание

CompanyService API предоставляет RESTful интерфейс для управления компаниями, отделами и сотрудниками. Этот API включает возможности для создания, получения, обновления и удаления данных об организациях, их подразделениях и сотрудниках.

## Стек технологий

- **.NET 8** — Основная платформа для разработки API.
- **Entity Framework Core** — ORM для работы с базой данных PostgreSQL.
- **Swagger** — Документация и тестирование API.
- **Serilog** — Логирование.
- **PostgreSQL** — Реляционная база данных.

## Установка и запуск

### 1. Клонирование репозитория

```bash
git clone https://github.com/yourusername/CompanyService.git
cd CompanyService
```

### 2. Установка зависимостей

Убедитесь, что у вас установлены все необходимые инструменты и библиотеки.

```bash
dotnet restore
```

### 3. Настройка базы данных

Создайте базу данных PostgreSQL и обновите строку подключения в `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=yourusername;Password=yourpassword;Database=companyservice"
  }
}
```

### 4. Миграции базы данных

Примените миграции для создания схемы базы данных:

```bash
dotnet ef database update
```

### 5. Запуск приложения

Запустите приложение:

```bash
dotnet run
```

Теперь API будет доступен по адресу `http://localhost:5044`.

## Документация API

Swagger предоставляет визуальную документацию и тестирование API. После запуска приложения вы можете получить доступ к Swagger UI по адресу [http://localhost:5044/swagger](http://localhost:5044/swagger).

## Тестирование

### Юнит-тесты

Для запуска юнит-тестов, используйте `xUnit` и `Moq`. Запустите тесты с помощью следующей команды:

```bash
dotnet test
```

### Интеграционные тесты

Интеграционные тесты написаны на Python с использованием `pytest`.

### Требования для тестов

- Python 3.x
- `pytest`
- `requests`

Запустите тесты с помощью:

```bash
pytest
```


## Среда разработки

Рекомендуется использовать [Visual Studio Code](https://code.visualstudio.com/) или [Visual Studio](https://visualstudio.microsoft.com/) для разработки и отладки проекта.

## Логирование

Проект использует [Serilog](https://serilog.net/) для логирования. Логи сохраняются в консоль и могут быть настроены для записи в файлы или внешние системы.
