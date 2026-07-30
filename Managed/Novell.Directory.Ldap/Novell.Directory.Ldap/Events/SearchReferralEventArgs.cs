using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000AB RID: 171
	public class SearchReferralEventArgs : LdapEventArgs
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x00014513 File Offset: 0x00012713
		public SearchReferralEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType)
			: base(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, LdapEventType.LDAP_PSEARCH_ANY)
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001451F File Offset: 0x0001271F
		public string[] getUrls()
		{
			return ((LdapSearchResultReference)this.ldap_message).Referrals;
		}
	}
}
