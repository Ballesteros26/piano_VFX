using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A38 RID: 2616
	internal sealed class ObjectEqualityComparer : IEqualityComparer
	{
		// Token: 0x06006077 RID: 24695 RVA: 0x00002111 File Offset: 0x00000311
		private ObjectEqualityComparer()
		{
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x0013DDB7 File Offset: 0x0013BFB7
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x0013DDC4 File Offset: 0x0013BFC4
		bool IEqualityComparer.Equals(object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x04003086 RID: 12422
		internal static readonly ObjectEqualityComparer Default = new ObjectEqualityComparer();
	}
}
