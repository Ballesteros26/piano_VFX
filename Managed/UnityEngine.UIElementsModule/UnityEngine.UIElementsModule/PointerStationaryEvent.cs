using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017E RID: 382
	public sealed class PointerStationaryEvent : PointerEventBase<PointerStationaryEvent>
	{
		// Token: 0x06000AAA RID: 2730 RVA: 0x000282A8 File Offset: 0x000264A8
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000280B0 File Offset: 0x000262B0
		private void LocalInit()
		{
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000282B9 File Offset: 0x000264B9
		public PointerStationaryEvent()
		{
			this.LocalInit();
		}
	}
}
