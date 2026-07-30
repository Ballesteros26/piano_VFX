using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020003B0 RID: 944
	internal class X509CertificateImplCollection : IDisposable
	{
		// Token: 0x06001CB0 RID: 7344 RVA: 0x00071EBD File Offset: 0x000700BD
		public X509CertificateImplCollection()
		{
			this.list = new List<X509CertificateImpl>();
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00071ED0 File Offset: 0x000700D0
		private X509CertificateImplCollection(X509CertificateImplCollection other)
		{
			this.list = new List<X509CertificateImpl>();
			foreach (X509CertificateImpl x509CertificateImpl in other.list)
			{
				this.list.Add(x509CertificateImpl.Clone());
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00071F40 File Offset: 0x00070140
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170005DF RID: 1503
		public X509CertificateImpl this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00071F5B File Offset: 0x0007015B
		public void Add(X509CertificateImpl impl, bool takeOwnership)
		{
			if (!takeOwnership)
			{
				impl = impl.Clone();
			}
			this.list.Add(impl);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00071F74 File Offset: 0x00070174
		public X509CertificateImplCollection Clone()
		{
			return new X509CertificateImplCollection(this);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00071F7C File Offset: 0x0007017C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00071F8C File Offset: 0x0007018C
		protected virtual void Dispose(bool disposing)
		{
			foreach (X509CertificateImpl x509CertificateImpl in this.list)
			{
				try
				{
					x509CertificateImpl.Dispose();
				}
				catch
				{
				}
			}
			this.list.Clear();
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00071FFC File Offset: 0x000701FC
		~X509CertificateImplCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x040019A1 RID: 6561
		private List<X509CertificateImpl> list;
	}
}
