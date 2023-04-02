namespace EngineBay.CommunityEdition
{
    using System.Collections.Generic;
    using System.Linq;
    using EngineBay.ActorEngine;
    using EngineBay.ApiDocumentation;
    using EngineBay.Authentication;
    using EngineBay.Blueprints;
    using EngineBay.Core;
    using EngineBay.Cors;
    using EngineBay.DatabaseManagement;
    using EngineBay.Logging;
    using EngineBay.Persistence;

    public static class ModuleRegistration
    {
        private static readonly List<IModule> RegisteredModules = new List<IModule>();

        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration)
        {
            var modules = DiscoverModules();
            foreach (var module in modules)
            {
                module.RegisterModule(services, configuration);
                RegisteredModules.Add(module);
            }

            return services;
        }

        public static WebApplication MapModuleEndpoints(this WebApplication app)
        {
            foreach (var module in RegisteredModules)
            {
                module.MapEndpoints(app);
            }

            return app;
        }

        public static WebApplication AddModuleMiddleware(this WebApplication app)
        {
            foreach (var module in RegisteredModules)
            {
                module.AddMiddleware(app);
            }

            return app;
        }

        private static IEnumerable<IModule> DiscoverModules()
        {
            var modules = new List<IModule>();

            modules.Add(new PersistenceModule());
            modules.Add(new DatabaseManagementModule());
            modules.Add(new BlueprintsModule());
            modules.Add(new ActorEngineModule());
            modules.Add(new ApiDocumentationModule());
            modules.Add(new LoggingModule());
            modules.Add(new CorsModule());
            modules.Add(new AuthenticationModule());

            Console.WriteLine($"Discovered {modules.Count} EngineBay modules");
            return modules;
        }
    }
}