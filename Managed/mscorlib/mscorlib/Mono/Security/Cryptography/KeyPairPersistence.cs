using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000082 RID: 130
	internal class KeyPairPersistence
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00017277 File Offset: 0x00015477
		public KeyPairPersistence(CspParameters parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00017281 File Offset: 0x00015481
		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this._params = this.Copy(parameters);
			this._keyvalue = keyPair;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000172AC File Offset: 0x000154AC
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00017338 File Offset: 0x00015538
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00017340 File Offset: 0x00015540
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00017351 File Offset: 0x00015551
		public CspParameters Parameters
		{
			get
			{
				return this.Copy(this._params);
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00017360 File Offset: 0x00015560
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

		// Token: 0x06000401 RID: 1025 RVA: 0x000173B4 File Offset: 0x000155B4
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

		// Token: 0x06000402 RID: 1026 RVA: 0x00017428 File Offset: 0x00015628
		public void Remove()
		{
			File.Delete(this.Filename);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00017438 File Offset: 0x00015638
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00017558 File Offset: 0x00015758
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

		// Token: 0x06000405 RID: 1029
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _CanSecure(string root);

		// Token: 0x06000406 RID: 1030
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _ProtectUser(string path);

		// Token: 0x06000407 RID: 1031
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _ProtectMachine(string path);

		// Token: 0x06000408 RID: 1032
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _IsUserProtected(string path);

		// Token: 0x06000409 RID: 1033
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _IsMachineProtected(string path);

		// Token: 0x0600040A RID: 1034 RVA: 0x00017678 File Offset: 0x00015878
		private static bool CanSecure(string path)
		{
			int platform = (int)Environment.OSVersion.Platform;
			return platform == 4 || platform == 128 || platform == 6 || KeyPairPersistence._CanSecure(Path.GetPathRoot(path));
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000176AD File Offset: 0x000158AD
		private static bool ProtectUser(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._ProtectUser(path);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000176BF File Offset: 0x000158BF
		private static bool ProtectMachine(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._ProtectMachine(path);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000176D1 File Offset: 0x000158D1
		private static bool IsUserProtected(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._IsUserProtected(path);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000176E3 File Offset: 0x000158E3
		private static bool IsMachineProtected(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._IsMachineProtected(path);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000176F5 File Offset: 0x000158F5
		private bool CanChange
		{
			get
			{
				return this._keyvalue == null;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00017700 File Offset: 0x00015900
		private bool UseDefaultKeyContainer
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00017712 File Offset: 0x00015912
		private bool UseMachineKeyStore
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00017724 File Offset: 0x00015924
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

		// Token: 0x06000413 RID: 1043 RVA: 0x000177CD File Offset: 0x000159CD
		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00017800 File Offset: 0x00015A00
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

		// Token: 0x06000415 RID: 1045 RVA: 0x00017864 File Offset: 0x00015A64
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

		// Token: 0x04000555 RID: 1365
		private static bool _userPathExists;

		// Token: 0x04000556 RID: 1366
		private static string _userPath;

		// Token: 0x04000557 RID: 1367
		private static bool _machinePathExists;

		// Token: 0x04000558 RID: 1368
		private static string _machinePath;

		// Token: 0x04000559 RID: 1369
		private CspParameters _params;

		// Token: 0x0400055A RID: 1370
		private string _keyvalue;

		// Token: 0x0400055B RID: 1371
		private string _filename;

		// Token: 0x0400055C RID: 1372
		private string _container;

		// Token: 0x0400055D RID: 1373
		private static object lockobj = new object();
	}
}
