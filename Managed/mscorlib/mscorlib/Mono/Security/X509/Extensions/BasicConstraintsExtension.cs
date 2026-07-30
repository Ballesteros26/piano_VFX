using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200007A RID: 122
	internal class BasicConstraintsExtension : X509Extension
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0001570A File Offset: 0x0001390A
		public BasicConstraintsExtension()
		{
			this.extnOid = "2.5.29.19";
			this.pathLenConstraint = -1;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00015724 File Offset: 0x00013924
		public BasicConstraintsExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001572D File Offset: 0x0001392D
		public BasicConstraintsExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015738 File Offset: 0x00013938
		protected override void Decode()
		{
			this.cA = false;
			this.pathLenConstraint = -1;
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid BasicConstraints extension");
			}
			int num = 0;
			ASN1 asn2 = asn[num++];
			if (asn2 != null && asn2.Tag == 1)
			{
				this.cA = asn2.Value[0] == byte.MaxValue;
				asn2 = asn[num++];
			}
			if (asn2 != null && asn2.Tag == 2)
			{
				this.pathLenConstraint = ASN1Convert.ToInt32(asn2);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000157CC File Offset: 0x000139CC
		protected override void Encode()
		{
			ASN1 asn = new ASN1(48);
			if (this.cA)
			{
				asn.Add(new ASN1(1, new byte[] { byte.MaxValue }));
			}
			if (this.cA && this.pathLenConstraint >= 0)
			{
				asn.Add(ASN1Convert.FromInt32(this.pathLenConstraint));
			}
			this.extnValue = new ASN1(4);
			this.extnValue.Add(asn);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00015840 File Offset: 0x00013A40
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00015848 File Offset: 0x00013A48
		public bool CertificateAuthority
		{
			get
			{
				return this.cA;
			}
			set
			{
				this.cA = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00015851 File Offset: 0x00013A51
		public override string Name
		{
			get
			{
				return "Basic Constraints";
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00015858 File Offset: 0x00013A58
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00015860 File Offset: 0x00013A60
		public int PathLenConstraint
		{
			get
			{
				return this.pathLenConstraint;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException(Locale.GetText("PathLenConstraint must be positive or -1 for none ({0}).", new object[] { value }));
				}
				this.pathLenConstraint = value;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001588C File Offset: 0x00013A8C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Subject Type=");
			stringBuilder.Append(this.cA ? "CA" : "End Entity");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("Path Length Constraint=");
			if (this.pathLenConstraint == -1)
			{
				stringBuilder.Append("None");
			}
			else
			{
				stringBuilder.Append(this.pathLenConstraint.ToString(CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x0400053B RID: 1339
		public const int NoPathLengthConstraint = -1;

		// Token: 0x0400053C RID: 1340
		private bool cA;

		// Token: 0x0400053D RID: 1341
		private int pathLenConstraint;
	}
}
