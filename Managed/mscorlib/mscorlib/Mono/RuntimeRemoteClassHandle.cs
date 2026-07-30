using System;

namespace Mono
{
	// Token: 0x02000014 RID: 20
	internal struct RuntimeRemoteClassHandle
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003C6A File Offset: 0x00001E6A
		internal unsafe RuntimeRemoteClassHandle(RuntimeStructs.RemoteClass* value)
		{
			this.value = value;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003C73 File Offset: 0x00001E73
		internal unsafe RuntimeClassHandle ProxyClass
		{
			get
			{
				return new RuntimeClassHandle(this.value->proxy_class);
			}
		}

		// Token: 0x0400037F RID: 895
		private unsafe RuntimeStructs.RemoteClass* value;
	}
}
