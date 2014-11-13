using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DMClasses;

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
        return (Environment.OSVersion.Version.Major > 5) ?
          DataGridViewHeaderBorderStyle.None :
          DataGridViewHeaderBorderStyle.Raised;
      }
    } 

    private void frmMain_Load(object sender, EventArgs e)
    {
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
      DropMarkColumns.SetColumnInfo(grid);

      textBox1.AddMenuItem("Contains", true);
      textBox1.AddMenuItem("Starts With");
      textBox1.AddMenuItem("Equals");

      m_DelegateOpenFile = new DelegateOpenFile(this.OpenFile);
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
      DropMark dm = _dm[grid.CurrentRow.Index];
      MessageBox.Show(dm.Title);
      //_dm.Add(new DropMark("Can We Add Something", "http://www.microsoft.com"));

      // This is easy...
      //grid.Rows[0].Visible = !grid.Rows[0].Visible;
      //_bs.Filter = "Title = 'Google' or Title = 'Bing'";
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

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      // Filter the list based on what they typed
      grid.FilterBy(((SearchTextBox)sender).Text, false);
    }

    private void textBox1_ClearClicked(object sender, EventArgs e, string searchText)
    {
      // Clear the filter
      grid.FilterClear();
    }

    private void frmMain_DragEnter(object sender, DragEventArgs e)
    {
      //if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
      //else
      //  e.Effect = DragDropEffects.None;
    }

    private delegate void DelegateOpenFile(String s);
    private DelegateOpenFile m_DelegateOpenFile;

    private void OpenFile(string sFile)
    {
      MessageBox.Show(sFile);
    }

    //http://www.codeproject.com/Articles/3598/Drag-and-Drop-files-from-Windows-Explorer-to-Windo

    private void frmMain_DragDrop(object sender, DragEventArgs e)
    {
      Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
      if (a != null)
      {
        // Extract string from first array element
        // (ignore all files except first if number of files are dropped).
        string s = a.GetValue(0).ToString();

        // Call OpenFile asynchronously.
        // Explorer instance from which file is dropped is not responding
        // all the time when DragDrop handler is active, so we need to return
        // immidiately (especially if OpenFile shows MessageBox).

        this.BeginInvoke(m_DelegateOpenFile, new Object[] { s });

        this.Activate();        // in the case Explorer overlaps this form
      }
      else
      {

        //a = (Array)e.Data.GetFormats(true);
        //MessageBox.Show(String.Join(" - ", (string[])a));
        StreamReader reader = new StreamReader((Stream)e.Data.GetData("UniformResourceLocator"));
        OpenFile(reader.ReadToEnd());
      }
    }

    private void textBox1_MenuItemClicked(object sender, MenuClickedArgs mca)
    {
      //if (mca.Text == "Contains") grid.SetFilterMode(FilterMode.Contains);
      //if (mca.Text == "Starts With") grid.SetFilterMode(FilterMode.StartsWith);

      // Better for localizations
      if (mca.Index == 0) grid.SetFilterMode(FilterMode.Contains);
      if (mca.Index == 1) grid.SetFilterMode(FilterMode.StartsWith);
      if (mca.Index == 2) grid.SetFilterMode(FilterMode.Equals);
    }

    //private void button1_Click(object sender, EventArgs e)
    //{
    //  //DropMark dm = new DropMark("Stack Overflow", "http://stackoverflow.com");
    //  //dm.Comment = "Handy Dandy Website!";
    //  //_dm.Add(dm);
    //}

  }
}
