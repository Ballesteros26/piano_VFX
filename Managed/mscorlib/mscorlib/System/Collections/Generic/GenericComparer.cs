using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A3A RID: 2618
	[Serializable]
	internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x06006081 RID: 24705 RVA: 0x0013DF11 File Offset: 0x0013C111
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
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

		// Token: 0x06006082 RID: 24706 RVA: 0x0013DF3F File Offset: 0x0013C13F
		public override bool Equals(object obj)
		{
			return obj is GenericComparer<T>;
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
