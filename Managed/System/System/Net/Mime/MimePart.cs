using System;
using System.IO;
using System.Net.Mail;

namespace System.Net.Mime
{
	// Token: 0x020005A4 RID: 1444
	internal class MimePart : MimeBasePart, IDisposable
	{
		// Token: 0x06002D02 RID: 11522 RVA: 0x000B21D4 File Offset: 0x000B03D4
		internal MimePart()
		{
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000B21DC File Offset: 0x000B03DC
		public void Dispose()
		{
			if (this.stream != null)
			{
				this.stream.Close();
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x000B21F1 File Offset: 0x000B03F1
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x000B21F9 File Offset: 0x000B03F9
		// (set) Token: 0x06002D06 RID: 11526 RVA: 0x000B2201 File Offset: 0x000B0401
		internal ContentDisposition ContentDisposition
		{
			get
			{
				return this.contentDisposition;
			}
			set
			{
				this.contentDisposition = value;
				if (value == null)
				{
					((HeaderCollection)base.Headers).InternalRemove(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition));
					return;
				}
				this.contentDisposition.PersistIfNeeded((HeaderCollection)base.Headers, true);
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x000B223C File Offset: 0x000B043C
		// (set) Token: 0x06002D08 RID: 11528 RVA: 0x000B229C File Offset: 0x000B049C
		internal TransferEncoding TransferEncoding
		{
			get
			{
				string text = base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)];
				if (text.Equals("base64", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.Base64;
				}
				if (text.Equals("quoted-printable", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.QuotedPrintable;
				}
				if (text.Equals("7bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.SevenBit;
				}
				if (text.Equals("8bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.EightBit;
				}
				return TransferEncoding.Unknown;
			}
			set
			{
				if (value == TransferEncoding.Base64)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "base64";
					return;
				}
				if (value == TransferEncoding.QuotedPrintable)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "quoted-printable";
					return;
				}
				if (value == TransferEncoding.SevenBit)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "7bit";
					return;
				}
				if (value == TransferEncoding.EightBit)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "8bit";
					return;
				}
				throw new NotSupportedException(global::SR.GetString("The MIME transfer encoding '{0}' is not supported.", new object[] { value }));
			}
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000B2334 File Offset: 0x000B0534
		internal void SetContent(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.streamSet)
			{
				this.stream.Close();
				this.stream = null;
				this.streamSet = false;
			}
			this.stream = stream;
			this.streamSet = true;
			this.streamUsedOnce = false;
			this.TransferEncoding = TransferEncoding.Base64;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000B238C File Offset: 0x000B058C
		internal void SetContent(Stream stream, string name, string mimeType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (mimeType != null && mimeType != string.Empty)
			{
				this.contentType = new ContentType(mimeType);
			}
			if (name != null && name != string.Empty)
			{
				base.ContentType.Name = name;
			}
			this.SetContent(stream);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000B23E6 File Offset: 0x000B05E6
		internal void SetContent(Stream stream, ContentType contentType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.contentType = contentType;
			this.SetContent(stream);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000B2404 File Offset: 0x000B0604
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			if (mimePartContext.completed)
			{
				throw e;
			}
			try
			{
				if (mimePartContext.outputStream != null)
				{
					mimePartContext.outputStream.Close();
				}
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext.completed = true;
			mimePartContext.result.InvokeCallback(e);
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000B246C File Offset: 0x000B066C
		internal void ReadCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.ReadCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000B24B8 File Offset: 0x000B06B8
		internal void ReadCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			mimePartContext.bytesLeft = this.Stream.EndRead(result);
			if (mimePartContext.bytesLeft > 0)
			{
				IAsyncResult asyncResult = mimePartContext.outputStream.BeginWrite(mimePartContext.buffer, 0, mimePartContext.bytesLeft, this.writeCallback, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.WriteCallbackHandler(asyncResult);
					return;
				}
			}
			else
			{
				this.Complete(result, null);
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000B2524 File Offset: 0x000B0724
		internal void WriteCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.WriteCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000B2570 File Offset: 0x000B0770
		internal void WriteCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			mimePartContext.outputStream.EndWrite(result);
			IAsyncResult asyncResult = this.Stream.BeginRead(mimePartContext.buffer, 0, mimePartContext.buffer.Length, this.readCallback, mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadCallbackHandler(asyncResult);
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000B25C8 File Offset: 0x000B07C8
		internal Stream GetEncodedStream(Stream stream)
		{
			Stream stream2 = stream;
			if (this.TransferEncoding == TransferEncoding.Base64)
			{
				stream2 = new Base64Stream(stream2, new Base64WriteStateInfo());
			}
			else if (this.TransferEncoding == TransferEncoding.QuotedPrintable)
			{
				stream2 = new QuotedPrintableStream(stream2, true);
			}
			else if (this.TransferEncoding == TransferEncoding.SevenBit || this.TransferEncoding == TransferEncoding.EightBit)
			{
				stream2 = new EightBitStream(stream2);
			}
			return stream2;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000B261C File Offset: 0x000B081C
		internal void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			Stream stream = mimePartContext.writer.EndGetContentStream(result);
			mimePartContext.outputStream = this.GetEncodedStream(stream);
			this.readCallback = new AsyncCallback(this.ReadCallback);
			this.writeCallback = new AsyncCallback(this.WriteCallback);
			IAsyncResult asyncResult = this.Stream.BeginRead(mimePartContext.buffer, 0, mimePartContext.buffer.Length, this.readCallback, mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadCallbackHandler(asyncResult);
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000B26A4 File Offset: 0x000B08A4
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000B26F0 File Offset: 0x000B08F0
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult mimePartAsyncResult = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimePart.MimePartContext mimePartContext = new MimePart.MimePartContext(writer, mimePartAsyncResult);
			this.ResetStream();
			this.streamUsedOnce = true;
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return mimePartAsyncResult;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000B2754 File Offset: 0x000B0954
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			if (this.Stream != null)
			{
				byte[] array = new byte[17408];
				base.PrepareHeaders(allowUnicode);
				writer.WriteHeaders(base.Headers, allowUnicode);
				Stream stream = writer.GetContentStream();
				stream = this.GetEncodedStream(stream);
				this.ResetStream();
				this.streamUsedOnce = true;
				int num;
				while ((num = this.Stream.Read(array, 0, 17408)) > 0)
				{
					stream.Write(array, 0, num);
				}
				stream.Close();
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000B27CC File Offset: 0x000B09CC
		internal void ResetStream()
		{
			if (!this.streamUsedOnce)
			{
				return;
			}
			if (this.Stream.CanSeek)
			{
				this.Stream.Seek(0L, SeekOrigin.Begin);
				this.streamUsedOnce = false;
				return;
			}
			throw new InvalidOperationException(global::SR.GetString("One of the streams has already been used and can't be reset to the origin."));
		}

		// Token: 0x04002533 RID: 9523
		private Stream stream;

		// Token: 0x04002534 RID: 9524
		private bool streamSet;

		// Token: 0x04002535 RID: 9525
		private bool streamUsedOnce;

		// Token: 0x04002536 RID: 9526
		private AsyncCallback readCallback;

		// Token: 0x04002537 RID: 9527
		private AsyncCallback writeCallback;

		// Token: 0x04002538 RID: 9528
		private const int maxBufferSize = 17408;

		// Token: 0x020005A5 RID: 1445
		internal class MimePartContext
		{
			// Token: 0x06002D17 RID: 11543 RVA: 0x000B280A File Offset: 0x000B0A0A
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result)
			{
				this.writer = writer;
				this.result = result;
				this.buffer = new byte[17408];
			}

			// Token: 0x04002539 RID: 9529
			internal Stream outputStream;

			// Token: 0x0400253A RID: 9530
			internal LazyAsyncResult result;

			// Token: 0x0400253B RID: 9531
			internal int bytesLeft;

			// Token: 0x0400253C RID: 9532
			internal BaseWriter writer;

			// Token: 0x0400253D RID: 9533
			internal byte[] buffer;

			// Token: 0x0400253E RID: 9534
			internal bool completed;

			// Token: 0x0400253F RID: 9535
			internal bool completedSynchronously = true;
		}
	}
}
