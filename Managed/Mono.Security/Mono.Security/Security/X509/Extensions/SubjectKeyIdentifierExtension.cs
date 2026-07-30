using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200002A RID: 42
	public class SubjectKeyIdentifierExtension : X509Extension
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000D253 File Offset: 0x0000B453
		public SubjectKeyIdentifierExtension()
		{
			this.extnOid = "2.5.29.14";
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D266 File Offset: 0x0000B466
		public SubjectKeyIdentifierExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D26F File Offset: 0x0000B46F
		public SubjectKeyIdentifierExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D278 File Offset: 0x0000B478
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 4)
			{
				throw new ArgumentException("Invalid SubjectKeyIdentifier extension");
			}
			this.ski = asn.Value;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
		protected override void Encode()
		{
			if (this.ski == null)
			{
				throw new InvalidOperationException("Invalid SubjectKeyIdentifier extension");
			}
			ASN1 asn = new ASN1(4, this.ski);
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000D2FE File Offset: 0x0000B4FE
		public override string Name
		{
			get
			{
				return "Subject Key Identifier";
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000D305 File Offset: 0x0000B505
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000D321 File Offset: 0x0000B521
		public byte[] Identifier
		{
			get
			{
				if (this.ski == null)
				{
					return null;
				}
				return (byte[])this.ski.Clone();
			}
			set
			{
				this.ski = value;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000D32C File Offset: 0x0000B52C
		public override string ToString()
		{
			if (this.ski == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.ski.Length; i++)
			{
				stringBuilder.Append(this.ski[i].ToString("X2", CultureInfo.InvariantCulture));
				if (i % 2 == 1)
				{
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000F6 RID: 246
		private byte[] ski;
	}
}
