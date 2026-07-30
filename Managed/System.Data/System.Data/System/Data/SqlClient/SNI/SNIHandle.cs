using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000246 RID: 582
	internal abstract class SNIHandle
	{
		// Token: 0x060019CB RID: 6603
		public abstract void Dispose();

		// Token: 0x060019CC RID: 6604
		public abstract void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback);

		// Token: 0x060019CD RID: 6605
		public abstract void SetBufferSize(int bufferSize);

		// Token: 0x060019CE RID: 6606
		public abstract uint Send(SNIPacket packet);

		// Token: 0x060019CF RID: 6607
		public abstract uint SendAsync(SNIPacket packet, SNIAsyncCallback callback = null);

		// Token: 0x060019D0 RID: 6608
		public abstract uint Receive(out SNIPacket packet, int timeoutInMilliseconds);

		// Token: 0x060019D1 RID: 6609
		public abstract uint ReceiveAsync(ref SNIPacket packet);

		// Token: 0x060019D2 RID: 6610
		public abstract uint EnableSsl(uint options);

		// Token: 0x060019D3 RID: 6611
		public abstract void DisableSsl();

		// Token: 0x060019D4 RID: 6612
		public abstract uint CheckConnection();

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060019D5 RID: 6613
		public abstract uint Status { get; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060019D6 RID: 6614
		public abstract Guid ConnectionId { get; }
	}
}
