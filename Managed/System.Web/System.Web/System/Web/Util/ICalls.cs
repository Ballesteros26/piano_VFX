using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Web.Util
{
	// Token: 0x02000140 RID: 320
	internal class ICalls
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x00002050 File Offset: 0x00000250
		private ICalls()
		{
		}

		// Token: 0x06000E9D RID: 3741
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetMachineConfigPath();

		// Token: 0x06000E9E RID: 3742
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetMachineInstallDirectory();

		// Token: 0x06000E9F RID: 3743
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetUnmanagedResourcesPtr(Assembly assembly, out IntPtr ptr, out int length);
	}
}
