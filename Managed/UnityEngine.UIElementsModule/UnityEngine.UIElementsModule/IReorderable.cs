using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011A RID: 282
	internal interface IReorderable<T>
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000860 RID: 2144
		// (set) Token: 0x06000861 RID: 2145
		bool enableReordering { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000862 RID: 2146
		// (set) Token: 0x06000863 RID: 2147
		Action<ItemMoveArgs<T>> onItemMoved { get; set; }
	}
}
