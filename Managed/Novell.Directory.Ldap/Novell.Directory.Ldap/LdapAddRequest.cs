using System;
using System.Collections;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200000B RID: 11
	public class LdapAddRequest : LdapMessage
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003658 File Offset: 0x00001858
		public virtual LdapEntry Entry
		{
			get
			{
				RfcAddRequest rfcAddRequest = (RfcAddRequest)this.Asn1Object.getRequest();
				LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
				foreach (RfcAttributeTypeAndValues rfcAttributeTypeAndValues in rfcAddRequest.Attributes.toArray())
				{
					LdapAttribute ldapAttribute = new LdapAttribute(((Asn1OctetString)rfcAttributeTypeAndValues.get_Renamed(0)).stringValue());
					object[] array2 = ((Asn1SetOf)rfcAttributeTypeAndValues.get_Renamed(1)).toArray();
					for (int j = 0; j < array2.Length; j++)
					{
						ldapAttribute.addValue(((Asn1OctetString)array2[j]).byteValue());
					}
					ldapAttributeSet.Add(ldapAttribute);
				}
				return new LdapEntry(this.Asn1Object.RequestDN, ldapAttributeSet);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003706 File Offset: 0x00001906
		public LdapAddRequest(LdapEntry entry, LdapControl[] cont)
			: base(8, new RfcAddRequest(new RfcLdapDN(entry.DN), LdapAddRequest.makeRfcAttrList(entry)), cont)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003728 File Offset: 0x00001928
		private static RfcAttributeList makeRfcAttrList(LdapEntry entry)
		{
			LdapAttributeSet attributeSet = entry.getAttributeSet();
			RfcAttributeList rfcAttributeList = new RfcAttributeList(attributeSet.Count);
			foreach (object obj in attributeSet)
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				Asn1SetOf asn1SetOf = new Asn1SetOf(ldapAttribute.size());
				IEnumerator byteValues = ldapAttribute.ByteValues;
				while (byteValues.MoveNext())
				{
					object obj2 = byteValues.Current;
					asn1SetOf.add(new RfcAttributeValue((sbyte[])obj2));
				}
				rfcAttributeList.add(new RfcAttributeTypeAndValues(new RfcAttributeDescription(ldapAttribute.Name), asn1SetOf));
			}
			return rfcAttributeList;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000037B1 File Offset: 0x000019B1
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
