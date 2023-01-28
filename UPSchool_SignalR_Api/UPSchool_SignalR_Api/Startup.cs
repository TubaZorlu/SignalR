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
using UPSchool_SignalR_Api.Hubs;
using UPSchool_SignalR_Api.Models;

namespace UPSchool_SignalR_Api
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
			

			//cors configuration nu startupm olarak geçilmeli
			services.AddCors();
			services.AddSignalR();
			services.AddControllers();

			services.AddDbContext<Context>(opt =>
			{
				opt.UseSqlServer(Configuration["ConStr"]);
			});
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


			//cors configuration nu startupm olarak geçilmeli
			app.UseCors(x => x
			.AllowAnyMethod() //heheangi metoda izin ver
			.AllowAnyHeader()//herhangi başlığa izin ver
			.AllowCredentials() // tüm kimlik yapılarına izin ver
			.SetIsOriginAllowed(origin => true)); // dışarında gelen gelen kaynağa zin ver

			app.UseAuthorization();



			//burada end point olarak bu kökeni yönlendirmeye vermeliyiz tıpkı area gibi
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<MyHub>("/MyHub");
			});
		}
	}
}
