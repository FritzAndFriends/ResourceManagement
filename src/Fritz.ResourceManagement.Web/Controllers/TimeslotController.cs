using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.Scheduling;
using Fritz.ResourceManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
		public async Task<IActionResult> GetForSchedule([FromRoute]int scheduleId, [FromRoute]DateTime startDate, [FromRoute]DateTime endDate)
		{

			var theSchedule = await _MyDbContext.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleId);
			if (theSchedule == null) return NotFound();

			var timeSlots = _ScheduleManager.ExpandSchedule(theSchedule, startDate, endDate);

			return Ok(timeSlots.ToArray());

		}


	}
}
