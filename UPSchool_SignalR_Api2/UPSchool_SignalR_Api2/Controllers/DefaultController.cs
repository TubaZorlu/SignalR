using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using UPSchool_SignalR_Api2.Hubs;
using UPSchool_SignalR_Api2.Models;

namespace UPSchool_SignalR_Api2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DefaultController : ControllerBase
	{
		private readonly ElektrikService _elektrikService;

		public DefaultController(ElektrikService elektrikService)
		{
			_elektrikService = elektrikService;
		}


		[HttpPost]
		public async Task<IActionResult> SaveElectric(Elektrik elektrik) 
		{
			await _elektrikService.SaveElectric(elektrik);
			//IQueryable<Elektrik> elektrikList =_elektrikService.GetList();
			return Ok(_elektrikService.GetElectricChartsList());
		}

		//portainer.io üye ol 

		[HttpGet]
		public IActionResult SendData() 
		{
			Random rnd = new Random();
			Enumerable.Range(1, 10).ToList().ForEach(x =>
			{

				foreach (Ecity item in Enum.GetValues(typeof(Ecity)))
				{
					var newElectrik = new Elektrik
					{
						City = item,
						Count = rnd.Next(100, 1000),
						ElectricDate = DateTime.Now.AddDays(x)
					};

					_elektrikService.SaveElectric(newElectrik).Wait();
					System.Threading.Thread.Sleep(1000);

				}
			});
			return Ok("elektrik verileri veri tabanına kaydelidi");
			

		}



	}
}
