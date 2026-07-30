using System;
using System.Collections.Generic;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000249 RID: 585
	internal class SNIMarsConnection
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x00082EE5 File Offset: 0x000810E5
		public Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00082EF0 File Offset: 0x000810F0
		public SNIMarsConnection(SNIHandle lowerHandle)
		{
			this._lowerHandle = lowerHandle;
			this._lowerHandle.SetAsyncCallbacks(new SNIAsyncCallback(this.HandleReceiveComplete), new SNIAsyncCallback(this.HandleSendComplete));
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00082F50 File Offset: 0x00081150
		public SNIMarsHandle CreateMarsSession(object callbackObject, bool async)
		{
			SNIMarsHandle snimarsHandle2;
			lock (this)
			{
				ushort nextSessionId = this._nextSessionId;
				this._nextSessionId = nextSessionId + 1;
				ushort num = nextSessionId;
				SNIMarsHandle snimarsHandle = new SNIMarsHandle(this, num, callbackObject, async);
				this._sessions.Add((int)num, snimarsHandle);
				snimarsHandle2 = snimarsHandle;
			}
			return snimarsHandle2;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00082FB8 File Offset: 0x000811B8
		public uint StartReceive()
		{
			SNIPacket snipacket = null;
			if (this.ReceiveAsync(ref snipacket) == 997U)
			{
				return 997U;
			}
			return SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 0U, 19U, string.Empty);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00082FEC File Offset: 0x000811EC
		public uint Send(SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				num = this._lowerHandle.Send(packet);
			}
			return num;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00083030 File Offset: 0x00081230
		public uint SendAsync(SNIPacket packet, SNIAsyncCallback callback)
		{
			uint num;
			lock (this)
			{
				num = this._lowerHandle.SendAsync(packet, callback);
			}
			return num;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00083074 File Offset: 0x00081274
		public uint ReceiveAsync(ref SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				num = this._lowerHandle.ReceiveAsync(ref packet);
			}
			return num;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000830B8 File Offset: 0x000812B8
		public uint CheckConnection()
		{
			uint num;
			lock (this)
			{
				num = this._lowerHandle.CheckConnection();
			}
			return num;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000830FC File Offset: 0x000812FC
		public void HandleReceiveError(SNIPacket packet)
		{
			foreach (SNIMarsHandle snimarsHandle in this._sessions.Values)
			{
				snimarsHandle.HandleReceiveError(packet);
			}
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00083154 File Offset: 0x00081354
		public void HandleSendComplete(SNIPacket packet, uint sniErrorCode)
		{
			packet.InvokeCompletionCallback(sniErrorCode);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00083160 File Offset: 0x00081360
		public void HandleReceiveComplete(SNIPacket packet, uint sniErrorCode)
		{
			SNISMUXHeader snismuxheader = null;
			SNIPacket snipacket = null;
			SNIMarsHandle snimarsHandle = null;
			if (sniErrorCode != 0U)
			{
				SNIMarsConnection snimarsConnection = this;
				lock (snimarsConnection)
				{
					this.HandleReceiveError(packet);
					return;
				}
			}
			for (;;)
			{
				SNIMarsConnection snimarsConnection = this;
				lock (snimarsConnection)
				{
					if (this._currentHeaderByteCount != 16)
					{
						snismuxheader = null;
						snipacket = null;
						snimarsHandle = null;
						while (this._currentHeaderByteCount != 16)
						{
							int num = packet.TakeData(this._headerBytes, this._currentHeaderByteCount, 16 - this._currentHeaderByteCount);
							this._currentHeaderByteCount += num;
							if (num == 0)
							{
								sniErrorCode = this.ReceiveAsync(ref packet);
								if (sniErrorCode == 997U)
								{
									return;
								}
								this.HandleReceiveError(packet);
								return;
							}
						}
						this._currentHeader = new SNISMUXHeader
						{
							SMID = this._headerBytes[0],
							flags = this._headerBytes[1],
							sessionId = BitConverter.ToUInt16(this._headerBytes, 2),
							length = BitConverter.ToUInt32(this._headerBytes, 4) - 16U,
							sequenceNumber = BitConverter.ToUInt32(this._headerBytes, 8),
							highwater = BitConverter.ToUInt32(this._headerBytes, 12)
						};
						this._dataBytesLeft = (int)this._currentHeader.length;
						this._currentPacket = new SNIPacket(null);
						this._currentPacket.Allocate((int)this._currentHeader.length);
					}
					snismuxheader = this._currentHeader;
					snipacket = this._currentPacket;
					if (this._currentHeader.flags == 8 && this._dataBytesLeft > 0)
					{
						int num2 = packet.TakeData(this._currentPacket, this._dataBytesLeft);
						this._dataBytesLeft -= num2;
						if (this._dataBytesLeft > 0)
						{
							sniErrorCode = this.ReceiveAsync(ref packet);
							if (sniErrorCode == 997U)
							{
								break;
							}
							this.HandleReceiveError(packet);
							break;
						}
					}
					this._currentHeaderByteCount = 0;
					if (!this._sessions.ContainsKey((int)this._currentHeader.sessionId))
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.SMUX_PROV, 0U, 5U, string.Empty);
						this.HandleReceiveError(packet);
						this._lowerHandle.Dispose();
						this._lowerHandle = null;
						break;
					}
					if (this._currentHeader.flags == 4)
					{
						this._sessions.Remove((int)this._currentHeader.sessionId);
					}
					else
					{
						snimarsHandle = this._sessions[(int)this._currentHeader.sessionId];
					}
				}
				if (snismuxheader.flags == 8)
				{
					snimarsHandle.HandleReceiveComplete(snipacket, snismuxheader);
				}
				if (this._currentHeader.flags == 2)
				{
					try
					{
						snimarsHandle.HandleAck(snismuxheader.highwater);
					}
					catch (Exception ex)
					{
						SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 35U, ex);
					}
				}
				snimarsConnection = this;
				lock (snimarsConnection)
				{
					if (packet.DataLeft != 0)
					{
						continue;
					}
					sniErrorCode = this.ReceiveAsync(ref packet);
					if (sniErrorCode != 997U)
					{
						this.HandleReceiveError(packet);
					}
				}
				break;
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000834B4 File Offset: 0x000816B4
		public uint EnableSsl(uint options)
		{
			return this._lowerHandle.EnableSsl(options);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000834C2 File Offset: 0x000816C2
		public void DisableSsl()
		{
			this._lowerHandle.DisableSsl();
		}

		// Token: 0x04001299 RID: 4761
		private readonly Guid _connectionId = Guid.NewGuid();

		// Token: 0x0400129A RID: 4762
		private readonly Dictionary<int, SNIMarsHandle> _sessions = new Dictionary<int, SNIMarsHandle>();

		// Token: 0x0400129B RID: 4763
		private readonly byte[] _headerBytes = new byte[16];

		// Token: 0x0400129C RID: 4764
		private SNIHandle _lowerHandle;

		// Token: 0x0400129D RID: 4765
		private ushort _nextSessionId;

		// Token: 0x0400129E RID: 4766
		private int _currentHeaderByteCount;

		// Token: 0x0400129F RID: 4767
		private int _dataBytesLeft;

		// Token: 0x040012A0 RID: 4768
		private SNISMUXHeader _currentHeader;

		// Token: 0x040012A1 RID: 4769
		private SNIPacket _currentPacket;
	}
}
