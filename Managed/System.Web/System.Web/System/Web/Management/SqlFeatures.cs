using System;

namespace System.Web.Management
{
	/// <summary>Specifies the ASP.Net features to install or remove using the methods provided by the <see cref="T:System.Web.Management.SqlServices" /> class.</summary>
	// Token: 0x0200052E RID: 1326
	[Flags]
	public enum SqlFeatures
	{
		/// <summary>No features.</summary>
		// Token: 0x04001F6B RID: 8043
		None = 0,
		/// <summary>The membership feature.</summary>
		// Token: 0x04001F6C RID: 8044
		Membership = 1,
		/// <summary>The profile feature.</summary>
		// Token: 0x04001F6D RID: 8045
		Profile = 2,
		/// <summary>The role manager feature.</summary>
		// Token: 0x04001F6E RID: 8046
		RoleManager = 4,
		/// <summary>The personalization feature.</summary>
		// Token: 0x04001F6F RID: 8047
		Personalization = 8,
		/// <summary>The Web event provider feature.</summary>
		// Token: 0x04001F70 RID: 8048
		SqlWebEventProvider = 16,
		/// <summary>All features.</summary>
		// Token: 0x04001F71 RID: 8049
		All = 1073741855
	}
}
