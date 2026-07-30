using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200025C RID: 604
	internal class TdsParserStateObjectManaged : TdsParserStateObject
	{
		// Token: 0x06001AA0 RID: 6816 RVA: 0x0008666A File Offset: 0x0008486A
		public TdsParserStateObjectManaged(TdsParser parser)
			: base(parser)
		{
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00086694 File Offset: 0x00084894
		internal TdsParserStateObjectManaged(TdsParser parser, TdsParserStateObject physicalConnection, bool async)
			: base(parser, physicalConnection, async)
		{
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x000866C0 File Offset: 0x000848C0
		internal SNIHandle Handle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x000866C8 File Offset: 0x000848C8
		internal override uint Status
		{
			get
			{
				if (this._sessionHandle == null)
				{
					return uint.MaxValue;
				}
				return this._sessionHandle.Status;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x000866C0 File Offset: 0x000848C0
		internal override object SessionHandle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00004526 File Offset: 0x00002726
		protected override object EmptyReadPacket
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000866E0 File Offset: 0x000848E0
		protected override bool CheckPacket(object packet, TaskCompletionSource<object> source)
		{
			SNIPacket snipacket = packet as SNIPacket;
			return snipacket.IsInvalid || (!snipacket.IsInvalid && source != null);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0008670C File Offset: 0x0008490C
		protected override void CreateSessionHandle(TdsParserStateObject physicalConnection, bool async)
		{
			TdsParserStateObjectManaged tdsParserStateObjectManaged = physicalConnection as TdsParserStateObjectManaged;
			this._sessionHandle = tdsParserStateObjectManaged.CreateMarsSession(this, async);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0008672E File Offset: 0x0008492E
		internal SNIMarsHandle CreateMarsSession(object callbackObject, bool async)
		{
			return this._marsConnection.CreateMarsSession(callbackObject, async);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0008673D File Offset: 0x0008493D
		protected override uint SNIPacketGetData(object packet, byte[] _inBuff, ref uint dataSize)
		{
			return SNIProxy.Singleton.PacketGetData(packet as SNIPacket, _inBuff, ref dataSize);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00086754 File Offset: 0x00084954
		internal override void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool parallel, bool isIntegratedSecurity)
		{
			this._sessionHandle = SNIProxy.Singleton.CreateConnectionHandle(this, serverName, ignoreSniOpenTimeout, timerExpire, out instanceName, ref spnBuffer, flushCache, async, parallel, isIntegratedSecurity);
			if (this._sessionHandle == null)
			{
				this._parser.ProcessSNIError(this);
				return;
			}
			if (async)
			{
				SNIAsyncCallback sniasyncCallback = new SNIAsyncCallback(this.ReadAsyncCallback);
				SNIAsyncCallback sniasyncCallback2 = new SNIAsyncCallback(this.WriteAsyncCallback);
				this._sessionHandle.SetAsyncCallbacks(sniasyncCallback, sniasyncCallback2);
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000867C2 File Offset: 0x000849C2
		internal void ReadAsyncCallback(SNIPacket packet, uint error)
		{
			base.ReadAsyncCallback<SNIPacket>(IntPtr.Zero, packet, error);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000867D1 File Offset: 0x000849D1
		internal void WriteAsyncCallback(SNIPacket packet, uint sniError)
		{
			base.WriteAsyncCallback<SNIPacket>(IntPtr.Zero, packet, sniError);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00005E03 File Offset: 0x00004003
		protected override void RemovePacketFromPendingList(object packet)
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000867E0 File Offset: 0x000849E0
		internal override void Dispose()
		{
			SNIPacket sniPacket = this._sniPacket;
			SNIHandle sessionHandle = this._sessionHandle;
			SNIPacket sniAsyncAttnPacket = this._sniAsyncAttnPacket;
			this._sniPacket = null;
			this._sessionHandle = null;
			this._sniAsyncAttnPacket = null;
			this._marsConnection = null;
			base.DisposeCounters();
			if (sessionHandle != null || sniPacket != null)
			{
				if (sniPacket != null)
				{
					sniPacket.Dispose();
				}
				if (sniAsyncAttnPacket != null)
				{
					sniAsyncAttnPacket.Dispose();
				}
				if (sessionHandle != null)
				{
					sessionHandle.Dispose();
					base.DecrementPendingCallbacks(true);
				}
			}
			this.DisposePacketCache();
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00086854 File Offset: 0x00084A54
		internal override void DisposePacketCache()
		{
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Dispose();
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00005E03 File Offset: 0x00004003
		protected override void FreeGcHandle(int remaining, bool release)
		{
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0008689C File Offset: 0x00084A9C
		internal override bool IsFailedHandle()
		{
			return this._sessionHandle.Status > 0U;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000868AC File Offset: 0x00084AAC
		internal override object ReadSyncOverAsync(int timeoutRemaining, bool isMarsOn, out uint error)
		{
			SNIHandle handle = this.Handle;
			if (handle == null)
			{
				throw ADP.ClosedConnectionError();
			}
			if (isMarsOn)
			{
				base.IncrementPendingCallbacks();
			}
			SNIPacket snipacket = null;
			error = SNIProxy.Singleton.ReadSyncOverAsync(handle, out snipacket, timeoutRemaining);
			return snipacket;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000868E6 File Offset: 0x00084AE6
		internal override bool IsPacketEmpty(object packet)
		{
			return packet == null;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000868EC File Offset: 0x00084AEC
		internal override void ReleasePacket(object syncReadPacket)
		{
			((SNIPacket)syncReadPacket).Dispose();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000868FC File Offset: 0x00084AFC
		internal override uint CheckConnection()
		{
			SNIHandle handle = this.Handle;
			if (handle != null)
			{
				return SNIProxy.Singleton.CheckConnection(handle);
			}
			return 0U;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00086920 File Offset: 0x00084B20
		internal override object ReadAsync(out uint error, ref object handle)
		{
			SNIPacket snipacket;
			error = SNIProxy.Singleton.ReadAsync((SNIHandle)handle, out snipacket);
			return snipacket;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00086944 File Offset: 0x00084B44
		internal override object CreateAndSetAttentionPacket()
		{
			SNIPacket snipacket = new SNIPacket(this.Handle);
			this._sniAsyncAttnPacket = snipacket;
			this.SetPacketData(snipacket, SQL.AttentionHeader, 8);
			return snipacket;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00086972 File Offset: 0x00084B72
		internal override uint WritePacket(object packet, bool sync)
		{
			return SNIProxy.Singleton.WritePacket(this.Handle, (SNIPacket)packet, sync);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00005DA6 File Offset: 0x00003FA6
		internal override object AddPacketToPendingList(object packet)
		{
			return packet;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0008698B File Offset: 0x00084B8B
		internal override bool IsValidPacket(object packetPointer)
		{
			return (SNIPacket)packetPointer != null && !((SNIPacket)packetPointer).IsInvalid;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000869A8 File Offset: 0x00084BA8
		internal override object GetResetWritePacket()
		{
			if (this._sniPacket != null)
			{
				this._sniPacket.Reset();
			}
			else
			{
				object writePacketLockObject = this._writePacketLockObject;
				lock (writePacketLockObject)
				{
					this._sniPacket = this._writePacketCache.Take(this.Handle);
				}
			}
			return this._sniPacket;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00086A14 File Offset: 0x00084C14
		internal override void ClearAllWritePackets()
		{
			if (this._sniPacket != null)
			{
				this._sniPacket.Dispose();
				this._sniPacket = null;
			}
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Clear();
			}
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00086A74 File Offset: 0x00084C74
		internal override void SetPacketData(object packet, byte[] buffer, int bytesUsed)
		{
			SNIProxy.Singleton.PacketSetData((SNIPacket)packet, buffer, bytesUsed);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00086A88 File Offset: 0x00084C88
		internal override uint SniGetConnectionId(ref Guid clientConnectionId)
		{
			return SNIProxy.Singleton.GetConnectionId(this.Handle, ref clientConnectionId);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00086A9B File Offset: 0x00084C9B
		internal override uint DisabeSsl()
		{
			return SNIProxy.Singleton.DisableSsl(this.Handle);
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00086AAD File Offset: 0x00084CAD
		internal override uint EnableMars(ref uint info)
		{
			this._marsConnection = new SNIMarsConnection(this.Handle);
			if (this._marsConnection.StartReceive() == 997U)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00086AD5 File Offset: 0x00084CD5
		internal override uint EnableSsl(ref uint info)
		{
			return SNIProxy.Singleton.EnableSsl(this.Handle, info);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00086AE9 File Offset: 0x00084CE9
		internal override uint SetConnectionBufferSize(ref uint unsignedPacketSize)
		{
			return SNIProxy.Singleton.SetConnectionBufferSize(this.Handle, unsignedPacketSize);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00086AFD File Offset: 0x00084CFD
		internal override uint GenerateSspiClientContext(byte[] receivedBuff, uint receivedLength, ref byte[] sendBuff, ref uint sendLength, byte[] _sniSpnBuffer)
		{
			SNIProxy.Singleton.GenSspiClientContext(this.sspiClientContextStatus, receivedBuff, ref sendBuff, _sniSpnBuffer);
			sendLength = (uint)((sendBuff != null) ? sendBuff.Length : 0);
			return 0U;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000061D5 File Offset: 0x000043D5
		internal override uint WaitForSSLHandShakeToComplete()
		{
			return 0U;
		}

		// Token: 0x04001324 RID: 4900
		private SNIMarsConnection _marsConnection;

		// Token: 0x04001325 RID: 4901
		private SNIHandle _sessionHandle;

		// Token: 0x04001326 RID: 4902
		private SNIPacket _sniPacket;

		// Token: 0x04001327 RID: 4903
		internal SNIPacket _sniAsyncAttnPacket;

		// Token: 0x04001328 RID: 4904
		private readonly Dictionary<SNIPacket, SNIPacket> _pendingWritePackets = new Dictionary<SNIPacket, SNIPacket>();

		// Token: 0x04001329 RID: 4905
		private readonly TdsParserStateObjectManaged.WritePacketCache _writePacketCache = new TdsParserStateObjectManaged.WritePacketCache();

		// Token: 0x0400132A RID: 4906
		internal SspiClientContextStatus sspiClientContextStatus = new SspiClientContextStatus();

		// Token: 0x0200025D RID: 605
		internal sealed class WritePacketCache : IDisposable
		{
			// Token: 0x06001AC5 RID: 6853 RVA: 0x00086B22 File Offset: 0x00084D22
			public WritePacketCache()
			{
				this._disposed = false;
				this._packets = new Stack<SNIPacket>();
			}

			// Token: 0x06001AC6 RID: 6854 RVA: 0x00086B3C File Offset: 0x00084D3C
			public SNIPacket Take(SNIHandle sniHandle)
			{
				SNIPacket snipacket;
				if (this._packets.Count > 0)
				{
					snipacket = this._packets.Pop();
					snipacket.Reset();
				}
				else
				{
					snipacket = new SNIPacket(sniHandle);
				}
				return snipacket;
			}

			// Token: 0x06001AC7 RID: 6855 RVA: 0x00086B73 File Offset: 0x00084D73
			public void Add(SNIPacket packet)
			{
				if (!this._disposed)
				{
					this._packets.Push(packet);
					return;
				}
				packet.Dispose();
			}

			// Token: 0x06001AC8 RID: 6856 RVA: 0x00086B90 File Offset: 0x00084D90
			public void Clear()
			{
				while (this._packets.Count > 0)
				{
					this._packets.Pop().Dispose();
				}
			}

			// Token: 0x06001AC9 RID: 6857 RVA: 0x00086BB2 File Offset: 0x00084DB2
			public void Dispose()
			{
				if (!this._disposed)
				{
					this._disposed = true;
					this.Clear();
				}
			}

			// Token: 0x0400132B RID: 4907
			private bool _disposed;

			// Token: 0x0400132C RID: 4908
			private Stack<SNIPacket> _packets;
		}
	}
}
