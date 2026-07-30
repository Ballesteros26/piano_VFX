using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000023 RID: 35
	public class LdapLocalException : LdapException
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00007D66 File Offset: 0x00005F66
		public LdapLocalException()
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007D6E File Offset: 0x00005F6E
		public LdapLocalException(string messageOrKey, int resultCode)
			: base(messageOrKey, resultCode, null)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007D79 File Offset: 0x00005F79
		public LdapLocalException(string messageOrKey, object[] arguments, int resultCode)
			: base(messageOrKey, arguments, resultCode, null)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007D85 File Offset: 0x00005F85
		public LdapLocalException(string messageOrKey, int resultCode, Exception rootException)
			: base(messageOrKey, resultCode, null, rootException)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007D91 File Offset: 0x00005F91
		public LdapLocalException(string messageOrKey, object[] arguments, int resultCode, Exception rootException)
			: base(messageOrKey, arguments, resultCode, null, rootException)
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007D9F File Offset: 0x00005F9F
		public override string ToString()
		{
			return this.getExceptionString("LdapLocalException");
		}
	}
}
