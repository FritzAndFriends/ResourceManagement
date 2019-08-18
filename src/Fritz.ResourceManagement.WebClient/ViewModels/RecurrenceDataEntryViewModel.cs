using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;
using Microsoft.AspNetCore.Components;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class RecurrenceDataEntryViewModel : ComponentBase
	{
		// Cheer 1000 cpayette 27/06/19
		// Cheer 500 cpayette 8/08/19
		
		[Parameter]
		public RecurringSchedule Schedule { get; set; }

		[Parameter]
		public EventCallback OnSave { get; set; }

		public string Pattern { get; set; }

		public TimeSpan TimeOfDay { get; set; }

		public HashSet<string> DaysOfTheWeek { get; set; } = new HashSet<string>();

		public TimeSpan EndTime { get; set; }
		
		public void OnDOWChange(string day)
		{
			if (this.DaysOfTheWeek.Contains(day))
			{
				this.DaysOfTheWeek.Remove(day);
			}
			else
			{
				this.DaysOfTheWeek.Add(day);
			}
		}

		public void Save()
		{
			if (this.OnSave.HasDelegate)
			{
				this.Schedule.CronPattern = this.CalculatedCronPattern();

				this.OnSave.InvokeAsync(this.Schedule);
			}
		}

		public async Task TimeChanged(UIChangeEventArgs args)
		{
			// TODO: Simon G - This was pulled in from the Code block in RecurrenceDataEntry.razor but does not appear to be called is it still needed?
			if (EndTime != null)
				return;

			if (this.TimeOfDay > EndTime)
			{
				this.EndTime = this.EndTime.Add(TimeSpan.FromDays(1));
			}
			this.Schedule.Duration = this.EndTime.Subtract(this.TimeOfDay);
		}
		
		private string CalculatedCronPattern()
		{
			var sb = new System.Text.StringBuilder();

			sb.Append($"{this.TimeOfDay.Minutes} ");
			sb.Append($"{this.TimeOfDay.Hours} ");
			sb.Append("* * ");

			if (this.Pattern == "D" || !this.DaysOfTheWeek.Any())
			{
				sb.Append("*");
			}
			else
			{
				sb.Append(string.Join(",", this.DaysOfTheWeek.ToArray()));
			}
			return sb.ToString();
		}
	}
}
