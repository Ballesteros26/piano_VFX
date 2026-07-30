using System;

namespace System.Linq
{
	// Token: 0x020000FA RID: 250
	internal abstract class EnumerableSorter<TElement>
	{
		// Token: 0x060008BE RID: 2238
		internal abstract void ComputeKeys(TElement[] elements, int count);

		// Token: 0x060008BF RID: 2239
		internal abstract int CompareAnyKeys(int index1, int index2);

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C700 File Offset: 0x0001A900
		private int[] ComputeMap(TElement[] elements, int count)
		{
			this.ComputeKeys(elements, count);
			int[] array = new int[count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			return array;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001C730 File Offset: 0x0001A930
		internal int[] Sort(TElement[] elements, int count)
		{
			int[] array = this.ComputeMap(elements, count);
			this.QuickSort(array, 0, count - 1);
			return array;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001C754 File Offset: 0x0001A954
		internal int[] Sort(TElement[] elements, int count, int minIdx, int maxIdx)
		{
			int[] array = this.ComputeMap(elements, count);
			this.PartialQuickSort(array, 0, count - 1, minIdx, maxIdx);
			return array;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001C779 File Offset: 0x0001A979
		internal TElement ElementAt(TElement[] elements, int count, int idx)
		{
			return elements[this.QuickSelect(this.ComputeMap(elements, count), count - 1, idx)];
		}

		// Token: 0x060008C4 RID: 2244
		protected abstract void QuickSort(int[] map, int left, int right);

		// Token: 0x060008C5 RID: 2245
		protected abstract void PartialQuickSort(int[] map, int left, int right, int minIdx, int maxIdx);

		// Token: 0x060008C6 RID: 2246
		protected abstract int QuickSelect(int[] map, int right, int idx);
	}
}
