using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015A RID: 346
	public class GeometryChangedEvent : EventBase<GeometryChangedEvent>
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x000255E0 File Offset: 0x000237E0
		public static GeometryChangedEvent GetPooled(Rect oldRect, Rect newRect)
		{
			GeometryChangedEvent pooled = EventBase<GeometryChangedEvent>.GetPooled();
			pooled.oldRect = oldRect;
			pooled.newRect = newRect;
			return pooled;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00025609 File Offset: 0x00023809
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002561A File Offset: 0x0002381A
		private void LocalInit()
		{
			this.oldRect = Rect.zero;
			this.newRect = Rect.zero;
			this.layoutPass = 0;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0002563D File Offset: 0x0002383D
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00025645 File Offset: 0x00023845
		public Rect oldRect { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0002564E File Offset: 0x0002384E
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00025656 File Offset: 0x00023856
		public Rect newRect { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002565F File Offset: 0x0002385F
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00025667 File Offset: 0x00023867
		internal int layoutPass { get; set; }

		// Token: 0x060009AC RID: 2476 RVA: 0x00025670 File Offset: 0x00023870
		public GeometryChangedEvent()
		{
			this.LocalInit();
		}
	}
}
