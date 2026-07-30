using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000112 RID: 274
	internal interface IDragAndDropData
	{
		// Token: 0x06000845 RID: 2117
		object GetGenericData(string key);

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000846 RID: 2118
		object userData { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000847 RID: 2119
		IEnumerable<Object> unityObjectReferences { get; }
	}
}
