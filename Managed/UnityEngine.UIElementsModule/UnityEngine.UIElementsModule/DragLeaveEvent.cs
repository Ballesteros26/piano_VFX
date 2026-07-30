using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000136 RID: 310
	public class DragLeaveEvent : DragAndDropEventBase<DragLeaveEvent>
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x00022EC3 File Offset: 0x000210C3
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00022EA7 File Offset: 0x000210A7
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00022ED4 File Offset: 0x000210D4
		public DragLeaveEvent()
		{
			this.LocalInit();
		}
	}
}
