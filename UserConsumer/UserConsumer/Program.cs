using Common.MessagingService;
using Domain.User.Repository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserConsumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<UserWorker>();

builder.Services.AddDbContext<TechChallengeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.AddTransient<IUserRepository, Infraestructure.Repository.User.UserRepository>();

var host = builder.Build();
host.Run();
