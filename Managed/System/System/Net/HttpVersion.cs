using System;

namespace System.Net
{
	/// <summary>Defines the HTTP version numbers that are supported by the <see cref="T:System.Net.HttpWebRequest" /> and <see cref="T:System.Net.HttpWebResponse" /> classes.</summary>
	// Token: 0x0200042C RID: 1068
	public class HttpVersion
	{
		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.0.</summary>
		// Token: 0x04001C93 RID: 7315
		public static readonly Version Version10 = new Version(1, 0);

		/// <summary>Defines a <see cref="T:System.Version" /> instance for HTTP 1.1.</summary>
		// Token: 0x04001C94 RID: 7316
		public static readonly Version Version11 = new Version(1, 1);
	}
}
