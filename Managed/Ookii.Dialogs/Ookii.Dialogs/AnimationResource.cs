using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x02000003 RID: 3
	public sealed class AnimationResource
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AnimationResource(string resourceFile, int resourceId)
		{
			bool flag = resourceFile == null;
			if (flag)
			{
				throw new ArgumentNullException("resourceFile");
			}
			this.ResourceFile = resourceFile;
			this.ResourceId = resourceId;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002090 File Offset: 0x00000290
		public string ResourceFile { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002099 File Offset: 0x00000299
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020A1 File Offset: 0x000002A1
		public int ResourceId { get; private set; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020AC File Offset: 0x000002AC
		public static AnimationResource GetShellAnimation(ShellAnimation animation)
		{
			bool flag = !Enum.IsDefined(typeof(ShellAnimation), animation);
			if (flag)
			{
				throw new ArgumentOutOfRangeException("animation");
			}
			return new AnimationResource("shell32.dll", (int)animation);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F0 File Offset: 0x000002F0
		internal SafeModuleHandle LoadLibrary()
		{
			SafeModuleHandle safeModuleHandle = NativeMethods.LoadLibraryEx(this.ResourceFile, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
			bool isInvalid = safeModuleHandle.IsInvalid;
			if (!isInvalid)
			{
				return safeModuleHandle;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			bool flag = lastWin32Error == 2;
			if (flag)
			{
				throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, Resources.FileNotFoundFormat, new object[] { this.ResourceFile }));
			}
			throw new Win32Exception(lastWin32Error);
		}
	}
}
