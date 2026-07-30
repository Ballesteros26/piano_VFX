using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000A24 RID: 2596
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x06005FD6 RID: 24534 RVA: 0x00002111 File Offset: 0x00000311
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x06005FD7 RID: 24535 RVA: 0x0013B68F File Offset: 0x0013988F
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x06005FD8 RID: 24536 RVA: 0x0013B69F File Offset: 0x0013989F
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x04003055 RID: 12373
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
