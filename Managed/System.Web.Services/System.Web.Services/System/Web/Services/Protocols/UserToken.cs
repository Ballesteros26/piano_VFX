using System;
using System.Threading;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000020 RID: 32
	internal class UserToken
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003868 File Offset: 0x00001A68
		internal UserToken(SendOrPostCallback callback, object userState)
		{
			this.callback = callback;
			this.userState = userState;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000387E File Offset: 0x00001A7E
		internal SendOrPostCallback Callback
		{
			get
			{
				return this.callback;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003886 File Offset: 0x00001A86
		internal object UserState
		{
			get
			{
				return this.userState;
			}
		}

		// Token: 0x040001C5 RID: 453
		private SendOrPostCallback callback;

		// Token: 0x040001C6 RID: 454
		private object userState;
	}
}
