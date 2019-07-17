using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fritz.ResourceManagement.Scheduling.Extensions
{
  public static class DateTimeExtensions
  {
	public static bool IsOn(this DateTime value, DateTime date)
	{
	  return value.Date == date.Date;
	}
	
	public static bool IsAfter(this DateTime value, DateTime date)
	{
	  return value.Date > date.Date;
	}

	public static bool IsBefore(this DateTime value, DateTime date)
	{
	  return value.Date < date.Date;
	}

	public static bool IsOnOrAfter(this DateTime value, DateTime date)
	{
	  return value.IsOn(date) || value.IsAfter(date);
	}

	public static bool IsOnOrBefore(this DateTime value, DateTime date)
	{
	  return value.IsOn(date) || value.IsBefore(date);
	}

	public static bool IsBetween(this DateTime value, DateTime firstDate, DateTime secondDate)
	{
	  if (firstDate.IsAfter(secondDate))
		return value.IsBetween(secondDate, firstDate);

	  return value.IsOnOrAfter(firstDate) && value.IsBefore(secondDate);
	}
  }
}
