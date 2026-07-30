using System;
using System.Collections;

namespace System.Web.Compilation
{
	// Token: 0x02000644 RID: 1604
	internal class OneNullCollection : ICollection, IEnumerable
	{
		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x00008B66 File Offset: 0x00006D66
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x000BCDF8 File Offset: 0x000BAFF8
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException();
			}
			int length = array.Length;
			if (index >= length || index > length - 1)
			{
				throw new ArgumentException();
			}
			array.SetValue(null, index);
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x000BCE46 File Offset: 0x000BB046
		public IEnumerator GetEnumerator()
		{
			yield return null;
			yield break;
		}
	}
}
