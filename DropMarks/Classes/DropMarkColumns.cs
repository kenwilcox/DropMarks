using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DropMarks
{
  public class DropMarkColumns
  {
    // So we're doing formatting here...
    public static void SetColumnInfo(DataGridViewColumn col)
    {
      switch (col.HeaderText)
      {
        case "CreatedAt": col.Visible = false; break; //col.HeaderText = "Created At"; break;
        case "UpdatedAt": col.HeaderText = "Modified"; col.Width = 120; break;
        case "Comment": col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; break;
        case "Url": col.Visible = false; break;
        case "Tags": col.Visible = false; break; // Move to Tree View?
        //case "Notes": col.Visible = false; break;
      }
    }

    public static void SetColumnInfo(DataGridView grid)
    {
      foreach (DataGridViewColumn col in grid.Columns)
      {
        SetColumnInfo(col);
      }
    }
  }
}
