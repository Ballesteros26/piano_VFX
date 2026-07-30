using System;
using System.Collections;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002A RID: 42
	public class LdapModifyRequest : LdapMessage
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000088E1 File Offset: 0x00006AE1
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000088F0 File Offset: 0x00006AF0
		public virtual LdapModification[] Modifications
		{
			get
			{
				Asn1Object[] array = ((RfcModifyRequest)this.Asn1Object.getRequest()).Modifications.toArray();
				LdapModification[] array2 = new LdapModification[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)array[i];
					if (asn1Sequence.size() != 2)
					{
						throw new SystemException(string.Concat(new object[]
						{
							"LdapModifyRequest: modification ",
							i,
							" is wrong size: ",
							asn1Sequence.size()
						}));
					}
					Asn1Object[] array3 = asn1Sequence.toArray();
					int num = ((Asn1Enumerated)array3[0]).intValue();
					Asn1Object[] array4 = ((Asn1Sequence)array3[1]).toArray();
					string text = ((RfcAttributeDescription)array4[0]).stringValue();
					Asn1Object[] array5 = ((Asn1SetOf)array4[1]).toArray();
					LdapAttribute ldapAttribute = new LdapAttribute(text);
					foreach (RfcAttributeValue rfcAttributeValue in array5)
					{
						ldapAttribute.addValue(rfcAttributeValue.byteValue());
					}
					array2[i] = new LdapModification(num, ldapAttribute);
				}
				return array2;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008A00 File Offset: 0x00006C00
		public LdapModifyRequest(string dn, LdapModification[] mods, LdapControl[] cont)
			: base(6, new RfcModifyRequest(new RfcLdapDN(dn), LdapModifyRequest.encodeModifications(mods)), cont)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008A1C File Offset: 0x00006C1C
		private static Asn1SequenceOf encodeModifications(LdapModification[] mods)
		{
			Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf(mods.Length);
			for (int i = 0; i < mods.Length; i++)
			{
				LdapAttribute attribute = mods[i].Attribute;
				Asn1SetOf asn1SetOf = new Asn1SetOf(attribute.size());
				if (attribute.size() > 0)
				{
					IEnumerator byteValues = attribute.ByteValues;
					while (byteValues.MoveNext())
					{
						object obj = byteValues.Current;
						asn1SetOf.add(new RfcAttributeValue((sbyte[])obj));
					}
				}
				Asn1Sequence asn1Sequence = new Asn1Sequence(2);
				asn1Sequence.add(new Asn1Enumerated(mods[i].Op));
				asn1Sequence.add(new RfcAttributeTypeAndValues(new RfcAttributeDescription(attribute.Name), asn1SetOf));
				asn1SequenceOf.add(asn1Sequence);
			}
			return asn1SequenceOf;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008ACB File Offset: 0x00006CCB
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
