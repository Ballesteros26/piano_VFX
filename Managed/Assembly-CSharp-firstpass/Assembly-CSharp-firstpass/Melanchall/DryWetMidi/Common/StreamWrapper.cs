using System;
using System.IO;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001CD RID: 461
	internal sealed class StreamWrapper : Stream
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x00024ED2 File Offset: 0x000230D2
		public StreamWrapper(Stream stream, int bufferCapacity)
		{
			this._stream = stream;
			this._buffer = new CircularBuffer<byte>(bufferCapacity);
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00003941 File Offset: 0x00001B41
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00003941 File Offset: 0x00001B41
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0001E512 File Offset: 0x0001C712
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00024F09 File Offset: 0x00023109
		public override long Length
		{
			get
			{
				return long.MaxValue;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00024F14 File Offset: 0x00023114
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x00024F1C File Offset: 0x0002311C
		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				ThrowIfArgument.IsNegative("value", value, "Position is negative.");
				int num = (int)(value - this._position);
				if (num == 0)
				{
					return;
				}
				if (num > 0)
				{
					this.SkipBytes(num);
				}
				else
				{
					this._buffer.MovePositionBack(-num);
				}
				this._position = value;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00024F68 File Offset: 0x00023168
		public bool IsEndReached()
		{
			if (this.Read(this._peekBuffer, 0, 1) == 0)
			{
				return true;
			}
			long position = this.Position;
			this.Position = position - 1L;
			return false;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00024F99 File Offset: 0x00023199
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00024FA0 File Offset: 0x000231A0
		public override int Read(byte[] buffer, int offset, int count)
		{
			byte[] array = this._buffer.MovePositionForward(count);
			Buffer.BlockCopy(array, 0, buffer, offset, array.Length);
			offset += array.Length;
			int num = this._stream.Read(buffer, offset, count - array.Length);
			for (int i = 0; i < num; i++)
			{
				this._buffer.Add(buffer[offset + i]);
			}
			int num2 = array.Length + num;
			this._position += (long)num2;
			if (count > 0 && num2 == 0)
			{
				this._position = long.MaxValue;
			}
			return num2;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00024F99 File Offset: 0x00023199
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00024F99 File Offset: 0x00023199
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00024F99 File Offset: 0x00023199
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00025028 File Offset: 0x00023228
		private void SkipBytes(int count)
		{
			while (count > 0)
			{
				int num = this.Read(this._skipBytesBuffer, 0, Math.Min(count, this._skipBytesBuffer.Length));
				if (num == 0)
				{
					break;
				}
				count -= num;
			}
		}

		// Token: 0x04000A25 RID: 2597
		private readonly Stream _stream;

		// Token: 0x04000A26 RID: 2598
		private readonly CircularBuffer<byte> _buffer;

		// Token: 0x04000A27 RID: 2599
		private readonly byte[] _peekBuffer = new byte[1];

		// Token: 0x04000A28 RID: 2600
		private readonly byte[] _skipBytesBuffer = new byte[1024];

		// Token: 0x04000A29 RID: 2601
		private long _position;
	}
}
