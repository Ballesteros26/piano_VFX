using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020005AB RID: 1451
	internal class QuotedPrintableStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06002D43 RID: 11587 RVA: 0x000B31E0 File Offset: 0x000B13E0
		internal QuotedPrintableStream(Stream stream, int lineLength)
			: base(stream)
		{
			if (lineLength < 0)
			{
				throw new ArgumentOutOfRangeException("lineLength");
			}
			this.lineLength = lineLength;
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000B31FF File Offset: 0x000B13FF
		internal QuotedPrintableStream(Stream stream, bool encodeCRLF)
			: this(stream, EncodedStreamFactory.DefaultMaxLineLength)
		{
			this.encodeCRLF = encodeCRLF;
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x000B3214 File Offset: 0x000B1414
		private QuotedPrintableStream.ReadStateInfo ReadState
		{
			get
			{
				if (this.readState == null)
				{
					this.readState = new QuotedPrintableStream.ReadStateInfo();
				}
				return this.readState;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x000B322F File Offset: 0x000B142F
		internal WriteStateInfoBase WriteState
		{
			get
			{
				if (this.writeState == null)
				{
					this.writeState = new WriteStateInfoBase(1024, null, null, this.lineLength);
				}
				return this.writeState;
			}
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000B3258 File Offset: 0x000B1458
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
			QuotedPrintableStream.WriteAsyncResult writeAsyncResult = new QuotedPrintableStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000B32AE File Offset: 0x000B14AE
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000B32BC File Offset: 0x000B14BC
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
							byte b = QuotedPrintableStream.hexDecodeMap[(int)(*ptr3)];
							byte b2 = QuotedPrintableStream.hexDecodeMap[(int)ptr3[1]];
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
							byte b3 = QuotedPrintableStream.hexDecodeMap[(int)this.ReadState.Byte];
							byte b4 = QuotedPrintableStream.hexDecodeMap[(int)(*ptr3)];
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
									byte b5 = QuotedPrintableStream.hexDecodeMap[(int)ptr3[1]];
									byte b6 = QuotedPrintableStream.hexDecodeMap[(int)ptr3[2]];
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
					*(ptr4++) = *(ptr3++);
				}
				count = (int)((long)(ptr4 - ptr2));
			}
			return count;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000B3558 File Offset: 0x000B1758
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this.lineLength != -1 && this.WriteState.CurrentLineLength + 3 + 2 >= this.lineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this.writeState.CurrentLineLength + 3 + 2 >= EncodedStreamFactory.DefaultMaxLineLength)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.AppendCRLF(false);
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < (this.encodeCRLF ? 6 : 2))
					{
						return i - offset;
					}
					i++;
					if (this.encodeCRLF)
					{
						this.WriteState.Append(new byte[] { 61, 48, 68, 61, 48, 65 });
					}
					else
					{
						this.WriteState.AppendCRLF(false);
					}
				}
				else if ((buffer[i] < 32 && buffer[i] != 9) || buffer[i] == 61 || buffer[i] > 126)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.Append(QuotedPrintableStream.hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QuotedPrintableStream.hexEncodeMap[(int)(buffer[i] & 15)]);
				}
				else
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 1)
					{
						return i - offset;
					}
					if ((buffer[i] == 9 || buffer[i] == 32) && i + 1 >= count + offset)
					{
						if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
						{
							return i - offset;
						}
						this.WriteState.Append(61);
						this.WriteState.Append(QuotedPrintableStream.hexEncodeMap[buffer[i] >> 4]);
						this.WriteState.Append(QuotedPrintableStream.hexEncodeMap[(int)(buffer[i] & 15)]);
					}
					else
					{
						this.WriteState.Append(buffer[i]);
					}
				}
			}
			return i - offset;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x00002068 File Offset: 0x00000268
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000B37A3 File Offset: 0x000B19A3
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000B37C6 File Offset: 0x000B19C6
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QuotedPrintableStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000B37CE File Offset: 0x000B19CE
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000B37DC File Offset: 0x000B19DC
		private void FlushInternal()
		{
			if (this.writeState != null && this.writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000B381C File Offset: 0x000B1A1C
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

		// Token: 0x04002552 RID: 9554
		private bool encodeCRLF;

		// Token: 0x04002553 RID: 9555
		private const int sizeOfSoftCRLF = 3;

		// Token: 0x04002554 RID: 9556
		private const int sizeOfEncodedChar = 3;

		// Token: 0x04002555 RID: 9557
		private const int sizeOfEncodedCRLF = 6;

		// Token: 0x04002556 RID: 9558
		private const int sizeOfNonEncodedCRLF = 2;

		// Token: 0x04002557 RID: 9559
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

		// Token: 0x04002558 RID: 9560
		private static byte[] hexEncodeMap = new byte[]
		{
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			65, 66, 67, 68, 69, 70
		};

		// Token: 0x04002559 RID: 9561
		private int lineLength;

		// Token: 0x0400255A RID: 9562
		private QuotedPrintableStream.ReadStateInfo readState;

		// Token: 0x0400255B RID: 9563
		private WriteStateInfoBase writeState;

		// Token: 0x020005AC RID: 1452
		private class ReadStateInfo
		{
			// Token: 0x17000994 RID: 2452
			// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000B38B0 File Offset: 0x000B1AB0
			// (set) Token: 0x06002D53 RID: 11603 RVA: 0x000B38B8 File Offset: 0x000B1AB8
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

			// Token: 0x17000995 RID: 2453
			// (get) Token: 0x06002D54 RID: 11604 RVA: 0x000B38C1 File Offset: 0x000B1AC1
			// (set) Token: 0x06002D55 RID: 11605 RVA: 0x000B38C9 File Offset: 0x000B1AC9
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

			// Token: 0x0400255C RID: 9564
			private bool isEscaped;

			// Token: 0x0400255D RID: 9565
			private short b1 = -1;
		}

		// Token: 0x020005AD RID: 1453
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002D57 RID: 11607 RVA: 0x000B38E1 File Offset: 0x000B1AE1
			internal WriteAsyncResult(QuotedPrintableStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x06002D58 RID: 11608 RVA: 0x000B390B File Offset: 0x000B1B0B
			private void CompleteWrite(IAsyncResult result)
			{
				this.parent.BaseStream.EndWrite(result);
				this.parent.WriteState.BufferFlushed();
			}

			// Token: 0x06002D59 RID: 11609 RVA: 0x000B392E File Offset: 0x000B1B2E
			internal static void End(IAsyncResult result)
			{
				((QuotedPrintableStream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x06002D5A RID: 11610 RVA: 0x000B393C File Offset: 0x000B1B3C
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QuotedPrintableStream.WriteAsyncResult writeAsyncResult = (QuotedPrintableStream.WriteAsyncResult)result.AsyncState;
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

			// Token: 0x06002D5B RID: 11611 RVA: 0x000B3988 File Offset: 0x000B1B88
			internal void Write()
			{
				for (;;)
				{
					this.written += this.parent.EncodeBytes(this.buffer, this.offset + this.written, this.count - this.written);
					if (this.written >= this.count)
					{
						break;
					}
					IAsyncResult asyncResult = this.parent.BaseStream.BeginWrite(this.parent.WriteState.Buffer, 0, this.parent.WriteState.Length, QuotedPrintableStream.WriteAsyncResult.onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x0400255E RID: 9566
			private QuotedPrintableStream parent;

			// Token: 0x0400255F RID: 9567
			private byte[] buffer;

			// Token: 0x04002560 RID: 9568
			private int offset;

			// Token: 0x04002561 RID: 9569
			private int count;

			// Token: 0x04002562 RID: 9570
			private static AsyncCallback onWrite = new AsyncCallback(QuotedPrintableStream.WriteAsyncResult.OnWrite);

			// Token: 0x04002563 RID: 9571
			private int written;
		}
	}
}
