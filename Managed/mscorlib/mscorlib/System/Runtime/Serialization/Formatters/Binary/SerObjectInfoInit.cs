using System;
using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000735 RID: 1845
	internal sealed class SerObjectInfoInit
	{
		// Token: 0x040028D1 RID: 10449
		internal Hashtable seenBeforeTable = new Hashtable();

		// Token: 0x040028D2 RID: 10450
		internal int objectInfoIdCount = 1;

		// Token: 0x040028D3 RID: 10451
		internal SerStack oiPool = new SerStack("SerObjectInfo Pool");
	}
}
