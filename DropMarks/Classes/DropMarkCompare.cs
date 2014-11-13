using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DropMarks
{
  // With the SortableBindingList - this class is no longer needed
  public static class DropMarkCompare
  {
    public static int CompareTitle(DropMark a, DropMark b)
    {
      string A = a.Title;
      string B = b.Title;

      return A.CompareTo(B);
    }

    public static int CompareUtl(DropMark a, DropMark b)
    {
      string A = a.Url;
      string B = b.Url;

      return A.CompareTo(B);
    }

    public static int CompareCreateDate(DropMark a, DropMark b)
    {
      DateTime A = a.CreatedAt;
      DateTime B = b.CreatedAt;

      return A.CompareTo(B);
    }

    public static int CompareUpdateDate(DropMark a, DropMark b)
    {
      DateTime A = a.UpdatedAt;
      DateTime B = b.UpdatedAt;

      return A.CompareTo(B);
    }

  }
}
