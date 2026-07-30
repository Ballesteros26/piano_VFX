using System;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000083 RID: 131
	internal class MethodCallHeaderHandler
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x0000E898 File Offset: 0x0000CA98
		public MethodCallHeaderHandler(string uri)
		{
			this._uri = uri;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000E8A7 File Offset: 0x0000CAA7
		public object HandleHeaders(Header[] headers)
		{
			return this._uri;
		}

		// Token: 0x040004A8 RID: 1192
		private string _uri;
	}
}
