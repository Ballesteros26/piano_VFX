using System;
using System.ComponentModel;

namespace System.Drawing.Printing
{
	/// <summary>Defines a reusable object that sends output to a printer, when printing from a Windows Forms application.</summary>
	// Token: 0x020000CC RID: 204
	[DefaultEvent("PrintPage")]
	[DefaultProperty("DocumentName")]
	[ToolboxItemFilter("System.Drawing.Printing", ToolboxItemFilterType.Allow)]
	public class PrintDocument : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintDocument" /> class.</summary>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00017A48 File Offset: 0x00015C48
		public PrintDocument()
		{
			this.documentname = "document";
			this.printersettings = new PrinterSettings();
			this.defaultpagesettings = (PageSettings)this.printersettings.DefaultPageSettings.Clone();
			this.printcontroller = new StandardPrintController();
		}

		/// <summary>Gets or sets page settings that are used as defaults for all pages to be printed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PageSettings" /> that specifies the default page settings for the document.</returns>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00017A97 File Offset: 0x00015C97
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00017A9F File Offset: 0x00015C9F
		[SRDescription("The settings for the current page.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PageSettings DefaultPageSettings
		{
			get
			{
				return this.defaultpagesettings;
			}
			set
			{
				this.defaultpagesettings = value;
			}
		}

		/// <summary>Gets or sets the document name to display (for example, in a print status dialog box or printer queue) while printing the document.</summary>
		/// <returns>The document name to display while printing the document. The default is "document".</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00017AA8 File Offset: 0x00015CA8
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00017AB0 File Offset: 0x00015CB0
		[SRDescription("The name of the document.")]
		[DefaultValue("document")]
		public string DocumentName
		{
			get
			{
				return this.documentname;
			}
			set
			{
				this.documentname = value;
			}
		}

		/// <summary>Gets or sets the print controller that guides the printing process.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintController" /> that guides the printing process. The default is a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00017AB9 File Offset: 0x00015CB9
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00017AC1 File Offset: 0x00015CC1
		[SRDescription("The print controller object.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrintController PrintController
		{
			get
			{
				return this.printcontroller;
			}
			set
			{
				this.printcontroller = value;
			}
		}

		/// <summary>Gets or sets the printer that prints the document.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that specifies where and how the document is printed. The default is a <see cref="T:System.Drawing.Printing.PrinterSettings" /> with its properties set to their default values.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00017ACA File Offset: 0x00015CCA
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00017AD2 File Offset: 0x00015CD2
		[Browsable(false)]
		[SRDescription("The current settings for the active printer.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printersettings;
			}
			set
			{
				this.printersettings = ((value == null) ? new PrinterSettings() : value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the position of a graphics object associated with a page is located just inside the user-specified margins or at the top-left corner of the printable area of the page.</summary>
		/// <returns>true if the graphics origin starts at the page margins; false if the graphics origin is at the top-left corner of the printable page. The default is false.</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00017AE5 File Offset: 0x00015CE5
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00017AED File Offset: 0x00015CED
		[DefaultValue(false)]
		[SRDescription("Determines if the origin is set at the specified margins.")]
		public bool OriginAtMargins
		{
			get
			{
				return this.originAtMargins;
			}
			set
			{
				this.originAtMargins = value;
			}
		}

		/// <summary>Starts the document's printing process.</summary>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public void Print()
		{
			PrintEventArgs printEventArgs = new PrintEventArgs();
			this.OnBeginPrint(printEventArgs);
			if (printEventArgs.Cancel)
			{
				return;
			}
			this.PrintController.OnStartPrint(this, printEventArgs);
			if (printEventArgs.Cancel)
			{
				return;
			}
			Graphics graphics = null;
			if (printEventArgs.GraphicsContext != null)
			{
				graphics = Graphics.FromHdc(printEventArgs.GraphicsContext.Hdc);
				printEventArgs.GraphicsContext.Graphics = graphics;
			}
			PrintPageEventArgs printPageEventArgs;
			do
			{
				QueryPageSettingsEventArgs queryPageSettingsEventArgs = new QueryPageSettingsEventArgs(this.DefaultPageSettings.Clone() as PageSettings);
				this.OnQueryPageSettings(queryPageSettingsEventArgs);
				PageSettings pageSettings = queryPageSettingsEventArgs.PageSettings;
				printPageEventArgs = new PrintPageEventArgs(graphics, pageSettings.Bounds, new Rectangle(0, 0, pageSettings.PaperSize.Width, pageSettings.PaperSize.Height), pageSettings);
				printPageEventArgs.GraphicsContext = printEventArgs.GraphicsContext;
				Graphics graphics2 = this.PrintController.OnStartPage(this, printPageEventArgs);
				printPageEventArgs.SetGraphics(graphics2);
				if (!printPageEventArgs.Cancel)
				{
					this.OnPrintPage(printPageEventArgs);
				}
				this.PrintController.OnEndPage(this, printPageEventArgs);
			}
			while (!printPageEventArgs.Cancel && printPageEventArgs.HasMorePages);
			this.OnEndPrint(printEventArgs);
			this.PrintController.OnEndPrint(this, printEventArgs);
		}

		/// <summary>Provides information about the print document, in string form.</summary>
		/// <returns>A string.</returns>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x00017C12 File Offset: 0x00015E12
		public override string ToString()
		{
			return "[PrintDocument " + this.DocumentName + "]";
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> event. It is called after the <see cref="M:System.Drawing.Printing.PrintDocument.Print" /> method is called and before the first page of the document prints.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data. </param>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x00017C29 File Offset: 0x00015E29
		protected virtual void OnBeginPrint(PrintEventArgs e)
		{
			if (this.BeginPrint != null)
			{
				this.BeginPrint(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> event. It is called when the last page of the document has printed.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data. </param>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x00017C40 File Offset: 0x00015E40
		protected virtual void OnEndPrint(PrintEventArgs e)
		{
			if (this.EndPrint != null)
			{
				this.EndPrint(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event. It is called before a page prints.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data. </param>
		// Token: 0x06000AE8 RID: 2792 RVA: 0x00017C57 File Offset: 0x00015E57
		protected virtual void OnPrintPage(PrintPageEventArgs e)
		{
			if (this.PrintPage != null)
			{
				this.PrintPage(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.QueryPageSettings" /> event. It is called immediately before each <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.QueryPageSettingsEventArgs" /> that contains the event data. </param>
		// Token: 0x06000AE9 RID: 2793 RVA: 0x00017C6E File Offset: 0x00015E6E
		protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e)
		{
			if (this.QueryPageSettings != null)
			{
				this.QueryPageSettings(this, e);
			}
		}

		/// <summary>Occurs when the <see cref="M:System.Drawing.Printing.PrintDocument.Print" /> method is called and before the first page of the document prints.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000AEA RID: 2794 RVA: 0x00017C88 File Offset: 0x00015E88
		// (remove) Token: 0x06000AEB RID: 2795 RVA: 0x00017CC0 File Offset: 0x00015EC0
		[SRDescription("Raised when printing begins")]
		public event PrintEventHandler BeginPrint;

		/// <summary>Occurs when the last page of the document has printed.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000AEC RID: 2796 RVA: 0x00017CF8 File Offset: 0x00015EF8
		// (remove) Token: 0x06000AED RID: 2797 RVA: 0x00017D30 File Offset: 0x00015F30
		[SRDescription("Raised when printing ends")]
		public event PrintEventHandler EndPrint;

		/// <summary>Occurs when the output to print for the current page is needed.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000AEE RID: 2798 RVA: 0x00017D68 File Offset: 0x00015F68
		// (remove) Token: 0x06000AEF RID: 2799 RVA: 0x00017DA0 File Offset: 0x00015FA0
		[SRDescription("Raised when printing of a new page begins")]
		public event PrintPageEventHandler PrintPage;

		/// <summary>Occurs immediately before each <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000AF0 RID: 2800 RVA: 0x00017DD8 File Offset: 0x00015FD8
		// (remove) Token: 0x06000AF1 RID: 2801 RVA: 0x00017E10 File Offset: 0x00016010
		[SRDescription("Raised before printing of a new page begins")]
		public event QueryPageSettingsEventHandler QueryPageSettings;

		// Token: 0x0400070A RID: 1802
		private PageSettings defaultpagesettings;

		// Token: 0x0400070B RID: 1803
		private PrinterSettings printersettings;

		// Token: 0x0400070C RID: 1804
		private PrintController printcontroller;

		// Token: 0x0400070D RID: 1805
		private string documentname;

		// Token: 0x0400070E RID: 1806
		private bool originAtMargins;
	}
}
