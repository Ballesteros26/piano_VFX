using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.HtmlWindow.Error" /> event. </summary>
	// Token: 0x020001B9 RID: 441
	public sealed class HtmlElementErrorEventArgs : EventArgs
	{
		// Token: 0x06001D09 RID: 7433 RVA: 0x0006EBB8 File Offset: 0x0006CDB8
		internal HtmlElementErrorEventArgs(string description, int lineNumber, Uri url)
		{
			this.description = description;
			this.line_number = lineNumber;
			this.url = url;
		}

		/// <summary>Gets the descriptive string corresponding to the error.</summary>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0006EBD8 File Offset: 0x0006CDD8
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>Gets or sets whether this error has been handled by the application hosting the document.</summary>
		/// <returns>True if the event has been handled; otherwise, false. The default is false.</returns>
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0006EBE0 File Offset: 0x0006CDE0
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x0006EBE8 File Offset: 0x0006CDE8
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		/// <summary>Gets the line of HTML script code on which the error occurred.</summary>
		/// <returns>An <see cref="T:System.Int32" /> designating the script line number.</returns>
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0006EBF4 File Offset: 0x0006CDF4
		public int LineNumber
		{
			get
			{
				return this.line_number;
			}
		}

		/// <summary>Gets the location of the document that generated the error.</summary>
		/// <returns>A <see cref="T:System.Uri" />.</returns>
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0006EBFC File Offset: 0x0006CDFC
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x04000F6E RID: 3950
		private string description;

		// Token: 0x04000F6F RID: 3951
		private bool handled;

		// Token: 0x04000F70 RID: 3952
		private int line_number;

		// Token: 0x04000F71 RID: 3953
		private Uri url;
	}
}
