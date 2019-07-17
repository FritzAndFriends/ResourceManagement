using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fritz.ResourceManagement.Scheduling.Extensions
{
  public static class DateTimeExtensions
  {
	#region extensions for date only
		/// <summary>
		/// Returns 'true' if Date is the same as object
		/// </summary>
	public static bool IsOnDate(this DateTime value, DateTime date)
	{
	  return value.Date == date.Date;
	}

	/// <summary>
	/// Returns 'true' if this DateTime's date is after <c>date</c>
	/// </summary>
	public static bool IsAfterDate(this DateTime value, DateTime date)
	{
	  return value.Date > date.Date;
	}

	/// <summary>
	/// Returns 'true' if this DateTime's date is before <c>date</c>
	/// </summary>
	public static bool IsBeforeDate(this DateTime value, DateTime date)
	{
	  return value.Date < date.Date;
	}

	/// <summary>
	/// Returns 'true' if this DateTime's date is same as or after <c>date</c>
	/// </summary>
	public static bool IsOnOrAfterDate(this DateTime value, DateTime date)
	{
	  return value.IsOnDate(date) || value.IsAfterDate(date);
	}

	/// <summary>
	/// Returns 'true' if this DateTime's date is same as or before <c>date</c>
	/// </summary>
	public static bool IsOnOrBeforeDate(this DateTime value, DateTime date)
	{
	  return value.IsOnDate(date) || value.IsBeforeDate(date);
	}

	/// <summary>
	/// Returns 'true' if this DateTime's date is between <c>firstDate</c> and <c>secondDate</c>
	/// </summary>
	public static bool IsBetweenDates(this DateTime value, DateTime firstDate, DateTime secondDate)
	{
	  if (firstDate.IsAfterDate(secondDate))
		return value.IsBetweenDates(secondDate, firstDate);

	  return value.IsOnOrAfterDate(firstDate) && value.IsBeforeDate(secondDate);
	}

	#endregion

	#region extensions for dateTime 

	/// <summary>
	/// Returns 'true' if this <c>DateTime</c> is after <c>dateTime</c>
	/// </summary>
	public static bool IsAfter(this DateTime value, DateTime dateTime)
	{
	  return value > dateTime;
	}

	/// <summary>
	/// Returns 'true' if this <c>DateTime</c> is same as or after <c>dateTime</c>
	/// </summary>
	public static bool IsSameOrAfter(this DateTime value, DateTime dateTime)
	{
	  return value >= dateTime;
	}

	/// <summary>
	/// Returns 'true' if this <c>DateTime</c> is same as or before <c>dateTime</c>
	/// </summary>
	public static bool IsSameOrBefore(this DateTime value, DateTime dateTime)
	{
	  return value <= dateTime;
	}

	/// <summary>
	/// Returns 'true' if this <c>DateTime</c> is before <c>dateTime</c>
	/// </summary>
	public static bool IsBefore(this DateTime value, DateTime dateTime)
	{
	  return value < dateTime;
	}

	#endregion
  }
}
