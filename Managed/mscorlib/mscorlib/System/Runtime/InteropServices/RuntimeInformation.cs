using System;
using System.IO;
using Mono;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090C RID: 2316
	public static class RuntimeInformation
	{
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x001292C5 File Offset: 0x001274C5
		public static string FrameworkDescription
		{
			get
			{
				return "Mono " + Runtime.GetDisplayName();
			}
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x001292D8 File Offset: 0x001274D8
		public static bool IsOSPlatform(OSPlatform osPlatform)
		{
			PlatformID platform = Environment.OSVersion.Platform;
			if (platform == PlatformID.Win32NT)
			{
				return osPlatform == OSPlatform.Windows;
			}
			if (platform != PlatformID.Unix)
			{
				return false;
			}
			if (File.Exists("/usr/lib/libc.dylib"))
			{
				return osPlatform == OSPlatform.OSX;
			}
			return osPlatform == OSPlatform.Linux;
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060055D1 RID: 21969 RVA: 0x0012932B File Offset: 0x0012752B
		public static string OSDescription
		{
			get
			{
				return Environment.OSVersion.VersionString;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060055D2 RID: 21970 RVA: 0x00129337 File Offset: 0x00127537
		public static Architecture OSArchitecture
		{
			get
			{
				if (!Environment.Is64BitOperatingSystem)
				{
					return Architecture.X86;
				}
				return Architecture.X64;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060055D3 RID: 21971 RVA: 0x00129343 File Offset: 0x00127543
		public static Architecture ProcessArchitecture
		{
			get
			{
				if (!Environment.Is64BitProcess)
				{
					return Architecture.X86;
				}
				return Architecture.X64;
			}
		}
	}
}
