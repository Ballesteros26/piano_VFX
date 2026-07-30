using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000036 RID: 54
	public class LdapSearchResult : LdapMessage
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public virtual LdapEntry Entry
		{
			get
			{
				if (this.entry == null)
				{
					LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
					foreach (Asn1Sequence asn1Sequence in ((RfcSearchResultEntry)this.message.Response).Attributes.toArray())
					{
						LdapAttribute ldapAttribute = new LdapAttribute(((Asn1OctetString)asn1Sequence.get_Renamed(0)).stringValue());
						object[] array2 = ((Asn1Set)asn1Sequence.get_Renamed(1)).toArray();
						for (int j = 0; j < array2.Length; j++)
						{
							ldapAttribute.addValue(((Asn1OctetString)array2[j]).byteValue());
						}
						ldapAttributeSet.Add(ldapAttribute);
					}
					this.entry = new LdapEntry(((RfcSearchResultEntry)this.message.Response).ObjectName.stringValue(), ldapAttributeSet);
				}
				return this.entry;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A78C File Offset: 0x0000898C
		internal LdapSearchResult(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A795 File Offset: 0x00008995
		public LdapSearchResult(LdapEntry entry, LdapControl[] cont)
		{
			if (entry == null)
			{
				throw new ArgumentException("Argument \"entry\" cannot be null");
			}
			this.entry = entry;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A7B4 File Offset: 0x000089B4
		public override string ToString()
		{
			string text;
			if (this.entry == null)
			{
				text = base.ToString();
			}
			else
			{
				text = this.entry.ToString();
			}
			return text;
		}

		// Token: 0x0400015B RID: 347
		private LdapEntry entry;
	}
}
