using System;
using System.Runtime.InteropServices;

namespace XamMac.CoreFoundation
{
	// Token: 0x0200000C RID: 12
	internal static class CFHelpers
	{
		// Token: 0x06000015 RID: 21
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern void CFRelease(IntPtr obj);

		// Token: 0x06000016 RID: 22
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern IntPtr CFRetain(IntPtr obj);

		// Token: 0x06000017 RID: 23
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CharSet = CharSet.Unicode)]
		private static extern IntPtr CFStringCreateWithCharacters(IntPtr allocator, string str, IntPtr count);

		// Token: 0x06000018 RID: 24
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CharSet = CharSet.Unicode)]
		private static extern IntPtr CFStringGetLength(IntPtr handle);

		// Token: 0x06000019 RID: 25
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CharSet = CharSet.Unicode)]
		private static extern IntPtr CFStringGetCharactersPtr(IntPtr handle);

		// Token: 0x0600001A RID: 26
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", CharSet = CharSet.Unicode)]
		private static extern IntPtr CFStringGetCharacters(IntPtr handle, CFHelpers.CFRange range, IntPtr buffer);

		// Token: 0x0600001B RID: 27 RVA: 0x00002198 File Offset: 0x00000398
		internal unsafe static string FetchString(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			int num = (int)CFHelpers.CFStringGetLength(handle);
			IntPtr intPtr = CFHelpers.CFStringGetCharactersPtr(handle);
			IntPtr intPtr2 = IntPtr.Zero;
			if (intPtr == IntPtr.Zero)
			{
				CFHelpers.CFRange cfrange = new CFHelpers.CFRange(0, num);
				intPtr2 = Marshal.AllocCoTaskMem(num * 2);
				CFHelpers.CFStringGetCharacters(handle, cfrange, intPtr2);
				intPtr = intPtr2;
			}
			string text = new string((char*)(void*)intPtr, 0, num);
			if (intPtr2 != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(intPtr2);
			}
			return text;
		}

		// Token: 0x0600001C RID: 28
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDataGetLength(IntPtr handle);

		// Token: 0x0600001D RID: 29
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDataGetBytePtr(IntPtr handle);

		// Token: 0x0600001E RID: 30 RVA: 0x00002218 File Offset: 0x00000418
		internal static byte[] FetchDataBuffer(IntPtr handle)
		{
			byte[] array = new byte[(int)CFHelpers.CFDataGetLength(handle)];
			Marshal.Copy(CFHelpers.CFDataGetBytePtr(handle), array, 0, array.Length);
			return array;
		}

		// Token: 0x0600001F RID: 31
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDataCreateWithBytesNoCopy(IntPtr allocator, IntPtr bytes, IntPtr length, IntPtr bytesDeallocator);

		// Token: 0x06000020 RID: 32
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDataCreate(IntPtr allocator, IntPtr bytes, IntPtr length);

		// Token: 0x06000021 RID: 33
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecCertificateCreateWithData(IntPtr allocator, IntPtr cfData);

		// Token: 0x06000022 RID: 34 RVA: 0x00002248 File Offset: 0x00000448
		internal unsafe static IntPtr CreateCertificateFromData(byte[] data)
		{
			void* ptr;
			if (data == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&data[0]);
			}
			IntPtr intPtr = CFHelpers.CFDataCreate(IntPtr.Zero, (IntPtr)ptr, new IntPtr(data.Length));
			if (intPtr == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr2 = CFHelpers.SecCertificateCreateWithData(IntPtr.Zero, intPtr);
			if (intPtr != IntPtr.Zero)
			{
				CFHelpers.CFRelease(intPtr);
			}
			return intPtr2;
		}

		// Token: 0x04000370 RID: 880
		internal const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x04000371 RID: 881
		internal const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x0200000D RID: 13
		private struct CFRange
		{
			// Token: 0x06000023 RID: 35 RVA: 0x000022B7 File Offset: 0x000004B7
			public CFRange(int loc, int len)
			{
				this = new CFHelpers.CFRange((long)loc, (long)len);
			}

			// Token: 0x06000024 RID: 36 RVA: 0x000022C3 File Offset: 0x000004C3
			public CFRange(long l, long len)
			{
				this.loc = (IntPtr)l;
				this.len = (IntPtr)len;
			}

			// Token: 0x04000372 RID: 882
			public IntPtr loc;

			// Token: 0x04000373 RID: 883
			public IntPtr len;
		}
	}
}
