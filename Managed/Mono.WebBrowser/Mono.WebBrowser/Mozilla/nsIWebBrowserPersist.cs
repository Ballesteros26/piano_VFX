using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000127 RID: 295
	[Guid("dd4e0a6a-210f-419a-ad85-40e8543b9465")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIWebBrowserPersist : nsICancelable
	{
		// Token: 0x060008BF RID: 2239
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cancel(int aReason);

		// Token: 0x060008C0 RID: 2240
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPersistFlags(out uint ret);

		// Token: 0x060008C1 RID: 2241
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPersistFlags(uint value);

		// Token: 0x060008C2 RID: 2242
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getCurrentState(out uint ret);

		// Token: 0x060008C3 RID: 2243
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getResult(out uint ret);

		// Token: 0x060008C4 RID: 2244
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getProgressListener([MarshalAs(UnmanagedType.Interface)] out nsIWebProgressListener ret);

		// Token: 0x060008C5 RID: 2245
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setProgressListener([MarshalAs(UnmanagedType.Interface)] nsIWebProgressListener value);

		// Token: 0x060008C6 RID: 2246
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int saveURI([MarshalAs(UnmanagedType.Interface)] nsIURI aURI, IntPtr aCacheKey, [MarshalAs(UnmanagedType.Interface)] nsIURI aReferrer, [MarshalAs(UnmanagedType.Interface)] nsIInputStream aPostData, [MarshalAs(UnmanagedType.LPStr)] string aExtraHeaders, IntPtr aFile);

		// Token: 0x060008C7 RID: 2247
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int saveChannel([MarshalAs(UnmanagedType.Interface)] nsIChannel aChannel, IntPtr aFile);

		// Token: 0x060008C8 RID: 2248
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int saveDocument([MarshalAs(UnmanagedType.Interface)] nsIDOMDocument aDocument, IntPtr aFile, IntPtr aDataPath, [MarshalAs(UnmanagedType.LPStr)] string aOutputContentType, uint aEncodingFlags, uint aWrapColumn);

		// Token: 0x060008C9 RID: 2249
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int cancelSave();
	}
}
