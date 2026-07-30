using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200074A RID: 1866
	[Serializable]
	internal class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x06004D34 RID: 19764 RVA: 0x00116C65 File Offset: 0x00114E65
		public EnvoyInfo(IMessageSink sinks)
		{
			this.envoySinks = sinks;
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004D35 RID: 19765 RVA: 0x00116C74 File Offset: 0x00114E74
		// (set) Token: 0x06004D36 RID: 19766 RVA: 0x00116C7C File Offset: 0x00114E7C
		public IMessageSink EnvoySinks
		{
			get
			{
				return this.envoySinks;
			}
			set
			{
				this.envoySinks = value;
			}
		}

		// Token: 0x04002990 RID: 10640
		private IMessageSink envoySinks;
	}
}
