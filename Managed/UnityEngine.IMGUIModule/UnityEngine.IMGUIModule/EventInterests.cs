using System;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	internal struct EventInterests
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000036FA File Offset: 0x000018FA
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003702 File Offset: 0x00001902
		public bool wantsMouseMove { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000370B File Offset: 0x0000190B
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00003713 File Offset: 0x00001913
		public bool wantsMouseEnterLeaveWindow { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000371C File Offset: 0x0000191C
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003724 File Offset: 0x00001924
		public bool wantsLessLayoutEvents { get; set; }

		// Token: 0x0600004F RID: 79 RVA: 0x00003730 File Offset: 0x00001930
		public bool WantsEvent(EventType type)
		{
			bool flag;
			if (type != EventType.MouseMove)
			{
				flag = type - EventType.MouseEnterWindow > 1 || this.wantsMouseEnterLeaveWindow;
			}
			else
			{
				flag = this.wantsMouseMove;
			}
			return flag;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003768 File Offset: 0x00001968
		public bool WantsLayoutPass(EventType type)
		{
			bool flag = !this.wantsLessLayoutEvents;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				switch (type)
				{
				case EventType.MouseDown:
				case EventType.MouseUp:
					return this.wantsMouseMove;
				case EventType.MouseMove:
				case EventType.MouseDrag:
				case EventType.ScrollWheel:
					break;
				case EventType.KeyDown:
				case EventType.KeyUp:
					return GUIUtility.textFieldInput;
				case EventType.Repaint:
					return true;
				default:
					if (type - EventType.MouseEnterWindow <= 1)
					{
						return this.wantsMouseEnterLeaveWindow;
					}
					break;
				}
				flag2 = false;
			}
			return flag2;
		}
	}
}
