using System;
using System.Collections;
using System.IO;
using System.Text;
using Mono.Security;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003AD RID: 941
	internal class X509Certificate2ImplMono : X509Certificate2Impl
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00070DA0 File Offset: 0x0006EFA0
		public override bool IsValid
		{
			get
			{
				return this._cert != null;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00070DAB File Offset: 0x0006EFAB
		public override IntPtr Handle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00070DAB File Offset: 0x0006EFAB
		public override IntPtr GetNativeAppleCertificate()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00070DB2 File Offset: 0x0006EFB2
		private X509Certificate2ImplMono(Mono.Security.X509.X509Certificate cert)
		{
			this._cert = cert;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00070DC1 File Offset: 0x0006EFC1
		private X509Certificate2ImplMono(X509Certificate2ImplMono other)
		{
			this._cert = other._cert;
			if (other.intermediateCerts != null)
			{
				this.intermediateCerts = other.intermediateCerts.Clone();
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00070DEE File Offset: 0x0006EFEE
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509Certificate2ImplMono(this);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00070DFC File Offset: 0x0006EFFC
		public override string GetIssuerName(bool legacyV1Mode)
		{
			base.ThrowIfContextInvalid();
			if (legacyV1Mode)
			{
				return this._cert.IssuerName;
			}
			return Mono.Security.X509.X501.ToString(this._cert.GetIssuerName(), true, ", ", true);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00070E2A File Offset: 0x0006F02A
		public override string GetSubjectName(bool legacyV1Mode)
		{
			base.ThrowIfContextInvalid();
			if (legacyV1Mode)
			{
				return this._cert.SubjectName;
			}
			return Mono.Security.X509.X501.ToString(this._cert.GetSubjectName(), true, ", ", true);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x00070E58 File Offset: 0x0006F058
		public override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			return this._cert.RawData;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x00070E6B File Offset: 0x0006F06B
		protected override byte[] GetCertHash(bool lazy)
		{
			base.ThrowIfContextInvalid();
			return SHA1.Create().ComputeHash(this._cert.RawData);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00070E88 File Offset: 0x0006F088
		public override DateTime GetValidFrom()
		{
			base.ThrowIfContextInvalid();
			return this._cert.ValidFrom;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00070E9B File Offset: 0x0006F09B
		public override DateTime GetValidUntil()
		{
			base.ThrowIfContextInvalid();
			return this._cert.ValidUntil;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00070EAE File Offset: 0x0006F0AE
		public override bool Equals(X509CertificateImpl other, out bool result)
		{
			result = false;
			return false;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00070EB4 File Offset: 0x0006F0B4
		public override string GetKeyAlgorithm()
		{
			base.ThrowIfContextInvalid();
			return this._cert.KeyAlgorithm;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00070EC7 File Offset: 0x0006F0C7
		public override byte[] GetKeyAlgorithmParameters()
		{
			base.ThrowIfContextInvalid();
			return this._cert.KeyAlgorithmParameters;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00070EDA File Offset: 0x0006F0DA
		public override byte[] GetPublicKey()
		{
			base.ThrowIfContextInvalid();
			return this._cert.PublicKey;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00070EED File Offset: 0x0006F0ED
		public override byte[] GetSerialNumber()
		{
			base.ThrowIfContextInvalid();
			return this._cert.SerialNumber;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00070F00 File Offset: 0x0006F100
		public override byte[] Export(X509ContentType contentType, byte[] password)
		{
			base.ThrowIfContextInvalid();
			switch (contentType)
			{
			case X509ContentType.Cert:
				return this.GetRawCertData();
			case X509ContentType.SerializedCert:
				throw new NotSupportedException();
			case X509ContentType.Pfx:
				throw new NotSupportedException();
			default:
				throw new CryptographicException(global::Locale.GetText("This certificate format '{0}' cannot be exported.", new object[] { contentType }));
			}
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00070F5A File Offset: 0x0006F15A
		public X509Certificate2ImplMono()
		{
			this._cert = null;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x00070F69 File Offset: 0x0006F169
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x00070F84 File Offset: 0x0006F184
		public override bool Archived
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				return this._archived;
			}
			set
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				this._archived = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00070FA0 File Offset: 0x0006F1A0
		public override global::System.Security.Cryptography.X509Certificates.X509ExtensionCollection Extensions
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (this._extensions == null)
				{
					this._extensions = new global::System.Security.Cryptography.X509Certificates.X509ExtensionCollection(this._cert);
				}
				return this._extensions;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x00070FD4 File Offset: 0x0006F1D4
		public override bool HasPrivateKey
		{
			get
			{
				return this.PrivateKey != null;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x00070FDF File Offset: 0x0006F1DF
		public override X500DistinguishedName IssuerName
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (this.issuer_name == null)
				{
					this.issuer_name = new X500DistinguishedName(this._cert.GetIssuerName().GetBytes());
				}
				return this.issuer_name;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x00071020 File Offset: 0x0006F220
		// (set) Token: 0x06001C82 RID: 7298 RVA: 0x00071120 File Offset: 0x0006F320
		public override AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				try
				{
					if (this._cert.RSA != null)
					{
						RSACryptoServiceProvider rsacryptoServiceProvider = this._cert.RSA as RSACryptoServiceProvider;
						if (rsacryptoServiceProvider != null)
						{
							return rsacryptoServiceProvider.PublicOnly ? null : rsacryptoServiceProvider;
						}
						Mono.Security.Cryptography.RSAManaged rsamanaged = this._cert.RSA as Mono.Security.Cryptography.RSAManaged;
						if (rsamanaged != null)
						{
							return rsamanaged.PublicOnly ? null : rsamanaged;
						}
						this._cert.RSA.ExportParameters(true);
						return this._cert.RSA;
					}
					else if (this._cert.DSA != null)
					{
						DSACryptoServiceProvider dsacryptoServiceProvider = this._cert.DSA as DSACryptoServiceProvider;
						if (dsacryptoServiceProvider != null)
						{
							return dsacryptoServiceProvider.PublicOnly ? null : dsacryptoServiceProvider;
						}
						this._cert.DSA.ExportParameters(true);
						return this._cert.DSA;
					}
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (value == null)
				{
					this._cert.RSA = null;
					this._cert.DSA = null;
					return;
				}
				if (value is RSA)
				{
					this._cert.RSA = (RSA)value;
					return;
				}
				if (value is DSA)
				{
					this._cert.DSA = (DSA)value;
					return;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00071198 File Offset: 0x0006F398
		public override PublicKey PublicKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (this._publicKey == null)
				{
					try
					{
						this._publicKey = new PublicKey(this._cert);
					}
					catch (Exception ex)
					{
						throw new CryptographicException(global::Locale.GetText("Unable to decode public key."), ex);
					}
				}
				return this._publicKey;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000711FC File Offset: 0x0006F3FC
		public override Oid SignatureAlgorithm
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (this.signature_algorithm == null)
				{
					this.signature_algorithm = new Oid(this._cert.SignatureAlgorithm);
				}
				return this.signature_algorithm;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00071235 File Offset: 0x0006F435
		public override X500DistinguishedName SubjectName
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (this.subject_name == null)
				{
					this.subject_name = new X500DistinguishedName(this._cert.GetSubjectName().GetBytes());
				}
				return this.subject_name;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00071273 File Offset: 0x0006F473
		public override int Version
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				return this._cert.Version;
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00071294 File Offset: 0x0006F494
		[MonoTODO("always return String.Empty for UpnName, DnsFromAlternativeName and UrlName")]
		public override string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			switch (nameType)
			{
			case X509NameType.SimpleName:
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				Mono.Security.ASN1 asn = (forIssuer ? this._cert.GetIssuerName() : this._cert.GetSubjectName());
				Mono.Security.ASN1 asn2 = this.Find(X509Certificate2ImplMono.commonName, asn);
				if (asn2 != null)
				{
					return this.GetValueAsString(asn2);
				}
				if (asn.Count == 0)
				{
					return string.Empty;
				}
				Mono.Security.ASN1 asn3 = asn[asn.Count - 1];
				if (asn3.Count == 0)
				{
					return string.Empty;
				}
				return this.GetValueAsString(asn3[0]);
			}
			case X509NameType.EmailName:
			{
				Mono.Security.ASN1 asn4 = this.Find(X509Certificate2ImplMono.email, forIssuer ? this._cert.GetIssuerName() : this._cert.GetSubjectName());
				if (asn4 != null)
				{
					return this.GetValueAsString(asn4);
				}
				return string.Empty;
			}
			case X509NameType.UpnName:
				return string.Empty;
			case X509NameType.DnsName:
			{
				Mono.Security.ASN1 asn5 = this.Find(X509Certificate2ImplMono.commonName, forIssuer ? this._cert.GetIssuerName() : this._cert.GetSubjectName());
				if (asn5 != null)
				{
					return this.GetValueAsString(asn5);
				}
				return string.Empty;
			}
			case X509NameType.DnsFromAlternativeName:
				return string.Empty;
			case X509NameType.UrlName:
				return string.Empty;
			default:
				throw new ArgumentException("nameType");
			}
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000713D4 File Offset: 0x0006F5D4
		private Mono.Security.ASN1 Find(byte[] oid, Mono.Security.ASN1 dn)
		{
			if (dn.Count == 0)
			{
				return null;
			}
			for (int i = 0; i < dn.Count; i++)
			{
				Mono.Security.ASN1 asn = dn[i];
				for (int j = 0; j < asn.Count; j++)
				{
					Mono.Security.ASN1 asn2 = asn[j];
					if (asn2.Count == 2)
					{
						Mono.Security.ASN1 asn3 = asn2[0];
						if (asn3 != null && asn3.CompareValue(oid))
						{
							return asn2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00071440 File Offset: 0x0006F640
		private string GetValueAsString(Mono.Security.ASN1 pair)
		{
			if (pair.Count != 2)
			{
				return string.Empty;
			}
			Mono.Security.ASN1 asn = pair[1];
			if (asn.Value == null || asn.Length == 0)
			{
				return string.Empty;
			}
			if (asn.Tag == 30)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 1; i < asn.Value.Length; i += 2)
				{
					stringBuilder.Append((char)asn.Value[i]);
				}
				return stringBuilder.ToString();
			}
			return Encoding.UTF8.GetString(asn.Value);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000714C4 File Offset: 0x0006F6C4
		private Mono.Security.X509.X509Certificate ImportPkcs12(byte[] rawData, string password)
		{
			Mono.Security.X509.PKCS12 pkcs = null;
			if (string.IsNullOrEmpty(password))
			{
				try
				{
					pkcs = new Mono.Security.X509.PKCS12(rawData, null);
					goto IL_002B;
				}
				catch
				{
					pkcs = new Mono.Security.X509.PKCS12(rawData, string.Empty);
					goto IL_002B;
				}
			}
			pkcs = new Mono.Security.X509.PKCS12(rawData, password);
			IL_002B:
			if (pkcs.Certificates.Count == 0)
			{
				return null;
			}
			if (pkcs.Keys.Count == 0)
			{
				return pkcs.Certificates[0];
			}
			Mono.Security.X509.X509Certificate x509Certificate = null;
			AsymmetricAlgorithm asymmetricAlgorithm = pkcs.Keys[0] as AsymmetricAlgorithm;
			string text = asymmetricAlgorithm.ToXmlString(false);
			foreach (Mono.Security.X509.X509Certificate x509Certificate2 in pkcs.Certificates)
			{
				if ((x509Certificate2.RSA != null && text == x509Certificate2.RSA.ToXmlString(false)) || (x509Certificate2.DSA != null && text == x509Certificate2.DSA.ToXmlString(false)))
				{
					x509Certificate = x509Certificate2;
					break;
				}
			}
			if (x509Certificate == null)
			{
				x509Certificate = pkcs.Certificates[0];
			}
			else
			{
				x509Certificate.RSA = asymmetricAlgorithm as RSA;
				x509Certificate.DSA = asymmetricAlgorithm as DSA;
			}
			if (pkcs.Certificates.Count > 1)
			{
				this.intermediateCerts = new X509CertificateImplCollection();
				foreach (Mono.Security.X509.X509Certificate x509Certificate3 in pkcs.Certificates)
				{
					if (x509Certificate3 != x509Certificate)
					{
						X509Certificate2ImplMono x509Certificate2ImplMono = new X509Certificate2ImplMono(x509Certificate3);
						this.intermediateCerts.Add(x509Certificate2ImplMono, true);
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0007167C File Offset: 0x0006F87C
		[MonoTODO("missing KeyStorageFlags support")]
		public override void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			Mono.Security.X509.X509Certificate x509Certificate = null;
			if (password == null)
			{
				try
				{
					x509Certificate = new Mono.Security.X509.X509Certificate(rawData);
					goto IL_004A;
				}
				catch (Exception ex)
				{
					try
					{
						x509Certificate = this.ImportPkcs12(rawData, null);
					}
					catch
					{
						throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex);
					}
					goto IL_004A;
				}
			}
			try
			{
				x509Certificate = this.ImportPkcs12(rawData, password);
			}
			catch
			{
				x509Certificate = new Mono.Security.X509.X509Certificate(rawData);
			}
			IL_004A:
			this._cert = x509Certificate;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00071704 File Offset: 0x0006F904
		[MonoTODO("X509ContentType.SerializedCert is not supported")]
		public override byte[] Export(X509ContentType contentType, string password)
		{
			if (this._cert == null)
			{
				throw new CryptographicException(X509Certificate2ImplMono.empty_error);
			}
			switch (contentType)
			{
			case X509ContentType.Cert:
				return this._cert.RawData;
			case X509ContentType.SerializedCert:
				throw new NotSupportedException();
			case X509ContentType.Pfx:
				return this.ExportPkcs12(password);
			default:
				throw new CryptographicException(global::Locale.GetText("This certificate format '{0}' cannot be exported.", new object[] { contentType }));
			}
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00071774 File Offset: 0x0006F974
		private byte[] ExportPkcs12(string password)
		{
			Mono.Security.X509.PKCS12 pkcs = new Mono.Security.X509.PKCS12();
			byte[] bytes;
			try
			{
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = arrayList;
				byte[] array = new byte[4];
				array[0] = 1;
				arrayList2.Add(array);
				hashtable.Add("1.2.840.113549.1.9.21", arrayList);
				if (password != null)
				{
					pkcs.Password = password;
				}
				pkcs.AddCertificate(this._cert, hashtable);
				AsymmetricAlgorithm privateKey = this.PrivateKey;
				if (privateKey != null)
				{
					pkcs.AddPkcs8ShroudedKeyBag(privateKey, hashtable);
				}
				bytes = pkcs.GetBytes();
			}
			finally
			{
				pkcs.Password = null;
			}
			return bytes;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00071800 File Offset: 0x0006FA00
		public override void Reset()
		{
			this._cert = null;
			this._archived = false;
			this._extensions = null;
			this._publicKey = null;
			this.issuer_name = null;
			this.subject_name = null;
			this.signature_algorithm = null;
			if (this.intermediateCerts != null)
			{
				this.intermediateCerts.Dispose();
				this.intermediateCerts = null;
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00071858 File Offset: 0x0006FA58
		public override string ToString()
		{
			if (this._cert == null)
			{
				return "System.Security.Cryptography.X509Certificates.X509Certificate2";
			}
			return this.ToString(true);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00071870 File Offset: 0x0006FA70
		public override string ToString(bool verbose)
		{
			if (this._cert == null)
			{
				return "System.Security.Cryptography.X509Certificates.X509Certificate2";
			}
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			if (!verbose)
			{
				stringBuilder.AppendFormat("[Subject]{0}  {1}{0}{0}", newLine, this.GetSubjectName(false));
				stringBuilder.AppendFormat("[Issuer]{0}  {1}{0}{0}", newLine, this.GetIssuerName(false));
				stringBuilder.AppendFormat("[Not Before]{0}  {1}{0}{0}", newLine, this.GetValidFrom().ToLocalTime());
				stringBuilder.AppendFormat("[Not After]{0}  {1}{0}{0}", newLine, this.GetValidUntil().ToLocalTime());
				stringBuilder.AppendFormat("[Thumbprint]{0}  {1}{0}", newLine, X509Helper.ToHexString(base.GetCertHash()));
				stringBuilder.Append(newLine);
				return stringBuilder.ToString();
			}
			stringBuilder.AppendFormat("[Version]{0}  V{1}{0}{0}", newLine, this.Version);
			stringBuilder.AppendFormat("[Subject]{0}  {1}{0}{0}", newLine, this.GetSubjectName(false));
			stringBuilder.AppendFormat("[Issuer]{0}  {1}{0}{0}", newLine, this.GetIssuerName(false));
			stringBuilder.AppendFormat("[Serial Number]{0}  {1}{0}{0}", newLine, this.GetSerialNumber());
			stringBuilder.AppendFormat("[Not Before]{0}  {1}{0}{0}", newLine, this.GetValidFrom().ToLocalTime());
			stringBuilder.AppendFormat("[Not After]{0}  {1}{0}{0}", newLine, this.GetValidUntil().ToLocalTime());
			stringBuilder.AppendFormat("[Thumbprint]{0}  {1}{0}", newLine, X509Helper.ToHexString(base.GetCertHash()));
			stringBuilder.AppendFormat("[Signature Algorithm]{0}  {1}({2}){0}{0}", newLine, this.SignatureAlgorithm.FriendlyName, this.SignatureAlgorithm.Value);
			AsymmetricAlgorithm key = this.PublicKey.Key;
			stringBuilder.AppendFormat("[Public Key]{0}  Algorithm: ", newLine);
			if (key is RSA)
			{
				stringBuilder.Append("RSA");
			}
			else if (key is DSA)
			{
				stringBuilder.Append("DSA");
			}
			else
			{
				stringBuilder.Append(key.ToString());
			}
			stringBuilder.AppendFormat("{0}  Length: {1}{0}  Key Blob: ", newLine, key.KeySize);
			X509Certificate2ImplMono.AppendBuffer(stringBuilder, this.PublicKey.EncodedKeyValue.RawData);
			stringBuilder.AppendFormat("{0}  Parameters: ", newLine);
			X509Certificate2ImplMono.AppendBuffer(stringBuilder, this.PublicKey.EncodedParameters.RawData);
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00071AAC File Offset: 0x0006FCAC
		private static void AppendBuffer(StringBuilder sb, byte[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				sb.Append(buffer[i].ToString("x2"));
				if (i < buffer.Length - 1)
				{
					sb.Append(" ");
				}
			}
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00071AF7 File Offset: 0x0006FCF7
		[MonoTODO("by default this depends on the incomplete X509Chain")]
		public override bool Verify(X509Certificate2 thisCertificate)
		{
			if (this._cert == null)
			{
				throw new CryptographicException(X509Certificate2ImplMono.empty_error);
			}
			return global::System.Security.Cryptography.X509Certificates.X509Chain.Create().Build(thisCertificate);
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00071B1C File Offset: 0x0006FD1C
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12, Pkcs7 and Unknown")]
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("rawData");
			}
			X509ContentType x509ContentType = X509ContentType.Unknown;
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(rawData);
				if (asn.Tag != 48)
				{
					throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."));
				}
				if (asn.Count == 0)
				{
					return x509ContentType;
				}
				if (asn.Count == 3)
				{
					byte tag = asn[0].Tag;
					if (tag != 2)
					{
						if (tag == 48 && asn[1].Tag == 48 && asn[2].Tag == 3)
						{
							x509ContentType = X509ContentType.Cert;
						}
					}
					else if (asn[1].Tag == 48 && asn[2].Tag == 48)
					{
						x509ContentType = X509ContentType.Pfx;
					}
				}
				if (asn[0].Tag == 6 && asn[0].CompareValue(X509Certificate2ImplMono.signedData))
				{
					x509ContentType = X509ContentType.Pkcs7;
				}
			}
			catch (Exception ex)
			{
				throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex);
			}
			return x509ContentType;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00071C20 File Offset: 0x0006FE20
		[MonoTODO("Detection limited to Cert, Pfx, Pkcs12 and Unknown")]
		public static X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("fileName");
			}
			return X509Certificate2ImplMono.GetCertContentType(File.ReadAllBytes(fileName));
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x00071C4E File Offset: 0x0006FE4E
		internal override X509CertificateImplCollection IntermediateCertificates
		{
			get
			{
				return this.intermediateCerts;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00071C56 File Offset: 0x0006FE56
		internal Mono.Security.X509.X509Certificate MonoCertificate
		{
			get
			{
				return this._cert;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00002068 File Offset: 0x00000268
		internal override X509Certificate2Impl FallbackImpl
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04001994 RID: 6548
		private bool _archived;

		// Token: 0x04001995 RID: 6549
		private global::System.Security.Cryptography.X509Certificates.X509ExtensionCollection _extensions;

		// Token: 0x04001996 RID: 6550
		private PublicKey _publicKey;

		// Token: 0x04001997 RID: 6551
		private X500DistinguishedName issuer_name;

		// Token: 0x04001998 RID: 6552
		private X500DistinguishedName subject_name;

		// Token: 0x04001999 RID: 6553
		private Oid signature_algorithm;

		// Token: 0x0400199A RID: 6554
		private X509CertificateImplCollection intermediateCerts;

		// Token: 0x0400199B RID: 6555
		private Mono.Security.X509.X509Certificate _cert;

		// Token: 0x0400199C RID: 6556
		private static string empty_error = global::Locale.GetText("Certificate instance is empty.");

		// Token: 0x0400199D RID: 6557
		private static byte[] commonName = new byte[] { 85, 4, 3 };

		// Token: 0x0400199E RID: 6558
		private static byte[] email = new byte[] { 42, 134, 72, 134, 247, 13, 1, 9, 1 };

		// Token: 0x0400199F RID: 6559
		private static byte[] signedData = new byte[] { 42, 134, 72, 134, 247, 13, 1, 7, 2 };
	}
}
