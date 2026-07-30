using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Defines the integer values corresponding to security zones used by security policy.</summary>
	// Token: 0x02000551 RID: 1361
	[ComVisible(true)]
	[Serializable]
	public enum SecurityZone
	{
		/// <summary>The local computer zone is an implicit zone used for content that exists on the user's computer.</summary>
		// Token: 0x04001F8C RID: 8076
		MyComputer,
		/// <summary>The local intranet zone is used for content located on a company's intranet. Because the servers and information would be within a company's firewall, a user or company could assign a higher trust level to the content on the intranet.</summary>
		// Token: 0x04001F8D RID: 8077
		Intranet,
		/// <summary>The trusted sites zone is used for content located on Web sites considered more reputable or trustworthy than other sites on the Internet. Users can use this zone to assign a higher trust level to these sites to minimize the number of authentication requests. The URLs of these trusted Web sites need to be mapped into this zone by the user.</summary>
		// Token: 0x04001F8E RID: 8078
		Trusted,
		/// <summary>The Internet zone is used for the Web sites on the Internet that do not belong to another zone.</summary>
		// Token: 0x04001F8F RID: 8079
		Internet,
		/// <summary>The restricted sites zone is used for Web sites with content that could cause, or could have caused, problems when downloaded. The URLs of these untrusted Web sites need to be mapped into this zone by the user.</summary>
		// Token: 0x04001F90 RID: 8080
		Untrusted,
		/// <summary>No zone is specified.</summary>
		// Token: 0x04001F91 RID: 8081
		NoZone = -1
	}
}
