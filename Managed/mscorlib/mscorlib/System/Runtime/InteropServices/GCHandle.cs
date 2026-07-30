using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a way to access a managed object from unmanaged memory.</summary>
	// Token: 0x02000915 RID: 2325
	[ComVisible(true)]
	[MonoTODO("Struct should be [StructLayout(LayoutKind.Sequential)] but will need to be reordered for that.")]
	public struct GCHandle
	{
		// Token: 0x060055DA RID: 21978 RVA: 0x001293CF File Offset: 0x001275CF
		private GCHandle(IntPtr h)
		{
			this.handle = (int)h;
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x001293DD File Offset: 0x001275DD
		private GCHandle(object obj)
		{
			this = new GCHandle(obj, GCHandleType.Normal);
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x001293E7 File Offset: 0x001275E7
		internal GCHandle(object value, GCHandleType type)
		{
			if (type < GCHandleType.Weak || type > GCHandleType.Pinned)
			{
				type = GCHandleType.Normal;
			}
			this.handle = GCHandle.GetTargetHandle(value, 0, type);
		}

		/// <summary>Gets a value indicating whether the handle is allocated.</summary>
		/// <returns>true if the handle is allocated; otherwise, false.</returns>
		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x00129402 File Offset: 0x00127602
		public bool IsAllocated
		{
			get
			{
				return this.handle != 0;
			}
		}

		/// <summary>Gets or sets the object this handle represents.</summary>
		/// <returns>The object this handle represents.</returns>
		/// <exception cref="T:System.InvalidOperationException">The handle was freed, or never initialized. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060055DE RID: 21982 RVA: 0x0012940D File Offset: 0x0012760D
		// (set) Token: 0x060055DF RID: 21983 RVA: 0x00129432 File Offset: 0x00127632
		public object Target
		{
			get
			{
				if (!this.IsAllocated)
				{
					throw new InvalidOperationException(Locale.GetText("Handle is not allocated"));
				}
				return GCHandle.GetTarget(this.handle);
			}
			set
			{
				this.handle = GCHandle.GetTargetHandle(value, this.handle, (GCHandleType)(-1));
			}
		}

		/// <summary>Retrieves the address of an object in a <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" /> handle.</summary>
		/// <returns>The address of the of the Pinned object as an <see cref="T:System.IntPtr" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The handle is any type other than <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055E0 RID: 21984 RVA: 0x00129447 File Offset: 0x00127647
		public IntPtr AddrOfPinnedObject()
		{
			IntPtr addrOfPinnedObject = GCHandle.GetAddrOfPinnedObject(this.handle);
			if (addrOfPinnedObject == (IntPtr)(-1))
			{
				throw new ArgumentException("Object contains non-primitive or non-blittable data.");
			}
			if (addrOfPinnedObject == (IntPtr)(-2))
			{
				throw new InvalidOperationException("Handle is not pinned.");
			}
			return addrOfPinnedObject;
		}

		/// <summary>Allocates a <see cref="F:System.Runtime.InteropServices.GCHandleType.Normal" /> handle for the specified object.</summary>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> that protects the object from garbage collection. This <see cref="T:System.Runtime.InteropServices.GCHandle" /> must be released with <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> when it is no longer needed.</returns>
		/// <param name="value">The object that uses the <see cref="T:System.Runtime.InteropServices.GCHandle" />. </param>
		/// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055E1 RID: 21985 RVA: 0x00129487 File Offset: 0x00127687
		public static GCHandle Alloc(object value)
		{
			return new GCHandle(value);
		}

		/// <summary>Allocates a handle of the specified type for the specified object.</summary>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> of the specified type. This <see cref="T:System.Runtime.InteropServices.GCHandle" /> must be released with <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> when it is no longer needed.</returns>
		/// <param name="value">The object that uses the <see cref="T:System.Runtime.InteropServices.GCHandle" />. </param>
		/// <param name="type">One of the <see cref="T:System.Runtime.InteropServices.GCHandleType" /> values, indicating the type of <see cref="T:System.Runtime.InteropServices.GCHandle" /> to create. </param>
		/// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055E2 RID: 21986 RVA: 0x0012948F File Offset: 0x0012768F
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		/// <summary>Releases a <see cref="T:System.Runtime.InteropServices.GCHandle" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The handle was freed or never initialized. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055E3 RID: 21987 RVA: 0x00129498 File Offset: 0x00127698
		public void Free()
		{
			int num = this.handle;
			if (num != 0 && Interlocked.CompareExchange(ref this.handle, 0, num) == num)
			{
				GCHandle.FreeHandle(num);
				return;
			}
			throw new InvalidOperationException("Handle is not initialized.");
		}

		/// <summary>A <see cref="T:System.Runtime.InteropServices.GCHandle" /> is stored using an internal integer representation.</summary>
		/// <returns>The integer value.</returns>
		/// <param name="value">The <see cref="T:System.Runtime.InteropServices.GCHandle" /> for which the integer is required. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055E4 RID: 21988 RVA: 0x001294D0 File Offset: 0x001276D0
		public static explicit operator IntPtr(GCHandle value)
		{
			return (IntPtr)value.handle;
		}

		/// <summary>A <see cref="T:System.Runtime.InteropServices.GCHandle" /> is stored using an internal integer representation.</summary>
		/// <returns>The stored <see cref="T:System.Runtime.InteropServices.GCHandle" /> object using an internal integer representation.</returns>
		/// <param name="value">An <see cref="T:System.IntPtr" /> that indicates the handle for which the conversion is required. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060055E5 RID: 21989 RVA: 0x001294DD File Offset: 0x001276DD
		public static explicit operator GCHandle(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				throw new InvalidOperationException("GCHandle value cannot be zero");
			}
			if (!GCHandle.CheckCurrentDomain((int)value))
			{
				throw new ArgumentException("GCHandle value belongs to a different domain");
			}
			return new GCHandle(value);
		}

		// Token: 0x060055E6 RID: 21990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckCurrentDomain(int handle);

		// Token: 0x060055E7 RID: 21991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetTarget(int handle);

		// Token: 0x060055E8 RID: 21992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTargetHandle(object obj, int handle, GCHandleType type);

		// Token: 0x060055E9 RID: 21993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeHandle(int handle);

		// Token: 0x060055EA RID: 21994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetAddrOfPinnedObject(int handle);

		/// <summary>Returns a value indicating whether two <see cref="T:System.Runtime.InteropServices.GCHandle" /> objects are equal.</summary>
		/// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters are equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="a" /> parameter.  </param>
		// Token: 0x060055EB RID: 21995 RVA: 0x00129515 File Offset: 0x00127715
		public static bool operator ==(GCHandle a, GCHandle b)
		{
			return a.handle == b.handle;
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Runtime.InteropServices.GCHandle" /> objects are not equal.</summary>
		/// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters are not equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="a" /> parameter.  </param>
		// Token: 0x060055EC RID: 21996 RVA: 0x00129525 File Offset: 0x00127725
		public static bool operator !=(GCHandle a, GCHandle b)
		{
			return !(a == b);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Runtime.InteropServices.GCHandle" /> object is equal to the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <returns>true if the specified <see cref="T:System.Runtime.InteropServices.GCHandle" /> object is equal to the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</param>
		// Token: 0x060055ED RID: 21997 RVA: 0x00129531 File Offset: 0x00127731
		public override bool Equals(object o)
		{
			return o is GCHandle && this == (GCHandle)o;
		}

		/// <summary>Returns an identifier for the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <returns>An identifier for the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</returns>
		// Token: 0x060055EE RID: 21998 RVA: 0x0012954E File Offset: 0x0012774E
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		/// <summary>Returns a new <see cref="T:System.Runtime.InteropServices.GCHandle" /> object created from a handle to a managed object.</summary>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> object that corresponds to the value parameter.  </returns>
		/// <param name="value">An <see cref="T:System.IntPtr" /> handle to a managed object to create a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object from.</param>
		/// <exception cref="T:System.InvalidOperationException">The value of the <paramref name="value" /> parameter is <see cref="F:System.IntPtr.Zero" />.</exception>
		// Token: 0x060055EF RID: 21999 RVA: 0x0012955B File Offset: 0x0012775B
		public static GCHandle FromIntPtr(IntPtr value)
		{
			return (GCHandle)value;
		}

		/// <summary>Returns the internal integer representation of a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> object that represents a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object. </returns>
		/// <param name="value">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to retrieve an internal integer representation from.</param>
		// Token: 0x060055F0 RID: 22000 RVA: 0x00129563 File Offset: 0x00127763
		public static IntPtr ToIntPtr(GCHandle value)
		{
			return (IntPtr)value;
		}

		// Token: 0x04002D9D RID: 11677
		private int handle;
	}
}
