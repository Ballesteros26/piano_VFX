using System;
using System.Collections;

namespace System.Drawing.Printing
{
	/// <summary>Specifies a print controller that displays a document on a screen as a series of images.</summary>
	// Token: 0x020000CA RID: 202
	public class PreviewPrintController : PrintController
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PreviewPrintController" /> class.</summary>
		// Token: 0x06000ACA RID: 2762 RVA: 0x000178CD File Offset: 0x00015ACD
		public PreviewPrintController()
		{
			this.pageInfoList = new ArrayList();
		}

		/// <summary>Gets a value indicating whether this controller is used for print preview. </summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool IsPreview
		{
			get
			{
				return true;
			}
		}

		/// <summary>Completes the control sequence that determines when and how to preview a page in a print document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed. </param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains data about how to preview a page in the print document. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ACC RID: 2764 RVA: 0x00002CE2 File Offset: 0x00000EE2
		[MonoTODO]
		public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
		}

		/// <summary>Begins the control sequence that determines when and how to preview a print document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed. </param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains data about how to print the document. </param>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ACD RID: 2765 RVA: 0x000178E0 File Offset: 0x00015AE0
		[MonoTODO]
		public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
			if (!document.PrinterSettings.IsValid)
			{
				throw new InvalidPrinterException(document.PrinterSettings);
			}
			foreach (object obj in this.pageInfoList)
			{
				((PreviewPageInfo)obj).Image.Dispose();
			}
			this.pageInfoList.Clear();
		}

		/// <summary>Completes the control sequence that determines when and how to preview a print document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed. </param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains data about how to preview the print document. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ACE RID: 2766 RVA: 0x00002CE2 File Offset: 0x00000EE2
		[MonoTODO]
		public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
		}

		/// <summary>Begins the control sequence that determines when and how to preview a page in a print document.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed. </param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains data about how to preview a page in the print document. Initially, the <see cref="P:System.Drawing.Printing.PrintPageEventArgs.Graphics" /> property of this parameter will be null. The value returned from this method will be used to set this property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ACF RID: 2767 RVA: 0x00017960 File Offset: 0x00015B60
		[MonoTODO]
		public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			Image image = new Bitmap(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height);
			PreviewPageInfo previewPageInfo = new PreviewPageInfo(image, new Size(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height));
			this.pageInfoList.Add(previewPageInfo);
			Graphics graphics = Graphics.FromImage(previewPageInfo.Image);
			graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(new Point(0, 0), new Size(image.Width, image.Height)));
			return graphics;
		}

		/// <summary>Gets or sets a value indicating whether to use anti-aliasing when displaying the print preview.</summary>
		/// <returns>true if the print preview uses anti-aliasing; otherwise, false. The default is false.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00017A04 File Offset: 0x00015C04
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x00017A0C File Offset: 0x00015C0C
		public virtual bool UseAntiAlias
		{
			get
			{
				return this.useantialias;
			}
			set
			{
				this.useantialias = value;
			}
		}

		/// <summary>Captures the pages of a document as a series of images.</summary>
		/// <returns>An array of type <see cref="T:System.Drawing.Printing.PreviewPageInfo" /> that contains the pages of a <see cref="T:System.Drawing.Printing.PrintDocument" /> as a series of images.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AD2 RID: 2770 RVA: 0x00017A18 File Offset: 0x00015C18
		public PreviewPageInfo[] GetPreviewPageInfo()
		{
			PreviewPageInfo[] array = new PreviewPageInfo[this.pageInfoList.Count];
			this.pageInfoList.CopyTo(array);
			return array;
		}

		// Token: 0x04000708 RID: 1800
		private bool useantialias;

		// Token: 0x04000709 RID: 1801
		private ArrayList pageInfoList;
	}
}
