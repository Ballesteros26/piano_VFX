using System;
using System.Drawing;
using System.Drawing.Printing;

namespace System.Windows.Forms
{
	/// <summary>Controls how a document is printed from a Windows Forms application.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000290 RID: 656
	public class PrintControllerWithStatusDialog : PrintController
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class, wrapping the supplied <see cref="T:System.Drawing.Printing.PrintController" />.</summary>
		/// <param name="underlyingController">A <see cref="T:System.Drawing.Printing.PrintController" /> to encapsulate. </param>
		// Token: 0x06002A91 RID: 10897 RVA: 0x000A3F48 File Offset: 0x000A2148
		public PrintControllerWithStatusDialog(PrintController underlyingController)
		{
			this.underlyingController = underlyingController;
			this.dialog = new PrintControllerWithStatusDialog.PrintingDialog();
			this.dialog.Text = "Printing";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class, wrapping the supplied <see cref="T:System.Drawing.Printing.PrintController" /> and specifying a title for the dialog box.</summary>
		/// <param name="underlyingController">A <see cref="T:System.Drawing.Printing.PrintController" /> to encapsulate. </param>
		/// <param name="dialogTitle">A <see cref="T:System.String" /> containing a title for the status dialog box. </param>
		// Token: 0x06002A92 RID: 10898 RVA: 0x000A3F80 File Offset: 0x000A2180
		public PrintControllerWithStatusDialog(PrintController underlyingController, string dialogTitle)
			: this(underlyingController)
		{
			this.dialog.Text = dialogTitle;
		}

		/// <summary>Completes the control sequence that determines when and how to print a page of a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002A93 RID: 10899 RVA: 0x000A3F98 File Offset: 0x000A2198
		public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
			if (this.dialog.DialogResult == DialogResult.Cancel)
			{
				e.Cancel = true;
				this.dialog.Hide();
				return;
			}
			this.underlyingController.OnEndPage(document, e);
		}

		/// <summary>Completes the control sequence that determines when and how to print a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A94 RID: 10900 RVA: 0x000A3FD8 File Offset: 0x000A21D8
		public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
			this.dialog.Hide();
			this.underlyingController.OnEndPrint(document, e);
		}

		/// <summary>Begins the control sequence that determines when and how to print a page of a document.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> object that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A95 RID: 10901 RVA: 0x000A3FF4 File Offset: 0x000A21F4
		public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			if (this.dialog.DialogResult == DialogResult.Cancel)
			{
				e.Cancel = true;
				this.dialog.Hide();
				return null;
			}
			this.dialog.LabelText = string.Format("Page {0} of document", ++this.currentPage);
			return this.underlyingController.OnStartPage(document, e);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000A4060 File Offset: 0x000A2260
		private void Set_PrinterSettings_PrintFileName(PrinterSettings settings, string filename)
		{
			settings.PrintFileName = filename;
		}

		/// <summary>Begins the control sequence that determines when and how to print a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002A97 RID: 10903 RVA: 0x000A406C File Offset: 0x000A226C
		public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
			try
			{
				this.currentPage = 0;
				this.dialog.Show();
				if (document.PrinterSettings.PrintToFile)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					if (saveFileDialog.ShowDialog() != DialogResult.OK)
					{
						throw new Exception("The operation was canceled by the user");
					}
					this.Set_PrinterSettings_PrintFileName(document.PrinterSettings, saveFileDialog.FileName);
				}
				this.underlyingController.OnStartPrint(document, e);
			}
			catch
			{
				this.dialog.Hide();
				throw;
			}
		}

		/// <summary>Gets a value indicating this <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> is used for print preview.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> is used for print preview, otherwise, false.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x000A410C File Offset: 0x000A230C
		public override bool IsPreview
		{
			get
			{
				return this.underlyingController.IsPreview;
			}
		}

		// Token: 0x04001517 RID: 5399
		private PrintController underlyingController;

		// Token: 0x04001518 RID: 5400
		private PrintControllerWithStatusDialog.PrintingDialog dialog;

		// Token: 0x04001519 RID: 5401
		private int currentPage;

		// Token: 0x02000291 RID: 657
		private class PrintingDialog : Form
		{
			// Token: 0x06002A99 RID: 10905 RVA: 0x000A411C File Offset: 0x000A231C
			public PrintingDialog()
			{
				this.buttonCancel = new Button();
				this.label = new Label();
				base.SuspendLayout();
				this.buttonCancel.Location = new Point(88, 88);
				this.buttonCancel.Name = "buttonCancel";
				this.buttonCancel.TabIndex = 0;
				this.buttonCancel.Text = "Cancel";
				this.label.Location = new Point(0, 40);
				this.label.Name = "label";
				this.label.Size = new Size(257, 23);
				this.label.TabIndex = 1;
				this.label.Text = "Page 1 of document";
				this.label.TextAlign = 32;
				this.AutoScaleBaseSize = new Size(5, 13);
				base.CancelButton = this.buttonCancel;
				base.ClientSize = new Size(258, 124);
				base.ControlBox = false;
				base.Controls.Add(this.label);
				base.Controls.Add(this.buttonCancel);
				base.FormBorderStyle = FormBorderStyle.FixedDialog;
				base.Name = "PrintingDialog";
				base.ShowInTaskbar = false;
				this.Text = "Printing";
				base.ResumeLayout(false);
			}

			// Token: 0x17000A75 RID: 2677
			// (get) Token: 0x06002A9A RID: 10906 RVA: 0x000A4270 File Offset: 0x000A2470
			// (set) Token: 0x06002A9B RID: 10907 RVA: 0x000A4280 File Offset: 0x000A2480
			public string LabelText
			{
				get
				{
					return this.label.Text;
				}
				set
				{
					this.label.Text = value;
				}
			}

			// Token: 0x0400151A RID: 5402
			private Button buttonCancel;

			// Token: 0x0400151B RID: 5403
			private Label label;
		}
	}
}
