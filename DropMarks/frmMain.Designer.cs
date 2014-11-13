namespace DropMarks
{
  partial class frmMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.pnlDrop = new System.Windows.Forms.Panel();
      this.pbDrop = new System.Windows.Forms.PictureBox();
      this.grid = new System.Windows.Forms.DataGridView();
      this.textBox1 = new DMClasses.SearchTextBox(this.components);
      this.pnlDrop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbDrop)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlDrop
      // 
      this.pnlDrop.BackColor = System.Drawing.Color.White;
      this.pnlDrop.Controls.Add(this.pbDrop);
      this.pnlDrop.Location = new System.Drawing.Point(-200, -200);
      this.pnlDrop.Name = "pnlDrop";
      this.pnlDrop.Size = new System.Drawing.Size(164, 176);
      this.pnlDrop.TabIndex = 0;
      // 
      // pbDrop
      // 
      this.pbDrop.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbDrop.Image = ((System.Drawing.Image)(resources.GetObject("pbDrop.Image")));
      this.pbDrop.Location = new System.Drawing.Point(0, 0);
      this.pbDrop.Name = "pbDrop";
      this.pbDrop.Size = new System.Drawing.Size(164, 176);
      this.pbDrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pbDrop.TabIndex = 0;
      this.pbDrop.TabStop = false;
      // 
      // grid
      // 
      this.grid.AllowUserToAddRows = false;
      this.grid.AllowUserToDeleteRows = false;
      this.grid.AllowUserToOrderColumns = true;
      this.grid.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grid.Location = new System.Drawing.Point(12, 38);
      this.grid.MultiSelect = false;
      this.grid.Name = "grid";
      this.grid.RowHeadersVisible = false;
      this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.grid.Size = new System.Drawing.Size(437, 289);
      this.grid.TabIndex = 1;
      this.grid.DragDrop += new System.Windows.Forms.DragEventHandler(this.grid_DragDrop);
      this.grid.DragEnter += new System.Windows.Forms.DragEventHandler(this.grid_DragEnter);
      this.grid.DragLeave += new System.EventHandler(this.grid_DragLeave);
      this.grid.DoubleClick += new System.EventHandler(this.grid_DoubleClick);
      this.grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grid_MouseDown);
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(281, 11);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(168, 20);
      this.textBox1.TabIndex = 3;
      this.textBox1.MenuItemClicked += new DMClasses.SearchTextBox.MenuClickedEvent(this.textBox1_MenuItemClicked);
      this.textBox1.ClearClicked += new DMClasses.SearchTextBox.ClearClickedEvent(this.textBox1_ClearClicked);
      this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
      // 
      // frmMain
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(461, 339);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.grid);
      this.Controls.Add(this.pnlDrop);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.MinimumSize = new System.Drawing.Size(119, 189);
      this.Name = "frmMain";
      this.Text = "DropMarks";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMain_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmMain_DragEnter);
      this.Resize += new System.EventHandler(this.frmMain_Resize);
      this.pnlDrop.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbDrop)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlDrop;
    private System.Windows.Forms.PictureBox pbDrop;
    private System.Windows.Forms.DataGridView grid;
    private DMClasses.SearchTextBox textBox1;

  }
}