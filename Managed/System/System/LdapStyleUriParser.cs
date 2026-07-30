using System;

namespace System
{
	/// <summary>A customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
	// Token: 0x0200010B RID: 267
	public class LdapStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
		// Token: 0x06000753 RID: 1875 RVA: 0x0002462F File Offset: 0x0002282F
		public LdapStyleUriParser()
			: base(UriParser.LdapUri.Flags)
		{
		}
	}
}
