using GeekShopping.Util.Service;
using GeekShopping.Util.Service.IService;
using System.Net.Http.Headers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(); 
        builder.Services.AddControllers();

        var apiSettings = builder.Configuration.GetSection("ApiSettings");
    

        builder.Services.AddScoped<IReadApiExternal, ReadApiExternal>();
        builder.Services.AddHttpClient();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
