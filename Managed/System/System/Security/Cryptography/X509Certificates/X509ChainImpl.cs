using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003B5 RID: 949
	internal abstract class X509ChainImpl : IDisposable
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001CE8 RID: 7400
		public abstract bool IsValid { get; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001CE9 RID: 7401
		public abstract IntPtr Handle { get; }

		// Token: 0x06001CEA RID: 7402 RVA: 0x00072585 File Offset: 0x00070785
		protected void ThrowIfContextInvalid()
		{
			if (!this.IsValid)
			{
				throw X509Helper2.GetInvalidChainContextException();
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001CEB RID: 7403
		public abstract X509ChainElementCollection ChainElements { get; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001CEC RID: 7404
		// (set) Token: 0x06001CED RID: 7405
		public abstract X509ChainPolicy ChainPolicy { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001CEE RID: 7406
		public abstract X509ChainStatus[] ChainStatus { get; }

		// Token: 0x06001CEF RID: 7407
		public abstract bool Build(X509Certificate2 certificate);

		// Token: 0x06001CF0 RID: 7408
		public abstract void Reset();

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00072595 File Offset: 0x00070795
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000725A4 File Offset: 0x000707A4
		~X509ChainImpl()
		{
			this.Dispose(false);
		}
	}
}
