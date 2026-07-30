using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000530 RID: 1328
	internal sealed class PrefixQName
	{
		// Token: 0x0600356A RID: 13674 RVA: 0x0012CBE9 File Offset: 0x0012ADE9
		internal void ClearPrefix()
		{
			this.Prefix = string.Empty;
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x0012CBF6 File Offset: 0x0012ADF6
		internal void SetQName(string qname)
		{
			PrefixQName.ParseQualifiedName(qname, out this.Prefix, out this.Name);
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x0012CC0C File Offset: 0x0012AE0C
		public static void ParseQualifiedName(string qname, out string prefix, out string local)
		{
			prefix = string.Empty;
			local = string.Empty;
			int num = ValidateNames.ParseNCName(qname);
			if (num == 0)
			{
				throw XsltException.Create("'{0}' is an invalid QName.", new string[] { qname });
			}
			local = qname.Substring(0, num);
			if (num < qname.Length)
			{
				if (qname[num] == ':')
				{
					int num2;
					num = (num2 = num + 1);
					prefix = local;
					int num3 = ValidateNames.ParseNCName(qname, num);
					num += num3;
					if (num3 == 0)
					{
						throw XsltException.Create("'{0}' is an invalid QName.", new string[] { qname });
					}
					local = qname.Substring(num2, num3);
				}
				if (num < qname.Length)
				{
					throw XsltException.Create("'{0}' is an invalid QName.", new string[] { qname });
				}
			}
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x0012CCBA File Offset: 0x0012AEBA
		public static bool ValidatePrefix(string prefix)
		{
			return prefix.Length != 0 && ValidateNames.ParseNCName(prefix, 0) == prefix.Length;
		}

		// Token: 0x0400221B RID: 8731
		public string Prefix;

		// Token: 0x0400221C RID: 8732
		public string Name;

		// Token: 0x0400221D RID: 8733
		public string Namespace;
	}
}
