using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000116 RID: 278
	internal class EmptyCollection : ICollection, IEnumerable, IEnumerator
	{
		// Token: 0x06000DFE RID: 3582 RVA: 0x00002050 File Offset: 0x00000250
		private EmptyCollection()
		{
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0002622C File Offset: 0x0002442C
		internal static EmptyCollection Instance
		{
			get
			{
				return EmptyCollection.s_theEmptyCollection;
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00002058 File Offset: 0x00000258
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00008A69 File Offset: 0x00006C69
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00008B66 File Offset: 0x00006D66
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00002058 File Offset: 0x00000258
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0000393A File Offset: 0x00001B3A
		public void CopyTo(Array array, int index)
		{
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00003BEA File Offset: 0x00001DEA
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00008A69 File Offset: 0x00006C69
		bool IEnumerator.MoveNext()
		{
			return false;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0000393A File Offset: 0x00001B3A
		void IEnumerator.Reset()
		{
		}

		// Token: 0x040011B1 RID: 4529
		private static EmptyCollection s_theEmptyCollection = new EmptyCollection();
	}
}
