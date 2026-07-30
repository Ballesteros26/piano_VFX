using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace Microsoft.Win32
{
	/// <summary>Represents a key-level node in the Windows registry. This class is a registry encapsulation.</summary>
	// Token: 0x020000AF RID: 175
	[ComVisible(true)]
	public sealed class RegistryKey : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x0001F406 File Offset: 0x0001D606
		static RegistryKey()
		{
			if (Path.DirectorySeparatorChar == '\\')
			{
				RegistryKey.RegistryApi = new Win32RegistryApi();
				return;
			}
			RegistryKey.RegistryApi = new UnixRegistryApi();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001F426 File Offset: 0x0001D626
		internal RegistryKey(RegistryHive hiveId)
			: this(hiveId, new IntPtr((int)hiveId), false)
		{
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001F436 File Offset: 0x0001D636
		internal RegistryKey(RegistryHive hiveId, IntPtr keyHandle, bool remoteRoot)
		{
			this.hive = hiveId;
			this.handle = keyHandle;
			this.qname = RegistryKey.GetHiveName(hiveId);
			this.isRemoteRoot = remoteRoot;
			this.isWritable = true;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001F470 File Offset: 0x0001D670
		internal RegistryKey(object data, string keyName, bool writable)
		{
			this.handle = data;
			this.qname = keyName;
			this.isWritable = writable;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001F490 File Offset: 0x0001D690
		internal static bool IsEquals(RegistryKey a, RegistryKey b)
		{
			return a.hive == b.hive && a.handle == b.handle && a.qname == b.qname && a.isRemoteRoot == b.isRemoteRoot && a.isWritable == b.isWritable;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:Microsoft.Win32.RegistryKey" /> class.</summary>
		// Token: 0x0600058C RID: 1420 RVA: 0x0001F4EA File Offset: 0x0001D6EA
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Close();
		}

		/// <summary>Retrieves the name of the key.</summary>
		/// <returns>The absolute (qualified) name of the key.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed). </exception>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001F4F8 File Offset: 0x0001D6F8
		public string Name
		{
			get
			{
				return this.qname;
			}
		}

		/// <summary>Writes all the attributes of the specified open registry key into the registry.</summary>
		// Token: 0x0600058E RID: 1422 RVA: 0x0001F500 File Offset: 0x0001D700
		public void Flush()
		{
			RegistryKey.RegistryApi.Flush(this);
		}

		/// <summary>Closes the key and flushes it to disk if its contents have been modified.</summary>
		// Token: 0x0600058F RID: 1423 RVA: 0x0001F50D File Offset: 0x0001D70D
		public void Close()
		{
			this.Flush();
			if (!this.isRemoteRoot && this.IsRoot)
			{
				return;
			}
			RegistryKey.RegistryApi.Close(this);
			this.handle = null;
			this.safe_handle = null;
		}

		/// <summary>Retrieves the count of subkeys of the current key.</summary>
		/// <returns>The number of subkeys of the current key.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have read permission for the key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001F53F File Offset: 0x0001D73F
		public int SubKeyCount
		{
			get
			{
				this.AssertKeyStillValid();
				return RegistryKey.RegistryApi.SubKeyCount(this);
			}
		}

		/// <summary>Retrieves the count of values in the key.</summary>
		/// <returns>The number of name/value pairs in the key.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have read permission for the key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001F552 File Offset: 0x0001D752
		public int ValueCount
		{
			get
			{
				this.AssertKeyStillValid();
				return RegistryKey.RegistryApi.ValueCount(this);
			}
		}

		/// <summary>Gets a <see cref="T:Microsoft.Win32.SafeHandles.SafeRegistryHandle" /> object that represents the registry key that the current <see cref="T:Microsoft.Win32.RegistryKey" /> object encapsulates.</summary>
		/// <returns>The handle to the registry key.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The registry key is closed. Closed keys cannot be accessed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.IO.IOException">A system error occurred, such as deletion of the current key.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read the key.</exception>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001F568 File Offset: 0x0001D768
		[ComVisible(false)]
		[MonoTODO("Not implemented in Unix")]
		public SafeRegistryHandle Handle
		{
			get
			{
				this.AssertKeyStillValid();
				if (this.safe_handle == null)
				{
					IntPtr intPtr = RegistryKey.RegistryApi.GetHandle(this);
					this.safe_handle = new SafeRegistryHandle(intPtr, true);
				}
				return this.safe_handle;
			}
		}

		/// <summary>Gets the view that was used to create the registry key. </summary>
		/// <returns>The view that was used to create the registry key.-or-<see cref="F:Microsoft.Win32.RegistryView.Default" />, if no view was used.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00015ED5 File Offset: 0x000140D5
		[ComVisible(false)]
		[MonoLimitation("View is ignored in Mono.")]
		public RegistryView View
		{
			get
			{
				return RegistryView.Default;
			}
		}

		/// <summary>Sets the specified name/value pair.</summary>
		/// <param name="name">The name of the value to store. </param>
		/// <param name="value">The data to be stored. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is an unsupported data type. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and cannot be written to; for example, the key has not been opened with write access. -or-The <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a root-level node, and the operating system is Windows Millennium Edition or Windows 98.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a root-level node, and the operating system is Windows 2000, Windows XP, or Windows Server 2003.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000594 RID: 1428 RVA: 0x0001F5A2 File Offset: 0x0001D7A2
		public void SetValue(string name, object value)
		{
			this.AssertKeyStillValid();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name != null)
			{
				this.AssertKeyNameLength(name);
			}
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey.RegistryApi.SetValue(this, name, value);
		}

		/// <summary>Sets the value of a name/value pair in the registry key, using the specified registry data type.</summary>
		/// <param name="name">The name of the value to be stored. </param>
		/// <param name="value">The data to be stored. </param>
		/// <param name="valueKind">The registry data type to use when storing the data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The type of <paramref name="value" /> did not match the registry data type specified by <paramref name="valueKind" />, therefore the data could not be converted properly. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and cannot be written to; for example, the key has not been opened with write access.-or-The <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a root-level node, and the operating system is Windows Millennium Edition or Windows 98. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a root-level node, and the operating system is Windows 2000, Windows XP, or Windows Server 2003.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000595 RID: 1429 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		[ComVisible(false)]
		public void SetValue(string name, object value, RegistryValueKind valueKind)
		{
			this.AssertKeyStillValid();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name != null)
			{
				this.AssertKeyNameLength(name);
			}
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey.RegistryApi.SetValue(this, name, value, valueKind);
		}

		/// <summary>Retrieves a subkey as read-only.</summary>
		/// <returns>The subkey requested, or null if the operation failed.</returns>
		/// <param name="name">The name or path of the subkey to open as read-only. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read the registry key. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06000596 RID: 1430 RVA: 0x0001F630 File Offset: 0x0001D830
		public RegistryKey OpenSubKey(string name)
		{
			return this.OpenSubKey(name, false);
		}

		/// <summary>Retrieves a specified subkey, and specifies whether write access is to be applied to the key. </summary>
		/// <returns>The subkey requested, or null if the operation failed.</returns>
		/// <param name="name">Name or path of the subkey to open. </param>
		/// <param name="writable">Set to true if you need write access to the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to access the registry key in the specified mode. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000597 RID: 1431 RVA: 0x0001F63A File Offset: 0x0001D83A
		public RegistryKey OpenSubKey(string name, bool writable)
		{
			this.AssertKeyStillValid();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.AssertKeyNameLength(name);
			return RegistryKey.RegistryApi.OpenSubKey(this, name, writable);
		}

		/// <summary>Retrieves the value associated with the specified name. Returns null if the name/value pair does not exist in the registry.</summary>
		/// <returns>The value associated with <paramref name="name" />, or null if <paramref name="name" /> is not found.</returns>
		/// <param name="name">The name of the value to retrieve. This string is not case-sensitive.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06000598 RID: 1432 RVA: 0x0001F664 File Offset: 0x0001D864
		public object GetValue(string name)
		{
			return this.GetValue(name, null);
		}

		/// <summary>Retrieves the value associated with the specified name. If the name is not found, returns the default value that you provide.</summary>
		/// <returns>The value associated with <paramref name="name" />, with any embedded environment variables left unexpanded, or <paramref name="defaultValue" /> if <paramref name="name" /> is not found.</returns>
		/// <param name="name">The name of the value to retrieve. This string is not case-sensitive.</param>
		/// <param name="defaultValue">The value to return if <paramref name="name" /> does not exist. </param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06000599 RID: 1433 RVA: 0x0001F66E File Offset: 0x0001D86E
		public object GetValue(string name, object defaultValue)
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetValue(this, name, defaultValue, RegistryValueOptions.None);
		}

		/// <summary>Retrieves the value associated with the specified name and retrieval options. If the name is not found, returns the default value that you provide.</summary>
		/// <returns>The value associated with <paramref name="name" />, processed according to the specified <paramref name="options" />, or <paramref name="defaultValue" /> if <paramref name="name" /> is not found.</returns>
		/// <param name="name">The name of the value to retrieve. This string is not case-sensitive.</param>
		/// <param name="defaultValue">The value to return if <paramref name="name" /> does not exist. </param>
		/// <param name="options">One of the enumeration values that specifies optional processing of the retrieved value.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:Microsoft.Win32.RegistryValueOptions" /> value; for example, an invalid value is cast to <see cref="T:Microsoft.Win32.RegistryValueOptions" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x0600059A RID: 1434 RVA: 0x0001F684 File Offset: 0x0001D884
		[ComVisible(false)]
		public object GetValue(string name, object defaultValue, RegistryValueOptions options)
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetValue(this, name, defaultValue, options);
		}

		/// <summary>Retrieves the registry data type of the value associated with the specified name.</summary>
		/// <returns>The registry data type of the value associated with <paramref name="name" />.</returns>
		/// <param name="name">The name of the value whose registry data type is to be retrieved. This string is not case-sensitive.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.IO.IOException">The subkey that contains the specified value does not exist.-or-The name/value pair specified by <paramref name="name" /> does not exist.This exception is not thrown on Windows 95, Windows 98, or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x0600059B RID: 1435 RVA: 0x0001F69A File Offset: 0x0001D89A
		[ComVisible(false)]
		public RegistryValueKind GetValueKind(string name)
		{
			return RegistryKey.RegistryApi.GetValueKind(this, name);
		}

		/// <summary>Creates a new subkey or opens an existing subkey for write access.  </summary>
		/// <returns>The newly created subkey, or null if the operation failed. If a zero-length string is specified for <paramref name="subkey" />, the current <see cref="T:Microsoft.Win32.RegistryKey" /> object is returned.</returns>
		/// <param name="subkey">The name or path of the subkey to create or open. This string is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or open the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> on which this method is being invoked is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> cannot be written to; for example, it was not opened as a writable key , or the user does not have the necessary access rights. </exception>
		/// <exception cref="T:System.IO.IOException">The nesting level exceeds 510.-or-A system error occurred, such as deletion of the key, or an attempt to create a key in the <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> root.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600059C RID: 1436 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public RegistryKey CreateSubKey(string subkey)
		{
			this.AssertKeyStillValid();
			this.AssertKeyNameNotNull(subkey);
			this.AssertKeyNameLength(subkey);
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			return RegistryKey.RegistryApi.CreateSubKey(this, subkey);
		}

		/// <summary>Creates a new subkey or opens an existing subkey for write access, using the specified permission check option. </summary>
		/// <returns>The newly created subkey, or null if the operation failed. If a zero-length string is specified for <paramref name="subkey" />, the current <see cref="T:Microsoft.Win32.RegistryKey" /> object is returned.</returns>
		/// <param name="subkey">The name or path of the subkey to create or open. This string is not case-sensitive.</param>
		/// <param name="permissionCheck">One of the enumeration values that specifies whether the key is opened for read or read/write access.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or open the registry key. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionCheck" /> contains an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> on which this method is being invoked is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> cannot be written to; for example, it was not opened as a writable key, or the user does not have the necessary access rights. </exception>
		/// <exception cref="T:System.IO.IOException">The nesting level exceeds 510.-or-A system error occurred, such as deletion of the key, or an attempt to create a key in the <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> root.</exception>
		// Token: 0x0600059D RID: 1437 RVA: 0x0001F6DD File Offset: 0x0001D8DD
		[ComVisible(false)]
		[MonoLimitation("permissionCheck is ignored in Mono")]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
		{
			return this.CreateSubKey(subkey);
		}

		/// <summary>Creates a new subkey or opens an existing subkey for write access, using the specified permission check option and registry security. </summary>
		/// <returns>The newly created subkey, or null if the operation failed. If a zero-length string is specified for <paramref name="subkey" />, the current <see cref="T:Microsoft.Win32.RegistryKey" /> object is returned.</returns>
		/// <param name="subkey">The name or path of the subkey to create or open. This string is not case-sensitive.</param>
		/// <param name="permissionCheck">One of the enumeration values that specifies whether the key is opened for read or read/write access.</param>
		/// <param name="registrySecurity">The access control security for the new key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or open the registry key. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionCheck" /> contains an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> on which this method is being invoked is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:Microsoft.Win32.RegistryKey" /> cannot be written to; for example, it was not opened as a writable key, or the user does not have the necessary access rights.</exception>
		/// <exception cref="T:System.IO.IOException">The nesting level exceeds 510.-or-A system error occurred, such as deletion of the key, or an attempt to create a key in the <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> root.</exception>
		// Token: 0x0600059E RID: 1438 RVA: 0x0001F6DD File Offset: 0x0001D8DD
		[ComVisible(false)]
		[MonoLimitation("permissionCheck and registrySecurity are ignored in Mono")]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistrySecurity registrySecurity)
		{
			return this.CreateSubKey(subkey);
		}

		/// <summary>Creates a subkey or opens a subkey for write access, using the specified permission check and registry options. </summary>
		/// <returns>The newly created subkey, or null if the operation failed.</returns>
		/// <param name="subkey">The name or path of the subkey to create or open. </param>
		/// <param name="permissionCheck">One of the enumeration values that specifies whether the key is opened for read or read/write access.</param>
		/// <param name="options">The registry option to use; for example, that creates a volatile key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey " />is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:Microsoft.Win32.RegistryKey" /> object is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:Microsoft.Win32.RegistryKey" /> object cannot be written to; for example, it was not opened as a writable key, or the user does not have the required access rights.</exception>
		/// <exception cref="T:System.IO.IOException">The nesting level exceeds 510.-or-A system error occurred, such as deletion of the key or an attempt to create a key in the <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> root. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or open the registry key.</exception>
		// Token: 0x0600059F RID: 1439 RVA: 0x0001F6E6 File Offset: 0x0001D8E6
		[ComVisible(false)]
		[MonoLimitation("permissionCheck is ignored in Mono")]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
		{
			this.AssertKeyStillValid();
			this.AssertKeyNameNotNull(subkey);
			this.AssertKeyNameLength(subkey);
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			return RegistryKey.RegistryApi.CreateSubKey(this, subkey, options);
		}

		/// <summary>Creates a subkey or opens a subkey for write access, using the specified permission check option, registry option, and registry security.</summary>
		/// <returns>The newly created subkey, or null if the operation failed.  </returns>
		/// <param name="subkey">The name or path of the subkey to create or open.</param>
		/// <param name="permissionCheck">One of the enumeration values that specifies whether the key is opened for read or read/write access.</param>
		/// <param name="registryOptions">The registry option to use.</param>
		/// <param name="registrySecurity">The access control security for the new subkey. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey " />is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:Microsoft.Win32.RegistryKey" /> object is closed. Closed keys cannot be accessed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:Microsoft.Win32.RegistryKey" /> object cannot be written to; for example, it was not opened as a writable key, or the user does not have the required access rights.</exception>
		/// <exception cref="T:System.IO.IOException">The nesting level exceeds 510.-or-A system error occurred, such as deletion of the key or an attempt to create a key in the <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> root. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or open the registry key.</exception>
		// Token: 0x060005A0 RID: 1440 RVA: 0x0001F71C File Offset: 0x0001D91C
		[ComVisible(false)]
		[MonoLimitation("permissionCheck and registrySecurity are ignored in Mono")]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions registryOptions, RegistrySecurity registrySecurity)
		{
			return this.CreateSubKey(subkey, permissionCheck, registryOptions);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001F727 File Offset: 0x0001D927
		[ComVisible(false)]
		public RegistryKey CreateSubKey(string subkey, bool writable)
		{
			return this.CreateSubKey(subkey, writable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001F737 File Offset: 0x0001D937
		[ComVisible(false)]
		public RegistryKey CreateSubKey(string subkey, bool writable, RegistryOptions options)
		{
			return this.CreateSubKey(subkey, writable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree, options);
		}

		/// <summary>Deletes the specified subkey. </summary>
		/// <param name="subkey">The name of the subkey to delete. This string is not case-sensitive.</param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="subkey" /> has child subkeys </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="subkey" /> parameter does not specify a valid registry key </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to delete the key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060005A3 RID: 1443 RVA: 0x0001F748 File Offset: 0x0001D948
		public void DeleteSubKey(string subkey)
		{
			this.DeleteSubKey(subkey, true);
		}

		/// <summary>Deletes the specified subkey, and specifies whether an exception is raised if the subkey is not found. </summary>
		/// <param name="subkey">The name of the subkey to delete. This string is not case-sensitive.</param>
		/// <param name="throwOnMissingSubKey">Indicates whether an exception should be raised if the specified subkey cannot be found. If this argument is true and the specified subkey does not exist, an exception is raised. If this argument is false and the specified subkey does not exist, no action is taken. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="subkey" /> has child subkeys. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subkey" /> does not specify a valid registry key, and <paramref name="throwOnMissingSubKey" /> is true. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to delete the key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001F754 File Offset: 0x0001D954
		public void DeleteSubKey(string subkey, bool throwOnMissingSubKey)
		{
			this.AssertKeyStillValid();
			this.AssertKeyNameNotNull(subkey);
			this.AssertKeyNameLength(subkey);
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey registryKey = this.OpenSubKey(subkey);
			if (registryKey == null)
			{
				if (throwOnMissingSubKey)
				{
					throw new ArgumentException("Cannot delete a subkey tree because the subkey does not exist.");
				}
				return;
			}
			else
			{
				if (registryKey.SubKeyCount > 0)
				{
					throw new InvalidOperationException("Registry key has subkeys and recursive removes are not supported by this method.");
				}
				registryKey.Close();
				RegistryKey.RegistryApi.DeleteKey(this, subkey, throwOnMissingSubKey);
				return;
			}
		}

		/// <summary>Deletes a subkey and any child subkeys recursively. </summary>
		/// <param name="subkey">The subkey to delete. This string is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">Deletion of a root hive is attempted.-or-<paramref name="subkey" /> does not specify a valid registry subkey. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to delete the key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060005A5 RID: 1445 RVA: 0x0001F7C9 File Offset: 0x0001D9C9
		public void DeleteSubKeyTree(string subkey)
		{
			this.DeleteSubKeyTree(subkey, true);
		}

		/// <summary>Deletes the specified subkey and any child subkeys recursively, and specifies whether an exception is raised if the subkey is not found. </summary>
		/// <param name="subkey">The name of the subkey to delete. This string is not case-sensitive.</param>
		/// <param name="throwOnMissingSubKey">Indicates whether an exception should be raised if the specified subkey cannot be found. If this argument is true and the specified subkey does not exist, an exception is raised. If this argument is false and the specified subkey does not exist, no action is taken.</param>
		/// <exception cref="T:System.ArgumentException">An attempt was made to delete the root hive of the tree.-or-<paramref name="subkey" /> does not specify a valid registry subkey, and <paramref name="throwOnMissingSubKey" /> is true.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="subkey" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to delete the key.</exception>
		// Token: 0x060005A6 RID: 1446 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
		public void DeleteSubKeyTree(string subkey, bool throwOnMissingSubKey)
		{
			this.AssertKeyStillValid();
			this.AssertKeyNameNotNull(subkey);
			this.AssertKeyNameLength(subkey);
			RegistryKey registryKey = this.OpenSubKey(subkey, true);
			if (registryKey != null)
			{
				registryKey.DeleteChildKeysAndValues();
				registryKey.Close();
				this.DeleteSubKey(subkey, false);
				return;
			}
			if (!throwOnMissingSubKey)
			{
				return;
			}
			throw new ArgumentException("Cannot delete a subkey tree because the subkey does not exist.");
		}

		/// <summary>Deletes the specified value from this key.</summary>
		/// <param name="name">The name of the value to delete. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid reference to a value. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to delete the value. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is read-only. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060005A7 RID: 1447 RVA: 0x0001F824 File Offset: 0x0001DA24
		public void DeleteValue(string name)
		{
			this.DeleteValue(name, true);
		}

		/// <summary>Deletes the specified value from this key, and specifies whether an exception is raised if the value is not found.</summary>
		/// <param name="name">The name of the value to delete. </param>
		/// <param name="throwOnMissingValue">Indicates whether an exception should be raised if the specified value cannot be found. If this argument is true and the specified value does not exist, an exception is raised. If this argument is false and the specified value does not exist, no action is taken. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid reference to a value and <paramref name="throwOnMissingValue" /> is true. -or- <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to delete the value. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is read-only. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060005A8 RID: 1448 RVA: 0x0001F82E File Offset: 0x0001DA2E
		public void DeleteValue(string name, bool throwOnMissingValue)
		{
			this.AssertKeyStillValid();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey.RegistryApi.DeleteValue(this, name, throwOnMissingValue);
		}

		/// <summary>Returns the access control security for the current registry key.</summary>
		/// <returns>An object that describes the access control permissions on the registry key represented by the current <see cref="T:Microsoft.Win32.RegistryKey" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the necessary permissions.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed).</exception>
		/// <exception cref="T:System.InvalidOperationException">The current key has been deleted.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005A9 RID: 1449 RVA: 0x0001F864 File Offset: 0x0001DA64
		public RegistrySecurity GetAccessControl()
		{
			return this.GetAccessControl(AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Returns the specified sections of the access control security for the current registry key.</summary>
		/// <returns>An object that describes the access control permissions on the registry key represented by the current <see cref="T:Microsoft.Win32.RegistryKey" />.</returns>
		/// <param name="includeSections">A bitwise combination of enumeration values that specifies the type of security information to get. </param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the necessary permissions.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed).</exception>
		/// <exception cref="T:System.InvalidOperationException">The current key has been deleted.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005AA RID: 1450 RVA: 0x0001F86E File Offset: 0x0001DA6E
		public RegistrySecurity GetAccessControl(AccessControlSections includeSections)
		{
			return new RegistrySecurity(this.Name, includeSections);
		}

		/// <summary>Retrieves an array of strings that contains all the subkey names.</summary>
		/// <returns>An array of strings that contains the names of the subkeys for the current key.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005AB RID: 1451 RVA: 0x0001F87C File Offset: 0x0001DA7C
		public string[] GetSubKeyNames()
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetSubKeyNames(this);
		}

		/// <summary>Retrieves an array of strings that contains all the value names associated with this key.</summary>
		/// <returns>An array of strings that contains the value names for the current key.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" />  being manipulated is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.IO.IOException">A system error occurred; for example, the current key has been deleted.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005AC RID: 1452 RVA: 0x0001F88F File Offset: 0x0001DA8F
		public string[] GetValueNames()
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetValueNames(this);
		}

		/// <summary>Creates a registry key from a specified handle.</summary>
		/// <returns>A registry key.</returns>
		/// <param name="handle">The handle to the registry key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handle" /> is null.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to perform this action.</exception>
		// Token: 0x060005AD RID: 1453 RVA: 0x0001F8A2 File Offset: 0x0001DAA2
		[ComVisible(false)]
		[MonoTODO("Not implemented on unix")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static RegistryKey FromHandle(SafeRegistryHandle handle)
		{
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			return RegistryKey.RegistryApi.FromHandle(handle);
		}

		/// <summary>Creates a registry key from a specified handle and registry view setting. </summary>
		/// <returns>A registry key.</returns>
		/// <param name="handle">The handle to the registry key.</param>
		/// <param name="view">The registry view to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="view" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handle" /> is null.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to perform this action.</exception>
		// Token: 0x060005AE RID: 1454 RVA: 0x0001F8BD File Offset: 0x0001DABD
		[MonoTODO("Not implemented on unix")]
		[ComVisible(false)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static RegistryKey FromHandle(SafeRegistryHandle handle, RegistryView view)
		{
			return RegistryKey.FromHandle(handle);
		}

		/// <summary>Opens a new <see cref="T:Microsoft.Win32.RegistryKey" /> that represents the requested key on a remote machine.</summary>
		/// <returns>The requested registry key.</returns>
		/// <param name="hKey">The HKEY to open, from the <see cref="T:Microsoft.Win32.RegistryHive" /> enumeration. </param>
		/// <param name="machineName">The remote machine. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hKey" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="machineName" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="machineName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the proper permissions to perform this operation. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060005AF RID: 1455 RVA: 0x0001F8C5 File Offset: 0x0001DAC5
		[MonoTODO("Not implemented on unix")]
		public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			return RegistryKey.RegistryApi.OpenRemoteBaseKey(hKey, machineName);
		}

		/// <summary>Opens a new registry key that represents the requested key on a remote machine with the specified view.</summary>
		/// <returns>The requested registry key.</returns>
		/// <param name="hKey">The HKEY to open from the <see cref="T:Microsoft.Win32.RegistryHive" /> enumeration.. </param>
		/// <param name="machineName">The remote machine.</param>
		/// <param name="view">The registry view to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hKey" /> or <paramref name="view" /> is invalid.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="machineName" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="machineName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="machineName" /> is null. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the required permissions to perform this operation.</exception>
		// Token: 0x060005B0 RID: 1456 RVA: 0x0001F8C5 File Offset: 0x0001DAC5
		[MonoTODO("Not implemented on unix")]
		[ComVisible(false)]
		public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName, RegistryView view)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			return RegistryKey.RegistryApi.OpenRemoteBaseKey(hKey, machineName);
		}

		/// <summary>Opens a new <see cref="T:Microsoft.Win32.RegistryKey" /> that represents the requested key on the local machine with the specified view.</summary>
		/// <returns>The requested registry key.</returns>
		/// <param name="hKey">The HKEY to open.</param>
		/// <param name="view">The registry view to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hKey" /> or <paramref name="view" /> is invalid.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to perform this action.</exception>
		// Token: 0x060005B1 RID: 1457 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
		[ComVisible(false)]
		[MonoLimitation("View is ignored in Mono")]
		public static RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
		{
			switch (hKey)
			{
			case RegistryHive.ClassesRoot:
				return Registry.ClassesRoot;
			case RegistryHive.CurrentUser:
				return Registry.CurrentUser;
			case RegistryHive.LocalMachine:
				return Registry.LocalMachine;
			case RegistryHive.Users:
				return Registry.Users;
			case RegistryHive.PerformanceData:
				return Registry.PerformanceData;
			case RegistryHive.CurrentConfig:
				return Registry.CurrentConfig;
			case RegistryHive.DynData:
				return Registry.DynData;
			default:
				throw new ArgumentException("hKey");
			}
		}

		/// <summary>Retrieves the specified subkey for read or read/write access.</summary>
		/// <returns>The subkey requested, or null if the operation failed.</returns>
		/// <param name="name">The name or path of the subkey to create or open.</param>
		/// <param name="permissionCheck">One of the enumeration values that specifies whether the key is opened for read or read/write access.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionCheck" /> contains an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read the registry key. </exception>
		// Token: 0x060005B2 RID: 1458 RVA: 0x0001F94F File Offset: 0x0001DB4F
		[ComVisible(false)]
		public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
		{
			return this.OpenSubKey(name, permissionCheck == RegistryKeyPermissionCheck.ReadWriteSubTree);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001F95C File Offset: 0x0001DB5C
		[ComVisible(false)]
		[MonoLimitation("rights are ignored in Mono")]
		public RegistryKey OpenSubKey(string name, RegistryRights rights)
		{
			return this.OpenSubKey(name);
		}

		/// <summary>Retrieves the specified subkey for read or read/write access, requesting the specified access rights.</summary>
		/// <returns>The subkey requested, or null if the operation failed.</returns>
		/// <param name="name">The name or path of the subkey to create or open.</param>
		/// <param name="permissionCheck">One of the enumeration values that specifies whether the key is opened for read or read/write access.</param>
		/// <param name="rights">A bitwise combination of enumeration values that specifies the desired security access.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionCheck" /> contains an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed). </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="rights" /> includes invalid registry rights values.-or-The user does not have the requested permissions. </exception>
		// Token: 0x060005B4 RID: 1460 RVA: 0x0001F94F File Offset: 0x0001DB4F
		[ComVisible(false)]
		[MonoLimitation("rights are ignored in Mono")]
		public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
		{
			return this.OpenSubKey(name, permissionCheck == RegistryKeyPermissionCheck.ReadWriteSubTree);
		}

		/// <summary>Applies Windows access control security to an existing registry key.</summary>
		/// <param name="registrySecurity">The access control security to apply to the current subkey. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:Microsoft.Win32.RegistryKey" /> object represents a key with access control security, and the caller does not have <see cref="F:System.Security.AccessControl.RegistryRights.ChangePermissions" /> rights.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="registrySecurity" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed).</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060005B5 RID: 1461 RVA: 0x0001F965 File Offset: 0x0001DB65
		public void SetAccessControl(RegistrySecurity registrySecurity)
		{
			if (registrySecurity == null)
			{
				throw new ArgumentNullException("registrySecurity");
			}
			registrySecurity.PersistModifications(this.Name);
		}

		/// <summary>Retrieves a string representation of this key.</summary>
		/// <returns>A string representing the key. If the specified key is invalid (cannot be found) then null is returned.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:Microsoft.Win32.RegistryKey" /> being accessed is closed (closed keys cannot be accessed). </exception>
		// Token: 0x060005B6 RID: 1462 RVA: 0x0001F981 File Offset: 0x0001DB81
		public override string ToString()
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.ToString(this);
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001F994 File Offset: 0x0001DB94
		internal bool IsRoot
		{
			get
			{
				return this.hive != null;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001F99F File Offset: 0x0001DB9F
		private bool IsWritable
		{
			get
			{
				return this.isWritable;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001F9A7 File Offset: 0x0001DBA7
		internal RegistryHive Hive
		{
			get
			{
				if (!this.IsRoot)
				{
					throw new NotSupportedException();
				}
				return (RegistryHive)this.hive;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001F9C2 File Offset: 0x0001DBC2
		internal object InternalHandle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001F9CA File Offset: 0x0001DBCA
		private void AssertKeyStillValid()
		{
			if (this.handle == null)
			{
				throw new ObjectDisposedException("Microsoft.Win32.RegistryKey");
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001F9DF File Offset: 0x0001DBDF
		private void AssertKeyNameNotNull(string subKeyName)
		{
			if (subKeyName == null)
			{
				throw new ArgumentNullException("name");
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001F9EF File Offset: 0x0001DBEF
		private void AssertKeyNameLength(string name)
		{
			if (name.Length > 255)
			{
				throw new ArgumentException("Name of registry key cannot be greater than 255 characters");
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001FA0C File Offset: 0x0001DC0C
		private void DeleteChildKeysAndValues()
		{
			if (this.IsRoot)
			{
				return;
			}
			foreach (string text in this.GetSubKeyNames())
			{
				RegistryKey registryKey = this.OpenSubKey(text, true);
				registryKey.DeleteChildKeysAndValues();
				registryKey.Close();
				this.DeleteSubKey(text, false);
			}
			foreach (string text2 in this.GetValueNames())
			{
				this.DeleteValue(text2, false);
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001FA78 File Offset: 0x0001DC78
		internal static string DecodeString(byte[] data)
		{
			string text = Encoding.Unicode.GetString(data);
			if (text.IndexOf('\0') != -1)
			{
				text = text.TrimEnd(new char[1]);
			}
			return text;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001FAA9 File Offset: 0x0001DCA9
		internal static IOException CreateMarkedForDeletionException()
		{
			throw new IOException("Illegal operation attempted on a registry key that has been marked for deletion.");
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
		private static string GetHiveName(RegistryHive hive)
		{
			switch (hive)
			{
			case RegistryHive.ClassesRoot:
				return "HKEY_CLASSES_ROOT";
			case RegistryHive.CurrentUser:
				return "HKEY_CURRENT_USER";
			case RegistryHive.LocalMachine:
				return "HKEY_LOCAL_MACHINE";
			case RegistryHive.Users:
				return "HKEY_USERS";
			case RegistryHive.PerformanceData:
				return "HKEY_PERFORMANCE_DATA";
			case RegistryHive.CurrentConfig:
				return "HKEY_CURRENT_CONFIG";
			case RegistryHive.DynData:
				return "HKEY_DYN_DATA";
			default:
				throw new NotImplementedException(string.Format("Registry hive '{0}' is not implemented.", hive.ToString()));
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal RegistryKey()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400061D RID: 1565
		private object handle;

		// Token: 0x0400061E RID: 1566
		private SafeRegistryHandle safe_handle;

		// Token: 0x0400061F RID: 1567
		private object hive;

		// Token: 0x04000620 RID: 1568
		private readonly string qname;

		// Token: 0x04000621 RID: 1569
		private readonly bool isRemoteRoot;

		// Token: 0x04000622 RID: 1570
		private readonly bool isWritable;

		// Token: 0x04000623 RID: 1571
		private static readonly IRegistryApi RegistryApi;
	}
}
