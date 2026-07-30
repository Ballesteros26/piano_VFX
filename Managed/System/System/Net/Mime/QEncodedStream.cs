using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020005A8 RID: 1448
	internal class QEncodedStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06002D2A RID: 11562 RVA: 0x000B2A9F File Offset: 0x000B0C9F
		internal QEncodedStream(WriteStateInfoBase wsi)
		{
			this.writeState = wsi;
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000B2AAE File Offset: 0x000B0CAE
		private QEncodedStream.ReadStateInfo ReadState
		{
			get
			{
				if (this.readState == null)
				{
					this.readState = new QEncodedStream.ReadStateInfo();
				}
				return this.readState;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000B2AC9 File Offset: 0x000B0CC9
		internal WriteStateInfoBase WriteState
		{
			get
			{
				return this.writeState;
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000B2AD4 File Offset: 0x000B0CD4
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
			QEncodedStream.WriteAsyncResult writeAsyncResult = new QEncodedStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000B2B2A File Offset: 0x000B0D2A
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000B2B38 File Offset: 0x000B0D38
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
				if (this.ReadState.IsEscaped)
				{
					if (this.ReadState.Byte == -1)
					{
						if (count == 1)
						{
							this.ReadState.Byte = (short)(*ptr3);
							return 0;
						}
						if (*ptr3 != 13 || ptr3[1] != 10)
						{
							byte b = QEncodedStream.hexDecodeMap[(int)(*ptr3)];
							byte b2 = QEncodedStream.hexDecodeMap[(int)ptr3[1]];
							if (b == 255)
							{
								throw new FormatException(global::SR.GetString("Invalid hex digit '{0}'.", new object[] { b }));
							}
							if (b2 == 255)
							{
								throw new FormatException(global::SR.GetString("Invalid hex digit '{0}'.", new object[] { b2 }));
							}
							*(ptr4++) = (byte)(((int)b << 4) + (int)b2);
						}
						ptr3 += 2;
					}
					else
					{
						if (this.ReadState.Byte != 13 || *ptr3 != 10)
						{
							byte b3 = QEncodedStream.hexDecodeMap[(int)this.ReadState.Byte];
							byte b4 = QEncodedStream.hexDecodeMap[(int)(*ptr3)];
							if (b3 == 255)
							{
								throw new FormatException(global::SR.GetString("Invalid hex digit '{0}'.", new object[] { b3 }));
							}
							if (b4 == 255)
							{
								throw new FormatException(global::SR.GetString("Invalid hex digit '{0}'.", new object[] { b4 }));
							}
							*(ptr4++) = (byte)(((int)b3 << 4) + (int)b4);
						}
						ptr3++;
					}
					this.ReadState.IsEscaped = false;
					this.ReadState.Byte = -1;
				}
				while (ptr3 < ptr5)
				{
					if (*ptr3 == 61)
					{
						long num = (long)(ptr5 - ptr3);
						if (num != 1L)
						{
							if (num != 2L)
							{
								if (ptr3[1] != 13 || ptr3[2] != 10)
								{
									byte b5 = QEncodedStream.hexDecodeMap[(int)ptr3[1]];
									byte b6 = QEncodedStream.hexDecodeMap[(int)ptr3[2]];
									if (b5 == 255)
									{
										throw new FormatException(global::SR.GetString("Invalid hex digit '{0}'.", new object[] { b5 }));
									}
									if (b6 == 255)
									{
										throw new FormatException(global::SR.GetString("Invalid hex digit '{0}'.", new object[] { b6 }));
									}
									*(ptr4++) = (byte)(((int)b5 << 4) + (int)b6);
								}
								ptr3 += 3;
								continue;
							}
							this.ReadState.Byte = (short)ptr3[1];
						}
						this.ReadState.IsEscaped = true;
						break;
					}
					if (*ptr3 == 95)
					{
						*(ptr4++) = 32;
						ptr3++;
					}
					else
					{
						*(ptr4++) = *(ptr3++);
					}
				}
				count = (int)((long)(ptr4 - ptr2));
			}
			return count;
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000B2DF0 File Offset: 0x000B0FF0
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			this.writeState.AppendHeader();
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this.WriteState.CurrentLineLength + 3 + this.WriteState.FooterLength >= this.WriteState.MaxLineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this.WriteState.CurrentLineLength + this.writeState.FooterLength >= this.WriteState.MaxLineLength)
				{
					this.WriteState.AppendCRLF(true);
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					i++;
					this.WriteState.Append(new byte[] { 61, 48, 68, 61, 48, 65 });
				}
				else if (buffer[i] == 32)
				{
					this.WriteState.Append(95);
				}
				else if (Uri.IsAsciiLetterOrDigit((char)buffer[i]))
				{
					this.WriteState.Append(buffer[i]);
				}
				else
				{
					this.WriteState.Append(61);
					this.WriteState.Append(QEncodedStream.hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QEncodedStream.hexEncodeMap[(int)(buffer[i] & 15)]);
				}
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x00002068 File Offset: 0x00000268
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000B2F40 File Offset: 0x000B1140
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000B2F63 File Offset: 0x000B1163
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QEncodedStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000B2F6B File Offset: 0x000B116B
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000B2F79 File Offset: 0x000B1179
		private void FlushInternal()
		{
			if (this.writeState != null && this.writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.Reset();
			}
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000B2FBC File Offset: 0x000B11BC
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
				num += this.EncodeBytes(buffer, offset + num, count - num);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x04002545 RID: 9541
		private const int sizeOfFoldingCRLF = 3;

		// Token: 0x04002546 RID: 9542
		private static byte[] hexDecodeMap = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 0, 1,
			2, 3, 4, 5, 6, 7, 8, 9, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 10, 11, 12, 13, 14,
			15, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 10, 11, 12,
			13, 14, 15, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
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
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue
		};

		// Token: 0x04002547 RID: 9543
		private static byte[] hexEncodeMap = new byte[]
		{
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			65, 66, 67, 68, 69, 70
		};

		// Token: 0x04002548 RID: 9544
		private QEncodedStream.ReadStateInfo readState;

		// Token: 0x04002549 RID: 9545
		private WriteStateInfoBase writeState;

		// Token: 0x020005A9 RID: 1449
		private class ReadStateInfo
		{
			// Token: 0x17000990 RID: 2448
			// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000B3050 File Offset: 0x000B1250
			// (set) Token: 0x06002D39 RID: 11577 RVA: 0x000B3058 File Offset: 0x000B1258
			internal bool IsEscaped
			{
				get
				{
					return this.isEscaped;
				}
				set
				{
					this.isEscaped = value;
				}
			}

			// Token: 0x17000991 RID: 2449
			// (get) Token: 0x06002D3A RID: 11578 RVA: 0x000B3061 File Offset: 0x000B1261
			// (set) Token: 0x06002D3B RID: 11579 RVA: 0x000B3069 File Offset: 0x000B1269
			internal short Byte
			{
				get
				{
					return this.b1;
				}
				set
				{
					this.b1 = value;
				}
			}

			// Token: 0x0400254A RID: 9546
			private bool isEscaped;

			// Token: 0x0400254B RID: 9547
			private short b1 = -1;
		}

		// Token: 0x020005AA RID: 1450
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002D3D RID: 11581 RVA: 0x000B3081 File Offset: 0x000B1281
			internal WriteAsyncResult(QEncodedStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x06002D3E RID: 11582 RVA: 0x000B30AB File Offset: 0x000B12AB
			private void CompleteWrite(IAsyncResult result)
			{
				this.parent.BaseStream.EndWrite(result);
				this.parent.WriteState.Reset();
			}

			// Token: 0x06002D3F RID: 11583 RVA: 0x000B30CE File Offset: 0x000B12CE
			internal static void End(IAsyncResult result)
			{
				((QEncodedStream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x06002D40 RID: 11584 RVA: 0x000B30DC File Offset: 0x000B12DC
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QEncodedStream.WriteAsyncResult writeAsyncResult = (QEncodedStream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception ex)
					{
						writeAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x06002D41 RID: 11585 RVA: 0x000B3128 File Offset: 0x000B1328
			internal void Write()
			{
				for (;;)
				{
					this.written += this.parent.EncodeBytes(this.buffer, this.offset + this.written, this.count - this.written);
					if (this.written >= this.count)
					{
						break;
					}
					IAsyncResult asyncResult = this.parent.BaseStream.BeginWrite(this.parent.WriteState.Buffer, 0, this.parent.WriteState.Length, QEncodedStream.WriteAsyncResult.onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x0400254C RID: 9548
			private QEncodedStream parent;

			// Token: 0x0400254D RID: 9549
			private byte[] buffer;

			// Token: 0x0400254E RID: 9550
			private int offset;

			// Token: 0x0400254F RID: 9551
			private int count;

			// Token: 0x04002550 RID: 9552
			private static AsyncCallback onWrite = new AsyncCallback(QEncodedStream.WriteAsyncResult.OnWrite);

			// Token: 0x04002551 RID: 9553
			private int written;
		}
	}
}
