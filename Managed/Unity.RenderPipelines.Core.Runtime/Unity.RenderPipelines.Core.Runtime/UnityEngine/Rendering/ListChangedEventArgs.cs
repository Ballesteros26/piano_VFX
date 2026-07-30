using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002B RID: 43
	public sealed class ListChangedEventArgs<T> : EventArgs
	{
		// Token: 0x060000EF RID: 239 RVA: 0x0000552E File Offset: 0x0000372E
		public ListChangedEventArgs(int index, T item)
		{
			this.index = index;
			this.item = item;
		}

		// Token: 0x040000BE RID: 190
		public readonly int index;

		// Token: 0x040000BF RID: 191
		public readonly T item;
	}
}
