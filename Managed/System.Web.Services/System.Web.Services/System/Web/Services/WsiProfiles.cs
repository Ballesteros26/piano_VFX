using System;

namespace System.Web.Services
{
	/// <summary>Describes the Web services interoperability (WSI) specification to which a Web service claims to conform.</summary>
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum WsiProfiles
	{
		/// <summary>The web service makes no conformance claims.</summary>
		// Token: 0x04000083 RID: 131
		None = 0,
		/// <summary>The web service claims to conform to the WSI Basic Profile version 1.1.</summary>
		// Token: 0x04000084 RID: 132
		BasicProfile1_1 = 1
	}
}
