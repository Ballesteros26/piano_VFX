using System;
using Microsoft.Win32;

namespace System.Web.Configuration
{
	// Token: 0x020005B7 RID: 1463
	internal class MachineKeyRegistryStorage
	{
		// Token: 0x06003EBB RID: 16059 RVA: 0x000A6118 File Offset: 0x000A4318
		static MachineKeyRegistryStorage()
		{
			string applicationName = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
			if (applicationName == null)
			{
				return;
			}
			string text = applicationName.GetHashCode().ToString("x");
			MachineKeyRegistryStorage.keyEncryption = string.Concat(new string[]
			{
				"software\\mono\\asp.net\\",
				Environment.Version.ToString(),
				"\\autogenkeys\\",
				text,
				"-",
				1.ToString()
			});
			MachineKeyRegistryStorage.keyValidation = string.Concat(new string[]
			{
				"software\\mono\\asp.net\\",
				Environment.Version.ToString(),
				"\\autogenkeys\\",
				text,
				"-",
				0.ToString()
			});
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x000A61D8 File Offset: 0x000A43D8
		public static byte[] Retrieve(MachineKeyRegistryStorage.KeyType kt)
		{
			string text;
			if (kt != MachineKeyRegistryStorage.KeyType.Validation)
			{
				if (kt != MachineKeyRegistryStorage.KeyType.Encryption)
				{
					throw new ArgumentException("Unknown key type.");
				}
				text = MachineKeyRegistryStorage.keyEncryption;
			}
			else
			{
				text = MachineKeyRegistryStorage.keyValidation;
			}
			if (text == null)
			{
				return null;
			}
			object obj = null;
			try
			{
				obj = MachineKeyRegistryStorage.OpenRegistryKey(text, false).GetValue("AutoGenKey", null);
			}
			catch (Exception)
			{
				return null;
			}
			if (obj == null || obj.GetType() != typeof(byte[]))
			{
				return null;
			}
			return (byte[])obj;
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000A6260 File Offset: 0x000A4460
		private static RegistryKey OpenRegistryKey(string path, bool write)
		{
			string[] array = path.Split(new char[] { '\\' });
			int num = array.Length;
			RegistryKey registryKey = Registry.CurrentUser;
			for (int i = 0; i < num; i++)
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey(array[i], true);
				if (registryKey2 == null)
				{
					if (!write)
					{
						return null;
					}
					registryKey2 = registryKey.CreateSubKey(array[i]);
				}
				registryKey = registryKey2;
			}
			return registryKey;
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x000A62BC File Offset: 0x000A44BC
		public static void Store(byte[] buf, MachineKeyRegistryStorage.KeyType kt)
		{
			if (buf == null)
			{
				return;
			}
			string text;
			if (kt != MachineKeyRegistryStorage.KeyType.Validation)
			{
				if (kt != MachineKeyRegistryStorage.KeyType.Encryption)
				{
					throw new ArgumentException("Unknown key type.");
				}
				text = MachineKeyRegistryStorage.keyEncryption;
			}
			else
			{
				text = MachineKeyRegistryStorage.keyValidation;
			}
			if (text == null)
			{
				return;
			}
			try
			{
				using (RegistryKey registryKey = MachineKeyRegistryStorage.OpenRegistryKey(text, true))
				{
					registryKey.SetValue("AutoGenKey", buf, RegistryValueKind.Binary);
					registryKey.SetValue("AutoGenKeyCreationTime", DateTime.Now.Ticks, RegistryValueKind.QWord);
					registryKey.SetValue("AutoGenKeyFormat", 2, RegistryValueKind.DWord);
					registryKey.Flush();
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("(info) Auto generated encryption keys not saved: {0}", ex);
			}
		}

		// Token: 0x04002243 RID: 8771
		private static string keyEncryption;

		// Token: 0x04002244 RID: 8772
		private static string keyValidation;

		// Token: 0x020005B8 RID: 1464
		public enum KeyType
		{
			// Token: 0x04002246 RID: 8774
			Validation,
			// Token: 0x04002247 RID: 8775
			Encryption
		}
	}
}
