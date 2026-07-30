using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000189 RID: 393
	public class TooltipEvent : EventBase<TooltipEvent>
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00028766 File Offset: 0x00026966
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0002876E File Offset: 0x0002696E
		public string tooltip { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00028777 File Offset: 0x00026977
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0002877F File Offset: 0x0002697F
		public Rect rect { get; set; }

		// Token: 0x06000ACB RID: 2763 RVA: 0x00028788 File Offset: 0x00026988
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002879C File Offset: 0x0002699C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			this.rect = default(Rect);
			this.tooltip = string.Empty;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000287D0 File Offset: 0x000269D0
		internal static TooltipEvent GetPooled(string tooltip, Rect rect)
		{
			TooltipEvent pooled = EventBase<TooltipEvent>.GetPooled();
			pooled.tooltip = tooltip;
			pooled.rect = rect;
			return pooled;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000287F9 File Offset: 0x000269F9
		public TooltipEvent()
		{
			this.LocalInit();
		}
	}
}
