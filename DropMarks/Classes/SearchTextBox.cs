using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace DMClasses
{
  public partial class SearchTextBox : TextBox
  {
    //Properties
    private string mCue;
    private PictureBox pb;
    private ContextMenuStrip menu;
    private Image loupe;
    private Image cancel;


    //Constructors
    public SearchTextBox()
    {
      InitializeComponent();
      InitializeProperties();
    }

    public SearchTextBox(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
      InitializeProperties();
    }

    private void InitializeProperties()
    {
      mCue = " Search";
    }

    //Externs
    [DllImport("user32.dll", EntryPoint = "SendMessageW")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);


    //Acccessors
    [Category("CC")]
    [Description("The cue (help) text associated with the control.")]
    [DefaultValue(" Search")]
    public string Cue
    {
      get
      {
        return mCue;
      }
      set
      {
        mCue = value;

        if (String.IsNullOrEmpty(mCue))
        {
          mCue = " Search";
        }
        updateCue();
      }
    }

    [Browsable(false)]
    public override bool Multiline
    {
      get
      {
        return false;
      }
      set
      {
        base.Multiline = false;
      }
    }


    //Events
    EventHandler click;

    //public delegate void MenuClickedEvent(object sender, EventArgs e, string menuText);
    public delegate void MenuClickedEvent(object sender, MenuClickedArgs mca);
    public delegate void ClearClickedEvent(object sender, EventArgs e, string searchText);

    /// <summary>
    /// Returns the text of the menu clicked.
    /// </summary>
    [Description("Returns the text of the menu clicked.")]
    public event MenuClickedEvent MenuItemClicked;

    /// <summary>
    /// Returns the search text before it is cleared.
    /// </summary>
    [Description("Fires when the user clicks the 'cancel' button.")]
    public event ClearClickedEvent ClearClicked;

    //Event Handlers
    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      //this.BorderStyle = BorderStyle.FixedSingle;

      loupe = LoupeImage();
      cancel = CancelImage();

      SetMargin(1, 20);
      pb = new PictureBox();
      pb.Parent = this;

      // The border is not the same and the picture box looks 
      // weird if we don't move it.
      if (this.BorderStyle == BorderStyle.FixedSingle)
        pb.Top = 2;
      else
        pb.Top = 1;

      pb.Left = this.Width - 20;
      pb.Size = new Size(13, 13);
      pb.Click += new System.EventHandler(this.pb_Click);
      pb.Cursor = Cursors.Default;
      pb.Image = loupe;

      this.TextChanged += new System.EventHandler(this.text_TextChanged);
      click = new System.EventHandler(this.menu_Click);
      menu = new ContextMenuStrip();

      updateCue();
    }

    protected override void OnResize(EventArgs e)
    {
      // move the picture box to the right place
      if (pb != null)
      {
        pb.Left = this.Width - 20;
        if (this.BorderStyle == BorderStyle.FixedSingle)
          pb.Top = 2;
        else
          pb.Top = 1;

      }
    }

    protected override void OnReadOnlyChanged(EventArgs e)
    {
      // I don't have access to _BackColorReadOnly...
      base.OnReadOnlyChanged(e);
      // ...So it's easier to just hide the picture and menu control
      pb.Visible = !ReadOnly;
    }

    private void text_TextChanged(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(this.Text))
        pb.Image = loupe;
      else
        pb.Image = cancel;
    }

    private void pb_Click(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(this.Text))
        menu.Show(Control.MousePosition);
      else
      {
        if (ClearClicked != null)
          ClearClicked(sender, e, this.Text);
        this.Text = String.Empty;
      }
    }

    private void menu_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem item = (sender as ToolStripMenuItem);
      // Uncheck everyone
      //foreach (ToolStripMenuItem i in menu.Items)
      //  i.Checked = false;
      int index = -1;
      for (int i = 0; i < menu.Items.Count; i++)
      {
        ToolStripMenuItem id = (ToolStripMenuItem)menu.Items[i];
        id.Checked = false;
        if (id.Text.Equals((sender as ToolStripMenuItem).Text)) index = i;
      }
        // Check me!
      item.Checked = true;
      string s = item.Text;

      Cue = " " + s;
      //updateCue();
      if (MenuItemClicked != null)
        MenuItemClicked(sender, new MenuClickedArgs(e, index, s));
    }


    //Methods
    private void updateCue()
    {
      if (!this.IsHandleCreated || string.IsNullOrEmpty(mCue)) return;

      IntPtr mem = Marshal.StringToHGlobalUni(mCue);
      SendMessage(this.Handle, 0x1501, (IntPtr)1, mem);
      Marshal.FreeHGlobal(mem);
    }

    // http://www.developerfusion.com/code/167/margins-in-a-textbox/
    // Make the margin so the search "icons" don't cover up entered text
    private void SetMargin(int left, int right)
    {
      int EM_SETMARGINS = 0xD3;
      int EC_LEFTMARGIN = 0x01;
      int EC_RIGHTMARGIN = 0x02;

      // right needs to be in the hi-word, so multiply by 65536
      long value = 65536 * right + left;
      IntPtr ptr = new IntPtr(EC_LEFTMARGIN | EC_RIGHTMARGIN);
      SendMessage(this.Handle, EM_SETMARGINS, ptr, (IntPtr)value);
    }

    private Image CancelImage()
    {
      // Trust me, it's a picture of an X - Ken Wilcox - Dec 11, 2009
      byte[] cancelData = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 
                0, 0, 13, 0, 0, 0, 13, 8, 2, 0, 0, 0, 253, 137, 115, 43, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 
                206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 32, 99, 72, 
                82, 77, 0, 0, 122, 38, 0, 0, 128, 132, 0, 0, 250, 0, 0, 0, 128, 232, 0, 0, 117, 48, 0, 0, 234, 
                96, 0, 0, 58, 152, 0, 0, 23, 112, 156, 186, 81, 60, 0, 0, 0, 130, 73, 68, 65, 84, 40, 83, 99, 
                252, 255, 255, 63, 3, 49, 0, 168, 142, 24, 192, 64, 140, 34, 144, 157, 112, 117, 201, 203, 174, 
                245, 236, 186, 7, 231, 2, 217, 64, 17, 56, 23, 161, 174, 103, 219, 53, 195, 166, 189, 64, 18, 
                40, 135, 204, 134, 40, 69, 177, 183, 101, 221, 57, 195, 210, 181, 112, 18, 217, 73, 232, 238, 
                107, 89, 118, 76, 51, 115, 14, 144, 68, 115, 55, 170, 121, 243, 246, 106, 70, 246, 180, 192, 
                72, 236, 230, 85, 79, 219, 170, 232, 91, 13, 36, 129, 210, 200, 108, 116, 247, 5, 229, 246, 
                84, 79, 90, 11, 55, 3, 200, 6, 138, 96, 241, 47, 254, 128, 36, 61, 156, 241, 155, 7, 0, 82, 
                113, 170, 68, 247, 231, 104, 36, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 };
      MemoryStream ms = new MemoryStream(cancelData);
      return Image.FromStream(ms);
    }

    private Image LoupeImage()
    {
      // Trust me, it's a picture of a loupe (magnifing glass) - Ken Wilcox - Dec 11, 2009
      byte[] loupeData = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 
                0, 13, 0, 0, 0, 13, 8, 6, 0, 0, 0, 114, 235, 228, 124, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 
                28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 32, 99, 72, 82, 
                77, 0, 0, 122, 38, 0, 0, 128, 132, 0, 0, 250, 0, 0, 0, 128, 232, 0, 0, 117, 48, 0, 0, 234, 96, 
                0, 0, 58, 152, 0, 0, 23, 112, 156, 186, 81, 60, 0, 0, 1, 8, 73, 68, 65, 84, 40, 83, 99, 252, 15, 
                4, 12, 56, 192, 220, 59, 31, 24, 56, 89, 24, 24, 162, 20, 4, 80, 85, 128, 52, 161, 131, 236, 101, 
                215, 254, 27, 118, 29, 67, 193, 213, 155, 174, 193, 149, 49, 96, 104, 88, 116, 238, 191, 97, 211, 
                222, 255, 217, 203, 206, 253, 63, 118, 253, 253, 255, 189, 151, 159, 255, 79, 158, 7, 17, 171, 
                94, 7, 209, 136, 161, 201, 178, 18, 168, 1, 168, 8, 29, 68, 245, 29, 251, 15, 146, 195, 208, 52, 
                103, 215, 189, 255, 150, 133, 91, 255, 239, 61, 255, 28, 67, 211, 210, 125, 8, 57, 20, 155, 150, 
                130, 52, 229, 110, 253, 191, 245, 56, 166, 166, 57, 219, 174, 129, 229, 142, 1, 157, 139, 233, 
                188, 204, 181, 255, 131, 160, 206, 64, 182, 206, 9, 232, 2, 39, 160, 38, 172, 126, 2, 73, 88, 2, 
                53, 130, 20, 77, 1, 122, 188, 103, 213, 53, 176, 98, 144, 216, 214, 227, 247, 48, 53, 121, 1, 21, 
                90, 166, 174, 253, 159, 220, 181, 23, 76, 195, 48, 72, 211, 214, 195, 16, 13, 40, 54, 57, 101, 
                46, 253, 111, 24, 59, 231, 127, 203, 172, 99, 112, 201, 115, 215, 159, 255, 191, 118, 31, 211, 
                127, 96, 63, 89, 198, 246, 252, 215, 244, 109, 249, 95, 61, 9, 226, 102, 66, 128, 97, 233, 166, 
                99, 255, 21, 109, 178, 255, 23, 119, 45, 37, 164, 22, 213, 121, 123, 247, 33, 156, 68, 140, 78, 
                0, 9, 152, 38, 84, 28, 139, 66, 168, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130};
      MemoryStream ms = new MemoryStream(loupeData);
      return Image.FromStream(ms);
    }

    // Used to convert an Image to a byte array so it can be used in code like above
    public static byte[] ImageToBytes(Image image)
    {
      MemoryStream ms = new MemoryStream();
      image.Save(ms, ImageFormat.Png);
      byte[] data = ms.ToArray();
      //foreach (byte d in data)
      //{
      //    textBox1.Text += d.ToString() + ",";
      //}
      return data;
    }

    public void AddMenuItem(string text)
    {
      AddMenuItem(text, false);
    }

    public void AddMenuItem(string text, bool selected)
    {
      ToolStripMenuItem item = (ToolStripMenuItem)menu.Items.Add(text, null, click);
      if (selected)
      {
        foreach (ToolStripMenuItem i in menu.Items)
          i.Checked = false;

        item.Checked = true;
      }
    }
  }

  public class MenuClickedArgs
  {
    public EventArgs e { get; set; }
    public int Index { get; set; }
    public string Text { get; set; }

    public MenuClickedArgs(EventArgs e, int index, string text)
    {
      this.e = e;
      this.Index = index;
      this.Text = text;
    }
  }
}
