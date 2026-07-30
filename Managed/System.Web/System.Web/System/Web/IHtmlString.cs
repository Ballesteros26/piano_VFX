using System;

namespace System.Web
{
	/// <summary>Represents an HTML-encoded string that should not be encoded again.</summary>
	// Token: 0x02000045 RID: 69
	public interface IHtmlString
	{
		/// <summary>Returns an HTML-encoded string.</summary>
		/// <returns>An HTML-encoded string.</returns>
		// Token: 0x060003C2 RID: 962
		string ToHtmlString();
	}
}
