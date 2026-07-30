using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000A33 RID: 2611
	internal class DictionaryHashHelpers
	{
		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x0600606A RID: 24682 RVA: 0x0013DC93 File Offset: 0x0013BE93
		internal static ConditionalWeakTable<object, SerializationInfo> SerializationInfoTable { get; } = new ConditionalWeakTable<object, SerializationInfo>();
	}
}
