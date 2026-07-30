using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200007D RID: 125
	internal class SubjectKeyIdentifierExtension : X509Extension
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00015C81 File Offset: 0x00013E81
		public SubjectKeyIdentifierExtension()
		{
			this.extnOid = "2.5.29.14";
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015724 File Offset: 0x00013924
		public SubjectKeyIdentifierExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001572D File Offset: 0x0001392D
		public SubjectKeyIdentifierExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00015C94 File Offset: 0x00013E94
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 4)
			{
				throw new ArgumentException("Invalid SubjectKeyIdentifier extension");
			}
			this.ski = asn.Value;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00015CD4 File Offset: 0x00013ED4
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00015D1A File Offset: 0x00013F1A
		public override string Name
		{
			get
			{
				return "Subject Key Identifier";
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00015D21 File Offset: 0x00013F21
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00015D3D File Offset: 0x00013F3D
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

		// Token: 0x060003C1 RID: 961 RVA: 0x00015D48 File Offset: 0x00013F48
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

		// Token: 0x0400054A RID: 1354
		private byte[] ski;
	}
}
