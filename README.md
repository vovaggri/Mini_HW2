# Mini homework 2 for Software Design
Этот проект представляет собой веб-приложение для автоматизации бизнес‑процессов зоопарка на основе принципов Domain‑Driven Design и Clean Architecture. Данные хранятся в in‑memory репозиториях, API реализовано на ASP.NET Core Web API через Swagger.

## Структура проекта
- **Zoo.sln**
  - Zoo.Domain: Доменные сущности, события, Value Objects
  - Zoo.Application: Сервисы бизнес‑логики и интерфейсы репозиториев
  - Zoo.Infrastructure: In‑memory репозитории и диспетчер событий
  - Zoo.Presentation: DTO и контроллеры Web API
  - Zoo.API: Хост ASP.NET Core, Program.cs

 ## Реализованный функционал
 - Добавление/удаление животного: `AnimalsController` + `InMemoryAnimalRepository`
 - Получение списка и деталей животного: `AnimalsController`
 - Перемещение животного между вольерами: `AnimalTransferService`
 - Просмотр/добавление/удаление вольера: `EnclosuresController`, `InMemoryEnclosureRepository`
 - Планирование кормления: `FeedingOrganizationService`, `FeedingSchedulesController`
 - Отметка выполнения кормления: `FeedingOrganizationService`
 - Просмотр статистики (кол‑во, свободные): `ZooStatisticsService`, `StatisticsController`

## Domain-Driver Design
Пременины основные концепции DDD:
- **Entities**:
  - `Animal`, `Enclosure`, `FeedingSchedule`
- **Value Objects (валидация примитивов)**:
  - `Species`, `AnimalName`, `FoodType`
- **Domain Events**:
  - `AnimalMovedEvent`, `FeedingTimeEvent`
- **Инкапсуляция бизнес‑правил внутри сущностей**:
  - Логика кормления и лечения в `Animal.Feed()` и `Animal.Heal()`
  - Логика перемещения и учёта вместимости в `Enclosure.AddAnimal()`/`RemoveAnimal()`


## Clean Architecture
Соблюдены принципы:
1. **Независимость слоёв**: Domain не зависит ни от чего, Application зависит только от Domain, Infrastructure и Presentation зависят от Application (почему-то Rider показывает, что есть зависимость от Domain, однако если посмотреть csproj двух слоев, то там только от Application).
2. **Интерфейсы**: все зависимости между слоями через абстракции (`IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository`, `IEventDispatcher`).
3. **Изоляция бизнес‑логики**: вся логика реализована в Domain (сущности, события) и Application (сервисы). Infrastructure отвечает только за хранение и доставку событий, Presentation — за HTTP.

## Запуск через Swagger
1. Запустить приложение (`dotnet run` в проекте Zoo.API).
2. Открыть Swagger UI по адресу `https://localhost:5089/swagger`.
3. Использовать UI для ручек по REST API.

## Инструкция по запуску
1. Склонируйте или скачайте проект.
2. Откройте проект в Visual Studio или другой IDE для .NET (начиная 8.0).
3. Установите пакет [Microsoft.AspNetCore.OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi) и [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore) через NuGet.
4. Перейти в каталог `Zoo.API` и запустить: `dotnet run`
5. Перейти в браузере на `https://localhost:5089/swagger` для открытия UI.
