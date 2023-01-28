namespace UPSchool_SignalR_Api.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string Name { get; set; }
		
		public Room Room { get; set; }

		//bunu yazmasakda .ner core da algılar ilişkiyi
		public int RoomId { get; set; }

		
	}

}
