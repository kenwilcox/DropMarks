using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DropMarks
{
  public enum FilterMode
  {
    Contains,
    StartsWith,
    Equals,
  }

  public static class GridFilterEx
  {
    private static FilterMode _filterMode = FilterMode.Contains;

    private static bool FilterCompare(string str1, string str2)
    {
      bool ret = false;
      switch(_filterMode)
      {
        case FilterMode.Contains:
          ret = str1.Contains(str2); break;
        case FilterMode.StartsWith:
          ret = str1.StartsWith(str2); break;
        case FilterMode.Equals:
          ret = str1.Equals(str2); break;
      }
      return ret;
    }

    public static void SetFilterMode(this DataGridView grid, FilterMode mode)
    {
      _filterMode = mode;
    }

    public static void FilterBy(this DataGridView grid, string Value, bool visibleOnly)
    {
      FilterClear(grid);
      DataGridViewCell cell = grid.CurrentCell;
      grid.CurrentCell = null;
      Value = Value.ToUpper();

      foreach (DataGridViewRow row in grid.Rows)
      {
        bool visible = false;

        for (int i = 0; i <= row.Cells.Count - 1; i++)
        {
          if (visibleOnly)
          {
            // If they're only wanting to filter by visible cells
            // and if the current cell isn't visible, then
            // go to the next cell
            if (!row.Cells[i].Visible) continue;
          }
          
          //if (row.Cells[i].Value.ToString().ToUpper().Contains(Value))
          if (FilterCompare(row.Cells[i].Value.ToString().ToUpper(), Value))
          {
            visible = true;
            break;
          }
        }
        
        row.Visible = visible;
      }

      // Try to set the current cell
      try
      {
        grid.CurrentCell = cell;
      }
      catch { }
    }

    public static void FilterClear(this DataGridView grid)
    {
      foreach (DataGridViewRow row in grid.Rows)
      {
        row.Visible = true;
      }
    }
  }
}
