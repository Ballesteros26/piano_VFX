using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x0200033B RID: 827
	internal static class CollectionExtensions
	{
		// Token: 0x0600190C RID: 6412 RVA: 0x0005265C File Offset: 0x0005085C
		public static TrueReadOnlyCollection<T> AddFirst<T>(this ReadOnlyCollection<T> list, T item)
		{
			T[] array = new T[list.Count + 1];
			array[0] = item;
			list.CopyTo(array, 1);
			return new TrueReadOnlyCollection<T>(array);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00052690 File Offset: 0x00050890
		public static T[] AddFirst<T>(this T[] array, T item)
		{
			T[] array2 = new T[array.Length + 1];
			array2[0] = item;
			array.CopyTo(array2, 1);
			return array2;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000526BC File Offset: 0x000508BC
		public static T[] AddLast<T>(this T[] array, T item)
		{
			T[] array2 = new T[array.Length + 1];
			array.CopyTo(array2, 0);
			array2[array.Length] = item;
			return array2;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000526E8 File Offset: 0x000508E8
		public static T[] RemoveFirst<T>(this T[] array)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, 1, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00052710 File Offset: 0x00050910
		public static T[] RemoveLast<T>(this T[] array)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, 0, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00052738 File Offset: 0x00050938
		public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return EmptyReadOnlyCollection<T>.Instance;
			}
			TrueReadOnlyCollection<T> trueReadOnlyCollection = enumerable as TrueReadOnlyCollection<T>;
			if (trueReadOnlyCollection != null)
			{
				return trueReadOnlyCollection;
			}
			ReadOnlyCollectionBuilder<T> readOnlyCollectionBuilder = enumerable as ReadOnlyCollectionBuilder<T>;
			if (readOnlyCollectionBuilder != null)
			{
				return readOnlyCollectionBuilder.ToReadOnlyCollection();
			}
			T[] array = EnumerableHelpers.ToArray<T>(enumerable);
			if (array.Length != 0)
			{
				return new TrueReadOnlyCollection<T>(array);
			}
			return EmptyReadOnlyCollection<T>.Instance;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00052784 File Offset: 0x00050984
		public static int ListHashCode<T>(this ReadOnlyCollection<T> list)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			int num = 6551;
			foreach (T t in list)
			{
				num ^= (num << 5) ^ @default.GetHashCode(t);
			}
			return num;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000527E0 File Offset: 0x000509E0
		public static bool ListEquals<T>(this ReadOnlyCollection<T> first, ReadOnlyCollection<T> second)
		{
			if (first == second)
			{
				return true;
			}
			int count = first.Count;
			if (count != second.Count)
			{
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int num = 0; num != count; num++)
			{
				if (!@default.Equals(first[num], second[num]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
