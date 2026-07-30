using System;

namespace System.Net
{
	// Token: 0x02000496 RID: 1174
	internal class ProxyScriptChain : ProxyChain
	{
		// Token: 0x060022C9 RID: 8905 RVA: 0x00086A98 File Offset: 0x00084C98
		internal ProxyScriptChain(WebProxy proxy, Uri destination)
			: base(destination)
		{
			this.m_Proxy = proxy;
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x00086AA8 File Offset: 0x00084CA8
		protected override bool GetNextProxy(out Uri proxy)
		{
			if (this.m_CurrentIndex < 0)
			{
				proxy = null;
				return false;
			}
			if (this.m_CurrentIndex == 0)
			{
				this.m_ScriptProxies = this.m_Proxy.GetProxiesAuto(base.Destination, ref this.m_SyncStatus);
			}
			if (this.m_ScriptProxies == null || this.m_CurrentIndex >= this.m_ScriptProxies.Length)
			{
				proxy = this.m_Proxy.GetProxyAutoFailover(base.Destination);
				this.m_CurrentIndex = -1;
				return true;
			}
			Uri[] scriptProxies = this.m_ScriptProxies;
			int currentIndex = this.m_CurrentIndex;
			this.m_CurrentIndex = currentIndex + 1;
			proxy = scriptProxies[currentIndex];
			return true;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x00086B37 File Offset: 0x00084D37
		internal override void Abort()
		{
			this.m_Proxy.AbortGetProxiesAuto(ref this.m_SyncStatus);
		}

		// Token: 0x04001F23 RID: 7971
		private WebProxy m_Proxy;

		// Token: 0x04001F24 RID: 7972
		private Uri[] m_ScriptProxies;

		// Token: 0x04001F25 RID: 7973
		private int m_CurrentIndex;

		// Token: 0x04001F26 RID: 7974
		private int m_SyncStatus;
	}
}
