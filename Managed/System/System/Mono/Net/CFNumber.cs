using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x0200004F RID: 79
	internal class CFNumber : CFObject
	{
		// Token: 0x06000148 RID: 328 RVA: 0x000043D8 File Offset: 0x000025D8
		public CFNumber(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x06000149 RID: 329
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool CFNumberGetValue(IntPtr handle, IntPtr type, [MarshalAs(UnmanagedType.I1)] out bool value);

		// Token: 0x0600014A RID: 330 RVA: 0x000045B4 File Offset: 0x000027B4
		public static bool AsBool(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return false;
			}
			bool flag;
			CFNumber.CFNumberGetValue(handle, (IntPtr)1, out flag);
			return flag;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000045E0 File Offset: 0x000027E0
		public static implicit operator bool(CFNumber number)
		{
			return CFNumber.AsBool(number.Handle);
		}

		// Token: 0x0600014C RID: 332
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool CFNumberGetValue(IntPtr handle, IntPtr type, out int value);

		// Token: 0x0600014D RID: 333 RVA: 0x000045F0 File Offset: 0x000027F0
		public static int AsInt32(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return 0;
			}
			int num;
			CFNumber.CFNumberGetValue(handle, (IntPtr)9, out num);
			return num;
		}

		// Token: 0x0600014E RID: 334
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFNumberCreate(IntPtr allocator, IntPtr theType, IntPtr valuePtr);

		// Token: 0x0600014F RID: 335 RVA: 0x0000461D File Offset: 0x0000281D
		public static CFNumber FromInt32(int number)
		{
			return new CFNumber(CFNumber.CFNumberCreate(IntPtr.Zero, (IntPtr)9, (IntPtr)number), true);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000463C File Offset: 0x0000283C
		public static implicit operator int(CFNumber number)
		{
			return CFNumber.AsInt32(number.Handle);
		}
	}
}
