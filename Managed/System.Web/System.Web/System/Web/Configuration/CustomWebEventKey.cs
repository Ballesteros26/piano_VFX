using System;

namespace System.Web.Configuration
{
	// Token: 0x02000563 RID: 1379
	internal class CustomWebEventKey
	{
		// Token: 0x06003B4B RID: 15179 RVA: 0x0009F06D File Offset: 0x0009D26D
		internal CustomWebEventKey(Type eventType, int eventCode)
		{
			this._type = eventType;
			this._eventCode = eventCode;
		}

		// Token: 0x04002014 RID: 8212
		internal Type _type;

		// Token: 0x04002015 RID: 8213
		internal int _eventCode;
	}
}
