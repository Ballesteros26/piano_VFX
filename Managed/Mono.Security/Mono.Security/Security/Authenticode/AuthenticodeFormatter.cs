using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x020000A6 RID: 166
	public class AuthenticodeFormatter : AuthenticodeBase
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0001DDEE File Offset: 0x0001BFEE
		public AuthenticodeFormatter()
		{
			this.certs = new X509CertificateCollection();
			this.crls = new ArrayList();
			this.authority = Authority.Maximum;
			this.pkcs7 = new PKCS7.SignedData();
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001DE1E File Offset: 0x0001C01E
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0001DE26 File Offset: 0x0001C026
		public Authority Authority
		{
			get
			{
				return this.authority;
			}
			set
			{
				this.authority = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001DE2F File Offset: 0x0001C02F
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001DE37 File Offset: 0x0001C037
		public ArrayList Crl
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0001DE3F File Offset: 0x0001C03F
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0001DE5C File Offset: 0x0001C05C
		public string Hash
		{
			get
			{
				if (this.hash == null)
				{
					this.hash = "SHA1";
				}
				return this.hash;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Hash");
				}
				string text = value.ToUpper(CultureInfo.InvariantCulture);
				if (text == "MD5" || text == "SHA1")
				{
					this.hash = text;
					return;
				}
				throw new ArgumentException("Invalid Authenticode hash algorithm");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001DEAF File Offset: 0x0001C0AF
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0001DEB7 File Offset: 0x0001C0B7
		public RSA RSA
		{
			get
			{
				return this.rsa;
			}
			set
			{
				this.rsa = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001DEC0 File Offset: 0x0001C0C0
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		public Uri TimestampUrl
		{
			get
			{
				return this.timestamp;
			}
			set
			{
				this.timestamp = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001DED1 File Offset: 0x0001C0D1
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0001DED9 File Offset: 0x0001C0D9
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001DEE2 File Offset: 0x0001C0E2
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0001DEEA File Offset: 0x0001C0EA
		public Uri Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001DEF3 File Offset: 0x0001C0F3
		private ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(5));
			return asn;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001DF16 File Offset: 0x0001C116
		private ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid(oid));
			asn.Add(new ASN1(49)).Add(value);
			return asn;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001DF40 File Offset: 0x0001C140
		private ASN1 Opus(string description, string url)
		{
			ASN1 asn = new ASN1(48);
			if (description != null)
			{
				asn.Add(new ASN1(160)).Add(new ASN1(128, Encoding.BigEndianUnicode.GetBytes(description)));
			}
			if (url != null)
			{
				asn.Add(new ASN1(161)).Add(new ASN1(128, Encoding.ASCII.GetBytes(url)));
			}
			return asn;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		private byte[] Header(byte[] fileHash, string hashAlgorithm)
		{
			string text = CryptoConfig.MapNameToOID(hashAlgorithm);
			ASN1 asn = new ASN1(48);
			ASN1 asn2 = asn.Add(new ASN1(48));
			asn2.Add(ASN1Convert.FromOid("1.3.6.1.4.1.311.2.1.15"));
			asn2.Add(new ASN1(48, AuthenticodeFormatter.obsolete));
			ASN1 asn3 = asn.Add(new ASN1(48));
			asn3.Add(this.AlgorithmIdentifier(text));
			asn3.Add(new ASN1(4, fileHash));
			this.pkcs7.HashName = hashAlgorithm;
			this.pkcs7.Certificates.AddRange(this.certs);
			this.pkcs7.ContentInfo.ContentType = "1.3.6.1.4.1.311.2.1.4";
			this.pkcs7.ContentInfo.Content.Add(asn);
			this.pkcs7.SignerInfo.Certificate = this.certs[0];
			this.pkcs7.SignerInfo.Key = this.rsa;
			ASN1 asn4;
			if (this.url == null)
			{
				asn4 = this.Attribute("1.3.6.1.4.1.311.2.1.12", this.Opus(this.description, null));
			}
			else
			{
				asn4 = this.Attribute("1.3.6.1.4.1.311.2.1.12", this.Opus(this.description, this.url.ToString()));
			}
			this.pkcs7.SignerInfo.AuthenticatedAttributes.Add(asn4);
			this.pkcs7.GetASN1();
			return this.pkcs7.SignerInfo.Signature;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001E12C File Offset: 0x0001C32C
		public ASN1 TimestampRequest(byte[] signature)
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
			contentInfo.Content.Add(new ASN1(4, signature));
			return PKCS7.AlgorithmIdentifier("1.3.6.1.4.1.311.3.2.1", contentInfo.ASN1);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001E168 File Offset: 0x0001C368
		public void ProcessTimestamp(byte[] response)
		{
			ASN1 asn = new ASN1(Convert.FromBase64String(Encoding.ASCII.GetString(response)));
			for (int i = 0; i < asn[1][0][3].Count; i++)
			{
				this.pkcs7.Certificates.Add(new X509Certificate(asn[1][0][3][i].GetBytes()));
			}
			this.pkcs7.SignerInfo.UnauthenticatedAttributes.Add(this.Attribute("1.2.840.113549.1.9.6", asn[1][0][4][0]));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001E21C File Offset: 0x0001C41C
		private byte[] Timestamp(byte[] signature)
		{
			ASN1 asn = this.TimestampRequest(signature);
			WebClient webClient = new WebClient();
			webClient.Headers.Add("Content-Type", "application/octet-stream");
			webClient.Headers.Add("Accept", "application/octet-stream");
			byte[] bytes = Encoding.ASCII.GetBytes(Convert.ToBase64String(asn.GetBytes()));
			return webClient.UploadData(this.timestamp.ToString(), bytes);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001E288 File Offset: 0x0001C488
		private bool Save(string fileName, byte[] asn)
		{
			File.Copy(fileName, fileName + ".bak", true);
			using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
			{
				int num;
				if (base.SecurityOffset > 0)
				{
					num = base.SecurityOffset;
				}
				else if (base.CoffSymbolTableOffset > 0)
				{
					fileStream.Seek((long)(base.PEOffset + 12), SeekOrigin.Begin);
					for (int i = 0; i < 8; i++)
					{
						fileStream.WriteByte(0);
					}
					num = base.CoffSymbolTableOffset;
				}
				else
				{
					num = (int)fileStream.Length;
				}
				int num2 = num & 7;
				if (num2 > 0)
				{
					num2 = 8 - num2;
				}
				byte[] array = BitConverterLE.GetBytes(num + num2);
				fileStream.Seek((long)(base.PEOffset + 152), SeekOrigin.Begin);
				fileStream.Write(array, 0, 4);
				int num3 = asn.Length + 8;
				int num4 = num3 & 7;
				if (num4 > 0)
				{
					num4 = 8 - num4;
				}
				array = BitConverterLE.GetBytes(num3 + num4);
				fileStream.Seek((long)(base.PEOffset + 156), SeekOrigin.Begin);
				fileStream.Write(array, 0, 4);
				fileStream.Seek((long)num, SeekOrigin.Begin);
				if (num2 > 0)
				{
					byte[] array2 = new byte[num2];
					fileStream.Write(array2, 0, array2.Length);
				}
				fileStream.Write(array, 0, array.Length);
				array = BitConverterLE.GetBytes(131584);
				fileStream.Write(array, 0, array.Length);
				fileStream.Write(asn, 0, asn.Length);
				if (num4 > 0)
				{
					byte[] array3 = new byte[num4];
					fileStream.Write(array3, 0, array3.Length);
				}
				fileStream.Close();
			}
			return true;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001E410 File Offset: 0x0001C610
		public bool Sign(string fileName)
		{
			try
			{
				base.Open(fileName);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.Hash);
				byte[] array = base.GetHash(hashAlgorithm);
				byte[] array2 = this.Header(array, this.Hash);
				if (this.timestamp != null)
				{
					byte[] array3 = this.Timestamp(array2);
					this.ProcessTimestamp(array3);
				}
				PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.2");
				contentInfo.Content.Add(this.pkcs7.ASN1);
				this.authenticode = contentInfo.ASN1;
				base.Close();
				return this.Save(fileName, this.authenticode.GetBytes());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			return false;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001E4CC File Offset: 0x0001C6CC
		public bool Timestamp(string fileName)
		{
			try
			{
				byte[] signature = new AuthenticodeDeformatter(fileName).Signature;
				if (signature != null)
				{
					base.Open(fileName);
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(signature);
					this.pkcs7 = new PKCS7.SignedData(contentInfo.Content);
					byte[] array = this.Timestamp(this.pkcs7.SignerInfo.Signature);
					ASN1 asn = new ASN1(Convert.FromBase64String(Encoding.ASCII.GetString(array)));
					ASN1 asn2 = new ASN1(signature);
					ASN1 asn3 = asn2.Element(1, 160);
					if (asn3 == null)
					{
						return false;
					}
					ASN1 asn4 = asn3.Element(0, 48);
					if (asn4 == null)
					{
						return false;
					}
					ASN1 asn5 = asn4.Element(3, 160);
					if (asn5 == null)
					{
						asn5 = new ASN1(160);
						asn4.Add(asn5);
					}
					for (int i = 0; i < asn[1][0][3].Count; i++)
					{
						asn5.Add(asn[1][0][3][i]);
					}
					ASN1 asn6 = asn4[asn4.Count - 1][0];
					ASN1 asn7 = asn6[asn6.Count - 1];
					if (asn7.Tag != 161)
					{
						asn7 = new ASN1(161);
						asn6.Add(asn7);
					}
					asn7.Add(this.Attribute("1.2.840.113549.1.9.6", asn[1][0][4][0]));
					return this.Save(fileName, asn2.GetBytes());
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			return false;
		}

		// Token: 0x04000413 RID: 1043
		private Authority authority;

		// Token: 0x04000414 RID: 1044
		private X509CertificateCollection certs;

		// Token: 0x04000415 RID: 1045
		private ArrayList crls;

		// Token: 0x04000416 RID: 1046
		private string hash;

		// Token: 0x04000417 RID: 1047
		private RSA rsa;

		// Token: 0x04000418 RID: 1048
		private Uri timestamp;

		// Token: 0x04000419 RID: 1049
		private ASN1 authenticode;

		// Token: 0x0400041A RID: 1050
		private PKCS7.SignedData pkcs7;

		// Token: 0x0400041B RID: 1051
		private string description;

		// Token: 0x0400041C RID: 1052
		private Uri url;

		// Token: 0x0400041D RID: 1053
		private const string signedData = "1.2.840.113549.1.7.2";

		// Token: 0x0400041E RID: 1054
		private const string countersignature = "1.2.840.113549.1.9.6";

		// Token: 0x0400041F RID: 1055
		private const string spcStatementType = "1.3.6.1.4.1.311.2.1.11";

		// Token: 0x04000420 RID: 1056
		private const string spcSpOpusInfo = "1.3.6.1.4.1.311.2.1.12";

		// Token: 0x04000421 RID: 1057
		private const string spcPelmageData = "1.3.6.1.4.1.311.2.1.15";

		// Token: 0x04000422 RID: 1058
		private const string commercialCodeSigning = "1.3.6.1.4.1.311.2.1.22";

		// Token: 0x04000423 RID: 1059
		private const string timestampCountersignature = "1.3.6.1.4.1.311.3.2.1";

		// Token: 0x04000424 RID: 1060
		private static byte[] obsolete = new byte[]
		{
			3, 1, 0, 160, 32, 162, 30, 128, 28, 0,
			60, 0, 60, 0, 60, 0, 79, 0, 98, 0,
			115, 0, 111, 0, 108, 0, 101, 0, 116, 0,
			101, 0, 62, 0, 62, 0, 62
		};
	}
}
