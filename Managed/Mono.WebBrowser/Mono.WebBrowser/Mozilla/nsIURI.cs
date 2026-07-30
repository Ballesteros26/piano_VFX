using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000119 RID: 281
	[Guid("07a22cc0-0ce5-11d3-9331-00104ba0fd40")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIURI
	{
		// Token: 0x06000873 RID: 2163
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getSpec(HandleRef ret);

		// Token: 0x06000874 RID: 2164
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setSpec(HandleRef value);

		// Token: 0x06000875 RID: 2165
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPrePath(HandleRef ret);

		// Token: 0x06000876 RID: 2166
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getScheme(HandleRef ret);

		// Token: 0x06000877 RID: 2167
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setScheme(HandleRef value);

		// Token: 0x06000878 RID: 2168
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getUserPass(HandleRef ret);

		// Token: 0x06000879 RID: 2169
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setUserPass(HandleRef value);

		// Token: 0x0600087A RID: 2170
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getUsername(HandleRef ret);

		// Token: 0x0600087B RID: 2171
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setUsername(HandleRef value);

		// Token: 0x0600087C RID: 2172
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPassword(HandleRef ret);

		// Token: 0x0600087D RID: 2173
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPassword(HandleRef value);

		// Token: 0x0600087E RID: 2174
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getHostPort(HandleRef ret);

		// Token: 0x0600087F RID: 2175
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setHostPort(HandleRef value);

		// Token: 0x06000880 RID: 2176
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getHost(HandleRef ret);

		// Token: 0x06000881 RID: 2177
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setHost(HandleRef value);

		// Token: 0x06000882 RID: 2178
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPort(out int ret);

		// Token: 0x06000883 RID: 2179
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPort(int value);

		// Token: 0x06000884 RID: 2180
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPath(HandleRef ret);

		// Token: 0x06000885 RID: 2181
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPath(HandleRef value);

		// Token: 0x06000886 RID: 2182
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int equals([MarshalAs(UnmanagedType.Interface)] nsIURI other, out bool ret);

		// Token: 0x06000887 RID: 2183
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int schemeIs([MarshalAs(UnmanagedType.LPStr)] string scheme, out bool ret);

		// Token: 0x06000888 RID: 2184
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int clone([MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

		// Token: 0x06000889 RID: 2185
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int resolve(HandleRef relativePath, HandleRef ret);

		// Token: 0x0600088A RID: 2186
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAsciiSpec(HandleRef ret);

		// Token: 0x0600088B RID: 2187
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getAsciiHost(HandleRef ret);

		// Token: 0x0600088C RID: 2188
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getOriginCharset(HandleRef ret);
	}
}
