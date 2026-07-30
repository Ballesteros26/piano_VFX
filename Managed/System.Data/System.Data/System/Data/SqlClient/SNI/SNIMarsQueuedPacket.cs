using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200024B RID: 587
	internal class SNIMarsQueuedPacket
	{
		// Token: 0x06001A05 RID: 6661 RVA: 0x00083E00 File Offset: 0x00082000
		public SNIMarsQueuedPacket(SNIPacket packet, SNIAsyncCallback callback)
		{
			this._packet = packet;
			this._callback = callback;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00083E16 File Offset: 0x00082016
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x00083E1E File Offset: 0x0008201E
		public SNIPacket Packet
		{
			get
			{
				return this._packet;
			}
			set
			{
				this._packet = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00083E27 File Offset: 0x00082027
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x00083E2F File Offset: 0x0008202F
		public SNIAsyncCallback Callback
		{
			get
			{
				return this._callback;
			}
			set
			{
				this._callback = value;
			}
		}

		// Token: 0x040012B3 RID: 4787
		private SNIPacket _packet;

		// Token: 0x040012B4 RID: 4788
		private SNIAsyncCallback _callback;
	}
}
