using Microsoft.VisualBasic;
using System;

namespace UPSchool_SignalR_Api2.Models
{
	public enum Ecity 
	{
		istanbul =1,
		ankara=2,
		izmir=3,
		konya=4,
		trabzon=5,
	}

	public class Elektrik
	{
		public int ElektrikId { get; set; }
		public Ecity City { get; set; }
		public int Count { get; set; }
		public DateTime ElectricDate { get; set; }
	}
}
