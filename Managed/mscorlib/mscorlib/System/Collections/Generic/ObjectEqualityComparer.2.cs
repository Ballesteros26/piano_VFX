using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A46 RID: 2630
	[Serializable]
	internal class ObjectEqualityComparer<T> : EqualityComparer<T>
	{
		// Token: 0x060060B0 RID: 24752 RVA: 0x0013E622 File Offset: 0x0013C822
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x0013E3CE File Offset: 0x0013C5CE
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x0013E658 File Offset: 0x0013C858
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

		// Token: 0x060060B3 RID: 24755 RVA: 0x0013E6CC File Offset: 0x0013C8CC
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

		// Token: 0x060060B4 RID: 24756 RVA: 0x0013E73F File Offset: 0x0013C93F
		public override bool Equals(object obj)
		{
			return obj is ObjectEqualityComparer<T>;
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
