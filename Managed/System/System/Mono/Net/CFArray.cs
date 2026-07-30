using System;
using System.Runtime.InteropServices;
using ObjCRuntimeInternal;

namespace Mono.Net
{
	// Token: 0x0200004E RID: 78
	internal class CFArray : CFObject
	{
		// Token: 0x0600013B RID: 315 RVA: 0x000043D8 File Offset: 0x000025D8
		public CFArray(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x0600013C RID: 316
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayCreate(IntPtr allocator, IntPtr values, IntPtr numValues, IntPtr callbacks);

		// Token: 0x0600013D RID: 317 RVA: 0x000043E4 File Offset: 0x000025E4
		static CFArray()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				CFArray.kCFTypeArrayCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeArrayCallBacks");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004438 File Offset: 0x00002638
		public static CFArray FromNativeObjects(params INativeObject[] values)
		{
			return new CFArray(CFArray.Create(values), true);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004448 File Offset: 0x00002648
		public unsafe static IntPtr Create(params IntPtr[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			IntPtr* ptr;
			if (values == null || values.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &values[0];
			}
			return CFArray.CFArrayCreate(IntPtr.Zero, (IntPtr)((void*)ptr), (IntPtr)values.Length, CFArray.kCFTypeArrayCallbacks);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004498 File Offset: 0x00002698
		internal unsafe static CFArray CreateArray(params IntPtr[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			IntPtr* ptr;
			if (values == null || values.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &values[0];
			}
			return new CFArray(CFArray.CFArrayCreate(IntPtr.Zero, (IntPtr)((void*)ptr), (IntPtr)values.Length, CFArray.kCFTypeArrayCallbacks), false);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004438 File Offset: 0x00002638
		public static CFArray CreateArray(params INativeObject[] values)
		{
			return new CFArray(CFArray.Create(values), true);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000044F0 File Offset: 0x000026F0
		public static IntPtr Create(params INativeObject[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			IntPtr[] array = new IntPtr[values.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = values[i].Handle;
			}
			return CFArray.Create(array);
		}

		// Token: 0x06000143 RID: 323
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayGetCount(IntPtr handle);

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004533 File Offset: 0x00002733
		public int Count
		{
			get
			{
				return (int)CFArray.CFArrayGetCount(base.Handle);
			}
		}

		// Token: 0x06000145 RID: 325
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayGetValueAtIndex(IntPtr handle, IntPtr index);

		// Token: 0x17000026 RID: 38
		public IntPtr this[int index]
		{
			get
			{
				return CFArray.CFArrayGetValueAtIndex(base.Handle, (IntPtr)index);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004558 File Offset: 0x00002758
		public static T[] ArrayFromHandle<T>(IntPtr handle, Func<IntPtr, T> creation) where T : class, INativeObject
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr = CFArray.CFArrayGetCount(handle);
			T[] array = new T[(int)intPtr];
			for (uint num = 0U; num < (uint)(int)intPtr; num += 1U)
			{
				array[(int)num] = creation(CFArray.CFArrayGetValueAtIndex(handle, (IntPtr)((long)((ulong)num))));
			}
			return array;
		}

		// Token: 0x04000745 RID: 1861
		private static readonly IntPtr kCFTypeArrayCallbacks;
	}
}
