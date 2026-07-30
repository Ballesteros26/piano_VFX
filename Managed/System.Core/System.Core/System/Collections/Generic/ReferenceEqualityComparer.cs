using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200034C RID: 844
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x060019A9 RID: 6569 RVA: 0x00002320 File Offset: 0x00000520
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000544EC File Offset: 0x000526EC
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000544FC File Offset: 0x000526FC
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x04000B67 RID: 2919
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
