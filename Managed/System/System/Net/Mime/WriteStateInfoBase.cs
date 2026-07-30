using System;

namespace System.Net.Mime
{
	// Token: 0x020005B0 RID: 1456
	internal class WriteStateInfoBase
	{
		// Token: 0x06002D6A RID: 11626 RVA: 0x000B3FEC File Offset: 0x000B21EC
		internal WriteStateInfoBase()
		{
			this.buffer = new byte[1024];
			this._header = new byte[0];
			this._footer = new byte[0];
			this._maxLineLength = EncodedStreamFactory.DefaultMaxLineLength;
			this._currentLineLength = 0;
			this._currentBufferUsed = 0;
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000B4040 File Offset: 0x000B2240
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength)
			: this(bufferSize, header, footer, maxLineLength, 0)
		{
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000B404E File Offset: 0x000B224E
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
		{
			this.buffer = new byte[bufferSize];
			this._header = header;
			this._footer = footer;
			this._maxLineLength = maxLineLength;
			this._currentLineLength = mimeHeaderLength;
			this._currentBufferUsed = 0;
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x000B4087 File Offset: 0x000B2287
		internal int FooterLength
		{
			get
			{
				return this._footer.Length;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000B4091 File Offset: 0x000B2291
		internal byte[] Footer
		{
			get
			{
				return this._footer;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000B4099 File Offset: 0x000B2299
		internal byte[] Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000B40A1 File Offset: 0x000B22A1
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000B40A9 File Offset: 0x000B22A9
		internal int Length
		{
			get
			{
				return this._currentBufferUsed;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000B40B1 File Offset: 0x000B22B1
		internal int CurrentLineLength
		{
			get
			{
				return this._currentLineLength;
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000B40BC File Offset: 0x000B22BC
		private void EnsureSpaceInBuffer(int moreBytes)
		{
			int num = this.Buffer.Length;
			while (this._currentBufferUsed + moreBytes >= num)
			{
				num *= 2;
			}
			if (num > this.Buffer.Length)
			{
				byte[] array = new byte[num];
				this.buffer.CopyTo(array, 0);
				this.buffer = array;
			}
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000B410C File Offset: 0x000B230C
		internal void Append(byte aByte)
		{
			this.EnsureSpaceInBuffer(1);
			byte[] array = this.Buffer;
			int currentBufferUsed = this._currentBufferUsed;
			this._currentBufferUsed = currentBufferUsed + 1;
			array[currentBufferUsed] = aByte;
			this._currentLineLength++;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000B4147 File Offset: 0x000B2347
		internal void Append(params byte[] bytes)
		{
			this.EnsureSpaceInBuffer(bytes.Length);
			bytes.CopyTo(this.buffer, this.Length);
			this._currentLineLength += bytes.Length;
			this._currentBufferUsed += bytes.Length;
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000B4184 File Offset: 0x000B2384
		internal void AppendCRLF(bool includeSpace)
		{
			this.AppendFooter();
			this.Append(new byte[] { 13, 10 });
			this._currentLineLength = 0;
			if (includeSpace)
			{
				this.Append(32);
			}
			this.AppendHeader();
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000B41BA File Offset: 0x000B23BA
		internal void AppendHeader()
		{
			if (this.Header != null && this.Header.Length != 0)
			{
				this.Append(this.Header);
			}
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000B41D9 File Offset: 0x000B23D9
		internal void AppendFooter()
		{
			if (this.Footer != null && this.Footer.Length != 0)
			{
				this.Append(this.Footer);
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x000B41F8 File Offset: 0x000B23F8
		internal int MaxLineLength
		{
			get
			{
				return this._maxLineLength;
			}
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000B4200 File Offset: 0x000B2400
		internal void Reset()
		{
			this._currentBufferUsed = 0;
			this._currentLineLength = 0;
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000B4210 File Offset: 0x000B2410
		internal void BufferFlushed()
		{
			this._currentBufferUsed = 0;
		}

		// Token: 0x0400257A RID: 9594
		protected byte[] _header;

		// Token: 0x0400257B RID: 9595
		protected byte[] _footer;

		// Token: 0x0400257C RID: 9596
		protected int _maxLineLength;

		// Token: 0x0400257D RID: 9597
		protected byte[] buffer;

		// Token: 0x0400257E RID: 9598
		protected int _currentLineLength;

		// Token: 0x0400257F RID: 9599
		protected int _currentBufferUsed;

		// Token: 0x04002580 RID: 9600
		protected const int defaultBufferSize = 1024;
	}
}
