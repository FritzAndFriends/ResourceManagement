using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.Scheduling;
using Fritz.ResourceManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Fritz.ResourceManagement.Web.Controllers
{

	[ApiController]
	[Route("api/[Controller]")]
	public class TimeslotController : Controller
	{
		private readonly MyDbContext _MyDbContext;
		private readonly ScheduleManager _ScheduleManager;

		public TimeslotController(MyDbContext myDbContext, ScheduleManager scheduleManager)
		{
			_MyDbContext = myDbContext;
			_ScheduleManager = scheduleManager;
		}

		[HttpGet("{scheduleId}/{startDate}/{endDate}")]
		public async Task<IActionResult> GetForSchedule([FromRoute]int scheduleId, [FromRoute]string startDate, [FromRoute]string endDate)
		{

			var theStartDate = DateTime.ParseExact(startDate, "MM.dd.yyyy", CultureInfo.InvariantCulture);
			var theEndDate = DateTime.ParseExact(endDate, "MM.dd.yyyy", CultureInfo.InvariantCulture);


			var theSchedule = await _MyDbContext.Schedules
				.Include(s => s.ScheduleItems)
				.Include(s => s.RecurringSchedules)
				.Include(s => s.ScheduleExceptions)
				.FirstOrDefaultAsync(s => s.Id == scheduleId);
			if (theSchedule == null) return NotFound();

			var timeSlots = _ScheduleManager.ExpandSchedule(theSchedule, theStartDate, theEndDate);

			return Ok(timeSlots.ToArray());

		}


	}
}
