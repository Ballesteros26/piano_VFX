using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x0200011A RID: 282
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal static class GCUtil
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x000263F6 File Offset: 0x000245F6
		public static IntPtr RootObject(object obj)
		{
			if (obj == null)
			{
				return IntPtr.Zero;
			}
			return (IntPtr)GCHandle.Alloc(obj);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0002640C File Offset: 0x0002460C
		public static object UnrootObject(IntPtr pointer)
		{
			if (pointer != IntPtr.Zero)
			{
				GCHandle gchandle = (GCHandle)pointer;
				if (gchandle.IsAllocated)
				{
					object target = gchandle.Target;
					gchandle.Free();
					return target;
				}
			}
			return null;
		}
	}
}
