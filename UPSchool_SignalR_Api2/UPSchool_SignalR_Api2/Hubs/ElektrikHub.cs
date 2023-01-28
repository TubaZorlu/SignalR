using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPSchool_SignalR_Api2.Models;

namespace UPSchool_SignalR_Api2.Hubs
{
	public class ElektrikHub:Hub
	{
		private readonly ElektrikService _service;

		public ElektrikHub(ElektrikService service)
		{
			_service = service;
		}

		public async Task GetElektricList() 
		{
			await Clients.All.SendAsync("ReceiveElectricList", _service.GetElectricChartsList());
		}

		
	}
}
