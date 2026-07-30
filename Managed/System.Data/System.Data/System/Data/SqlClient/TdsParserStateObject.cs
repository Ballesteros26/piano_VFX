using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x02000227 RID: 551
	internal abstract class TdsParserStateObject
	{
		// Token: 0x060018A5 RID: 6309 RVA: 0x0007DA20 File Offset: 0x0007BC20
		internal TdsParserStateObject(TdsParser parser)
		{
			this._parser = parser;
			this.SetPacketSize(4096);
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = new LastIOTimer();
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0007DAC4 File Offset: 0x0007BCC4
		internal TdsParserStateObject(TdsParser parser, TdsParserStateObject physicalConnection, bool async)
		{
			this._parser = parser;
			this.SniContext = SniContext.Snix_GetMarsSession;
			this.SetPacketSize(this._parser._physicalStateObj._outBuff.Length);
			this.CreateSessionHandle(physicalConnection, async);
			if (this.IsFailedHandle())
			{
				this.AddError(parser.ProcessSNIError(this));
				this.ThrowExceptionAndWarning(false, false);
			}
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = parser._physicalStateObj._lastSuccessfulIOTimer;
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0007DBA6 File Offset: 0x0007BDA6
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x0007DBAE File Offset: 0x0007BDAE
		internal bool BcpLock
		{
			get
			{
				return this._bcpLock;
			}
			set
			{
				this._bcpLock = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0007DBB7 File Offset: 0x0007BDB7
		internal bool HasOpenResult
		{
			get
			{
				return this._hasOpenResult;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0007DBBF File Offset: 0x0007BDBF
		internal bool IsOrphaned
		{
			get
			{
				return this._activateCount != 0 && !this._owner.IsAlive;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x0007DBDC File Offset: 0x0007BDDC
		internal object Owner
		{
			set
			{
				SqlDataReader sqlDataReader = value as SqlDataReader;
				if (sqlDataReader == null)
				{
					this._readerState = null;
				}
				else
				{
					this._readerState = sqlDataReader._sharedState;
				}
				this._owner.Target = value;
			}
		}

		// Token: 0x060018AC RID: 6316
		internal abstract uint DisabeSsl();

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0007DC14 File Offset: 0x0007BE14
		internal bool HasOwner
		{
			get
			{
				return this._owner.IsAlive;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0007DC21 File Offset: 0x0007BE21
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x060018AF RID: 6319
		internal abstract uint EnableMars(ref uint info);

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0007DC29 File Offset: 0x0007BE29
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0007DC31 File Offset: 0x0007BE31
		internal SniContext SniContext
		{
			get
			{
				return this._sniContext;
			}
			set
			{
				this._sniContext = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060018B2 RID: 6322
		internal abstract uint Status { get; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060018B3 RID: 6323
		internal abstract object SessionHandle { get; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0007DC3A File Offset: 0x0007BE3A
		internal bool TimeoutHasExpired
		{
			get
			{
				return TdsParserStaticMethods.TimeoutHasExpired(this._timeoutTime);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0007DC47 File Offset: 0x0007BE47
		// (set) Token: 0x060018B6 RID: 6326 RVA: 0x0007DC70 File Offset: 0x0007BE70
		internal long TimeoutTime
		{
			get
			{
				if (this._timeoutMilliseconds != 0L)
				{
					this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
					this._timeoutMilliseconds = 0L;
				}
				return this._timeoutTime;
			}
			set
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = value;
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0007DC84 File Offset: 0x0007BE84
		internal int GetTimeoutRemaining()
		{
			int num;
			if (this._timeoutMilliseconds != 0L)
			{
				num = (int)Math.Min(2147483647L, this._timeoutMilliseconds);
				this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
				this._timeoutMilliseconds = 0L;
			}
			else
			{
				num = TdsParserStaticMethods.GetTimeoutMilliseconds(this._timeoutTime);
			}
			return num;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0007DCD4 File Offset: 0x0007BED4
		internal bool TryStartNewRow(bool isNullCompressed, int nullBitmapColumnsCount = 0)
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneNullBitmapInfo();
			}
			if (isNullCompressed)
			{
				if (!this._nullBitmapInfo.TryInitialize(this, nullBitmapColumnsCount))
				{
					return false;
				}
			}
			else
			{
				this._nullBitmapInfo.Clean();
			}
			return true;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0007DD0C File Offset: 0x0007BF0C
		internal bool IsRowTokenReady()
		{
			int num = Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed) - 1;
			if (num > 0)
			{
				if (this._inBuff[this._inBytesUsed] == 209)
				{
					return true;
				}
				if (this._inBuff[this._inBytesUsed] == 210)
				{
					return 1 + (this._cleanupMetaData.Length + 7) / 8 <= num;
				}
			}
			return false;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0007DD7B File Offset: 0x0007BF7B
		internal bool IsNullCompressionBitSet(int columnOrdinal)
		{
			return this._nullBitmapInfo.IsGuaranteedNull(columnOrdinal);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0007DD89 File Offset: 0x0007BF89
		internal void Activate(object owner)
		{
			this.Owner = owner;
			Interlocked.Increment(ref this._activateCount);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0007DDA0 File Offset: 0x0007BFA0
		internal void Cancel(object caller)
		{
			bool flag = false;
			try
			{
				while (!flag && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
				{
					Monitor.TryEnter(this, 100, ref flag);
					if (flag && !this._cancelled && this._cancellationOwner.Target == caller)
					{
						this._cancelled = true;
						if (this._pendingData && !this._attentionSent)
						{
							bool flag2 = false;
							while (!flag2 && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
							{
								try
								{
									this._parser.Connection._parserLock.Wait(false, 100, ref flag2);
									if (flag2)
									{
										this._parser.Connection.ThreadHasParserLockForClose = true;
										this.SendAttention(false);
									}
								}
								finally
								{
									if (flag2)
									{
										if (this._parser.Connection.ThreadHasParserLockForClose)
										{
											this._parser.Connection.ThreadHasParserLockForClose = false;
										}
										this._parser.Connection._parserLock.Release();
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0007DEFC File Offset: 0x0007C0FC
		internal void CancelRequest()
		{
			this.ResetBuffer();
			this._outputPacketNumber = 1;
			if (!this._bulkCopyWriteTimeout)
			{
				this.SendAttention(false);
				this.Parser.ProcessPendingAck(this);
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0007DF28 File Offset: 0x0007C128
		public void CheckSetResetConnectionState(uint error, CallbackType callbackType)
		{
			if (this._fResetEventOwned)
			{
				if (callbackType == CallbackType.Read && error == 0U)
				{
					this._parser._fResetConnection = false;
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
				if (error != 0U)
				{
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0007DF9E File Offset: 0x0007C19E
		internal void CloseSession()
		{
			this.ResetCancelAndProcessAttention();
			this.Parser.PutSession(this);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0007DFB4 File Offset: 0x0007C1B4
		private void ResetCancelAndProcessAttention()
		{
			lock (this)
			{
				this._cancelled = false;
				this._cancellationOwner.Target = null;
				if (this._attentionSent)
				{
					this.Parser.ProcessPendingAck(this);
				}
				this._internalTimeout = false;
			}
		}

		// Token: 0x060018C1 RID: 6337
		internal abstract void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool fParallel, bool isIntegratedSecurity = false);

		// Token: 0x060018C2 RID: 6338
		internal abstract uint SniGetConnectionId(ref Guid clientConnectionId);

		// Token: 0x060018C3 RID: 6339
		internal abstract bool IsFailedHandle();

		// Token: 0x060018C4 RID: 6340
		protected abstract void CreateSessionHandle(TdsParserStateObject physicalConnection, bool async);

		// Token: 0x060018C5 RID: 6341
		protected abstract void FreeGcHandle(int remaining, bool release);

		// Token: 0x060018C6 RID: 6342
		internal abstract uint EnableSsl(ref uint info);

		// Token: 0x060018C7 RID: 6343
		internal abstract uint WaitForSSLHandShakeToComplete();

		// Token: 0x060018C8 RID: 6344
		internal abstract void Dispose();

		// Token: 0x060018C9 RID: 6345
		internal abstract void DisposePacketCache();

		// Token: 0x060018CA RID: 6346
		internal abstract bool IsPacketEmpty(object readPacket);

		// Token: 0x060018CB RID: 6347
		internal abstract object ReadSyncOverAsync(int timeoutRemaining, bool isMarsOn, out uint error);

		// Token: 0x060018CC RID: 6348
		internal abstract object ReadAsync(out uint error, ref object handle);

		// Token: 0x060018CD RID: 6349
		internal abstract uint CheckConnection();

		// Token: 0x060018CE RID: 6350
		internal abstract uint SetConnectionBufferSize(ref uint unsignedPacketSize);

		// Token: 0x060018CF RID: 6351
		internal abstract void ReleasePacket(object syncReadPacket);

		// Token: 0x060018D0 RID: 6352
		protected abstract uint SNIPacketGetData(object packet, byte[] _inBuff, ref uint dataSize);

		// Token: 0x060018D1 RID: 6353
		internal abstract object GetResetWritePacket();

		// Token: 0x060018D2 RID: 6354
		internal abstract void ClearAllWritePackets();

		// Token: 0x060018D3 RID: 6355
		internal abstract object AddPacketToPendingList(object packet);

		// Token: 0x060018D4 RID: 6356
		protected abstract void RemovePacketFromPendingList(object pointer);

		// Token: 0x060018D5 RID: 6357
		internal abstract uint GenerateSspiClientContext(byte[] receivedBuff, uint receivedLength, ref byte[] sendBuff, ref uint sendLength, byte[] _sniSpnBuffer);

		// Token: 0x060018D6 RID: 6358 RVA: 0x0007E01C File Offset: 0x0007C21C
		internal bool Deactivate()
		{
			bool flag = false;
			try
			{
				TdsParserState state = this.Parser.State;
				if (state != TdsParserState.Broken && state != TdsParserState.Closed)
				{
					if (this._pendingData)
					{
						this.Parser.DrainData(this);
					}
					if (this.HasOpenResult)
					{
						this.DecrementOpenResultCount();
					}
					this.ResetCancelAndProcessAttention();
					flag = true;
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
			}
			return flag;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0007E088 File Offset: 0x0007C288
		internal void RemoveOwner()
		{
			if (this._parser.MARSOn)
			{
				Interlocked.Decrement(ref this._activateCount);
			}
			this.Owner = null;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0007E0AA File Offset: 0x0007C2AA
		internal void DecrementOpenResultCount()
		{
			if (this._executedUnderTransaction == null)
			{
				this._parser.DecrementNonTransactedOpenResultCount();
			}
			else
			{
				this._executedUnderTransaction.DecrementAndObtainOpenResultCount();
				this._executedUnderTransaction = null;
			}
			this._hasOpenResult = false;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0007E0DC File Offset: 0x0007C2DC
		internal int DecrementPendingCallbacks(bool release)
		{
			int num = Interlocked.Decrement(ref this._pendingCallbacks);
			this.FreeGcHandle(num, release);
			return num;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0007E100 File Offset: 0x0007C300
		internal void DisposeCounters()
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				this._networkPacketTimeout = null;
				networkPacketTimeout.Dispose();
			}
			if (Volatile.Read(ref this._readingCount) > 0)
			{
				SpinWait.SpinUntil(() => Volatile.Read(ref this._readingCount) == 0);
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0007E143 File Offset: 0x0007C343
		internal int IncrementAndObtainOpenResultCount(SqlInternalTransaction transaction)
		{
			this._hasOpenResult = true;
			if (transaction == null)
			{
				return this._parser.IncrementNonTransactedOpenResultCount();
			}
			this._executedUnderTransaction = transaction;
			return transaction.IncrementAndObtainOpenResultCount();
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0007E168 File Offset: 0x0007C368
		internal int IncrementPendingCallbacks()
		{
			return Interlocked.Increment(ref this._pendingCallbacks);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0007E175 File Offset: 0x0007C375
		internal void SetTimeoutSeconds(int timeout)
		{
			this.SetTimeoutMilliseconds((long)timeout * 1000L);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0007E186 File Offset: 0x0007C386
		internal void SetTimeoutMilliseconds(long timeout)
		{
			if (timeout <= 0L)
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = long.MaxValue;
				return;
			}
			this._timeoutMilliseconds = timeout;
			this._timeoutTime = 0L;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0007E1B4 File Offset: 0x0007C3B4
		internal void StartSession(object cancellationOwner)
		{
			this._cancellationOwner.Target = cancellationOwner;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0007E1C2 File Offset: 0x0007C3C2
		internal void ThrowExceptionAndWarning(bool callerHasConnectionLock = false, bool asyncClose = false)
		{
			this._parser.ThrowExceptionAndWarning(this, callerHasConnectionLock, asyncClose);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0007E1D4 File Offset: 0x0007C3D4
		internal Task ExecuteFlush()
		{
			Task task2;
			lock (this)
			{
				if (this._cancelled && 1 == this._outputPacketNumber)
				{
					this.ResetBuffer();
					this._cancelled = false;
					throw SQL.OperationCancelled();
				}
				Task task = this.WritePacket(1, false);
				if (task == null)
				{
					this._pendingData = true;
					this._messageStatus = 0;
					task2 = null;
				}
				else
				{
					task2 = AsyncHelper.CreateContinuationTask(task, delegate
					{
						this._pendingData = true;
						this._messageStatus = 0;
					}, null, null);
				}
			}
			return task2;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0007E264 File Offset: 0x0007C464
		internal bool TryProcessHeader()
		{
			if (this._partialHeaderBytesRead > 0 || this._inBytesUsed + this._inputHeaderLen > this._inBytesRead)
			{
				for (;;)
				{
					int num = Math.Min(this._inBytesRead - this._inBytesUsed, this._inputHeaderLen - this._partialHeaderBytesRead);
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, this._partialHeaderBuffer, this._partialHeaderBytesRead, num);
					this._partialHeaderBytesRead += num;
					this._inBytesUsed += num;
					if (this._partialHeaderBytesRead == this._inputHeaderLen)
					{
						this._partialHeaderBytesRead = 0;
						this._inBytesPacket = (((int)this._partialHeaderBuffer[2] << 8) | (int)this._partialHeaderBuffer[3]) - this._inputHeaderLen;
						this._messageStatus = this._partialHeaderBuffer[1];
					}
					else
					{
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							break;
						}
						if (!this.TryReadNetworkPacket())
						{
							return false;
						}
						if (this._internalTimeout)
						{
							goto Block_5;
						}
					}
					if (this._partialHeaderBytesRead == 0)
					{
						goto Block_6;
					}
				}
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_5:
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_6:;
			}
			else
			{
				this._messageStatus = this._inBuff[this._inBytesUsed + 1];
				this._inBytesPacket = (((int)this._inBuff[this._inBytesUsed + 2] << 8) | (int)this._inBuff[this._inBytesUsed + 2 + 1]) - this._inputHeaderLen;
				this._inBytesUsed += this._inputHeaderLen;
			}
			if (this._inBytesPacket < 0)
			{
				throw SQL.ParsingError();
			}
			return true;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0007E3E8 File Offset: 0x0007C5E8
		internal bool TryPrepareBuffer()
		{
			if (this._inBytesPacket == 0 && this._inBytesUsed < this._inBytesRead && !this.TryProcessHeader())
			{
				return false;
			}
			if (this._inBytesUsed == this._inBytesRead)
			{
				if (this._inBytesPacket > 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
				}
				else if (this._inBytesPacket == 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
					if (!this.TryProcessHeader())
					{
						return false;
					}
					if (this._inBytesUsed == this._inBytesRead && !this.TryReadNetworkPacket())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0007E46B File Offset: 0x0007C66B
		internal void ResetBuffer()
		{
			this._outBytesUsed = this._outputHeaderLen;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0007E47C File Offset: 0x0007C67C
		internal bool SetPacketSize(int size)
		{
			if (size > 32768)
			{
				throw SQL.InvalidPacketSize();
			}
			if (this._inBuff == null || this._inBuff.Length != size)
			{
				if (this._inBuff == null)
				{
					this._inBuff = new byte[size];
					this._inBytesRead = 0;
					this._inBytesUsed = 0;
				}
				else if (size != this._inBuff.Length)
				{
					if (this._inBytesRead > this._inBytesUsed)
					{
						byte[] inBuff = this._inBuff;
						this._inBuff = new byte[size];
						int num = this._inBytesRead - this._inBytesUsed;
						if (inBuff.Length < this._inBytesUsed + num || this._inBuff.Length < num)
						{
							throw SQL.InvalidInternalPacketSize(string.Concat(new object[]
							{
								SR.GetString("Invalid internal packet size:"),
								" ",
								inBuff.Length,
								", ",
								this._inBytesUsed,
								", ",
								num,
								", ",
								this._inBuff.Length
							}));
						}
						Buffer.BlockCopy(inBuff, this._inBytesUsed, this._inBuff, 0, num);
						this._inBytesRead -= this._inBytesUsed;
						this._inBytesUsed = 0;
					}
					else
					{
						this._inBuff = new byte[size];
						this._inBytesRead = 0;
						this._inBytesUsed = 0;
					}
				}
				this._outBuff = new byte[size];
				this._outBytesUsed = this._outputHeaderLen;
				return true;
			}
			return false;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0007E602 File Offset: 0x0007C802
		internal bool TryPeekByte(out byte value)
		{
			if (!this.TryReadByte(out value))
			{
				return false;
			}
			this._inBytesPacket++;
			this._inBytesUsed--;
			return true;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0007E62C File Offset: 0x0007C82C
		public bool TryReadByteArray(byte[] buff, int offset, int len)
		{
			int num;
			return this.TryReadByteArray(buff, offset, len, out num);
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0007E644 File Offset: 0x0007C844
		public bool TryReadByteArray(byte[] buff, int offset, int len, out int totalRead)
		{
			totalRead = 0;
			while (len > 0)
			{
				if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
				{
					return false;
				}
				int num = Math.Min(len, Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed));
				if (buff != null)
				{
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, buff, offset + totalRead, num);
				}
				totalRead += num;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
				len -= num;
			}
			return this._messageStatus == 1 || (this._inBytesPacket != 0 && this._inBytesUsed != this._inBytesRead) || this.TryPrepareBuffer();
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0007E710 File Offset: 0x0007C910
		internal bool TryReadByte(out byte value)
		{
			value = 0;
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				return false;
			}
			this._inBytesPacket--;
			byte[] inBuff = this._inBuff;
			int inBytesUsed = this._inBytesUsed;
			this._inBytesUsed = inBytesUsed + 1;
			value = inBuff[inBytesUsed];
			return true;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0007E76C File Offset: 0x0007C96C
		internal bool TryReadChar(out char value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = '\0';
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (char)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0007E7EC File Offset: 0x0007C9EC
		internal bool TryReadInt16(out short value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (short)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0007E86C File Offset: 0x0007CA6C
		internal bool TryReadInt32(out int value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 4))
			{
				value = 0;
				return false;
			}
			value = BitConverter.ToInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0007E8E8 File Offset: 0x0007CAE8
		internal bool TryReadInt64(out long value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0L;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToInt64(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			int num = 0;
			if (!this.TryReadByteArray(this._bTmp, this._bTmpRead, 8 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0L;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToInt64(this._bTmp, 0);
			return true;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0007E9B8 File Offset: 0x0007CBB8
		internal bool TryReadUInt16(out ushort value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (ushort)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0007EA38 File Offset: 0x0007CC38
		internal bool TryReadUInt32(out uint value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0U;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToUInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			int num = 0;
			if (!this.TryReadByteArray(this._bTmp, this._bTmpRead, 4 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0U;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToUInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0007EB04 File Offset: 0x0007CD04
		internal bool TryReadSingle(out float value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToSingle(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 4))
			{
				value = 0f;
				return false;
			}
			value = BitConverter.ToSingle(this._bTmp, 0);
			return true;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0007EB84 File Offset: 0x0007CD84
		internal bool TryReadDouble(out double value)
		{
			if (this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToDouble(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 8))
			{
				value = 0.0;
				return false;
			}
			value = BitConverter.ToDouble(this._bTmp, 0);
			return true;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0007EC08 File Offset: 0x0007CE08
		internal bool TryReadString(int length, out string value)
		{
			int num = length << 1;
			int num2 = 0;
			byte[] array;
			if (this._inBytesUsed + num > this._inBytesRead || this._inBytesPacket < num)
			{
				if (this._bTmp == null || this._bTmp.Length < num)
				{
					this._bTmp = new byte[num];
				}
				if (!this.TryReadByteArray(this._bTmp, 0, num))
				{
					value = null;
					return false;
				}
				array = this._bTmp;
			}
			else
			{
				array = this._inBuff;
				num2 = this._inBytesUsed;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
			}
			value = Encoding.Unicode.GetString(array, num2, num);
			return true;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0007ECAC File Offset: 0x0007CEAC
		internal bool TryReadStringWithEncoding(int length, Encoding encoding, bool isPlp, out string value)
		{
			if (encoding == null)
			{
				if (isPlp)
				{
					ulong num;
					if (!this._parser.TrySkipPlpValue((ulong)((long)length), this, out num))
					{
						value = null;
						return false;
					}
				}
				else if (!this.TrySkipBytes(length))
				{
					value = null;
					return false;
				}
				this._parser.ThrowUnsupportedCollationEncountered(this);
			}
			byte[] array = null;
			int num2 = 0;
			if (isPlp)
			{
				if (!this.TryReadPlpBytes(ref array, 0, 2147483647, out length))
				{
					value = null;
					return false;
				}
			}
			else if (this._inBytesUsed + length > this._inBytesRead || this._inBytesPacket < length)
			{
				if (this._bTmp == null || this._bTmp.Length < length)
				{
					this._bTmp = new byte[length];
				}
				if (!this.TryReadByteArray(this._bTmp, 0, length))
				{
					value = null;
					return false;
				}
				array = this._bTmp;
			}
			else
			{
				array = this._inBuff;
				num2 = this._inBytesUsed;
				this._inBytesUsed += length;
				this._inBytesPacket -= length;
			}
			value = encoding.GetString(array, num2, length);
			return true;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0007EDA4 File Offset: 0x0007CFA4
		internal ulong ReadPlpLength(bool returnPlpNullIfNull)
		{
			ulong num;
			if (!this.TryReadPlpLength(returnPlpNullIfNull, out num))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0007EDC4 File Offset: 0x0007CFC4
		internal bool TryReadPlpLength(bool returnPlpNullIfNull, out ulong lengthLeft)
		{
			bool flag = false;
			if (this._longlen == 0UL)
			{
				long num;
				if (!this.TryReadInt64(out num))
				{
					lengthLeft = 0UL;
					return false;
				}
				this._longlen = (ulong)num;
			}
			if (this._longlen == 18446744073709551615UL)
			{
				this._longlen = 0UL;
				this._longlenleft = 0UL;
				flag = true;
			}
			else
			{
				uint num2;
				if (!this.TryReadUInt32(out num2))
				{
					lengthLeft = 0UL;
					return false;
				}
				if (num2 == 0U)
				{
					this._longlenleft = 0UL;
					this._longlen = 0UL;
				}
				else
				{
					this._longlenleft = (ulong)num2;
				}
			}
			if (flag && returnPlpNullIfNull)
			{
				lengthLeft = ulong.MaxValue;
				return true;
			}
			lengthLeft = this._longlenleft;
			return true;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0007EE54 File Offset: 0x0007D054
		internal int ReadPlpBytesChunk(byte[] buff, int offset, int len)
		{
			int num = (int)Math.Min(this._longlenleft, (ulong)((long)len));
			int num2;
			bool flag = this.TryReadByteArray(buff, offset, num, out num2);
			this._longlenleft -= (ulong)((long)num);
			if (!flag)
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return num2;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0007EE94 File Offset: 0x0007D094
		internal bool TryReadPlpBytes(ref byte[] buff, int offset, int len, out int totalBytesRead)
		{
			int num = 0;
			if (this._longlen == 0UL)
			{
				if (buff == null)
				{
					buff = Array.Empty<byte>();
				}
				totalBytesRead = 0;
				return true;
			}
			int i = len;
			if (buff == null && this._longlen != 18446744073709551614UL)
			{
				buff = new byte[Math.Min((int)this._longlen, len)];
			}
			if (this._longlenleft == 0UL)
			{
				ulong num2;
				if (!this.TryReadPlpLength(false, out num2))
				{
					totalBytesRead = 0;
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					totalBytesRead = 0;
					return true;
				}
			}
			if (buff == null)
			{
				buff = new byte[this._longlenleft];
			}
			totalBytesRead = 0;
			while (i > 0)
			{
				int num3 = (int)Math.Min(this._longlenleft, (ulong)((long)i));
				if (buff.Length < offset + num3)
				{
					byte[] array = new byte[offset + num3];
					Buffer.BlockCopy(buff, 0, array, 0, offset);
					buff = array;
				}
				bool flag = this.TryReadByteArray(buff, offset, num3, out num);
				i -= num;
				offset += num;
				totalBytesRead += num;
				this._longlenleft -= (ulong)((long)num);
				if (!flag)
				{
					return false;
				}
				ulong num2;
				if (this._longlenleft == 0UL && !this.TryReadPlpLength(false, out num2))
				{
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0007EFAC File Offset: 0x0007D1AC
		internal bool TrySkipLongBytes(long num)
		{
			while (num > 0L)
			{
				int num2 = (int)Math.Min(2147483647L, num);
				if (!this.TryReadByteArray(null, 0, num2))
				{
					return false;
				}
				num -= (long)num2;
			}
			return true;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0007EFE4 File Offset: 0x0007D1E4
		internal bool TrySkipBytes(int num)
		{
			return this.TryReadByteArray(null, 0, num);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0007EFEF File Offset: 0x0007D1EF
		internal void SetSnapshot()
		{
			this._snapshot = new TdsParserStateObject.StateSnapshot(this);
			this._snapshot.Snap();
			this._snapshotReplay = false;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0007F00F File Offset: 0x0007D20F
		internal void ResetSnapshot()
		{
			this._snapshot = null;
			this._snapshotReplay = false;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0007F020 File Offset: 0x0007D220
		internal bool TryReadNetworkPacket()
		{
			if (this._snapshot != null)
			{
				if (this._snapshotReplay && this._snapshot.Replay())
				{
					return true;
				}
				this._inBuff = new byte[this._inBuff.Length];
			}
			if (this._syncOverAsync)
			{
				this.ReadSniSyncOverAsync();
				return true;
			}
			this.ReadSni(new TaskCompletionSource<object>());
			return false;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0007F07B File Offset: 0x0007D27B
		internal void PrepareReplaySnapshot()
		{
			this._networkPacketTaskSource = null;
			this._snapshot.PrepareReplay();
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0007F090 File Offset: 0x0007D290
		internal void ReadSniSyncOverAsync()
		{
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			object obj = null;
			bool flag = false;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				flag = true;
				uint num;
				obj = this.ReadSyncOverAsync(this.GetTimeoutRemaining(), false, out num);
				Interlocked.Decrement(ref this._readingCount);
				flag = false;
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(num, CallbackType.Read);
				}
				if (num == 0U)
				{
					this.ProcessSniPacket(obj, 0U);
				}
				else
				{
					this.ReadSniError(this, num);
				}
			}
			finally
			{
				if (flag)
				{
					Interlocked.Decrement(ref this._readingCount);
				}
				if (!this.IsPacketEmpty(obj))
				{
					this.ReleasePacket(obj);
				}
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0007F14C File Offset: 0x0007D34C
		internal void OnConnectionClosed()
		{
			this.Parser.State = TdsParserState.Broken;
			this.Parser.Connection.BreakConnection();
			Interlocked.MemoryBarrier();
			TaskCompletionSource<object> taskCompletionSource = this._networkPacketTaskSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
			taskCompletionSource = this._writeCompletionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0007F1B4 File Offset: 0x0007D3B4
		private void OnTimeout(object state)
		{
			if (!this._internalTimeout)
			{
				this._internalTimeout = true;
				lock (this)
				{
					if (!this._attentionSent)
					{
						this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
						TaskCompletionSource<object> source = this._networkPacketTaskSource;
						if (this._parser.Connection.IsInPool)
						{
							this._parser.State = TdsParserState.Broken;
							this._parser.Connection.BreakConnection();
							if (source != null)
							{
								source.TrySetCanceled();
							}
						}
						else if (this._parser.State == TdsParserState.OpenLoggedIn)
						{
							try
							{
								this.SendAttention(true);
							}
							catch (Exception ex)
							{
								if (!ADP.IsCatchableExceptionType(ex))
								{
									throw;
								}
								if (source != null)
								{
									source.TrySetCanceled();
								}
							}
						}
						if (source != null)
						{
							Task.Delay(5000).ContinueWith(delegate(Task _)
							{
								if (!source.Task.IsCompleted)
								{
									int num = this.IncrementPendingCallbacks();
									try
									{
										if (num == 3 && !source.Task.IsCompleted)
										{
											bool flag2 = false;
											try
											{
												this.CheckThrowSNIException();
											}
											catch (Exception ex2)
											{
												if (source.TrySetException(ex2))
												{
													flag2 = true;
												}
											}
											this._parser.State = TdsParserState.Broken;
											this._parser.Connection.BreakConnection();
											if (!flag2)
											{
												source.TrySetCanceled();
											}
										}
									}
									finally
									{
										this.DecrementPendingCallbacks(false);
									}
								}
							});
						}
					}
				}
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0007F320 File Offset: 0x0007D520
		internal void ReadSni(TaskCompletionSource<object> completion)
		{
			this._networkPacketTaskSource = completion;
			Interlocked.MemoryBarrier();
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			object obj = null;
			uint num = 0U;
			try
			{
				if (this._networkPacketTimeout == null)
				{
					this._networkPacketTimeout = new Timer(new TimerCallback(this.OnTimeout), null, -1, -1);
				}
				int timeoutRemaining = this.GetTimeoutRemaining();
				if (timeoutRemaining > 0)
				{
					this.ChangeNetworkPacketTimeout(timeoutRemaining, -1);
				}
				object obj2 = null;
				Interlocked.Increment(ref this._readingCount);
				obj2 = this.SessionHandle;
				if (obj2 != null)
				{
					this.IncrementPendingCallbacks();
					obj = this.ReadAsync(out num, ref obj2);
					if (num != 0U && 997U != num)
					{
						this.DecrementPendingCallbacks(false);
					}
				}
				Interlocked.Decrement(ref this._readingCount);
				if (obj2 == null)
				{
					throw ADP.ClosedConnectionError();
				}
				if (num == 0U)
				{
					this.ReadAsyncCallback<object>(IntPtr.Zero, obj, 0U);
				}
				else if (997U != num)
				{
					this.ReadSniError(this, num);
					this._networkPacketTaskSource.TrySetResult(null);
					this.ChangeNetworkPacketTimeout(-1, -1);
				}
				else if (timeoutRemaining == 0)
				{
					this.ChangeNetworkPacketTimeout(0, -1);
				}
			}
			finally
			{
				if (!TdsParserStateObjectFactory.UseManagedSNI && !this.IsPacketEmpty(obj))
				{
					this.ReleasePacket(obj);
				}
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0007F454 File Offset: 0x0007D654
		internal bool IsConnectionAlive(bool throwOnException)
		{
			bool flag = true;
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value > 50000L)
			{
				if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					flag = false;
					if (throwOnException)
					{
						throw SQL.ConnectionDoomed();
					}
				}
				else if (this._pendingCallbacks <= 1 && (this._parser.Connection == null || this._parser.Connection.IsInPool))
				{
					object emptyReadPacket = this.EmptyReadPacket;
					try
					{
						this.SniContext = SniContext.Snix_Connect;
						uint num = this.CheckConnection();
						if (num != 0U && num != 258U)
						{
							flag = false;
							if (throwOnException)
							{
								this.AddError(this._parser.ProcessSNIError(this));
								this.ThrowExceptionAndWarning(false, false);
							}
						}
						else
						{
							this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
						}
					}
					finally
					{
						if (!this.IsPacketEmpty(emptyReadPacket))
						{
							this.ReleasePacket(emptyReadPacket);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0007F560 File Offset: 0x0007D760
		internal bool ValidateSNIConnection()
		{
			if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				return false;
			}
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value <= 50000L)
			{
				return true;
			}
			uint num = 0U;
			this.SniContext = SniContext.Snix_Connect;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				num = this.CheckConnection();
			}
			finally
			{
				Interlocked.Decrement(ref this._readingCount);
			}
			return num == 0U || num == 258U;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0007F600 File Offset: 0x0007D800
		private void ReadSniError(TdsParserStateObject stateObj, uint error)
		{
			if (258U == error)
			{
				bool flag = false;
				if (this._internalTimeout)
				{
					flag = true;
				}
				else
				{
					stateObj._internalTimeout = true;
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
					if (!stateObj._attentionSent)
					{
						if (stateObj.Parser.State == TdsParserState.OpenLoggedIn)
						{
							stateObj.SendAttention(true);
							object obj = null;
							bool flag2 = false;
							try
							{
								Interlocked.Increment(ref this._readingCount);
								flag2 = true;
								obj = this.ReadSyncOverAsync(stateObj.GetTimeoutRemaining(), this._parser.MARSOn, out error);
								Interlocked.Decrement(ref this._readingCount);
								flag2 = false;
								if (error == 0U)
								{
									stateObj.ProcessSniPacket(obj, 0U);
									return;
								}
								flag = true;
								goto IL_013D;
							}
							finally
							{
								if (flag2)
								{
									Interlocked.Decrement(ref this._readingCount);
								}
								if (!this.IsPacketEmpty(obj))
								{
									this.ReleasePacket(obj);
								}
							}
						}
						if (this._parser._loginWithFailover)
						{
							this._parser.Disconnect();
						}
						else if (this._parser.State == TdsParserState.OpenNotLoggedIn && this._parser.Connection.ConnectionOptions.MultiSubnetFailover)
						{
							this._parser.Disconnect();
						}
						else
						{
							flag = true;
						}
					}
				}
				IL_013D:
				if (flag)
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
				}
			}
			else
			{
				this.AddError(this._parser.ProcessSNIError(stateObj));
			}
			this.ThrowExceptionAndWarning(false, false);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0007F798 File Offset: 0x0007D998
		public void ProcessSniPacket(object packet, uint error)
		{
			if (error != 0U)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				this.AddError(this._parser.ProcessSNIError(this));
				return;
			}
			else
			{
				uint num = 0U;
				if (this.SNIPacketGetData(packet, this._inBuff, ref num) != 0U)
				{
					throw SQL.ParsingError();
				}
				if ((long)this._inBuff.Length < (long)((ulong)num))
				{
					throw SQL.InvalidInternalPacketSize(SR.GetString("Invalid array size."));
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
				this._inBytesRead = (int)num;
				this._inBytesUsed = 0;
				if (this._snapshot != null)
				{
					this._snapshot.PushBuffer(this._inBuff, this._inBytesRead);
					if (this._snapshotReplay)
					{
						this._snapshot.Replay();
					}
				}
				this.SniReadStatisticsAndTracing();
				return;
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0007F86C File Offset: 0x0007DA6C
		private void ChangeNetworkPacketTimeout(int dueTime, int period)
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				try
				{
					networkPacketTimeout.Change(dueTime, period);
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0007F8A4 File Offset: 0x0007DAA4
		public void ReadAsyncCallback<T>(T packet, uint error)
		{
			this.ReadAsyncCallback<T>(IntPtr.Zero, packet, error);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0007F8B4 File Offset: 0x0007DAB4
		public void ReadAsyncCallback<T>(IntPtr key, T packet, uint error)
		{
			TaskCompletionSource<object> source = this._networkPacketTaskSource;
			if (source == null && this._parser._pMarsPhysicalConObj == this)
			{
				return;
			}
			bool flag = true;
			try
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(error, CallbackType.Read);
				}
				this.ChangeNetworkPacketTimeout(-1, -1);
				this.ProcessSniPacket(packet, error);
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				int num = this.DecrementPendingCallbacks(false);
				if (flag && source != null && num < 2)
				{
					if (error == 0U)
					{
						if (this._executionContext != null)
						{
							ExecutionContext.Run(this._executionContext, delegate(object state)
							{
								source.TrySetResult(null);
							}, null);
						}
						else
						{
							source.TrySetResult(null);
						}
					}
					else if (this._executionContext != null)
					{
						ExecutionContext.Run(this._executionContext, delegate(object state)
						{
							this.ReadAsyncCallbackCaptureException(source);
						}, null);
					}
					else
					{
						this.ReadAsyncCallbackCaptureException(source);
					}
				}
			}
		}

		// Token: 0x06001909 RID: 6409
		protected abstract bool CheckPacket(object packet, TaskCompletionSource<object> source);

		// Token: 0x0600190A RID: 6410 RVA: 0x0007F9C0 File Offset: 0x0007DBC0
		private void ReadAsyncCallbackCaptureException(TaskCompletionSource<object> source)
		{
			bool flag = false;
			try
			{
				if (this._hasErrorOrWarning)
				{
					this.ThrowExceptionAndWarning(false, true);
				}
				else if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
			}
			catch (Exception ex)
			{
				if (source.TrySetException(ex))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Task.Factory.StartNew(delegate
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
					source.TrySetCanceled();
				});
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0007FA58 File Offset: 0x0007DC58
		public void WriteAsyncCallback<T>(T packet, uint sniError)
		{
			this.WriteAsyncCallback<T>(IntPtr.Zero, packet, sniError);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0007FA68 File Offset: 0x0007DC68
		public void WriteAsyncCallback<T>(IntPtr key, T packet, uint sniError)
		{
			this.RemovePacketFromPendingList(packet);
			try
			{
				if (sniError != 0U)
				{
					try
					{
						this.AddError(this._parser.ProcessSNIError(this));
						this.ThrowExceptionAndWarning(false, true);
						goto IL_009E;
					}
					catch (Exception ex)
					{
						TaskCompletionSource<object> taskCompletionSource = this._writeCompletionSource;
						if (taskCompletionSource != null)
						{
							taskCompletionSource.TrySetException(ex);
						}
						else
						{
							this._delayedWriteAsyncCallbackException = ex;
							Interlocked.MemoryBarrier();
							taskCompletionSource = this._writeCompletionSource;
							if (taskCompletionSource != null)
							{
								Exception ex2 = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
								if (ex2 != null)
								{
									taskCompletionSource.TrySetException(ex2);
								}
							}
						}
						return;
					}
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncWriteCount);
			}
			IL_009E:
			TaskCompletionSource<object> writeCompletionSource = this._writeCompletionSource;
			if (this._asyncWriteCount == 0 && writeCompletionSource != null)
			{
				writeCompletionSource.TrySetResult(null);
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0007FB50 File Offset: 0x0007DD50
		internal Task WaitForAccumulatedWrites()
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0)
			{
				return null;
			}
			this._writeCompletionSource = new TaskCompletionSource<object>();
			Task task = this._writeCompletionSource.Task;
			Interlocked.MemoryBarrier();
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
			{
				task = null;
			}
			return task;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0007FBEC File Offset: 0x0007DDEC
		internal void WriteByte(byte b)
		{
			if (this._outBytesUsed == this._outBuff.Length)
			{
				this.WritePacket(0, true);
			}
			byte[] outBuff = this._outBuff;
			int outBytesUsed = this._outBytesUsed;
			this._outBytesUsed = outBytesUsed + 1;
			outBuff[outBytesUsed] = b;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0007FC2C File Offset: 0x0007DE2C
		internal Task WriteByteArray(byte[] b, int len, int offsetBuffer, bool canAccumulate = true, TaskCompletionSource<object> completion = null)
		{
			Task task3;
			try
			{
				bool asyncWrite = this._parser._asyncWrite;
				int num = offsetBuffer;
				while (this._outBytesUsed + len > this._outBuff.Length)
				{
					int num2 = this._outBuff.Length - this._outBytesUsed;
					Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, num2);
					num += num2;
					this._outBytesUsed += num2;
					len -= num2;
					Task task = this.WritePacket(0, canAccumulate);
					if (task != null)
					{
						Task task2 = null;
						if (completion == null)
						{
							completion = new TaskCompletionSource<object>();
							task2 = completion.Task;
						}
						this.WriteByteArraySetupContinuation(b, len, completion, num, task);
						return task2;
					}
					if (len <= 0)
					{
						IL_00B9:
						if (completion != null)
						{
							completion.SetResult(null);
						}
						return null;
					}
				}
				Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, len);
				this._outBytesUsed += len;
				goto IL_00B9;
			}
			catch (Exception ex)
			{
				if (completion == null)
				{
					throw;
				}
				completion.SetException(ex);
				task3 = null;
			}
			return task3;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0007FD2C File Offset: 0x0007DF2C
		private void WriteByteArraySetupContinuation(byte[] b, int len, TaskCompletionSource<object> completion, int offset, Task packetTask)
		{
			AsyncHelper.ContinueTask(packetTask, completion, delegate
			{
				this.WriteByteArray(b, len, offset, false, completion);
			}, this._parser.Connection, null, null, null, null);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0007FD8C File Offset: 0x0007DF8C
		internal Task WritePacket(byte flushMode, bool canAccumulate = false)
		{
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			if ((this._parser.State == TdsParserState.OpenLoggedIn && !this._bulkCopyOpperationInProgress && this._outBytesUsed == this._outputHeaderLen + BitConverter.ToInt32(this._outBuff, this._outputHeaderLen) && this._outputPacketNumber == 1) || (this._outBytesUsed == this._outputHeaderLen && this._outputPacketNumber == 1))
			{
				return null;
			}
			byte outputPacketNumber = this._outputPacketNumber;
			bool flag = this._cancelled && this._parser._asyncWrite;
			byte b;
			if (flag)
			{
				b = 3;
				this._outputPacketNumber = 1;
			}
			else if (1 == flushMode)
			{
				b = 1;
				this._outputPacketNumber = 1;
			}
			else if (flushMode == 0)
			{
				b = 4;
				this._outputPacketNumber += 1;
			}
			else
			{
				b = 1;
			}
			this._outBuff[0] = this._outputMessageType;
			this._outBuff[1] = b;
			this._outBuff[2] = (byte)(this._outBytesUsed >> 8);
			this._outBuff[3] = (byte)(this._outBytesUsed & 255);
			this._outBuff[4] = 0;
			this._outBuff[5] = 0;
			this._outBuff[6] = outputPacketNumber;
			this._outBuff[7] = 0;
			this._parser.CheckResetConnection(this);
			Task task = this.WriteSni(canAccumulate);
			if (flag)
			{
				task = AsyncHelper.CreateContinuationTask(task, new Action(this.CancelWritePacket), this._parser.Connection, null);
			}
			return task;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0007FF00 File Offset: 0x0007E100
		private void CancelWritePacket()
		{
			this._parser.Connection.ThreadHasParserLockForClose = true;
			try
			{
				this.SendAttention(false);
				this.ResetCancelAndProcessAttention();
				throw SQL.OperationCancelled();
			}
			finally
			{
				this._parser.Connection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0007FF54 File Offset: 0x0007E154
		private Task SNIWritePacket(object packet, out uint sniError, bool canAccumulate, bool callerHasConnectionLock)
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			Task task = null;
			this._writeCompletionSource = null;
			object obj = this.EmptyReadPacket;
			bool flag = !this._parser._asyncWrite;
			if (flag && this._asyncWriteCount > 0)
			{
				Task task2 = this.WaitForAccumulatedWrites();
				if (task2 != null)
				{
					try
					{
						task2.Wait();
					}
					catch (AggregateException ex2)
					{
						throw ex2.InnerException;
					}
				}
			}
			if (!flag)
			{
				obj = this.AddPacketToPendingList(packet);
			}
			try
			{
			}
			finally
			{
				sniError = this.WritePacket(packet, flag);
			}
			if (sniError == 997U)
			{
				Interlocked.Increment(ref this._asyncWriteCount);
				if (!canAccumulate)
				{
					this._writeCompletionSource = new TaskCompletionSource<object>();
					task = this._writeCompletionSource.Task;
					Interlocked.MemoryBarrier();
					ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
					if (ex != null)
					{
						throw ex;
					}
					if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
					{
						task = null;
					}
				}
			}
			else
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(sniError, CallbackType.Write);
				}
				if (sniError == 0U)
				{
					this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
					if (!flag)
					{
						this.RemovePacketFromPendingList(obj);
					}
				}
				else
				{
					this.AddError(this._parser.ProcessSNIError(this));
					this.ThrowExceptionAndWarning(callerHasConnectionLock, false);
				}
			}
			return task;
		}

		// Token: 0x06001914 RID: 6420
		internal abstract bool IsValidPacket(object packetPointer);

		// Token: 0x06001915 RID: 6421
		internal abstract uint WritePacket(object packet, bool sync);

		// Token: 0x06001916 RID: 6422 RVA: 0x000800B8 File Offset: 0x0007E2B8
		internal void SendAttention(bool mustTakeWriteLock = false)
		{
			if (!this._attentionSent)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				object obj = this.CreateAndSetAttentionPacket();
				try
				{
					this._attentionSending = true;
					bool flag = false;
					if (mustTakeWriteLock && !this._parser.Connection.ThreadHasParserLockForClose)
					{
						flag = true;
						this._parser.Connection._parserLock.Wait(false);
						this._parser.Connection.ThreadHasParserLockForClose = true;
					}
					try
					{
						if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
						{
							return;
						}
						this._parser._asyncWrite = false;
						uint num;
						this.SNIWritePacket(obj, out num, false, false);
					}
					finally
					{
						if (flag)
						{
							this._parser.Connection.ThreadHasParserLockForClose = false;
							this._parser.Connection._parserLock.Release();
						}
					}
					this.SetTimeoutSeconds(5);
					this._attentionSent = true;
				}
				finally
				{
					this._attentionSending = false;
				}
			}
		}

		// Token: 0x06001917 RID: 6423
		internal abstract object CreateAndSetAttentionPacket();

		// Token: 0x06001918 RID: 6424
		internal abstract void SetPacketData(object packet, byte[] buffer, int bytesUsed);

		// Token: 0x06001919 RID: 6425 RVA: 0x000801D8 File Offset: 0x0007E3D8
		private Task WriteSni(bool canAccumulate)
		{
			object resetWritePacket = this.GetResetWritePacket();
			this.SetPacketData(resetWritePacket, this._outBuff, this._outBytesUsed);
			uint num;
			Task task = this.SNIWritePacket(resetWritePacket, out num, canAccumulate, true);
			if (this._bulkCopyOpperationInProgress && this.GetTimeoutRemaining() == 0)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
					this._bulkCopyWriteTimeout = true;
					this.SendAttention(false);
					this._parser.ProcessPendingAck(this);
					this.ThrowExceptionAndWarning(false, false);
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
			if (this._parser.State == TdsParserState.OpenNotLoggedIn && this._parser.EncryptionOptions == EncryptionOptions.LOGIN)
			{
				this._parser.RemoveEncryption();
				this._parser.EncryptionOptions = EncryptionOptions.OFF;
				this.ClearAllWritePackets();
			}
			this.SniWriteStatisticsAndTracing();
			this.ResetBuffer();
			return task;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000802FC File Offset: 0x0007E4FC
		private void SniReadStatisticsAndTracing()
		{
			SqlStatistics statistics = this.Parser.Statistics;
			if (statistics != null)
			{
				if (statistics.WaitForReply)
				{
					statistics.SafeIncrement(ref statistics._serverRoundtrips);
					statistics.ReleaseAndUpdateNetworkServerTimer();
				}
				statistics.SafeAdd(ref statistics._bytesReceived, (long)this._inBytesRead);
				statistics.SafeIncrement(ref statistics._buffersReceived);
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00080354 File Offset: 0x0007E554
		private void SniWriteStatisticsAndTracing()
		{
			SqlStatistics statistics = this._parser.Statistics;
			if (statistics != null)
			{
				statistics.SafeIncrement(ref statistics._buffersSent);
				statistics.SafeAdd(ref statistics._bytesSent, (long)this._outBytesUsed);
				statistics.RequestNetworkServerTimer();
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00080398 File Offset: 0x0007E598
		[Conditional("DEBUG")]
		private void AssertValidState()
		{
			if (this._inBytesUsed < 0 || this._inBytesRead < 0)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "either _inBytesUsed or _inBytesRead is negative: {0}, {1}", this._inBytesUsed, this._inBytesRead);
			}
			else if (this._inBytesUsed > this._inBytesRead)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "_inBytesUsed > _inBytesRead: {0} > {1}", this._inBytesUsed, this._inBytesRead);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x00080417 File Offset: 0x0007E617
		internal bool HasErrorOrWarning
		{
			get
			{
				return this._hasErrorOrWarning;
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00080420 File Offset: 0x0007E620
		internal void AddError(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._errors == null)
				{
					this._errors = new SqlErrorCollection();
				}
				this._errors.Add(error);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00080488 File Offset: 0x0007E688
		internal int ErrorCount
		{
			get
			{
				int num = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._errors != null)
					{
						num = this._errors.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000804DC File Offset: 0x0007E6DC
		internal void AddWarning(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._warnings == null)
				{
					this._warnings = new SqlErrorCollection();
				}
				this._warnings.Add(error);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x00080544 File Offset: 0x0007E744
		internal int WarningCount
		{
			get
			{
				int num = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._warnings != null)
					{
						num = this._warnings.Count;
					}
				}
				return num;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001922 RID: 6434
		protected abstract object EmptyReadPacket { get; }

		// Token: 0x06001923 RID: 6435 RVA: 0x00080598 File Offset: 0x0007E798
		internal SqlErrorCollection GetFullErrorAndWarningCollection(out bool broken)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			broken = false;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this.AddErrorsToCollection(this._errors, ref sqlErrorCollection, ref broken);
				this.AddErrorsToCollection(this._warnings, ref sqlErrorCollection, ref broken);
				this._errors = null;
				this._warnings = null;
				this.AddErrorsToCollection(this._preAttentionErrors, ref sqlErrorCollection, ref broken);
				this.AddErrorsToCollection(this._preAttentionWarnings, ref sqlErrorCollection, ref broken);
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
			return sqlErrorCollection;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0008063C File Offset: 0x0007E83C
		private void AddErrorsToCollection(SqlErrorCollection inCollection, ref SqlErrorCollection collectionToAddTo, ref bool broken)
		{
			if (inCollection != null)
			{
				foreach (object obj in inCollection)
				{
					SqlError sqlError = (SqlError)obj;
					collectionToAddTo.Add(sqlError);
					broken |= sqlError.Class >= 20;
				}
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000806A8 File Offset: 0x0007E8A8
		internal void StoreErrorAndWarningForAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this._preAttentionErrors = this._errors;
				this._preAttentionWarnings = this._warnings;
				this._errors = null;
				this._warnings = null;
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00080710 File Offset: 0x0007E910
		internal void RestoreErrorAndWarningAfterAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = (this._preAttentionErrors != null && this._preAttentionErrors.Count > 0) || (this._preAttentionWarnings != null && this._preAttentionWarnings.Count > 0);
				this._errors = this._preAttentionErrors;
				this._warnings = this._preAttentionWarnings;
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000807A8 File Offset: 0x0007E9A8
		internal void CheckThrowSNIException()
		{
			if (this.HasErrorOrWarning)
			{
				this.ThrowExceptionAndWarning(false, false);
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000807BC File Offset: 0x0007E9BC
		[Conditional("DEBUG")]
		internal void AssertStateIsClean()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State != TdsParserState.Closed)
			{
				TdsParserState state = parser.State;
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000807E4 File Offset: 0x0007E9E4
		internal void CloneCleanupAltMetaDataSetArray()
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneCleanupAltMetaDataSetArray();
			}
		}

		// Token: 0x040011C1 RID: 4545
		private const int AttentionTimeoutSeconds = 5;

		// Token: 0x040011C2 RID: 4546
		private const long CheckConnectionWindow = 50000L;

		// Token: 0x040011C3 RID: 4547
		protected readonly TdsParser _parser;

		// Token: 0x040011C4 RID: 4548
		private readonly WeakReference _owner = new WeakReference(null);

		// Token: 0x040011C5 RID: 4549
		internal SqlDataReader.SharedState _readerState;

		// Token: 0x040011C6 RID: 4550
		private int _activateCount;

		// Token: 0x040011C7 RID: 4551
		internal readonly int _inputHeaderLen = 8;

		// Token: 0x040011C8 RID: 4552
		internal readonly int _outputHeaderLen = 8;

		// Token: 0x040011C9 RID: 4553
		internal byte[] _outBuff;

		// Token: 0x040011CA RID: 4554
		internal int _outBytesUsed = 8;

		// Token: 0x040011CB RID: 4555
		protected byte[] _inBuff;

		// Token: 0x040011CC RID: 4556
		internal int _inBytesUsed;

		// Token: 0x040011CD RID: 4557
		internal int _inBytesRead;

		// Token: 0x040011CE RID: 4558
		internal int _inBytesPacket;

		// Token: 0x040011CF RID: 4559
		internal byte _outputMessageType;

		// Token: 0x040011D0 RID: 4560
		internal byte _messageStatus;

		// Token: 0x040011D1 RID: 4561
		internal byte _outputPacketNumber = 1;

		// Token: 0x040011D2 RID: 4562
		internal bool _pendingData;

		// Token: 0x040011D3 RID: 4563
		internal volatile bool _fResetEventOwned;

		// Token: 0x040011D4 RID: 4564
		internal volatile bool _fResetConnectionSent;

		// Token: 0x040011D5 RID: 4565
		internal bool _errorTokenReceived;

		// Token: 0x040011D6 RID: 4566
		internal bool _bulkCopyOpperationInProgress;

		// Token: 0x040011D7 RID: 4567
		internal bool _bulkCopyWriteTimeout;

		// Token: 0x040011D8 RID: 4568
		protected readonly object _writePacketLockObject = new object();

		// Token: 0x040011D9 RID: 4569
		private int _pendingCallbacks;

		// Token: 0x040011DA RID: 4570
		private long _timeoutMilliseconds;

		// Token: 0x040011DB RID: 4571
		private long _timeoutTime;

		// Token: 0x040011DC RID: 4572
		internal volatile bool _attentionSent;

		// Token: 0x040011DD RID: 4573
		internal bool _attentionReceived;

		// Token: 0x040011DE RID: 4574
		internal volatile bool _attentionSending;

		// Token: 0x040011DF RID: 4575
		internal bool _internalTimeout;

		// Token: 0x040011E0 RID: 4576
		private readonly LastIOTimer _lastSuccessfulIOTimer;

		// Token: 0x040011E1 RID: 4577
		private bool _cancelled;

		// Token: 0x040011E2 RID: 4578
		private const int _waitForCancellationLockPollTimeout = 100;

		// Token: 0x040011E3 RID: 4579
		private WeakReference _cancellationOwner = new WeakReference(null);

		// Token: 0x040011E4 RID: 4580
		internal bool _hasOpenResult;

		// Token: 0x040011E5 RID: 4581
		internal SqlInternalTransaction _executedUnderTransaction;

		// Token: 0x040011E6 RID: 4582
		internal ulong _longlen;

		// Token: 0x040011E7 RID: 4583
		internal ulong _longlenleft;

		// Token: 0x040011E8 RID: 4584
		internal int[] _decimalBits;

		// Token: 0x040011E9 RID: 4585
		internal byte[] _bTmp = new byte[12];

		// Token: 0x040011EA RID: 4586
		internal int _bTmpRead;

		// Token: 0x040011EB RID: 4587
		internal Decoder _plpdecoder;

		// Token: 0x040011EC RID: 4588
		internal bool _accumulateInfoEvents;

		// Token: 0x040011ED RID: 4589
		internal List<SqlError> _pendingInfoEvents;

		// Token: 0x040011EE RID: 4590
		private byte[] _partialHeaderBuffer = new byte[8];

		// Token: 0x040011EF RID: 4591
		internal int _partialHeaderBytesRead;

		// Token: 0x040011F0 RID: 4592
		internal _SqlMetaDataSet _cleanupMetaData;

		// Token: 0x040011F1 RID: 4593
		internal _SqlMetaDataSetCollection _cleanupAltMetaDataSetArray;

		// Token: 0x040011F2 RID: 4594
		internal bool _receivedColMetaData;

		// Token: 0x040011F3 RID: 4595
		private SniContext _sniContext;

		// Token: 0x040011F4 RID: 4596
		private bool _bcpLock;

		// Token: 0x040011F5 RID: 4597
		private TdsParserStateObject.NullBitmap _nullBitmapInfo;

		// Token: 0x040011F6 RID: 4598
		internal TaskCompletionSource<object> _networkPacketTaskSource;

		// Token: 0x040011F7 RID: 4599
		private Timer _networkPacketTimeout;

		// Token: 0x040011F8 RID: 4600
		internal bool _syncOverAsync = true;

		// Token: 0x040011F9 RID: 4601
		private bool _snapshotReplay;

		// Token: 0x040011FA RID: 4602
		private TdsParserStateObject.StateSnapshot _snapshot;

		// Token: 0x040011FB RID: 4603
		internal ExecutionContext _executionContext;

		// Token: 0x040011FC RID: 4604
		internal bool _asyncReadWithoutSnapshot;

		// Token: 0x040011FD RID: 4605
		internal SqlErrorCollection _errors;

		// Token: 0x040011FE RID: 4606
		internal SqlErrorCollection _warnings;

		// Token: 0x040011FF RID: 4607
		internal object _errorAndWarningsLock = new object();

		// Token: 0x04001200 RID: 4608
		private bool _hasErrorOrWarning;

		// Token: 0x04001201 RID: 4609
		internal SqlErrorCollection _preAttentionErrors;

		// Token: 0x04001202 RID: 4610
		internal SqlErrorCollection _preAttentionWarnings;

		// Token: 0x04001203 RID: 4611
		private volatile TaskCompletionSource<object> _writeCompletionSource;

		// Token: 0x04001204 RID: 4612
		protected volatile int _asyncWriteCount;

		// Token: 0x04001205 RID: 4613
		private volatile Exception _delayedWriteAsyncCallbackException;

		// Token: 0x04001206 RID: 4614
		private int _readingCount;

		// Token: 0x02000228 RID: 552
		private struct NullBitmap
		{
			// Token: 0x0600192C RID: 6444 RVA: 0x0008081C File Offset: 0x0007EA1C
			internal bool TryInitialize(TdsParserStateObject stateObj, int columnsCount)
			{
				this._columnsCount = columnsCount;
				int num = (columnsCount + 7) / 8;
				if (this._nullBitmap == null || this._nullBitmap.Length != num)
				{
					this._nullBitmap = new byte[num];
				}
				return stateObj.TryReadByteArray(this._nullBitmap, 0, this._nullBitmap.Length);
			}

			// Token: 0x0600192D RID: 6445 RVA: 0x0008086F File Offset: 0x0007EA6F
			internal bool ReferenceEquals(TdsParserStateObject.NullBitmap obj)
			{
				return this._nullBitmap == obj._nullBitmap;
			}

			// Token: 0x0600192E RID: 6446 RVA: 0x00080880 File Offset: 0x0007EA80
			internal TdsParserStateObject.NullBitmap Clone()
			{
				return new TdsParserStateObject.NullBitmap
				{
					_nullBitmap = ((this._nullBitmap == null) ? null : ((byte[])this._nullBitmap.Clone())),
					_columnsCount = this._columnsCount
				};
			}

			// Token: 0x0600192F RID: 6447 RVA: 0x000808C5 File Offset: 0x0007EAC5
			internal void Clean()
			{
				this._columnsCount = 0;
			}

			// Token: 0x06001930 RID: 6448 RVA: 0x000808D0 File Offset: 0x0007EAD0
			internal bool IsGuaranteedNull(int columnOrdinal)
			{
				if (this._columnsCount == 0)
				{
					return false;
				}
				byte b = (byte)(1 << (columnOrdinal & 7));
				byte b2 = this._nullBitmap[columnOrdinal >> 3];
				return (b & b2) > 0;
			}

			// Token: 0x04001207 RID: 4615
			private byte[] _nullBitmap;

			// Token: 0x04001208 RID: 4616
			private int _columnsCount;
		}

		// Token: 0x02000229 RID: 553
		private class PacketData
		{
			// Token: 0x04001209 RID: 4617
			public byte[] Buffer;

			// Token: 0x0400120A RID: 4618
			public int Read;
		}

		// Token: 0x0200022A RID: 554
		private class StateSnapshot
		{
			// Token: 0x06001932 RID: 6450 RVA: 0x00080900 File Offset: 0x0007EB00
			public StateSnapshot(TdsParserStateObject state)
			{
				this._snapshotInBuffs = new List<TdsParserStateObject.PacketData>();
				this._stateObj = state;
			}

			// Token: 0x06001933 RID: 6451 RVA: 0x0008091A File Offset: 0x0007EB1A
			internal void CloneNullBitmapInfo()
			{
				if (this._stateObj._nullBitmapInfo.ReferenceEquals(this._snapshotNullBitmapInfo))
				{
					this._stateObj._nullBitmapInfo = this._stateObj._nullBitmapInfo.Clone();
				}
			}

			// Token: 0x06001934 RID: 6452 RVA: 0x00080950 File Offset: 0x0007EB50
			internal void CloneCleanupAltMetaDataSetArray()
			{
				if (this._stateObj._cleanupAltMetaDataSetArray != null && this._snapshotCleanupAltMetaDataSetArray == this._stateObj._cleanupAltMetaDataSetArray)
				{
					this._stateObj._cleanupAltMetaDataSetArray = (_SqlMetaDataSetCollection)this._stateObj._cleanupAltMetaDataSetArray.Clone();
				}
			}

			// Token: 0x06001935 RID: 6453 RVA: 0x000809A0 File Offset: 0x0007EBA0
			internal void PushBuffer(byte[] buffer, int read)
			{
				TdsParserStateObject.PacketData packetData = new TdsParserStateObject.PacketData();
				packetData.Buffer = buffer;
				packetData.Read = read;
				this._snapshotInBuffs.Add(packetData);
			}

			// Token: 0x06001936 RID: 6454 RVA: 0x000809D0 File Offset: 0x0007EBD0
			internal bool Replay()
			{
				if (this._snapshotInBuffCurrent < this._snapshotInBuffs.Count)
				{
					TdsParserStateObject.PacketData packetData = this._snapshotInBuffs[this._snapshotInBuffCurrent];
					this._stateObj._inBuff = packetData.Buffer;
					this._stateObj._inBytesUsed = 0;
					this._stateObj._inBytesRead = packetData.Read;
					this._snapshotInBuffCurrent++;
					return true;
				}
				return false;
			}

			// Token: 0x06001937 RID: 6455 RVA: 0x00080A44 File Offset: 0x0007EC44
			internal void Snap()
			{
				this._snapshotInBuffs.Clear();
				this._snapshotInBuffCurrent = 0;
				this._snapshotInBytesUsed = this._stateObj._inBytesUsed;
				this._snapshotInBytesPacket = this._stateObj._inBytesPacket;
				this._snapshotPendingData = this._stateObj._pendingData;
				this._snapshotErrorTokenReceived = this._stateObj._errorTokenReceived;
				this._snapshotMessageStatus = this._stateObj._messageStatus;
				this._snapshotNullBitmapInfo = this._stateObj._nullBitmapInfo;
				this._snapshotLongLen = this._stateObj._longlen;
				this._snapshotLongLenLeft = this._stateObj._longlenleft;
				this._snapshotCleanupMetaData = this._stateObj._cleanupMetaData;
				this._snapshotCleanupAltMetaDataSetArray = this._stateObj._cleanupAltMetaDataSetArray;
				this._snapshotHasOpenResult = this._stateObj._hasOpenResult;
				this._snapshotReceivedColumnMetadata = this._stateObj._receivedColMetaData;
				this._snapshotAttentionReceived = this._stateObj._attentionReceived;
				this.PushBuffer(this._stateObj._inBuff, this._stateObj._inBytesRead);
			}

			// Token: 0x06001938 RID: 6456 RVA: 0x00080B5C File Offset: 0x0007ED5C
			internal void ResetSnapshotState()
			{
				this._snapshotInBuffCurrent = 0;
				this.Replay();
				this._stateObj._inBytesUsed = this._snapshotInBytesUsed;
				this._stateObj._inBytesPacket = this._snapshotInBytesPacket;
				this._stateObj._pendingData = this._snapshotPendingData;
				this._stateObj._errorTokenReceived = this._snapshotErrorTokenReceived;
				this._stateObj._messageStatus = this._snapshotMessageStatus;
				this._stateObj._nullBitmapInfo = this._snapshotNullBitmapInfo;
				this._stateObj._cleanupMetaData = this._snapshotCleanupMetaData;
				this._stateObj._cleanupAltMetaDataSetArray = this._snapshotCleanupAltMetaDataSetArray;
				this._stateObj._hasOpenResult = this._snapshotHasOpenResult;
				this._stateObj._receivedColMetaData = this._snapshotReceivedColumnMetadata;
				this._stateObj._attentionReceived = this._snapshotAttentionReceived;
				this._stateObj._bTmpRead = 0;
				this._stateObj._partialHeaderBytesRead = 0;
				this._stateObj._longlen = this._snapshotLongLen;
				this._stateObj._longlenleft = this._snapshotLongLenLeft;
				this._stateObj._snapshotReplay = true;
			}

			// Token: 0x06001939 RID: 6457 RVA: 0x00080C78 File Offset: 0x0007EE78
			internal void PrepareReplay()
			{
				this.ResetSnapshotState();
			}

			// Token: 0x0400120B RID: 4619
			private List<TdsParserStateObject.PacketData> _snapshotInBuffs;

			// Token: 0x0400120C RID: 4620
			private int _snapshotInBuffCurrent;

			// Token: 0x0400120D RID: 4621
			private int _snapshotInBytesUsed;

			// Token: 0x0400120E RID: 4622
			private int _snapshotInBytesPacket;

			// Token: 0x0400120F RID: 4623
			private bool _snapshotPendingData;

			// Token: 0x04001210 RID: 4624
			private bool _snapshotErrorTokenReceived;

			// Token: 0x04001211 RID: 4625
			private bool _snapshotHasOpenResult;

			// Token: 0x04001212 RID: 4626
			private bool _snapshotReceivedColumnMetadata;

			// Token: 0x04001213 RID: 4627
			private bool _snapshotAttentionReceived;

			// Token: 0x04001214 RID: 4628
			private byte _snapshotMessageStatus;

			// Token: 0x04001215 RID: 4629
			private TdsParserStateObject.NullBitmap _snapshotNullBitmapInfo;

			// Token: 0x04001216 RID: 4630
			private ulong _snapshotLongLen;

			// Token: 0x04001217 RID: 4631
			private ulong _snapshotLongLenLeft;

			// Token: 0x04001218 RID: 4632
			private _SqlMetaDataSet _snapshotCleanupMetaData;

			// Token: 0x04001219 RID: 4633
			private _SqlMetaDataSetCollection _snapshotCleanupAltMetaDataSetArray;

			// Token: 0x0400121A RID: 4634
			private readonly TdsParserStateObject _stateObj;
		}
	}
}
