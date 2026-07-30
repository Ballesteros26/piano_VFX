using System;
using System.IO;

namespace System.Net
{
	// Token: 0x020004C0 RID: 1216
	internal sealed class FileWebStream : FileStream, ICloseEx
	{
		// Token: 0x06002405 RID: 9221 RVA: 0x0008CA3C File Offset: 0x0008AC3C
		public FileWebStream(FileWebRequest request, string path, FileMode mode, FileAccess access, FileShare sharing)
			: base(path, mode, access, sharing)
		{
			this.m_request = request;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0008CA51 File Offset: 0x0008AC51
		public FileWebStream(FileWebRequest request, string path, FileMode mode, FileAccess access, FileShare sharing, int length, bool async)
			: base(path, mode, access, sharing, length, async)
		{
			this.m_request = request;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0008CA6C File Offset: 0x0008AC6C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_request != null)
				{
					this.m_request.UnblockReader();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x0008CAAC File Offset: 0x0008ACAC
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if ((closeState & CloseExState.Abort) != CloseExState.Normal)
			{
				this.SafeFileHandle.Close();
				return;
			}
			this.Close();
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x0008CAC8 File Offset: 0x0008ACC8
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = base.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x0008CB04 File Offset: 0x0008AD04
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				base.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0008CB3C File Offset: 0x0008AD3C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = base.BeginRead(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0008CB7C File Offset: 0x0008AD7C
		public override int EndRead(IAsyncResult ar)
		{
			int num;
			try
			{
				num = base.EndRead(ar);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0008CBB0 File Offset: 0x0008ADB0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = base.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0008CBF0 File Offset: 0x0008ADF0
		public override void EndWrite(IAsyncResult ar)
		{
			try
			{
				base.EndWrite(ar);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0008CC20 File Offset: 0x0008AE20
		private void CheckError()
		{
			if (this.m_request.Aborted)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x04002003 RID: 8195
		private FileWebRequest m_request;
	}
}
