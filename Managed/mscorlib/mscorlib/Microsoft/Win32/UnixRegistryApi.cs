using System;
using System.Globalization;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x020000B8 RID: 184
	internal class UnixRegistryApi : IRegistryApi
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x000211F6 File Offset: 0x0001F3F6
		private static string ToUnix(string keyname)
		{
			if (keyname.IndexOf('\\') != -1)
			{
				keyname = keyname.Replace('\\', '/');
			}
			return keyname.ToLower();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00021215 File Offset: 0x0001F415
		private static bool IsWellKnownKey(string parentKeyName, string keyname)
		{
			return (parentKeyName == Registry.CurrentUser.Name || parentKeyName == Registry.LocalMachine.Name) && string.Compare("software", keyname, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00021251 File Offset: 0x0001F451
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyname)
		{
			return this.CreateSubKey(rkey, keyname, true);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002125C File Offset: 0x0001F45C
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyname, RegistryOptions options)
		{
			return this.CreateSubKey(rkey, keyname, true, options == RegistryOptions.Volatile);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0002126B File Offset: 0x0001F46B
		public RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00021274 File Offset: 0x0001F474
		public RegistryKey OpenSubKey(RegistryKey rkey, string keyname, bool writable)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return null;
			}
			RegistryKey registryKey = keyHandler.Probe(rkey, UnixRegistryApi.ToUnix(keyname), writable);
			if (registryKey == null && UnixRegistryApi.IsWellKnownKey(rkey.Name, keyname))
			{
				registryKey = this.CreateSubKey(rkey, keyname, writable);
			}
			return registryKey;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0002126B File Offset: 0x0001F46B
		public RegistryKey FromHandle(SafeRegistryHandle handle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000212BC File Offset: 0x0001F4BC
		public void Flush(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, false);
			if (keyHandler == null)
			{
				return;
			}
			keyHandler.Flush();
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000212DB File Offset: 0x0001F4DB
		public void Close(RegistryKey rkey)
		{
			KeyHandler.Drop(rkey);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000212E4 File Offset: 0x0001F4E4
		public object GetValue(RegistryKey rkey, string name, object default_value, RegistryValueOptions options)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return default_value;
			}
			if (keyHandler.ValueExists(name))
			{
				return keyHandler.GetValue(name, options);
			}
			return default_value;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00021312 File Offset: 0x0001F512
		public void SetValue(RegistryKey rkey, string name, object value)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			keyHandler.SetValue(name, value);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0002132B File Offset: 0x0001F52B
		public void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			keyHandler.SetValue(name, value, valueKind);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00021346 File Offset: 0x0001F546
		public int SubKeyCount(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.GetSubKeyCount();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0002135D File Offset: 0x0001F55D
		public int ValueCount(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.ValueCount;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00021374 File Offset: 0x0001F574
		public void DeleteValue(RegistryKey rkey, string name, bool throw_if_missing)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return;
			}
			if (throw_if_missing && !keyHandler.ValueExists(name))
			{
				throw new ArgumentException("the given value does not exist");
			}
			keyHandler.RemoveValue(name);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000213AC File Offset: 0x0001F5AC
		public void DeleteKey(RegistryKey rkey, string keyname, bool throw_if_missing)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				if (!throw_if_missing)
				{
					return;
				}
				throw new ArgumentException("the given value does not exist");
			}
			else
			{
				if (!KeyHandler.Delete(Path.Combine(keyHandler.Dir, UnixRegistryApi.ToUnix(keyname))) && throw_if_missing)
				{
					throw new ArgumentException("the given value does not exist");
				}
				return;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000213FB File Offset: 0x0001F5FB
		public string[] GetSubKeyNames(RegistryKey rkey)
		{
			return KeyHandler.Lookup(rkey, true).GetSubKeyNames();
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00021409 File Offset: 0x0001F609
		public string[] GetValueNames(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.GetValueNames();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00021420 File Offset: 0x0001F620
		public string ToString(RegistryKey rkey)
		{
			return rkey.Name;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00021428 File Offset: 0x0001F628
		private RegistryKey CreateSubKey(RegistryKey rkey, string keyname, bool writable)
		{
			return this.CreateSubKey(rkey, keyname, writable, false);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00021434 File Offset: 0x0001F634
		private RegistryKey CreateSubKey(RegistryKey rkey, string keyname, bool writable, bool is_volatile)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			if (KeyHandler.VolatileKeyExists(keyHandler.Dir) && !is_volatile)
			{
				throw new IOException("Cannot create a non volatile subkey under a volatile key.");
			}
			return keyHandler.Ensure(rkey, UnixRegistryApi.ToUnix(keyname), writable, is_volatile);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00021474 File Offset: 0x0001F674
		public RegistryValueKind GetValueKind(RegistryKey rkey, string name)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler != null)
			{
				return keyHandler.GetValueKind(name);
			}
			return RegistryValueKind.Unknown;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002126B File Offset: 0x0001F46B
		public IntPtr GetHandle(RegistryKey key)
		{
			throw new NotImplementedException();
		}
	}
}
