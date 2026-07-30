using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	/// <summary>Provides <see cref="T:Microsoft.Win32.RegistryKey" /> objects that represent the root keys in the Windows registry, and static methods to access key/value pairs.</summary>
	// Token: 0x020000AD RID: 173
	[ComVisible(true)]
	public static class Registry
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x0001F154 File Offset: 0x0001D354
		private static RegistryKey ToKey(string keyName, bool setting)
		{
			if (keyName == null)
			{
				throw new ArgumentException("Not a valid registry key name", "keyName");
			}
			string[] array = keyName.Split(new char[] { '\\' });
			string text = array[0];
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			RegistryKey registryKey;
			if (num <= 1097425318U)
			{
				if (num != 126972219U)
				{
					if (num != 457190004U)
					{
						if (num == 1097425318U)
						{
							if (text == "HKEY_CLASSES_ROOT")
							{
								registryKey = Registry.ClassesRoot;
								goto IL_0146;
							}
						}
					}
					else if (text == "HKEY_LOCAL_MACHINE")
					{
						registryKey = Registry.LocalMachine;
						goto IL_0146;
					}
				}
				else if (text == "HKEY_CURRENT_CONFIG")
				{
					registryKey = Registry.CurrentConfig;
					goto IL_0146;
				}
			}
			else if (num <= 1568329430U)
			{
				if (num != 1198714601U)
				{
					if (num == 1568329430U)
					{
						if (text == "HKEY_CURRENT_USER")
						{
							registryKey = Registry.CurrentUser;
							goto IL_0146;
						}
					}
				}
				else if (text == "HKEY_USERS")
				{
					registryKey = Registry.Users;
					goto IL_0146;
				}
			}
			else if (num != 2823865611U)
			{
				if (num == 3554990456U)
				{
					if (text == "HKEY_PERFORMANCE_DATA")
					{
						registryKey = Registry.PerformanceData;
						goto IL_0146;
					}
				}
			}
			else if (text == "HKEY_DYN_DATA")
			{
				registryKey = Registry.DynData;
				goto IL_0146;
			}
			throw new ArgumentException("Keyname does not start with a valid registry root", "keyName");
			IL_0146:
			for (int i = 1; i < array.Length; i++)
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey(array[i], setting);
				if (registryKey2 == null)
				{
					if (!setting)
					{
						return null;
					}
					registryKey2 = registryKey.CreateSubKey(array[i]);
				}
				registryKey = registryKey2;
			}
			return registryKey;
		}

		/// <summary>Sets the specified name/value pair on the specified registry key. If the specified key does not exist, it is created.</summary>
		/// <param name="keyName">The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".</param>
		/// <param name="valueName">The name of the name/value pair.</param>
		/// <param name="value">The value to be stored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keyName" /> does not begin with a valid registry root. -or-<paramref name="keyName" /> is longer than the maximum length allowed (255 characters).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and thus cannot be written to; for example, it is a root-level node. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000583 RID: 1411 RVA: 0x0001F2DF File Offset: 0x0001D4DF
		public static void SetValue(string keyName, string valueName, object value)
		{
			RegistryKey registryKey = Registry.ToKey(keyName, true);
			if (valueName.Length > 255)
			{
				throw new ArgumentException("valueName is larger than 255 characters", "valueName");
			}
			if (registryKey == null)
			{
				throw new ArgumentException("cant locate that keyName", "keyName");
			}
			registryKey.SetValue(valueName, value);
		}

		/// <summary>Sets the name/value pair on the specified registry key, using the specified registry data type. If the specified key does not exist, it is created.</summary>
		/// <param name="keyName">The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".</param>
		/// <param name="valueName">The name of the name/value pair.</param>
		/// <param name="value">The value to be stored.</param>
		/// <param name="valueKind">The registry data type to use when storing the data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keyName" /> does not begin with a valid registry root.-or-<paramref name="keyName" /> is longer than the maximum length allowed (255 characters).-or- The type of <paramref name="value" /> did not match the registry data type specified by <paramref name="valueKind" />, therefore the data could not be converted properly. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and thus cannot be written to; for example, it is a root-level node, or the key has not been opened with write access. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000584 RID: 1412 RVA: 0x0001F320 File Offset: 0x0001D520
		public static void SetValue(string keyName, string valueName, object value, RegistryValueKind valueKind)
		{
			RegistryKey registryKey = Registry.ToKey(keyName, true);
			if (valueName.Length > 255)
			{
				throw new ArgumentException("valueName is larger than 255 characters", "valueName");
			}
			if (registryKey == null)
			{
				throw new ArgumentException("cant locate that keyName", "keyName");
			}
			registryKey.SetValue(valueName, value, valueKind);
		}

		/// <summary>Retrieves the value associated with the specified name, in the specified registry key. If the name is not found in the specified key, returns a default value that you provide, or null if the specified key does not exist. </summary>
		/// <returns>null if the subkey specified by <paramref name="keyName" /> does not exist; otherwise, the value associated with <paramref name="valueName" />, or <paramref name="defaultValue" /> if <paramref name="valueName" /> is not found.</returns>
		/// <param name="keyName">The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".</param>
		/// <param name="valueName">The name of the name/value pair.</param>
		/// <param name="defaultValue">The value to return if <paramref name="valueName" /> does not exist.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keyName" /> does not begin with a valid registry root. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x06000585 RID: 1413 RVA: 0x0001F36C File Offset: 0x0001D56C
		public static object GetValue(string keyName, string valueName, object defaultValue)
		{
			RegistryKey registryKey = Registry.ToKey(keyName, false);
			if (registryKey == null)
			{
				return defaultValue;
			}
			return registryKey.GetValue(valueName, defaultValue);
		}

		/// <summary>Defines the types (or classes) of documents and the properties associated with those types. This field reads the Windows registry base key HKEY_CLASSES_ROOT.</summary>
		// Token: 0x0400060E RID: 1550
		public static readonly RegistryKey ClassesRoot = new RegistryKey(RegistryHive.ClassesRoot);

		/// <summary>Contains configuration information pertaining to the hardware that is not specific to the user. This field reads the Windows registry base key HKEY_CURRENT_CONFIG.</summary>
		// Token: 0x0400060F RID: 1551
		public static readonly RegistryKey CurrentConfig = new RegistryKey(RegistryHive.CurrentConfig);

		/// <summary>Contains information about the current user preferences. This field reads the Windows registry base key HKEY_CURRENT_USER </summary>
		// Token: 0x04000610 RID: 1552
		public static readonly RegistryKey CurrentUser = new RegistryKey(RegistryHive.CurrentUser);

		/// <summary>Contains dynamic registry data. This field reads the Windows registry base key HKEY_DYN_DATA.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The operating system does not support dynamic data; that is, it is not Windows 98, Windows 98 Second Edition, or Windows Millennium Edition (Windows Me).</exception>
		// Token: 0x04000611 RID: 1553
		[Obsolete("Use PerformanceData instead")]
		public static readonly RegistryKey DynData = new RegistryKey(RegistryHive.DynData);

		/// <summary>Contains the configuration data for the local machine. This field reads the Windows registry base key HKEY_LOCAL_MACHINE.</summary>
		// Token: 0x04000612 RID: 1554
		public static readonly RegistryKey LocalMachine = new RegistryKey(RegistryHive.LocalMachine);

		/// <summary>Contains performance information for software components. This field reads the Windows registry base key HKEY_PERFORMANCE_DATA.</summary>
		// Token: 0x04000613 RID: 1555
		public static readonly RegistryKey PerformanceData = new RegistryKey(RegistryHive.PerformanceData);

		/// <summary>Contains information about the default user configuration. This field reads the Windows registry base key HKEY_USERS.</summary>
		// Token: 0x04000614 RID: 1556
		public static readonly RegistryKey Users = new RegistryKey(RegistryHive.Users);
	}
}
