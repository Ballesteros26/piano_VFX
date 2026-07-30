using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000571 RID: 1393
	internal class BufferBuilder
	{
		// Token: 0x06002B41 RID: 11073 RVA: 0x000A976E File Offset: 0x000A796E
		internal BufferBuilder()
			: this(256)
		{
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000A977B File Offset: 0x000A797B
		internal BufferBuilder(int initialSize)
		{
			this.buffer = new byte[initialSize];
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000A9790 File Offset: 0x000A7990
		private void EnsureBuffer(int count)
		{
			if (count > this.buffer.Length - this.offset)
			{
				byte[] array = new byte[(this.buffer.Length * 2 > this.buffer.Length + count) ? (this.buffer.Length * 2) : (this.buffer.Length + count)];
				Buffer.BlockCopy(this.buffer, 0, array, 0, this.offset);
				this.buffer = array;
			}
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000A97FC File Offset: 0x000A79FC
		internal void Append(byte value)
		{
			this.EnsureBuffer(1);
			byte[] array = this.buffer;
			int num = this.offset;
			this.offset = num + 1;
			array[num] = value;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000A9829 File Offset: 0x000A7A29
		internal void Append(byte[] value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000A9836 File Offset: 0x000A7A36
		internal void Append(byte[] value, int offset, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, offset, this.buffer, this.offset, count);
			this.offset += count;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000A9861 File Offset: 0x000A7A61
		internal void Append(string value)
		{
			this.Append(value, false);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000A986B File Offset: 0x000A7A6B
		internal void Append(string value, bool allowUnicode)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			this.Append(value, 0, value.Length, allowUnicode);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000A9888 File Offset: 0x000A7A88
		internal void Append(string value, int offset, int count, bool allowUnicode)
		{
			if (allowUnicode)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value.ToCharArray(), offset, count);
				this.Append(bytes);
				return;
			}
			this.Append(value, offset, count);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000A98C0 File Offset: 0x000A7AC0
		internal void Append(string value, int offset, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[offset + i];
				if (c > 'ÿ')
				{
					throw new FormatException(global::SR.GetString("An invalid character was found in the mail header: '{0}'.", new object[] { c }));
				}
				this.buffer[this.offset + i] = (byte)c;
			}
			this.offset += count;
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000A9930 File Offset: 0x000A7B30
		internal int Length
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000A9938 File Offset: 0x000A7B38
		internal byte[] GetBuffer()
		{
			return this.buffer;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000A9940 File Offset: 0x000A7B40
		internal void Reset()
		{
			this.offset = 0;
		}

		// Token: 0x0400242A RID: 9258
		private byte[] buffer;

		// Token: 0x0400242B RID: 9259
		private int offset;
	}
}
