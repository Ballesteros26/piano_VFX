using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A44 RID: 2628
	[Serializable]
	internal class GenericEqualityComparer<T> : EqualityComparer<T> where T : IEquatable<T>
	{
		// Token: 0x060060A2 RID: 24738 RVA: 0x0013E3A0 File Offset: 0x0013C5A0
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x0013E3CE File Offset: 0x0013C5CE
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x0013E3E8 File Offset: 0x0013C5E8
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x0013E454 File Offset: 0x0013C654
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x0013E4C2 File Offset: 0x0013C6C2
		public override bool Equals(object obj)
		{
			return obj is GenericEqualityComparer<T>;
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
