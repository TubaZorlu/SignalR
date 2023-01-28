using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UPSchool_SignalR_Api2.Hubs;
using UPSchool_SignalR_Api2.Models;

namespace UPSchool_SignalR_Api2
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
            services.AddDbContext<Context>(opt =>
            {
                opt.UseSqlServer(Configuration["ConStr"]);
            });
       
			services.AddCors();
			services.AddSignalR();
			services.AddControllers();

            services.AddScoped<ElektrikService>();

        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(x => x
			.AllowAnyMethod() //heheangi metoda izin ver
			.AllowAnyHeader()//herhangi başlığa izin ver
			.AllowCredentials() // tüm kimlik yapılarına izin ver
			.SetIsOriginAllowed(origin => true)); // dışarında gelen gelen kaynağa zin ver


			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<ElektrikHub>("/ElektrikHub");
			});
		}
	}
}
