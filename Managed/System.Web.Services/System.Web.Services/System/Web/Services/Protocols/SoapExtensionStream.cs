using System;
using System.IO;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000064 RID: 100
	internal class SoapExtensionStream : Stream
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000BD75 File Offset: 0x00009F75
		internal SoapExtensionStream()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000BD7D File Offset: 0x00009F7D
		private bool EnsureStreamReady()
		{
			if (this.streamReady)
			{
				return true;
			}
			throw new InvalidOperationException(Res.GetString("WebBadStreamState"));
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000BD98 File Offset: 0x00009F98
		public override bool CanRead
		{
			get
			{
				this.EnsureStreamReady();
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public override bool CanSeek
		{
			get
			{
				this.EnsureStreamReady();
				return this.innerStream.CanSeek;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public override bool CanWrite
		{
			get
			{
				this.EnsureStreamReady();
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000BDD4 File Offset: 0x00009FD4
		internal bool HasWritten
		{
			get
			{
				return this.hasWritten;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public override long Length
		{
			get
			{
				this.EnsureStreamReady();
				return this.innerStream.Length;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000BE04 File Offset: 0x0000A004
		public override long Position
		{
			get
			{
				this.EnsureStreamReady();
				return this.innerStream.Position;
			}
			set
			{
				this.EnsureStreamReady();
				this.hasWritten = true;
				this.innerStream.Position = value;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000BE20 File Offset: 0x0000A020
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.EnsureStreamReady();
					this.hasWritten = true;
					this.innerStream.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000BE64 File Offset: 0x0000A064
		public override void Flush()
		{
			this.EnsureStreamReady();
			this.hasWritten = true;
			this.innerStream.Flush();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000BE7F File Offset: 0x0000A07F
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.EnsureStreamReady();
			return this.innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000BE9A File Offset: 0x0000A09A
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.EnsureStreamReady();
			return this.innerStream.EndRead(asyncResult);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000BEAF File Offset: 0x0000A0AF
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.EnsureStreamReady();
			this.hasWritten = true;
			return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000BED1 File Offset: 0x0000A0D1
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.EnsureStreamReady();
			this.hasWritten = true;
			this.innerStream.EndWrite(asyncResult);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BEED File Offset: 0x0000A0ED
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.EnsureStreamReady();
			return this.innerStream.Seek(offset, origin);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000BF03 File Offset: 0x0000A103
		public override void SetLength(long value)
		{
			this.EnsureStreamReady();
			this.innerStream.SetLength(value);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000BF18 File Offset: 0x0000A118
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.EnsureStreamReady();
			return this.innerStream.Read(buffer, offset, count);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000BF2F File Offset: 0x0000A12F
		public override int ReadByte()
		{
			this.EnsureStreamReady();
			return this.innerStream.ReadByte();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000BF43 File Offset: 0x0000A143
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.EnsureStreamReady();
			this.hasWritten = true;
			this.innerStream.Write(buffer, offset, count);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000BF61 File Offset: 0x0000A161
		public override void WriteByte(byte value)
		{
			this.EnsureStreamReady();
			this.hasWritten = true;
			this.innerStream.WriteByte(value);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000BF7D File Offset: 0x0000A17D
		internal void SetInnerStream(Stream stream)
		{
			this.innerStream = stream;
			this.hasWritten = false;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000BF8D File Offset: 0x0000A18D
		internal void SetStreamReady()
		{
			this.streamReady = true;
		}

		// Token: 0x0400026F RID: 623
		internal Stream innerStream;

		// Token: 0x04000270 RID: 624
		private bool hasWritten;

		// Token: 0x04000271 RID: 625
		private bool streamReady;
	}
}
