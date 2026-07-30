using System;
using System.Collections;
using System.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security
{
	// Token: 0x0200000A RID: 10
	public sealed class PKCS7
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000037FC File Offset: 0x000019FC
		private PKCS7()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003804 File Offset: 0x00001A04
		public static ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(49)).Add(value);
			return asn;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000382E File Offset: 0x00001A2E
		public static ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(5));
			return asn;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003851 File Offset: 0x00001A51
		public static ASN1 AlgorithmIdentifier(string oid, ASN1 parameters)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(parameters);
			return asn;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003870 File Offset: 0x00001A70
		public static ASN1 IssuerAndSerialNumber(X509Certificate x509)
		{
			ASN1 asn = null;
			ASN1 asn2 = null;
			ASN1 asn3 = new ASN1(x509.RawData);
			int i = 0;
			bool flag = false;
			while (i < asn3[0].Count)
			{
				ASN1 asn4 = asn3[0][i++];
				if (asn4.Tag == 2)
				{
					asn2 = asn4;
				}
				else if (asn4.Tag == 48)
				{
					if (flag)
					{
						asn = asn4;
						break;
					}
					flag = true;
				}
			}
			ASN1 asn5 = new ASN1(48);
			asn5.Add(asn);
			asn5.Add(asn2);
			return asn5;
		}

		// Token: 0x020000BA RID: 186
		public class Oid
		{
			// Token: 0x04000485 RID: 1157
			public const string rsaEncryption = "1.2.840.113549.1.1.1";

			// Token: 0x04000486 RID: 1158
			public const string data = "1.2.840.113549.1.7.1";

			// Token: 0x04000487 RID: 1159
			public const string signedData = "1.2.840.113549.1.7.2";

			// Token: 0x04000488 RID: 1160
			public const string envelopedData = "1.2.840.113549.1.7.3";

			// Token: 0x04000489 RID: 1161
			public const string signedAndEnvelopedData = "1.2.840.113549.1.7.4";

			// Token: 0x0400048A RID: 1162
			public const string digestedData = "1.2.840.113549.1.7.5";

			// Token: 0x0400048B RID: 1163
			public const string encryptedData = "1.2.840.113549.1.7.6";

			// Token: 0x0400048C RID: 1164
			public const string contentType = "1.2.840.113549.1.9.3";

			// Token: 0x0400048D RID: 1165
			public const string messageDigest = "1.2.840.113549.1.9.4";

			// Token: 0x0400048E RID: 1166
			public const string signingTime = "1.2.840.113549.1.9.5";

			// Token: 0x0400048F RID: 1167
			public const string countersignature = "1.2.840.113549.1.9.6";
		}

		// Token: 0x020000BB RID: 187
		public class ContentInfo
		{
			// Token: 0x060006D6 RID: 1750 RVA: 0x0002008F File Offset: 0x0001E28F
			public ContentInfo()
			{
				this.content = new ASN1(160);
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x000200A7 File Offset: 0x0001E2A7
			public ContentInfo(string oid)
				: this()
			{
				this.contentType = oid;
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x000200B6 File Offset: 0x0001E2B6
			public ContentInfo(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x000200C4 File Offset: 0x0001E2C4
			public ContentInfo(ASN1 asn1)
			{
				if (asn1.Tag != 48 || (asn1.Count < 1 && asn1.Count > 2))
				{
					throw new ArgumentException("Invalid ASN1");
				}
				if (asn1[0].Tag != 6)
				{
					throw new ArgumentException("Invalid contentType");
				}
				this.contentType = ASN1Convert.ToOid(asn1[0]);
				if (asn1.Count > 1)
				{
					if (asn1[1].Tag != 160)
					{
						throw new ArgumentException("Invalid content");
					}
					this.content = asn1[1];
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060006DA RID: 1754 RVA: 0x0002015E File Offset: 0x0001E35E
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060006DB RID: 1755 RVA: 0x00020166 File Offset: 0x0001E366
			// (set) Token: 0x060006DC RID: 1756 RVA: 0x0002016E File Offset: 0x0001E36E
			public ASN1 Content
			{
				get
				{
					return this.content;
				}
				set
				{
					this.content = value;
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x060006DD RID: 1757 RVA: 0x00020177 File Offset: 0x0001E377
			// (set) Token: 0x060006DE RID: 1758 RVA: 0x0002017F File Offset: 0x0001E37F
			public string ContentType
			{
				get
				{
					return this.contentType;
				}
				set
				{
					this.contentType = value;
				}
			}

			// Token: 0x060006DF RID: 1759 RVA: 0x00020188 File Offset: 0x0001E388
			internal ASN1 GetASN1()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.contentType));
				if (this.content != null && this.content.Count > 0)
				{
					asn.Add(this.content);
				}
				return asn;
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x000201D3 File Offset: 0x0001E3D3
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000490 RID: 1168
			private string contentType;

			// Token: 0x04000491 RID: 1169
			private ASN1 content;
		}

		// Token: 0x020000BC RID: 188
		public class EncryptedData
		{
			// Token: 0x060006E1 RID: 1761 RVA: 0x000201E0 File Offset: 0x0001E3E0
			public EncryptedData()
			{
				this._version = 0;
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x000201EF File Offset: 0x0001E3EF
			public EncryptedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x00020200 File Offset: 0x0001E400
			public EncryptedData(ASN1 asn1)
				: this()
			{
				if (asn1.Tag != 48 || asn1.Count < 2)
				{
					throw new ArgumentException("Invalid EncryptedData");
				}
				if (asn1[0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this._version = asn1[0].Value[0];
				ASN1 asn2 = asn1[1];
				if (asn2.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				this._content = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn3));
				ASN1 asn4 = asn2[1];
				if (asn4.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				this._encryptionAlgorithm = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn4[0]));
				this._encryptionAlgorithm.Content = asn4[1];
				ASN1 asn5 = asn2[2];
				if (asn5.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				this._encrypted = asn5.Value;
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00020319 File Offset: 0x0001E519
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00020321 File Offset: 0x0001E521
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00020329 File Offset: 0x0001E529
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00020331 File Offset: 0x0001E531
			public byte[] EncryptedContent
			{
				get
				{
					if (this._encrypted == null)
					{
						return null;
					}
					return (byte[])this._encrypted.Clone();
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0002034D File Offset: 0x0001E54D
			// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00020355 File Offset: 0x0001E555
			public byte Version
			{
				get
				{
					return this._version;
				}
				set
				{
					this._version = value;
				}
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x0002035E File Offset: 0x0001E55E
			internal ASN1 GetASN1()
			{
				return null;
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x00020361 File Offset: 0x0001E561
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000492 RID: 1170
			private byte _version;

			// Token: 0x04000493 RID: 1171
			private PKCS7.ContentInfo _content;

			// Token: 0x04000494 RID: 1172
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x04000495 RID: 1173
			private byte[] _encrypted;
		}

		// Token: 0x020000BD RID: 189
		public class EnvelopedData
		{
			// Token: 0x060006EC RID: 1772 RVA: 0x0002036E File Offset: 0x0001E56E
			public EnvelopedData()
			{
				this._version = 0;
				this._content = new PKCS7.ContentInfo();
				this._encryptionAlgorithm = new PKCS7.ContentInfo();
				this._recipientInfos = new ArrayList();
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x0002039E File Offset: 0x0001E59E
			public EnvelopedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x000203AC File Offset: 0x0001E5AC
			public EnvelopedData(ASN1 asn1)
				: this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 3)
				{
					throw new ArgumentException("Invalid EnvelopedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this._version = asn1[0][0].Value[0];
				ASN1 asn2 = asn1[0][1];
				if (asn2.Tag != 49)
				{
					throw new ArgumentException("missing RecipientInfos");
				}
				for (int i = 0; i < asn2.Count; i++)
				{
					ASN1 asn3 = asn2[i];
					this._recipientInfos.Add(new PKCS7.RecipientInfo(asn3));
				}
				ASN1 asn4 = asn1[0][2];
				if (asn4.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 asn5 = asn4[0];
				if (asn5.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				this._content = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn5));
				ASN1 asn6 = asn4[1];
				if (asn6.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				this._encryptionAlgorithm = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn6[0]));
				this._encryptionAlgorithm.Content = asn6[1];
				ASN1 asn7 = asn4[2];
				if (asn7.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				this._encrypted = asn7.Value;
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x060006EF RID: 1775 RVA: 0x00020538 File Offset: 0x0001E738
			public ArrayList RecipientInfos
			{
				get
				{
					return this._recipientInfos;
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00020540 File Offset: 0x0001E740
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00020548 File Offset: 0x0001E748
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00020550 File Offset: 0x0001E750
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00020558 File Offset: 0x0001E758
			public byte[] EncryptedContent
			{
				get
				{
					if (this._encrypted == null)
					{
						return null;
					}
					return (byte[])this._encrypted.Clone();
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00020574 File Offset: 0x0001E774
			// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0002057C File Offset: 0x0001E77C
			public byte Version
			{
				get
				{
					return this._version;
				}
				set
				{
					this._version = value;
				}
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x00020585 File Offset: 0x0001E785
			internal ASN1 GetASN1()
			{
				return new ASN1(48);
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x0002058E File Offset: 0x0001E78E
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000496 RID: 1174
			private byte _version;

			// Token: 0x04000497 RID: 1175
			private PKCS7.ContentInfo _content;

			// Token: 0x04000498 RID: 1176
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x04000499 RID: 1177
			private ArrayList _recipientInfos;

			// Token: 0x0400049A RID: 1178
			private byte[] _encrypted;
		}

		// Token: 0x020000BE RID: 190
		public class RecipientInfo
		{
			// Token: 0x060006F8 RID: 1784 RVA: 0x0002059B File Offset: 0x0001E79B
			public RecipientInfo()
			{
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x000205A4 File Offset: 0x0001E7A4
			public RecipientInfo(ASN1 data)
			{
				if (data.Tag != 48)
				{
					throw new ArgumentException("Invalid RecipientInfo");
				}
				ASN1 asn = data[0];
				if (asn.Tag != 2)
				{
					throw new ArgumentException("missing Version");
				}
				this._version = (int)asn.Value[0];
				ASN1 asn2 = data[1];
				if (asn2.Tag == 128 && this._version == 3)
				{
					this._ski = asn2.Value;
				}
				else
				{
					this._issuer = X501.ToString(asn2[0]);
					this._serial = asn2[1].Value;
				}
				ASN1 asn3 = data[2];
				this._oid = ASN1Convert.ToOid(asn3[0]);
				ASN1 asn4 = data[3];
				this._key = asn4.Value;
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x060006FA RID: 1786 RVA: 0x00020674 File Offset: 0x0001E874
			public string Oid
			{
				get
				{
					return this._oid;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x060006FB RID: 1787 RVA: 0x0002067C File Offset: 0x0001E87C
			public byte[] Key
			{
				get
				{
					if (this._key == null)
					{
						return null;
					}
					return (byte[])this._key.Clone();
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x060006FC RID: 1788 RVA: 0x00020698 File Offset: 0x0001E898
			public byte[] SubjectKeyIdentifier
			{
				get
				{
					if (this._ski == null)
					{
						return null;
					}
					return (byte[])this._ski.Clone();
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x060006FD RID: 1789 RVA: 0x000206B4 File Offset: 0x0001E8B4
			public string Issuer
			{
				get
				{
					return this._issuer;
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x060006FE RID: 1790 RVA: 0x000206BC File Offset: 0x0001E8BC
			public byte[] Serial
			{
				get
				{
					if (this._serial == null)
					{
						return null;
					}
					return (byte[])this._serial.Clone();
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x060006FF RID: 1791 RVA: 0x000206D8 File Offset: 0x0001E8D8
			public int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x0400049B RID: 1179
			private int _version;

			// Token: 0x0400049C RID: 1180
			private string _oid;

			// Token: 0x0400049D RID: 1181
			private byte[] _key;

			// Token: 0x0400049E RID: 1182
			private byte[] _ski;

			// Token: 0x0400049F RID: 1183
			private string _issuer;

			// Token: 0x040004A0 RID: 1184
			private byte[] _serial;
		}

		// Token: 0x020000BF RID: 191
		public class SignedData
		{
			// Token: 0x06000700 RID: 1792 RVA: 0x000206E0 File Offset: 0x0001E8E0
			public SignedData()
			{
				this.version = 1;
				this.contentInfo = new PKCS7.ContentInfo();
				this.certs = new X509CertificateCollection();
				this.crls = new ArrayList();
				this.signerInfo = new PKCS7.SignerInfo();
				this.mda = true;
				this.signed = false;
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x00020734 File Offset: 0x0001E934
			public SignedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x00020744 File Offset: 0x0001E944
			public SignedData(ASN1 asn1)
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 4)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this.version = asn1[0][0].Value[0];
				this.contentInfo = new PKCS7.ContentInfo(asn1[0][2]);
				int num = 3;
				this.certs = new X509CertificateCollection();
				if (asn1[0][num].Tag == 160)
				{
					for (int i = 0; i < asn1[0][num].Count; i++)
					{
						this.certs.Add(new X509Certificate(asn1[0][num][i].GetBytes()));
					}
					num++;
				}
				this.crls = new ArrayList();
				if (asn1[0][num].Tag == 161)
				{
					for (int j = 0; j < asn1[0][num].Count; j++)
					{
						this.crls.Add(asn1[0][num][j].GetBytes());
					}
					num++;
				}
				if (asn1[0][num].Count > 0)
				{
					this.signerInfo = new PKCS7.SignerInfo(asn1[0][num]);
				}
				else
				{
					this.signerInfo = new PKCS7.SignerInfo();
				}
				if (this.signerInfo.HashName != null)
				{
					this.HashName = this.OidToName(this.signerInfo.HashName);
				}
				this.mda = this.signerInfo.AuthenticatedAttributes.Count > 0;
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000703 RID: 1795 RVA: 0x00020923 File Offset: 0x0001EB23
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000704 RID: 1796 RVA: 0x0002092B File Offset: 0x0001EB2B
			public X509CertificateCollection Certificates
			{
				get
				{
					return this.certs;
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000705 RID: 1797 RVA: 0x00020933 File Offset: 0x0001EB33
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this.contentInfo;
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000706 RID: 1798 RVA: 0x0002093B File Offset: 0x0001EB3B
			public ArrayList Crls
			{
				get
				{
					return this.crls;
				}
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000707 RID: 1799 RVA: 0x00020943 File Offset: 0x0001EB43
			// (set) Token: 0x06000708 RID: 1800 RVA: 0x0002094B File Offset: 0x0001EB4B
			public string HashName
			{
				get
				{
					return this.hashAlgorithm;
				}
				set
				{
					this.hashAlgorithm = value;
					this.signerInfo.HashName = value;
				}
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000709 RID: 1801 RVA: 0x00020960 File Offset: 0x0001EB60
			public PKCS7.SignerInfo SignerInfo
			{
				get
				{
					return this.signerInfo;
				}
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x0600070A RID: 1802 RVA: 0x00020968 File Offset: 0x0001EB68
			// (set) Token: 0x0600070B RID: 1803 RVA: 0x00020970 File Offset: 0x0001EB70
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

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x0600070C RID: 1804 RVA: 0x00020979 File Offset: 0x0001EB79
			// (set) Token: 0x0600070D RID: 1805 RVA: 0x00020981 File Offset: 0x0001EB81
			public bool UseAuthenticatedAttributes
			{
				get
				{
					return this.mda;
				}
				set
				{
					this.mda = value;
				}
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x0002098C File Offset: 0x0001EB8C
			public bool VerifySignature(AsymmetricAlgorithm aa)
			{
				if (aa == null)
				{
					return false;
				}
				RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(aa);
				rsapkcs1SignatureDeformatter.SetHashAlgorithm(this.hashAlgorithm);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashAlgorithm);
				byte[] signature = this.signerInfo.Signature;
				byte[] array;
				if (this.mda)
				{
					ASN1 asn = new ASN1(49);
					foreach (object obj in this.signerInfo.AuthenticatedAttributes)
					{
						ASN1 asn2 = (ASN1)obj;
						asn.Add(asn2);
					}
					array = hashAlgorithm.ComputeHash(asn.GetBytes());
				}
				else
				{
					array = hashAlgorithm.ComputeHash(this.contentInfo.Content[0].Value);
				}
				return array != null && signature != null && rsapkcs1SignatureDeformatter.VerifySignature(array, signature);
			}

			// Token: 0x0600070F RID: 1807 RVA: 0x00020A78 File Offset: 0x0001EC78
			internal string OidToName(string oid)
			{
				if (oid == "1.3.14.3.2.26")
				{
					return "SHA1";
				}
				if (oid == "1.2.840.113549.2.2")
				{
					return "MD2";
				}
				if (oid == "1.2.840.113549.2.5")
				{
					return "MD5";
				}
				if (oid == "2.16.840.1.101.3.4.1")
				{
					return "SHA256";
				}
				if (oid == "2.16.840.1.101.3.4.2")
				{
					return "SHA384";
				}
				if (!(oid == "2.16.840.1.101.3.4.3"))
				{
					return oid;
				}
				return "SHA512";
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x00020AFC File Offset: 0x0001ECFC
			internal ASN1 GetASN1()
			{
				ASN1 asn = new ASN1(48);
				byte[] array = new byte[] { this.version };
				asn.Add(new ASN1(2, array));
				ASN1 asn2 = asn.Add(new ASN1(49));
				if (this.hashAlgorithm != null)
				{
					string text = CryptoConfig.MapNameToOID(this.hashAlgorithm);
					asn2.Add(PKCS7.AlgorithmIdentifier(text));
				}
				ASN1 asn3 = this.contentInfo.ASN1;
				asn.Add(asn3);
				if (!this.signed && this.hashAlgorithm != null)
				{
					if (this.mda)
					{
						ASN1 asn4 = PKCS7.Attribute("1.2.840.113549.1.9.3", asn3[0]);
						this.signerInfo.AuthenticatedAttributes.Add(asn4);
						byte[] array2 = HashAlgorithm.Create(this.hashAlgorithm).ComputeHash(asn3[1][0].Value);
						ASN1 asn5 = new ASN1(48);
						ASN1 asn6 = PKCS7.Attribute("1.2.840.113549.1.9.4", asn5.Add(new ASN1(4, array2)));
						this.signerInfo.AuthenticatedAttributes.Add(asn6);
					}
					else
					{
						RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.signerInfo.Key);
						rsapkcs1SignatureFormatter.SetHashAlgorithm(this.hashAlgorithm);
						byte[] array3 = HashAlgorithm.Create(this.hashAlgorithm).ComputeHash(asn3[1][0].Value);
						this.signerInfo.Signature = rsapkcs1SignatureFormatter.CreateSignature(array3);
					}
					this.signed = true;
				}
				if (this.certs.Count > 0)
				{
					ASN1 asn7 = asn.Add(new ASN1(160));
					foreach (X509Certificate x509Certificate in this.certs)
					{
						asn7.Add(new ASN1(x509Certificate.RawData));
					}
				}
				if (this.crls.Count > 0)
				{
					ASN1 asn8 = asn.Add(new ASN1(161));
					foreach (object obj in this.crls)
					{
						byte[] array4 = (byte[])obj;
						asn8.Add(new ASN1(array4));
					}
				}
				ASN1 asn9 = asn.Add(new ASN1(49));
				if (this.signerInfo.Key != null)
				{
					asn9.Add(this.signerInfo.ASN1);
				}
				return asn;
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x00020D9C File Offset: 0x0001EF9C
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040004A1 RID: 1185
			private byte version;

			// Token: 0x040004A2 RID: 1186
			private string hashAlgorithm;

			// Token: 0x040004A3 RID: 1187
			private PKCS7.ContentInfo contentInfo;

			// Token: 0x040004A4 RID: 1188
			private X509CertificateCollection certs;

			// Token: 0x040004A5 RID: 1189
			private ArrayList crls;

			// Token: 0x040004A6 RID: 1190
			private PKCS7.SignerInfo signerInfo;

			// Token: 0x040004A7 RID: 1191
			private bool mda;

			// Token: 0x040004A8 RID: 1192
			private bool signed;
		}

		// Token: 0x020000C0 RID: 192
		public class SignerInfo
		{
			// Token: 0x06000712 RID: 1810 RVA: 0x00020DA9 File Offset: 0x0001EFA9
			public SignerInfo()
			{
				this.version = 1;
				this.authenticatedAttributes = new ArrayList();
				this.unauthenticatedAttributes = new ArrayList();
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x00020DCE File Offset: 0x0001EFCE
			public SignerInfo(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x06000714 RID: 1812 RVA: 0x00020DDC File Offset: 0x0001EFDC
			public SignerInfo(ASN1 asn1)
				: this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 5)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this.version = asn1[0][0].Value[0];
				ASN1 asn2 = asn1[0][1];
				if (asn2.Tag == 128 && this.version == 3)
				{
					this.ski = asn2.Value;
				}
				else
				{
					this.issuer = X501.ToString(asn2[0]);
					this.serial = asn2[1].Value;
				}
				ASN1 asn3 = asn1[0][2];
				this.hashAlgorithm = ASN1Convert.ToOid(asn3[0]);
				int num = 3;
				ASN1 asn4 = asn1[0][num];
				if (asn4.Tag == 160)
				{
					num++;
					for (int i = 0; i < asn4.Count; i++)
					{
						this.authenticatedAttributes.Add(asn4[i]);
					}
				}
				num++;
				ASN1 asn5 = asn1[0][num++];
				if (asn5.Tag == 4)
				{
					this.signature = asn5.Value;
				}
				ASN1 asn6 = asn1[0][num];
				if (asn6 != null && asn6.Tag == 161)
				{
					for (int j = 0; j < asn6.Count; j++)
					{
						this.unauthenticatedAttributes.Add(asn6[j]);
					}
				}
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000715 RID: 1813 RVA: 0x00020F8D File Offset: 0x0001F18D
			public string IssuerName
			{
				get
				{
					return this.issuer;
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000716 RID: 1814 RVA: 0x00020F95 File Offset: 0x0001F195
			public byte[] SerialNumber
			{
				get
				{
					if (this.serial == null)
					{
						return null;
					}
					return (byte[])this.serial.Clone();
				}
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000717 RID: 1815 RVA: 0x00020FB1 File Offset: 0x0001F1B1
			public byte[] SubjectKeyIdentifier
			{
				get
				{
					if (this.ski == null)
					{
						return null;
					}
					return (byte[])this.ski.Clone();
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000718 RID: 1816 RVA: 0x00020FCD File Offset: 0x0001F1CD
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x06000719 RID: 1817 RVA: 0x00020FD5 File Offset: 0x0001F1D5
			public ArrayList AuthenticatedAttributes
			{
				get
				{
					return this.authenticatedAttributes;
				}
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x0600071A RID: 1818 RVA: 0x00020FDD File Offset: 0x0001F1DD
			// (set) Token: 0x0600071B RID: 1819 RVA: 0x00020FE5 File Offset: 0x0001F1E5
			public X509Certificate Certificate
			{
				get
				{
					return this.x509;
				}
				set
				{
					this.x509 = value;
				}
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x0600071C RID: 1820 RVA: 0x00020FEE File Offset: 0x0001F1EE
			// (set) Token: 0x0600071D RID: 1821 RVA: 0x00020FF6 File Offset: 0x0001F1F6
			public string HashName
			{
				get
				{
					return this.hashAlgorithm;
				}
				set
				{
					this.hashAlgorithm = value;
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x0600071E RID: 1822 RVA: 0x00020FFF File Offset: 0x0001F1FF
			// (set) Token: 0x0600071F RID: 1823 RVA: 0x00021007 File Offset: 0x0001F207
			public AsymmetricAlgorithm Key
			{
				get
				{
					return this.key;
				}
				set
				{
					this.key = value;
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000720 RID: 1824 RVA: 0x00021010 File Offset: 0x0001F210
			// (set) Token: 0x06000721 RID: 1825 RVA: 0x0002102C File Offset: 0x0001F22C
			public byte[] Signature
			{
				get
				{
					if (this.signature == null)
					{
						return null;
					}
					return (byte[])this.signature.Clone();
				}
				set
				{
					if (value != null)
					{
						this.signature = (byte[])value.Clone();
					}
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x00021042 File Offset: 0x0001F242
			public ArrayList UnauthenticatedAttributes
			{
				get
				{
					return this.unauthenticatedAttributes;
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000723 RID: 1827 RVA: 0x0002104A File Offset: 0x0001F24A
			// (set) Token: 0x06000724 RID: 1828 RVA: 0x00021052 File Offset: 0x0001F252
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

			// Token: 0x06000725 RID: 1829 RVA: 0x0002105C File Offset: 0x0001F25C
			internal ASN1 GetASN1()
			{
				if (this.key == null || this.hashAlgorithm == null)
				{
					return null;
				}
				byte[] array = new byte[] { this.version };
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, array));
				asn.Add(PKCS7.IssuerAndSerialNumber(this.x509));
				string text = CryptoConfig.MapNameToOID(this.hashAlgorithm);
				asn.Add(PKCS7.AlgorithmIdentifier(text));
				ASN1 asn2 = null;
				if (this.authenticatedAttributes.Count > 0)
				{
					asn2 = asn.Add(new ASN1(160));
					this.authenticatedAttributes.Sort(new PKCS7.SortedSet());
					foreach (object obj in this.authenticatedAttributes)
					{
						ASN1 asn3 = (ASN1)obj;
						asn2.Add(asn3);
					}
				}
				if (this.key is RSA)
				{
					asn.Add(PKCS7.AlgorithmIdentifier("1.2.840.113549.1.1.1"));
					if (asn2 != null)
					{
						RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.key);
						rsapkcs1SignatureFormatter.SetHashAlgorithm(this.hashAlgorithm);
						byte[] bytes = asn2.GetBytes();
						bytes[0] = 49;
						byte[] array2 = HashAlgorithm.Create(this.hashAlgorithm).ComputeHash(bytes);
						this.signature = rsapkcs1SignatureFormatter.CreateSignature(array2);
					}
					asn.Add(new ASN1(4, this.signature));
					if (this.unauthenticatedAttributes.Count > 0)
					{
						ASN1 asn4 = asn.Add(new ASN1(161));
						this.unauthenticatedAttributes.Sort(new PKCS7.SortedSet());
						foreach (object obj2 in this.unauthenticatedAttributes)
						{
							ASN1 asn5 = (ASN1)obj2;
							asn4.Add(asn5);
						}
					}
					return asn;
				}
				if (this.key is DSA)
				{
					throw new NotImplementedException("not yet");
				}
				throw new CryptographicException("Unknown assymetric algorithm");
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x0002127C File Offset: 0x0001F47C
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x040004A9 RID: 1193
			private byte version;

			// Token: 0x040004AA RID: 1194
			private X509Certificate x509;

			// Token: 0x040004AB RID: 1195
			private string hashAlgorithm;

			// Token: 0x040004AC RID: 1196
			private AsymmetricAlgorithm key;

			// Token: 0x040004AD RID: 1197
			private ArrayList authenticatedAttributes;

			// Token: 0x040004AE RID: 1198
			private ArrayList unauthenticatedAttributes;

			// Token: 0x040004AF RID: 1199
			private byte[] signature;

			// Token: 0x040004B0 RID: 1200
			private string issuer;

			// Token: 0x040004B1 RID: 1201
			private byte[] serial;

			// Token: 0x040004B2 RID: 1202
			private byte[] ski;
		}

		// Token: 0x020000C1 RID: 193
		internal class SortedSet : IComparer
		{
			// Token: 0x06000727 RID: 1831 RVA: 0x0002128C File Offset: 0x0001F48C
			public int Compare(object x, object y)
			{
				if (x == null)
				{
					if (y != null)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (y == null)
					{
						return 1;
					}
					ASN1 asn = x as ASN1;
					ASN1 asn2 = y as ASN1;
					if (asn == null || asn2 == null)
					{
						throw new ArgumentException(Locale.GetText("Invalid objects."));
					}
					byte[] bytes = asn.GetBytes();
					byte[] bytes2 = asn2.GetBytes();
					int num = 0;
					while (num < bytes.Length && num != bytes2.Length)
					{
						if (bytes[num] != bytes2[num])
						{
							if (bytes[num] >= bytes2[num])
							{
								return 1;
							}
							return -1;
						}
						else
						{
							num++;
						}
					}
					if (bytes.Length > bytes2.Length)
					{
						return 1;
					}
					if (bytes.Length < bytes2.Length)
					{
						return -1;
					}
					return 0;
				}
			}
		}
	}
}
