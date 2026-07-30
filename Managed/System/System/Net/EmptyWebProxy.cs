using System;

namespace System.Net
{
	// Token: 0x020004A8 RID: 1192
	[Serializable]
	internal sealed class EmptyWebProxy : IAutoWebProxy, IWebProxy
	{
		// Token: 0x0600231A RID: 8986 RVA: 0x0000206B File Offset: 0x0000026B
		public Uri GetProxy(Uri uri)
		{
			return uri;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000027E2 File Offset: 0x000009E2
		public bool IsBypassed(Uri uri)
		{
			return true;
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00087EF8 File Offset: 0x000860F8
		// (set) Token: 0x0600231D RID: 8989 RVA: 0x00087F00 File Offset: 0x00086100
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00087F09 File Offset: 0x00086109
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			return new DirectProxy(destination);
		}

		// Token: 0x04001F58 RID: 8024
		[NonSerialized]
		private ICredentials m_credentials;
	}
}
