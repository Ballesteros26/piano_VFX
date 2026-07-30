using System;

namespace System.Web
{
	/// <summary>Maps document extensions to content MIME types.</summary>
	// Token: 0x020000C5 RID: 197
	public static class MimeMapping
	{
		/// <summary>Returns the MIME mapping for the specified file name.</summary>
		/// <param name="fileName">The file name that is used to determine the MIME type.</param>
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001A2DC File Offset: 0x000184DC
		public static string GetMimeMapping(string fileName)
		{
			return MimeTypes.GetMimeType(fileName);
		}
	}
}
