using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UPSchool_SignalR_Api.Models;

namespace UPSchool_SignalR_Api.Hubs
{
	public class MyHub : Hub
	{
		private readonly Context _context;

		public MyHub(Context context)
		{
			_context = context;
		}

		public static List<string> Names { get; set; } = new List<string>();

		//odada bulunan kişi sayısı
		public static int ClientCount { get; set; } = 0;

		//odama max kaç kişi bulabilir için
		public static int RoomCount { get; set; } = 7;

		//bunu else de yazdığımız için kapattık
		//public async Task SendName(string name)
		//{
		//    Names.Add(name);
		//    await Clients.All.SendAsync("ReceiveName", name);
		//}

		public async Task GetNames()
		{
			await Clients.All.SendAsync("ReceiveNames", Names);
		}

		public override async Task OnConnectedAsync()
		{
			ClientCount++;
			await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			ClientCount--;
			await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
		}


		public async Task SendName(string name)
		{
			if (Names.Count >= RoomCount)
			{
				await Clients.Caller.SendAsync("Error", $"bu oda en fazla {RoomCount} kişi kada üye alabilir");

			}
			else
			{
				Names.Add(name);
				await Clients.All.SendAsync("ReceiveName", name);
			}
		}

		public async Task SendNameByGroup(string name, string roomName)
		{
			var room = _context.Rooms.Where(x => x.RoomName == roomName).FirstOrDefault();

		

			if (room != null)
			{
				room.Users.Add(new User
				{
					Name = name
				});

			}

			else
			{
				var newRoom = new Room
				{
					RoomName = roomName
				};
				newRoom.Users.Add(new User { Name = name });
				
				
				_context.Rooms.Add(newRoom);
				
			}

			await _context.SaveChangesAsync();
			await Clients.Group(roomName).SendAsync("ReceiveGroup", name, room.RoomId);
		}


		public async Task getNamesByGroup ()
		{
			var rooms = _context.Rooms.Include(x => x.Users).Select(x => new
			{

				roomID = x.RoomId,
				Userss = x.Users.ToList()
			});

			await Clients.All.SendAsync("ReceiveNamesByGroup", rooms);
		}

		//gruba ekleme
		public async Task AddGroup(string roomName)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
		}

		//gruptan çıkarma
		public async Task removeGroup(string roomName)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
		}


	}
}
