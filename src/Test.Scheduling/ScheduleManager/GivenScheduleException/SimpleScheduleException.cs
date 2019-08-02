using System;
using Fritz.ResourceManagement.Domain;

namespace Test.Scheduling.ScheduleManager.GivenScheduleException
{
	public abstract class SimpleScheduleException
	{

		protected DateTime BeginExceptionDate { get; set; } = new DateTime(2019, 7, 19);
		protected DateTime EndExceptionDate { get; set; } = new DateTime(2019, 7, 21, 23, 59, 59);

		protected ScheduleException TheWeekend
		{
			get
			{

				return new ScheduleException()
				{
					StartDateTime = BeginExceptionDate,
					EndDateTime = EndExceptionDate,
					Name = "The Weekend"
				};

			}
		}


	}

}
