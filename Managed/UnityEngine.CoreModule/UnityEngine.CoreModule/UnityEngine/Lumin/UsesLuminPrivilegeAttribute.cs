using System;

namespace UnityEngine.Lumin
{
	// Token: 0x02000309 RID: 777
	[AttributeUsage(4, AllowMultiple = true)]
	public sealed class UsesLuminPrivilegeAttribute : Attribute
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		public UsesLuminPrivilegeAttribute(string privilege)
		{
			this.m_Privilege = privilege;
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0002B9C4 File Offset: 0x00029BC4
		public string privilege
		{
			get
			{
				return this.m_Privilege;
			}
		}

		// Token: 0x04000834 RID: 2100
		private readonly string m_Privilege;
	}
}
