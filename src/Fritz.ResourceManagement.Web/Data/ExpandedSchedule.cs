using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;

namespace Fritz.ResourceManagement.Web.Data
{
	public class ExpandedSchedule
	{

		public List<TimeSlot> TimeSlots { get; } = new List<TimeSlot>();

	}
}
