using System;
using System.IO;
using System.Threading;
using Mono.Security.Protocol.Tls.Handshake;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200003F RID: 63
	internal abstract class RecordProtocol
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000F1F7 File Offset: 0x0000D3F7
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000F1FF File Offset: 0x0000D3FF
		public Context Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F208 File Offset: 0x0000D408
		public RecordProtocol(Stream innerStream, Context context)
		{
			this.innerStream = innerStream;
			this.context = context;
			this.context.RecordProtocol = this;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F22C File Offset: 0x0000D42C
		public virtual void SendRecord(HandshakeType type)
		{
			IAsyncResult asyncResult = this.BeginSendRecord(type, null, null);
			this.EndSendRecord(asyncResult);
		}

		// Token: 0x06000295 RID: 661
		protected abstract void ProcessHandshakeMessage(TlsStream handMsg);

		// Token: 0x06000296 RID: 662 RVA: 0x0000F24C File Offset: 0x0000D44C
		protected virtual void ProcessChangeCipherSpec()
		{
			Context context = this.Context;
			context.ReadSequenceNumber = 0UL;
			if (context is ClientContext)
			{
				context.EndSwitchingSecurityParameters(true);
			}
			else
			{
				context.StartSwitchingSecurityParameters(false);
			}
			context.ChangeCipherSpecDone = true;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000F287 File Offset: 0x0000D487
		public virtual HandshakeMessage GetMessage(HandshakeType type)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000F290 File Offset: 0x0000D490
		public IAsyncResult BeginReceiveRecord(Stream record, AsyncCallback callback, object state)
		{
			if (this.context.ReceivedConnectionEnd)
			{
				throw new TlsException(AlertDescription.InternalError, "The session is finished and it's no longer valid.");
			}
			RecordProtocol.record_processing.Reset();
			byte[] array = new byte[1];
			RecordProtocol.ReceiveRecordAsyncResult receiveRecordAsyncResult = new RecordProtocol.ReceiveRecordAsyncResult(callback, state, array, record);
			record.BeginRead(receiveRecordAsyncResult.InitialBuffer, 0, receiveRecordAsyncResult.InitialBuffer.Length, new AsyncCallback(this.InternalReceiveRecordCallback), receiveRecordAsyncResult);
			return receiveRecordAsyncResult;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		private void InternalReceiveRecordCallback(IAsyncResult asyncResult)
		{
			RecordProtocol.ReceiveRecordAsyncResult receiveRecordAsyncResult = asyncResult.AsyncState as RecordProtocol.ReceiveRecordAsyncResult;
			Stream record = receiveRecordAsyncResult.Record;
			try
			{
				if (receiveRecordAsyncResult.Record.EndRead(asyncResult) == 0)
				{
					receiveRecordAsyncResult.SetComplete(null);
				}
				else
				{
					int num = (int)receiveRecordAsyncResult.InitialBuffer[0];
					ContentType contentType = (ContentType)num;
					byte[] array = this.ReadRecordBuffer(num, record);
					if (array == null)
					{
						receiveRecordAsyncResult.SetComplete(null);
					}
					else
					{
						if ((contentType != ContentType.Alert || array.Length != 2) && this.Context.Read != null && this.Context.Read.Cipher != null)
						{
							array = this.decryptRecordFragment(contentType, array);
						}
						switch (contentType)
						{
						case ContentType.ChangeCipherSpec:
							this.ProcessChangeCipherSpec();
							break;
						case ContentType.Alert:
							this.ProcessAlert((AlertLevel)array[0], (AlertDescription)array[1]);
							if (record.CanSeek)
							{
								record.SetLength(0L);
							}
							array = null;
							break;
						case ContentType.Handshake:
						{
							TlsStream tlsStream = new TlsStream(array);
							while (!tlsStream.EOF)
							{
								this.ProcessHandshakeMessage(tlsStream);
							}
							break;
						}
						case ContentType.ApplicationData:
							break;
						default:
							if (contentType != (ContentType)128)
							{
								throw new TlsException(AlertDescription.UnexpectedMessage, "Unknown record received from server.");
							}
							this.context.HandshakeMessages.Write(array);
							break;
						}
						receiveRecordAsyncResult.SetComplete(array);
					}
				}
			}
			catch (Exception ex)
			{
				receiveRecordAsyncResult.SetComplete(ex);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000F450 File Offset: 0x0000D650
		public byte[] EndReceiveRecord(IAsyncResult asyncResult)
		{
			RecordProtocol.ReceiveRecordAsyncResult receiveRecordAsyncResult = asyncResult as RecordProtocol.ReceiveRecordAsyncResult;
			if (receiveRecordAsyncResult == null)
			{
				throw new ArgumentException("Either the provided async result is null or was not created by this RecordProtocol.");
			}
			if (!receiveRecordAsyncResult.IsCompleted)
			{
				receiveRecordAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (receiveRecordAsyncResult.CompletedWithError)
			{
				throw receiveRecordAsyncResult.AsyncException;
			}
			byte[] resultingBuffer = receiveRecordAsyncResult.ResultingBuffer;
			RecordProtocol.record_processing.Set();
			return resultingBuffer;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		public byte[] ReceiveRecord(Stream record)
		{
			if (this.context.ReceivedConnectionEnd)
			{
				throw new TlsException(AlertDescription.InternalError, "The session is finished and it's no longer valid.");
			}
			RecordProtocol.record_processing.Reset();
			byte[] array = new byte[1];
			if (record.Read(array, 0, array.Length) == 0)
			{
				return null;
			}
			int num = (int)array[0];
			ContentType contentType = (ContentType)num;
			byte[] array2 = this.ReadRecordBuffer(num, record);
			if (array2 == null)
			{
				return null;
			}
			if ((contentType != ContentType.Alert || array2.Length != 2) && this.Context.Read != null && this.Context.Read.Cipher != null)
			{
				array2 = this.decryptRecordFragment(contentType, array2);
			}
			switch (contentType)
			{
			case ContentType.ChangeCipherSpec:
				this.ProcessChangeCipherSpec();
				break;
			case ContentType.Alert:
				this.ProcessAlert((AlertLevel)array2[0], (AlertDescription)array2[1]);
				if (record.CanSeek)
				{
					record.SetLength(0L);
				}
				array2 = null;
				break;
			case ContentType.Handshake:
			{
				TlsStream tlsStream = new TlsStream(array2);
				while (!tlsStream.EOF)
				{
					this.ProcessHandshakeMessage(tlsStream);
				}
				break;
			}
			case ContentType.ApplicationData:
				break;
			default:
				if (contentType != (ContentType)128)
				{
					throw new TlsException(AlertDescription.UnexpectedMessage, "Unknown record received from server.");
				}
				this.context.HandshakeMessages.Write(array2);
				break;
			}
			RecordProtocol.record_processing.Set();
			return array2;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		private byte[] ReadRecordBuffer(int contentType, Stream record)
		{
			if (!Enum.IsDefined(typeof(ContentType), (ContentType)contentType))
			{
				throw new TlsException(AlertDescription.DecodeError);
			}
			byte[] array = new byte[4];
			if (record.Read(array, 0, 4) != 4)
			{
				throw new TlsException("buffer underrun");
			}
			short num = (short)(((int)array[0] << 8) | (int)array[1]);
			short num2 = (short)(((int)array[2] << 8) | (int)array[3]);
			if (record.CanSeek && (long)(num2 + 5) > record.Length)
			{
				return null;
			}
			int num3 = 0;
			byte[] array2 = new byte[(int)num2];
			while (num3 != (int)num2)
			{
				int num4 = record.Read(array2, num3, array2.Length - num3);
				if (num4 == 0)
				{
					throw new TlsException(AlertDescription.CloseNotify, "Received 0 bytes from stream. It must be closed.");
				}
				num3 += num4;
			}
			if (num != this.context.Protocol && this.context.ProtocolNegotiated)
			{
				throw new TlsException(AlertDescription.ProtocolVersion, "Invalid protocol version on message received");
			}
			return array2;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000F6A3 File Offset: 0x0000D8A3
		private void ProcessAlert(AlertLevel alertLevel, AlertDescription alertDesc)
		{
			if (alertLevel != AlertLevel.Warning && alertLevel == AlertLevel.Fatal)
			{
				throw new TlsException(alertLevel, alertDesc);
			}
			if (alertDesc == AlertDescription.CloseNotify)
			{
				this.context.ReceivedConnectionEnd = true;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		internal void SendAlert(ref Exception ex)
		{
			TlsException ex2 = ex as TlsException;
			Alert alert = ((ex2 != null) ? ex2.Alert : new Alert(AlertDescription.InternalError));
			try
			{
				this.SendAlert(alert);
			}
			catch (Exception ex3)
			{
				ex = new IOException(string.Format("Error while sending TLS Alert ({0}:{1}): {2}", alert.Level, alert.Description, ex), ex3);
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000F734 File Offset: 0x0000D934
		public void SendAlert(AlertDescription description)
		{
			this.SendAlert(new Alert(description));
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000F742 File Offset: 0x0000D942
		public void SendAlert(AlertLevel level, AlertDescription description)
		{
			this.SendAlert(new Alert(level, description));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000F754 File Offset: 0x0000D954
		public void SendAlert(Alert alert)
		{
			AlertLevel alertLevel;
			AlertDescription alertDescription;
			bool flag;
			if (alert == null)
			{
				alertLevel = AlertLevel.Fatal;
				alertDescription = AlertDescription.InternalError;
				flag = true;
			}
			else
			{
				alertLevel = alert.Level;
				alertDescription = alert.Description;
				flag = alert.IsCloseNotify;
			}
			this.SendRecord(ContentType.Alert, new byte[]
			{
				(byte)alertLevel,
				(byte)alertDescription
			});
			if (flag)
			{
				this.context.SentConnectionEnd = true;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public void SendChangeCipherSpec()
		{
			this.SendRecord(ContentType.ChangeCipherSpec, new byte[] { 1 });
			Context context = this.context;
			context.WriteSequenceNumber = 0UL;
			if (context is ClientContext)
			{
				context.StartSwitchingSecurityParameters(true);
				return;
			}
			context.EndSwitchingSecurityParameters(false);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		public void SendChangeCipherSpec(Stream recordStream)
		{
			byte[] array = this.EncodeRecord(ContentType.ChangeCipherSpec, new byte[] { 1 });
			recordStream.Write(array, 0, array.Length);
			Context context = this.context;
			context.WriteSequenceNumber = 0UL;
			if (context is ClientContext)
			{
				context.StartSwitchingSecurityParameters(true);
				return;
			}
			context.EndSwitchingSecurityParameters(false);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000F841 File Offset: 0x0000DA41
		public IAsyncResult BeginSendChangeCipherSpec(AsyncCallback callback, object state)
		{
			return this.BeginSendRecord(ContentType.ChangeCipherSpec, new byte[] { 1 }, callback, state);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F858 File Offset: 0x0000DA58
		public void EndSendChangeCipherSpec(IAsyncResult asyncResult)
		{
			this.EndSendRecord(asyncResult);
			Context context = this.context;
			context.WriteSequenceNumber = 0UL;
			if (context is ClientContext)
			{
				context.StartSwitchingSecurityParameters(true);
				return;
			}
			context.EndSwitchingSecurityParameters(false);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000F894 File Offset: 0x0000DA94
		public IAsyncResult BeginSendRecord(HandshakeType handshakeType, AsyncCallback callback, object state)
		{
			HandshakeMessage message = this.GetMessage(handshakeType);
			message.Process();
			RecordProtocol.SendRecordAsyncResult sendRecordAsyncResult = new RecordProtocol.SendRecordAsyncResult(callback, state, message);
			this.BeginSendRecord(message.ContentType, message.EncodeMessage(), new AsyncCallback(this.InternalSendRecordCallback), sendRecordAsyncResult);
			return sendRecordAsyncResult;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000F8DC File Offset: 0x0000DADC
		private void InternalSendRecordCallback(IAsyncResult ar)
		{
			RecordProtocol.SendRecordAsyncResult sendRecordAsyncResult = ar.AsyncState as RecordProtocol.SendRecordAsyncResult;
			try
			{
				this.EndSendRecord(ar);
				sendRecordAsyncResult.Message.Update();
				sendRecordAsyncResult.Message.Reset();
				sendRecordAsyncResult.SetComplete();
			}
			catch (Exception ex)
			{
				sendRecordAsyncResult.SetComplete(ex);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000F934 File Offset: 0x0000DB34
		public IAsyncResult BeginSendRecord(ContentType contentType, byte[] recordData, AsyncCallback callback, object state)
		{
			if (this.context.SentConnectionEnd)
			{
				throw new TlsException(AlertDescription.InternalError, "The session is finished and it's no longer valid.");
			}
			byte[] array = this.EncodeRecord(contentType, recordData);
			return this.innerStream.BeginWrite(array, 0, array.Length, callback, state);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000F978 File Offset: 0x0000DB78
		public void EndSendRecord(IAsyncResult asyncResult)
		{
			if (asyncResult is RecordProtocol.SendRecordAsyncResult)
			{
				RecordProtocol.SendRecordAsyncResult sendRecordAsyncResult = asyncResult as RecordProtocol.SendRecordAsyncResult;
				if (!sendRecordAsyncResult.IsCompleted)
				{
					sendRecordAsyncResult.AsyncWaitHandle.WaitOne();
				}
				if (sendRecordAsyncResult.CompletedWithError)
				{
					throw sendRecordAsyncResult.AsyncException;
				}
			}
			else
			{
				this.innerStream.EndWrite(asyncResult);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		public void SendRecord(ContentType contentType, byte[] recordData)
		{
			IAsyncResult asyncResult = this.BeginSendRecord(contentType, recordData, null, null);
			this.EndSendRecord(asyncResult);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000F9E3 File Offset: 0x0000DBE3
		public byte[] EncodeRecord(ContentType contentType, byte[] recordData)
		{
			return this.EncodeRecord(contentType, recordData, 0, recordData.Length);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public byte[] EncodeRecord(ContentType contentType, byte[] recordData, int offset, int count)
		{
			if (this.context.SentConnectionEnd)
			{
				throw new TlsException(AlertDescription.InternalError, "The session is finished and it's no longer valid.");
			}
			TlsStream tlsStream = new TlsStream();
			short num;
			for (int i = offset; i < offset + count; i += (int)num)
			{
				if (count + offset - i > 16384)
				{
					num = 16384;
				}
				else
				{
					num = (short)(count + offset - i);
				}
				byte[] array = new byte[(int)num];
				Buffer.BlockCopy(recordData, i, array, 0, (int)num);
				if (this.Context.Write != null && this.Context.Write.Cipher != null)
				{
					array = this.encryptRecordFragment(contentType, array);
				}
				tlsStream.Write((byte)contentType);
				tlsStream.Write(this.context.Protocol);
				tlsStream.Write((short)array.Length);
				tlsStream.Write(array);
			}
			return tlsStream.ToArray();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
		public byte[] EncodeHandshakeRecord(HandshakeType handshakeType)
		{
			HandshakeMessage message = this.GetMessage(handshakeType);
			message.Process();
			byte[] array = this.EncodeRecord(message.ContentType, message.EncodeMessage());
			message.Update();
			message.Reset();
			return array;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		private byte[] encryptRecordFragment(ContentType contentType, byte[] fragment)
		{
			byte[] array;
			if (this.Context is ClientContext)
			{
				array = this.context.Write.Cipher.ComputeClientRecordMAC(contentType, fragment);
			}
			else
			{
				array = this.context.Write.Cipher.ComputeServerRecordMAC(contentType, fragment);
			}
			byte[] array2 = this.context.Write.Cipher.EncryptRecord(fragment, array);
			Context context = this.context;
			ulong writeSequenceNumber = context.WriteSequenceNumber;
			context.WriteSequenceNumber = writeSequenceNumber + 1UL;
			return array2;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000FB78 File Offset: 0x0000DD78
		private byte[] decryptRecordFragment(ContentType contentType, byte[] fragment)
		{
			byte[] array = null;
			byte[] array2 = null;
			try
			{
				this.context.Read.Cipher.DecryptRecord(fragment, out array, out array2);
			}
			catch
			{
				if (this.context is ServerContext)
				{
					this.Context.RecordProtocol.SendAlert(AlertDescription.DecryptionFailed);
				}
				throw;
			}
			byte[] array3;
			if (this.Context is ClientContext)
			{
				array3 = this.context.Read.Cipher.ComputeServerRecordMAC(contentType, array);
			}
			else
			{
				array3 = this.context.Read.Cipher.ComputeClientRecordMAC(contentType, array);
			}
			if (!this.Compare(array3, array2))
			{
				throw new TlsException(AlertDescription.BadRecordMAC, "Bad record MAC");
			}
			Context context = this.context;
			ulong readSequenceNumber = context.ReadSequenceNumber;
			context.ReadSequenceNumber = readSequenceNumber + 1UL;
			return array;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000FC48 File Offset: 0x0000DE48
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null)
			{
				return array2 == null;
			}
			if (array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400017B RID: 379
		private static ManualResetEvent record_processing = new ManualResetEvent(true);

		// Token: 0x0400017C RID: 380
		protected Stream innerStream;

		// Token: 0x0400017D RID: 381
		protected Context context;

		// Token: 0x020000DE RID: 222
		private class ReceiveRecordAsyncResult : IAsyncResult
		{
			// Token: 0x0600077E RID: 1918 RVA: 0x00021C9F File Offset: 0x0001FE9F
			public ReceiveRecordAsyncResult(AsyncCallback userCallback, object userState, byte[] initialBuffer, Stream record)
			{
				this._userCallback = userCallback;
				this._userState = userState;
				this._initialBuffer = initialBuffer;
				this._record = record;
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x0600077F RID: 1919 RVA: 0x00021CCF File Offset: 0x0001FECF
			public Stream Record
			{
				get
				{
					return this._record;
				}
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x06000780 RID: 1920 RVA: 0x00021CD7 File Offset: 0x0001FED7
			public byte[] ResultingBuffer
			{
				get
				{
					return this._resultingBuffer;
				}
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000781 RID: 1921 RVA: 0x00021CDF File Offset: 0x0001FEDF
			public byte[] InitialBuffer
			{
				get
				{
					return this._initialBuffer;
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000782 RID: 1922 RVA: 0x00021CE7 File Offset: 0x0001FEE7
			public object AsyncState
			{
				get
				{
					return this._userState;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000783 RID: 1923 RVA: 0x00021CEF File Offset: 0x0001FEEF
			public Exception AsyncException
			{
				get
				{
					return this._asyncException;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000784 RID: 1924 RVA: 0x00021CF7 File Offset: 0x0001FEF7
			public bool CompletedWithError
			{
				get
				{
					return this.IsCompleted && this._asyncException != null;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000785 RID: 1925 RVA: 0x00021D0C File Offset: 0x0001FF0C
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					object obj = this.locker;
					lock (obj)
					{
						if (this.handle == null)
						{
							this.handle = new ManualResetEvent(this.completed);
						}
					}
					return this.handle;
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000786 RID: 1926 RVA: 0x00021D68 File Offset: 0x0001FF68
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000787 RID: 1927 RVA: 0x00021D6C File Offset: 0x0001FF6C
			public bool IsCompleted
			{
				get
				{
					object obj = this.locker;
					bool flag2;
					lock (obj)
					{
						flag2 = this.completed;
					}
					return flag2;
				}
			}

			// Token: 0x06000788 RID: 1928 RVA: 0x00021DB0 File Offset: 0x0001FFB0
			private void SetComplete(Exception ex, byte[] resultingBuffer)
			{
				object obj = this.locker;
				lock (obj)
				{
					if (!this.completed)
					{
						this.completed = true;
						this._asyncException = ex;
						this._resultingBuffer = resultingBuffer;
						if (this.handle != null)
						{
							this.handle.Set();
						}
						if (this._userCallback != null)
						{
							this._userCallback.BeginInvoke(this, null, null);
						}
					}
				}
			}

			// Token: 0x06000789 RID: 1929 RVA: 0x00021E34 File Offset: 0x00020034
			public void SetComplete(Exception ex)
			{
				this.SetComplete(ex, null);
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x00021E3E File Offset: 0x0002003E
			public void SetComplete(byte[] resultingBuffer)
			{
				this.SetComplete(null, resultingBuffer);
			}

			// Token: 0x0600078B RID: 1931 RVA: 0x00021E48 File Offset: 0x00020048
			public void SetComplete()
			{
				this.SetComplete(null, null);
			}

			// Token: 0x040004EA RID: 1258
			private object locker = new object();

			// Token: 0x040004EB RID: 1259
			private AsyncCallback _userCallback;

			// Token: 0x040004EC RID: 1260
			private object _userState;

			// Token: 0x040004ED RID: 1261
			private Exception _asyncException;

			// Token: 0x040004EE RID: 1262
			private ManualResetEvent handle;

			// Token: 0x040004EF RID: 1263
			private byte[] _resultingBuffer;

			// Token: 0x040004F0 RID: 1264
			private Stream _record;

			// Token: 0x040004F1 RID: 1265
			private bool completed;

			// Token: 0x040004F2 RID: 1266
			private byte[] _initialBuffer;
		}

		// Token: 0x020000DF RID: 223
		private class SendRecordAsyncResult : IAsyncResult
		{
			// Token: 0x0600078C RID: 1932 RVA: 0x00021E52 File Offset: 0x00020052
			public SendRecordAsyncResult(AsyncCallback userCallback, object userState, HandshakeMessage message)
			{
				this._userCallback = userCallback;
				this._userState = userState;
				this._message = message;
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x0600078D RID: 1933 RVA: 0x00021E7A File Offset: 0x0002007A
			public HandshakeMessage Message
			{
				get
				{
					return this._message;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x0600078E RID: 1934 RVA: 0x00021E82 File Offset: 0x00020082
			public object AsyncState
			{
				get
				{
					return this._userState;
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x0600078F RID: 1935 RVA: 0x00021E8A File Offset: 0x0002008A
			public Exception AsyncException
			{
				get
				{
					return this._asyncException;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000790 RID: 1936 RVA: 0x00021E92 File Offset: 0x00020092
			public bool CompletedWithError
			{
				get
				{
					return this.IsCompleted && this._asyncException != null;
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000791 RID: 1937 RVA: 0x00021EA8 File Offset: 0x000200A8
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					object obj = this.locker;
					lock (obj)
					{
						if (this.handle == null)
						{
							this.handle = new ManualResetEvent(this.completed);
						}
					}
					return this.handle;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06000792 RID: 1938 RVA: 0x00021F04 File Offset: 0x00020104
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000793 RID: 1939 RVA: 0x00021F08 File Offset: 0x00020108
			public bool IsCompleted
			{
				get
				{
					object obj = this.locker;
					bool flag2;
					lock (obj)
					{
						flag2 = this.completed;
					}
					return flag2;
				}
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x00021F4C File Offset: 0x0002014C
			public void SetComplete(Exception ex)
			{
				object obj = this.locker;
				lock (obj)
				{
					if (!this.completed)
					{
						this.completed = true;
						if (this.handle != null)
						{
							this.handle.Set();
						}
						if (this._userCallback != null)
						{
							this._userCallback.BeginInvoke(this, null, null);
						}
						this._asyncException = ex;
					}
				}
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x00021FCC File Offset: 0x000201CC
			public void SetComplete()
			{
				this.SetComplete(null);
			}

			// Token: 0x040004F3 RID: 1267
			private object locker = new object();

			// Token: 0x040004F4 RID: 1268
			private AsyncCallback _userCallback;

			// Token: 0x040004F5 RID: 1269
			private object _userState;

			// Token: 0x040004F6 RID: 1270
			private Exception _asyncException;

			// Token: 0x040004F7 RID: 1271
			private ManualResetEvent handle;

			// Token: 0x040004F8 RID: 1272
			private HandshakeMessage _message;

			// Token: 0x040004F9 RID: 1273
			private bool completed;
		}
	}
}
