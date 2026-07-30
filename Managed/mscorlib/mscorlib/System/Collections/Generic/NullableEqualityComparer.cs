using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A45 RID: 2629
	[Serializable]
	internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
	{
		// Token: 0x060060A9 RID: 24745 RVA: 0x0013E4D5 File Offset: 0x0013C6D5
		public override bool Equals(T? x, T? y)
		{
			if (x != null)
			{
				return y != null && x.value.Equals(y.value);
			}
			return y == null;
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x0013E510 File Offset: 0x0013C710
		public override int GetHashCode(T? obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x0013E520 File Offset: 0x0013C720
		internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x0013E598 File Offset: 0x0013C798
		internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x0013E60F File Offset: 0x0013C80F
		public override bool Equals(object obj)
		{
			return obj is NullableEqualityComparer<T>;
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x0013DF4A File Offset: 0x0013C14A
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
