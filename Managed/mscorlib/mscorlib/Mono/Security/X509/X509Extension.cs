using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x02000061 RID: 97
	internal class X509Extension
	{
		// Token: 0x06000337 RID: 823 RVA: 0x000140A9 File Offset: 0x000122A9
		protected X509Extension()
		{
			this.extnCritical = false;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000140B8 File Offset: 0x000122B8
		public X509Extension(ASN1 asn1)
		{
			if (asn1.Tag != 48 || asn1.Count < 2)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			if (asn1[0].Tag != 6)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			this.extnOid = ASN1Convert.ToOid(asn1[0]);
			this.extnCritical = asn1[1].Tag == 1 && asn1[1].Value[0] == byte.MaxValue;
			this.extnValue = asn1[asn1.Count - 1];
			if (this.extnValue.Tag == 4 && this.extnValue.Length > 0 && this.extnValue.Count == 0)
			{
				try
				{
					ASN1 asn2 = new ASN1(this.extnValue.Value);
					this.extnValue.Value = null;
					this.extnValue.Add(asn2);
				}
				catch
				{
				}
			}
			this.Decode();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000141D0 File Offset: 0x000123D0
		public X509Extension(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (extension.Value == null || extension.Value.Tag != 4 || extension.Value.Count != 1)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			this.extnOid = extension.Oid;
			this.extnCritical = extension.Critical;
			this.extnValue = extension.Value;
			this.Decode();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void Decode()
		{
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void Encode()
		{
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00014250 File Offset: 0x00012450
		public ASN1 ASN1
		{
			get
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.extnOid));
				if (this.extnCritical)
				{
					asn.Add(new ASN1(1, new byte[] { byte.MaxValue }));
				}
				this.Encode();
				asn.Add(this.extnValue);
				return asn;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000142AE File Offset: 0x000124AE
		public string Oid
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600033E RID: 830 RVA: 0x000142B6 File Offset: 0x000124B6
		// (set) Token: 0x0600033F RID: 831 RVA: 0x000142BE File Offset: 0x000124BE
		public bool Critical
		{
			get
			{
				return this.extnCritical;
			}
			set
			{
				this.extnCritical = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000340 RID: 832 RVA: 0x000142AE File Offset: 0x000124AE
		public virtual string Name
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000142C7 File Offset: 0x000124C7
		public ASN1 Value
		{
			get
			{
				if (this.extnValue == null)
				{
					this.Encode();
				}
				return this.extnValue;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000142E0 File Offset: 0x000124E0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			X509Extension x509Extension = obj as X509Extension;
			if (x509Extension == null)
			{
				return false;
			}
			if (this.extnCritical != x509Extension.extnCritical)
			{
				return false;
			}
			if (this.extnOid != x509Extension.extnOid)
			{
				return false;
			}
			if (this.extnValue.Length != x509Extension.extnValue.Length)
			{
				return false;
			}
			for (int i = 0; i < this.extnValue.Length; i++)
			{
				if (this.extnValue[i] != x509Extension.extnValue[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00014370 File Offset: 0x00012570
		public byte[] GetBytes()
		{
			return this.ASN1.GetBytes();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001437D File Offset: 0x0001257D
		public override int GetHashCode()
		{
			return this.extnOid.GetHashCode();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001438C File Offset: 0x0001258C
		private void WriteLine(StringBuilder sb, int n, int pos)
		{
			byte[] value = this.extnValue.Value;
			int num = pos;
			for (int i = 0; i < 8; i++)
			{
				if (i < n)
				{
					sb.Append(value[num++].ToString("X2", CultureInfo.InvariantCulture));
					sb.Append(" ");
				}
				else
				{
					sb.Append("   ");
				}
			}
			sb.Append("  ");
			num = pos;
			for (int j = 0; j < n; j++)
			{
				byte b = value[num++];
				if (b < 32)
				{
					sb.Append(".");
				}
				else
				{
					sb.Append(Convert.ToChar(b));
				}
			}
			sb.Append(Environment.NewLine);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00014444 File Offset: 0x00012644
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.extnValue.Length >> 3;
			int num2 = this.extnValue.Length - (num << 3);
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				this.WriteLine(stringBuilder, 8, num3);
				num3 += 8;
			}
			this.WriteLine(stringBuilder, num2, num3);
			return stringBuilder.ToString();
		}

		// Token: 0x04000519 RID: 1305
		protected string extnOid;

		// Token: 0x0400051A RID: 1306
		protected bool extnCritical;

		// Token: 0x0400051B RID: 1307
		protected ASN1 extnValue;
	}
}
