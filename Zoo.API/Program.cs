using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zoo.Application.Interfaces;
using Zoo.Application.Services;
using Zoo.Infrastructure.Events;
using Zoo.Infrastructure.Repositories;
using Zoo.Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);

// ----- Регистрация зависимостей (IoC) -----
builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
builder.Services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
builder.Services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();
builder.Services.AddSingleton<IEventDispatcher, InMemoryEventDispatcher>();

builder.Services.AddScoped<AnimalTransferService>();
builder.Services.AddScoped<FeedingOrganizationService>();
builder.Services.AddScoped<ZooStatisticsService>();

// ----- Регистрация MVC и подключение внешней сборки с контроллерами -----
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(AnimalsController).Assembly) // <-- Zoo.Presentation.Controllers
    .AddControllersAsServices();                           // чтобы DI инжектил контроллеры

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----- Middleware -----
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();