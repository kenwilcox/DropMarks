using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DropMarks
{
  public partial class frmMain : Form
  {
    private Size _compare;
    private DropMarks _dm;
    //private BindingSource _bs;

    public frmMain()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Remove the column header border in the Aero theme in Vista/Win 7,
    /// but keep it for other themes such as standard and classic.
    /// http://www.dotnetperls.com/datagridview-tips
    /// </summary>
    static DataGridViewHeaderBorderStyle ProperColumnHeadersBorderStyle
    {
      get
      {
        // Need to compare this to windows version - NOT a fontname...
        /*
         Windows XP 5.1
         Windows XP64 5.2
         Windows Vista 6.0
         Windows 7 6.1
 
         Windows 2003 5.2
         Windows 2008 6.0
        */
        //return (SystemFonts.MessageBoxFont.Name == "Segoe UI") ? DataGridViewHeaderBorderStyle.None : DataGridViewHeaderBorderStyle.Raised;
        return (Environment.OSVersion.Version.Major > 5) ?
          DataGridViewHeaderBorderStyle.None :
          DataGridViewHeaderBorderStyle.Raised;
      }
    } 

    private void frmMain_Load(object sender, EventArgs e)
    {
      //grid.ColumnHeadersBorderStyle = ProperColumnHeadersBorderStyle;

      _compare = new Size(250, 250);

      pnlDrop.Visible = false;
      pnlDrop.Dock = DockStyle.Fill;

      _dm = new DropMarks();
      if (!_dm.Load())
      {
        // If I was unsuccessful loading any dropmarks, start with a default set
        _dm.Add(new DropMark("Google", "http://www.google.com"));
        _dm.Add(new DropMark("Bing", "http://www.bing.com"));
        _dm.Add(new DropMark("Flickr", "http://www.flickr.com"));
        _dm.Add(new DropMark("Tumblr", "http://www.tumblr.com"));
        _dm.Add(new DropMark("Piccsy", "http://www.piccsy.com"));
        _dm.Add(new DropMark("500px", "http://www.500px.com"));
      }
      //_dm.Sort(DropMarkCompare.CompareTitle);
      //_bs = new BindingSource();
      //_bs.DataSource = _dm;
      //grid.DataSource = _bs;

      grid.DataSource = _dm;
      // Format the Columns
      DataMarkColumns.SetColumnInfo(grid);
    }

    private void frmMain_ResizeEnd(object sender, EventArgs e)
    {
      // 172, 202
      //MessageBox.Show(Size.ToString());
    }

    private void frmMain_Resize(object sender, EventArgs e)
    {
      Size form = this.Size;
      if (form.Height < _compare.Height || form.Width < _compare.Width)
      {
        pnlDrop.Visible = true;
        pnlDrop.BringToFront();
      }
      else
      {
        pnlDrop.Visible = false;
      }
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_dm != null) _dm.Save();
      
      //foreach (DataGridViewColumn col in grid.Columns)
      //{
      //  if (col.Visible) MessageBox.Show(col.Width.ToString());
      //}
    }

    private void grid_DoubleClick(object sender, EventArgs e)
    {
      // This works
      //DropMark dm = _dm[grid.CurrentRow.Index];
      //MessageBox.Show(dm.Title);
      //_dm.Add(new DropMark("Can We Add Something", "http://www.microsoft.com"));

      // This is easy...
      //grid.Rows[0].Visible = !grid.Rows[0].Visible;
      //_bs.Filter = "Title = 'Google' or Title = 'Bing'";
    }

    private void button1_Click(object sender, EventArgs e)
    {
      //_bs.Filter = "Title = 'Google' OR Title = 'Bing'";
      //int i = 0;
      foreach (DataGridViewRow row in grid.Rows)
      {
        //MessageBox.Show(row.Cells["Title"].Value.ToString());
        if (row.Cells["Title"].Value.ToString().Contains("Google"))
        {
          row.Visible = false;
        }
        /*
        if (row["Title"].ToString().Contains("Google"))
        {
          grid.Rows[i].Visible = false;
        }
        i++;
        */
      }
    }

    private void grid_DragLeave(object sender, EventArgs e)
    {
      this.Text = "DragLeave";
    }

    private void grid_DragEnter(object sender, DragEventArgs e)
    {
      //e.Effect = DragDropEffects.Copy;
    }

    private void grid_DragDrop(object sender, DragEventArgs e)
    {
      //e.Effect = DragDropEffects.Copy;
    }

    private void grid_MouseDown(object sender, MouseEventArgs e)
    {
      // This kills the sorting...
      //string file = @"C:\Temp\temp.url";
      //DataObject dataObj = new DataObject(DataFormats.FileDrop, file);
      //DragDropEffects dropEffect = grid.DoDragDrop(dataObj, DragDropEffects.All);
      ////MessageBox.Show(dropEffect.ToString());
      ////this.Text = dropEffect.ToString();
      //if (dropEffect == DragDropEffects.Copy)
      //{
      //  //this.Text = dataObj.ToString();
      //  // Where Was I dropped?
      //  foreach (string item in Clipboard.GetFileDropList())
      //  {
      //    Text += " " + item;
      //  }
      //}

    }

  }
}
