using System;

namespace System
{
	/// <summary>A customizable parser based on the HTTP scheme.</summary>
	// Token: 0x02000106 RID: 262
	public class HttpStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the HTTP scheme.</summary>
		// Token: 0x0600074E RID: 1870 RVA: 0x000245D5 File Offset: 0x000227D5
		public HttpStyleUriParser()
			: base(UriParser.HttpUri.Flags)
		{
		}
	}
}
