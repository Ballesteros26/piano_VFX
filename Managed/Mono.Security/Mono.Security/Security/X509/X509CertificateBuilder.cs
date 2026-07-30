using System;
using System.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000014 RID: 20
	public class X509CertificateBuilder : X509Builder
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000097B3 File Offset: 0x000079B3
		public X509CertificateBuilder()
			: this(3)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000097BC File Offset: 0x000079BC
		public X509CertificateBuilder(byte version)
		{
			if (version > 3)
			{
				throw new ArgumentException("Invalid certificate version");
			}
			this.version = version;
			this.extensions = new X509ExtensionCollection();
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000097E5 File Offset: 0x000079E5
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000097ED File Offset: 0x000079ED
		public byte Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000097F6 File Offset: 0x000079F6
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000097FE File Offset: 0x000079FE
		public byte[] SerialNumber
		{
			get
			{
				return this.sn;
			}
			set
			{
				this.sn = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00009807 File Offset: 0x00007A07
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000980F File Offset: 0x00007A0F
		public string IssuerName
		{
			get
			{
				return this.issuer;
			}
			set
			{
				this.issuer = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00009818 File Offset: 0x00007A18
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00009820 File Offset: 0x00007A20
		public DateTime NotBefore
		{
			get
			{
				return this.notBefore;
			}
			set
			{
				this.notBefore = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00009829 File Offset: 0x00007A29
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00009831 File Offset: 0x00007A31
		public DateTime NotAfter
		{
			get
			{
				return this.notAfter;
			}
			set
			{
				this.notAfter = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000983A File Offset: 0x00007A3A
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00009842 File Offset: 0x00007A42
		public string SubjectName
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000984B File Offset: 0x00007A4B
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00009853 File Offset: 0x00007A53
		public AsymmetricAlgorithm SubjectPublicKey
		{
			get
			{
				return this.aa;
			}
			set
			{
				this.aa = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000985C File Offset: 0x00007A5C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00009864 File Offset: 0x00007A64
		public byte[] IssuerUniqueId
		{
			get
			{
				return this.issuerUniqueID;
			}
			set
			{
				this.issuerUniqueID = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000986D File Offset: 0x00007A6D
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00009875 File Offset: 0x00007A75
		public byte[] SubjectUniqueId
		{
			get
			{
				return this.subjectUniqueID;
			}
			set
			{
				this.subjectUniqueID = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000987E File Offset: 0x00007A7E
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00009888 File Offset: 0x00007A88
		private ASN1 SubjectPublicKeyInfo()
		{
			ASN1 asn = new ASN1(48);
			if (this.aa is RSA)
			{
				asn.Add(PKCS7.AlgorithmIdentifier("1.2.840.113549.1.1.1"));
				RSAParameters rsaparameters = (this.aa as RSA).ExportParameters(false);
				ASN1 asn2 = new ASN1(48);
				asn2.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Modulus));
				asn2.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Exponent));
				asn.Add(new ASN1(this.UniqueIdentifier(asn2.GetBytes())));
			}
			else
			{
				if (!(this.aa is DSA))
				{
					throw new NotSupportedException("Unknown Asymmetric Algorithm " + this.aa.ToString());
				}
				DSAParameters dsaparameters = (this.aa as DSA).ExportParameters(false);
				ASN1 asn3 = new ASN1(48);
				asn3.Add(ASN1Convert.FromUnsignedBigInteger(dsaparameters.P));
				asn3.Add(ASN1Convert.FromUnsignedBigInteger(dsaparameters.Q));
				asn3.Add(ASN1Convert.FromUnsignedBigInteger(dsaparameters.G));
				asn.Add(PKCS7.AlgorithmIdentifier("1.2.840.10040.4.1", asn3));
				asn.Add(new ASN1(3)).Add(ASN1Convert.FromUnsignedBigInteger(dsaparameters.Y));
			}
			return asn;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000099C8 File Offset: 0x00007BC8
		private byte[] UniqueIdentifier(byte[] id)
		{
			ASN1 asn = new ASN1(3);
			byte[] array = new byte[id.Length + 1];
			Buffer.BlockCopy(id, 0, array, 1, id.Length);
			asn.Value = array;
			return asn.GetBytes();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00009A00 File Offset: 0x00007C00
		protected override ASN1 ToBeSigned(string oid)
		{
			ASN1 asn = new ASN1(48);
			if (this.version > 1)
			{
				byte[] array = new byte[] { this.version - 1 };
				asn.Add(new ASN1(160)).Add(new ASN1(2, array));
			}
			asn.Add(new ASN1(2, this.sn));
			asn.Add(PKCS7.AlgorithmIdentifier(oid));
			asn.Add(X501.FromString(this.issuer));
			ASN1 asn2 = asn.Add(new ASN1(48));
			asn2.Add(ASN1Convert.FromDateTime(this.notBefore));
			asn2.Add(ASN1Convert.FromDateTime(this.notAfter));
			asn.Add(X501.FromString(this.subject));
			asn.Add(this.SubjectPublicKeyInfo());
			if (this.version > 1)
			{
				if (this.issuerUniqueID != null)
				{
					asn.Add(new ASN1(161, this.UniqueIdentifier(this.issuerUniqueID)));
				}
				if (this.subjectUniqueID != null)
				{
					asn.Add(new ASN1(161, this.UniqueIdentifier(this.subjectUniqueID)));
				}
				if (this.version > 2 && this.extensions.Count > 0)
				{
					asn.Add(new ASN1(163, this.extensions.GetBytes()));
				}
			}
			return asn;
		}

		// Token: 0x040000A5 RID: 165
		private byte version;

		// Token: 0x040000A6 RID: 166
		private byte[] sn;

		// Token: 0x040000A7 RID: 167
		private string issuer;

		// Token: 0x040000A8 RID: 168
		private DateTime notBefore;

		// Token: 0x040000A9 RID: 169
		private DateTime notAfter;

		// Token: 0x040000AA RID: 170
		private string subject;

		// Token: 0x040000AB RID: 171
		private AsymmetricAlgorithm aa;

		// Token: 0x040000AC RID: 172
		private byte[] issuerUniqueID;

		// Token: 0x040000AD RID: 173
		private byte[] subjectUniqueID;

		// Token: 0x040000AE RID: 174
		private X509ExtensionCollection extensions;
	}
}
