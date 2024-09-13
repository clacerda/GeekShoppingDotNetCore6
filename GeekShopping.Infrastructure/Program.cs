// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        // Executa docker-compose up -d
        RunDockerCompose();
        
        CreateHostBuilder(args).Build().Run();
    }

    private static void RunDockerCompose()
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "docker-compose",
            Arguments = "up -d",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using (var process = new Process())
        {
            process.StartInfo = processInfo;
            process.Start();
            process.WaitForExit();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(config =>
            {
                config.AddJsonFile("hostsettings.json", optional: true);
            });
}

