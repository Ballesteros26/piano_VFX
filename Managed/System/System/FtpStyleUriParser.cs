using System;

namespace System
{
	/// <summary>A customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
	// Token: 0x02000107 RID: 263
	public class FtpStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
		// Token: 0x0600074F RID: 1871 RVA: 0x000245E7 File Offset: 0x000227E7
		public FtpStyleUriParser()
			: base(UriParser.FtpUri.Flags)
		{
		}
	}
}
