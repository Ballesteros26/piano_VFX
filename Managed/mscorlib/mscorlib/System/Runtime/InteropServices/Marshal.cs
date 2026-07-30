using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Mono.Interop;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a collection of methods for allocating unmanaged memory, copying unmanaged memory blocks, and converting managed to unmanaged types, as well as other miscellaneous methods used when interacting with unmanaged code.</summary>
	// Token: 0x0200091F RID: 2335
	public static class Marshal
	{
		// Token: 0x06005606 RID: 22022
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddRefInternal(IntPtr pUnk);

		/// <summary>Increments the reference count on the specified interface.</summary>
		/// <returns>The new value of the reference count on the <paramref name="pUnk" /> parameter.</returns>
		/// <param name="pUnk">The interface reference count to increment.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005607 RID: 22023 RVA: 0x001295C6 File Offset: 0x001277C6
		public static int AddRef(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.AddRefInternal(pUnk);
		}

		/// <summary>Indicates whether runtime callable wrappers (RCWs) from any context are available for cleanup.</summary>
		/// <returns>true if there are any RCWs available for cleanup; otherwise, false.</returns>
		// Token: 0x06005608 RID: 22024 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO]
		public static bool AreComObjectsAvailableForCleanup()
		{
			return false;
		}

		/// <summary>Notifies the runtime to clean up all Runtime Callable Wrappers (RCWs) allocated in the current context.</summary>
		// Token: 0x06005609 RID: 22025 RVA: 0x001295EB File Offset: 0x001277EB
		[MonoTODO]
		public static void CleanupUnusedObjectsInCurrentContext()
		{
			if (Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Allocates a block of memory of specified size from the COM task memory allocator.</summary>
		/// <returns>An integer representing the address of the block of memory allocated. This memory must be released with <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" />.</returns>
		/// <param name="cb">The size of the block of memory to be allocated.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600560A RID: 22026
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocCoTaskMem(int cb);

		// Token: 0x0600560B RID: 22027
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr AllocCoTaskMemSize(UIntPtr sizet);

		/// <summary>Allocates memory from the unmanaged memory of the process by using the pointer to the specified number of bytes.</summary>
		/// <returns>A pointer to the newly allocated memory. This memory must be released using the <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> method.</returns>
		/// <param name="cb">The required number of bytes in memory.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600560C RID: 22028
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocHGlobal(IntPtr cb);

		/// <summary>Allocates memory from the unmanaged memory of the process by using the specified number of bytes.</summary>
		/// <returns>A pointer to the newly allocated memory. This memory must be released using the <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> method.</returns>
		/// <param name="cb">The required number of bytes in memory.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600560D RID: 22029 RVA: 0x001295FA File Offset: 0x001277FA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static IntPtr AllocHGlobal(int cb)
		{
			return Marshal.AllocHGlobal((IntPtr)cb);
		}

		/// <summary>Gets an interface pointer identified by the specified moniker.</summary>
		/// <returns>An object containing a reference to the interface pointer identified by the <paramref name="monikerName" /> parameter. A moniker is a name, and in this case, the moniker is defined by an interface.</returns>
		/// <param name="monikerName">The moniker corresponding to the desired interface pointer.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An unrecognized HRESULT was returned by the unmanaged BindToMoniker method.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600560E RID: 22030 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static object BindToMoniker(string monikerName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Changes the strength of an object's COM Callable Wrapper (CCW) handle.</summary>
		/// <param name="otp">The object whose CCW holds a reference counted handle. The handle is strong if the reference count on the CCW is greater than zero; otherwise, it is weak.</param>
		/// <param name="fIsWeak">true to change the strength of the handle on the <paramref name="otp" /> parameter to weak, regardless of its reference count; false to reset the handle strength on <paramref name="otp" /> to be reference counted.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600560F RID: 22031 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005610 RID: 22032
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void copy_to_unmanaged(Array source, int startIndex, IntPtr destination, int length);

		// Token: 0x06005611 RID: 22033
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void copy_from_unmanaged(IntPtr source, int startIndex, Array destination, int length);

		/// <summary>Copies data from a one-dimensional, managed 8-bit unsigned integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005612 RID: 22034 RVA: 0x00129607 File Offset: 0x00127807
		public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed character array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005613 RID: 22035 RVA: 0x00129607 File Offset: 0x00127807
		public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed 16-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005614 RID: 22036 RVA: 0x00129607 File Offset: 0x00127807
		public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed 32-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005615 RID: 22037 RVA: 0x00129607 File Offset: 0x00127807
		public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed 64-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005616 RID: 22038 RVA: 0x00129607 File Offset: 0x00127807
		public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed single-precision floating-point number array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005617 RID: 22039 RVA: 0x00129607 File Offset: 0x00127807
		public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed double-precision floating-point number array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005618 RID: 22040 RVA: 0x00129607 File Offset: 0x00127807
		public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed <see cref="T:System.IntPtr" /> array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		// Token: 0x06005619 RID: 22041 RVA: 0x00129607 File Offset: 0x00127807
		public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 8-bit unsigned integer array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600561A RID: 22042 RVA: 0x00129612 File Offset: 0x00127812
		public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed character array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600561B RID: 22043 RVA: 0x00129612 File Offset: 0x00127812
		public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 16-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600561C RID: 22044 RVA: 0x00129612 File Offset: 0x00127812
		public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 32-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600561D RID: 22045 RVA: 0x00129612 File Offset: 0x00127812
		public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 64-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600561E RID: 22046 RVA: 0x00129612 File Offset: 0x00127812
		public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed single-precision floating-point number array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600561F RID: 22047 RVA: 0x00129612 File Offset: 0x00127812
		public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed double-precision floating-point number array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005620 RID: 22048 RVA: 0x00129612 File Offset: 0x00127812
		public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed <see cref="T:System.IntPtr" /> array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		// Token: 0x06005621 RID: 22049 RVA: 0x00129612 File Offset: 0x00127812
		public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Aggregates a managed object with the specified COM object.</summary>
		/// <returns>The inner IUnknown pointer of the managed object.</returns>
		/// <param name="pOuter">The outer IUnknown pointer.</param>
		/// <param name="o">An object to aggregate.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is a Windows Runtime object.</exception>
		// Token: 0x06005622 RID: 22050 RVA: 0x0002126B File Offset: 0x0001F46B
		public static IntPtr CreateAggregatedObject(IntPtr pOuter, object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Aggregates a managed object of the specified type with the specified COM object. </summary>
		/// <returns>The inner IUnknown pointer of the managed object. </returns>
		/// <param name="pOuter">The outer IUnknown pointer. </param>
		/// <param name="o">The managed object to aggregate. </param>
		/// <typeparam name="T">The type of the managed object to aggregate. </typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is a Windows Runtime object. </exception>
		// Token: 0x06005623 RID: 22051 RVA: 0x0012961D File Offset: 0x0012781D
		public static IntPtr CreateAggregatedObject<T>(IntPtr pOuter, T o)
		{
			return Marshal.CreateAggregatedObject(pOuter, o);
		}

		/// <summary>Wraps the specified COM object in an object of the specified type.</summary>
		/// <returns>The newly wrapped object that is an instance of the desired type.</returns>
		/// <param name="o">The object to be wrapped. </param>
		/// <param name="t">The type of wrapper to create. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> must derive from __ComObject. -or-<paramref name="t" /> is a Windows Runtime type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> cannot be converted to the destination type because it does not support all required interfaces. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005624 RID: 22052 RVA: 0x0012962C File Offset: 0x0012782C
		public static object CreateWrapperOfType(object o, Type t)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException("o must derive from __ComObject", "o");
			}
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			foreach (Type type in o.GetType().GetInterfaces())
			{
				if (type.IsImport && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					throw new InvalidCastException();
				}
			}
			return ComInteropProxy.GetProxy(_ComObject.IUnknown, t).GetTransparentProxy();
		}

		/// <summary>Wraps the specified COM object in an object of the specified type.</summary>
		/// <returns>The newly wrapped object. </returns>
		/// <param name="o">The object to be wrapped. </param>
		/// <typeparam name="T">The type of object to wrap. </typeparam>
		/// <typeparam name="TWrapper">The type of object to return. </typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="T" /> must derive from __ComObject. -or-<paramref name="T" /> is a Windows Runtime type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> cannot be converted to the <paramref name="TWrapper" /> because it does not support all required interfaces. </exception>
		// Token: 0x06005625 RID: 22053 RVA: 0x001296B7 File Offset: 0x001278B7
		public static TWrapper CreateWrapperOfType<T, TWrapper>(T o)
		{
			return (TWrapper)((object)Marshal.CreateWrapperOfType(o, typeof(TWrapper)));
		}

		/// <summary>Frees all substructures that the specified unmanaged memory block points to.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory. </param>
		/// <param name="structuretype">Type of a formatted class. This provides the layout information necessary to delete the buffer in the <paramref name="ptr" /> parameter.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="structureType" /> has an automatic layout. Use sequential or explicit instead.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005626 RID: 22054
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

		/// <summary>Frees all substructures of a specified type that the specified unmanaged memory block points to. </summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory. </param>
		/// <typeparam name="T">The type of the formatted structure. This provides the layout information necessary to delete the buffer in the <paramref name="ptr" /> parameter. </typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="T" /> has an automatic layout. Use sequential or explicit instead. </exception>
		// Token: 0x06005627 RID: 22055 RVA: 0x001296D3 File Offset: 0x001278D3
		public static void DestroyStructure<T>(IntPtr ptr)
		{
			Marshal.DestroyStructure(ptr, typeof(T));
		}

		/// <summary>Frees a BSTR using the COM SysFreeString function.</summary>
		/// <param name="ptr">The address of the BSTR to be freed. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005628 RID: 22056
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeBSTR(IntPtr ptr);

		/// <summary>Frees a block of memory allocated by the unmanaged COM task memory allocator.</summary>
		/// <param name="ptr">The address of the memory to be freed. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005629 RID: 22057
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeCoTaskMem(IntPtr ptr);

		/// <summary>Frees memory previously allocated from the unmanaged memory of the process.</summary>
		/// <param name="hglobal">The handle returned by the original matching call to <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600562A RID: 22058
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeHGlobal(IntPtr hglobal);

		// Token: 0x0600562B RID: 22059 RVA: 0x001296E8 File Offset: 0x001278E8
		private static void ClearBSTR(IntPtr ptr)
		{
			int num = Marshal.ReadInt32(ptr, -4);
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteByte(ptr, i, 0);
			}
		}

		/// <summary>Frees a BSTR Data Type pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToBSTR(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the BSTR to free.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600562C RID: 22060 RVA: 0x00129712 File Offset: 0x00127912
		public static void ZeroFreeBSTR(IntPtr s)
		{
			Marshal.ClearBSTR(s);
			Marshal.FreeBSTR(s);
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x00129720 File Offset: 0x00127920
		private static void ClearAnsi(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadByte(ptr, num) != 0)
			{
				Marshal.WriteByte(ptr, num, 0);
				num++;
			}
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x00129748 File Offset: 0x00127948
		private static void ClearUnicode(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadInt16(ptr, num) != 0)
			{
				Marshal.WriteInt16(ptr, num, 0);
				num += 2;
			}
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemAnsi(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600562F RID: 22063 RVA: 0x0012976E File Offset: 0x0012796E
		public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeCoTaskMem(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemUnicode(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		// Token: 0x06005630 RID: 22064 RVA: 0x0012977C File Offset: 0x0012797C
		public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x0012976E File Offset: 0x0012796E
		public static void ZeroFreeCoTaskMemUTF8(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeCoTaskMem(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocAnsi(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005632 RID: 22066 RVA: 0x0012978A File Offset: 0x0012798A
		public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeHGlobal(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		// Token: 0x06005633 RID: 22067 RVA: 0x00129798 File Offset: 0x00127998
		public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeHGlobal(s);
		}

		/// <summary>Returns the globally unique identifier (GUID) for the specified type, or generates a GUID using the algorithm used by the Type Library Exporter (Tlbexp.exe).</summary>
		/// <returns>An identifier for the specified type.</returns>
		/// <param name="type">The type to generate a GUID for. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005634 RID: 22068 RVA: 0x001297A6 File Offset: 0x001279A6
		public static Guid GenerateGuidForType(Type type)
		{
			return type.GUID;
		}

		/// <summary>Returns a programmatic identifier (ProgID) for the specified type.</summary>
		/// <returns>The ProgID of the specified type.</returns>
		/// <param name="type">The type to get a ProgID for. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a class that can be create by COM. The class must be public, have a public default constructor, and be COM visible. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005635 RID: 22069 RVA: 0x001297B0 File Offset: 0x001279B0
		public static string GenerateProgIdForType(Type type)
		{
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(type))
			{
				if (customAttributeData.Constructor.DeclaringType.Name == "ProgIdAttribute")
				{
					IList<CustomAttributeTypedArgument> constructorArguments = customAttributeData.ConstructorArguments;
					string text = customAttributeData.ConstructorArguments[0].Value as string;
					if (text == null)
					{
						text = string.Empty;
					}
					return text;
				}
			}
			return type.FullName;
		}

		/// <summary>Obtains a running instance of the specified object from the running object table (ROT).</summary>
		/// <returns>The object that was requested; otherwise null. You can cast this object to any COM interface that it supports.</returns>
		/// <param name="progID">The programmatic identifier (ProgID) of the object that was requested.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The object was not found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005636 RID: 22070 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static object GetActiveObject(string progID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005637 RID: 22071
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCCW(object o, Type T);

		// Token: 0x06005638 RID: 22072 RVA: 0x0012984C File Offset: 0x00127A4C
		private static IntPtr GetComInterfaceForObjectInternal(object o, Type T)
		{
			if (Marshal.IsComObject(o))
			{
				return ((__ComObject)o).GetInterface(T);
			}
			return Marshal.GetCCW(o, T);
		}

		/// <summary>Returns a pointer to an IUnknown interface that represents the specified interface on the specified object. Custom query interface access is enabled by default.</summary>
		/// <returns>The interface pointer that represents the specified interface for the object.</returns>
		/// <param name="o">The object that provides the interface. </param>
		/// <param name="T">The type of interface that is requested. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="T" /> parameter is not an interface.-or- The type is not visible to COM. -or-The <paramref name="T" /> parameter is a generic type.</exception>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="o" /> parameter does not support the requested interface. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="o" /> parameter is null.-or- The <paramref name="T" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005639 RID: 22073 RVA: 0x0012986A File Offset: 0x00127A6A
		public static IntPtr GetComInterfaceForObject(object o, Type T)
		{
			IntPtr comInterfaceForObjectInternal = Marshal.GetComInterfaceForObjectInternal(o, T);
			Marshal.AddRef(comInterfaceForObjectInternal);
			return comInterfaceForObjectInternal;
		}

		/// <summary>Returns a pointer to an IUnknown interface that represents the specified interface on the specified object. Custom query interface access is controlled by the specified customization mode.</summary>
		/// <returns>The interface pointer that represents the interface for the object.</returns>
		/// <param name="o">The object that provides the interface.</param>
		/// <param name="T">The type of interface that is requested.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to apply an IUnknown::QueryInterface customization that is supplied by an <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="T" /> parameter is not an interface.-or- The type is not visible to COM.-or-The <paramref name="T" /> parameter is a generic type.</exception>
		/// <exception cref="T:System.InvalidCastException">The object <paramref name="o" /> does not support the requested interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="o" /> parameter is null.-or- The <paramref name="T" /> parameter is null.</exception>
		// Token: 0x0600563A RID: 22074 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a pointer to an IUnknown interface that represents the specified interface on an object of the specified type. Custom query interface access is enabled by default.</summary>
		/// <returns>The interface pointer that represents the <paramref name="TInterface" /> interface.</returns>
		/// <param name="o">The object that provides the interface. </param>
		/// <typeparam name="T">The type of <paramref name="o" />. </typeparam>
		/// <typeparam name="TInterface">The type of interface to return. </typeparam>
		/// <exception cref="T:System.ArgumentException">The <paramref name="TInterface" /> parameter is not an interface.-or- The type is not visible to COM. -or-The <paramref name="T" /> parameter is an open generic type.</exception>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="o" /> parameter does not support the <paramref name="TInterface" /> interface. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="o" /> parameter is null.</exception>
		// Token: 0x0600563B RID: 22075 RVA: 0x0012987A File Offset: 0x00127A7A
		public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
		{
			return Marshal.GetComInterfaceForObject(o, typeof(T));
		}

		/// <summary>Returns an interface pointer that represents the specified interface for an object, if the caller is in the same context as that object.</summary>
		/// <returns>The interface pointer specified by <paramref name="t" /> that represents the interface for the specified object, or null if the caller is not in the same context as the object.</returns>
		/// <param name="o">The object that provides the interface.</param>
		/// <param name="t">The type of interface that is requested.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not an interface.-or- The type is not visible to COM.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is null.-or- <paramref name="t" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600563C RID: 22076 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static IntPtr GetComInterfaceForObjectInContext(object o, Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves data that is referenced by the specified key from the specified COM object.</summary>
		/// <returns>The data represented by the <paramref name="key" /> parameter in the internal hash table of the <paramref name="obj" /> parameter.</returns>
		/// <param name="obj">The COM object that contains the data that you want.</param>
		/// <param name="key">The key in the internal hash table of <paramref name="obj" /> to retrieve the data from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null.-or- <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a COM object.-or-<paramref name="obj" /> is a Windows Runtime object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600563D RID: 22077 RVA: 0x00129891 File Offset: 0x00127A91
		[MonoNotSupported("MSDN states user code should never need to call this method.")]
		public static object GetComObjectData(object obj, object key)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		// Token: 0x0600563E RID: 22078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComSlotForMethodInfoInternal(MemberInfo m);

		/// <summary>Retrieves the virtual function table (v-table or VTBL) slot for a specified <see cref="T:System.Reflection.MemberInfo" /> type when that type is exposed to COM.</summary>
		/// <returns>The VTBL slot <paramref name="m" /> identifier when it is exposed to COM.</returns>
		/// <param name="m">An object that represents an interface method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MemberInfo" /> object.-or-The <paramref name="m" /> parameter is not an interface method.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600563F RID: 22079 RVA: 0x001298A0 File Offset: 0x00127AA0
		public static int GetComSlotForMethodInfo(MemberInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			if (!(m is MethodInfo))
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			if (!m.DeclaringType.IsInterface)
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			return Marshal.GetComSlotForMethodInfoInternal(m);
		}

		/// <summary>Retrieves the last slot in the virtual function table (v-table or VTBL) of a type when exposed to COM.</summary>
		/// <returns>The last VTBL slot of the interface when exposed to COM. If the <paramref name="t" /> parameter is a class, the returned VTBL slot is the last slot in the interface that is generated from the class.</returns>
		/// <param name="t">A type that represents an interface or class.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005640 RID: 22080 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static int GetEndComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a computer-independent description of an exception, and information about the state that existed for the thread when the exception occurred.</summary>
		/// <returns>A pointer to an EXCEPTION_POINTERS structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005641 RID: 22081 RVA: 0x0002126B File Offset: 0x0001F46B
		[ComVisible(true)]
		[MonoTODO]
		public static IntPtr GetExceptionPointers()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the instance handle (HINSTANCE) for the specified module.</summary>
		/// <returns>The HINSTANCE for <paramref name="m" />; or -1 if the module does not have an HINSTANCE.</returns>
		/// <param name="m">The module whose HINSTANCE is desired.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005642 RID: 22082 RVA: 0x001298FC File Offset: 0x00127AFC
		public static IntPtr GetHINSTANCE(Module m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			return m.GetHINSTANCE();
		}

		/// <summary>Retrieves a code that identifies the type of the exception that occurred.</summary>
		/// <returns>The type of the exception.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005643 RID: 22083 RVA: 0x000C8D51 File Offset: 0x000C6F51
		public static int GetExceptionCode()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Converts the specified exception to an HRESULT.</summary>
		/// <returns>The HRESULT mapped to the supplied exception.</returns>
		/// <param name="e">The exception to convert to an HRESULT.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005644 RID: 22084 RVA: 0x00129918 File Offset: 0x00127B18
		public static int GetHRForException(Exception e)
		{
			if (e == null)
			{
				return 0;
			}
			ManagedErrorInfo managedErrorInfo = new ManagedErrorInfo(e);
			Marshal.SetErrorInfo(0, managedErrorInfo);
			return e._HResult;
		}

		/// <summary>Returns the HRESULT corresponding to the last error incurred by Win32 code executed using <see cref="T:System.Runtime.InteropServices.Marshal" />.</summary>
		/// <returns>The HRESULT corresponding to the last Win32 error code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005645 RID: 22085 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int GetHRForLastWin32Error()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005646 RID: 22086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIDispatchForObjectInternal(object o);

		/// <summary>Returns an IDispatch interface from a managed object.</summary>
		/// <returns>The IDispatch pointer for the <paramref name="o" /> parameter.</returns>
		/// <param name="o">The object whose IDispatch interface is requested.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005647 RID: 22087 RVA: 0x0012993F File Offset: 0x00127B3F
		public static IntPtr GetIDispatchForObject(object o)
		{
			IntPtr idispatchForObjectInternal = Marshal.GetIDispatchForObjectInternal(o);
			Marshal.AddRef(idispatchForObjectInternal);
			return idispatchForObjectInternal;
		}

		/// <summary>Returns an IDispatch interface pointer from a managed object, if the caller is in the same context as that object.</summary>
		/// <returns>The IDispatch interface pointer for the specified object, or null if the caller is not in the same context as the specified object.</returns>
		/// <param name="o">The object whose IDispatch interface is requested.</param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005648 RID: 22088 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static IntPtr GetIDispatchForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeInfo" /> interface from a managed type.</summary>
		/// <returns>A pointer to the ITypeInfo interface for the <paramref name="t" /> parameter.</returns>
		/// <param name="t">The type whose ITypeInfo interface is being requested.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not a visible type to COM.-or-<paramref name="t" /> is a Windows Runtime type.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">A type library is registered for the assembly that contains the type, but the type definition cannot be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005649 RID: 22089 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static IntPtr GetITypeInfoForType(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an IUnknown interface from a managed object, if the caller is in the same context as that object.</summary>
		/// <returns>The IUnknown pointer for the specified object, or null if the caller is not in the same context as the specified object.</returns>
		/// <param name="o">The object whose IUnknown interface is requested.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600564A RID: 22090 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static IntPtr GetIUnknownForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a pointer to a runtime-generated function that marshals a call from managed to unmanaged code.</summary>
		/// <returns>A pointer to the function that will marshal a call from the <paramref name="pfnMethodToWrap" /> parameter to unmanaged code.</returns>
		/// <param name="pfnMethodToWrap">A pointer to the method to marshal.</param>
		/// <param name="pbSignature">A pointer to the method signature.</param>
		/// <param name="cbSignature">The number of bytes in <paramref name="pbSignature" />.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600564B RID: 22091 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[Obsolete("This method has been deprecated")]
		public static IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.Reflection.MemberInfo" /> object for the specified virtual function table (v-table or VTBL) slot.</summary>
		/// <returns>The object that represents the member at the specified VTBL slot.</returns>
		/// <param name="t">The type for which the <see cref="T:System.Reflection.MemberInfo" /> is to be retrieved.</param>
		/// <param name="slot">The VTBL slot.</param>
		/// <param name="memberType">On successful return, one of the enumeration values that specifies the type of the member. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not visible from COM. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600564C RID: 22092 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600564D RID: 22093
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIUnknownForObjectInternal(object o);

		/// <summary>Returns an IUnknown interface from a managed object.</summary>
		/// <returns>The IUnknown pointer for the <paramref name="o" /> parameter.</returns>
		/// <param name="o">The object whose IUnknown interface is requested.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600564E RID: 22094 RVA: 0x0012994E File Offset: 0x00127B4E
		public static IntPtr GetIUnknownForObject(object o)
		{
			IntPtr iunknownForObjectInternal = Marshal.GetIUnknownForObjectInternal(o);
			Marshal.AddRef(iunknownForObjectInternal);
			return iunknownForObjectInternal;
		}

		/// <summary>Converts an object to a COM VARIANT.</summary>
		/// <param name="obj">The object for which to get a COM VARIANT.</param>
		/// <param name="pDstNativeVariant">A pointer to receive the VARIANT that corresponds to the <paramref name="obj" /> parameter.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a generic type.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600564F RID: 22095 RVA: 0x00129960 File Offset: 0x00127B60
		public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant)
		{
			Variant variant = default(Variant);
			variant.SetValue(obj);
			Marshal.StructureToPtr<Variant>(variant, pDstNativeVariant, false);
		}

		/// <summary>Converts an object of a specified type to a COM VARIANT. </summary>
		/// <param name="obj">The object for which to get a COM VARIANT. </param>
		/// <param name="pDstNativeVariant">A pointer to receive the VARIANT that corresponds to the <paramref name="obj" /> parameter. </param>
		/// <typeparam name="T">The type of the object to convert. </typeparam>
		// Token: 0x06005650 RID: 22096 RVA: 0x00129985 File Offset: 0x00127B85
		public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
		{
			Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
		}

		// Token: 0x06005651 RID: 22097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetObjectForCCW(IntPtr pUnk);

		/// <summary>Returns an instance of a type that represents a COM object by a pointer to its IUnknown interface.</summary>
		/// <returns>An object that represents the specified unmanaged COM object.</returns>
		/// <param name="pUnk">A pointer to the IUnknown interface. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005652 RID: 22098 RVA: 0x00129994 File Offset: 0x00127B94
		public static object GetObjectForIUnknown(IntPtr pUnk)
		{
			object obj = Marshal.GetObjectForCCW(pUnk);
			if (obj == null)
			{
				obj = ComInteropProxy.GetProxy(pUnk, typeof(__ComObject)).GetTransparentProxy();
			}
			return obj;
		}

		/// <summary>Converts a COM VARIANT to an object.</summary>
		/// <returns>An object that corresponds to the <paramref name="pSrcNativeVariant" /> parameter.</returns>
		/// <param name="pSrcNativeVariant">A pointer to a COM VARIANT.</param>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
		///   <paramref name="pSrcNativeVariant" /> is not a valid VARIANT type.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="pSrcNativeVariant" /> has an unsupported type.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005653 RID: 22099 RVA: 0x001299C4 File Offset: 0x00127BC4
		public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
		{
			return ((Variant)Marshal.PtrToStructure(pSrcNativeVariant, typeof(Variant))).GetValue();
		}

		/// <summary>Converts a COM VARIANT to an object of a specified type. </summary>
		/// <returns>An object of the specified type that corresponds to the <paramref name="pSrcNativeVariant" /> parameter. </returns>
		/// <param name="pSrcNativeVariant">A pointer to a COM VARIANT. </param>
		/// <typeparam name="T">The type to which to convert the COM VARIANT. </typeparam>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
		///   <paramref name="pSrcNativeVariant" /> is not a valid VARIANT type. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="pSrcNativeVariant" /> has an unsupported type. </exception>
		// Token: 0x06005654 RID: 22100 RVA: 0x001299F0 File Offset: 0x00127BF0
		public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
		{
			return (T)((object)((Variant)Marshal.PtrToStructure(pSrcNativeVariant, typeof(Variant))).GetValue());
		}

		/// <summary>Converts an array of COM VARIANTs to an array of objects. </summary>
		/// <returns>An object array that corresponds to <paramref name="aSrcNativeVariant" />.</returns>
		/// <param name="aSrcNativeVariant">A pointer to the first element of an array of COM VARIANTs.</param>
		/// <param name="cVars">The count of COM VARIANTs in <paramref name="aSrcNativeVariant" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cVars" /> is a negative number.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005655 RID: 22101 RVA: 0x00129A20 File Offset: 0x00127C20
		public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
		{
			if (cVars < 0)
			{
				throw new ArgumentOutOfRangeException("cVars", "cVars cannot be a negative number.");
			}
			object[] array = new object[cVars];
			for (int i = 0; i < cVars; i++)
			{
				array[i] = Marshal.GetObjectForNativeVariant((IntPtr)(aSrcNativeVariant.ToInt64() + (long)(i * Marshal.SizeOf(typeof(Variant)))));
			}
			return array;
		}

		/// <summary>Converts an array of COM VARIANTs to an array of a specified type. </summary>
		/// <returns>An array of <paramref name="T" /> objects that corresponds to <paramref name="aSrcNativeVariant" />. </returns>
		/// <param name="aSrcNativeVariant">A pointer to the first element of an array of COM VARIANTs. </param>
		/// <param name="cVars">The count of COM VARIANTs in <paramref name="aSrcNativeVariant" />. </param>
		/// <typeparam name="T">The type of the array to return. </typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cVars" /> is a negative number. </exception>
		// Token: 0x06005656 RID: 22102 RVA: 0x00129A7C File Offset: 0x00127C7C
		public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
		{
			if (cVars < 0)
			{
				throw new ArgumentOutOfRangeException("cVars", "cVars cannot be a negative number.");
			}
			T[] array = new T[cVars];
			for (int i = 0; i < cVars; i++)
			{
				array[i] = Marshal.GetObjectForNativeVariant<T>((IntPtr)(aSrcNativeVariant.ToInt64() + (long)(i * Marshal.SizeOf(typeof(Variant)))));
			}
			return array;
		}

		/// <summary>Gets the first slot in the virtual function table (v-table or VTBL) that contains user-defined methods.</summary>
		/// <returns>The first VTBL slot that contains user-defined methods. The first slot is 3 if the interface is based on IUnknown, and 7 if the interface is based on IDispatch.</returns>
		/// <param name="t">A type that represents an interface.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not visible from COM.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005657 RID: 22103 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static int GetStartComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a fiber cookie into the corresponding <see cref="T:System.Threading.Thread" /> instance.</summary>
		/// <returns>A thread that corresponds to the <paramref name="cookie" /> parameter.</returns>
		/// <param name="cookie">An integer that represents a fiber cookie.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="cookie" /> parameter is 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005658 RID: 22104 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[Obsolete("This method has been deprecated")]
		public static Thread GetThreadFromFiberCookie(int cookie)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a managed object of a specified type that represents a COM object.</summary>
		/// <returns>An instance of the class corresponding to the <see cref="T:System.Type" /> object that represents the requested unmanaged COM object.</returns>
		/// <param name="pUnk">A pointer to the IUnknown interface of the unmanaged object.</param>
		/// <param name="t">The type of the requested managed class.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not attributed with <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />.-or-<paramref name="t" /> is a Windows Runtime type.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005659 RID: 22105 RVA: 0x00129ADC File Offset: 0x00127CDC
		public static object GetTypedObjectForIUnknown(IntPtr pUnk, Type t)
		{
			__ComObject _ComObject = (__ComObject)new ComInteropProxy(pUnk, t).GetTransparentProxy();
			foreach (Type type in t.GetInterfaces())
			{
				if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.Import && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					return null;
				}
			}
			return _ComObject;
		}

		/// <summary>Converts an unmanaged ITypeInfo object into a managed <see cref="T:System.Type" /> object.</summary>
		/// <returns>A managed type that represents the unmanaged ITypeInfo object.</returns>
		/// <param name="piTypeInfo">The ITypeInfo interface to marshal. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600565A RID: 22106 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of the type represented by an ITypeInfo object.</summary>
		/// <returns>The name of the type that the <paramref name="pTI" /> parameter points to.</returns>
		/// <param name="pTI">An object that represents an ITypeInfo pointer. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600565B RID: 22107 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[Obsolete]
		public static string GetTypeInfoName(UCOMITypeInfo pTI)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) of a type library.</summary>
		/// <returns>The LIBID of the type library that the <paramref name="pTLB" /> parameter points to.</returns>
		/// <param name="pTLB">The type library whose LIBID is to be retrieved. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600565C RID: 22108 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[Obsolete]
		public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) of a type library.</summary>
		/// <returns>The LIBID of the specified type library.</returns>
		/// <param name="typelib">The type library whose LIBID is to be retrieved.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600565D RID: 22109 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static Guid GetTypeLibGuid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) that is assigned to a type library when it was exported from the specified assembly.</summary>
		/// <returns>The LIBID that is assigned to a type library when it is exported from the specified assembly.</returns>
		/// <param name="asm">The assembly from which the type library was exported.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asm" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600565E RID: 22110 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static Guid GetTypeLibGuidForAssembly(Assembly asm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the LCID of a type library.</summary>
		/// <returns>The LCID of the type library that the <paramref name="pTLB" /> parameter points to.</returns>
		/// <param name="pTLB">The type library whose LCID is to be retrieved.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600565F RID: 22111 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[Obsolete]
		public static int GetTypeLibLcid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the LCID of a type library.</summary>
		/// <returns>The LCID of the type library that the <paramref name="typelib" /> parameter points to.</returns>
		/// <param name="typelib">The type library whose LCID is to be retrieved.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005660 RID: 22112 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static int GetTypeLibLcid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of a type library.</summary>
		/// <returns>The name of the type library that the <paramref name="pTLB" /> parameter points to.</returns>
		/// <param name="pTLB">The type library whose name is to be retrieved.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005661 RID: 22113 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[Obsolete]
		public static string GetTypeLibName(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of a type library.</summary>
		/// <returns>The name of the type library that the <paramref name="typelib" /> parameter points to.</returns>
		/// <param name="typelib">The type library whose name is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typelib" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005662 RID: 22114 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static string GetTypeLibName(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the version number of a type library that will be exported from the specified assembly.</summary>
		/// <param name="inputAssembly">A managed assembly.</param>
		/// <param name="majorVersion">The major version number.</param>
		/// <param name="minorVersion">The minor version number.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputAssembly" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005663 RID: 22115 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a pointer to a runtime-generated function that marshals a call from unmanaged to managed code.</summary>
		/// <returns>A pointer to a function that will marshal a call from <paramref name="pfnMethodToWrap" /> to managed code.</returns>
		/// <param name="pfnMethodToWrap">A pointer to the method to marshal.</param>
		/// <param name="pbSignature">A pointer to the method signature.</param>
		/// <param name="cbSignature">The number of bytes in <paramref name="pbSignature" />.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005664 RID: 22116 RVA: 0x0002126B File Offset: 0x0001F46B
		[Obsolete("This method has been deprecated")]
		[MonoTODO]
		public static IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a type is visible to COM clients.</summary>
		/// <returns>true if the type is visible to COM; otherwise, false.</returns>
		/// <param name="t">The type to check for COM visibility.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005665 RID: 22117 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static bool IsTypeVisibleFromCom(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Calculates the number of bytes in unmanaged memory that are required to hold the parameters for the specified method.</summary>
		/// <returns>The number of bytes required to represent the method parameters in unmanaged memory.</returns>
		/// <param name="m">The method to be checked.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MethodInfo" /> object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005666 RID: 22118 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static int NumParamBytes(MethodInfo m)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the type associated with the specified class identifier (CLSID). </summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid. </returns>
		/// <param name="clsid">The CLSID of the type to return. </param>
		// Token: 0x06005667 RID: 22119 RVA: 0x000C8D51 File Offset: 0x000C6F51
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Retrieves the name of the type represented by an ITypeInfo object.</summary>
		/// <returns>The name of the type that the <paramref name="typeInfo" /> parameter points to.</returns>
		/// <param name="typeInfo">An object that represents an ITypeInfo pointer.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typeInfo" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005668 RID: 22120 RVA: 0x000C8D51 File Offset: 0x000C6F51
		public static string GetTypeInfoName(ITypeInfo typeInfo)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Creates a unique Runtime Callable Wrapper (RCW) object for a given IUnknown interface.</summary>
		/// <returns>A unique RCW for the specified IUnknown interface.</returns>
		/// <param name="unknown">A managed pointer to an IUnknown interface.</param>
		// Token: 0x06005669 RID: 22121 RVA: 0x000C8D51 File Offset: 0x000C6F51
		public static object GetUniqueObjectForIUnknown(IntPtr unknown)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Indicates whether a specified object represents a COM object.</summary>
		/// <returns>true if the <paramref name="o" /> parameter is a COM type; otherwise, false.</returns>
		/// <param name="o">The object to check.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600566A RID: 22122
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsComObject(object o);

		/// <summary>Returns the error code returned by the last unmanaged function that was called using platform invoke that has the <see cref="F:System.Runtime.InteropServices.DllImportAttribute.SetLastError" /> flag set.</summary>
		/// <returns>The last error code set by a call to the Win32 SetLastError function.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600566B RID: 22123
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastWin32Error();

		/// <summary>Returns the field offset of the unmanaged form of the managed class.</summary>
		/// <returns>The offset, in bytes, for the <paramref name="fieldName" /> parameter within the specified class that is declared by platform invoke.</returns>
		/// <param name="t">A value type or formatted reference type that specifies the managed class. You must apply the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> to the class.</param>
		/// <param name="fieldName">The field within the <paramref name="t" /> parameter.</param>
		/// <exception cref="T:System.ArgumentException">The class cannot be exported as a structure or the field is nonpublic. Beginning with the .NET Framework version 2.0, the field may be private.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600566C RID: 22124
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr OffsetOf(Type t, string fieldName);

		/// <summary>Returns the field offset of the unmanaged form of a specified managed class.</summary>
		/// <returns>The offset, in bytes, for the <paramref name="fieldName" /> parameter within the specified class that is declared by platform invoke. </returns>
		/// <param name="fieldName">The name of the field in the <paramref name="T" /> type. </param>
		/// <typeparam name="T">A managed value type or formatted reference type. You must apply the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> attribute to the class. </typeparam>
		// Token: 0x0600566D RID: 22125 RVA: 0x00129B3D File Offset: 0x00127D3D
		public static IntPtr OffsetOf<T>(string fieldName)
		{
			return Marshal.OffsetOf(typeof(T), fieldName);
		}

		/// <summary>Executes one-time method setup tasks without calling the method.</summary>
		/// <param name="m">The method to be checked.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MethodInfo" /> object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600566E RID: 22126
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Prelink(MethodInfo m);

		/// <summary>Performs a pre-link check for all methods on a class.</summary>
		/// <param name="c">The class whose methods are to be checked.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="c" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600566F RID: 22127
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrelinkAll(Type c);

		/// <summary>Copies all characters up to the first null character from an unmanaged ANSI string to a managed <see cref="T:System.String" />, and widens each ANSI character to Unicode.</summary>
		/// <returns>A managed string that holds a copy of the unmanaged ANSI string. If <paramref name="ptr" /> is null, the method returns a null string.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005670 RID: 22128
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringAnsi(IntPtr ptr);

		/// <summary>Allocates a managed <see cref="T:System.String" />, copies a specified number of characters from an unmanaged ANSI string into it, and widens each ANSI character to Unicode.</summary>
		/// <returns>A managed string that holds a copy of the native ANSI string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <param name="len">The byte count of the input string to copy.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="len" /> is less than zero.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005671 RID: 22129
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringAnsi(IntPtr ptr, int len);

		// Token: 0x06005672 RID: 22130 RVA: 0x00129B4F File Offset: 0x00127D4F
		public static string PtrToStringUTF8(IntPtr ptr)
		{
			return Marshal.PtrToStringAnsi(ptr);
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x00129B57 File Offset: 0x00127D57
		public static string PtrToStringUTF8(IntPtr ptr, int byteLen)
		{
			return Marshal.PtrToStringAnsi(ptr, byteLen);
		}

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies all characters up to the first null character from a string stored in unmanaged memory into it.</summary>
		/// <returns>A managed string that holds a copy of the unmanaged string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">For Unicode platforms, the address of the first Unicode character.-or- For ANSI plaforms, the address of the first ANSI character.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005674 RID: 22132 RVA: 0x00129B60 File Offset: 0x00127D60
		public static string PtrToStringAuto(IntPtr ptr)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.PtrToStringAnsi(ptr);
			}
			return Marshal.PtrToStringUni(ptr);
		}

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies the specified number of characters from a string stored in unmanaged memory into it.</summary>
		/// <returns>A managed string that holds a copy of the native string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">For Unicode platforms, the address of the first Unicode character.-or- For ANSI plaforms, the address of the first ANSI character.</param>
		/// <param name="len">The number of characters to copy.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="len" /> is less than zero.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005675 RID: 22133 RVA: 0x00129B77 File Offset: 0x00127D77
		public static string PtrToStringAuto(IntPtr ptr, int len)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.PtrToStringAnsi(ptr, len);
			}
			return Marshal.PtrToStringUni(ptr, len);
		}

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies all characters up to the first null character from an unmanaged Unicode string into it.</summary>
		/// <returns>A managed string that holds a copy of the unmanaged string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005676 RID: 22134
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringUni(IntPtr ptr);

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies a specified number of characters from an unmanaged Unicode string into it.</summary>
		/// <returns>A managed string that holds a copy of the unmanaged string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <param name="len">The number of Unicode characters to copy.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005677 RID: 22135
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringUni(IntPtr ptr, int len);

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies a BSTR Data Type string stored in unmanaged memory into it.</summary>
		/// <returns>A managed string that holds a copy of the unmanaged string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005678 RID: 22136
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringBSTR(IntPtr ptr);

		/// <summary>Marshals data from an unmanaged block of memory to a managed object.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory.</param>
		/// <param name="structure">The object to which the data is to be copied. This must be an instance of a formatted class.</param>
		/// <exception cref="T:System.ArgumentException">Structure layout is not sequential or explicit.-or- Structure is a boxed value type.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005679 RID: 22137
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PtrToStructure(IntPtr ptr, object structure);

		/// <summary>Marshals data from an unmanaged block of memory to a newly allocated managed object of the specified type.</summary>
		/// <returns>A managed object containing the data pointed to by the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">A pointer to an unmanaged block of memory.</param>
		/// <param name="structureType">The type of object to be created. This object must represent a formatted class or a structure.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="structureType" /> parameter layout is not sequential or explicit.-or-The <paramref name="structureType" /> parameter is a generic type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="structureType" /> is null.</exception>
		/// <exception cref="T:System.MissingMethodException">The class specified by <paramref name="structureType" /> does not have an accessible default constructor. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600567A RID: 22138
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object PtrToStructure(IntPtr ptr, Type structureType);

		/// <summary>Marshals data from an unmanaged block of memory to a managed object of the specified type. </summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory. </param>
		/// <param name="structure">The object to which the data is to be copied. </param>
		/// <typeparam name="T">The type of <paramref name="structure" />. This must be a formatted class. </typeparam>
		/// <exception cref="T:System.ArgumentException">Structure layout is not sequential or explicit. </exception>
		// Token: 0x0600567B RID: 22139 RVA: 0x00129B90 File Offset: 0x00127D90
		public static void PtrToStructure<T>(IntPtr ptr, T structure)
		{
			Marshal.PtrToStructure(ptr, structure);
		}

		/// <summary>Marshals data from an unmanaged block of memory to a newly allocated managed object of the type specified by a generic type parameter. </summary>
		/// <returns>A managed object that contains the data that the <paramref name="ptr" /> parameter points to. </returns>
		/// <param name="ptr">A pointer to an unmanaged block of memory. </param>
		/// <typeparam name="T">The type of the object to which the data is to be copied. This must be a formatted class or a structure. </typeparam>
		/// <exception cref="T:System.ArgumentException">The layout of <paramref name="T" /> is not sequential or explicit.</exception>
		/// <exception cref="T:System.MissingMethodException">The class specified by <paramref name="T" /> does not have an accessible default constructor. </exception>
		// Token: 0x0600567C RID: 22140 RVA: 0x00129B9E File Offset: 0x00127D9E
		public static T PtrToStructure<T>(IntPtr ptr)
		{
			return (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
		}

		// Token: 0x0600567D RID: 22141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int QueryInterfaceInternal(IntPtr pUnk, ref Guid iid, out IntPtr ppv);

		/// <summary>Requests a pointer to a specified interface from a COM object.</summary>
		/// <returns>An HRESULT that indicates the success or failure of the call.</returns>
		/// <param name="pUnk">The interface to be queried.</param>
		/// <param name="iid">The interface identifier (IID) of the requested interface.</param>
		/// <param name="ppv">When this method returns, contains a reference to the returned interface.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600567E RID: 22142 RVA: 0x00129BB5 File Offset: 0x00127DB5
		public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.QueryInterfaceInternal(pUnk, ref iid, out ppv);
		}

		/// <summary>Reads a single byte from unmanaged memory.</summary>
		/// <returns>The byte read from unmanaged memory.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null. -or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600567F RID: 22143 RVA: 0x00129BDC File Offset: 0x00127DDC
		public unsafe static byte ReadByte(IntPtr ptr)
		{
			return *(byte*)(void*)ptr;
		}

		/// <summary>Reads a single byte at a given offset (or index) from unmanaged memory.</summary>
		/// <returns>The byte read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005680 RID: 22144 RVA: 0x00129BE5 File Offset: 0x00127DE5
		public unsafe static byte ReadByte(IntPtr ptr, int ofs)
		{
			return ((byte*)(void*)ptr)[ofs];
		}

		/// <summary>Reads a single byte at a given offset (or index) from unmanaged memory. </summary>
		/// <returns>The byte read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005681 RID: 22145 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[SuppressUnmanagedCodeSecurity]
		public static byte ReadByte([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 16-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 16-bit signed integer read from unmanaged memory.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005682 RID: 22146 RVA: 0x00129BF0 File Offset: 0x00127DF0
		public unsafe static short ReadInt16(IntPtr ptr)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 1U) == 0U)
			{
				return *(short*)ptr2;
			}
			short num;
			Buffer.Memcpy((byte*)(&num), (byte*)(void*)ptr, 2);
			return num;
		}

		/// <summary>Reads a 16-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <returns>The 16-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005683 RID: 22147 RVA: 0x00129C20 File Offset: 0x00127E20
		public unsafe static short ReadInt16(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 1U) == 0U)
			{
				return *(short*)ptr2;
			}
			short num;
			Buffer.Memcpy((byte*)(&num), ptr2, 2);
			return num;
		}

		/// <summary>Reads a 16-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <returns>The 16-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005684 RID: 22148 RVA: 0x0002126B File Offset: 0x0001F46B
		[SuppressUnmanagedCodeSecurity]
		[MonoTODO]
		public static short ReadInt16([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 32-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 32-bit signed integer read from unmanaged memory.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005685 RID: 22149 RVA: 0x00129C4C File Offset: 0x00127E4C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static int ReadInt32(IntPtr ptr)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 3U) == 0U)
			{
				return *(int*)ptr2;
			}
			int num;
			Buffer.Memcpy((byte*)(&num), ptr2, 4);
			return num;
		}

		/// <summary>Reads a 32-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <returns>The 32-bit signed integer read from unmanaged memory.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005686 RID: 22150 RVA: 0x00129C74 File Offset: 0x00127E74
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static int ReadInt32(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 3) == 0)
			{
				return *(int*)ptr2;
			}
			int num;
			Buffer.Memcpy((byte*)(&num), ptr2, 4);
			return num;
		}

		/// <summary>Reads a 32-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <returns>The 32-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005687 RID: 22151 RVA: 0x0002126B File Offset: 0x0001F46B
		[SuppressUnmanagedCodeSecurity]
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int ReadInt32([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 64-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 64-bit signed integer read from unmanaged memory.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005688 RID: 22152 RVA: 0x00129CA0 File Offset: 0x00127EA0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static long ReadInt64(IntPtr ptr)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 7U) == 0U)
			{
				return *(long*)(void*)ptr;
			}
			long num;
			Buffer.Memcpy((byte*)(&num), ptr2, 8);
			return num;
		}

		/// <summary>Reads a 64-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <returns>The 64-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005689 RID: 22153 RVA: 0x00129CD0 File Offset: 0x00127ED0
		public unsafe static long ReadInt64(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 7U) == 0U)
			{
				return *(long*)ptr2;
			}
			long num;
			Buffer.Memcpy((byte*)(&num), ptr2, 8);
			return num;
		}

		/// <summary>Reads a 64-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <returns>The 64-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600568A RID: 22154 RVA: 0x0002126B File Offset: 0x0001F46B
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MonoTODO]
		public static long ReadInt64([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a processor native-sized integer from unmanaged memory.</summary>
		/// <returns>The integer read from unmanaged memory. A 32 bit integer is returned on 32 bit machines and a 64 bit integer is returned on 64 bit machines.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null. -or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600568B RID: 22155 RVA: 0x00129CFA File Offset: 0x00127EFA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr)
		{
			if (IntPtr.Size == 4)
			{
				return (IntPtr)Marshal.ReadInt32(ptr);
			}
			return (IntPtr)Marshal.ReadInt64(ptr);
		}

		/// <summary>Reads a processor native sized integer at a given offset from unmanaged memory.</summary>
		/// <returns>The integer read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600568C RID: 22156 RVA: 0x00129D1B File Offset: 0x00127F1B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
		{
			if (IntPtr.Size == 4)
			{
				return (IntPtr)Marshal.ReadInt32(ptr, ofs);
			}
			return (IntPtr)Marshal.ReadInt64(ptr, ofs);
		}

		/// <summary>Reads a processor native sized integer from unmanaged memory.</summary>
		/// <returns>The integer read from unmanaged memory at the given offset.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600568D RID: 22157 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Resizes a block of memory previously allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />.</summary>
		/// <returns>An integer representing the address of the reallocated block of memory. This memory must be released with <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" />.</returns>
		/// <param name="pv">A pointer to memory allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />.</param>
		/// <param name="cb">The new size of the allocated block.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600568E RID: 22158
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReAllocCoTaskMem(IntPtr pv, int cb);

		/// <summary>Resizes a block of memory previously allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.</summary>
		/// <returns>A pointer to the reallocated memory. This memory must be released using <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" />.</returns>
		/// <param name="pv">A pointer to memory allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.</param>
		/// <param name="cb">The new size of the allocated block. This is not a pointer; it is the byte count you are requesting, cast to type <see cref="T:System.IntPtr" />. If you pass a pointer, it is treated as a size.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600568F RID: 22159
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb);

		// Token: 0x06005690 RID: 22160
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReleaseInternal(IntPtr pUnk);

		/// <summary>Decrements the reference count on the specified interface.</summary>
		/// <returns>The new value of the reference count on the interface specified by the <paramref name="pUnk" /> parameter.</returns>
		/// <param name="pUnk">The interface to release.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005691 RID: 22161 RVA: 0x00129D3E File Offset: 0x00127F3E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Release(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.ReleaseInternal(pUnk);
		}

		// Token: 0x06005692 RID: 22162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReleaseComObjectInternal(object co);

		/// <summary>Decrements the reference count of the specified Runtime Callable Wrapper (RCW) associated with the specified COM object.</summary>
		/// <returns>The new value of the reference count of the RCW associated with <paramref name="o" />. This value is typically zero since the RCW keeps just one reference to the wrapped COM object regardless of the number of managed clients calling it.</returns>
		/// <param name="o">The COM object to release.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is not a valid COM object.</exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="o" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005693 RID: 22163 RVA: 0x00129D63 File Offset: 0x00127F63
		public static int ReleaseComObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentException("Value cannot be null.", "o");
			}
			if (!Marshal.IsComObject(o))
			{
				throw new ArgumentException("Value must be a Com object.", "o");
			}
			return Marshal.ReleaseComObjectInternal(o);
		}

		/// <summary>Releases the thread cache.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005694 RID: 22164 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[Obsolete]
		public static void ReleaseThreadCache()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets data referenced by the specified key in the specified COM object.</summary>
		/// <returns>true if the data was set successfully; otherwise, false.</returns>
		/// <param name="obj">The COM object in which to store the data.</param>
		/// <param name="key">The key in the internal hash table of the COM object in which to store the data.</param>
		/// <param name="data">The data to set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null.-or- <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a COM object.-or-<paramref name="obj" /> is a Windows Runtime object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005695 RID: 22165 RVA: 0x00129891 File Offset: 0x00127A91
		[MonoNotSupported("MSDN states user code should never need to call this method.")]
		public static bool SetComObjectData(object obj, object key, object data)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		/// <summary>Returns the unmanaged size of an object in bytes.</summary>
		/// <returns>The size of the specified object in unmanaged code.</returns>
		/// <param name="structure">The object whose size is to be returned.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="structure" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005696 RID: 22166 RVA: 0x00129D96 File Offset: 0x00127F96
		[ComVisible(true)]
		public static int SizeOf(object structure)
		{
			return Marshal.SizeOf(structure.GetType());
		}

		/// <summary>Returns the size of an unmanaged type in bytes.</summary>
		/// <returns>The size of the specified type in unmanaged code.</returns>
		/// <param name="t">The type whose size is to be returned.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="t" /> parameter is a generic type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005697 RID: 22167
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int SizeOf(Type t);

		/// <summary>Returns the size of an unmanaged type in bytes. </summary>
		/// <returns>The size, in bytes, of the type that is specified by the <paramref name="T" /> generic type parameter. </returns>
		/// <typeparam name="T">The type whose size is to be returned. </typeparam>
		// Token: 0x06005698 RID: 22168 RVA: 0x00129DA3 File Offset: 0x00127FA3
		public static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		/// <summary>Returns the unmanaged size of an object of a specified type in bytes. </summary>
		/// <returns>The size, in bytes, of the specified object in unmanaged code. </returns>
		/// <param name="structure">The object whose size is to be returned. </param>
		/// <typeparam name="T">The type of the <paramref name="structure" /> parameter. </typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="structure" /> parameter is null.</exception>
		// Token: 0x06005699 RID: 22169 RVA: 0x00129DB4 File Offset: 0x00127FB4
		public static int SizeOf<T>(T structure)
		{
			return Marshal.SizeOf(structure.GetType());
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x00129DC8 File Offset: 0x00127FC8
		internal static uint SizeOfType(Type type)
		{
			return (uint)Marshal.SizeOf(type);
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x00129DD0 File Offset: 0x00127FD0
		internal static uint AlignedSizeOf<T>() where T : struct
		{
			uint num = Marshal.SizeOfType(typeof(T));
			if (num == 1U || num == 2U)
			{
				return num;
			}
			if (IntPtr.Size == 8 && num == 4U)
			{
				return num;
			}
			return (num + 3U) & 4294967292U;
		}

		/// <summary>Allocates a BSTR Data Type and copies the contents of a managed <see cref="T:System.String" /> into it.</summary>
		/// <returns>An unmanaged pointer to the BSTR, or 0 if <paramref name="s" /> is null.</returns>
		/// <param name="s">The managed string to be copied.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="s" /> is out of range.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600569C RID: 22172
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr StringToBSTR(string s);

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>An integer representing a pointer to the block of memory allocated for the string, or 0 if <paramref name="s" /> is null.</returns>
		/// <param name="s">A managed string to be copied.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600569D RID: 22173 RVA: 0x00129E0B File Offset: 0x0012800B
		public static IntPtr StringToCoTaskMemAnsi(string s)
		{
			return Marshal.StringToAllocatedMemoryUTF8(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>The allocated memory block, or 0 if <paramref name="s" /> is null.</returns>
		/// <param name="s">A managed string to be copied.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="s" /> is out of range.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600569E RID: 22174 RVA: 0x00129E13 File Offset: 0x00128013
		public static IntPtr StringToCoTaskMemAuto(string s)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.StringToCoTaskMemAnsi(s);
			}
			return Marshal.StringToCoTaskMemUni(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>An integer representing a pointer to the block of memory allocated for the string, or 0 if s is null.</returns>
		/// <param name="s">A managed string to be copied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600569F RID: 22175 RVA: 0x00129E2C File Offset: 0x0012802C
		public static IntPtr StringToCoTaskMemUni(string s)
		{
			int num = s.Length + 1;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * 2);
			char[] array = new char[num];
			s.CopyTo(0, array, 0, s.Length);
			array[s.Length] = '\0';
			Marshal.copy_to_unmanaged(array, 0, intPtr, num);
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory, converting into ANSI format as it copies.</summary>
		/// <returns>The address, in unmanaged memory, to where <paramref name="s" /> was copied, or 0 if <paramref name="s" /> is null.</returns>
		/// <param name="s">A managed string to be copied.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056A0 RID: 22176
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr StringToHGlobalAnsi(string s);

		// Token: 0x060056A1 RID: 22177 RVA: 0x00129E74 File Offset: 0x00128074
		public unsafe static IntPtr StringToAllocatedMemoryUTF8(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * 3;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Marshal.AllocCoTaskMemSize(new UIntPtr((uint)(num + 1)));
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			byte* ptr = (byte*)(void*)intPtr;
			int bytesFromEncoding = s.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			ptr[bytesFromEncoding] = 0;
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory, converting into ANSI format if required.</summary>
		/// <returns>The address, in unmanaged memory, to where the string was copied, or 0 if <paramref name="s" /> is null.</returns>
		/// <param name="s">A managed string to be copied.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056A2 RID: 22178 RVA: 0x00129EE3 File Offset: 0x001280E3
		public static IntPtr StringToHGlobalAuto(string s)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.StringToHGlobalAnsi(s);
			}
			return Marshal.StringToHGlobalUni(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory.</summary>
		/// <returns>The address, in unmanaged memory, to where the <paramref name="s" /> was copied, or 0 if <paramref name="s" /> is null.</returns>
		/// <param name="s">A managed string to be copied.</param>
		/// <exception cref="T:System.OutOfMemoryException">The method could not allocate enough native heap memory.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056A3 RID: 22179
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr StringToHGlobalUni(string s);

		/// <summary>Allocates a BSTR Data Type and copies the contents of a managed <see cref="T:System.Security.SecureString" /> object into it.</summary>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null object was supplied.</returns>
		/// <param name="s">The managed object to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056A4 RID: 22180 RVA: 0x00129EFC File Offset: 0x001280FC
		public static IntPtr SecureStringToBSTR(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			byte[] buffer = s.GetBuffer();
			int length = s.Length;
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < buffer.Length; i += 2)
				{
					byte b = buffer[i];
					buffer[i] = buffer[i + 1];
					buffer[i + 1] = b;
				}
			}
			return Marshal.BufferToBSTR(buffer, length);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null object was supplied.</returns>
		/// <param name="s">The managed object to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056A5 RID: 22181 RVA: 0x00129F54 File Offset: 0x00128154
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem(length + 1);
			byte[] array = new byte[length + 1];
			try
			{
				byte[] buffer = s.GetBuffer();
				int i = 0;
				int num = 0;
				while (i < length)
				{
					array[i] = buffer[num + 1];
					buffer[num] = 0;
					buffer[num + 1] = 0;
					i++;
					num += 2;
				}
				array[i] = 0;
				Marshal.copy_to_unmanaged(array, 0, intPtr, length + 1);
			}
			finally
			{
				int j = length;
				while (j > 0)
				{
					j--;
					array[j] = 0;
				}
			}
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null object was supplied.</returns>
		/// <param name="s">The managed object to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060056A6 RID: 22182 RVA: 0x00129FF8 File Offset: 0x001281F8
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem(length * 2 + 2);
			byte[] array = null;
			try
			{
				array = s.GetBuffer();
				for (int i = 0; i < length; i++)
				{
					Marshal.WriteInt16(intPtr, i * 2, (short)(((int)array[i * 2] << 8) | (int)array[i * 2 + 1]));
				}
				Marshal.WriteInt16(intPtr, array.Length, 0);
			}
			finally
			{
				if (array != null)
				{
					int j = array.Length;
					while (j > 0)
					{
						j--;
						array[j] = 0;
					}
				}
			}
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> into unmanaged memory, converting into ANSI format as it copies.</summary>
		/// <returns>The address, in unmanaged memory, to where the <paramref name="s" /> parameter was copied, or 0 if a null object was supplied.</returns>
		/// <param name="s">The managed object to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056A7 RID: 22183 RVA: 0x0012A08C File Offset: 0x0012828C
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToCoTaskMemAnsi(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object into unmanaged memory.</summary>
		/// <returns>The address, in unmanaged memory, where <paramref name="s" /> was copied, or 0 if <paramref name="s" /> is a <see cref="T:System.Security.SecureString" /> object whose length is 0.</returns>
		/// <param name="s">The managed object to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060056A8 RID: 22184 RVA: 0x0012A0A2 File Offset: 0x001282A2
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToCoTaskMemUnicode(s);
		}

		/// <summary>Marshals data from a managed object to an unmanaged block of memory.</summary>
		/// <param name="structure">A managed object that holds the data to be marshaled. This object must be a structure or an instance of a formatted class. </param>
		/// <param name="ptr">A pointer to an unmanaged block of memory, which must be allocated before this method is called.</param>
		/// <param name="fDeleteOld">true to call the <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure(System.IntPtr,System.Type)" /> method on the <paramref name="ptr" /> parameter before this method copies the data. The block must contain valid data. Note that passing false when the memory block already contains data can lead to a memory leak.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="structure" /> is a reference type that is not a formatted class. -or-<paramref name="structure" /> is a generic type. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056A9 RID: 22185
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

		/// <summary>Marshals data from a managed object of a specified type to an unmanaged block of memory. </summary>
		/// <param name="structure">A managed object that holds the data to be marshaled. The object must be a structure or an instance of a formatted class. </param>
		/// <param name="ptr">A pointer to an unmanaged block of memory, which must be allocated before this method is called. </param>
		/// <param name="fDeleteOld">true to call the <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure``1(System.IntPtr)" /> method on the <paramref name="ptr" /> parameter before this method copies the data. The block must contain valid data. Note that passing false when the memory block already contains data can lead to a memory leak.</param>
		/// <typeparam name="T">The type of the managed object. </typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="structure" /> is a reference type that is not a formatted class. </exception>
		// Token: 0x060056AA RID: 22186 RVA: 0x0012A0B8 File Offset: 0x001282B8
		public static void StructureToPtr<T>(T structure, IntPtr ptr, bool fDeleteOld)
		{
			Marshal.StructureToPtr(structure, ptr, fDeleteOld);
		}

		/// <summary>Throws an exception with a specific failure HRESULT value.</summary>
		/// <param name="errorCode">The HRESULT corresponding to the desired exception.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056AB RID: 22187 RVA: 0x0012A0C8 File Offset: 0x001282C8
		public static void ThrowExceptionForHR(int errorCode)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		/// <summary>Throws an exception with a specific failure HRESULT, based on the specified IErrorInfo Interface interface.</summary>
		/// <param name="errorCode">The HRESULT corresponding to the desired exception.</param>
		/// <param name="errorInfo">A pointer to the IErrorInfo interface that provides more information about the error. You can specify IntPtr(0) to use the current IErrorInfo interface, or IntPtr(-1) to ignore the current IErrorInfo interface and construct the exception just from the error code.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056AC RID: 22188 RVA: 0x0012A0E4 File Offset: 0x001282E4
		public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode, errorInfo);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		// Token: 0x060056AD RID: 22189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr BufferToBSTR(Array ptr, int slen);

		/// <summary>Gets the address of the element at the specified index inside the specified array.</summary>
		/// <returns>The address of <paramref name="index" /> inside <paramref name="arr" />.</returns>
		/// <param name="arr">The array that contains the desired element.</param>
		/// <param name="index">The index in the <paramref name="arr" /> parameter of the desired element.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056AE RID: 22190
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index);

		/// <summary>Gets the address of the element at the specified index in an array of a specified type. </summary>
		/// <returns>The address of <paramref name="index" /> in <paramref name="arr" />. </returns>
		/// <param name="arr">The array that contains the desired element. </param>
		/// <param name="index">The index of the desired element in the <paramref name="arr" /> array. </param>
		/// <typeparam name="T">The type of the array. </typeparam>
		// Token: 0x060056AF RID: 22191 RVA: 0x0012A0FE File Offset: 0x001282FE
		public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(arr, index);
		}

		/// <summary>Writes a single byte value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B0 RID: 22192 RVA: 0x0012A107 File Offset: 0x00128307
		public unsafe static void WriteByte(IntPtr ptr, byte val)
		{
			*(byte*)(void*)ptr = val;
		}

		/// <summary>Writes a single byte value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B1 RID: 22193 RVA: 0x0012A111 File Offset: 0x00128311
		public unsafe static void WriteByte(IntPtr ptr, int ofs, byte val)
		{
			*(byte*)(void*)IntPtr.Add(ptr, ofs) = val;
		}

		/// <summary>Writes a single byte value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B2 RID: 22194 RVA: 0x0002126B File Offset: 0x0001F46B
		[SuppressUnmanagedCodeSecurity]
		[MonoTODO]
		public static void WriteByte([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, byte val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 16-bit integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B3 RID: 22195 RVA: 0x0012A124 File Offset: 0x00128324
		public unsafe static void WriteInt16(IntPtr ptr, short val)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 1U) == 0U)
			{
				*(short*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 2);
		}

		/// <summary>Writes a 16-bit signed integer value into unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B4 RID: 22196 RVA: 0x0012A14C File Offset: 0x0012834C
		public unsafe static void WriteInt16(IntPtr ptr, int ofs, short val)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 1U) == 0U)
			{
				*(short*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 2);
		}

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B5 RID: 22197 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[SuppressUnmanagedCodeSecurity]
		public static void WriteInt16([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, short val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a character as a 16-bit integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B6 RID: 22198 RVA: 0x0012A176 File Offset: 0x00128376
		public static void WriteInt16(IntPtr ptr, char val)
		{
			Marshal.WriteInt16(ptr, 0, (short)val);
		}

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in the native heap to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B7 RID: 22199 RVA: 0x0012A181 File Offset: 0x00128381
		public static void WriteInt16(IntPtr ptr, int ofs, char val)
		{
			Marshal.WriteInt16(ptr, ofs, (short)val);
		}

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B8 RID: 22200 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static void WriteInt16([In] [Out] object ptr, int ofs, char val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 32-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null. -or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056B9 RID: 22201 RVA: 0x0012A18C File Offset: 0x0012838C
		public unsafe static void WriteInt32(IntPtr ptr, int val)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 3U) == 0U)
			{
				*(int*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 4);
		}

		/// <summary>Writes a 32-bit signed integer value into unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056BA RID: 22202 RVA: 0x0012A1B4 File Offset: 0x001283B4
		public unsafe static void WriteInt32(IntPtr ptr, int ofs, int val)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 3U) == 0U)
			{
				*(int*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 4);
		}

		/// <summary>Writes a 32-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056BB RID: 22203 RVA: 0x0002126B File Offset: 0x0001F46B
		[SuppressUnmanagedCodeSecurity]
		[MonoTODO]
		public static void WriteInt32([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, int val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056BC RID: 22204 RVA: 0x0012A1E0 File Offset: 0x001283E0
		public unsafe static void WriteInt64(IntPtr ptr, long val)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 7U) == 0U)
			{
				*(long*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 8);
		}

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056BD RID: 22205 RVA: 0x0012A208 File Offset: 0x00128408
		public unsafe static void WriteInt64(IntPtr ptr, int ofs, long val)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 7U) == 0U)
			{
				*(long*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 8);
		}

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056BE RID: 22206 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[SuppressUnmanagedCodeSecurity]
		public static void WriteInt64([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, long val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a processor native sized integer value into unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.-or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056BF RID: 22207 RVA: 0x0012A232 File Offset: 0x00128432
		public static void WriteIntPtr(IntPtr ptr, IntPtr val)
		{
			if (IntPtr.Size == 4)
			{
				Marshal.WriteInt32(ptr, (int)val);
				return;
			}
			Marshal.WriteInt64(ptr, (long)val);
		}

		/// <summary>Writes a processor native-sized integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056C0 RID: 22208 RVA: 0x0012A255 File Offset: 0x00128455
		public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
		{
			if (IntPtr.Size == 4)
			{
				Marshal.WriteInt32(ptr, ofs, (int)val);
				return;
			}
			Marshal.WriteInt64(ptr, ofs, (long)val);
		}

		/// <summary>Writes a processor native sized integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056C1 RID: 22209 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static void WriteIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, IntPtr val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x0012A27C File Offset: 0x0012847C
		private static Exception ConvertHrToException(int errorCode)
		{
			if (errorCode > -2147024362)
			{
				if (errorCode <= -2146232828)
				{
					if (errorCode <= -2146893792)
					{
						if (errorCode != -2147023895)
						{
							if (errorCode != -2146893792)
							{
								goto IL_03A9;
							}
							return new CryptographicException();
						}
					}
					else
					{
						if (errorCode == -2146234348)
						{
							return new AppDomainUnloadedException();
						}
						switch (errorCode)
						{
						case -2146233088:
							return new Exception();
						case -2146233087:
							return new SystemException();
						case -2146233086:
							return new ArgumentOutOfRangeException();
						case -2146233085:
							return new ArrayTypeMismatchException();
						case -2146233084:
							return new ContextMarshalException();
						case -2146233083:
						case -2146233074:
						case -2146233073:
						case -2146233061:
						case -2146233060:
						case -2146233059:
						case -2146233058:
						case -2146233057:
						case -2146233055:
						case -2146233053:
						case -2146233052:
						case -2146233051:
						case -2146233050:
						case -2146233046:
						case -2146233045:
						case -2146233044:
						case -2146233043:
						case -2146233042:
						case -2146233041:
						case -2146233040:
						case -2146233035:
						case -2146233034:
							goto IL_03A9;
						case -2146233082:
							return new ExecutionEngineException();
						case -2146233081:
							return new FieldAccessException();
						case -2146233080:
							return new IndexOutOfRangeException();
						case -2146233079:
							return new InvalidOperationException();
						case -2146233078:
							return new SecurityException();
						case -2146233077:
							return new RemotingException();
						case -2146233076:
							return new SerializationException();
						case -2146233075:
							return new VerificationException();
						case -2146233072:
							return new MethodAccessException();
						case -2146233071:
							return new MissingFieldException();
						case -2146233070:
							return new MissingMemberException();
						case -2146233069:
							return new MissingMethodException();
						case -2146233068:
							return new MulticastNotSupportedException();
						case -2146233067:
							return new NotSupportedException();
						case -2146233066:
							return new OverflowException();
						case -2146233065:
							return new RankException();
						case -2146233064:
							return new SynchronizationLockException();
						case -2146233063:
							return new ThreadInterruptedException();
						case -2146233062:
							return new MemberAccessException();
						case -2146233056:
							return new ThreadStateException();
						case -2146233054:
							return new TypeLoadException();
						case -2146233049:
							return new InvalidComObjectException();
						case -2146233048:
							return new NotFiniteNumberException();
						case -2146233047:
							return new DuplicateWaitObjectException();
						case -2146233039:
							return new InvalidOleVariantTypeException();
						case -2146233038:
							return new MissingManifestResourceException();
						case -2146233037:
							return new SafeArrayTypeMismatchException();
						case -2146233036:
							return new TypeInitializationException("", null);
						case -2146233033:
							return new FormatException();
						default:
							switch (errorCode)
							{
							case -2146232832:
								return new ApplicationException();
							case -2146232831:
								return new InvalidFilterCriteriaException();
							case -2146232830:
								return new ReflectionTypeLoadException(new Type[0], new Exception[0]);
							case -2146232829:
								return new TargetException();
							case -2146232828:
								return new TargetInvocationException(null);
							default:
								goto IL_03A9;
							}
							break;
						}
					}
				}
				else if (errorCode <= 3)
				{
					if (errorCode == -2146232800)
					{
						return new IOException();
					}
					if (errorCode == 2)
					{
						goto IL_02A6;
					}
					if (errorCode != 3)
					{
						goto IL_03A9;
					}
					goto IL_027C;
				}
				else
				{
					if (errorCode == 11)
					{
						goto IL_026A;
					}
					if (errorCode == 206)
					{
						goto IL_032A;
					}
					if (errorCode != 1001)
					{
						goto IL_03A9;
					}
				}
				return new StackOverflowException();
			}
			if (errorCode <= -2147024893)
			{
				if (errorCode <= -2147352562)
				{
					switch (errorCode)
					{
					case -2147467263:
						return new NotImplementedException();
					case -2147467262:
						return new InvalidCastException();
					case -2147467261:
						return new NullReferenceException();
					default:
						if (errorCode != -2147352562)
						{
							goto IL_03A9;
						}
						return new TargetParameterCountException();
					}
				}
				else
				{
					if (errorCode == -2147352558)
					{
						return new DivideByZeroException();
					}
					if (errorCode == -2147024894)
					{
						goto IL_02A6;
					}
					if (errorCode != -2147024893)
					{
						goto IL_03A9;
					}
					goto IL_027C;
				}
			}
			else if (errorCode <= -2147024858)
			{
				if (errorCode != -2147024885)
				{
					if (errorCode == -2147024882)
					{
						return new OutOfMemoryException();
					}
					if (errorCode != -2147024858)
					{
						goto IL_03A9;
					}
					return new EndOfStreamException();
				}
			}
			else
			{
				if (errorCode == -2147024809)
				{
					return new ArgumentException();
				}
				if (errorCode == -2147024690)
				{
					goto IL_032A;
				}
				if (errorCode != -2147024362)
				{
					goto IL_03A9;
				}
				return new ArithmeticException();
			}
			IL_026A:
			return new BadImageFormatException();
			IL_027C:
			return new DirectoryNotFoundException();
			IL_02A6:
			return new FileNotFoundException();
			IL_032A:
			return new PathTooLongException();
			IL_03A9:
			if (errorCode < 0)
			{
				return new COMException("", errorCode);
			}
			return null;
		}

		// Token: 0x060056C3 RID: 22211
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetErrorInfo")]
		private static extern int _SetErrorInfo(int dwReserved, [MarshalAs(UnmanagedType.Interface)] IErrorInfo pIErrorInfo);

		// Token: 0x060056C4 RID: 22212
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetErrorInfo")]
		private static extern int _GetErrorInfo(int dwReserved, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppIErrorInfo);

		// Token: 0x060056C5 RID: 22213 RVA: 0x0012A644 File Offset: 0x00128844
		internal static int SetErrorInfo(int dwReserved, IErrorInfo errorInfo)
		{
			int num = 0;
			errorInfo = null;
			if (Marshal.SetErrorInfoNotAvailable)
			{
				return -1;
			}
			try
			{
				num = Marshal._SetErrorInfo(dwReserved, errorInfo);
			}
			catch (Exception)
			{
				Marshal.SetErrorInfoNotAvailable = true;
			}
			return num;
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x0012A684 File Offset: 0x00128884
		internal static int GetErrorInfo(int dwReserved, out IErrorInfo errorInfo)
		{
			int num = 0;
			errorInfo = null;
			if (Marshal.GetErrorInfoNotAvailable)
			{
				return -1;
			}
			try
			{
				num = Marshal._GetErrorInfo(dwReserved, out errorInfo);
			}
			catch (Exception)
			{
				Marshal.GetErrorInfoNotAvailable = true;
			}
			return num;
		}

		/// <summary>Converts the specified HRESULT error code to a corresponding <see cref="T:System.Exception" /> object.</summary>
		/// <returns>An object that represents the converted HRESULT.</returns>
		/// <param name="errorCode">The HRESULT to be converted.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056C7 RID: 22215 RVA: 0x0012A6C4 File Offset: 0x001288C4
		public static Exception GetExceptionForHR(int errorCode)
		{
			return Marshal.GetExceptionForHR(errorCode, IntPtr.Zero);
		}

		/// <summary>Converts the specified HRESULT error code to a corresponding <see cref="T:System.Exception" /> object, with additional error information passed in an IErrorInfo interface for the exception object.</summary>
		/// <returns>An object that represents the converted HRESULT and information obtained from <paramref name="errorInfo" />.</returns>
		/// <param name="errorCode">The HRESULT to be converted.</param>
		/// <param name="errorInfo">A pointer to the IErrorInfo interface that provides more information about the error. You can specify IntPtr(0) to use the current IErrorInfo interface, or IntPtr(-1) to ignore the current IErrorInfo interface and construct the exception just from the error code. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056C8 RID: 22216 RVA: 0x0012A6D4 File Offset: 0x001288D4
		public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			IErrorInfo errorInfo2 = null;
			if (errorInfo != (IntPtr)(-1))
			{
				if (errorInfo == IntPtr.Zero)
				{
					if (Marshal.GetErrorInfo(0, out errorInfo2) != 0)
					{
						errorInfo2 = null;
					}
				}
				else
				{
					errorInfo2 = Marshal.GetObjectForIUnknown(errorInfo) as IErrorInfo;
				}
			}
			if (errorInfo2 is ManagedErrorInfo && ((ManagedErrorInfo)errorInfo2).Exception._HResult == errorCode)
			{
				return ((ManagedErrorInfo)errorInfo2).Exception;
			}
			Exception ex = Marshal.ConvertHrToException(errorCode);
			if (errorInfo2 != null && ex != null)
			{
				uint num;
				errorInfo2.GetHelpContext(out num);
				string text;
				errorInfo2.GetSource(out text);
				ex.Source = text;
				errorInfo2.GetDescription(out text);
				ex.SetMessage(text);
				errorInfo2.GetHelpFile(out text);
				if (num == 0U)
				{
					ex.HelpLink = text;
				}
				else
				{
					ex.HelpLink = string.Format("{0}#{1}", text, num);
				}
			}
			return ex;
		}

		/// <summary>Releases all references to a Runtime Callable Wrapper (RCW) by setting its reference count to 0.</summary>
		/// <returns>The new value of the reference count of the RCW associated with the <paramref name="o" />parameter, which is 0 (zero) if the release is successful.</returns>
		/// <param name="o">The RCW to be released.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is not a valid COM object.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056C9 RID: 22217 RVA: 0x0012A7A2 File Offset: 0x001289A2
		public static int FinalReleaseComObject(object o)
		{
			while (Marshal.ReleaseComObject(o) != 0)
			{
			}
			return 0;
		}

		// Token: 0x060056CA RID: 22218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

		/// <summary>Converts an unmanaged function pointer to a delegate.</summary>
		/// <returns>A delegate instance that can be cast to the appropriate delegate type.</returns>
		/// <param name="ptr">The unmanaged function pointer to be converted.</param>
		/// <param name="t">The type of the delegate to be returned.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="t" /> parameter is not a delegate or is generic.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ptr" /> parameter is null.-or-The <paramref name="t" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056CB RID: 22219 RVA: 0x0012A7B0 File Offset: 0x001289B0
		public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsSubclassOf(typeof(MulticastDelegate)) || t == typeof(MulticastDelegate))
			{
				throw new ArgumentException("Type is not a delegate", "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException("The specified Type must not be a generic type definition.");
			}
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
		}

		/// <summary>Converts an unmanaged function pointer to a delegate of a specified type. </summary>
		/// <returns>A instance of the specified delegate type.</returns>
		/// <param name="ptr">The unmanaged function pointer to convert. </param>
		/// <typeparam name="TDelegate">The type of the delegate to return. </typeparam>
		/// <exception cref="T:System.ArgumentException">The <paramref name="TDelegate" /> generic parameter is not a delegate, or it is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ptr" /> parameter is null.</exception>
		// Token: 0x060056CC RID: 22220 RVA: 0x0012A837 File Offset: 0x00128A37
		public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
		{
			return (TDelegate)((object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate)));
		}

		// Token: 0x060056CD RID: 22221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

		/// <summary>Converts a delegate into a function pointer that is callable from unmanaged code.</summary>
		/// <returns>A value that can be passed to unmanaged code, which, in turn, can use it to call the underlying managed delegate. </returns>
		/// <param name="d">The delegate to be passed to unmanaged code.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="d" /> parameter is a generic type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="d" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060056CE RID: 22222 RVA: 0x0012A84E File Offset: 0x00128A4E
		public static IntPtr GetFunctionPointerForDelegate(Delegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal(d);
		}

		/// <summary>Converts a delegate of a specified type to a function pointer that is callable from unmanaged code. </summary>
		/// <returns>A value that can be passed to unmanaged code, which, in turn, can use it to call the underlying managed delegate. </returns>
		/// <param name="d">The delegate to be passed to unmanaged code. </param>
		/// <typeparam name="TDelegate">The type of delegate to convert. </typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="d" /> parameter is null. </exception>
		// Token: 0x060056CF RID: 22223 RVA: 0x0012A864 File Offset: 0x00128A64
		public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal((Delegate)((object)d));
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x00002194 File Offset: 0x00000394
		internal static void SetLastWin32Error(int error)
		{
		}

		// Token: 0x060056D1 RID: 22225
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetRawIUnknownForComObjectNoAddRef(object o);

		// Token: 0x060056D2 RID: 22226
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetHRForException_WinRT(Exception e);

		// Token: 0x060056D3 RID: 22227
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetNativeActivationFactory(Type type);

		/// <summary>Represents the maximum size of a double byte character set (DBCS) size, in bytes, for the current operating system. This field is read-only.</summary>
		// Token: 0x04002DAD RID: 11693
		public static readonly int SystemMaxDBCSCharSize = 2;

		/// <summary>Represents the default character size on the system; the default is 2 for Unicode systems and 1 for ANSI systems. This field is read-only.</summary>
		// Token: 0x04002DAE RID: 11694
		public static readonly int SystemDefaultCharSize = (Environment.IsRunningOnWindows ? 2 : 1);

		// Token: 0x04002DAF RID: 11695
		private static bool SetErrorInfoNotAvailable;

		// Token: 0x04002DB0 RID: 11696
		private static bool GetErrorInfoNotAvailable;
	}
}
