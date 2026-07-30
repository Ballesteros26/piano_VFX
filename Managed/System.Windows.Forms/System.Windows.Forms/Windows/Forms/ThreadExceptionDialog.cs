using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Implements a dialog box that is displayed when an unhandled exception occurs in a thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000331 RID: 817
	[ClassInterface(1)]
	[ComVisible(true)]
	public partial class ThreadExceptionDialog : Form
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ThreadExceptionDialog" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that represents the exception that occurred. </param>
		// Token: 0x060038C7 RID: 14535 RVA: 0x000E9E3C File Offset: 0x000E803C
		public ThreadExceptionDialog(Exception t)
		{
			this.e = t;
			this.InitializeComponent();
			this.labelException.Text = t.Message;
			if (Form.ActiveForm != null)
			{
				this.Text = Form.ActiveForm.Text;
			}
			else
			{
				this.Text = "Mono";
			}
			this.buttonAbort.Enabled = Application.AllowQuit;
			this.RefreshDetails();
			this.FillExceptionDetails();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ThreadExceptionDialog.AutoSize" /> property changes.</summary>
		// Token: 0x14000342 RID: 834
		// (add) Token: 0x060038C8 RID: 14536 RVA: 0x000E9EB4 File Offset: 0x000E80B4
		// (remove) Token: 0x060038C9 RID: 14537 RVA: 0x000E9EC0 File Offset: 0x000E80C0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000EA2F4 File Offset: 0x000E84F4
		private void buttonDetails_Click(object sender, EventArgs e)
		{
			this.details = !this.details;
			this.RefreshDetails();
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000EA30C File Offset: 0x000E850C
		private void FillExceptionDetails()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.e.ToString());
			stringBuilder.Append(Environment.NewLine + Environment.NewLine);
			stringBuilder.Append("Loaded assemblies:" + Environment.NewLine + Environment.NewLine);
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName name = assembly.GetName();
				stringBuilder.AppendFormat("Name:\t{0}" + Environment.NewLine, name.Name);
				stringBuilder.AppendFormat("Version:\t{0}" + Environment.NewLine, name.Version);
				stringBuilder.AppendFormat("Location:\t{0}" + Environment.NewLine, name.CodeBase);
				stringBuilder.Append(Environment.NewLine);
			}
			this.textBoxDetails.Text = stringBuilder.ToString();
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000EA404 File Offset: 0x000E8604
		private void RefreshDetails()
		{
			if (this.details)
			{
				this.buttonDetails.Text = "Hide &Details";
				base.Height = 410;
				this.label1.Visible = true;
				this.textBoxDetails.Visible = true;
				return;
			}
			this.buttonDetails.Text = "Show &Details";
			this.label1.Visible = false;
			this.textBoxDetails.Visible = false;
			base.Height = 180;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000EA484 File Offset: 0x000E8684
		private void buttonAbort_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000EA48C File Offset: 0x000E868C
		private void PaintHandler(object o, PaintEventArgs args)
		{
			Graphics graphics = args.Graphics;
			graphics.DrawIcon(SystemIcons.Error, 15, 10);
		}

		/// <summary>Gets or sets a value indicating whether the dialog box automatically sizes to its content.</summary>
		/// <returns>true if the dialog box automatically sizes; otherwise, false. </returns>
		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000EA4B0 File Offset: 0x000E86B0
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x000EA4B8 File Offset: 0x000E86B8
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x040019BA RID: 6586
		private Exception e;

		// Token: 0x040019BB RID: 6587
		private bool details;
	}
}
