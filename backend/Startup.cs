using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


// Instalamos o Entity Framework 
// dotnet tool install --global dotnet-ef  

// O comando acima precisa ser instalado apenas uma vez, os demais para cada projeto.

// Baixamos o pacote SQLServer do Entity Framework
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer

// Baixamos o pacota que irá escrever nossos códigos
// dotnet add package Microsoft.EntityFrameworkCore.Design

// Testamos se os pacotes foram instalados 
// dotnet restore 

// Testamos a instalação do EF
// dotnet ef

// Código que criará o nosso Contexto da Base de Dados e nossos Models
// dotnet ef  dbcontext scaffold "Server=DESKTOP-MB5G0BQ\SQLEXPRESS; Database=Gufos; User Id=sa; Password=132" Microsoft.EntityFrameworkCore.SqlServer -o Models -d



namespace backend
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
            services.AddControllersWithViews().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            ); // oq esta sendo feito 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection(); // Redirecionar http para https

            app.UseRouting(); 

            app.UseAuthorization(); 

            app.UseEndpoints(endpoints => // mapeando endpoints
            {
                endpoints.MapControllers();
            });
        }
    }
}
