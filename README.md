# Advertising Platform

## Описание

Проект представляет собой backend-сервис для работы с рекламодателями и локациями, реализованный с использованием ASP.NET Core, CQRS и Clean Architecture.

---

## Как запускать проект

### Запуск через Docker

1. Откройте терминал и перейдите в директорию: `/backend/AdvertisingPlatform`
2. Введите в терминал команду запуска докера
   ```
   docker compose up -d --build
   ```
3. После сборки и запуска сервер будет доступен по адресу:
  [http://localhost:8080/swagger](http://localhost:8080/swagger)

### Запуск Unit-тестов
Перейдите в ту же директорию проекта и выполните:
```
dotnet test
```

Но также можно запустить юнит-тесты вручную через Visual Studio.

### Используемые технологии
- ASP.NET Core
- MediatR (CQRS)
- Clean Architecture
- xUnit + FluentAssertions
- Docker + Docker Compose
