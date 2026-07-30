using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000157 RID: 343
	public abstract class KeyboardEventBase<T> : EventBase<T>, IKeyboardEvent where T : KeyboardEventBase<T>, new()
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000253F1 File Offset: 0x000235F1
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x000253F9 File Offset: 0x000235F9
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00025402 File Offset: 0x00023602
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x0002540A File Offset: 0x0002360A
		public char character { get; protected set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00025413 File Offset: 0x00023613
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0002541B File Offset: 0x0002361B
		public KeyCode keyCode { get; protected set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00025424 File Offset: 0x00023624
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00025444 File Offset: 0x00023644
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00025464 File Offset: 0x00023664
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00025484 File Offset: 0x00023684
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x000254A4 File Offset: 0x000236A4
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool flag2;
				if (flag)
				{
					flag2 = this.commandKey;
				}
				else
				{
					flag2 = this.ctrlKey;
				}
				return flag2;
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000254DD File Offset: 0x000236DD
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000254EE File Offset: 0x000236EE
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
			this.modifiers = EventModifiers.None;
			this.character = '\0';
			this.keyCode = KeyCode.None;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00025514 File Offset: 0x00023714
		public static T GetPooled(char c, KeyCode keyCode, EventModifiers modifiers)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.character = c;
			pooled.keyCode = keyCode;
			return pooled;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00025554 File Offset: 0x00023754
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.character = systemEvent.character;
				pooled.keyCode = systemEvent.keyCode;
			}
			return pooled;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000255BA File Offset: 0x000237BA
		protected KeyboardEventBase()
		{
			this.LocalInit();
		}
	}
}
