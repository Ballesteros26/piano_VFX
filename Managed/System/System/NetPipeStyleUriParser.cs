using System;

namespace System
{
	/// <summary>A parser based on the NetPipe scheme for the "Indigo" system.</summary>
	// Token: 0x0200010C RID: 268
	public class NetPipeStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetPipe scheme for the "Indigo" system.</summary>
		// Token: 0x06000754 RID: 1876 RVA: 0x00024641 File Offset: 0x00022841
		public NetPipeStyleUriParser()
			: base(UriParser.NetPipeUri.Flags)
		{
		}
	}
}
