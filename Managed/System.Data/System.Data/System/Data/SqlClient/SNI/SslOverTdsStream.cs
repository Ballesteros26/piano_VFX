using System;
using System.IO;
using System.IO.Pipes;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200025A RID: 602
	internal sealed class SslOverTdsStream : Stream
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x00086470 File Offset: 0x00084670
		public SslOverTdsStream(Stream stream)
		{
			this._stream = stream;
			this._encapsulate = true;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00086486 File Offset: 0x00084686
		public void FinishHandshake()
		{
			this._encapsulate = false;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00086490 File Offset: 0x00084690
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = 0;
			byte[] array = new byte[(count < 8) ? 8 : count];
			if (this._encapsulate)
			{
				if (this._packetBytes == 0)
				{
					while (i < 8)
					{
						i += this._stream.Read(array, i, 8 - i);
					}
					this._packetBytes = ((int)array[2] << 8) | (int)array[3];
					this._packetBytes -= 8;
				}
				if (count > this._packetBytes)
				{
					count = this._packetBytes;
				}
			}
			i = this._stream.Read(array, 0, count);
			if (this._encapsulate)
			{
				this._packetBytes -= i;
			}
			Buffer.BlockCopy(array, 0, buffer, offset, i);
			return i;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00086534 File Offset: 0x00084734
		public override void Write(byte[] buffer, int offset, int count)
		{
			int num = offset;
			while (count > 0)
			{
				int num2;
				if (this._encapsulate)
				{
					if (count > 4088)
					{
						num2 = 4088;
					}
					else
					{
						num2 = count;
					}
					count -= num2;
					byte[] array = new byte[8 + num2];
					array[0] = 18;
					array[1] = ((count > 0) ? 0 : 1);
					array[2] = (byte)((num2 + 8) / 256);
					array[3] = (byte)((num2 + 8) % 256);
					array[4] = 0;
					array[5] = 0;
					array[6] = 0;
					array[7] = 0;
					for (int i = 8; i < array.Length; i++)
					{
						array[i] = buffer[num + (i - 8)];
					}
					this._stream.Write(array, 0, array.Length);
				}
				else
				{
					num2 = count;
					count = 0;
					this._stream.Write(buffer, num, num2);
				}
				this._stream.Flush();
				num += num2;
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0007BE7D File Offset: 0x0007A07D
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00086603 File Offset: 0x00084803
		public override void Flush()
		{
			if (!(this._stream is PipeStream))
			{
				this._stream.Flush();
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0007BE7D File Offset: 0x0007A07D
		// (set) Token: 0x06001A93 RID: 6803 RVA: 0x0007BE7D File Offset: 0x0007A07D
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0007BE7D File Offset: 0x0007A07D
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0008661D File Offset: 0x0008481D
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0008662A File Offset: 0x0008482A
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0007BE7D File Offset: 0x0007A07D
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400131C RID: 4892
		private readonly Stream _stream;

		// Token: 0x0400131D RID: 4893
		private int _packetBytes;

		// Token: 0x0400131E RID: 4894
		private bool _encapsulate;

		// Token: 0x0400131F RID: 4895
		private const int PACKET_SIZE_WITHOUT_HEADER = 4088;

		// Token: 0x04001320 RID: 4896
		private const int PRELOGIN_PACKET_TYPE = 18;
	}
}
