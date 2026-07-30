using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x020000EB RID: 235
	[Guid("c8c0a080-0868-11d3-915f-d9d889d48e3c")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface nsIFile
	{
		// Token: 0x0600077A RID: 1914
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int append(HandleRef node);

		// Token: 0x0600077B RID: 1915
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int appendNative(HandleRef node);

		// Token: 0x0600077C RID: 1916
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int normalize();

		// Token: 0x0600077D RID: 1917
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int create(uint type, uint permissions);

		// Token: 0x0600077E RID: 1918
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLeafName(HandleRef ret);

		// Token: 0x0600077F RID: 1919
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLeafName(HandleRef value);

		// Token: 0x06000780 RID: 1920
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNativeLeafName(HandleRef ret);

		// Token: 0x06000781 RID: 1921
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setNativeLeafName(HandleRef value);

		// Token: 0x06000782 RID: 1922
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int copyTo([MarshalAs(UnmanagedType.Interface)] nsIFile newParentDir, HandleRef newName);

		// Token: 0x06000783 RID: 1923
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int CopyToNative([MarshalAs(UnmanagedType.Interface)] nsIFile newParentDir, HandleRef newName);

		// Token: 0x06000784 RID: 1924
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int copyToFollowingLinks([MarshalAs(UnmanagedType.Interface)] nsIFile newParentDir, HandleRef newName);

		// Token: 0x06000785 RID: 1925
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int copyToFollowingLinksNative([MarshalAs(UnmanagedType.Interface)] nsIFile newParentDir, HandleRef newName);

		// Token: 0x06000786 RID: 1926
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int moveTo([MarshalAs(UnmanagedType.Interface)] nsIFile newParentDir, HandleRef newName);

		// Token: 0x06000787 RID: 1927
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int moveToNative([MarshalAs(UnmanagedType.Interface)] nsIFile newParentDir, HandleRef newName);

		// Token: 0x06000788 RID: 1928
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int remove(bool recursive);

		// Token: 0x06000789 RID: 1929
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPermissions(out uint ret);

		// Token: 0x0600078A RID: 1930
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPermissions(uint value);

		// Token: 0x0600078B RID: 1931
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPermissionsOfLink(out uint ret);

		// Token: 0x0600078C RID: 1932
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setPermissionsOfLink(uint value);

		// Token: 0x0600078D RID: 1933
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastModifiedTime(out long ret);

		// Token: 0x0600078E RID: 1934
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLastModifiedTime(long value);

		// Token: 0x0600078F RID: 1935
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getLastModifiedTimeOfLink(out long ret);

		// Token: 0x06000790 RID: 1936
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setLastModifiedTimeOfLink(long value);

		// Token: 0x06000791 RID: 1937
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFileSize(out long ret);

		// Token: 0x06000792 RID: 1938
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int setFileSize(long value);

		// Token: 0x06000793 RID: 1939
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getFileSizeOfLink(out long ret);

		// Token: 0x06000794 RID: 1940
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getTarget(HandleRef ret);

		// Token: 0x06000795 RID: 1941
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNativeTarget(HandleRef ret);

		// Token: 0x06000796 RID: 1942
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getPath(HandleRef ret);

		// Token: 0x06000797 RID: 1943
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getNativePath(HandleRef ret);

		// Token: 0x06000798 RID: 1944
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int exists(out bool ret);

		// Token: 0x06000799 RID: 1945
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isWritable(out bool ret);

		// Token: 0x0600079A RID: 1946
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isReadable(out bool ret);

		// Token: 0x0600079B RID: 1947
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isExecutable(out bool ret);

		// Token: 0x0600079C RID: 1948
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isHidden(out bool ret);

		// Token: 0x0600079D RID: 1949
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isDirectory(out bool ret);

		// Token: 0x0600079E RID: 1950
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isFile(out bool ret);

		// Token: 0x0600079F RID: 1951
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isSymlink(out bool ret);

		// Token: 0x060007A0 RID: 1952
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int isSpecial(out bool ret);

		// Token: 0x060007A1 RID: 1953
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int createUnique(uint type, uint permissions);

		// Token: 0x060007A2 RID: 1954
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int clone([MarshalAs(UnmanagedType.Interface)] out nsIFile ret);

		// Token: 0x060007A3 RID: 1955
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int equals([MarshalAs(UnmanagedType.Interface)] nsIFile inFile, out bool ret);

		// Token: 0x060007A4 RID: 1956
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int contains([MarshalAs(UnmanagedType.Interface)] nsIFile inFile, bool recur, out bool ret);

		// Token: 0x060007A5 RID: 1957
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getParent([MarshalAs(UnmanagedType.Interface)] out nsIFile ret);

		// Token: 0x060007A6 RID: 1958
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		int getDirectoryEntries([MarshalAs(UnmanagedType.Interface)] out nsISimpleEnumerator ret);
	}
}
