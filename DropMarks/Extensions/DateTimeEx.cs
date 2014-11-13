using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DropMarks
{
  public static class DateTimeEx
  {
    public static DateTime ParseUnixTimestamp(double timestamp)
    {
      DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
      return epoch.AddSeconds(timestamp);
    }

    public static double ToUnixTimestamp(DateTime date)
    {
      DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
      TimeSpan span = (date.ToLocalTime() - epoch);
      return Math.Floor(span.TotalSeconds);
    }
  }
}
