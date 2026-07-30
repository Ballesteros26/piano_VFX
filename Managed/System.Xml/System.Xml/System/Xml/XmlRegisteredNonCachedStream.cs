using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200028E RID: 654
	internal class XmlRegisteredNonCachedStream : Stream
	{
		// Token: 0x06001869 RID: 6249 RVA: 0x0008E4AE File Offset: 0x0008C6AE
		internal XmlRegisteredNonCachedStream(Stream stream, XmlDownloadManager downloadManager, string host)
		{
			this.stream = stream;
			this.downloadManager = downloadManager;
			this.host = host;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0008E4CC File Offset: 0x0008C6CC
		~XmlRegisteredNonCachedStream()
		{
			if (this.downloadManager != null)
			{
				this.downloadManager.Remove(this.host);
			}
			this.stream = null;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0008E514 File Offset: 0x0008C714
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.stream != null)
				{
					if (this.downloadManager != null)
					{
						this.downloadManager.Remove(this.host);
					}
					this.stream.Close();
				}
				this.stream = null;
				GC.SuppressFinalize(this);
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0008E578 File Offset: 0x0008C778
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0008E58C File Offset: 0x0008C78C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0008E5A0 File Offset: 0x0008C7A0
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0008E5AE File Offset: 0x0008C7AE
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0008E5BC File Offset: 0x0008C7BC
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0008E5C9 File Offset: 0x0008C7C9
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0008E5D9 File Offset: 0x0008C7D9
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0008E5E6 File Offset: 0x0008C7E6
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0008E5F5 File Offset: 0x0008C7F5
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0008E603 File Offset: 0x0008C803
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0008E613 File Offset: 0x0008C813
		public override void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0008E621 File Offset: 0x0008C821
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0008E62E File Offset: 0x0008C82E
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0008E63B File Offset: 0x0008C83B
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0008E648 File Offset: 0x0008C848
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0008E655 File Offset: 0x0008C855
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x0008E662 File Offset: 0x0008C862
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x04001012 RID: 4114
		protected Stream stream;

		// Token: 0x04001013 RID: 4115
		private XmlDownloadManager downloadManager;

		// Token: 0x04001014 RID: 4116
		private string host;
	}
}
