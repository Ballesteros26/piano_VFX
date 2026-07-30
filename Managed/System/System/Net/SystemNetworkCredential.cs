using System;

namespace System.Net
{
	// Token: 0x0200041E RID: 1054
	internal class SystemNetworkCredential : NetworkCredential
	{
		// Token: 0x06002010 RID: 8208 RVA: 0x0007D255 File Offset: 0x0007B455
		private SystemNetworkCredential()
			: base(string.Empty, string.Empty, string.Empty)
		{
		}

		// Token: 0x04001BD3 RID: 7123
		internal static readonly SystemNetworkCredential defaultCredential = new SystemNetworkCredential();
	}
}
