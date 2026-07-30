using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018F RID: 399
	internal class EventDebuggerEventRecord
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002989E File Offset: 0x00027A9E
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x000298A6 File Offset: 0x00027AA6
		public string eventBaseName { get; private set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x000298AF File Offset: 0x00027AAF
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x000298B7 File Offset: 0x00027AB7
		public long eventTypeId { get; private set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000298C0 File Offset: 0x00027AC0
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x000298C8 File Offset: 0x00027AC8
		public ulong eventId { get; private set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000298D1 File Offset: 0x00027AD1
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x000298D9 File Offset: 0x00027AD9
		private ulong triggerEventId { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000298E2 File Offset: 0x00027AE2
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x000298EA File Offset: 0x00027AEA
		private long timestamp { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000298F3 File Offset: 0x00027AF3
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x000298FB File Offset: 0x00027AFB
		public IEventHandler target { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00029904 File Offset: 0x00027B04
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0002990C File Offset: 0x00027B0C
		private List<IEventHandler> skipElements { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00029915 File Offset: 0x00027B15
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0002991D File Offset: 0x00027B1D
		public bool hasUnderlyingPhysicalEvent { get; private set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00029926 File Offset: 0x00027B26
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0002992E File Offset: 0x00027B2E
		private bool isPropagationStopped { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00029937 File Offset: 0x00027B37
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0002993F File Offset: 0x00027B3F
		private bool isImmediatePropagationStopped { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00029948 File Offset: 0x00027B48
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00029950 File Offset: 0x00027B50
		private bool isDefaultPrevented { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00029959 File Offset: 0x00027B59
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00029961 File Offset: 0x00027B61
		public PropagationPhase propagationPhase { get; private set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002996A File Offset: 0x00027B6A
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00029972 File Offset: 0x00027B72
		private IEventHandler currentTarget { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002997B File Offset: 0x00027B7B
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00029983 File Offset: 0x00027B83
		private bool dispatch { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002998C File Offset: 0x00027B8C
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00029994 File Offset: 0x00027B94
		private Vector2 originalMousePosition { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002999D File Offset: 0x00027B9D
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x000299A5 File Offset: 0x00027BA5
		public EventModifiers modifiers { get; private set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000299AE File Offset: 0x00027BAE
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x000299B6 File Offset: 0x00027BB6
		public Vector2 mousePosition { get; private set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000299BF File Offset: 0x00027BBF
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x000299C7 File Offset: 0x00027BC7
		public int clickCount { get; private set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x000299D0 File Offset: 0x00027BD0
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x000299D8 File Offset: 0x00027BD8
		public int button { get; private set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x000299E1 File Offset: 0x00027BE1
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x000299E9 File Offset: 0x00027BE9
		public int pressedButtons { get; private set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x000299F2 File Offset: 0x00027BF2
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x000299FA File Offset: 0x00027BFA
		public Vector3 delta { get; private set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00029A03 File Offset: 0x00027C03
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00029A0B File Offset: 0x00027C0B
		public char character { get; private set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00029A14 File Offset: 0x00027C14
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00029A1C File Offset: 0x00027C1C
		public KeyCode keyCode { get; private set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00029A25 File Offset: 0x00027C25
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00029A2D File Offset: 0x00027C2D
		public string commandName { get; private set; }

		// Token: 0x06000B23 RID: 2851 RVA: 0x00029A38 File Offset: 0x00027C38
		private void Init(EventBase evt)
		{
			this.eventBaseName = evt.GetType().Name;
			this.eventTypeId = evt.eventTypeId;
			this.eventId = evt.eventId;
			this.triggerEventId = evt.triggerEventId;
			this.timestamp = evt.timestamp;
			this.target = evt.target;
			this.skipElements = evt.skipElements;
			this.isPropagationStopped = evt.isPropagationStopped;
			this.isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
			this.isDefaultPrevented = evt.isDefaultPrevented;
			IMouseEvent mouseEvent = evt as IMouseEvent;
			IMouseEventInternal mouseEventInternal = evt as IMouseEventInternal;
			this.hasUnderlyingPhysicalEvent = mouseEvent != null && mouseEventInternal != null && mouseEventInternal.triggeredByOS;
			this.propagationPhase = evt.propagationPhase;
			this.originalMousePosition = evt.originalMousePosition;
			this.currentTarget = evt.currentTarget;
			this.dispatch = evt.dispatch;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.modifiers = mouseEvent.modifiers;
				this.mousePosition = mouseEvent.mousePosition;
				this.button = mouseEvent.button;
				this.pressedButtons = mouseEvent.pressedButtons;
				this.clickCount = mouseEvent.clickCount;
			}
			IPointerEvent pointerEvent = evt as IPointerEvent;
			IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
			this.hasUnderlyingPhysicalEvent = pointerEvent != null && pointerEventInternal != null && pointerEventInternal.triggeredByOS;
			bool flag2 = pointerEvent != null;
			if (flag2)
			{
				this.modifiers = pointerEvent.modifiers;
				this.mousePosition = pointerEvent.position;
				this.button = pointerEvent.button;
				this.pressedButtons = pointerEvent.pressedButtons;
				this.clickCount = pointerEvent.clickCount;
			}
			IKeyboardEvent keyboardEvent = evt as IKeyboardEvent;
			bool flag3 = keyboardEvent != null;
			if (flag3)
			{
				this.character = keyboardEvent.character;
				this.keyCode = keyboardEvent.keyCode;
			}
			ICommandEvent commandEvent = evt as ICommandEvent;
			bool flag4 = commandEvent != null;
			if (flag4)
			{
				this.commandName = commandEvent.commandName;
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00029C3C File Offset: 0x00027E3C
		public EventDebuggerEventRecord(EventBase evt)
		{
			this.Init(evt);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00029C50 File Offset: 0x00027E50
		public string TimestampString()
		{
			long num = (long)((float)this.timestamp / 1000f * 10000000f);
			return new DateTime(num).ToString("HH:mm:ss.ffffff");
		}
	}
}
