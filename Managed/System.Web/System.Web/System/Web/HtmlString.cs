using System;

namespace System.Web
{
	/// <summary>Represents an HTML-encoded string that should not be encoded again.</summary>
	// Token: 0x02000042 RID: 66
	public class HtmlString : IHtmlString
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HtmlString" /> class.</summary>
		/// <param name="value">An HTML-encoded string that should not be encoded again.</param>
		// Token: 0x060003B6 RID: 950 RVA: 0x0000722F File Offset: 0x0000542F
		public HtmlString(string value)
		{
			this._htmlString = value;
		}

		/// <summary>Returns an HTML-encoded string.</summary>
		/// <returns>An HTML-encoded string.</returns>
		// Token: 0x060003B7 RID: 951 RVA: 0x0000723E File Offset: 0x0000543E
		public string ToHtmlString()
		{
			return this._htmlString;
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060003B8 RID: 952 RVA: 0x0000723E File Offset: 0x0000543E
		public override string ToString()
		{
			return this._htmlString;
		}

		// Token: 0x04000DA0 RID: 3488
		private string _htmlString;
	}
}
