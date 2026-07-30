using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A29 RID: 2601
	internal class ArraySortHelper<T>
	{
		// Token: 0x06005FE7 RID: 24551 RVA: 0x0013B838 File Offset: 0x00139A38
		public static void Sort(T[] keys, int index, int length, IComparer<T> comparer)
		{
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				ArraySortHelper<T>.IntrospectiveSort(keys, index, length, new Comparison<T>(comparer.Compare));
			}
			catch (IndexOutOfRangeException)
			{
				IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(comparer);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
			}
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x0013B898 File Offset: 0x00139A98
		public static int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int num;
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				num = ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
			}
			return num;
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x0013B8DC File Offset: 0x00139ADC
		internal static void Sort(T[] keys, int index, int length, Comparison<T> comparer)
		{
			try
			{
				ArraySortHelper<T>.IntrospectiveSort(keys, index, length, comparer);
			}
			catch (IndexOutOfRangeException)
			{
				IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(comparer);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
			}
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x0013B928 File Offset: 0x00139B28
		internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = comparer.Compare(array[num2], value);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x0013B970 File Offset: 0x00139B70
		private static void SwapIfGreater(T[] keys, Comparison<T> comparer, int a, int b)
		{
			if (a != b && comparer(keys[a], keys[b]) > 0)
			{
				T t = keys[a];
				keys[a] = keys[b];
				keys[b] = t;
			}
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x0013B9B8 File Offset: 0x00139BB8
		private static void Swap(T[] a, int i, int j)
		{
			if (i != j)
			{
				T t = a[i];
				a[i] = a[j];
				a[j] = t;
			}
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x0013B9E7 File Offset: 0x00139BE7
		internal static void IntrospectiveSort(T[] keys, int left, int length, Comparison<T> comparer)
		{
			if (length < 2)
			{
				return;
			}
			ArraySortHelper<T>.IntroSort(keys, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2(keys.Length), comparer);
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x0013BA08 File Offset: 0x00139C08
		private static void IntroSort(T[] keys, int lo, int hi, int depthLimit, Comparison<T> comparer)
		{
			while (hi > lo)
			{
				int num = hi - lo + 1;
				if (num <= 16)
				{
					if (num == 1)
					{
						return;
					}
					if (num == 2)
					{
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
						return;
					}
					if (num == 3)
					{
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi - 1);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, hi - 1, hi);
						return;
					}
					ArraySortHelper<T>.InsertionSort(keys, lo, hi, comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						ArraySortHelper<T>.Heapsort(keys, lo, hi, comparer);
						return;
					}
					depthLimit--;
					int num2 = ArraySortHelper<T>.PickPivotAndPartition(keys, lo, hi, comparer);
					ArraySortHelper<T>.IntroSort(keys, num2 + 1, hi, depthLimit, comparer);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x0013BAA4 File Offset: 0x00139CA4
		private static int PickPivotAndPartition(T[] keys, int lo, int hi, Comparison<T> comparer)
		{
			int num = lo + (hi - lo) / 2;
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, num);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, num, hi);
			T t = keys[num];
			ArraySortHelper<T>.Swap(keys, num, hi - 1);
			int i = lo;
			int num2 = hi - 1;
			while (i < num2)
			{
				while (comparer(keys[++i], t) < 0)
				{
				}
				while (comparer(t, keys[--num2]) < 0)
				{
				}
				if (i >= num2)
				{
					break;
				}
				ArraySortHelper<T>.Swap(keys, i, num2);
			}
			ArraySortHelper<T>.Swap(keys, i, hi - 1);
			return i;
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x0013BB34 File Offset: 0x00139D34
		private static void Heapsort(T[] keys, int lo, int hi, Comparison<T> comparer)
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				ArraySortHelper<T>.DownHeap(keys, i, num, lo, comparer);
			}
			for (int j = num; j > 1; j--)
			{
				ArraySortHelper<T>.Swap(keys, lo, lo + j - 1);
				ArraySortHelper<T>.DownHeap(keys, 1, j - 1, lo, comparer);
			}
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x0013BB84 File Offset: 0x00139D84
		private static void DownHeap(T[] keys, int i, int n, int lo, Comparison<T> comparer)
		{
			T t = keys[lo + i - 1];
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && comparer(keys[lo + num - 1], keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer(t, keys[lo + num - 1]) >= 0)
				{
					break;
				}
				keys[lo + i - 1] = keys[lo + num - 1];
				i = num;
			}
			keys[lo + i - 1] = t;
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x0013BC0C File Offset: 0x00139E0C
		private static void InsertionSort(T[] keys, int lo, int hi, Comparison<T> comparer)
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T t = keys[i + 1];
				while (num >= lo && comparer(t, keys[num]) < 0)
				{
					keys[num + 1] = keys[num];
					num--;
				}
				keys[num + 1] = t;
			}
		}
	}
}
