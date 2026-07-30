using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x020000AC RID: 172
	internal interface IRegistryApi
	{
		// Token: 0x0600056F RID: 1391
		RegistryKey CreateSubKey(RegistryKey rkey, string keyname);

		// Token: 0x06000570 RID: 1392
		RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName);

		// Token: 0x06000571 RID: 1393
		RegistryKey OpenSubKey(RegistryKey rkey, string keyname, bool writtable);

		// Token: 0x06000572 RID: 1394
		void Flush(RegistryKey rkey);

		// Token: 0x06000573 RID: 1395
		void Close(RegistryKey rkey);

		// Token: 0x06000574 RID: 1396
		object GetValue(RegistryKey rkey, string name, object default_value, RegistryValueOptions options);

		// Token: 0x06000575 RID: 1397
		RegistryValueKind GetValueKind(RegistryKey rkey, string name);

		// Token: 0x06000576 RID: 1398
		void SetValue(RegistryKey rkey, string name, object value);

		// Token: 0x06000577 RID: 1399
		int SubKeyCount(RegistryKey rkey);

		// Token: 0x06000578 RID: 1400
		int ValueCount(RegistryKey rkey);

		// Token: 0x06000579 RID: 1401
		void DeleteValue(RegistryKey rkey, string value, bool throw_if_missing);

		// Token: 0x0600057A RID: 1402
		void DeleteKey(RegistryKey rkey, string keyName, bool throw_if_missing);

		// Token: 0x0600057B RID: 1403
		string[] GetSubKeyNames(RegistryKey rkey);

		// Token: 0x0600057C RID: 1404
		string[] GetValueNames(RegistryKey rkey);

		// Token: 0x0600057D RID: 1405
		string ToString(RegistryKey rkey);

		// Token: 0x0600057E RID: 1406
		void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind);

		// Token: 0x0600057F RID: 1407
		RegistryKey CreateSubKey(RegistryKey rkey, string keyname, RegistryOptions options);

		// Token: 0x06000580 RID: 1408
		RegistryKey FromHandle(SafeRegistryHandle handle);

		// Token: 0x06000581 RID: 1409
		IntPtr GetHandle(RegistryKey key);
	}
}
