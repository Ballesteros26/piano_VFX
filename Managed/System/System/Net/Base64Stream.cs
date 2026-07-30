using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net
{
	// Token: 0x020004C3 RID: 1219
	internal class Base64Stream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06002421 RID: 9249 RVA: 0x0008CED8 File Offset: 0x0008B0D8
		internal Base64Stream(Stream stream, Base64WriteStateInfo writeStateInfo)
			: base(stream)
		{
			this.writeState = new Base64WriteStateInfo();
			this.lineLength = writeStateInfo.MaxLineLength;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x0008CEF8 File Offset: 0x0008B0F8
		internal Base64Stream(Stream stream, int lineLength)
			: base(stream)
		{
			this.lineLength = lineLength;
			this.writeState = new Base64WriteStateInfo();
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x0008CF13 File Offset: 0x0008B113
		internal Base64Stream(Base64WriteStateInfo writeStateInfo)
		{
			this.lineLength = writeStateInfo.MaxLineLength;
			this.writeState = writeStateInfo;
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x0008CF2E File Offset: 0x0008B12E
		public override bool CanWrite
		{
			get
			{
				return base.CanWrite;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x0008CF36 File Offset: 0x0008B136
		private Base64Stream.ReadStateInfo ReadState
		{
			get
			{
				if (this.readState == null)
				{
					this.readState = new Base64Stream.ReadStateInfo();
				}
				return this.readState;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x0008CF51 File Offset: 0x0008B151
		internal Base64WriteStateInfo WriteState
		{
			get
			{
				return this.writeState;
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x0008CF5C File Offset: 0x0008B15C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			Base64Stream.ReadAsyncResult readAsyncResult = new Base64Stream.ReadAsyncResult(this, buffer, offset, count, callback, state);
			readAsyncResult.Read();
			return readAsyncResult;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0008CFB4 File Offset: 0x0008B1B4
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			Base64Stream.WriteAsyncResult writeAsyncResult = new Base64Stream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0008D00C File Offset: 0x0008B20C
		public override void Close()
		{
			if (this.writeState != null && this.WriteState.Length > 0)
			{
				int padding = this.WriteState.Padding;
				if (padding != 1)
				{
					if (padding == 2)
					{
						this.WriteState.Append(new byte[]
						{
							Base64Stream.base64EncodeMap[(int)this.WriteState.LastBits],
							Base64Stream.base64EncodeMap[64],
							Base64Stream.base64EncodeMap[64]
						});
					}
				}
				else
				{
					this.WriteState.Append(new byte[]
					{
						Base64Stream.base64EncodeMap[(int)this.WriteState.LastBits],
						Base64Stream.base64EncodeMap[64]
					});
				}
				this.WriteState.Padding = 0;
				this.FlushInternal();
			}
			base.Close();
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x0008D0D0 File Offset: 0x0008B2D0
		public unsafe int DecodeBytes(byte[] buffer, int offset, int count)
		{
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				byte* ptr2 = ptr + offset;
				byte* ptr3 = ptr2;
				byte* ptr4 = ptr2;
				byte* ptr5 = ptr2 + count;
				while (ptr3 < ptr5)
				{
					if (*ptr3 == 13 || *ptr3 == 10 || *ptr3 == 61 || *ptr3 == 32 || *ptr3 == 9)
					{
						ptr3++;
					}
					else
					{
						byte b = Base64Stream.base64DecodeMap[(int)(*ptr3)];
						if (b == 255)
						{
							throw new FormatException(global::SR.GetString("An invalid character was found in the Base-64 stream."));
						}
						switch (this.ReadState.Pos)
						{
						case 0:
						{
							this.ReadState.Val = (byte)(b << 2);
							Base64Stream.ReadStateInfo readStateInfo = this.ReadState;
							byte b2 = readStateInfo.Pos;
							readStateInfo.Pos = b2 + 1;
							break;
						}
						case 1:
						{
							*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 4));
							this.ReadState.Val = (byte)(b << 4);
							Base64Stream.ReadStateInfo readStateInfo2 = this.ReadState;
							byte b2 = readStateInfo2.Pos;
							readStateInfo2.Pos = b2 + 1;
							break;
						}
						case 2:
						{
							*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 2));
							this.ReadState.Val = (byte)(b << 6);
							Base64Stream.ReadStateInfo readStateInfo3 = this.ReadState;
							byte b2 = readStateInfo3.Pos;
							readStateInfo3.Pos = b2 + 1;
							break;
						}
						case 3:
							*(ptr4++) = this.ReadState.Val + b;
							this.ReadState.Pos = 0;
							break;
						}
						ptr3++;
					}
				}
				count = (int)((long)(ptr4 - ptr2));
			}
			return count;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x0008D26A File Offset: 0x0008B46A
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			return this.EncodeBytes(buffer, offset, count, true, true);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x0008D278 File Offset: 0x0008B478
		internal int EncodeBytes(byte[] buffer, int offset, int count, bool dontDeferFinalBytes, bool shouldAppendSpaceToCRLF)
		{
			int i = offset;
			this.WriteState.AppendHeader();
			int num = this.WriteState.Padding;
			if (num != 1)
			{
				if (num == 2)
				{
					this.WriteState.Append(Base64Stream.base64EncodeMap[(int)this.WriteState.LastBits | ((buffer[i] & 240) >> 4)]);
					if (count == 1)
					{
						this.WriteState.LastBits = (byte)((buffer[i] & 15) << 2);
						this.WriteState.Padding = 1;
						return i - offset;
					}
					this.WriteState.Append(Base64Stream.base64EncodeMap[((int)(buffer[i] & 15) << 2) | ((buffer[i + 1] & 192) >> 6)]);
					this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i + 1] & 63)]);
					i += 2;
					count -= 2;
					this.WriteState.Padding = 0;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.base64EncodeMap[(int)this.WriteState.LastBits | ((buffer[i] & 192) >> 6)]);
				this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i] & 63)]);
				i++;
				count--;
				this.WriteState.Padding = 0;
			}
			int num2 = i + (count - count % 3);
			while (i < num2)
			{
				if (this.lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this.writeState.FooterLength > this.lineLength)
				{
					this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
				}
				this.WriteState.Append(Base64Stream.base64EncodeMap[(buffer[i] & 252) >> 2]);
				this.WriteState.Append(Base64Stream.base64EncodeMap[((int)(buffer[i] & 3) << 4) | ((buffer[i + 1] & 240) >> 4)]);
				this.WriteState.Append(Base64Stream.base64EncodeMap[((int)(buffer[i + 1] & 15) << 2) | ((buffer[i + 2] & 192) >> 6)]);
				this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i + 2] & 63)]);
				i += 3;
			}
			i = num2;
			if (count % 3 != 0 && this.lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this.writeState.FooterLength >= this.lineLength)
			{
				this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
			}
			num = count % 3;
			if (num != 1)
			{
				if (num == 2)
				{
					this.WriteState.Append(Base64Stream.base64EncodeMap[(buffer[i] & 252) >> 2]);
					this.WriteState.Append(Base64Stream.base64EncodeMap[((int)(buffer[i] & 3) << 4) | ((buffer[i + 1] & 240) >> 4)]);
					if (dontDeferFinalBytes)
					{
						this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i + 1] & 15) << 2]);
						this.WriteState.Append(Base64Stream.base64EncodeMap[64]);
						this.WriteState.Padding = 0;
					}
					else
					{
						this.WriteState.LastBits = (byte)((buffer[i + 1] & 15) << 2);
						this.WriteState.Padding = 1;
					}
					i += 2;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.base64EncodeMap[(buffer[i] & 252) >> 2]);
				if (dontDeferFinalBytes)
				{
					this.WriteState.Append(Base64Stream.base64EncodeMap[(int)((byte)((buffer[i] & 3) << 4))]);
					this.WriteState.Append(Base64Stream.base64EncodeMap[64]);
					this.WriteState.Append(Base64Stream.base64EncodeMap[64]);
					this.WriteState.Padding = 0;
				}
				else
				{
					this.WriteState.LastBits = (byte)((buffer[i] & 3) << 4);
					this.WriteState.Padding = 2;
				}
				i++;
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00002068 File Offset: 0x00000268
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0008D615 File Offset: 0x0008B815
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0008D638 File Offset: 0x0008B838
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return Base64Stream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0008D64E File Offset: 0x0008B84E
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Base64Stream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0008D664 File Offset: 0x0008B864
		public override void Flush()
		{
			if (this.writeState != null && this.WriteState.Length > 0)
			{
				this.FlushInternal();
			}
			base.Flush();
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0008D688 File Offset: 0x0008B888
		private void FlushInternal()
		{
			base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
			this.WriteState.Reset();
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0008D6B4 File Offset: 0x0008B8B4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			for (;;)
			{
				int num = base.Read(buffer, offset, count);
				if (num == 0)
				{
					break;
				}
				num = this.DecodeBytes(buffer, offset, num);
				if (num > 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0008D718 File Offset: 0x0008B918
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = 0;
			for (;;)
			{
				num += this.EncodeBytes(buffer, offset + num, count - num, false, false);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x0400200C RID: 8204
		private static byte[] base64DecodeMap = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, 62, byte.MaxValue, byte.MaxValue, byte.MaxValue, 63, 52, 53,
			54, 55, 56, 57, 58, 59, 60, 61, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 0, 1, 2, 3, 4,
			5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
			25, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 26, 27, 28,
			29, 30, 31, 32, 33, 34, 35, 36, 37, 38,
			39, 40, 41, 42, 43, 44, 45, 46, 47, 48,
			49, 50, 51, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue
		};

		// Token: 0x0400200D RID: 8205
		private static byte[] base64EncodeMap = new byte[]
		{
			65, 66, 67, 68, 69, 70, 71, 72, 73, 74,
			75, 76, 77, 78, 79, 80, 81, 82, 83, 84,
			85, 86, 87, 88, 89, 90, 97, 98, 99, 100,
			101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
			111, 112, 113, 114, 115, 116, 117, 118, 119, 120,
			121, 122, 48, 49, 50, 51, 52, 53, 54, 55,
			56, 57, 43, 47, 61
		};

		// Token: 0x0400200E RID: 8206
		private int lineLength;

		// Token: 0x0400200F RID: 8207
		private Base64Stream.ReadStateInfo readState;

		// Token: 0x04002010 RID: 8208
		private Base64WriteStateInfo writeState;

		// Token: 0x04002011 RID: 8209
		private const int sizeOfBase64EncodedChar = 4;

		// Token: 0x04002012 RID: 8210
		private const byte invalidBase64Value = 255;

		// Token: 0x020004C4 RID: 1220
		private class ReadAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002436 RID: 9270 RVA: 0x0008D7AE File Offset: 0x0008B9AE
			internal ReadAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x06002437 RID: 9271 RVA: 0x0008D7D8 File Offset: 0x0008B9D8
			private bool CompleteRead(IAsyncResult result)
			{
				this.read = this.parent.BaseStream.EndRead(result);
				if (this.read == 0)
				{
					base.InvokeCallback();
					return true;
				}
				this.read = this.parent.DecodeBytes(this.buffer, this.offset, this.read);
				if (this.read > 0)
				{
					base.InvokeCallback();
					return true;
				}
				return false;
			}

			// Token: 0x06002438 RID: 9272 RVA: 0x0008D844 File Offset: 0x0008BA44
			internal void Read()
			{
				IAsyncResult asyncResult;
				do
				{
					asyncResult = this.parent.BaseStream.BeginRead(this.buffer, this.offset, this.count, Base64Stream.ReadAsyncResult.onRead, this);
				}
				while (asyncResult.CompletedSynchronously && !this.CompleteRead(asyncResult));
			}

			// Token: 0x06002439 RID: 9273 RVA: 0x0008D88C File Offset: 0x0008BA8C
			private static void OnRead(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result.AsyncState;
					try
					{
						if (!readAsyncResult.CompleteRead(result))
						{
							readAsyncResult.Read();
						}
					}
					catch (Exception ex)
					{
						if (readAsyncResult.IsCompleted)
						{
							throw;
						}
						readAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x0600243A RID: 9274 RVA: 0x0008D8E4 File Offset: 0x0008BAE4
			internal static int End(IAsyncResult result)
			{
				Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result;
				readAsyncResult.InternalWaitForCompletion();
				return readAsyncResult.read;
			}

			// Token: 0x04002013 RID: 8211
			private Base64Stream parent;

			// Token: 0x04002014 RID: 8212
			private byte[] buffer;

			// Token: 0x04002015 RID: 8213
			private int offset;

			// Token: 0x04002016 RID: 8214
			private int count;

			// Token: 0x04002017 RID: 8215
			private int read;

			// Token: 0x04002018 RID: 8216
			private static AsyncCallback onRead = new AsyncCallback(Base64Stream.ReadAsyncResult.OnRead);
		}

		// Token: 0x020004C5 RID: 1221
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x0600243C RID: 9276 RVA: 0x0008D90B File Offset: 0x0008BB0B
			internal WriteAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x0600243D RID: 9277 RVA: 0x0008D938 File Offset: 0x0008BB38
			internal void Write()
			{
				for (;;)
				{
					this.written += this.parent.EncodeBytes(this.buffer, this.offset + this.written, this.count - this.written, false, false);
					if (this.written >= this.count)
					{
						break;
					}
					IAsyncResult asyncResult = this.parent.BaseStream.BeginWrite(this.parent.WriteState.Buffer, 0, this.parent.WriteState.Length, Base64Stream.WriteAsyncResult.onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x0600243E RID: 9278 RVA: 0x0008D9DF File Offset: 0x0008BBDF
			private void CompleteWrite(IAsyncResult result)
			{
				this.parent.BaseStream.EndWrite(result);
				this.parent.WriteState.Reset();
			}

			// Token: 0x0600243F RID: 9279 RVA: 0x0008DA04 File Offset: 0x0008BC04
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.WriteAsyncResult writeAsyncResult = (Base64Stream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception ex)
					{
						if (writeAsyncResult.IsCompleted)
						{
							throw;
						}
						writeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x06002440 RID: 9280 RVA: 0x0008DA58 File Offset: 0x0008BC58
			internal static void End(IAsyncResult result)
			{
				((Base64Stream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x04002019 RID: 8217
			private Base64Stream parent;

			// Token: 0x0400201A RID: 8218
			private byte[] buffer;

			// Token: 0x0400201B RID: 8219
			private int offset;

			// Token: 0x0400201C RID: 8220
			private int count;

			// Token: 0x0400201D RID: 8221
			private static AsyncCallback onWrite = new AsyncCallback(Base64Stream.WriteAsyncResult.OnWrite);

			// Token: 0x0400201E RID: 8222
			private int written;
		}

		// Token: 0x020004C6 RID: 1222
		private class ReadStateInfo
		{
			// Token: 0x1700077D RID: 1917
			// (get) Token: 0x06002442 RID: 9282 RVA: 0x0008DA79 File Offset: 0x0008BC79
			// (set) Token: 0x06002443 RID: 9283 RVA: 0x0008DA81 File Offset: 0x0008BC81
			internal byte Val
			{
				get
				{
					return this.val;
				}
				set
				{
					this.val = value;
				}
			}

			// Token: 0x1700077E RID: 1918
			// (get) Token: 0x06002444 RID: 9284 RVA: 0x0008DA8A File Offset: 0x0008BC8A
			// (set) Token: 0x06002445 RID: 9285 RVA: 0x0008DA92 File Offset: 0x0008BC92
			internal byte Pos
			{
				get
				{
					return this.pos;
				}
				set
				{
					this.pos = value;
				}
			}

			// Token: 0x0400201F RID: 8223
			private byte val;

			// Token: 0x04002020 RID: 8224
			private byte pos;
		}
	}
}
