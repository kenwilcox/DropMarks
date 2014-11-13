using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DropMarks
{
  public partial class frmTags : Form
  {
    private DMTags _tag;
    DropMarks _dm;

    public frmTags()
    {
      InitializeComponent();
      _tag = DMTags.Instance;
    }

    private void RefreshList()
    {
      ((CurrencyManager)listBox1.BindingContext[listBox1.DataSource]).Refresh();
    }

    private void bAdd_Click(object sender, EventArgs e)
    {
      _tag.Add(textBox1.Text);
      RefreshList();
    }

    private void listBox1_DoubleClick(object sender, EventArgs e)
    {
      if (listBox1.SelectedItem != null)
      {
        _tag.Remove((string)listBox1.SelectedItem);
        RefreshList();
      }
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      _tag.Save("Tags.json");
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      try
      {
        _tag.Load("Tags.json");
      }
      catch
      {
        _tag.Add("One");
        _tag.Add("Two");
        _tag.Add("Three");
      }
      listBox1.DataSource = _tag.Items;
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        _tag.Add(textBox1.Text);
        RefreshList();
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (_dm == null)
      {
        _dm = new DropMarks();
        _dm.Add(new DropMark("This is my Title", "http://www.google.com"));
      }
      _dm.Save();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (_dm == null) _dm = new DropMarks();
      _dm.Load();
    }
  }
}
