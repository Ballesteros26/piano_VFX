using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A3C RID: 2620
	[Serializable]
	internal class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x06006089 RID: 24713 RVA: 0x0013DFB2 File Offset: 0x0013C1B2
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x0013DFCA File Offset: 0x0013C1CA
		public override bool Equals(object obj)
		{
			return obj is ObjectComparer<T>;
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
