using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014E RID: 334
	public class FocusInEvent : FocusEventBase<FocusInEvent>
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00024DB3 File Offset: 0x00022FB3
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00022E08 File Offset: 0x00021008
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00024DC4 File Offset: 0x00022FC4
		public FocusInEvent()
		{
			this.LocalInit();
		}
	}
}
