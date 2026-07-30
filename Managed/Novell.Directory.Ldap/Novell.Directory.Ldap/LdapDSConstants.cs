using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001B RID: 27
	public struct LdapDSConstants
	{
		// Token: 0x0400009A RID: 154
		public static readonly long LDAP_DS_ENTRY_BROWSE = 1L;

		// Token: 0x0400009B RID: 155
		public static readonly long LDAP_DS_ENTRY_ADD = 2L;

		// Token: 0x0400009C RID: 156
		public static readonly long LDAP_DS_ENTRY_DELETE = 4L;

		// Token: 0x0400009D RID: 157
		public static readonly long LDAP_DS_ENTRY_RENAME = 8L;

		// Token: 0x0400009E RID: 158
		public static readonly long LDAP_DS_ENTRY_SUPERVISOR = 16L;

		// Token: 0x0400009F RID: 159
		public static readonly long LDAP_DS_ENTRY_INHERIT_CTL = 64L;

		// Token: 0x040000A0 RID: 160
		public static readonly long LDAP_DS_ATTR_COMPARE = 1L;

		// Token: 0x040000A1 RID: 161
		public static readonly long LDAP_DS_ATTR_READ = 2L;

		// Token: 0x040000A2 RID: 162
		public static readonly long LDAP_DS_ATTR_WRITE = 4L;

		// Token: 0x040000A3 RID: 163
		public static readonly long LDAP_DS_ATTR_SELF = 8L;

		// Token: 0x040000A4 RID: 164
		public static readonly long LDAP_DS_ATTR_SUPERVISOR = 32L;

		// Token: 0x040000A5 RID: 165
		public static readonly long LDAP_DS_ATTR_INHERIT_CTL = 64L;

		// Token: 0x040000A6 RID: 166
		public static readonly long LDAP_DS_DYNAMIC_ACL = 1073741824L;

		// Token: 0x040000A7 RID: 167
		public static readonly int LDAP_DS_ALIAS_ENTRY = 1;

		// Token: 0x040000A8 RID: 168
		public static readonly int LDAP_DS_PARTITION_ROOT = 2;

		// Token: 0x040000A9 RID: 169
		public static readonly int LDAP_DS_CONTAINER_ENTRY = 4;

		// Token: 0x040000AA RID: 170
		public static readonly int LDAP_DS_CONTAINER_ALIAS = 8;

		// Token: 0x040000AB RID: 171
		public static readonly int LDAP_DS_MATCHES_LIST_FILTER = 16;

		// Token: 0x040000AC RID: 172
		public static readonly int LDAP_DS_REFERENCE_ENTRY = 32;

		// Token: 0x040000AD RID: 173
		public static readonly int LDAP_DS_40X_REFERENCE_ENTRY = 64;

		// Token: 0x040000AE RID: 174
		public static readonly int LDAP_DS_BACKLINKED = 128;

		// Token: 0x040000AF RID: 175
		public static readonly int LDAP_DS_NEW_ENTRY = 256;

		// Token: 0x040000B0 RID: 176
		public static readonly int LDAP_DS_TEMPORARY_REFERENCE = 512;

		// Token: 0x040000B1 RID: 177
		public static readonly int LDAP_DS_AUDITED = 1024;

		// Token: 0x040000B2 RID: 178
		public static readonly int LDAP_DS_ENTRY_NOT_PRESENT = 2048;

		// Token: 0x040000B3 RID: 179
		public static readonly int LDAP_DS_ENTRY_VERIFY_CTS = 4096;

		// Token: 0x040000B4 RID: 180
		public static readonly int LDAP_DS_ENTRY_DAMAGED = 8192;
	}
}
