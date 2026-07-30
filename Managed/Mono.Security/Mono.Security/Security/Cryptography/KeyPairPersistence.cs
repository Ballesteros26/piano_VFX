using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000094 RID: 148
	public class KeyPairPersistence
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x0001901C File Offset: 0x0001721C
		public KeyPairPersistence(CspParameters parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00019026 File Offset: 0x00017226
		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this._params = this.Copy(parameters);
			this._keyvalue = keyPair;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00019050 File Offset: 0x00017250
		public string Filename
		{
			get
			{
				if (this._filename == null)
				{
					this._filename = string.Format(CultureInfo.InvariantCulture, "[{0}][{1}][{2}].xml", this._params.ProviderType, this.ContainerName, this._params.KeyNumber);
					if (this.UseMachineKeyStore)
					{
						this._filename = Path.Combine(KeyPairPersistence.MachinePath, this._filename);
					}
					else
					{
						this._filename = Path.Combine(KeyPairPersistence.UserPath, this._filename);
					}
				}
				return this._filename;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x000190DC File Offset: 0x000172DC
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x000190E4 File Offset: 0x000172E4
		public string KeyValue
		{
			get
			{
				return this._keyvalue;
			}
			set
			{
				if (this.CanChange)
				{
					this._keyvalue = value;
				}
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000190F5 File Offset: 0x000172F5
		public CspParameters Parameters
		{
			get
			{
				return this.Copy(this._params);
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00019104 File Offset: 0x00017304
		public bool Load()
		{
			bool flag = File.Exists(this.Filename);
			if (flag)
			{
				using (StreamReader streamReader = File.OpenText(this.Filename))
				{
					this.FromXml(streamReader.ReadToEnd());
				}
			}
			return flag;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00019158 File Offset: 0x00017358
		public void Save()
		{
			using (FileStream fileStream = File.Open(this.Filename, FileMode.Create))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
				streamWriter.Write(this.ToXml());
				streamWriter.Close();
			}
			if (this.UseMachineKeyStore)
			{
				KeyPairPersistence.ProtectMachine(this.Filename);
				return;
			}
			KeyPairPersistence.ProtectUser(this.Filename);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000191CC File Offset: 0x000173CC
		public void Remove()
		{
			File.Delete(this.Filename);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000191DC File Offset: 0x000173DC
		private static string UserPath
		{
			get
			{
				object obj = KeyPairPersistence.lockobj;
				lock (obj)
				{
					if (KeyPairPersistence._userPath == null || !KeyPairPersistence._userPathExists)
					{
						KeyPairPersistence._userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
						KeyPairPersistence._userPath = Path.Combine(KeyPairPersistence._userPath, "keypairs");
						KeyPairPersistence._userPathExists = Directory.Exists(KeyPairPersistence._userPath);
						if (!KeyPairPersistence._userPathExists)
						{
							try
							{
								Directory.CreateDirectory(KeyPairPersistence._userPath);
							}
							catch (Exception ex)
							{
								throw new CryptographicException(string.Format(Locale.GetText("Could not create user key store '{0}'."), KeyPairPersistence._userPath), ex);
							}
							KeyPairPersistence._userPathExists = true;
						}
					}
					if (!KeyPairPersistence.IsUserProtected(KeyPairPersistence._userPath) && !KeyPairPersistence.ProtectUser(KeyPairPersistence._userPath))
					{
						throw new IOException(string.Format(Locale.GetText("Could not secure user key store '{0}'."), KeyPairPersistence._userPath));
					}
				}
				if (!KeyPairPersistence.IsUserProtected(KeyPairPersistence._userPath))
				{
					throw new CryptographicException(string.Format(Locale.GetText("Improperly protected user's key pairs in '{0}'."), KeyPairPersistence._userPath));
				}
				return KeyPairPersistence._userPath;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000192FC File Offset: 0x000174FC
		private static string MachinePath
		{
			get
			{
				object obj = KeyPairPersistence.lockobj;
				lock (obj)
				{
					if (KeyPairPersistence._machinePath == null || !KeyPairPersistence._machinePathExists)
					{
						KeyPairPersistence._machinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
						KeyPairPersistence._machinePath = Path.Combine(KeyPairPersistence._machinePath, "keypairs");
						KeyPairPersistence._machinePathExists = Directory.Exists(KeyPairPersistence._machinePath);
						if (!KeyPairPersistence._machinePathExists)
						{
							try
							{
								Directory.CreateDirectory(KeyPairPersistence._machinePath);
							}
							catch (Exception ex)
							{
								throw new CryptographicException(string.Format(Locale.GetText("Could not create machine key store '{0}'."), KeyPairPersistence._machinePath), ex);
							}
							KeyPairPersistence._machinePathExists = true;
						}
					}
					if (!KeyPairPersistence.IsMachineProtected(KeyPairPersistence._machinePath) && !KeyPairPersistence.ProtectMachine(KeyPairPersistence._machinePath))
					{
						throw new IOException(string.Format(Locale.GetText("Could not secure machine key store '{0}'."), KeyPairPersistence._machinePath));
					}
				}
				if (!KeyPairPersistence.IsMachineProtected(KeyPairPersistence._machinePath))
				{
					throw new CryptographicException(string.Format(Locale.GetText("Improperly protected machine's key pairs in '{0}'."), KeyPairPersistence._machinePath));
				}
				return KeyPairPersistence._machinePath;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001941C File Offset: 0x0001761C
		internal static bool _CanSecure(string root)
		{
			return true;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001941F File Offset: 0x0001761F
		internal static bool _ProtectUser(string path)
		{
			return true;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00019422 File Offset: 0x00017622
		internal static bool _ProtectMachine(string path)
		{
			return true;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00019425 File Offset: 0x00017625
		internal static bool _IsUserProtected(string path)
		{
			return true;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00019428 File Offset: 0x00017628
		internal static bool _IsMachineProtected(string path)
		{
			return true;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001942C File Offset: 0x0001762C
		private static bool CanSecure(string path)
		{
			int platform = (int)Environment.OSVersion.Platform;
			return platform == 4 || platform == 128 || platform == 6 || KeyPairPersistence._CanSecure(Path.GetPathRoot(path));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00019461 File Offset: 0x00017661
		private static bool ProtectUser(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._ProtectUser(path);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00019473 File Offset: 0x00017673
		private static bool ProtectMachine(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._ProtectMachine(path);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00019485 File Offset: 0x00017685
		private static bool IsUserProtected(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._IsUserProtected(path);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00019497 File Offset: 0x00017697
		private static bool IsMachineProtected(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._IsMachineProtected(path);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000194A9 File Offset: 0x000176A9
		private bool CanChange
		{
			get
			{
				return this._keyvalue == null;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000194B4 File Offset: 0x000176B4
		private bool UseDefaultKeyContainer
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000194C6 File Offset: 0x000176C6
		private bool UseMachineKeyStore
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000194D8 File Offset: 0x000176D8
		private string ContainerName
		{
			get
			{
				if (this._container == null)
				{
					if (this.UseDefaultKeyContainer)
					{
						this._container = "default";
					}
					else if (this._params.KeyContainerName == null || this._params.KeyContainerName.Length == 0)
					{
						this._container = Guid.NewGuid().ToString();
					}
					else
					{
						byte[] bytes = Encoding.UTF8.GetBytes(this._params.KeyContainerName);
						byte[] array = MD5.Create().ComputeHash(bytes);
						this._container = new Guid(array).ToString();
					}
				}
				return this._container;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00019581 File Offset: 0x00017781
		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000195B4 File Offset: 0x000177B4
		private void FromXml(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag == "KeyPair")
			{
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("KeyValue");
				if (securityElement2.Children.Count > 0)
				{
					this._keyvalue = securityElement2.Children[0].ToString();
				}
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00019618 File Offset: 0x00017818
		private string ToXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<KeyPair>{0}\t<Properties>{0}\t\t<Provider ", Environment.NewLine);
			if (this._params.ProviderName != null && this._params.ProviderName.Length != 0)
			{
				stringBuilder.AppendFormat("Name=\"{0}\" ", this._params.ProviderName);
			}
			stringBuilder.AppendFormat("Type=\"{0}\" />{1}\t\t<Container ", this._params.ProviderType, Environment.NewLine);
			stringBuilder.AppendFormat("Name=\"{0}\" />{1}\t</Properties>{1}\t<KeyValue", this.ContainerName, Environment.NewLine);
			if (this._params.KeyNumber != -1)
			{
				stringBuilder.AppendFormat(" Id=\"{0}\" ", this._params.KeyNumber);
			}
			stringBuilder.AppendFormat(">{1}\t\t{0}{1}\t</KeyValue>{1}</KeyPair>{1}", this.KeyValue, Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x040003AC RID: 940
		private static bool _userPathExists;

		// Token: 0x040003AD RID: 941
		private static string _userPath;

		// Token: 0x040003AE RID: 942
		private static bool _machinePathExists;

		// Token: 0x040003AF RID: 943
		private static string _machinePath;

		// Token: 0x040003B0 RID: 944
		private CspParameters _params;

		// Token: 0x040003B1 RID: 945
		private string _keyvalue;

		// Token: 0x040003B2 RID: 946
		private string _filename;

		// Token: 0x040003B3 RID: 947
		private string _container;

		// Token: 0x040003B4 RID: 948
		private static object lockobj = new object();
	}
}
