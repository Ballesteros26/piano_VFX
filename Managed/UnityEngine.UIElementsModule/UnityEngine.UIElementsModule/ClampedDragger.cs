using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x02000005 RID: 5
	internal class ClampedDragger<T> : PointerClickable where T : IComparable<T>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000009 RID: 9 RVA: 0x000020F8 File Offset: 0x000002F8
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x00002130 File Offset: 0x00000330
		[field: DebuggerBrowsable(0)]
		public event Action dragging;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002165 File Offset: 0x00000365
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000216D File Offset: 0x0000036D
		public ClampedDragger<T>.DragDirection dragDirection { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002176 File Offset: 0x00000376
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000217E File Offset: 0x0000037E
		private BaseSlider<T> slider { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002187 File Offset: 0x00000387
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000218F File Offset: 0x0000038F
		public Vector2 startMousePosition { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002198 File Offset: 0x00000398
		public Vector2 delta
		{
			get
			{
				return base.lastMousePosition - this.startMousePosition;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021AB File Offset: 0x000003AB
		public ClampedDragger(BaseSlider<T> slider, Action clickHandler, Action dragHandler)
			: base(clickHandler, 250L, 30L)
		{
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			this.slider = slider;
			this.dragging += dragHandler;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021D7 File Offset: 0x000003D7
		protected override void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.startMousePosition = localPosition;
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			base.ProcessDownEvent(evt, localPosition, pointerId);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021F4 File Offset: 0x000003F4
		protected override void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			base.ProcessMoveEvent(evt, localPosition);
			bool flag = this.dragDirection == ClampedDragger<T>.DragDirection.None;
			if (flag)
			{
				this.dragDirection = ClampedDragger<T>.DragDirection.Free;
			}
			bool flag2 = this.dragDirection == ClampedDragger<T>.DragDirection.Free;
			if (flag2)
			{
				Action action = this.dragging;
				if (action != null)
				{
					action.Invoke();
				}
			}
		}

		// Token: 0x02000006 RID: 6
		[Flags]
		public enum DragDirection
		{
			// Token: 0x04000009 RID: 9
			None = 0,
			// Token: 0x0400000A RID: 10
			LowToHigh = 1,
			// Token: 0x0400000B RID: 11
			HighToLow = 2,
			// Token: 0x0400000C RID: 12
			Free = 4
		}
	}
}
