using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000182 RID: 386
	public sealed class PointerEnterEvent : PointerEventBase<PointerEnterEvent>
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002848B File Offset: 0x0002668B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00022EA7 File Offset: 0x000210A7
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002849C File Offset: 0x0002669C
		public PointerEnterEvent()
		{
			this.LocalInit();
		}
	}
}
