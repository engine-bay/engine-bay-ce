namespace EngineBay.CommunityEdition
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddProblemDetails();
            builder.Services.AddHttpContextAccessor();

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

            app.InitializeDatabase();

            app.Run();
        }
    }
}