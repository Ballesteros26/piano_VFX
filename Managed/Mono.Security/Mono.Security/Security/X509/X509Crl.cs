using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x02000012 RID: 18
	public class X509Crl
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000084B4 File Offset: 0x000066B4
		public X509Crl(byte[] crl)
		{
			if (crl == null)
			{
				throw new ArgumentNullException("crl");
			}
			this.encoded = (byte[])crl.Clone();
			this.Parse(this.encoded);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000084E8 File Offset: 0x000066E8
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00008728 File Offset: 0x00006928
		public ArrayList Entries
		{
			get
			{
				return ArrayList.ReadOnly(this.entries);
			}
		}

		// Token: 0x17000016 RID: 22
		public X509Crl.X509CrlEntry this[int index]
		{
			get
			{
				return (X509Crl.X509CrlEntry)this.entries[index];
			}
		}

		// Token: 0x17000017 RID: 23
		public X509Crl.X509CrlEntry this[byte[] serialNumber]
		{
			get
			{
				return this.GetCrlEntry(serialNumber);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00008751 File Offset: 0x00006951
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000875C File Offset: 0x0000695C
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000087C4 File Offset: 0x000069C4
		public string IssuerName
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000087CC File Offset: 0x000069CC
		public DateTime NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000087D4 File Offset: 0x000069D4
		public DateTime ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000087DC File Offset: 0x000069DC
		public string SignatureAlgorithm
		{
			get
			{
				return this.signatureOID;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000087E4 File Offset: 0x000069E4
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00008800 File Offset: 0x00006A00
		public byte[] RawData
		{
			get
			{
				return (byte[])this.encoded.Clone();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00008812 File Offset: 0x00006A12
		public byte Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000881A File Offset: 0x00006A1A
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.Now);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00008827 File Offset: 0x00006A27
		public bool WasCurrent(DateTime instant)
		{
			if (this.nextUpdate == DateTime.MinValue)
			{
				return instant >= this.thisUpdate;
			}
			return instant >= this.thisUpdate && instant <= this.nextUpdate;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00008864 File Offset: 0x00006A64
		public byte[] GetBytes()
		{
			return (byte[])this.encoded.Clone();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00008878 File Offset: 0x00006A78
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

		// Token: 0x060000C8 RID: 200 RVA: 0x000088B8 File Offset: 0x00006AB8
		public X509Crl.X509CrlEntry GetCrlEntry(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			return this.GetCrlEntry(x509.SerialNumber);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000088D4 File Offset: 0x00006AD4
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

		// Token: 0x060000CA RID: 202 RVA: 0x0000892C File Offset: 0x00006B2C
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

		// Token: 0x060000CB RID: 203 RVA: 0x000089F0 File Offset: 0x00006BF0
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

		// Token: 0x060000CC RID: 204 RVA: 0x00008ADF File Offset: 0x00006CDF
		internal bool VerifySignature(RSA rsa)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(PKCS1.HashNameFromOid(this.signatureOID, true));
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.signature);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00008B0C File Offset: 0x00006D0C
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

		// Token: 0x060000CE RID: 206 RVA: 0x00008B68 File Offset: 0x00006D68
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

		// Token: 0x04000082 RID: 130
		private string issuer;

		// Token: 0x04000083 RID: 131
		private byte version;

		// Token: 0x04000084 RID: 132
		private DateTime thisUpdate;

		// Token: 0x04000085 RID: 133
		private DateTime nextUpdate;

		// Token: 0x04000086 RID: 134
		private ArrayList entries;

		// Token: 0x04000087 RID: 135
		private string signatureOID;

		// Token: 0x04000088 RID: 136
		private byte[] signature;

		// Token: 0x04000089 RID: 137
		private X509ExtensionCollection extensions;

		// Token: 0x0400008A RID: 138
		private byte[] encoded;

		// Token: 0x0400008B RID: 139
		private byte[] hash_value;

		// Token: 0x020000C5 RID: 197
		public class X509CrlEntry
		{
			// Token: 0x06000749 RID: 1865 RVA: 0x0002172E File Offset: 0x0001F92E
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

			// Token: 0x0600074A RID: 1866 RVA: 0x0002175C File Offset: 0x0001F95C
			internal X509CrlEntry(ASN1 entry)
			{
				this.sn = entry[0].Value;
				Array.Reverse<byte>(this.sn);
				this.revocationDate = ASN1Convert.ToDateTime(entry[1]);
				this.extensions = new X509ExtensionCollection(entry[2]);
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x0600074B RID: 1867 RVA: 0x000217B0 File Offset: 0x0001F9B0
			public byte[] SerialNumber
			{
				get
				{
					return (byte[])this.sn.Clone();
				}
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x0600074C RID: 1868 RVA: 0x000217C2 File Offset: 0x0001F9C2
			public DateTime RevocationDate
			{
				get
				{
					return this.revocationDate;
				}
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x0600074D RID: 1869 RVA: 0x000217CA File Offset: 0x0001F9CA
			public X509ExtensionCollection Extensions
			{
				get
				{
					return this.extensions;
				}
			}

			// Token: 0x0600074E RID: 1870 RVA: 0x000217D4 File Offset: 0x0001F9D4
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

			// Token: 0x040004C5 RID: 1221
			private byte[] sn;

			// Token: 0x040004C6 RID: 1222
			private DateTime revocationDate;

			// Token: 0x040004C7 RID: 1223
			private X509ExtensionCollection extensions;
		}
	}
}
