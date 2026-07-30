using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A3B RID: 2619
	[Serializable]
	internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
	{
		// Token: 0x06006085 RID: 24709 RVA: 0x0013DF64 File Offset: 0x0013C164
		public override int Compare(T? x, T? y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.value.CompareTo(y.value);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x0013DF9F File Offset: 0x0013C19F
		public override bool Equals(object obj)
		{
			return obj is NullableComparer<T>;
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
