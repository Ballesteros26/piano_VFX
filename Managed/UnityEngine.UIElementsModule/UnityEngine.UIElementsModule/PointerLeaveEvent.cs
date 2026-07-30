using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000183 RID: 387
	public sealed class PointerLeaveEvent : PointerEventBase<PointerLeaveEvent>
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x000284AD File Offset: 0x000266AD
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00022EA7 File Offset: 0x000210A7
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000284BE File Offset: 0x000266BE
		public PointerLeaveEvent()
		{
			this.LocalInit();
		}
	}
}
