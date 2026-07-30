using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x0200001A RID: 26
	public class X509Store
	{
		// Token: 0x06000150 RID: 336 RVA: 0x0000A8D8 File Offset: 0x00008AD8
		internal X509Store(string path, bool crl, bool newFormat)
		{
			this._storePath = path;
			this._crl = crl;
			this._newFormat = newFormat;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000A8F5 File Offset: 0x00008AF5
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000A917 File Offset: 0x00008B17
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000A94C File Offset: 0x00008B4C
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

		// Token: 0x06000154 RID: 340 RVA: 0x0000A98C File Offset: 0x00008B8C
		public void Clear()
		{
			this.ClearCertificates();
			this.ClearCrls();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A99A File Offset: 0x00008B9A
		private void ClearCertificates()
		{
			if (this._certificates != null)
			{
				this._certificates.Clear();
			}
			this._certificates = null;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		private void ClearCrls()
		{
			if (this._crls != null)
			{
				this._crls.Clear();
			}
			this._crls = null;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A9D4 File Offset: 0x00008BD4
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

		// Token: 0x06000158 RID: 344 RVA: 0x0000AB3C File Offset: 0x00008D3C
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

		// Token: 0x06000159 RID: 345 RVA: 0x0000ABBC File Offset: 0x00008DBC
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

		// Token: 0x0600015A RID: 346 RVA: 0x0000AC2C File Offset: 0x00008E2C
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

		// Token: 0x0600015B RID: 347 RVA: 0x0000AC70 File Offset: 0x00008E70
		private void ImportNewFormat(X509Certificate certificate)
		{
			using (X509Certificate x509Certificate = new X509Certificate(certificate.RawData))
			{
				long subjectNameHash = X509Helper2.GetSubjectNameHash(x509Certificate);
				string text = Path.Combine(this._storePath, string.Format("{0:x8}.0", subjectNameHash));
				if (!File.Exists(text))
				{
					using (FileStream fileStream = File.Create(text))
					{
						X509Helper2.ExportAsPEM(x509Certificate, fileStream, true);
					}
					this.ClearCertificates();
				}
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000AD00 File Offset: 0x00008F00
		private void RemoveNewFormat(X509Certificate certificate)
		{
			using (X509Certificate x509Certificate = new X509Certificate(certificate.RawData))
			{
				long subjectNameHash = X509Helper2.GetSubjectNameHash(x509Certificate);
				string text = Path.Combine(this._storePath, string.Format("{0:x8}.0", subjectNameHash));
				if (File.Exists(text))
				{
					File.Delete(text);
					this.ClearCertificates();
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AD6C File Offset: 0x00008F6C
		private string GetUniqueNameWithSerial(X509Certificate certificate)
		{
			return this.GetUniqueName(certificate, certificate.SerialNumber);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AD7C File Offset: 0x00008F7C
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

		// Token: 0x0600015F RID: 351 RVA: 0x0000ADBC File Offset: 0x00008FBC
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

		// Token: 0x06000160 RID: 352 RVA: 0x0000ADFC File Offset: 0x00008FFC
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

		// Token: 0x06000161 RID: 353 RVA: 0x0000AE68 File Offset: 0x00009068
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

		// Token: 0x06000162 RID: 354 RVA: 0x0000AEC4 File Offset: 0x000090C4
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

		// Token: 0x06000163 RID: 355 RVA: 0x0000AF18 File Offset: 0x00009118
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

		// Token: 0x06000164 RID: 356 RVA: 0x0000AFCC File Offset: 0x000091CC
		private X509Crl LoadCrl(string filename)
		{
			return new X509Crl(this.Load(filename));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000AFDC File Offset: 0x000091DC
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

		// Token: 0x06000166 RID: 358 RVA: 0x0000B020 File Offset: 0x00009220
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

		// Token: 0x06000167 RID: 359 RVA: 0x0000B0B4 File Offset: 0x000092B4
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

		// Token: 0x06000168 RID: 360 RVA: 0x0000B138 File Offset: 0x00009338
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

		// Token: 0x040000C0 RID: 192
		private string _storePath;

		// Token: 0x040000C1 RID: 193
		private X509CertificateCollection _certificates;

		// Token: 0x040000C2 RID: 194
		private ArrayList _crls;

		// Token: 0x040000C3 RID: 195
		private bool _crl;

		// Token: 0x040000C4 RID: 196
		private bool _newFormat;

		// Token: 0x040000C5 RID: 197
		private string _name;
	}
}
