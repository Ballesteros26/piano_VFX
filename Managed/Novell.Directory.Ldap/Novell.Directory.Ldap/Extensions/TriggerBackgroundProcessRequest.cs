using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A2 RID: 162
	public class TriggerBackgroundProcessRequest : LdapExtendedOperation
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00013EE0 File Offset: 0x000120E0
		public TriggerBackgroundProcessRequest(int processID)
			: base(null, null)
		{
			switch (processID)
			{
			case 1:
				this.setID("2.16.840.1.113719.1.27.100.43");
				return;
			case 2:
				this.setID("2.16.840.1.113719.1.27.100.47");
				return;
			case 3:
				this.setID("2.16.840.1.113719.1.27.100.49");
				return;
			case 4:
				this.setID("2.16.840.1.113719.1.27.100.51");
				return;
			case 5:
				this.setID("2.16.840.1.113719.1.27.100.53");
				return;
			case 6:
				this.setID("2.16.840.1.113719.1.27.100.55");
				return;
			default:
				throw new ArgumentException("PARAM_ERROR");
			}
		}

		// Token: 0x040002FC RID: 764
		public const int Ldap_BK_PROCESS_BKLINKER = 1;

		// Token: 0x040002FD RID: 765
		public const int Ldap_BK_PROCESS_JANITOR = 2;

		// Token: 0x040002FE RID: 766
		public const int Ldap_BK_PROCESS_LIMBER = 3;

		// Token: 0x040002FF RID: 767
		public const int Ldap_BK_PROCESS_SKULKER = 4;

		// Token: 0x04000300 RID: 768
		public const int Ldap_BK_PROCESS_SCHEMA_SYNC = 5;

		// Token: 0x04000301 RID: 769
		public const int Ldap_BK_PROCESS_PART_PURGE = 6;
	}
}
