using System;
using System.IO;

namespace System.Security.Cryptography
{
	// Token: 0x0200066C RID: 1644
	internal sealed class TailStream : Stream
	{
		// Token: 0x0600469E RID: 18078 RVA: 0x000F8747 File Offset: 0x000F6947
		public TailStream(int bufferSize)
		{
			this._Buffer = new byte[bufferSize];
			this._BufferSize = bufferSize;
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x0009A9F1 File Offset: 0x00098BF1
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x000F8764 File Offset: 0x000F6964
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._Buffer != null)
					{
						Array.Clear(this._Buffer, 0, this._Buffer.Length);
					}
					this._Buffer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x000F87B4 File Offset: 0x000F69B4
		public byte[] Buffer
		{
			get
			{
				return (byte[])this._Buffer.Clone();
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x000F87C6 File Offset: 0x000F69C6
		public override bool CanWrite
		{
			get
			{
				return this._Buffer != null;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060046A5 RID: 18085 RVA: 0x00094062 File Offset: 0x00092262
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x00094062 File Offset: 0x00092262
		// (set) Token: 0x060046A7 RID: 18087 RVA: 0x00094062 File Offset: 0x00092262
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x00002194 File Offset: 0x00000394
		public override void Flush()
		{
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x00094062 File Offset: 0x00092262
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x00094062 File Offset: 0x00092262
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x00094051 File Offset: 0x00092251
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000F87D4 File Offset: 0x000F69D4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._Buffer == null)
			{
				throw new ObjectDisposedException("TailStream");
			}
			if (count == 0)
			{
				return;
			}
			if (this._BufferFull)
			{
				if (count > this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					return;
				}
				global::System.Buffer.InternalBlockCopy(this._Buffer, this._BufferSize - count, this._Buffer, 0, this._BufferSize - count);
				global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferSize - count, count);
				return;
			}
			else
			{
				if (count > this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					this._BufferFull = true;
					return;
				}
				if (count + this._BufferIndex >= this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(this._Buffer, this._BufferIndex + count - this._BufferSize, this._Buffer, 0, this._BufferSize - count);
					global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
					this._BufferFull = true;
					return;
				}
				global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
				this._BufferIndex += count;
				return;
			}
		}

		// Token: 0x04002456 RID: 9302
		private byte[] _Buffer;

		// Token: 0x04002457 RID: 9303
		private int _BufferSize;

		// Token: 0x04002458 RID: 9304
		private int _BufferIndex;

		// Token: 0x04002459 RID: 9305
		private bool _BufferFull;
	}
}
