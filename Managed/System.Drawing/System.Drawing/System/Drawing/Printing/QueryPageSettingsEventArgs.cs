using System;

namespace System.Drawing.Printing
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.QueryPageSettings" /> event.</summary>
	// Token: 0x020000C6 RID: 198
	public class QueryPageSettingsEventArgs : PrintEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.QueryPageSettingsEventArgs" /> class.</summary>
		/// <param name="pageSettings">The page settings for the page to be printed. </param>
		// Token: 0x06000AA3 RID: 2723 RVA: 0x000171B9 File Offset: 0x000153B9
		public QueryPageSettingsEventArgs(PageSettings pageSettings)
		{
			this._pageSettings = pageSettings;
		}

		/// <summary>Gets or sets the page settings for the page to be printed.</summary>
		/// <returns>The page settings for the page to be printed.</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000171C8 File Offset: 0x000153C8
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x000171D7 File Offset: 0x000153D7
		public PageSettings PageSettings
		{
			get
			{
				this.PageSettingsChanged = true;
				return this._pageSettings;
			}
			set
			{
				if (value == null)
				{
					value = new PageSettings();
				}
				this._pageSettings = value;
				this.PageSettingsChanged = true;
			}
		}

		// Token: 0x040006FC RID: 1788
		private PageSettings _pageSettings;

		// Token: 0x040006FD RID: 1789
		internal bool PageSettingsChanged;
	}
}
