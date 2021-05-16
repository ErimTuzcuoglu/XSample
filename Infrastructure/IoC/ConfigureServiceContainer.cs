using System;
using System.IO;
using System.Reflection;
using Application.Orchestration;
using Application.Orchestrator;
using Dapper;
using Domain.Core;
using Domain.RequestHandlers.Countries.Queries.GetAll;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;
using Persistence.Data;
using Persistence.Data.UnitOfWork;
using Persistence.Migrations;

namespace Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Api", Version = "v1"}); });
            services.AddMediatR(AppDomain.CurrentDomain.Load("Domain"));

            services.AddScoped<IOrchestrator, Orchestrator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainNotifications, DomainNotifications>();
        }

        public static void EnsureDatabaseAndStartMigrations(IConfiguration configuration)
        {
            using (var connection =
                new NpgsqlConnection(configuration.GetConnectionString("DbCheckConnectionString")))
            {
                var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
                if (projectDirectory != null)
                {
                    var createDbScript = new FileInfo(
                        Path.Combine(projectDirectory, "Persistence/Script/CreateDBScript.sql"));
                    var checkDbScript =
                        new FileInfo(Path.Combine(projectDirectory, "Persistence/Script/CheckDBScript.sql"));

                    var checkDbCommand = new NpgsqlCommand(checkDbScript.OpenText().ReadToEnd(), connection);
                    var createDbCommand = new NpgsqlCommand(createDbScript.OpenText().ReadToEnd(), connection);

                    connection.Open();
                    if (checkDbCommand.ExecuteScalar() == null)
                        createDbCommand.ExecuteScalar();

                    if (checkDbCommand.ExecuteScalar() != null)
                    {
                        var serviceProvider = CreateMigratorServices(configuration);

                        using (var scope = serviceProvider.CreateScope())
                        {
                            UpdateDatabase(scope.ServiceProvider);
                        }
                    }

                    connection.Close();
                }
            }
        }

        public static IServiceProvider CreateMigratorServices(IConfiguration configuration)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
                    .ScanIn(typeof(Initial).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        public static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}