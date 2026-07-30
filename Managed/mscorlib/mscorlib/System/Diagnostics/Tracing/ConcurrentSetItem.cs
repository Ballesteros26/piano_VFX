using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A99 RID: 2713
	internal abstract class ConcurrentSetItem<KeyType, ItemType> where ItemType : ConcurrentSetItem<KeyType, ItemType>
	{
		// Token: 0x060062C5 RID: 25285
		public abstract int Compare(ItemType other);

		// Token: 0x060062C6 RID: 25286
		public abstract int Compare(KeyType key);
	}
}
