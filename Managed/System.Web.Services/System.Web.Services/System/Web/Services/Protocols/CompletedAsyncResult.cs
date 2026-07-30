using System;
using System.Threading;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200008E RID: 142
	internal class CompletedAsyncResult : IAsyncResult
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00011D24 File Offset: 0x0000FF24
		internal CompletedAsyncResult(object asyncState, bool completedSynchronously)
		{
			this.asyncState = asyncState;
			this.completedSynchronously = completedSynchronously;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00011D3A File Offset: 0x0000FF3A
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00011D42 File Offset: 0x0000FF42
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00002B54 File Offset: 0x00000D54
		public bool IsCompleted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00006C2F File Offset: 0x00004E2F
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400030D RID: 781
		private object asyncState;

		// Token: 0x0400030E RID: 782
		private bool completedSynchronously;
	}
}
