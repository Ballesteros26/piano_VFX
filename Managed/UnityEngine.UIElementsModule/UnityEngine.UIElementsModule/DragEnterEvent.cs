using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000135 RID: 309
	public class DragEnterEvent : DragAndDropEventBase<DragEnterEvent>
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00022E96 File Offset: 0x00021096
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00022EA7 File Offset: 0x000210A7
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00022EB2 File Offset: 0x000210B2
		public DragEnterEvent()
		{
			this.LocalInit();
		}
	}
}
