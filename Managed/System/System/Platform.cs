using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200011E RID: 286
	internal static class Platform
	{
		// Token: 0x060007AF RID: 1967
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x060007B0 RID: 1968 RVA: 0x000268E0 File Offset: 0x00024AE0
		private static void CheckOS()
		{
			if (Environment.OSVersion.Platform != PlatformID.Unix)
			{
				Platform.checkedOS = true;
				return;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(8192);
			try
			{
				if (Platform.uname(intPtr) == 0)
				{
					string text = Marshal.PtrToStringAnsi(intPtr);
					if (!(text == "Darwin"))
					{
						if (text == "FreeBSD")
						{
							Platform.isFreeBSD = true;
						}
					}
					else
					{
						Platform.isMacOS = true;
					}
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Platform.checkedOS = true;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00026964 File Offset: 0x00024B64
		public static bool IsMacOS
		{
			get
			{
				if (!Platform.checkedOS)
				{
					try
					{
						Platform.CheckOS();
					}
					catch (DllNotFoundException)
					{
						Platform.isMacOS = false;
					}
				}
				return Platform.isMacOS;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x000269A0 File Offset: 0x00024BA0
		public static bool IsFreeBSD
		{
			get
			{
				if (!Platform.checkedOS)
				{
					Platform.CheckOS();
				}
				return Platform.isFreeBSD;
			}
		}

		// Token: 0x04000D69 RID: 3433
		private static bool checkedOS;

		// Token: 0x04000D6A RID: 3434
		private static bool isMacOS;

		// Token: 0x04000D6B RID: 3435
		private static bool isFreeBSD;
	}
}
