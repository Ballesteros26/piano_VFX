using System;

namespace System.Drawing.Printing
{
	/// <summary>Controls how a document is printed, when printing from a Windows Forms application.</summary>
	// Token: 0x020000CB RID: 203
	public abstract class PrintController
	{
		/// <summary>Gets a value indicating whether the <see cref="T:System.Drawing.Printing.PrintController" /> is used for print preview.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0000915C File Offset: 0x0000735C
		public virtual bool IsPreview
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, completes the control sequence that determines when and how to print a page of a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed. </param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data. </param>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public virtual void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
		}

		/// <summary>When overridden in a derived class, begins the control sequence that determines when and how to print a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed. </param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public virtual void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
		}

		/// <summary>When overridden in a derived class, completes the control sequence that determines when and how to print a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed. </param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data. </param>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public virtual void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
		}

		/// <summary>When overridden in a derived class, begins the control sequence that determines when and how to print a page of a document.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed. </param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data. </param>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x00017A43 File Offset: 0x00015C43
		public virtual Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			return null;
		}
	}
}
