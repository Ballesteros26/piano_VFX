using System;

namespace System
{
	/// <summary>A customizable parser based on the Gopher scheme.</summary>
	// Token: 0x0200010A RID: 266
	public class GopherStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Gopher scheme.</summary>
		// Token: 0x06000752 RID: 1874 RVA: 0x0002461D File Offset: 0x0002281D
		public GopherStyleUriParser()
			: base(UriParser.GopherUri.Flags)
		{
		}
	}
}
