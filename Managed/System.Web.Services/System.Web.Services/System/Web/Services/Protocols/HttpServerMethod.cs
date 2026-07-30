using System;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200003C RID: 60
	internal class HttpServerMethod
	{
		// Token: 0x040001F4 RID: 500
		internal string name;

		// Token: 0x040001F5 RID: 501
		internal LogicalMethodInfo methodInfo;

		// Token: 0x040001F6 RID: 502
		internal Type[] readerTypes;

		// Token: 0x040001F7 RID: 503
		internal object[] readerInitializers;

		// Token: 0x040001F8 RID: 504
		internal Type writerType;

		// Token: 0x040001F9 RID: 505
		internal object writerInitializer;
	}
}
