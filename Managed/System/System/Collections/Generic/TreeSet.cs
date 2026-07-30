using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000732 RID: 1842
	[Serializable]
	internal sealed class TreeSet<T> : SortedSet<T>
	{
		// Token: 0x06003A0F RID: 14863 RVA: 0x000D3822 File Offset: 0x000D1A22
		public TreeSet()
		{
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000D382A File Offset: 0x000D1A2A
		public TreeSet(IComparer<T> comparer)
			: base(comparer)
		{
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000D3833 File Offset: 0x000D1A33
		public TreeSet(SerializationInfo siInfo, StreamingContext context)
			: base(siInfo, context)
		{
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000D383D File Offset: 0x000D1A3D
		internal override bool AddIfNotPresent(T item)
		{
			bool flag = base.AddIfNotPresent(item);
			if (!flag)
			{
				throw new ArgumentException(global::SR.Format("An item with the same key has already been added. Key: {0}", item));
			}
			return flag;
		}
	}
}
