namespace System.Windows.Forms
{
	/// <summary>Implements a dialog box that is displayed when an unhandled exception occurs in a thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000331 RID: 817
	[global::System.Runtime.InteropServices.ClassInterface(1)]
	[global::System.Runtime.InteropServices.ComVisible(true)]
	public partial class ThreadExceptionDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060038CA RID: 14538 RVA: 0x000E9ECC File Offset: 0x000E80CC
		private void InitializeComponent()
		{
			this.helpText = new global::System.Windows.Forms.Label();
			this.buttonAbort = new global::System.Windows.Forms.Button();
			this.buttonIgnore = new global::System.Windows.Forms.Button();
			this.buttonDetails = new global::System.Windows.Forms.Button();
			this.labelException = new global::System.Windows.Forms.Label();
			this.textBoxDetails = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.helpText.Location = new global::System.Drawing.Point(60, 8);
			this.helpText.Name = "helpText";
			this.helpText.Size = new global::System.Drawing.Size(356, 40);
			this.helpText.TabIndex = 0;
			this.helpText.Text = "An unhandled exception has occurred in you application. If you click Ignore the application will ignore this error and attempt to continue. If you click Abort, the application will quit immediately.";
			this.buttonAbort.DialogResult = global::System.Windows.Forms.DialogResult.Abort;
			this.buttonAbort.Location = new global::System.Drawing.Point(332, 112);
			this.buttonAbort.Name = "buttonAbort";
			this.buttonAbort.Size = new global::System.Drawing.Size(85, 23);
			this.buttonAbort.TabIndex = 4;
			this.buttonAbort.Text = "&Abort";
			this.buttonAbort.Click += new global::System.EventHandler(this.buttonAbort_Click);
			this.buttonIgnore.DialogResult = global::System.Windows.Forms.DialogResult.Ignore;
			this.buttonIgnore.Location = new global::System.Drawing.Point(236, 112);
			this.buttonIgnore.Name = "buttonIgnore";
			this.buttonIgnore.Size = new global::System.Drawing.Size(85, 23);
			this.buttonIgnore.TabIndex = 3;
			this.buttonIgnore.Text = "&Ignore";
			this.buttonDetails.Location = new global::System.Drawing.Point(140, 112);
			this.buttonDetails.Name = "buttonDetails";
			this.buttonDetails.Size = new global::System.Drawing.Size(85, 23);
			this.buttonDetails.TabIndex = 2;
			this.buttonDetails.Text = "Show &Details";
			this.buttonDetails.Click += new global::System.EventHandler(this.buttonDetails_Click);
			this.labelException.Location = new global::System.Drawing.Point(60, 64);
			this.labelException.Name = "labelException";
			this.labelException.Size = new global::System.Drawing.Size(356, 32);
			this.labelException.TabIndex = 1;
			this.textBoxDetails.Location = new global::System.Drawing.Point(8, 168);
			this.textBoxDetails.Multiline = true;
			this.textBoxDetails.Name = "textBoxDetails";
			this.textBoxDetails.ReadOnly = true;
			this.textBoxDetails.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.textBoxDetails.Size = new global::System.Drawing.Size(408, 196);
			this.textBoxDetails.TabIndex = 5;
			this.textBoxDetails.TabStop = false;
			this.textBoxDetails.Text = string.Empty;
			this.textBoxDetails.WordWrap = false;
			this.label1.Location = new global::System.Drawing.Point(8, 148);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(100, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Exception details";
			base.AcceptButton = this.buttonIgnore;
			base.CancelButton = this.buttonAbort;
			base.ClientSize = new global::System.Drawing.Size(428, 374);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBoxDetails);
			base.Controls.Add(this.labelException);
			base.Controls.Add(this.buttonDetails);
			base.Controls.Add(this.buttonIgnore);
			base.Controls.Add(this.buttonAbort);
			base.Controls.Add(this.helpText);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ThreadExceptionDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			base.TopMost = true;
			base.Paint += new global::System.Windows.Forms.PaintEventHandler(this.PaintHandler);
			base.ResumeLayout(false);
		}

		// Token: 0x040019BC RID: 6588
		private global::System.Windows.Forms.Button buttonIgnore;

		// Token: 0x040019BD RID: 6589
		private global::System.Windows.Forms.Button buttonAbort;

		// Token: 0x040019BE RID: 6590
		private global::System.Windows.Forms.Button buttonDetails;

		// Token: 0x040019BF RID: 6591
		private global::System.Windows.Forms.Label labelException;

		// Token: 0x040019C0 RID: 6592
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040019C1 RID: 6593
		private global::System.Windows.Forms.TextBox textBoxDetails;

		// Token: 0x040019C2 RID: 6594
		private global::System.Windows.Forms.Label helpText;
	}
}
