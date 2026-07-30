using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000AD RID: 173
	public class DebugUIHandlerWidget : MonoBehaviour
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00010D34 File Offset: 0x0000EF34
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00010D3C File Offset: 0x0000EF3C
		public DebugUIHandlerWidget parentUIHandler { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00010D45 File Offset: 0x0000EF45
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00010D4D File Offset: 0x0000EF4D
		public DebugUIHandlerWidget previousUIHandler { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00010D56 File Offset: 0x0000EF56
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00010D5E File Offset: 0x0000EF5E
		public DebugUIHandlerWidget nextUIHandler { get; set; }

		// Token: 0x06000469 RID: 1129 RVA: 0x00002788 File Offset: 0x00000988
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00010D67 File Offset: 0x0000EF67
		internal virtual void SetWidget(DebugUI.Widget widget)
		{
			this.m_Widget = widget;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00010D70 File Offset: 0x0000EF70
		internal DebugUI.Widget GetWidget()
		{
			return this.m_Widget;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00010D78 File Offset: 0x0000EF78
		protected T CastWidget<T>() where T : DebugUI.Widget
		{
			T t = this.m_Widget as T;
			string text = ((this.m_Widget == null) ? "null" : this.m_Widget.GetType().ToString());
			if (t == null)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Can't cast ",
					text,
					" to ",
					typeof(T)
				}));
			}
			return t;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000B492 File Offset: 0x00009692
		public virtual bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			return true;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00002788 File Offset: 0x00000988
		public virtual void OnDeselection()
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00002788 File Offset: 0x00000988
		public virtual void OnAction()
		{
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00002788 File Offset: 0x00000988
		public virtual void OnIncrement(bool fast)
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00002788 File Offset: 0x00000988
		public virtual void OnDecrement(bool fast)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00010DF1 File Offset: 0x0000EFF1
		public virtual DebugUIHandlerWidget Previous()
		{
			if (this.previousUIHandler != null)
			{
				return this.previousUIHandler;
			}
			if (this.parentUIHandler != null)
			{
				return this.parentUIHandler;
			}
			return null;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00010E20 File Offset: 0x0000F020
		public virtual DebugUIHandlerWidget Next()
		{
			if (this.nextUIHandler != null)
			{
				return this.nextUIHandler;
			}
			if (this.parentUIHandler != null)
			{
				DebugUIHandlerWidget debugUIHandlerWidget = this.parentUIHandler;
				while (debugUIHandlerWidget != null)
				{
					DebugUIHandlerWidget nextUIHandler = debugUIHandlerWidget.nextUIHandler;
					if (nextUIHandler != null)
					{
						return nextUIHandler;
					}
					debugUIHandlerWidget = debugUIHandlerWidget.parentUIHandler;
				}
			}
			return null;
		}

		// Token: 0x0400023F RID: 575
		[HideInInspector]
		public Color colorDefault = new Color(0.8f, 0.8f, 0.8f, 1f);

		// Token: 0x04000240 RID: 576
		[HideInInspector]
		public Color colorSelected = new Color(0.25f, 0.65f, 0.8f, 1f);

		// Token: 0x04000244 RID: 580
		protected DebugUI.Widget m_Widget;
	}
}
