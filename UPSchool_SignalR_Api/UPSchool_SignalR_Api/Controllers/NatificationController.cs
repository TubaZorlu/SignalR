using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using UPSchool_SignalR_Api.Hubs;

namespace UPSchool_SignalR_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NatificationController : ControllerBase
	{
		private readonly IHubContext<MyHub> _hubContext;

		public NatificationController(IHubContext<MyHub> hubContext)
		{
			_hubContext = hubContext;
		}

		[HttpGet("{roomCount}")]

		public async Task<IActionResult> SetRoomCount(int roomCount) 
		{
			MyHub.RoomCount = roomCount;
			await _hubContext.Clients.All.SendAsync("Notify", $"Bu oda en fazla {roomCount} kişi olabilir");
			return Ok();
		}

	}
}
