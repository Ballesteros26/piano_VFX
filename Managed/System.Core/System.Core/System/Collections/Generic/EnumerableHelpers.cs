using System;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x0200034B RID: 843
	internal static class EnumerableHelpers
	{
		// Token: 0x060019A4 RID: 6564 RVA: 0x000542F4 File Offset: 0x000524F4
		internal static bool TryGetCount<T>(IEnumerable<T> source, out int count)
		{
			ICollection<T> collection;
			if ((collection = source as ICollection<T>) != null)
			{
				count = collection.Count;
				return true;
			}
			IIListProvider<T> iilistProvider;
			if ((iilistProvider = source as IIListProvider<T>) != null)
			{
				return (count = iilistProvider.GetCount(true)) >= 0;
			}
			count = -1;
			return false;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00054338 File Offset: 0x00052538
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

		// Token: 0x060019A6 RID: 6566 RVA: 0x00054364 File Offset: 0x00052564
		internal static void IterativeCopy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			foreach (T t in source)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000543B4 File Offset: 0x000525B4
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

		// Token: 0x060019A8 RID: 6568 RVA: 0x00054404 File Offset: 0x00052604
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
