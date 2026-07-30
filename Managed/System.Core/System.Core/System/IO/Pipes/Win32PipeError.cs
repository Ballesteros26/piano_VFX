using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.IO.Pipes
{
	// Token: 0x02000045 RID: 69
	internal static class Win32PipeError
	{
		// Token: 0x0600015D RID: 349 RVA: 0x000041B6 File Offset: 0x000023B6
		public static Exception GetException()
		{
			return Win32PipeError.GetException(Marshal.GetLastWin32Error());
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000041C2 File Offset: 0x000023C2
		public static Exception GetException(int errorCode)
		{
			if (errorCode == 5)
			{
				return new UnauthorizedAccessException();
			}
			return new Win32Exception(errorCode);
		}
	}
}
