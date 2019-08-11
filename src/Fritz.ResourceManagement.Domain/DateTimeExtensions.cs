using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class DateTimeExtensions
  {
	#region extensions for date only
	/// <summary>
	/// Returns 'true' if  DateTime's date is the same as <c>date</c>
	/// </summary>
	public static bool IsOn(this DateTime value, DateTime date)
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
	public static bool IsBefore(this DateTime value, DateTime date)
	{
	  return value.Date < date.Date;
	}

	/// <summary>
	/// Returns 'true' if this DateTime's date is same as or after <c>date</c>
	/// </summary>
	public static bool IsOnOrAfter(this DateTime value, DateTime date)
	{
	  return value.IsOn(date) || value.IsAfterDate(date);
	}

	/// <summary>
	/// Returns 'true' if this DateTime's date is same as or before <c>date</c>
	/// </summary>
	public static bool IsOnOrBefore(this DateTime value, DateTime date)
	{
	  return value.IsOn(date) || value.IsBefore(date);
	}

	/// <summary>
	/// Returns 'true' if this DateTime's date is between <c>firstDate</c> and <c>secondDate</c>
	/// </summary>
	public static bool IsBetweenDates(this DateTime value, DateTime firstDate, DateTime secondDate)
	{
	  if (firstDate.IsAfterDate(secondDate))
		return value.IsBetweenDates(secondDate, firstDate);

	  return value.IsOnOrAfter(firstDate) && value.IsBefore(secondDate);
	}

	#endregion
  }
}
