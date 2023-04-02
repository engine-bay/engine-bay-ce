namespace EngineBay.CommunityEdition
{
    using EngineBay.DatabaseManagement;

    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddProblemDetails();

            builder.Services.RegisterModules(builder.Configuration);

            var app = builder.Build();

            app.UseExceptionHandler();
            app.UseStatusCodePages();
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(ExceptionMiddleware.HandleError());
            });

            app.MapModuleEndpoints();

            // Register health endpoint
            app.MapHealthChecks("/health");

            app.UseStaticFiles();

            app.AddModuleMiddleware();

            // Seed the database
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var dbInitialiser = serviceProvider.GetRequiredService<DbInitialiser>(); // todo this should probably be refactored into the EngineBay.DatabaseManagement module
            var seedFilePaths = new List<string>();
            seedFilePaths.Add(@"SeedData/maths-workbooks.json");

            dbInitialiser.Run(seedFilePaths);
            app.Run();
        }
    }
}