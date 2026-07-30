using System;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x0200039E RID: 926
	internal static class FileSystemEnumerableHelpers
	{
		// Token: 0x06002AEF RID: 10991 RVA: 0x000993F4 File Offset: 0x000975F4
		[SecurityCritical]
		internal static bool IsDir(Win32Native.WIN32_FIND_DATA data)
		{
			return (data.dwFileAttributes & 16) != 0 && !data.cFileName.Equals(".") && !data.cFileName.Equals("..");
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00099428 File Offset: 0x00097628
		[SecurityCritical]
		internal static bool IsFile(Win32Native.WIN32_FIND_DATA data)
		{
			return (data.dwFileAttributes & 16) == 0;
		}
	}
}
