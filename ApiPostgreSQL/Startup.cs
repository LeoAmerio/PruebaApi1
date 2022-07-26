using ApiPostgreSQL.Data;
using ApiPostgreSQL.Data.Repositories;
using Microsoft.OpenApi.Models;

namespace ApiPostgreSQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var portgreSQLconfiguration = new PostgreSQLConfig(Configuration.GetConnectionString("PostgreSQLConnection"));
            services.AddSingleton(portgreSQLconfiguration);

            services.AddScoped<ICarRepository, AutoRepository>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPostgreSQL", Version = "v1"});
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

        }
    }

}
