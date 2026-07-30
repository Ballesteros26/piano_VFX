using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data
{
	// Token: 0x02000119 RID: 281
	internal static class LocalDBAPI
	{
		// Token: 0x06000E55 RID: 3669 RVA: 0x0004B979 File Offset: 0x00049B79
		internal static string GetLocalDBMessage(int hrCode)
		{
			throw new PlatformNotSupportedException("LocalDB is not supported on this platform.");
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0004B988 File Offset: 0x00049B88
		internal static string GetLocalDbInstanceNameFromServerName(string serverName)
		{
			if (serverName == null)
			{
				return null;
			}
			serverName = serverName.TrimStart(Array.Empty<char>());
			if (!serverName.StartsWith("(localdb)\\", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			string text = serverName.Substring("(localdb)\\".Length).Trim();
			if (text.Length == 0)
			{
				return null;
			}
			return text;
		}

		// Token: 0x040009F0 RID: 2544
		private const string const_localDbPrefix = "(localdb)\\";

		// Token: 0x0200011A RID: 282
		// (Invoke) Token: 0x06000E58 RID: 3672
		[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private delegate int LocalDBFormatMessageDelegate(int hrLocalDB, uint dwFlags, uint dwLanguageId, StringBuilder buffer, ref uint buflen);
	}
}
