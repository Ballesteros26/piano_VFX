using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002E RID: 46
	public struct ManipulatorActivationFilter : IEquatable<ManipulatorActivationFilter>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000592C File Offset: 0x00003B2C
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00005934 File Offset: 0x00003B34
		public MouseButton button { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000593D File Offset: 0x00003B3D
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00005945 File Offset: 0x00003B45
		public EventModifiers modifiers { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000594E File Offset: 0x00003B4E
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00005956 File Offset: 0x00003B56
		public int clickCount { get; set; }

		// Token: 0x06000106 RID: 262 RVA: 0x00005960 File Offset: 0x00003B60
		public override bool Equals(object obj)
		{
			return obj is ManipulatorActivationFilter && this.Equals((ManipulatorActivationFilter)obj);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000598C File Offset: 0x00003B8C
		public bool Equals(ManipulatorActivationFilter other)
		{
			return this.button == other.button && this.modifiers == other.modifiers && this.clickCount == other.clickCount;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000059D0 File Offset: 0x00003BD0
		public override int GetHashCode()
		{
			int num = 390957112;
			num = num * -1521134295 + this.button.GetHashCode();
			num = num * -1521134295 + this.modifiers.GetHashCode();
			return num * -1521134295 + this.clickCount.GetHashCode();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005A3C File Offset: 0x00003C3C
		public bool Matches(IMouseEvent e)
		{
			bool flag = e == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				flag2 = this.button == (MouseButton)e.button && this.HasModifiers(e) && flag3;
			}
			return flag2;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005A94 File Offset: 0x00003C94
		private bool HasModifiers(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public bool Matches(IPointerEvent e)
		{
			bool flag = e == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				flag2 = this.button == (MouseButton)e.button && this.HasModifiers(e) && flag3;
			}
			return flag2;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005B28 File Offset: 0x00003D28
		private bool HasModifiers(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005B64 File Offset: 0x00003D64
		private bool MatchModifiers(bool alt, bool ctrl, bool shift, bool command)
		{
			bool flag = ((this.modifiers & EventModifiers.Alt) != EventModifiers.None && !alt) || ((this.modifiers & EventModifiers.Alt) == EventModifiers.None && alt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = ((this.modifiers & EventModifiers.Control) != EventModifiers.None && !ctrl) || ((this.modifiers & EventModifiers.Control) == EventModifiers.None && ctrl);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = ((this.modifiers & EventModifiers.Shift) != EventModifiers.None && !shift) || ((this.modifiers & EventModifiers.Shift) == EventModifiers.None && shift);
					flag2 = !flag4 && ((this.modifiers & EventModifiers.Command) == EventModifiers.None || command) && ((this.modifiers & EventModifiers.Command) != EventModifiers.None || !command);
				}
			}
			return flag2;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005C10 File Offset: 0x00003E10
		public static bool operator ==(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return filter1.Equals(filter2);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005C2C File Offset: 0x00003E2C
		public static bool operator !=(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return !(filter1 == filter2);
		}
	}
}
