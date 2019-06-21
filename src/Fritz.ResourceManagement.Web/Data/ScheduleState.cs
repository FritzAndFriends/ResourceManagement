using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fritz.ResourceManagement.Domain;

namespace Fritz.ResourceManagement.Web.Data
{

  public class ScheduleState
  {

	public Schedule Schedule { get; set; }

	public DateTime SelectedDate { get; private set; }

	public void SelectDate(DateTime newDate)
	{
	  SelectedDate = newDate;
	  OnSelectedDateChanged?.Invoke(null, new SelectedDateChangedArgs() { SelectedDate = newDate });
	}

	public void ScheduleUpdated()
	{
	  OnSelectedDateChanged?.Invoke(null, new SelectedDateChangedArgs() { SelectedDate = SelectedDate });
	}

	public event EventHandler<SelectedDateChangedArgs> OnSelectedDateChanged;

	public class SelectedDateChangedArgs : EventArgs {

	  public DateTime SelectedDate { get; set; }

	}



  }


}
