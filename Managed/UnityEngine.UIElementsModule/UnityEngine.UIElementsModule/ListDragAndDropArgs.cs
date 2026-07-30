using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000117 RID: 279
	internal struct ListDragAndDropArgs : IListDragAndDropArgs
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00021D1A File Offset: 0x0001FF1A
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00021D22 File Offset: 0x0001FF22
		public object target { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00021D2B File Offset: 0x0001FF2B
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00021D33 File Offset: 0x0001FF33
		public int insertAtIndex { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00021D3C File Offset: 0x0001FF3C
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00021D44 File Offset: 0x0001FF44
		public DragAndDropPosition dragAndDropPosition { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00021D50 File Offset: 0x0001FF50
		public IDragAndDropData dragAndDropData
		{
			get
			{
				return DragAndDropUtility.dragAndDrop.data;
			}
		}
	}
}
