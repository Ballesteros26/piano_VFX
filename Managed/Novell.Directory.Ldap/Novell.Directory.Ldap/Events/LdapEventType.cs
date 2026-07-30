using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A8 RID: 168
	public enum LdapEventType
	{
		// Token: 0x0400030B RID: 779
		TYPE_UNKNOWN = -1,
		// Token: 0x0400030C RID: 780
		LDAP_PSEARCH_ADD = 1,
		// Token: 0x0400030D RID: 781
		LDAP_PSEARCH_DELETE,
		// Token: 0x0400030E RID: 782
		LDAP_PSEARCH_MODIFY = 4,
		// Token: 0x0400030F RID: 783
		LDAP_PSEARCH_MODDN = 8,
		// Token: 0x04000310 RID: 784
		LDAP_PSEARCH_ANY = 15
	}
}
