using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Fritz.ResourceManagement.Web.Data
{
	public class ScheduleRepository : IScheduleRepository
	{

		public ScheduleRepository(MyDbContext db)
		{
			this.Db = db;
		}

		public MyDbContext Db { get; }

		public async Task<IEnumerable<Schedule>> GetSchedulesAsync(DateTime startDate, DateTime endDate)
		{

			return Db.Schedules;

		}
	}


}
