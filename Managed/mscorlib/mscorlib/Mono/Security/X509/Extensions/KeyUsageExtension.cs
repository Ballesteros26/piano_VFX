using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200007C RID: 124
	internal class KeyUsageExtension : X509Extension
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00015724 File Offset: 0x00013924
		public KeyUsageExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001572D File Offset: 0x0001392D
		public KeyUsageExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001591E File Offset: 0x00013B1E
		public KeyUsageExtension()
		{
			this.extnOid = "2.5.29.15";
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00015934 File Offset: 0x00013B34
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 3)
			{
				throw new ArgumentException("Invalid KeyUsage extension");
			}
			int i = 1;
			while (i < asn.Value.Length)
			{
				this.kubits = (this.kubits << 8) + (int)asn.Value[i++];
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00015990 File Offset: 0x00013B90
		protected override void Encode()
		{
			this.extnValue = new ASN1(4);
			ushort num = (ushort)this.kubits;
			if (num <= 0)
			{
				ASN1 extnValue = this.extnValue;
				byte b = 3;
				byte[] array = new byte[2];
				array[0] = 7;
				extnValue.Add(new ASN1(b, array));
				return;
			}
			byte b2 = 15;
			while (b2 > 0 && (num & 32768) != 32768)
			{
				num = (ushort)(num << 1);
				b2 -= 1;
			}
			if (this.kubits > 255)
			{
				b2 -= 8;
				this.extnValue.Add(new ASN1(3, new byte[]
				{
					b2,
					(byte)this.kubits,
					(byte)(this.kubits >> 8)
				}));
				return;
			}
			this.extnValue.Add(new ASN1(3, new byte[]
			{
				b2,
				(byte)this.kubits
			}));
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00015A64 File Offset: 0x00013C64
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00015A6C File Offset: 0x00013C6C
		public KeyUsages KeyUsage
		{
			get
			{
				return (KeyUsages)this.kubits;
			}
			set
			{
				this.kubits = Convert.ToInt32(value, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00015A84 File Offset: 0x00013C84
		public override string Name
		{
			get
			{
				return "Key Usage";
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00015A8C File Offset: 0x00013C8C
		public bool Support(KeyUsages usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.kubits) == num;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00015AB8 File Offset: 0x00013CB8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Support(KeyUsages.digitalSignature))
			{
				stringBuilder.Append("Digital Signature");
			}
			if (this.Support(KeyUsages.nonRepudiation))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Non-Repudiation");
			}
			if (this.Support(KeyUsages.keyEncipherment))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Key Encipherment");
			}
			if (this.Support(KeyUsages.dataEncipherment))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Data Encipherment");
			}
			if (this.Support(KeyUsages.keyAgreement))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Key Agreement");
			}
			if (this.Support(KeyUsages.keyCertSign))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Certificate Signing");
			}
			if (this.Support(KeyUsages.cRLSign))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("CRL Signing");
			}
			if (this.Support(KeyUsages.encipherOnly))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Encipher Only ");
			}
			if (this.Support(KeyUsages.decipherOnly))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Decipher Only");
			}
			stringBuilder.Append("(");
			stringBuilder.Append(this.kubits.ToString("X2", CultureInfo.InvariantCulture));
			stringBuilder.Append(")");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x04000549 RID: 1353
		private int kubits;
	}
}
