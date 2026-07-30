using System;

namespace Mono.Security.X509
{
	// Token: 0x0200000E RID: 14
	internal class SafeBag
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000041D9 File Offset: 0x000023D9
		public SafeBag(string bagOID, ASN1 asn1)
		{
			this._bagOID = bagOID;
			this._asn1 = asn1;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000041EF File Offset: 0x000023EF
		public string BagOID
		{
			get
			{
				return this._bagOID;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000041F7 File Offset: 0x000023F7
		public ASN1 ASN1
		{
			get
			{
				return this._asn1;
			}
		}

		// Token: 0x04000053 RID: 83
		private string _bagOID;

		// Token: 0x04000054 RID: 84
		private ASN1 _asn1;
	}
}
