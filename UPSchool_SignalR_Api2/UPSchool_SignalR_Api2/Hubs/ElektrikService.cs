using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UPSchool_SignalR_Api2.Models;

namespace UPSchool_SignalR_Api2.Hubs
{
	public class ElektrikService
	{
		private readonly Context _context;
		private readonly IHubContext<ElektrikHub> _hubContext;

		public ElektrikService(Context context, IHubContext<ElektrikHub> hubContext)
		{
			_context = context;
			_hubContext = hubContext;
		}

		public IQueryable<Elektrik> GetList() 
		{
			return _context.Elektriks.AsQueryable();
		}

		public async Task SaveElectric(Elektrik elektrik) 
		{
			await _context.Elektriks.AddAsync(elektrik);
			await _context.SaveChangesAsync();
			await _hubContext.Clients.All.SendAsync("ReceiveElectricList", GetElectricChartsList());
		}

		public List<ElectricChart> GetElectricChartsList()
		{
			List<ElectricChart> electricCharts = new List<ElectricChart>();

			using (var command = _context.Database.GetDbConnection().CreateCommand()) 
			{
				command.CommandText = "select tarih,[1],[2],[3],[4],[5] from (select [City],[Count],cast([ElectricDate] as date) as tarih from Elektriks) as electricT pivot(sum(count) for city in([1],[2],[3],[4],[5]) )  as ptable order by tarih asc";

				command.CommandType=System.Data.CommandType.Text;
				_context.Database.OpenConnection();

				using (var reader = command.ExecuteReader()) 
				{
					while (reader.Read()) 
					{
						ElectricChart electricChart= new ElectricChart();
						electricChart.ElectricDate = reader.GetDateTime(0).ToShortDateString();

						Enumerable.Range(1, 5).ToList().ForEach(x =>
						{
							if (System.DBNull.Value.Equals(reader[x]))
							{
								electricChart.Counts.Add(0);
							}
							else
							{
								electricChart.Counts.Add(reader.GetInt32(x));
							}
						});

						electricCharts.Add(electricChart);
					}
				}

				_context.Database.CloseConnection();
				return electricCharts;
			}
		}

	}
}
