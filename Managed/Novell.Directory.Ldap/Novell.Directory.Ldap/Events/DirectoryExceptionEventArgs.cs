using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000A5 RID: 165
	public class DirectoryExceptionEventArgs : BaseEventArgs
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00013FA1 File Offset: 0x000121A1
		public LdapException LdapExceptionObject
		{
			get
			{
				return this.ldap_exception_object;
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013FA9 File Offset: 0x000121A9
		public DirectoryExceptionEventArgs(LdapMessage message, LdapException ldapException)
			: base(message)
		{
			this.ldap_exception_object = ldapException;
		}

		// Token: 0x04000304 RID: 772
		protected LdapException ldap_exception_object;
	}
}
