using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000DF RID: 223
	[Guid("a6cf906b-15b3-11d2-932e-00805f8add32")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIDOMWindow
	{
		// Token: 0x06000745 RID: 1861
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

		// Token: 0x06000746 RID: 1862
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParent([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

		// Token: 0x06000747 RID: 1863
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTop([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

		// Token: 0x06000748 RID: 1864
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScrollbars([MarshalAs(UnmanagedType.Interface)] out nsIDOMBarProp ret);

		// Token: 0x06000749 RID: 1865
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFrames([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindowCollection ret);

		// Token: 0x0600074A RID: 1866
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getName(HandleRef ret);

		// Token: 0x0600074B RID: 1867
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setName(HandleRef value);

		// Token: 0x0600074C RID: 1868
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTextZoom(out float ret);

		// Token: 0x0600074D RID: 1869
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setTextZoom(float value);

		// Token: 0x0600074E RID: 1870
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScrollX(out int ret);

		// Token: 0x0600074F RID: 1871
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScrollY(out int ret);

		// Token: 0x06000750 RID: 1872
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int scrollTo(int xScroll, int yScroll);

		// Token: 0x06000751 RID: 1873
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int scrollBy(int xScrollDif, int yScrollDif);

		// Token: 0x06000752 RID: 1874
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getSelection([MarshalAs(UnmanagedType.Interface)] out nsISelection ret);

		// Token: 0x06000753 RID: 1875
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int scrollByLines(int numLines);

		// Token: 0x06000754 RID: 1876
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int scrollByPages(int numPages);

		// Token: 0x06000755 RID: 1877
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int sizeToContent();
	}
}
