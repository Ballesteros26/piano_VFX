using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000034 RID: 52
	internal class ClientSessionInfo : IDisposable
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		static ClientSessionInfo()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_TLS_SESSION_CACHE_TIMEOUT");
			if (environmentVariable == null)
			{
				ClientSessionInfo.ValidityInterval = 180;
				return;
			}
			try
			{
				ClientSessionInfo.ValidityInterval = int.Parse(environmentVariable);
			}
			catch
			{
				ClientSessionInfo.ValidityInterval = 180;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000E41C File Offset: 0x0000C61C
		public ClientSessionInfo(string hostname, byte[] id)
		{
			this.host = hostname;
			this.sid = id;
			this.KeepAlive();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000E438 File Offset: 0x0000C638
		~ClientSessionInfo()
		{
			this.Dispose(false);
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000E468 File Offset: 0x0000C668
		public string HostName
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000E470 File Offset: 0x0000C670
		public byte[] Id
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000E478 File Offset: 0x0000C678
		public bool Valid
		{
			get
			{
				return this.masterSecret != null && this.validuntil > DateTime.UtcNow;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000E494 File Offset: 0x0000C694
		public void GetContext(Context context)
		{
			this.CheckDisposed();
			if (context.MasterSecret != null)
			{
				this.masterSecret = (byte[])context.MasterSecret.Clone();
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000E4BA File Offset: 0x0000C6BA
		public void SetContext(Context context)
		{
			this.CheckDisposed();
			if (this.masterSecret != null)
			{
				context.MasterSecret = (byte[])this.masterSecret.Clone();
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public void KeepAlive()
		{
			this.CheckDisposed();
			this.validuntil = DateTime.UtcNow.AddSeconds((double)ClientSessionInfo.ValidityInterval);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000E50C File Offset: 0x0000C70C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000E51C File Offset: 0x0000C71C
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.validuntil = DateTime.MinValue;
				this.host = null;
				this.sid = null;
				if (this.masterSecret != null)
				{
					Array.Clear(this.masterSecret, 0, this.masterSecret.Length);
					this.masterSecret = null;
				}
			}
			this.disposed = true;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000E574 File Offset: 0x0000C774
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Cache session information were disposed."));
			}
		}

		// Token: 0x04000135 RID: 309
		private const int DefaultValidityInterval = 180;

		// Token: 0x04000136 RID: 310
		private static readonly int ValidityInterval;

		// Token: 0x04000137 RID: 311
		private bool disposed;

		// Token: 0x04000138 RID: 312
		private DateTime validuntil;

		// Token: 0x04000139 RID: 313
		private string host;

		// Token: 0x0400013A RID: 314
		private byte[] sid;

		// Token: 0x0400013B RID: 315
		private byte[] masterSecret;
	}
}
