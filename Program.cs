using Microsoft.EntityFrameworkCore;

namespace mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Регистрируем repository, service и контекст базы данных.
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IMyService, MovieService>();

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(connection));

            // Регистрируем контроллеры Web API (без Razor Views).
            builder.Services.AddControllers();

            var app = builder.Build();

            // Оставляем для статических файлов, например постеров из wwwroot.
            app.UseStaticFiles();

            // Подключаем атрибутные маршруты: api/Movies и api/Movies/{id}.
            app.MapControllers();

            app.Run();
        }
    }
}
