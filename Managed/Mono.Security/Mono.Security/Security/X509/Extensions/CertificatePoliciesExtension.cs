using System;
using System.Collections;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000021 RID: 33
	public class CertificatePoliciesExtension : X509Extension
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000BB74 File Offset: 0x00009D74
		public CertificatePoliciesExtension()
		{
			this.extnOid = "2.5.29.32";
			this.policies = new Hashtable();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000BB92 File Offset: 0x00009D92
		public CertificatePoliciesExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000BB9B File Offset: 0x00009D9B
		public CertificatePoliciesExtension(X509Extension extension)
			: base(extension)
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		protected override void Decode()
		{
			this.policies = new Hashtable();
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid CertificatePolicies extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				this.policies.Add(ASN1Convert.ToOid(asn[i][0]), null);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000BC11 File Offset: 0x00009E11
		public override string Name
		{
			get
			{
				return "Certificate Policies";
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000BC18 File Offset: 0x00009E18
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (object obj in this.policies)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				stringBuilder.Append("[");
				stringBuilder.Append(num++);
				stringBuilder.Append("]Certificate Policy:");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\tPolicyIdentifier=");
				stringBuilder.Append((string)dictionaryEntry.Key);
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000DA RID: 218
		private Hashtable policies;
	}
}
