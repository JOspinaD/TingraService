using Microsoft.EntityFrameworkCore;
using TingraService.BLL.Services.Contract;
using TingraService.BLL.Services.EmpresaServices;
using TingraService.BLL.Services.PreguntaServices;
using TingraService.BLL.Services.UsuarioServices;
using TingraService.DAL;
using TingraService.DAL.Contract;

namespace TingraService.IOC
{
    public static class Dependencia
    {
        public static IServiceCollection InyectarDependencias(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env, bool tryDbConnection = false)
        {
            if (env.IsProduction() || tryDbConnection)
            {
                Console.WriteLine("--> using SqlServer");
                var connectionString = configuration.GetConnectionString("TingraDb");
                Console.WriteLine("--> connectionString:" + connectionString);
                services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            }
            else
            {
                Console.WriteLine("--> Using InMemory");
                services.AddDbContext<AppDbContext>(option =>
                option.UseInMemoryDatabase("TingraDb"));
            }

            services.AddExceptionHandler<GlobalExcepcionHandler>();
            services.AddProblemDetails();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<PasswordService>();

            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IPreguntaService, PreguntaService>();

            // Registrar configuración JWT desde appsettings.json
            services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));

            return services;
        }
    }
}
