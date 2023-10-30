namespace EngineBay.APIConfiguration
{
    using System;

    public static class BaseApiConfiguration
    {
        public static string VersionNumber =>

            // placeholder for future change management
            DefaultAPIConstants.VersionNumber;

        public static string GetBasePath()
        {
            var basePath = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.APIBASEPATH);

            if (!string.IsNullOrEmpty(basePath))
            {
                return basePath;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.APIBASEPATH} was not set, defaulting to '{DefaultAPIConstants.BasePath}'.");

            return DefaultAPIConstants.BasePath;
        }
    }
}
