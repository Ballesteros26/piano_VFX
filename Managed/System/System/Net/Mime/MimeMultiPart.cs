using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x020005A1 RID: 1441
	internal class MimeMultiPart : MimeBasePart
	{
		// Token: 0x06002CF3 RID: 11507 RVA: 0x000B1D1B File Offset: 0x000AFF1B
		internal MimeMultiPart(MimeMultiPartType type)
		{
			this.MimeMultiPartType = type;
		}

		// Token: 0x17000988 RID: 2440
		// (set) Token: 0x06002CF4 RID: 11508 RVA: 0x000B1D2A File Offset: 0x000AFF2A
		internal MimeMultiPartType MimeMultiPartType
		{
			set
			{
				if (value > MimeMultiPartType.Related || value < MimeMultiPartType.Mixed)
				{
					throw new NotSupportedException(value.ToString());
				}
				this.SetType(value);
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000B1D4E File Offset: 0x000AFF4E
		private void SetType(MimeMultiPartType type)
		{
			base.ContentType.MediaType = "multipart/" + type.ToString().ToLower(CultureInfo.InvariantCulture);
			base.ContentType.Boundary = this.GetNextBoundary();
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000B1D8D File Offset: 0x000AFF8D
		internal Collection<MimeBasePart> Parts
		{
			get
			{
				if (this.parts == null)
				{
					this.parts = new Collection<MimeBasePart>();
				}
				return this.parts;
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000B1DA8 File Offset: 0x000AFFA8
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			if (mimePartContext.completed)
			{
				throw e;
			}
			try
			{
				mimePartContext.outputStream.Close();
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

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000B1E08 File Offset: 0x000B0008
		internal void MimeWriterCloseCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.MimeWriterCloseCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000B1E54 File Offset: 0x000B0054
		private void MimeWriterCloseCallbackHandler(IAsyncResult result)
		{
			((MimeWriter)((MimeMultiPart.MimePartContext)result.AsyncState).writer).EndClose(result);
			this.Complete(result, null);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000B1E7C File Offset: 0x000B007C
		internal void MimePartSentCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.MimePartSentCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000B1EC8 File Offset: 0x000B00C8
		private void MimePartSentCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext.partsEnumerator.Current.EndSend(result);
			if (mimePartContext.partsEnumerator.MoveNext())
			{
				IAsyncResult asyncResult = mimePartContext.partsEnumerator.Current.BeginSend(mimePartContext.writer, this.mimePartSentCallback, this.allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext.writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000B1F60 File Offset: 0x000B0160
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000B1FAC File Offset: 0x000B01AC
		private void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext.outputStream = mimePartContext.writer.EndGetContentStream(result);
			mimePartContext.writer = new MimeWriter(mimePartContext.outputStream, base.ContentType.Boundary);
			if (mimePartContext.partsEnumerator.MoveNext())
			{
				MimeBasePart mimeBasePart = mimePartContext.partsEnumerator.Current;
				this.mimePartSentCallback = new AsyncCallback(this.MimePartSentCallback);
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext.writer, this.mimePartSentCallback, this.allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext.writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000B2074 File Offset: 0x000B0274
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			this.allowUnicode = allowUnicode;
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult mimePartAsyncResult = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimeMultiPart.MimePartContext mimePartContext = new MimeMultiPart.MimePartContext(writer, mimePartAsyncResult, this.Parts.GetEnumerator());
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return mimePartAsyncResult;
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000B20E0 File Offset: 0x000B02E0
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			Stream contentStream = writer.GetContentStream();
			MimeWriter mimeWriter = new MimeWriter(contentStream, base.ContentType.Boundary);
			foreach (MimeBasePart mimeBasePart in this.Parts)
			{
				mimeBasePart.Send(mimeWriter, allowUnicode);
			}
			mimeWriter.Close();
			contentStream.Close();
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000B2168 File Offset: 0x000B0368
		internal string GetNextBoundary()
		{
			return "--boundary_" + (Interlocked.Increment(ref MimeMultiPart.boundary) - 1).ToString(CultureInfo.InvariantCulture) + "_" + Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x04002523 RID: 9507
		private Collection<MimeBasePart> parts;

		// Token: 0x04002524 RID: 9508
		private static int boundary;

		// Token: 0x04002525 RID: 9509
		private AsyncCallback mimePartSentCallback;

		// Token: 0x04002526 RID: 9510
		private bool allowUnicode;

		// Token: 0x020005A2 RID: 1442
		internal class MimePartContext
		{
			// Token: 0x06002D01 RID: 11521 RVA: 0x000B21B0 File Offset: 0x000B03B0
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result, IEnumerator<MimeBasePart> partsEnumerator)
			{
				this.writer = writer;
				this.result = result;
				this.partsEnumerator = partsEnumerator;
			}

			// Token: 0x04002527 RID: 9511
			internal IEnumerator<MimeBasePart> partsEnumerator;

			// Token: 0x04002528 RID: 9512
			internal Stream outputStream;

			// Token: 0x04002529 RID: 9513
			internal LazyAsyncResult result;

			// Token: 0x0400252A RID: 9514
			internal BaseWriter writer;

			// Token: 0x0400252B RID: 9515
			internal bool completed;

			// Token: 0x0400252C RID: 9516
			internal bool completedSynchronously = true;
		}
	}
}
