using System;
using System.Security;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000096 RID: 150
	internal class AuthenticodeDeformatter : AuthenticodeBase
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		public AuthenticodeDeformatter()
		{
			this.reason = -1;
			this.signerChain = new X509Chain();
			this.timestampChain = new X509Chain();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001C3CD File Offset: 0x0001A5CD
		public AuthenticodeDeformatter(string fileName)
			: this()
		{
			this.FileName = fileName;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		public string FileName
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.Reset();
				try
				{
					this.CheckSignature(value);
				}
				catch (SecurityException)
				{
					throw;
				}
				catch (Exception)
				{
					this.reason = 1;
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001C42C File Offset: 0x0001A62C
		public byte[] Hash
		{
			get
			{
				if (this.signedHash == null)
				{
					return null;
				}
				return (byte[])this.signedHash.Value.Clone();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001C44D File Offset: 0x0001A64D
		public int Reason
		{
			get
			{
				if (this.reason == -1)
				{
					this.IsTrusted();
				}
				return this.reason;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001C468 File Offset: 0x0001A668
		public bool IsTrusted()
		{
			if (this.entry == null)
			{
				this.reason = 1;
				return false;
			}
			if (this.signingCertificate == null)
			{
				this.reason = 7;
				return false;
			}
			if (this.signerChain.Root == null || !this.trustedRoot)
			{
				this.reason = 6;
				return false;
			}
			if (this.timestamp != DateTime.MinValue)
			{
				if (this.timestampChain.Root == null || !this.trustedTimestampRoot)
				{
					this.reason = 6;
					return false;
				}
				if (!this.signingCertificate.WasCurrent(this.Timestamp))
				{
					this.reason = 4;
					return false;
				}
			}
			else if (!this.signingCertificate.IsCurrent)
			{
				this.reason = 8;
				return false;
			}
			if (this.reason == -1)
			{
				this.reason = 0;
			}
			return true;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001C528 File Offset: 0x0001A728
		public byte[] Signature
		{
			get
			{
				if (this.entry == null)
				{
					return null;
				}
				return (byte[])this.entry.Clone();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001C544 File Offset: 0x0001A744
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001C54C File Offset: 0x0001A74C
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.coll;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001C554 File Offset: 0x0001A754
		public X509Certificate SigningCertificate
		{
			get
			{
				return this.signingCertificate;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001C55C File Offset: 0x0001A75C
		private bool CheckSignature(string fileName)
		{
			this.filename = fileName;
			base.Open(this.filename);
			this.entry = base.GetSecurityEntry();
			if (this.entry == null)
			{
				this.reason = 1;
				base.Close();
				return false;
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(this.entry);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				base.Close();
				return false;
			}
			PKCS7.SignedData signedData = new PKCS7.SignedData(contentInfo.Content);
			if (signedData.ContentInfo.ContentType != "1.3.6.1.4.1.311.2.1.4")
			{
				base.Close();
				return false;
			}
			this.coll = signedData.Certificates;
			ASN1 content = signedData.ContentInfo.Content;
			this.signedHash = content[0][1][1];
			int length = this.signedHash.Length;
			HashAlgorithm hashAlgorithm;
			if (length <= 20)
			{
				if (length == 16)
				{
					hashAlgorithm = MD5.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0167;
				}
				if (length == 20)
				{
					hashAlgorithm = SHA1.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0167;
				}
			}
			else
			{
				if (length == 32)
				{
					hashAlgorithm = SHA256.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0167;
				}
				if (length == 48)
				{
					hashAlgorithm = SHA384.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0167;
				}
				if (length == 64)
				{
					hashAlgorithm = SHA512.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0167;
				}
			}
			this.reason = 5;
			base.Close();
			return false;
			IL_0167:
			base.Close();
			if (!this.signedHash.CompareValue(this.hash))
			{
				this.reason = 2;
			}
			byte[] value = content[0].Value;
			hashAlgorithm.Initialize();
			byte[] array = hashAlgorithm.ComputeHash(value);
			return this.VerifySignature(signedData, array, hashAlgorithm) && this.reason == 0;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001C728 File Offset: 0x0001A928
		private bool CompareIssuerSerial(string issuer, byte[] serial, X509Certificate x509)
		{
			if (issuer != x509.IssuerName)
			{
				return false;
			}
			if (serial.Length != x509.SerialNumber.Length)
			{
				return false;
			}
			int num = serial.Length;
			for (int i = 0; i < serial.Length; i++)
			{
				if (serial[i] != x509.SerialNumber[--num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001C77C File Offset: 0x0001A97C
		private bool VerifySignature(PKCS7.SignedData sd, byte[] calculatedMessageDigest, HashAlgorithm ha)
		{
			string text = null;
			ASN1 asn = null;
			for (int i = 0; i < sd.SignerInfo.AuthenticatedAttributes.Count; i++)
			{
				ASN1 asn2 = (ASN1)sd.SignerInfo.AuthenticatedAttributes[i];
				string text2 = ASN1Convert.ToOid(asn2[0]);
				if (!(text2 == "1.2.840.113549.1.9.3"))
				{
					if (!(text2 == "1.2.840.113549.1.9.4"))
					{
						if (!(text2 == "1.3.6.1.4.1.311.2.1.11") && !(text2 == "1.3.6.1.4.1.311.2.1.12"))
						{
						}
					}
					else
					{
						asn = asn2[1][0];
					}
				}
				else
				{
					text = ASN1Convert.ToOid(asn2[1][0]);
				}
			}
			if (text != "1.3.6.1.4.1.311.2.1.4")
			{
				return false;
			}
			if (asn == null)
			{
				return false;
			}
			if (!asn.CompareValue(calculatedMessageDigest))
			{
				return false;
			}
			string text3 = CryptoConfig.MapNameToOID(ha.ToString());
			ASN1 asn3 = new ASN1(49);
			foreach (object obj in sd.SignerInfo.AuthenticatedAttributes)
			{
				ASN1 asn4 = (ASN1)obj;
				asn3.Add(asn4);
			}
			ha.Initialize();
			byte[] array = ha.ComputeHash(asn3.GetBytes());
			byte[] signature = sd.SignerInfo.Signature;
			string issuerName = sd.SignerInfo.IssuerName;
			byte[] serialNumber = sd.SignerInfo.SerialNumber;
			foreach (X509Certificate x509Certificate in this.coll)
			{
				if (this.CompareIssuerSerial(issuerName, serialNumber, x509Certificate) && x509Certificate.PublicKey.Length > signature.Length >> 3)
				{
					this.signingCertificate = x509Certificate;
					if (((RSACryptoServiceProvider)x509Certificate.RSA).VerifyHash(array, text3, signature))
					{
						this.signerChain.LoadCertificates(this.coll);
						this.trustedRoot = this.signerChain.Build(x509Certificate);
						break;
					}
				}
			}
			if (sd.SignerInfo.UnauthenticatedAttributes.Count == 0)
			{
				this.trustedTimestampRoot = true;
			}
			else
			{
				for (int j = 0; j < sd.SignerInfo.UnauthenticatedAttributes.Count; j++)
				{
					ASN1 asn5 = (ASN1)sd.SignerInfo.UnauthenticatedAttributes[j];
					string text4 = ASN1Convert.ToOid(asn5[0]);
					if (text4 == "1.2.840.113549.1.9.6")
					{
						PKCS7.SignerInfo signerInfo = new PKCS7.SignerInfo(asn5[1]);
						this.trustedTimestampRoot = this.VerifyCounterSignature(signerInfo, signature);
					}
				}
			}
			return this.trustedRoot && this.trustedTimestampRoot;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		private bool VerifyCounterSignature(PKCS7.SignerInfo cs, byte[] signature)
		{
			if (cs.Version > 1)
			{
				return false;
			}
			string text = null;
			ASN1 asn = null;
			for (int i = 0; i < cs.AuthenticatedAttributes.Count; i++)
			{
				ASN1 asn2 = (ASN1)cs.AuthenticatedAttributes[i];
				string text2 = ASN1Convert.ToOid(asn2[0]);
				if (!(text2 == "1.2.840.113549.1.9.3"))
				{
					if (!(text2 == "1.2.840.113549.1.9.4"))
					{
						if (text2 == "1.2.840.113549.1.9.5")
						{
							this.timestamp = ASN1Convert.ToDateTime(asn2[1][0]);
						}
					}
					else
					{
						asn = asn2[1][0];
					}
				}
				else
				{
					text = ASN1Convert.ToOid(asn2[1][0]);
				}
			}
			if (text != "1.2.840.113549.1.7.1")
			{
				return false;
			}
			if (asn == null)
			{
				return false;
			}
			string text3 = null;
			int length = asn.Length;
			if (length <= 20)
			{
				if (length != 16)
				{
					if (length == 20)
					{
						text3 = "SHA1";
					}
				}
				else
				{
					text3 = "MD5";
				}
			}
			else if (length != 32)
			{
				if (length != 48)
				{
					if (length == 64)
					{
						text3 = "SHA512";
					}
				}
				else
				{
					text3 = "SHA384";
				}
			}
			else
			{
				text3 = "SHA256";
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(text3);
			if (!asn.CompareValue(hashAlgorithm.ComputeHash(signature)))
			{
				return false;
			}
			byte[] signature2 = cs.Signature;
			ASN1 asn3 = new ASN1(49);
			foreach (object obj in cs.AuthenticatedAttributes)
			{
				ASN1 asn4 = (ASN1)obj;
				asn3.Add(asn4);
			}
			byte[] array = hashAlgorithm.ComputeHash(asn3.GetBytes());
			string issuerName = cs.IssuerName;
			byte[] serialNumber = cs.SerialNumber;
			foreach (X509Certificate x509Certificate in this.coll)
			{
				if (this.CompareIssuerSerial(issuerName, serialNumber, x509Certificate) && x509Certificate.PublicKey.Length > signature2.Length)
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate.RSA;
					RSAManaged rsamanaged = new RSAManaged();
					rsamanaged.ImportParameters(rsacryptoServiceProvider.ExportParameters(false));
					if (PKCS1.Verify_v15(rsamanaged, hashAlgorithm, array, signature2, true))
					{
						this.timestampChain.LoadCertificates(this.coll);
						return this.timestampChain.Build(x509Certificate);
					}
				}
			}
			return false;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001CCD8 File Offset: 0x0001AED8
		private void Reset()
		{
			this.filename = null;
			this.entry = null;
			this.hash = null;
			this.signedHash = null;
			this.signingCertificate = null;
			this.reason = -1;
			this.trustedRoot = false;
			this.trustedTimestampRoot = false;
			this.signerChain.Reset();
			this.timestampChain.Reset();
			this.timestamp = DateTime.MinValue;
		}

		// Token: 0x040005C6 RID: 1478
		private string filename;

		// Token: 0x040005C7 RID: 1479
		private byte[] hash;

		// Token: 0x040005C8 RID: 1480
		private X509CertificateCollection coll;

		// Token: 0x040005C9 RID: 1481
		private ASN1 signedHash;

		// Token: 0x040005CA RID: 1482
		private DateTime timestamp;

		// Token: 0x040005CB RID: 1483
		private X509Certificate signingCertificate;

		// Token: 0x040005CC RID: 1484
		private int reason;

		// Token: 0x040005CD RID: 1485
		private bool trustedRoot;

		// Token: 0x040005CE RID: 1486
		private bool trustedTimestampRoot;

		// Token: 0x040005CF RID: 1487
		private byte[] entry;

		// Token: 0x040005D0 RID: 1488
		private X509Chain signerChain;

		// Token: 0x040005D1 RID: 1489
		private X509Chain timestampChain;
	}
}
