using System;

namespace System.Collections.Generic
{
	// Token: 0x02000714 RID: 1812
	internal static class EnumerableHelpers
	{
		// Token: 0x06003927 RID: 14631 RVA: 0x000D0ECC File Offset: 0x000CF0CC
		internal static void Copy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			ICollection<T> collection;
			if ((collection = source as ICollection<T>) != null)
			{
				collection.CopyTo(array, arrayIndex);
				return;
			}
			EnumerableHelpers.IterativeCopy<T>(source, array, arrayIndex, count);
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000D0EF8 File Offset: 0x000CF0F8
		internal static void IterativeCopy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			foreach (T t in source)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000D0F48 File Offset: 0x000CF148
		internal static T[] ToArray<T>(IEnumerable<T> source)
		{
			ICollection<T> collection;
			if ((collection = source as ICollection<T>) == null)
			{
				LargeArrayBuilder<T> largeArrayBuilder = new LargeArrayBuilder<T>(true);
				largeArrayBuilder.AddRange(source);
				return largeArrayBuilder.ToArray();
			}
			int count = collection.Count;
			if (count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000D0F98 File Offset: 0x000CF198
		internal static T[] ToArray<T>(IEnumerable<T> source, out int length)
		{
			ICollection<T> collection;
			if ((collection = source as ICollection<T>) != null)
			{
				int count = collection.Count;
				if (count != 0)
				{
					T[] array = new T[count];
					collection.CopyTo(array, 0);
					length = count;
					return array;
				}
			}
			else
			{
				using (IEnumerator<T> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T[] array2 = new T[4];
						array2[0] = enumerator.Current;
						int num = 1;
						while (enumerator.MoveNext())
						{
							if (num == array2.Length)
							{
								int num2 = num << 1;
								if (num2 > 2146435071)
								{
									num2 = ((2146435071 <= num) ? (num + 1) : 2146435071);
								}
								Array.Resize<T>(ref array2, num2);
							}
							array2[num++] = enumerator.Current;
						}
						length = num;
						return array2;
					}
				}
			}
			length = 0;
			return Array.Empty<T>();
		}
	}
}
