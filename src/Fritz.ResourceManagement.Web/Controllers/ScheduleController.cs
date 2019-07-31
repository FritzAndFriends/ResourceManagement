using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fritz.ResourceManagement.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ScheduleController : ControllerBase
	{
		private readonly MyDbContext _DbContext;

		public ScheduleController(MyDbContext dbContext)
		{
			this._DbContext = dbContext;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByScheduleId([FromRoute]int id) {

			var schedule = await _DbContext
				.Schedules.AsNoTracking()
				.Include(s => s.ScheduleItems)
				.Include(s => s.RecurringSchedules)
				.FirstOrDefaultAsync(s => s.Id == id);

			if (schedule == null) return NotFound();

			return Ok(schedule);

		}

		[HttpGet("forrange/{startTime}/{endTime}")]
		public async Task<IActionResult> GetSchedulesForDateRange([FromRoute]DateTime startTime, [FromRoute]DateTime endTime) {

			return Ok(await _DbContext.Schedules.AsNoTracking().ToArrayAsync());
				
			// TODO: Filter the schedule appropriately...  each ScheduleItem, RecurringSchedule, and ScheduleException
			//.Where(s => s. MyScheduleState.DisplayBeginDate, MyScheduleState.DisplayEndDate);

		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSchedule([FromBody]Schedule schedule, [FromRoute]int id) {

			_DbContext.Attach(schedule);

			_DbContext.Schedules.Update(schedule);
			await _DbContext.SaveChangesAsync();

			return CreatedAtAction(nameof(GetByScheduleId), new { id = schedule.Id });

		}



	}
}
