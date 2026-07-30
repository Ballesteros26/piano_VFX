using System;

namespace System
{
	/// <summary>A customizable parser based on the File scheme.</summary>
	// Token: 0x02000108 RID: 264
	public class FileStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File scheme.</summary>
		// Token: 0x06000750 RID: 1872 RVA: 0x000245F9 File Offset: 0x000227F9
		public FileStyleUriParser()
			: base(UriParser.FileUri.Flags)
		{
		}
	}
}
