using System;
using System.Collections.Generic;
using System.Linq;
using Fritz.ResourceManagement.Domain;
using Fritz.ResourceManagement.Scheduling;
using Xunit;

namespace Test.Scheduling.GivenSimpleSchedule {

  public class WhenExpandingSchedule {

    [Fact]
    public void ShouldCreateTheScheduleItemsSpecified() {

      // arrange
      var mySchedule = new Schedule() {
        ScheduleItems = new List<ScheduleItem> {
          new ScheduleItem {
            StartDateTime=new DateTime(2019, 5, 17, 15, 0, 0),
            EndDateTime = new DateTime(2019, 5, 17, 16, 0, 0)
          }
        }
      };

      // act
      var sut = new ScheduleManager();
      var results  = sut.ExpandSchedule(mySchedule, new DateTime(2019, 1, 1), new DateTime(2019,12,31));

      // assert
      Assert.Single(results);
      Assert.Equal(15, results.First().StartDateTime.Hour);

    }

  }

}