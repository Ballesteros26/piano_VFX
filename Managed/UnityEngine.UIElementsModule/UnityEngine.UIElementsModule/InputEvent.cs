using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000154 RID: 340
	public class InputEvent : EventBase<InputEvent>
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00025281 File Offset: 0x00023481
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00025289 File Offset: 0x00023489
		public string previousData { get; protected set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00025292 File Offset: 0x00023492
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x0002529A File Offset: 0x0002349A
		public string newData { get; protected set; }

		// Token: 0x06000982 RID: 2434 RVA: 0x000252A3 File Offset: 0x000234A3
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000252B4 File Offset: 0x000234B4
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			this.previousData = null;
			this.newData = null;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000252D0 File Offset: 0x000234D0
		public static InputEvent GetPooled(string previousData, string newData)
		{
			InputEvent pooled = EventBase<InputEvent>.GetPooled();
			pooled.previousData = previousData;
			pooled.newData = newData;
			return pooled;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000252F9 File Offset: 0x000234F9
		public InputEvent()
		{
			this.LocalInit();
		}
	}
}
