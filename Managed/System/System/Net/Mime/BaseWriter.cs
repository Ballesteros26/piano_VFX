using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;

namespace System.Net.Mime
{
	// Token: 0x02000595 RID: 1429
	internal abstract class BaseWriter
	{
		// Token: 0x06002C72 RID: 11378 RVA: 0x000AF484 File Offset: 0x000AD684
		protected BaseWriter(Stream stream, bool shouldEncodeLeadingDots)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.shouldEncodeLeadingDots = shouldEncodeLeadingDots;
			this.onCloseHandler = new EventHandler(this.OnClose);
			this.bufferBuilder = new BufferBuilder();
			this.lineLength = BaseWriter.DefaultLineLength;
		}

		// Token: 0x06002C73 RID: 11379
		internal abstract void WriteHeaders(NameValueCollection headers, bool allowUnicode);

		// Token: 0x06002C74 RID: 11380 RVA: 0x000AF4DC File Offset: 0x000AD6DC
		internal void WriteHeader(string name, string value, bool allowUnicode)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.isInContent)
			{
				throw new InvalidOperationException(global::SR.GetString("This operation cannot be performed while in content."));
			}
			this.CheckBoundary();
			this.bufferBuilder.Append(name);
			this.bufferBuilder.Append(": ");
			this.WriteAndFold(value, name.Length + 2, allowUnicode);
			this.bufferBuilder.Append(BaseWriter.CRLF);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000AF560 File Offset: 0x000AD760
		private void WriteAndFold(string value, int charsAlreadyOnLine, bool allowUnicode)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < value.Length; i++)
			{
				if (MailBnfHelper.IsFWSAt(value, i))
				{
					i += 2;
					this.bufferBuilder.Append(value, num2, i - num2, allowUnicode);
					num2 = i;
					num = i;
					charsAlreadyOnLine = 0;
				}
				else if (i - num2 > this.lineLength - charsAlreadyOnLine && num != num2)
				{
					this.bufferBuilder.Append(value, num2, num - num2, allowUnicode);
					this.bufferBuilder.Append(BaseWriter.CRLF);
					num2 = num;
					charsAlreadyOnLine = 0;
				}
				else if (value[i] == MailBnfHelper.Space || value[i] == MailBnfHelper.Tab)
				{
					num = i;
				}
			}
			if (value.Length - num2 > 0)
			{
				this.bufferBuilder.Append(value, num2, value.Length - num2, allowUnicode);
			}
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000AF627 File Offset: 0x000AD827
		internal Stream GetContentStream()
		{
			return this.GetContentStream(null);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000AF630 File Offset: 0x000AD830
		private Stream GetContentStream(MultiAsyncResult multiResult)
		{
			if (this.isInContent)
			{
				throw new InvalidOperationException(global::SR.GetString("This operation cannot be performed while in content."));
			}
			this.isInContent = true;
			this.CheckBoundary();
			this.bufferBuilder.Append(BaseWriter.CRLF);
			this.Flush(multiResult);
			ClosableStream closableStream = new ClosableStream(new EightBitStream(this.stream, this.shouldEncodeLeadingDots), this.onCloseHandler);
			this.contentStream = closableStream;
			return closableStream;
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000AF6A0 File Offset: 0x000AD8A0
		internal IAsyncResult BeginGetContentStream(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			Stream stream = this.GetContentStream(multiAsyncResult);
			if (!(multiAsyncResult.Result is Exception))
			{
				multiAsyncResult.Result = stream;
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000AF6DC File Offset: 0x000AD8DC
		internal Stream EndGetContentStream(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return (Stream)obj;
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000AF708 File Offset: 0x000AD908
		protected void Flush(MultiAsyncResult multiResult)
		{
			if (this.bufferBuilder.Length > 0)
			{
				if (multiResult != null)
				{
					multiResult.Enter();
					IAsyncResult asyncResult = this.stream.BeginWrite(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length, BaseWriter.onWrite, multiResult);
					if (asyncResult.CompletedSynchronously)
					{
						this.stream.EndWrite(asyncResult);
						multiResult.Leave();
					}
				}
				else
				{
					this.stream.Write(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length);
				}
				this.bufferBuilder.Reset();
			}
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000AF7A0 File Offset: 0x000AD9A0
		protected static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				BaseWriter baseWriter = (BaseWriter)multiAsyncResult.Context;
				try
				{
					baseWriter.stream.EndWrite(result);
					multiAsyncResult.Leave();
				}
				catch (Exception ex)
				{
					multiAsyncResult.Leave(ex);
				}
			}
		}

		// Token: 0x06002C7C RID: 11388
		internal abstract void Close();

		// Token: 0x06002C7D RID: 11389
		protected abstract void OnClose(object sender, EventArgs args);

		// Token: 0x06002C7E RID: 11390 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void CheckBoundary()
		{
		}

		// Token: 0x040024E1 RID: 9441
		private static int DefaultLineLength = 76;

		// Token: 0x040024E2 RID: 9442
		private static AsyncCallback onWrite = new AsyncCallback(BaseWriter.OnWrite);

		// Token: 0x040024E3 RID: 9443
		protected static byte[] CRLF = new byte[] { 13, 10 };

		// Token: 0x040024E4 RID: 9444
		protected BufferBuilder bufferBuilder;

		// Token: 0x040024E5 RID: 9445
		protected Stream contentStream;

		// Token: 0x040024E6 RID: 9446
		protected bool isInContent;

		// Token: 0x040024E7 RID: 9447
		protected Stream stream;

		// Token: 0x040024E8 RID: 9448
		private int lineLength;

		// Token: 0x040024E9 RID: 9449
		private EventHandler onCloseHandler;

		// Token: 0x040024EA RID: 9450
		private bool shouldEncodeLeadingDots;
	}
}
