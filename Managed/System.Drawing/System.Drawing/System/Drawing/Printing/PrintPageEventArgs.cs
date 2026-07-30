using System;

namespace System.Drawing.Printing
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
	// Token: 0x020000CE RID: 206
	public class PrintPageEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the item. </param>
		/// <param name="marginBounds">The area between the margins. </param>
		/// <param name="pageBounds">The total area of the paper. </param>
		/// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> for the page. </param>
		// Token: 0x06000AF7 RID: 2807 RVA: 0x00017E75 File Offset: 0x00016075
		public PrintPageEventArgs(Graphics graphics, Rectangle marginBounds, Rectangle pageBounds, PageSettings pageSettings)
		{
			this.graphics = graphics;
			this.marginBounds = marginBounds;
			this.pageBounds = pageBounds;
			this.pageSettings = pageSettings;
		}

		/// <summary>Gets or sets a value indicating whether the print job should be canceled.</summary>
		/// <returns>true if the print job should be canceled; otherwise, false.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00017E9A File Offset: 0x0001609A
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00017EA2 File Offset: 0x000160A2
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the page.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the page.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00017EAB File Offset: 0x000160AB
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets or sets a value indicating whether an additional page should be printed.</summary>
		/// <returns>true if an additional page should be printed; otherwise, false. The default is false.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00017EB3 File Offset: 0x000160B3
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x00017EBB File Offset: 0x000160BB
		public bool HasMorePages
		{
			get
			{
				return this.hasmorePages;
			}
			set
			{
				this.hasmorePages = value;
			}
		}

		/// <summary>Gets the rectangular area that represents the portion of the page inside the margins.</summary>
		/// <returns>The rectangular area, measured in hundredths of an inch, that represents the portion of the page inside the margins. </returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00017EC4 File Offset: 0x000160C4
		public Rectangle MarginBounds
		{
			get
			{
				return this.marginBounds;
			}
		}

		/// <summary>Gets the rectangular area that represents the total area of the page.</summary>
		/// <returns>The rectangular area that represents the total area of the page.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00017ECC File Offset: 0x000160CC
		public Rectangle PageBounds
		{
			get
			{
				return this.pageBounds;
			}
		}

		/// <summary>Gets the page settings for the current page.</summary>
		/// <returns>The page settings for the current page.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00017ED4 File Offset: 0x000160D4
		public PageSettings PageSettings
		{
			get
			{
				return this.pageSettings;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00017EDC File Offset: 0x000160DC
		internal void SetGraphics(Graphics g)
		{
			this.graphics = g;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00017EE5 File Offset: 0x000160E5
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00017EED File Offset: 0x000160ED
		internal GraphicsPrinter GraphicsContext
		{
			get
			{
				return this.graphics_context;
			}
			set
			{
				this.graphics_context = value;
			}
		}

		// Token: 0x04000715 RID: 1813
		private bool cancel;

		// Token: 0x04000716 RID: 1814
		private Graphics graphics;

		// Token: 0x04000717 RID: 1815
		private bool hasmorePages;

		// Token: 0x04000718 RID: 1816
		private Rectangle marginBounds;

		// Token: 0x04000719 RID: 1817
		private Rectangle pageBounds;

		// Token: 0x0400071A RID: 1818
		private PageSettings pageSettings;

		// Token: 0x0400071B RID: 1819
		private GraphicsPrinter graphics_context;
	}
}
