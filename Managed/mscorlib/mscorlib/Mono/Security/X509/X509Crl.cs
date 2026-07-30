using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x0200005A RID: 90
	internal class X509Crl
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00012622 File Offset: 0x00010822
		public X509Crl(byte[] crl)
		{
			if (crl == null)
			{
				throw new ArgumentNullException("crl");
			}
			this.encoded = (byte[])crl.Clone();
			this.Parse(this.encoded);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00012658 File Offset: 0x00010858
		private void Parse(byte[] crl)
		{
			string text = "Input data cannot be coded as a valid CRL.";
			try
			{
				ASN1 asn = new ASN1(this.encoded);
				if (asn.Tag != 48 || asn.Count != 3)
				{
					throw new CryptographicException(text);
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 48 || asn2.Count < 3)
				{
					throw new CryptographicException(text);
				}
				int num = 0;
				if (asn2[num].Tag == 2)
				{
					this.version = asn2[num++].Value[0] + 1;
				}
				else
				{
					this.version = 1;
				}
				this.signatureOID = ASN1Convert.ToOid(asn2[num++][0]);
				this.issuer = X501.ToString(asn2[num++]);
				this.thisUpdate = ASN1Convert.ToDateTime(asn2[num++]);
				ASN1 asn3 = asn2[num++];
				if (asn3.Tag == 23 || asn3.Tag == 24)
				{
					this.nextUpdate = ASN1Convert.ToDateTime(asn3);
					asn3 = asn2[num++];
				}
				this.entries = new ArrayList();
				if (asn3 != null && asn3.Tag == 48)
				{
					ASN1 asn4 = asn3;
					for (int i = 0; i < asn4.Count; i++)
					{
						this.entries.Add(new X509Crl.X509CrlEntry(asn4[i]));
					}
				}
				else
				{
					num--;
				}
				ASN1 asn5 = asn2[num];
				if (asn5 != null && asn5.Tag == 160 && asn5.Count == 1)
				{
					this.extensions = new X509ExtensionCollection(asn5[0]);
				}
				else
				{
					this.extensions = new X509ExtensionCollection(null);
				}
				string text2 = ASN1Convert.ToOid(asn[1][0]);
				if (this.signatureOID != text2)
				{
					throw new CryptographicException(text + " [Non-matching signature algorithms in CRL]");
				}
				byte[] value = asn[2].Value;
				this.signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, this.signature, 0, this.signature.Length);
			}
			catch
			{
				throw new CryptographicException(text);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00012898 File Offset: 0x00010A98
		public ArrayList Entries
		{
			get
			{
				return ArrayList.ReadOnly(this.entries);
			}
		}

		// Token: 0x17000076 RID: 118
		public X509Crl.X509CrlEntry this[int index]
		{
			get
			{
				return (X509Crl.X509CrlEntry)this.entries[index];
			}
		}

		// Token: 0x17000077 RID: 119
		public X509Crl.X509CrlEntry this[byte[] serialNumber]
		{
			get
			{
				return this.GetCrlEntry(serialNumber);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000128C1 File Offset: 0x00010AC1
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000128CC File Offset: 0x00010ACC
		public byte[] Hash
		{
			get
			{
				if (this.hash_value == null)
				{
					byte[] bytes = new ASN1(this.encoded)[0].GetBytes();
					using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromOid(this.signatureOID))
					{
						this.hash_value = hashAlgorithm.ComputeHash(bytes);
					}
				}
				return this.hash_value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00012934 File Offset: 0x00010B34
		public string IssuerName
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0001293C File Offset: 0x00010B3C
		public DateTime NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00012944 File Offset: 0x00010B44
		public DateTime ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0001294C File Offset: 0x00010B4C
		public string SignatureAlgorithm
		{
			get
			{
				return this.signatureOID;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00012954 File Offset: 0x00010B54
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
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00012970 File Offset: 0x00010B70
		public byte[] RawData
		{
			get
			{
				return (byte[])this.encoded.Clone();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00012982 File Offset: 0x00010B82
		public byte Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0001298A File Offset: 0x00010B8A
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.Now);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00012997 File Offset: 0x00010B97
		public bool WasCurrent(DateTime instant)
		{
			if (this.nextUpdate == DateTime.MinValue)
			{
				return instant >= this.thisUpdate;
			}
			return instant >= this.thisUpdate && instant <= this.nextUpdate;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00012970 File Offset: 0x00010B70
		public byte[] GetBytes()
		{
			return (byte[])this.encoded.Clone();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000129D4 File Offset: 0x00010BD4
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00012A14 File Offset: 0x00010C14
		public X509Crl.X509CrlEntry GetCrlEntry(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			return this.GetCrlEntry(x509.SerialNumber);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00012A30 File Offset: 0x00010C30
		public X509Crl.X509CrlEntry GetCrlEntry(byte[] serialNumber)
		{
			if (serialNumber == null)
			{
				throw new ArgumentNullException("serialNumber");
			}
			for (int i = 0; i < this.entries.Count; i++)
			{
				X509Crl.X509CrlEntry x509CrlEntry = (X509Crl.X509CrlEntry)this.entries[i];
				if (this.Compare(serialNumber, x509CrlEntry.SerialNumber))
				{
					return x509CrlEntry;
				}
			}
			return null;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00012A88 File Offset: 0x00010C88
		public bool VerifySignature(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			if (x509.Version >= 3)
			{
				BasicConstraintsExtension basicConstraintsExtension = null;
				X509Extension x509Extension = x509.Extensions["2.5.29.19"];
				if (x509Extension != null)
				{
					basicConstraintsExtension = new BasicConstraintsExtension(x509Extension);
					if (!basicConstraintsExtension.CertificateAuthority)
					{
						return false;
					}
				}
				x509Extension = x509.Extensions["2.5.29.15"];
				if (x509Extension != null)
				{
					KeyUsageExtension keyUsageExtension = new KeyUsageExtension(x509Extension);
					if (!keyUsageExtension.Support(KeyUsages.cRLSign) && (basicConstraintsExtension == null || !keyUsageExtension.Support(KeyUsages.digitalSignature)))
					{
						return false;
					}
				}
			}
			if (this.issuer != x509.SubjectName)
			{
				return false;
			}
			string text = this.signatureOID;
			if (text == "1.2.840.10040.4.3")
			{
				return this.VerifySignature(x509.DSA);
			}
			return this.VerifySignature(x509.RSA);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00012B4C File Offset: 0x00010D4C
		internal bool VerifySignature(DSA dsa)
		{
			if (this.signatureOID != "1.2.840.10040.4.3")
			{
				throw new CryptographicException("Unsupported hash algorithm: " + this.signatureOID);
			}
			DSASignatureDeformatter dsasignatureDeformatter = new DSASignatureDeformatter(dsa);
			dsasignatureDeformatter.SetHashAlgorithm("SHA1");
			ASN1 asn = new ASN1(this.signature);
			if (asn == null || asn.Count != 2)
			{
				return false;
			}
			byte[] value = asn[0].Value;
			byte[] value2 = asn[1].Value;
			byte[] array = new byte[40];
			int num = Math.Max(0, value.Length - 20);
			int num2 = Math.Max(0, 20 - value.Length);
			Buffer.BlockCopy(value, num, array, num2, value.Length - num);
			int num3 = Math.Max(0, value2.Length - 20);
			int num4 = Math.Max(20, 40 - value2.Length);
			Buffer.BlockCopy(value2, num3, array, num4, value2.Length - num3);
			return dsasignatureDeformatter.VerifySignature(this.Hash, array);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00012C3B File Offset: 0x00010E3B
		internal bool VerifySignature(RSA rsa)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(PKCS1.HashNameFromOid(this.signatureOID, true));
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.signature);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00012C68 File Offset: 0x00010E68
		public bool VerifySignature(AsymmetricAlgorithm aa)
		{
			if (aa == null)
			{
				throw new ArgumentNullException("aa");
			}
			if (aa is RSA)
			{
				return this.VerifySignature(aa as RSA);
			}
			if (aa is DSA)
			{
				return this.VerifySignature(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00012CC4 File Offset: 0x00010EC4
		public static X509Crl CreateFromFile(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return new X509Crl(array);
		}

		// Token: 0x040004E5 RID: 1253
		private string issuer;

		// Token: 0x040004E6 RID: 1254
		private byte version;

		// Token: 0x040004E7 RID: 1255
		private DateTime thisUpdate;

		// Token: 0x040004E8 RID: 1256
		private DateTime nextUpdate;

		// Token: 0x040004E9 RID: 1257
		private ArrayList entries;

		// Token: 0x040004EA RID: 1258
		private string signatureOID;

		// Token: 0x040004EB RID: 1259
		private byte[] signature;

		// Token: 0x040004EC RID: 1260
		private X509ExtensionCollection extensions;

		// Token: 0x040004ED RID: 1261
		private byte[] encoded;

		// Token: 0x040004EE RID: 1262
		private byte[] hash_value;

		// Token: 0x0200005B RID: 91
		public class X509CrlEntry
		{
			// Token: 0x060002E2 RID: 738 RVA: 0x00012D20 File Offset: 0x00010F20
			internal X509CrlEntry(byte[] serialNumber, DateTime revocationDate, X509ExtensionCollection extensions)
			{
				this.sn = serialNumber;
				this.revocationDate = revocationDate;
				if (extensions == null)
				{
					this.extensions = new X509ExtensionCollection();
					return;
				}
				this.extensions = extensions;
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x00012D4C File Offset: 0x00010F4C
			internal X509CrlEntry(ASN1 entry)
			{
				this.sn = entry[0].Value;
				Array.Reverse<byte>(this.sn);
				this.revocationDate = ASN1Convert.ToDateTime(entry[1]);
				this.extensions = new X509ExtensionCollection(entry[2]);
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060002E4 RID: 740 RVA: 0x00012DA0 File Offset: 0x00010FA0
			public byte[] SerialNumber
			{
				get
				{
					return (byte[])this.sn.Clone();
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060002E5 RID: 741 RVA: 0x00012DB2 File Offset: 0x00010FB2
			public DateTime RevocationDate
			{
				get
				{
					return this.revocationDate;
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x00012DBA File Offset: 0x00010FBA
			public X509ExtensionCollection Extensions
			{
				get
				{
					return this.extensions;
				}
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x00012DC4 File Offset: 0x00010FC4
			public byte[] GetBytes()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, this.sn));
				asn.Add(ASN1Convert.FromDateTime(this.revocationDate));
				if (this.extensions.Count > 0)
				{
					asn.Add(new ASN1(this.extensions.GetBytes()));
				}
				return asn.GetBytes();
			}

			// Token: 0x040004EF RID: 1263
			private byte[] sn;

			// Token: 0x040004F0 RID: 1264
			private DateTime revocationDate;

			// Token: 0x040004F1 RID: 1265
			private X509ExtensionCollection extensions;
		}
	}
}
