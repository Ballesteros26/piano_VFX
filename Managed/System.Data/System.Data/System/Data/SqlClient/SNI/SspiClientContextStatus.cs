using System;
using System.Net;
using System.Net.Security;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200025B RID: 603
	internal class SspiClientContextStatus
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x00086637 File Offset: 0x00084837
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x0008663F File Offset: 0x0008483F
		public SafeFreeCredentials CredentialsHandle { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x00086648 File Offset: 0x00084848
		// (set) Token: 0x06001A9C RID: 6812 RVA: 0x00086650 File Offset: 0x00084850
		public SafeDeleteContext SecurityContext { get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x00086659 File Offset: 0x00084859
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x00086661 File Offset: 0x00084861
		public ContextFlagsPal ContextFlags { get; set; }
	}
}
