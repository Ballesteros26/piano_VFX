using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000028 RID: 40
	public class PrivateKeyUsagePeriodExtension : X509Extension
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000CFFD File Offset: 0x0000B1FD
		public PrivateKeyUsagePeriodExtension()
		{
			this.extnOid = "2.5.29.16";
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D010 File Offset: 0x0000B210
		public PrivateKeyUsagePeriodExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D019 File Offset: 0x0000B219
		public PrivateKeyUsagePeriodExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000D024 File Offset: 0x0000B224
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid PrivateKeyUsagePeriod extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				byte tag = asn[i].Tag;
				if (tag != 128)
				{
					if (tag != 129)
					{
						throw new ArgumentException("Invalid PrivateKeyUsagePeriod extension");
					}
					this.notAfter = ASN1Convert.ToDateTime(asn[i]);
				}
				else
				{
					this.notBefore = ASN1Convert.ToDateTime(asn[i]);
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000D0BA File Offset: 0x0000B2BA
		public override string Name
		{
			get
			{
				return "Private Key Usage Period";
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.notBefore != DateTime.MinValue)
			{
				stringBuilder.Append("Not Before: ");
				stringBuilder.Append(this.notBefore.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			if (this.notAfter != DateTime.MinValue)
			{
				stringBuilder.Append("Not After: ");
				stringBuilder.Append(this.notAfter.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000F3 RID: 243
		private DateTime notBefore;

		// Token: 0x040000F4 RID: 244
		private DateTime notAfter;
	}
}
