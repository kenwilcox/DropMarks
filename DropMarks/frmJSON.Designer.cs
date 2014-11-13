namespace DropMarks
{
  partial class frmJSON
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
      this.lbKeys = new System.Windows.Forms.ListBox();
      this.txtValue = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // lbKeys
      // 
      this.lbKeys.FormattingEnabled = true;
      this.lbKeys.Location = new System.Drawing.Point(13, 13);
      this.lbKeys.Name = "lbKeys";
      this.lbKeys.Size = new System.Drawing.Size(267, 121);
      this.lbKeys.TabIndex = 0;
      this.lbKeys.SelectedIndexChanged += new System.EventHandler(this.lbKeys_SelectedIndexChanged);
      // 
      // txtValue
      // 
      this.txtValue.Location = new System.Drawing.Point(13, 141);
      this.txtValue.Multiline = true;
      this.txtValue.Name = "txtValue";
      this.txtValue.Size = new System.Drawing.Size(267, 113);
      this.txtValue.TabIndex = 1;
      // 
      // frmJSON
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 266);
      this.Controls.Add(this.txtValue);
      this.Controls.Add(this.lbKeys);
      this.Name = "frmJSON";
      this.Text = "frmJSON";
      this.Load += new System.EventHandler(this.frmJSON_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox lbKeys;
    private System.Windows.Forms.TextBox txtValue;
  }
}