using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A3 RID: 163
	public class BaseEventArgs : EventArgs
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00013F69 File Offset: 0x00012169
		public LdapMessage ContianedEventInformation
		{
			get
			{
				return this.ldap_message;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00013F71 File Offset: 0x00012171
		public BaseEventArgs(LdapMessage message)
		{
			this.ldap_message = message;
		}

		// Token: 0x04000302 RID: 770
		protected LdapMessage ldap_message;
	}
}
