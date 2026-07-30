using System;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x0200039D RID: 925
	internal sealed class SearchResult
	{
		// Token: 0x06002AEB RID: 10987 RVA: 0x000993BF File Offset: 0x000975BF
		[SecurityCritical]
		internal SearchResult(string fullPath, string userPath, Win32Native.WIN32_FIND_DATA findData)
		{
			this.fullPath = fullPath;
			this.userPath = userPath;
			this.findData = findData;
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000993DC File Offset: 0x000975DC
		internal string FullPath
		{
			get
			{
				return this.fullPath;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000993E4 File Offset: 0x000975E4
		internal string UserPath
		{
			get
			{
				return this.userPath;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000993EC File Offset: 0x000975EC
		internal Win32Native.WIN32_FIND_DATA FindData
		{
			[SecurityCritical]
			get
			{
				return this.findData;
			}
		}

		// Token: 0x0400169B RID: 5787
		private string fullPath;

		// Token: 0x0400169C RID: 5788
		private string userPath;

		// Token: 0x0400169D RID: 5789
		[SecurityCritical]
		private Win32Native.WIN32_FIND_DATA findData;
	}
}
