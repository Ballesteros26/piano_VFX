using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000058 RID: 88
	public class RfcAttributeDescriptionList : Asn1SequenceOf
	{
		// Token: 0x06000334 RID: 820 RVA: 0x00010435 File Offset: 0x0000E635
		public RfcAttributeDescriptionList(int size)
			: base(size)
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010440 File Offset: 0x0000E640
		public RfcAttributeDescriptionList(string[] attrs)
			: base((attrs == null) ? 0 : attrs.Length)
		{
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					base.add(new RfcAttributeDescription(attrs[i]));
				}
			}
		}
	}
}
