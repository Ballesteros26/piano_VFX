using System;
using System.IO;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000052 RID: 82
	internal class TlsStream : Stream
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00012EB9 File Offset: 0x000110B9
		public bool EOF
		{
			get
			{
				return this.Position >= this.Length;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00012ECC File Offset: 0x000110CC
		public override bool CanWrite
		{
			get
			{
				return this.canWrite;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00012ED4 File Offset: 0x000110D4
		public override bool CanRead
		{
			get
			{
				return this.canRead;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00012EDC File Offset: 0x000110DC
		public override bool CanSeek
		{
			get
			{
				return this.buffer.CanSeek;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00012EE9 File Offset: 0x000110E9
		// (set) Token: 0x06000384 RID: 900 RVA: 0x00012EF6 File Offset: 0x000110F6
		public override long Position
		{
			get
			{
				return this.buffer.Position;
			}
			set
			{
				this.buffer.Position = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00012F04 File Offset: 0x00011104
		public override long Length
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00012F11 File Offset: 0x00011111
		public TlsStream()
		{
			this.buffer = new MemoryStream(0);
			this.canRead = false;
			this.canWrite = true;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00012F33 File Offset: 0x00011133
		public TlsStream(byte[] data)
		{
			if (data != null)
			{
				this.buffer = new MemoryStream(data);
			}
			else
			{
				this.buffer = new MemoryStream();
			}
			this.canRead = true;
			this.canWrite = false;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00012F68 File Offset: 0x00011168
		private byte[] ReadSmallValue(int length)
		{
			if (length > 4)
			{
				throw new ArgumentException("8 bytes maximum");
			}
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			if (this.Read(this.temp, 0, length) != length)
			{
				throw new TlsException(string.Format("buffer underrun", Array.Empty<object>()));
			}
			return this.temp;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00012FC4 File Offset: 0x000111C4
		public new byte ReadByte()
		{
			return this.ReadSmallValue(1)[0];
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00012FD0 File Offset: 0x000111D0
		public short ReadInt16()
		{
			byte[] array = this.ReadSmallValue(2);
			return (short)(((int)array[0] << 8) | (int)array[1]);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00012FF0 File Offset: 0x000111F0
		public int ReadInt24()
		{
			byte[] array = this.ReadSmallValue(3);
			return ((int)array[0] << 16) | ((int)array[1] << 8) | (int)array[2];
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00013018 File Offset: 0x00011218
		public int ReadInt32()
		{
			byte[] array = this.ReadSmallValue(4);
			return ((int)array[0] << 24) | ((int)array[1] << 16) | ((int)array[2] << 8) | (int)array[3];
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00013044 File Offset: 0x00011244
		public byte[] ReadBytes(int count)
		{
			byte[] array = new byte[count];
			if (this.Read(array, 0, count) != count)
			{
				throw new TlsException("buffer underrun");
			}
			return array;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00013070 File Offset: 0x00011270
		public void Write(byte value)
		{
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			this.temp[0] = value;
			this.Write(this.temp, 0, 1);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001309D File Offset: 0x0001129D
		public void Write(short value)
		{
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			this.temp[0] = (byte)(value >> 8);
			this.temp[1] = (byte)value;
			this.Write(this.temp, 0, 2);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000130D8 File Offset: 0x000112D8
		public void WriteInt24(int value)
		{
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			this.temp[0] = (byte)(value >> 16);
			this.temp[1] = (byte)(value >> 8);
			this.temp[2] = (byte)value;
			this.Write(this.temp, 0, 3);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001312C File Offset: 0x0001132C
		public void Write(int value)
		{
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			this.temp[0] = (byte)(value >> 24);
			this.temp[1] = (byte)(value >> 16);
			this.temp[2] = (byte)(value >> 8);
			this.temp[3] = (byte)value;
			this.Write(this.temp, 0, 4);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001318B File Offset: 0x0001138B
		public void Write(ulong value)
		{
			this.Write((int)(value >> 32));
			this.Write((int)value);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000131A0 File Offset: 0x000113A0
		public void Write(byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000131AD File Offset: 0x000113AD
		public void Reset()
		{
			this.buffer.SetLength(0L);
			this.buffer.Position = 0L;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000131C9 File Offset: 0x000113C9
		public byte[] ToArray()
		{
			return this.buffer.ToArray();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000131D6 File Offset: 0x000113D6
		public override void Flush()
		{
			this.buffer.Flush();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000131E3 File Offset: 0x000113E3
		public override void SetLength(long length)
		{
			this.buffer.SetLength(length);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000131F1 File Offset: 0x000113F1
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this.buffer.Seek(offset, loc);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00013200 File Offset: 0x00011400
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.canRead)
			{
				return this.buffer.Read(buffer, offset, count);
			}
			throw new InvalidOperationException("Read operations are not allowed by this stream");
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00013223 File Offset: 0x00011423
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.canWrite)
			{
				this.buffer.Write(buffer, offset, count);
				return;
			}
			throw new InvalidOperationException("Write operations are not allowed by this stream");
		}

		// Token: 0x040001C0 RID: 448
		private bool canRead;

		// Token: 0x040001C1 RID: 449
		private bool canWrite;

		// Token: 0x040001C2 RID: 450
		private MemoryStream buffer;

		// Token: 0x040001C3 RID: 451
		private byte[] temp;

		// Token: 0x040001C4 RID: 452
		private const int temp_size = 4;
	}
}
