using System;
using System.Security;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x020000A5 RID: 165
	public class AuthenticodeDeformatter : AuthenticodeBase
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0001D458 File Offset: 0x0001B658
		public AuthenticodeDeformatter()
		{
			this.reason = -1;
			this.signerChain = new X509Chain();
			this.timestampChain = new X509Chain();
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001D47D File Offset: 0x0001B67D
		public AuthenticodeDeformatter(string fileName)
			: this()
		{
			this.FileName = fileName;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001D48C File Offset: 0x0001B68C
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001D494 File Offset: 0x0001B694
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001D4DC File Offset: 0x0001B6DC
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001D4FD File Offset: 0x0001B6FD
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

		// Token: 0x06000610 RID: 1552 RVA: 0x0001D518 File Offset: 0x0001B718
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

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
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

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001D5F4 File Offset: 0x0001B7F4
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001D5FC File Offset: 0x0001B7FC
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.coll;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001D604 File Offset: 0x0001B804
		public X509Certificate SigningCertificate
		{
			get
			{
				return this.signingCertificate;
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001D60C File Offset: 0x0001B80C
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

		// Token: 0x06000616 RID: 1558 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
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

		// Token: 0x06000617 RID: 1559 RVA: 0x0001D82C File Offset: 0x0001BA2C
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

		// Token: 0x06000618 RID: 1560 RVA: 0x0001DAFC File Offset: 0x0001BCFC
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

		// Token: 0x06000619 RID: 1561 RVA: 0x0001DD88 File Offset: 0x0001BF88
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

		// Token: 0x04000407 RID: 1031
		private string filename;

		// Token: 0x04000408 RID: 1032
		private byte[] hash;

		// Token: 0x04000409 RID: 1033
		private X509CertificateCollection coll;

		// Token: 0x0400040A RID: 1034
		private ASN1 signedHash;

		// Token: 0x0400040B RID: 1035
		private DateTime timestamp;

		// Token: 0x0400040C RID: 1036
		private X509Certificate signingCertificate;

		// Token: 0x0400040D RID: 1037
		private int reason;

		// Token: 0x0400040E RID: 1038
		private bool trustedRoot;

		// Token: 0x0400040F RID: 1039
		private bool trustedTimestampRoot;

		// Token: 0x04000410 RID: 1040
		private byte[] entry;

		// Token: 0x04000411 RID: 1041
		private X509Chain signerChain;

		// Token: 0x04000412 RID: 1042
		private X509Chain timestampChain;
	}
}
