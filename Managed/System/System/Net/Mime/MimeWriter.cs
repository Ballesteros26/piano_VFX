using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020005A6 RID: 1446
	internal class MimeWriter : BaseWriter
	{
		// Token: 0x06002D18 RID: 11544 RVA: 0x000B2837 File Offset: 0x000B0A37
		internal MimeWriter(Stream stream, string boundary)
			: base(stream, false)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			this.boundaryBytes = Encoding.ASCII.GetBytes(boundary);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000B2868 File Offset: 0x000B0A68
		internal override void WriteHeaders(NameValueCollection headers, bool allowUnicode)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string text = (string)obj;
				base.WriteHeader(text, headers[text], allowUnicode);
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000B28D4 File Offset: 0x000B0AD4
		internal IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			this.Close(multiAsyncResult);
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000B28F8 File Offset: 0x000B0AF8
		internal void EndClose(IAsyncResult result)
		{
			MultiAsyncResult.End(result);
			this.stream.Close();
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000B290C File Offset: 0x000B0B0C
		internal override void Close()
		{
			this.Close(null);
			this.stream.Close();
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000B2920 File Offset: 0x000B0B20
		private void Close(MultiAsyncResult multiResult)
		{
			this.bufferBuilder.Append(BaseWriter.CRLF);
			this.bufferBuilder.Append(MimeWriter.DASHDASH);
			this.bufferBuilder.Append(this.boundaryBytes);
			this.bufferBuilder.Append(MimeWriter.DASHDASH);
			this.bufferBuilder.Append(BaseWriter.CRLF);
			base.Flush(multiResult);
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000B2985 File Offset: 0x000B0B85
		protected override void OnClose(object sender, EventArgs args)
		{
			if (this.contentStream != sender)
			{
				return;
			}
			this.contentStream.Flush();
			this.contentStream = null;
			this.writeBoundary = true;
			this.isInContent = false;
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000B29B4 File Offset: 0x000B0BB4
		protected override void CheckBoundary()
		{
			if (this.writeBoundary)
			{
				this.bufferBuilder.Append(BaseWriter.CRLF);
				this.bufferBuilder.Append(MimeWriter.DASHDASH);
				this.bufferBuilder.Append(this.boundaryBytes);
				this.bufferBuilder.Append(BaseWriter.CRLF);
				this.writeBoundary = false;
			}
		}

		// Token: 0x04002540 RID: 9536
		private static byte[] DASHDASH = new byte[] { 45, 45 };

		// Token: 0x04002541 RID: 9537
		private byte[] boundaryBytes;

		// Token: 0x04002542 RID: 9538
		private bool writeBoundary = true;
	}
}
