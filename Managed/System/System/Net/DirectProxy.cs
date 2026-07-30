using System;

namespace System.Net
{
	// Token: 0x02000497 RID: 1175
	internal class DirectProxy : ProxyChain
	{
		// Token: 0x060022CC RID: 8908 RVA: 0x00086B4A File Offset: 0x00084D4A
		internal DirectProxy(Uri destination)
			: base(destination)
		{
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x00086B53 File Offset: 0x00084D53
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = null;
			if (this.m_ProxyRetrieved)
			{
				return false;
			}
			this.m_ProxyRetrieved = true;
			return true;
		}

		// Token: 0x04001F27 RID: 7975
		private bool m_ProxyRetrieved;
	}
}
