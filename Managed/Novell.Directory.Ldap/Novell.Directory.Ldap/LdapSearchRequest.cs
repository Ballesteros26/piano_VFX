using System;
using System.Collections;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000035 RID: 53
	public class LdapSearchRequest : LdapMessage
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000A4C9 File Offset: 0x000086C9
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000A4D6 File Offset: 0x000086D6
		public virtual int Scope
		{
			get
			{
				return ((Asn1Enumerated)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(1)).intValue();
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A4F9 File Offset: 0x000086F9
		public virtual int Dereference
		{
			get
			{
				return ((Asn1Enumerated)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(2)).intValue();
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A51C File Offset: 0x0000871C
		public virtual int MaxResults
		{
			get
			{
				return ((Asn1Integer)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(3)).intValue();
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A53F File Offset: 0x0000873F
		public virtual int ServerTimeLimit
		{
			get
			{
				return ((Asn1Integer)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(4)).intValue();
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A562 File Offset: 0x00008762
		public virtual bool TypesOnly
		{
			get
			{
				return ((Asn1Boolean)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(5)).booleanValue();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A588 File Offset: 0x00008788
		public virtual string[] Attributes
		{
			get
			{
				RfcAttributeDescriptionList rfcAttributeDescriptionList = (RfcAttributeDescriptionList)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(7);
				string[] array = new string[rfcAttributeDescriptionList.size()];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ((RfcAttributeDescription)rfcAttributeDescriptionList.get_Renamed(i)).stringValue();
				}
				return array;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A5E1 File Offset: 0x000087E1
		public virtual string StringFilter
		{
			get
			{
				return this.RfcFilter.filterToString();
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A5EE File Offset: 0x000087EE
		private RfcFilter RfcFilter
		{
			get
			{
				return (RfcFilter)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(6);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000A60C File Offset: 0x0000880C
		public virtual IEnumerator SearchFilter
		{
			get
			{
				return this.RfcFilter.getFilterIterator();
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A61C File Offset: 0x0000881C
		public LdapSearchRequest(string base_Renamed, int scope, string filter, string[] attrs, int dereference, int maxResults, int serverTimeLimit, bool typesOnly, LdapControl[] cont)
			: base(3, new RfcSearchRequest(new RfcLdapDN(base_Renamed), new Asn1Enumerated(scope), new Asn1Enumerated(dereference), new Asn1Integer(maxResults), new Asn1Integer(serverTimeLimit), new Asn1Boolean(typesOnly), new RfcFilter(filter), new RfcAttributeDescriptionList(attrs)), cont)
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A66C File Offset: 0x0000886C
		public LdapSearchRequest(string base_Renamed, int scope, RfcFilter filter, string[] attrs, int dereference, int maxResults, int serverTimeLimit, bool typesOnly, LdapControl[] cont)
			: base(3, new RfcSearchRequest(new RfcLdapDN(base_Renamed), new Asn1Enumerated(scope), new Asn1Enumerated(dereference), new Asn1Integer(maxResults), new Asn1Integer(serverTimeLimit), new Asn1Boolean(typesOnly), filter, new RfcAttributeDescriptionList(attrs)), cont)
		{
		}

		// Token: 0x0400014E RID: 334
		public const int AND = 0;

		// Token: 0x0400014F RID: 335
		public const int OR = 1;

		// Token: 0x04000150 RID: 336
		public const int NOT = 2;

		// Token: 0x04000151 RID: 337
		public const int EQUALITY_MATCH = 3;

		// Token: 0x04000152 RID: 338
		public const int SUBSTRINGS = 4;

		// Token: 0x04000153 RID: 339
		public const int GREATER_OR_EQUAL = 5;

		// Token: 0x04000154 RID: 340
		public const int LESS_OR_EQUAL = 6;

		// Token: 0x04000155 RID: 341
		public const int PRESENT = 7;

		// Token: 0x04000156 RID: 342
		public const int APPROX_MATCH = 8;

		// Token: 0x04000157 RID: 343
		public const int EXTENSIBLE_MATCH = 9;

		// Token: 0x04000158 RID: 344
		public const int INITIAL = 0;

		// Token: 0x04000159 RID: 345
		public const int ANY = 1;

		// Token: 0x0400015A RID: 346
		public const int FINAL = 2;
	}
}
