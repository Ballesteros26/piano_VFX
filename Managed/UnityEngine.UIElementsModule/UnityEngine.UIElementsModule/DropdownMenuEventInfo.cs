using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000010 RID: 16
	public class DropdownMenuEventInfo
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000310B File Offset: 0x0000130B
		public EventModifiers modifiers { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003113 File Offset: 0x00001313
		public Vector2 mousePosition { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000311B File Offset: 0x0000131B
		public Vector2 localMousePosition { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003123 File Offset: 0x00001323
		private char character { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000312B File Offset: 0x0000132B
		private KeyCode keyCode { get; }

		// Token: 0x06000058 RID: 88 RVA: 0x00003134 File Offset: 0x00001334
		public DropdownMenuEventInfo(EventBase e)
		{
			IMouseEvent mouseEvent = e as IMouseEvent;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.mousePosition = mouseEvent.mousePosition;
				this.localMousePosition = mouseEvent.localMousePosition;
				this.modifiers = mouseEvent.modifiers;
				this.character = '\0';
				this.keyCode = KeyCode.None;
			}
			else
			{
				IKeyboardEvent keyboardEvent = e as IKeyboardEvent;
				bool flag2 = keyboardEvent != null;
				if (flag2)
				{
					this.character = keyboardEvent.character;
					this.keyCode = keyboardEvent.keyCode;
					this.modifiers = keyboardEvent.modifiers;
					this.mousePosition = Vector2.zero;
					this.localMousePosition = Vector2.zero;
				}
			}
		}
	}
}
