using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200024A RID: 586
	internal class SNIMarsHandle : SNIHandle
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x000834CF File Offset: 0x000816CF
		public override Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x000834D7 File Offset: 0x000816D7
		public override uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000834E0 File Offset: 0x000816E0
		public override void Dispose()
		{
			try
			{
				this.SendControlPacket(SNISMUXFlags.SMUX_FIN);
			}
			catch (Exception ex)
			{
				SNICommon.ReportSNIError(SNIProviders.SMUX_PROV, 35U, ex);
				throw;
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00083514 File Offset: 0x00081714
		public SNIMarsHandle(SNIMarsConnection connection, ushort sessionId, object callbackObject, bool async)
		{
			this._sessionId = sessionId;
			this._connection = connection;
			this._callbackObject = callbackObject;
			this.SendControlPacket(SNISMUXFlags.SMUX_SYN);
			this._status = 0U;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000835AC File Offset: 0x000817AC
		private void SendControlPacket(SNISMUXFlags flags)
		{
			byte[] array = null;
			lock (this)
			{
				this.GetSMUXHeaderBytes(0, (byte)flags, ref array);
			}
			SNIPacket snipacket = new SNIPacket(null);
			snipacket.SetData(array, 16);
			this._connection.Send(snipacket);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0008360C File Offset: 0x0008180C
		private void GetSMUXHeaderBytes(int length, byte flags, ref byte[] headerBytes)
		{
			headerBytes = new byte[16];
			this._currentHeader.SMID = 83;
			this._currentHeader.flags = flags;
			this._currentHeader.sessionId = this._sessionId;
			this._currentHeader.length = (uint)(16 + length);
			SNISMUXHeader currentHeader = this._currentHeader;
			uint num;
			if (flags != 4 && flags != 2)
			{
				uint sequenceNumber = this._sequenceNumber;
				this._sequenceNumber = sequenceNumber + 1U;
				num = sequenceNumber;
			}
			else
			{
				num = this._sequenceNumber - 1U;
			}
			currentHeader.sequenceNumber = num;
			this._currentHeader.highwater = this._receiveHighwater;
			this._receiveHighwaterLastAck = this._currentHeader.highwater;
			BitConverter.GetBytes((short)this._currentHeader.SMID).CopyTo(headerBytes, 0);
			BitConverter.GetBytes((short)this._currentHeader.flags).CopyTo(headerBytes, 1);
			BitConverter.GetBytes(this._currentHeader.sessionId).CopyTo(headerBytes, 2);
			BitConverter.GetBytes(this._currentHeader.length).CopyTo(headerBytes, 4);
			BitConverter.GetBytes(this._currentHeader.sequenceNumber).CopyTo(headerBytes, 8);
			BitConverter.GetBytes(this._currentHeader.highwater).CopyTo(headerBytes, 12);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0008373C File Offset: 0x0008193C
		private SNIPacket GetSMUXEncapsulatedPacket(SNIPacket packet)
		{
			uint sequenceNumber = this._sequenceNumber;
			byte[] array = null;
			this.GetSMUXHeaderBytes(packet.Length, 8, ref array);
			SNIPacket snipacket = new SNIPacket(null);
			snipacket.Description = string.Format("({0}) SMUX packet {1}", (packet.Description == null) ? "" : packet.Description, sequenceNumber);
			snipacket.Allocate(16 + packet.Length);
			snipacket.AppendData(array, 16);
			snipacket.AppendPacket(packet);
			return snipacket;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000837B4 File Offset: 0x000819B4
		public override uint Send(SNIPacket packet)
		{
			for (;;)
			{
				SNIMarsHandle snimarsHandle = this;
				lock (snimarsHandle)
				{
					if (this._sequenceNumber < this._sendHighwater)
					{
						break;
					}
				}
				this._ackEvent.Wait();
				snimarsHandle = this;
				lock (snimarsHandle)
				{
					this._ackEvent.Reset();
					continue;
				}
				break;
			}
			return this._connection.Send(this.GetSMUXEncapsulatedPacket(packet));
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00083848 File Offset: 0x00081A48
		private uint InternalSendAsync(SNIPacket packet, SNIAsyncCallback callback)
		{
			uint num;
			lock (this)
			{
				if (this._sequenceNumber >= this._sendHighwater)
				{
					num = 1048576U;
				}
				else
				{
					SNIPacket smuxencapsulatedPacket = this.GetSMUXEncapsulatedPacket(packet);
					if (callback != null)
					{
						smuxencapsulatedPacket.SetCompletionCallback(callback);
					}
					else
					{
						smuxencapsulatedPacket.SetCompletionCallback(new SNIAsyncCallback(this.HandleSendComplete));
					}
					num = this._connection.SendAsync(smuxencapsulatedPacket, callback);
				}
			}
			return num;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x000838CC File Offset: 0x00081ACC
		private uint SendPendingPackets()
		{
			for (;;)
			{
				lock (this)
				{
					if (this._sequenceNumber < this._sendHighwater)
					{
						if (this._sendPacketQueue.Count != 0)
						{
							SNIMarsQueuedPacket snimarsQueuedPacket = this._sendPacketQueue.Peek();
							uint num = this.InternalSendAsync(snimarsQueuedPacket.Packet, snimarsQueuedPacket.Callback);
							if (num != 0U && num != 997U)
							{
								return num;
							}
							this._sendPacketQueue.Dequeue();
							continue;
						}
						else
						{
							this._ackEvent.Set();
						}
					}
				}
				break;
			}
			return 0U;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0008396C File Offset: 0x00081B6C
		public override uint SendAsync(SNIPacket packet, SNIAsyncCallback callback = null)
		{
			lock (this)
			{
				this._sendPacketQueue.Enqueue(new SNIMarsQueuedPacket(packet, (callback != null) ? callback : new SNIAsyncCallback(this.HandleSendComplete)));
			}
			this.SendPendingPackets();
			return 997U;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000839D0 File Offset: 0x00081BD0
		public override uint ReceiveAsync(ref SNIPacket packet)
		{
			Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
			lock (receivedPacketQueue)
			{
				int count = this._receivedPacketQueue.Count;
				if (this._connectionError != null)
				{
					return SNICommon.ReportSNIError(this._connectionError);
				}
				if (count == 0)
				{
					this._asyncReceives++;
					return 997U;
				}
				packet = this._receivedPacketQueue.Dequeue();
				if (count == 1)
				{
					this._packetEvent.Reset();
				}
			}
			lock (this)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
			return 0U;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00083AA0 File Offset: 0x00081CA0
		public void HandleReceiveError(SNIPacket packet)
		{
			Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
			lock (receivedPacketQueue)
			{
				this._connectionError = SNILoadHandle.SingletonInstance.LastError;
				this._packetEvent.Set();
			}
			((TdsParserStateObject)this._callbackObject).ReadAsyncCallback<SNIPacket>(packet, 1U);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00083B08 File Offset: 0x00081D08
		public void HandleSendComplete(SNIPacket packet, uint sniErrorCode)
		{
			lock (this)
			{
				((TdsParserStateObject)this._callbackObject).WriteAsyncCallback<SNIPacket>(packet, sniErrorCode);
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00083B50 File Offset: 0x00081D50
		public void HandleAck(uint highwater)
		{
			lock (this)
			{
				if (this._sendHighwater != highwater)
				{
					this._sendHighwater = highwater;
					this.SendPendingPackets();
				}
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00083B9C File Offset: 0x00081D9C
		public void HandleReceiveComplete(SNIPacket packet, SNISMUXHeader header)
		{
			SNIMarsHandle snimarsHandle = this;
			lock (snimarsHandle)
			{
				if (this._sendHighwater != header.highwater)
				{
					this.HandleAck(header.highwater);
				}
				Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
				lock (receivedPacketQueue)
				{
					if (this._asyncReceives == 0)
					{
						this._receivedPacketQueue.Enqueue(packet);
						this._packetEvent.Set();
						return;
					}
					this._asyncReceives--;
					((TdsParserStateObject)this._callbackObject).ReadAsyncCallback<SNIPacket>(packet, 0U);
				}
			}
			snimarsHandle = this;
			lock (snimarsHandle)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00083C8C File Offset: 0x00081E8C
		private void SendAckIfNecessary()
		{
			uint receiveHighwater;
			uint receiveHighwaterLastAck;
			lock (this)
			{
				receiveHighwater = this._receiveHighwater;
				receiveHighwaterLastAck = this._receiveHighwaterLastAck;
			}
			if (receiveHighwater - receiveHighwaterLastAck > 2U)
			{
				this.SendControlPacket(SNISMUXFlags.SMUX_ACK);
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00083CDC File Offset: 0x00081EDC
		public override uint Receive(out SNIPacket packet, int timeoutInMilliseconds)
		{
			packet = null;
			uint num = 997U;
			for (;;)
			{
				Queue<SNIPacket> receivedPacketQueue = this._receivedPacketQueue;
				lock (receivedPacketQueue)
				{
					if (this._connectionError != null)
					{
						return SNICommon.ReportSNIError(this._connectionError);
					}
					int count = this._receivedPacketQueue.Count;
					if (count > 0)
					{
						packet = this._receivedPacketQueue.Dequeue();
						if (count == 1)
						{
							this._packetEvent.Reset();
						}
						num = 0U;
					}
				}
				if (num == 0U)
				{
					break;
				}
				if (!this._packetEvent.Wait(timeoutInMilliseconds))
				{
					goto Block_4;
				}
			}
			lock (this)
			{
				this._receiveHighwater += 1U;
			}
			this.SendAckIfNecessary();
			return num;
			Block_4:
			SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.SMUX_PROV, 0U, 11U, string.Empty);
			return 258U;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00083DD8 File Offset: 0x00081FD8
		public override uint CheckConnection()
		{
			return this._connection.CheckConnection();
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00005E03 File Offset: 0x00004003
		public override void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback)
		{
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00005E03 File Offset: 0x00004003
		public override void SetBufferSize(int bufferSize)
		{
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00083DE5 File Offset: 0x00081FE5
		public override uint EnableSsl(uint options)
		{
			return this._connection.EnableSsl(options);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00083DF3 File Offset: 0x00081FF3
		public override void DisableSsl()
		{
			this._connection.DisableSsl();
		}

		// Token: 0x040012A2 RID: 4770
		private const uint ACK_THRESHOLD = 2U;

		// Token: 0x040012A3 RID: 4771
		private readonly SNIMarsConnection _connection;

		// Token: 0x040012A4 RID: 4772
		private readonly uint _status = uint.MaxValue;

		// Token: 0x040012A5 RID: 4773
		private readonly Queue<SNIPacket> _receivedPacketQueue = new Queue<SNIPacket>();

		// Token: 0x040012A6 RID: 4774
		private readonly Queue<SNIMarsQueuedPacket> _sendPacketQueue = new Queue<SNIMarsQueuedPacket>();

		// Token: 0x040012A7 RID: 4775
		private readonly object _callbackObject;

		// Token: 0x040012A8 RID: 4776
		private readonly Guid _connectionId = Guid.NewGuid();

		// Token: 0x040012A9 RID: 4777
		private readonly ushort _sessionId;

		// Token: 0x040012AA RID: 4778
		private readonly ManualResetEventSlim _packetEvent = new ManualResetEventSlim(false);

		// Token: 0x040012AB RID: 4779
		private readonly ManualResetEventSlim _ackEvent = new ManualResetEventSlim(false);

		// Token: 0x040012AC RID: 4780
		private readonly SNISMUXHeader _currentHeader = new SNISMUXHeader();

		// Token: 0x040012AD RID: 4781
		private uint _sendHighwater = 4U;

		// Token: 0x040012AE RID: 4782
		private int _asyncReceives;

		// Token: 0x040012AF RID: 4783
		private uint _receiveHighwater = 4U;

		// Token: 0x040012B0 RID: 4784
		private uint _receiveHighwaterLastAck = 4U;

		// Token: 0x040012B1 RID: 4785
		private uint _sequenceNumber;

		// Token: 0x040012B2 RID: 4786
		private SNIError _connectionError;
	}
}
