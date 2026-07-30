using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000020 RID: 32
	public class CRLDistributionPointsExtension : X509Extension
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000B9EE File Offset: 0x00009BEE
		public CRLDistributionPointsExtension()
		{
			this.extnOid = "2.5.29.31";
			this.dps = new List<CRLDistributionPointsExtension.DistributionPoint>();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public CRLDistributionPointsExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000BA15 File Offset: 0x00009C15
		public CRLDistributionPointsExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000BA20 File Offset: 0x00009C20
		protected override void Decode()
		{
			this.dps = new List<CRLDistributionPointsExtension.DistributionPoint>();
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid CRLDistributionPoints extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				this.dps.Add(new CRLDistributionPointsExtension.DistributionPoint(asn[i]));
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000BA86 File Offset: 0x00009C86
		public override string Name
		{
			get
			{
				return "CRL Distribution Points";
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000BA8D File Offset: 0x00009C8D
		public IEnumerable<CRLDistributionPointsExtension.DistributionPoint> DistributionPoints
		{
			get
			{
				return this.dps;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000BA98 File Offset: 0x00009C98
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (CRLDistributionPointsExtension.DistributionPoint distributionPoint in this.dps)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(num++);
				stringBuilder.Append("]CRL Distribution Point");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\tDistribution Point Name:");
				stringBuilder.Append("\t\tFull Name:");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\t\t\t");
				stringBuilder.Append(distributionPoint.Name);
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000D9 RID: 217
		private List<CRLDistributionPointsExtension.DistributionPoint> dps;

		// Token: 0x020000DA RID: 218
		public class DistributionPoint
		{
			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000772 RID: 1906 RVA: 0x00021B7A File Offset: 0x0001FD7A
			// (set) Token: 0x06000773 RID: 1907 RVA: 0x00021B82 File Offset: 0x0001FD82
			public string Name { get; private set; }

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06000774 RID: 1908 RVA: 0x00021B8B File Offset: 0x0001FD8B
			// (set) Token: 0x06000775 RID: 1909 RVA: 0x00021B93 File Offset: 0x0001FD93
			public CRLDistributionPointsExtension.ReasonFlags Reasons { get; private set; }

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x06000776 RID: 1910 RVA: 0x00021B9C File Offset: 0x0001FD9C
			// (set) Token: 0x06000777 RID: 1911 RVA: 0x00021BA4 File Offset: 0x0001FDA4
			public string CRLIssuer { get; private set; }

			// Token: 0x06000778 RID: 1912 RVA: 0x00021BAD File Offset: 0x0001FDAD
			public DistributionPoint(string dp, CRLDistributionPointsExtension.ReasonFlags reasons, string issuer)
			{
				this.Name = dp;
				this.Reasons = reasons;
				this.CRLIssuer = issuer;
			}

			// Token: 0x06000779 RID: 1913 RVA: 0x00021BCC File Offset: 0x0001FDCC
			public DistributionPoint(ASN1 dp)
			{
				for (int i = 0; i < dp.Count; i++)
				{
					ASN1 asn = dp[i];
					switch (asn.Tag)
					{
					case 160:
					{
						for (int j = 0; j < asn.Count; j++)
						{
							ASN1 asn2 = asn[j];
							if (asn2.Tag == 160)
							{
								this.Name = new GeneralNames(asn2).ToString();
							}
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x020000DB RID: 219
		[Flags]
		public enum ReasonFlags
		{
			// Token: 0x040004D6 RID: 1238
			Unused = 0,
			// Token: 0x040004D7 RID: 1239
			KeyCompromise = 1,
			// Token: 0x040004D8 RID: 1240
			CACompromise = 2,
			// Token: 0x040004D9 RID: 1241
			AffiliationChanged = 3,
			// Token: 0x040004DA RID: 1242
			Superseded = 4,
			// Token: 0x040004DB RID: 1243
			CessationOfOperation = 5,
			// Token: 0x040004DC RID: 1244
			CertificateHold = 6,
			// Token: 0x040004DD RID: 1245
			PrivilegeWithdrawn = 7,
			// Token: 0x040004DE RID: 1246
			AACompromise = 8
		}
	}
}
