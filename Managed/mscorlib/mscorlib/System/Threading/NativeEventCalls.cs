using System;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020004A8 RID: 1192
	internal static class NativeEventCalls
	{
		// Token: 0x060037EA RID: 14314
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CreateEvent_internal(bool manual, bool initial, string name, out int errorCode);

		// Token: 0x060037EB RID: 14315 RVA: 0x000CB0E4 File Offset: 0x000C92E4
		public static bool SetEvent(SafeWaitHandle handle)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeEventCalls.SetEvent_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060037EC RID: 14316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetEvent_internal(IntPtr handle);

		// Token: 0x060037ED RID: 14317 RVA: 0x000CB124 File Offset: 0x000C9324
		public static bool ResetEvent(SafeWaitHandle handle)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeEventCalls.ResetEvent_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x060037EE RID: 14318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ResetEvent_internal(IntPtr handle);

		// Token: 0x060037EF RID: 14319
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CloseEvent_internal(IntPtr handle);

		// Token: 0x060037F0 RID: 14320
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr OpenEvent_internal(string name, EventWaitHandleRights rights, out int errorCode);
	}
}
