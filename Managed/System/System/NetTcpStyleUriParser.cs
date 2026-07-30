using System;

namespace System
{
	/// <summary>A parser based on the NetTcp scheme for the "Indigo" system.</summary>
	// Token: 0x0200010D RID: 269
	public class NetTcpStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetTcp scheme for the "Indigo" system.</summary>
		// Token: 0x06000755 RID: 1877 RVA: 0x00024653 File Offset: 0x00022853
		public NetTcpStyleUriParser()
			: base(UriParser.NetTcpUri.Flags)
		{
		}
	}
}
