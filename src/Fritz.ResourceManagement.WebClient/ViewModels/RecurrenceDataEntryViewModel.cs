using System;
using System.Collections.Generic;
using System.Linq;
using Fritz.ResourceManagement.Domain;
using Microsoft.AspNetCore.Components;

namespace Fritz.ResourceManagement.WebClient.ViewModels
{
	public class RecurrenceDataEntryViewModel
	{
		public RecurringSchedule Schedule { get; set; }

		public EventCallback OnSave { get; set; }

		public string Pattern { get; set; }

		public TimeSpan TimeOfDay { get; set; }

		public HashSet<string> DaysOfTheWeek { get; set; } = new HashSet<string>();


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
