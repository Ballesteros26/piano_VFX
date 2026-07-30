using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x020000B9 RID: 185
	internal class Win32RegistryApi : IRegistryApi
	{
		// Token: 0x06000608 RID: 1544
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegCreateKeyEx(IntPtr keyBase, string keyName, int reserved, IntPtr lpClass, int options, int access, IntPtr securityAttrs, out IntPtr keyHandle, out int disposition);

		// Token: 0x06000609 RID: 1545
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegCloseKey(IntPtr keyHandle);

		// Token: 0x0600060A RID: 1546
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegConnectRegistry(string machineName, IntPtr hKey, out IntPtr keyHandle);

		// Token: 0x0600060B RID: 1547
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegFlushKey(IntPtr keyHandle);

		// Token: 0x0600060C RID: 1548
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegOpenKeyEx(IntPtr keyBase, string keyName, IntPtr reserved, int access, out IntPtr keyHandle);

		// Token: 0x0600060D RID: 1549
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegDeleteKey(IntPtr keyHandle, string valueName);

		// Token: 0x0600060E RID: 1550
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegDeleteValue(IntPtr keyHandle, string valueName);

		// Token: 0x0600060F RID: 1551
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegEnumKeyExW")]
		internal unsafe static extern int RegEnumKeyEx(IntPtr keyHandle, int dwIndex, char* lpName, ref int lpcbName, int[] lpReserved, [Out] StringBuilder lpClass, int[] lpcbClass, long[] lpftLastWriteTime);

		// Token: 0x06000610 RID: 1552
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern int RegEnumValue(IntPtr hKey, int dwIndex, char* lpValueName, ref int lpcbValueName, IntPtr lpReserved_MustBeZero, int[] lpType, byte[] lpData, int[] lpcbData);

		// Token: 0x06000611 RID: 1553
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, string data, int rawDataLength);

		// Token: 0x06000612 RID: 1554
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, byte[] rawData, int rawDataLength);

		// Token: 0x06000613 RID: 1555
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, ref int data, int rawDataLength);

		// Token: 0x06000614 RID: 1556
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, ref long data, int rawDataLength);

		// Token: 0x06000615 RID: 1557
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, IntPtr zero, ref int dataSize);

		// Token: 0x06000616 RID: 1558
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, [Out] byte[] data, ref int dataSize);

		// Token: 0x06000617 RID: 1559
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, ref int data, ref int dataSize);

		// Token: 0x06000618 RID: 1560
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, ref long data, ref int dataSize);

		// Token: 0x06000619 RID: 1561
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryInfoKeyW")]
		internal static extern int RegQueryInfoKey(IntPtr hKey, [Out] StringBuilder lpClass, int[] lpcbClass, IntPtr lpReserved_MustBeZero, ref int lpcSubKeys, int[] lpcbMaxSubKeyLen, int[] lpcbMaxClassLen, ref int lpcValues, int[] lpcbMaxValueNameLen, int[] lpcbMaxValueLen, int[] lpcbSecurityDescriptor, int[] lpftLastWriteTime);

		// Token: 0x0600061A RID: 1562 RVA: 0x00021495 File Offset: 0x0001F695
		public IntPtr GetHandle(RegistryKey key)
		{
			return (IntPtr)key.InternalHandle;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000214A2 File Offset: 0x0001F6A2
		private static bool IsHandleValid(RegistryKey key)
		{
			return key.InternalHandle != null;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000214B0 File Offset: 0x0001F6B0
		public RegistryValueKind GetValueKind(RegistryKey rkey, string name)
		{
			RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
			int num = 0;
			int num2 = Win32RegistryApi.RegQueryValueEx(this.GetHandle(rkey), name, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, ref num);
			if (num2 == 2 || num2 == 1018)
			{
				return RegistryValueKind.Unknown;
			}
			return registryValueKind;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000214EC File Offset: 0x0001F6EC
		public object GetValue(RegistryKey rkey, string name, object defaultValue, RegistryValueOptions options)
		{
			RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
			int num = 0;
			IntPtr handle = this.GetHandle(rkey);
			int num2 = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, ref num);
			if (num2 == 2 || num2 == 1018)
			{
				return defaultValue;
			}
			if (num2 != 234 && num2 != 0)
			{
				this.GenerateException(num2);
			}
			object obj;
			if (registryValueKind == RegistryValueKind.String)
			{
				byte[] array;
				num2 = this.GetBinaryValue(rkey, name, registryValueKind, out array, num);
				obj = RegistryKey.DecodeString(array);
			}
			else if (registryValueKind == RegistryValueKind.ExpandString)
			{
				byte[] array2;
				num2 = this.GetBinaryValue(rkey, name, registryValueKind, out array2, num);
				obj = RegistryKey.DecodeString(array2);
				if ((options & RegistryValueOptions.DoNotExpandEnvironmentNames) == RegistryValueOptions.None)
				{
					obj = Environment.ExpandEnvironmentVariables((string)obj);
				}
			}
			else if (registryValueKind == RegistryValueKind.DWord)
			{
				int num3 = 0;
				num2 = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, ref num3, ref num);
				obj = num3;
			}
			else if (registryValueKind == RegistryValueKind.QWord)
			{
				long num4 = 0L;
				num2 = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, ref num4, ref num);
				obj = num4;
			}
			else if (registryValueKind == RegistryValueKind.Binary)
			{
				byte[] array3;
				num2 = this.GetBinaryValue(rkey, name, registryValueKind, out array3, num);
				obj = array3;
			}
			else
			{
				if (registryValueKind != RegistryValueKind.MultiString)
				{
					throw new SystemException();
				}
				obj = null;
				byte[] array4;
				num2 = this.GetBinaryValue(rkey, name, registryValueKind, out array4, num);
				if (num2 == 0)
				{
					obj = RegistryKey.DecodeString(array4).Split(new char[1]);
				}
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
			return obj;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002163C File Offset: 0x0001F83C
		public void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind)
		{
			Type type = value.GetType();
			IntPtr handle = this.GetHandle(rkey);
			switch (valueKind)
			{
			case RegistryValueKind.String:
			case RegistryValueKind.ExpandString:
				if (type == typeof(string))
				{
					string text = string.Format("{0}{1}", value, '\0');
					this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, valueKind, text, text.Length * this.NativeBytesPerCharacter));
					return;
				}
				goto IL_01B7;
			case RegistryValueKind.Binary:
				goto IL_009C;
			case RegistryValueKind.DWord:
				break;
			case (RegistryValueKind)5:
			case (RegistryValueKind)6:
			case (RegistryValueKind)8:
			case (RegistryValueKind)9:
			case (RegistryValueKind)10:
				goto IL_01A4;
			case RegistryValueKind.MultiString:
				if (type == typeof(string[]))
				{
					string[] array = (string[])value;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text2 in array)
					{
						stringBuilder.Append(text2);
						stringBuilder.Append('\0');
					}
					stringBuilder.Append('\0');
					byte[] bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
					this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.MultiString, bytes, bytes.Length));
					return;
				}
				goto IL_01B7;
			case RegistryValueKind.QWord:
				try
				{
					long num = Convert.ToInt64(value);
					this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.QWord, ref num, 8));
					return;
				}
				catch (OverflowException)
				{
					goto IL_01B7;
				}
				break;
			default:
				goto IL_01A4;
			}
			try
			{
				int num2 = Convert.ToInt32(value);
				this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.DWord, ref num2, 4));
				return;
			}
			catch (OverflowException)
			{
				goto IL_01B7;
			}
			IL_009C:
			if (type == typeof(byte[]))
			{
				byte[] array3 = (byte[])value;
				this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.Binary, array3, array3.Length));
				return;
			}
			goto IL_01B7;
			IL_01A4:
			if (type.IsArray)
			{
				throw new ArgumentException("Only string and byte arrays can written as registry values");
			}
			IL_01B7:
			throw new ArgumentException("Type does not match the valueKind");
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00021828 File Offset: 0x0001FA28
		public void SetValue(RegistryKey rkey, string name, object value)
		{
			Type type = value.GetType();
			IntPtr handle = this.GetHandle(rkey);
			int num2;
			if (type == typeof(int))
			{
				int num = (int)value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.DWord, ref num, 4);
			}
			else if (type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.Binary, array, array.Length);
			}
			else if (type == typeof(string[]))
			{
				string[] array2 = (string[])value;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in array2)
				{
					stringBuilder.Append(text);
					stringBuilder.Append('\0');
				}
				stringBuilder.Append('\0');
				byte[] bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.MultiString, bytes, bytes.Length);
			}
			else
			{
				if (type.IsArray)
				{
					throw new ArgumentException("Only string and byte arrays can written as registry values");
				}
				string text2 = string.Format("{0}{1}", value, '\0');
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.String, text2, text2.Length * this.NativeBytesPerCharacter);
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00021974 File Offset: 0x0001FB74
		private int GetBinaryValue(RegistryKey rkey, string name, RegistryValueKind type, out byte[] data, int size)
		{
			byte[] array = new byte[size];
			int num = Win32RegistryApi.RegQueryValueEx(this.GetHandle(rkey), name, IntPtr.Zero, ref type, array, ref size);
			data = array;
			return num;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000219A4 File Offset: 0x0001FBA4
		public int SubKeyCount(RegistryKey rkey)
		{
			int num = 0;
			int num2 = 0;
			int num3 = Win32RegistryApi.RegQueryInfoKey(this.GetHandle(rkey), null, null, IntPtr.Zero, ref num, null, null, ref num2, null, null, null, null);
			if (num3 != 0)
			{
				this.GenerateException(num3);
			}
			return num;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000219E0 File Offset: 0x0001FBE0
		public int ValueCount(RegistryKey rkey)
		{
			int num = 0;
			int num2 = 0;
			int num3 = Win32RegistryApi.RegQueryInfoKey(this.GetHandle(rkey), null, null, IntPtr.Zero, ref num2, null, null, ref num, null, null, null, null);
			if (num3 != 0)
			{
				this.GenerateException(num3);
			}
			return num;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00021A1C File Offset: 0x0001FC1C
		public RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			IntPtr intPtr = new IntPtr((int)hKey);
			IntPtr intPtr2;
			int num = Win32RegistryApi.RegConnectRegistry(machineName, intPtr, out intPtr2);
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return new RegistryKey(hKey, intPtr2, true);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00021A50 File Offset: 0x0001FC50
		public RegistryKey OpenSubKey(RegistryKey rkey, string keyName, bool writable)
		{
			int num = 131097;
			if (writable)
			{
				num |= 131078;
			}
			IntPtr intPtr;
			int num2 = Win32RegistryApi.RegOpenKeyEx(this.GetHandle(rkey), keyName, IntPtr.Zero, num, out intPtr);
			if (num2 == 2 || num2 == 1018)
			{
				return null;
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), writable);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00021AAF File Offset: 0x0001FCAF
		public void Flush(RegistryKey rkey)
		{
			if (!Win32RegistryApi.IsHandleValid(rkey))
			{
				return;
			}
			Win32RegistryApi.RegFlushKey(this.GetHandle(rkey));
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00021AC8 File Offset: 0x0001FCC8
		public void Close(RegistryKey rkey)
		{
			if (!Win32RegistryApi.IsHandleValid(rkey))
			{
				return;
			}
			SafeRegistryHandle handle = rkey.Handle;
			if (handle != null)
			{
				handle.Close();
				return;
			}
			Win32RegistryApi.RegCloseKey(this.GetHandle(rkey));
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00021AFC File Offset: 0x0001FCFC
		public RegistryKey FromHandle(SafeRegistryHandle handle)
		{
			return new RegistryKey(handle.DangerousGetHandle(), string.Empty, true);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00021B14 File Offset: 0x0001FD14
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyName)
		{
			IntPtr intPtr;
			int num2;
			int num = Win32RegistryApi.RegCreateKeyEx(this.GetHandle(rkey), keyName, 0, IntPtr.Zero, 0, 131103, IntPtr.Zero, out intPtr, out num2);
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), true);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00021B64 File Offset: 0x0001FD64
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyName, RegistryOptions options)
		{
			IntPtr intPtr;
			int num2;
			int num = Win32RegistryApi.RegCreateKeyEx(this.GetHandle(rkey), keyName, 0, IntPtr.Zero, (options == RegistryOptions.Volatile) ? 1 : 0, 131103, IntPtr.Zero, out intPtr, out num2);
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), true);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		public void DeleteKey(RegistryKey rkey, string keyName, bool shouldThrowWhenKeyMissing)
		{
			int num = Win32RegistryApi.RegDeleteKey(this.GetHandle(rkey), keyName);
			if (num != 2)
			{
				if (num != 0)
				{
					this.GenerateException(num);
				}
				return;
			}
			if (shouldThrowWhenKeyMissing)
			{
				throw new ArgumentException("key " + keyName);
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00021BF8 File Offset: 0x0001FDF8
		public void DeleteValue(RegistryKey rkey, string value, bool shouldThrowWhenKeyMissing)
		{
			int num = Win32RegistryApi.RegDeleteValue(this.GetHandle(rkey), value);
			if (num == 1018)
			{
				return;
			}
			if (num != 2)
			{
				if (num != 0)
				{
					this.GenerateException(num);
				}
				return;
			}
			if (shouldThrowWhenKeyMissing)
			{
				throw new ArgumentException("value " + value);
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00021C40 File Offset: 0x0001FE40
		public unsafe string[] GetSubKeyNames(RegistryKey rkey)
		{
			int num = this.SubKeyCount(rkey);
			string[] array = new string[num];
			if (num > 0)
			{
				IntPtr handle = this.GetHandle(rkey);
				char[] array2 = new char[256];
				fixed (char* ptr = &array2[0])
				{
					char* ptr2 = ptr;
					for (int i = 0; i < num; i++)
					{
						int num2 = array2.Length;
						int num3 = Win32RegistryApi.RegEnumKeyEx(handle, i, ptr2, ref num2, null, null, null, null);
						if (num3 != 0)
						{
							this.GenerateException(num3);
						}
						array[i] = new string(ptr2);
					}
				}
			}
			return array;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00021CC4 File Offset: 0x0001FEC4
		public unsafe string[] GetValueNames(RegistryKey rkey)
		{
			int num = this.ValueCount(rkey);
			string[] array = new string[num];
			if (num > 0)
			{
				IntPtr handle = this.GetHandle(rkey);
				char[] array2 = new char[16384];
				fixed (char* ptr = &array2[0])
				{
					char* ptr2 = ptr;
					for (int i = 0; i < num; i++)
					{
						int num2 = array2.Length;
						int num3 = Win32RegistryApi.RegEnumValue(handle, i, ptr2, ref num2, IntPtr.Zero, null, null, null);
						if (num3 != 0 && num3 != 234)
						{
							this.GenerateException(num3);
						}
						array[i] = new string(ptr2);
					}
				}
			}
			return array;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00021D55 File Offset: 0x0001FF55
		private void CheckResult(int result)
		{
			if (result != 0)
			{
				this.GenerateException(result);
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00021D64 File Offset: 0x0001FF64
		private void GenerateException(int errorCode)
		{
			if (errorCode <= 53)
			{
				switch (errorCode)
				{
				case 2:
					break;
				case 3:
				case 4:
					goto IL_0072;
				case 5:
					throw new SecurityException();
				case 6:
					throw new IOException("Invalid handle.");
				default:
					if (errorCode != 53)
					{
						goto IL_0072;
					}
					throw new IOException("The network path was not found.");
				}
			}
			else if (errorCode != 87)
			{
				if (errorCode == 1018)
				{
					throw RegistryKey.CreateMarkedForDeletionException();
				}
				if (errorCode != 1021)
				{
					goto IL_0072;
				}
				throw new IOException("Cannot create a stable subkey under a volatile parent key.");
			}
			throw new ArgumentException();
			IL_0072:
			throw new SystemException();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00021420 File Offset: 0x0001F620
		public string ToString(RegistryKey rkey)
		{
			return rkey.Name;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00021DE8 File Offset: 0x0001FFE8
		internal static string CombineName(RegistryKey rkey, string localName)
		{
			return rkey.Name + "\\" + localName;
		}

		// Token: 0x04000647 RID: 1607
		private const int OpenRegKeyRead = 131097;

		// Token: 0x04000648 RID: 1608
		private const int OpenRegKeyWrite = 131078;

		// Token: 0x04000649 RID: 1609
		private const int Int32ByteSize = 4;

		// Token: 0x0400064A RID: 1610
		private const int Int64ByteSize = 8;

		// Token: 0x0400064B RID: 1611
		private readonly int NativeBytesPerCharacter = Marshal.SystemDefaultCharSize;

		// Token: 0x0400064C RID: 1612
		private const int RegOptionsNonVolatile = 0;

		// Token: 0x0400064D RID: 1613
		private const int RegOptionsVolatile = 1;

		// Token: 0x0400064E RID: 1614
		private const int MaxKeyLength = 255;

		// Token: 0x0400064F RID: 1615
		private const int MaxValueLength = 16383;
	}
}
