using System;
using System.Collections;
using System.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security
{
	// Token: 0x02000042 RID: 66
	internal sealed class PKCS7
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00002111 File Offset: 0x00000311
		private PKCS7()
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A070 File Offset: 0x00008270
		public static ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(49)).Add(value);
			return asn;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A09A File Offset: 0x0000829A
		public static ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(5));
			return asn;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A0BD File Offset: 0x000082BD
		public static ASN1 AlgorithmIdentifier(string oid, ASN1 parameters)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(parameters);
			return asn;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A0DC File Offset: 0x000082DC
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

		// Token: 0x02000043 RID: 67
		public class Oid
		{
			// Token: 0x0400043F RID: 1087
			public const string rsaEncryption = "1.2.840.113549.1.1.1";

			// Token: 0x04000440 RID: 1088
			public const string data = "1.2.840.113549.1.7.1";

			// Token: 0x04000441 RID: 1089
			public const string signedData = "1.2.840.113549.1.7.2";

			// Token: 0x04000442 RID: 1090
			public const string envelopedData = "1.2.840.113549.1.7.3";

			// Token: 0x04000443 RID: 1091
			public const string signedAndEnvelopedData = "1.2.840.113549.1.7.4";

			// Token: 0x04000444 RID: 1092
			public const string digestedData = "1.2.840.113549.1.7.5";

			// Token: 0x04000445 RID: 1093
			public const string encryptedData = "1.2.840.113549.1.7.6";

			// Token: 0x04000446 RID: 1094
			public const string contentType = "1.2.840.113549.1.9.3";

			// Token: 0x04000447 RID: 1095
			public const string messageDigest = "1.2.840.113549.1.9.4";

			// Token: 0x04000448 RID: 1096
			public const string signingTime = "1.2.840.113549.1.9.5";

			// Token: 0x04000449 RID: 1097
			public const string countersignature = "1.2.840.113549.1.9.6";
		}

		// Token: 0x02000044 RID: 68
		public class ContentInfo
		{
			// Token: 0x060001B4 RID: 436 RVA: 0x0000A15F File Offset: 0x0000835F
			public ContentInfo()
			{
				this.content = new ASN1(160);
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x0000A177 File Offset: 0x00008377
			public ContentInfo(string oid)
				: this()
			{
				this.contentType = oid;
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x0000A186 File Offset: 0x00008386
			public ContentInfo(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x0000A194 File Offset: 0x00008394
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

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000A22E File Offset: 0x0000842E
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A236 File Offset: 0x00008436
			// (set) Token: 0x060001BA RID: 442 RVA: 0x0000A23E File Offset: 0x0000843E
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

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060001BB RID: 443 RVA: 0x0000A247 File Offset: 0x00008447
			// (set) Token: 0x060001BC RID: 444 RVA: 0x0000A24F File Offset: 0x0000844F
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

			// Token: 0x060001BD RID: 445 RVA: 0x0000A258 File Offset: 0x00008458
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

			// Token: 0x060001BE RID: 446 RVA: 0x0000A2A3 File Offset: 0x000084A3
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x0400044A RID: 1098
			private string contentType;

			// Token: 0x0400044B RID: 1099
			private ASN1 content;
		}

		// Token: 0x02000045 RID: 69
		public class EncryptedData
		{
			// Token: 0x060001BF RID: 447 RVA: 0x0000A2B0 File Offset: 0x000084B0
			public EncryptedData()
			{
				this._version = 0;
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x0000A2BF File Offset: 0x000084BF
			public EncryptedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x0000A2D0 File Offset: 0x000084D0
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

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000A3E9 File Offset: 0x000085E9
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000A3F1 File Offset: 0x000085F1
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000A3F9 File Offset: 0x000085F9
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000A401 File Offset: 0x00008601
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

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000A41D File Offset: 0x0000861D
			// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000A425 File Offset: 0x00008625
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

			// Token: 0x060001C8 RID: 456 RVA: 0x0000A42E File Offset: 0x0000862E
			internal ASN1 GetASN1()
			{
				return null;
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x0000A431 File Offset: 0x00008631
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x0400044C RID: 1100
			private byte _version;

			// Token: 0x0400044D RID: 1101
			private PKCS7.ContentInfo _content;

			// Token: 0x0400044E RID: 1102
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x0400044F RID: 1103
			private byte[] _encrypted;
		}

		// Token: 0x02000046 RID: 70
		public class EnvelopedData
		{
			// Token: 0x060001CA RID: 458 RVA: 0x0000A43E File Offset: 0x0000863E
			public EnvelopedData()
			{
				this._version = 0;
				this._content = new PKCS7.ContentInfo();
				this._encryptionAlgorithm = new PKCS7.ContentInfo();
				this._recipientInfos = new ArrayList();
			}

			// Token: 0x060001CB RID: 459 RVA: 0x0000A46E File Offset: 0x0000866E
			public EnvelopedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x060001CC RID: 460 RVA: 0x0000A47C File Offset: 0x0000867C
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

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060001CD RID: 461 RVA: 0x0000A608 File Offset: 0x00008808
			public ArrayList RecipientInfos
			{
				get
				{
					return this._recipientInfos;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A610 File Offset: 0x00008810
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A618 File Offset: 0x00008818
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000A620 File Offset: 0x00008820
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A628 File Offset: 0x00008828
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

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A644 File Offset: 0x00008844
			// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000A64C File Offset: 0x0000884C
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

			// Token: 0x060001D4 RID: 468 RVA: 0x0000A655 File Offset: 0x00008855
			internal ASN1 GetASN1()
			{
				return new ASN1(48);
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x0000A65E File Offset: 0x0000885E
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000450 RID: 1104
			private byte _version;

			// Token: 0x04000451 RID: 1105
			private PKCS7.ContentInfo _content;

			// Token: 0x04000452 RID: 1106
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x04000453 RID: 1107
			private ArrayList _recipientInfos;

			// Token: 0x04000454 RID: 1108
			private byte[] _encrypted;
		}

		// Token: 0x02000047 RID: 71
		public class RecipientInfo
		{
			// Token: 0x060001D6 RID: 470 RVA: 0x00002111 File Offset: 0x00000311
			public RecipientInfo()
			{
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x0000A66C File Offset: 0x0000886C
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

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000A73C File Offset: 0x0000893C
			public string Oid
			{
				get
				{
					return this._oid;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A744 File Offset: 0x00008944
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

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A760 File Offset: 0x00008960
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

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A77C File Offset: 0x0000897C
			public string Issuer
			{
				get
				{
					return this._issuer;
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060001DC RID: 476 RVA: 0x0000A784 File Offset: 0x00008984
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

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A7A0 File Offset: 0x000089A0
			public int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x04000455 RID: 1109
			private int _version;

			// Token: 0x04000456 RID: 1110
			private string _oid;

			// Token: 0x04000457 RID: 1111
			private byte[] _key;

			// Token: 0x04000458 RID: 1112
			private byte[] _ski;

			// Token: 0x04000459 RID: 1113
			private string _issuer;

			// Token: 0x0400045A RID: 1114
			private byte[] _serial;
		}

		// Token: 0x02000048 RID: 72
		public class SignedData
		{
			// Token: 0x060001DE RID: 478 RVA: 0x0000A7A8 File Offset: 0x000089A8
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

			// Token: 0x060001DF RID: 479 RVA: 0x0000A7FC File Offset: 0x000089FC
			public SignedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x0000A80C File Offset: 0x00008A0C
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

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000A9EB File Offset: 0x00008BEB
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A9F3 File Offset: 0x00008BF3
			public X509CertificateCollection Certificates
			{
				get
				{
					return this.certs;
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A9FB File Offset: 0x00008BFB
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this.contentInfo;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000AA03 File Offset: 0x00008C03
			public ArrayList Crls
			{
				get
				{
					return this.crls;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000AA0B File Offset: 0x00008C0B
			// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000AA13 File Offset: 0x00008C13
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

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000AA28 File Offset: 0x00008C28
			public PKCS7.SignerInfo SignerInfo
			{
				get
				{
					return this.signerInfo;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000AA30 File Offset: 0x00008C30
			// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000AA38 File Offset: 0x00008C38
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

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060001EA RID: 490 RVA: 0x0000AA41 File Offset: 0x00008C41
			// (set) Token: 0x060001EB RID: 491 RVA: 0x0000AA49 File Offset: 0x00008C49
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

			// Token: 0x060001EC RID: 492 RVA: 0x0000AA54 File Offset: 0x00008C54
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

			// Token: 0x060001ED RID: 493 RVA: 0x0000AB40 File Offset: 0x00008D40
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

			// Token: 0x060001EE RID: 494 RVA: 0x0000ABC4 File Offset: 0x00008DC4
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

			// Token: 0x060001EF RID: 495 RVA: 0x0000AE64 File Offset: 0x00009064
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x0400045B RID: 1115
			private byte version;

			// Token: 0x0400045C RID: 1116
			private string hashAlgorithm;

			// Token: 0x0400045D RID: 1117
			private PKCS7.ContentInfo contentInfo;

			// Token: 0x0400045E RID: 1118
			private X509CertificateCollection certs;

			// Token: 0x0400045F RID: 1119
			private ArrayList crls;

			// Token: 0x04000460 RID: 1120
			private PKCS7.SignerInfo signerInfo;

			// Token: 0x04000461 RID: 1121
			private bool mda;

			// Token: 0x04000462 RID: 1122
			private bool signed;
		}

		// Token: 0x02000049 RID: 73
		public class SignerInfo
		{
			// Token: 0x060001F0 RID: 496 RVA: 0x0000AE71 File Offset: 0x00009071
			public SignerInfo()
			{
				this.version = 1;
				this.authenticatedAttributes = new ArrayList();
				this.unauthenticatedAttributes = new ArrayList();
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x0000AE96 File Offset: 0x00009096
			public SignerInfo(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x0000AEA4 File Offset: 0x000090A4
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

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000B055 File Offset: 0x00009255
			public string IssuerName
			{
				get
				{
					return this.issuer;
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000B05D File Offset: 0x0000925D
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

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000B079 File Offset: 0x00009279
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

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000B095 File Offset: 0x00009295
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000B09D File Offset: 0x0000929D
			public ArrayList AuthenticatedAttributes
			{
				get
				{
					return this.authenticatedAttributes;
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000B0A5 File Offset: 0x000092A5
			// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000B0AD File Offset: 0x000092AD
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

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060001FA RID: 506 RVA: 0x0000B0B6 File Offset: 0x000092B6
			// (set) Token: 0x060001FB RID: 507 RVA: 0x0000B0BE File Offset: 0x000092BE
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

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060001FC RID: 508 RVA: 0x0000B0C7 File Offset: 0x000092C7
			// (set) Token: 0x060001FD RID: 509 RVA: 0x0000B0CF File Offset: 0x000092CF
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

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060001FE RID: 510 RVA: 0x0000B0D8 File Offset: 0x000092D8
			// (set) Token: 0x060001FF RID: 511 RVA: 0x0000B0F4 File Offset: 0x000092F4
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

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000200 RID: 512 RVA: 0x0000B10A File Offset: 0x0000930A
			public ArrayList UnauthenticatedAttributes
			{
				get
				{
					return this.unauthenticatedAttributes;
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000201 RID: 513 RVA: 0x0000B112 File Offset: 0x00009312
			// (set) Token: 0x06000202 RID: 514 RVA: 0x0000B11A File Offset: 0x0000931A
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

			// Token: 0x06000203 RID: 515 RVA: 0x0000B124 File Offset: 0x00009324
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

			// Token: 0x06000204 RID: 516 RVA: 0x0000B344 File Offset: 0x00009544
			public byte[] GetBytes()
			{
				return this.GetASN1().GetBytes();
			}

			// Token: 0x04000463 RID: 1123
			private byte version;

			// Token: 0x04000464 RID: 1124
			private X509Certificate x509;

			// Token: 0x04000465 RID: 1125
			private string hashAlgorithm;

			// Token: 0x04000466 RID: 1126
			private AsymmetricAlgorithm key;

			// Token: 0x04000467 RID: 1127
			private ArrayList authenticatedAttributes;

			// Token: 0x04000468 RID: 1128
			private ArrayList unauthenticatedAttributes;

			// Token: 0x04000469 RID: 1129
			private byte[] signature;

			// Token: 0x0400046A RID: 1130
			private string issuer;

			// Token: 0x0400046B RID: 1131
			private byte[] serial;

			// Token: 0x0400046C RID: 1132
			private byte[] ski;
		}

		// Token: 0x0200004A RID: 74
		internal class SortedSet : IComparer
		{
			// Token: 0x06000205 RID: 517 RVA: 0x0000B354 File Offset: 0x00009554
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
