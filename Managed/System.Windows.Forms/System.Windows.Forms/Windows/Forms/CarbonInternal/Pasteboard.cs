using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004B3 RID: 1203
	internal class Pasteboard
	{
		// Token: 0x06004C0A RID: 19466 RVA: 0x0012ED44 File Offset: 0x0012CF44
		static Pasteboard()
		{
			Pasteboard.PasteboardCreate(XplatUICarbon.__CFStringMakeConstantString("com.apple.pasteboard.clipboard"), ref Pasteboard.primary_pbref);
			Pasteboard.PasteboardCreate(IntPtr.Zero, ref Pasteboard.app_pbref);
			Pasteboard.internal_format = XplatUICarbon.__CFStringMakeConstantString("com.novell.mono.mwf.pasteboard");
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x0012ED88 File Offset: 0x0012CF88
		internal static object Retrieve(IntPtr pbref, int key)
		{
			uint num = 0U;
			key = (int)Pasteboard.internal_format;
			Pasteboard.PasteboardGetItemCount(pbref, ref num);
			int num2 = 1;
			while ((long)num2 <= (long)((ulong)num))
			{
				uint num3 = 0U;
				Pasteboard.PasteboardGetItemIdentifier(pbref, (uint)num2, ref num3);
				if (num3 == 64206U)
				{
					IntPtr zero = IntPtr.Zero;
					Pasteboard.PasteboardCopyItemFlavorData(pbref, 64206U, (uint)key, ref zero);
					if (zero != IntPtr.Zero)
					{
						return ((GCHandle)Marshal.ReadIntPtr(Pasteboard.CFDataGetBytePtr(zero))).Target;
					}
				}
				num2++;
			}
			return null;
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0012EE18 File Offset: 0x0012D018
		internal static void Store(IntPtr pbref, object data, int key)
		{
			IntPtr intPtr = (IntPtr)GCHandle.Alloc(data);
			IntPtr intPtr2 = Pasteboard.CFDataCreate(IntPtr.Zero, ref intPtr, Marshal.SizeOf(typeof(IntPtr)));
			key = (int)Pasteboard.internal_format;
			Pasteboard.PasteboardClear(pbref);
			Pasteboard.PasteboardPutItemFlavor(pbref, 64206U, (uint)key, intPtr2, 0U);
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x0012EE70 File Offset: 0x0012D070
		internal static IntPtr Primary
		{
			get
			{
				return Pasteboard.primary_pbref;
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x0012EE78 File Offset: 0x0012D078
		internal static IntPtr Application
		{
			get
			{
				return Pasteboard.app_pbref;
			}
		}

		// Token: 0x06004C0F RID: 19471
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CFDataCreate(IntPtr allocator, ref IntPtr buf, int length);

		// Token: 0x06004C10 RID: 19472
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CFDataGetBytePtr(IntPtr data);

		// Token: 0x06004C11 RID: 19473
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardClear(IntPtr pbref);

		// Token: 0x06004C12 RID: 19474
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardCreate(IntPtr str, ref IntPtr pbref);

		// Token: 0x06004C13 RID: 19475
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardCopyItemFlavorData(IntPtr pbref, uint itemid, uint key, ref IntPtr data);

		// Token: 0x06004C14 RID: 19476
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardGetItemCount(IntPtr pbref, ref uint count);

		// Token: 0x06004C15 RID: 19477
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardGetItemIdentifier(IntPtr pbref, uint itemindex, ref uint itemid);

		// Token: 0x06004C16 RID: 19478
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int PasteboardPutItemFlavor(IntPtr pbref, uint itemid, uint key, IntPtr data, uint flags);

		// Token: 0x04002963 RID: 10595
		private static IntPtr primary_pbref;

		// Token: 0x04002964 RID: 10596
		private static IntPtr app_pbref;

		// Token: 0x04002965 RID: 10597
		private static IntPtr internal_format;
	}
}
