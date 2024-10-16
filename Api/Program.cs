using Api.Models;
using CosmosDBAccessor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StacyClouds.SwaAuth.Models;

var environmentVariables = Environment.GetEnvironmentVariables();

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        {
            services.AddSingleton<IContainerAccess<CosmosTodo>>(factory => new CosmosContainerAccess<CosmosTodo>(
                new CosmosEndpointKeySettings(
                    (string)environmentVariables["CosmosEndpoint"]!,
                    (string)environmentVariables["CosmosKey"]!,
                    (string)environmentVariables["TodoDatabaseName"]!,
                    (string)environmentVariables["TodoContainerName"]!
                )));
            services.AddSingleton<TodoHandler>();
        }
        services.AddSingleton<IRoleProcessor, RoleProcessor>();
    })
    .Build();

host.Run();
