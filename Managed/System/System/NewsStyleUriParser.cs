using System;

namespace System
{
	/// <summary>A customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
	// Token: 0x02000109 RID: 265
	public class NewsStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
		// Token: 0x06000751 RID: 1873 RVA: 0x0002460B File Offset: 0x0002280B
		public NewsStyleUriParser()
			: base(UriParser.NewsUri.Flags)
		{
		}
	}
}
