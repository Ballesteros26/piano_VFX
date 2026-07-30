using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x02000063 RID: 99
	internal class X509Store
	{
		// Token: 0x06000358 RID: 856 RVA: 0x000147C4 File Offset: 0x000129C4
		internal X509Store(string path, bool crl, bool newFormat)
		{
			this._storePath = path;
			this._crl = crl;
			this._newFormat = newFormat;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000147E1 File Offset: 0x000129E1
		public X509CertificateCollection Certificates
		{
			get
			{
				if (this._certificates == null)
				{
					this._certificates = this.BuildCertificatesCollection(this._storePath);
				}
				return this._certificates;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00014803 File Offset: 0x00012A03
		public ArrayList Crls
		{
			get
			{
				if (!this._crl)
				{
					this._crls = new ArrayList();
				}
				if (this._crls == null)
				{
					this._crls = this.BuildCrlsCollection(this._storePath);
				}
				return this._crls;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00014838 File Offset: 0x00012A38
		public string Name
		{
			get
			{
				if (this._name == null)
				{
					int num = this._storePath.LastIndexOf(Path.DirectorySeparatorChar);
					this._name = this._storePath.Substring(num + 1);
				}
				return this._name;
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00014878 File Offset: 0x00012A78
		public void Clear()
		{
			this.ClearCertificates();
			this.ClearCrls();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00014886 File Offset: 0x00012A86
		private void ClearCertificates()
		{
			if (this._certificates != null)
			{
				this._certificates.Clear();
			}
			this._certificates = null;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000148A2 File Offset: 0x00012AA2
		private void ClearCrls()
		{
			if (this._crls != null)
			{
				this._crls.Clear();
			}
			this._crls = null;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000148C0 File Offset: 0x00012AC0
		public void Import(X509Certificate certificate)
		{
			this.CheckStore(this._storePath, true);
			if (this._newFormat)
			{
				this.ImportNewFormat(certificate);
				return;
			}
			string text = Path.Combine(this._storePath, this.GetUniqueName(certificate, null));
			if (!File.Exists(text))
			{
				text = Path.Combine(this._storePath, this.GetUniqueNameWithSerial(certificate));
				if (!File.Exists(text))
				{
					using (FileStream fileStream = File.Create(text))
					{
						byte[] rawData = certificate.RawData;
						fileStream.Write(rawData, 0, rawData.Length);
						fileStream.Close();
					}
					this.ClearCertificates();
				}
			}
			else
			{
				string text2 = Path.Combine(this._storePath, this.GetUniqueNameWithSerial(certificate));
				if (this.GetUniqueNameWithSerial(this.LoadCertificate(text)) != this.GetUniqueNameWithSerial(certificate))
				{
					using (FileStream fileStream2 = File.Create(text2))
					{
						byte[] rawData2 = certificate.RawData;
						fileStream2.Write(rawData2, 0, rawData2.Length);
						fileStream2.Close();
					}
					this.ClearCertificates();
				}
			}
			CspParameters cspParameters = new CspParameters();
			cspParameters.KeyContainerName = CryptoConvert.ToHex(certificate.Hash);
			if (this._storePath.StartsWith(X509StoreManager.LocalMachinePath) || this._storePath.StartsWith(X509StoreManager.NewLocalMachinePath))
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			this.ImportPrivateKey(certificate, cspParameters);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00014A28 File Offset: 0x00012C28
		public void Import(X509Crl crl)
		{
			this.CheckStore(this._storePath, true);
			if (this._newFormat)
			{
				throw new NotSupportedException();
			}
			string text = Path.Combine(this._storePath, this.GetUniqueName(crl));
			if (!File.Exists(text))
			{
				using (FileStream fileStream = File.Create(text))
				{
					byte[] rawData = crl.RawData;
					fileStream.Write(rawData, 0, rawData.Length);
				}
				this.ClearCrls();
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public void Remove(X509Certificate certificate)
		{
			if (this._newFormat)
			{
				this.RemoveNewFormat(certificate);
				return;
			}
			string text = Path.Combine(this._storePath, this.GetUniqueNameWithSerial(certificate));
			if (File.Exists(text))
			{
				File.Delete(text);
				this.ClearCertificates();
				return;
			}
			text = Path.Combine(this._storePath, this.GetUniqueName(certificate, null));
			if (File.Exists(text))
			{
				File.Delete(text);
				this.ClearCertificates();
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00014B18 File Offset: 0x00012D18
		public void Remove(X509Crl crl)
		{
			if (this._newFormat)
			{
				throw new NotSupportedException();
			}
			string text = Path.Combine(this._storePath, this.GetUniqueName(crl));
			if (File.Exists(text))
			{
				File.Delete(text);
				this.ClearCrls();
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00014B5A File Offset: 0x00012D5A
		private void ImportNewFormat(X509Certificate certificate)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00014B5A File Offset: 0x00012D5A
		private void RemoveNewFormat(X509Certificate certificate)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00014B61 File Offset: 0x00012D61
		private string GetUniqueNameWithSerial(X509Certificate certificate)
		{
			return this.GetUniqueName(certificate, certificate.SerialNumber);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00014B70 File Offset: 0x00012D70
		private string GetUniqueName(X509Certificate certificate, byte[] serial = null)
		{
			byte[] array = this.GetUniqueName(certificate.Extensions, serial);
			string text;
			if (array == null)
			{
				text = "tbp";
				array = certificate.Hash;
			}
			else
			{
				text = "ski";
			}
			return this.GetUniqueName(text, array, ".cer");
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00014BB0 File Offset: 0x00012DB0
		private string GetUniqueName(X509Crl crl)
		{
			byte[] array = this.GetUniqueName(crl.Extensions, null);
			string text;
			if (array == null)
			{
				text = "tbp";
				array = crl.Hash;
			}
			else
			{
				text = "ski";
			}
			return this.GetUniqueName(text, array, ".crl");
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00014BF0 File Offset: 0x00012DF0
		private byte[] GetUniqueName(X509ExtensionCollection extensions, byte[] serial = null)
		{
			X509Extension x509Extension = extensions["2.5.29.14"];
			if (x509Extension == null)
			{
				return null;
			}
			SubjectKeyIdentifierExtension subjectKeyIdentifierExtension = new SubjectKeyIdentifierExtension(x509Extension);
			if (serial == null)
			{
				return subjectKeyIdentifierExtension.Identifier;
			}
			byte[] array = new byte[subjectKeyIdentifierExtension.Identifier.Length + serial.Length];
			Buffer.BlockCopy(subjectKeyIdentifierExtension.Identifier, 0, array, 0, subjectKeyIdentifierExtension.Identifier.Length);
			Buffer.BlockCopy(serial, 0, array, subjectKeyIdentifierExtension.Identifier.Length, serial.Length);
			return array;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00014C5C File Offset: 0x00012E5C
		private string GetUniqueName(string method, byte[] name, string fileExtension)
		{
			StringBuilder stringBuilder = new StringBuilder(method);
			stringBuilder.Append("-");
			foreach (byte b in name)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(fileExtension);
			return stringBuilder.ToString();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00014CB8 File Offset: 0x00012EB8
		private byte[] Load(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.OpenRead(filename))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return array;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00014D0C File Offset: 0x00012F0C
		private X509Certificate LoadCertificate(string filename)
		{
			X509Certificate x509Certificate = new X509Certificate(this.Load(filename));
			CspParameters cspParameters = new CspParameters();
			cspParameters.KeyContainerName = CryptoConvert.ToHex(x509Certificate.Hash);
			if (this._storePath.StartsWith(X509StoreManager.LocalMachinePath) || this._storePath.StartsWith(X509StoreManager.NewLocalMachinePath))
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			KeyPairPersistence keyPairPersistence = new KeyPairPersistence(cspParameters);
			try
			{
				if (!keyPairPersistence.Load())
				{
					return x509Certificate;
				}
			}
			catch
			{
				return x509Certificate;
			}
			if (x509Certificate.RSA != null)
			{
				x509Certificate.RSA = new RSACryptoServiceProvider(cspParameters);
			}
			else if (x509Certificate.DSA != null)
			{
				x509Certificate.DSA = new DSACryptoServiceProvider(cspParameters);
			}
			return x509Certificate;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00014DC0 File Offset: 0x00012FC0
		private X509Crl LoadCrl(string filename)
		{
			return new X509Crl(this.Load(filename));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00014DD0 File Offset: 0x00012FD0
		private bool CheckStore(string path, bool throwException)
		{
			bool flag;
			try
			{
				if (Directory.Exists(path))
				{
					flag = true;
				}
				else
				{
					Directory.CreateDirectory(path);
					flag = Directory.Exists(path);
				}
			}
			catch
			{
				if (throwException)
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00014E14 File Offset: 0x00013014
		private X509CertificateCollection BuildCertificatesCollection(string storeName)
		{
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			string text = Path.Combine(this._storePath, storeName);
			if (!this.CheckStore(text, false))
			{
				return x509CertificateCollection;
			}
			string[] files = Directory.GetFiles(text, this._newFormat ? "*.0" : "*.cer");
			if (files != null && files.Length != 0)
			{
				foreach (string text2 in files)
				{
					try
					{
						X509Certificate x509Certificate = this.LoadCertificate(text2);
						x509CertificateCollection.Add(x509Certificate);
					}
					catch
					{
					}
				}
			}
			return x509CertificateCollection;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00014EA8 File Offset: 0x000130A8
		private ArrayList BuildCrlsCollection(string storeName)
		{
			ArrayList arrayList = new ArrayList();
			string text = Path.Combine(this._storePath, storeName);
			if (!this.CheckStore(text, false))
			{
				return arrayList;
			}
			string[] files = Directory.GetFiles(text, "*.crl");
			if (files != null && files.Length != 0)
			{
				foreach (string text2 in files)
				{
					try
					{
						X509Crl x509Crl = this.LoadCrl(text2);
						arrayList.Add(x509Crl);
					}
					catch
					{
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00014F2C File Offset: 0x0001312C
		private void ImportPrivateKey(X509Certificate certificate, CspParameters cspParams)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = certificate.RSA as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				if (rsacryptoServiceProvider.PublicOnly)
				{
					return;
				}
				RSACryptoServiceProvider rsacryptoServiceProvider2 = new RSACryptoServiceProvider(cspParams);
				rsacryptoServiceProvider2.ImportParameters(rsacryptoServiceProvider.ExportParameters(true));
				rsacryptoServiceProvider2.PersistKeyInCsp = true;
				return;
			}
			else
			{
				RSAManaged rsamanaged = certificate.RSA as RSAManaged;
				if (rsamanaged == null)
				{
					DSACryptoServiceProvider dsacryptoServiceProvider = certificate.DSA as DSACryptoServiceProvider;
					if (dsacryptoServiceProvider != null)
					{
						if (dsacryptoServiceProvider.PublicOnly)
						{
							return;
						}
						DSACryptoServiceProvider dsacryptoServiceProvider2 = new DSACryptoServiceProvider(cspParams);
						dsacryptoServiceProvider2.ImportParameters(dsacryptoServiceProvider.ExportParameters(true));
						dsacryptoServiceProvider2.PersistKeyInCsp = true;
					}
					return;
				}
				if (rsamanaged.PublicOnly)
				{
					return;
				}
				RSACryptoServiceProvider rsacryptoServiceProvider3 = new RSACryptoServiceProvider(cspParams);
				rsacryptoServiceProvider3.ImportParameters(rsamanaged.ExportParameters(true));
				rsacryptoServiceProvider3.PersistKeyInCsp = true;
				return;
			}
		}

		// Token: 0x0400051D RID: 1309
		private string _storePath;

		// Token: 0x0400051E RID: 1310
		private X509CertificateCollection _certificates;

		// Token: 0x0400051F RID: 1311
		private ArrayList _crls;

		// Token: 0x04000520 RID: 1312
		private bool _crl;

		// Token: 0x04000521 RID: 1313
		private bool _newFormat;

		// Token: 0x04000522 RID: 1314
		private string _name;
	}
}
