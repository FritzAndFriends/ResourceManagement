using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;

namespace Fritz.ResourceManagement.Web.Data
{
	public interface IScheduleRepository
	{

		Task<IEnumerable<Schedule>> GetSchedulesAsync(DateTime startDate, DateTime endDate);

	}


}
