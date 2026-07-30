using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000027 RID: 39
	public class NetscapeCertTypeExtension : X509Extension
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000CDD5 File Offset: 0x0000AFD5
		public NetscapeCertTypeExtension()
		{
			this.extnOid = "2.16.840.1.113730.1.1";
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public NetscapeCertTypeExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		public NetscapeCertTypeExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 3)
			{
				throw new ArgumentException("Invalid NetscapeCertType extension");
			}
			int i = 1;
			while (i < asn.Value.Length)
			{
				this.ctbits = (this.ctbits << 8) + (int)asn.Value[i++];
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000CE58 File Offset: 0x0000B058
		public override string Name
		{
			get
			{
				return "NetscapeCertType";
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000CE60 File Offset: 0x0000B060
		public bool Support(NetscapeCertTypeExtension.CertTypes usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & this.ctbits) == num;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslClient))
			{
				stringBuilder.Append("SSL Client Authentication");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslServer))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SSL Server Authentication");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.Smime))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SMIME");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.ObjectSigning))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Object Signing");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SslCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SSL CA");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.SmimeCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SMIME CA");
			}
			if (this.Support(NetscapeCertTypeExtension.CertTypes.ObjectSigningCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Object Signing CA");
			}
			stringBuilder.Append("(");
			stringBuilder.Append(this.ctbits.ToString("X2", CultureInfo.InvariantCulture));
			stringBuilder.Append(")");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x040000F2 RID: 242
		private int ctbits;

		// Token: 0x020000DC RID: 220
		[Flags]
		public enum CertTypes
		{
			// Token: 0x040004E0 RID: 1248
			SslClient = 128,
			// Token: 0x040004E1 RID: 1249
			SslServer = 64,
			// Token: 0x040004E2 RID: 1250
			Smime = 32,
			// Token: 0x040004E3 RID: 1251
			ObjectSigning = 16,
			// Token: 0x040004E4 RID: 1252
			SslCA = 4,
			// Token: 0x040004E5 RID: 1253
			SmimeCA = 2,
			// Token: 0x040004E6 RID: 1254
			ObjectSigningCA = 1
		}
	}
}
