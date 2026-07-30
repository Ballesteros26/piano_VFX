using System;

namespace System.Net
{
	// Token: 0x02000498 RID: 1176
	internal class StaticProxy : ProxyChain
	{
		// Token: 0x060022CE RID: 8910 RVA: 0x00086B6A File Offset: 0x00084D6A
		internal StaticProxy(Uri destination, Uri proxy)
			: base(destination)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.m_Proxy = proxy;
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00086B8E File Offset: 0x00084D8E
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = this.m_Proxy;
			if (proxy == null)
			{
				return false;
			}
			this.m_Proxy = null;
			return true;
		}

		// Token: 0x04001F28 RID: 7976
		private Uri m_Proxy;
	}
}
