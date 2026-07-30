using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	// Token: 0x0200063B RID: 1595
	[ComVisible(false)]
	internal class RoleClaimProvider
	{
		// Token: 0x0600455C RID: 17756 RVA: 0x000F464F File Offset: 0x000F284F
		public RoleClaimProvider(string issuer, string[] roles, ClaimsIdentity subject)
		{
			this.m_issuer = issuer;
			this.m_roles = roles;
			this.m_subject = subject;
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x000F466C File Offset: 0x000F286C
		public IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_roles.Length; i = num + 1)
				{
					if (this.m_roles[i] != null)
					{
						yield return new Claim(this.m_subject.RoleClaimType, this.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", this.m_issuer, this.m_issuer, this.m_subject);
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x040023B3 RID: 9139
		private string m_issuer;

		// Token: 0x040023B4 RID: 9140
		private string[] m_roles;

		// Token: 0x040023B5 RID: 9141
		private ClaimsIdentity m_subject;
	}
}
