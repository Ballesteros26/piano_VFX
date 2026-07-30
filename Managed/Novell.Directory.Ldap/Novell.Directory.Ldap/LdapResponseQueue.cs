using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000030 RID: 48
	public class LdapResponseQueue : LdapMessageQueue
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00009AC3 File Offset: 0x00007CC3
		internal LdapResponseQueue(MessageAgent agent)
			: base("LdapResponseQueue", agent)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public virtual void merge(LdapMessageQueue queue2)
		{
			LdapResponseQueue ldapResponseQueue = (LdapResponseQueue)queue2;
			this.agent.merge(ldapResponseQueue.MessageAgent);
		}
	}
}
