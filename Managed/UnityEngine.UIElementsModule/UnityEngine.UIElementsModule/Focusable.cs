using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001D RID: 29
	public abstract class Focusable : CallbackEventHandler
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003CB6 File Offset: 0x00001EB6
		protected Focusable()
		{
			this.focusable = true;
			this.tabIndex = 0;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008A RID: 138
		public abstract FocusController focusController { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003CD7 File Offset: 0x00001ED7
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003CDF File Offset: 0x00001EDF
		public bool focusable { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003CE8 File Offset: 0x00001EE8
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public int tabIndex { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003CFC File Offset: 0x00001EFC
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003D14 File Offset: 0x00001F14
		public bool delegatesFocus
		{
			get
			{
				return this.m_DelegatesFocus;
			}
			set
			{
				bool flag = !((VisualElement)this).isCompositeRoot;
				if (flag)
				{
					throw new InvalidOperationException("delegatesFocus should only be set on composite roots.");
				}
				this.m_DelegatesFocus = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003D48 File Offset: 0x00001F48
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003D60 File Offset: 0x00001F60
		internal bool excludeFromFocusRing
		{
			get
			{
				return this.m_ExcludeFromFocusRing;
			}
			set
			{
				bool flag = !((VisualElement)this).isCompositeRoot;
				if (flag)
				{
					throw new InvalidOperationException("excludeFromFocusRing should only be set on composite roots.");
				}
				this.m_ExcludeFromFocusRing = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003D93 File Offset: 0x00001F93
		public virtual bool canGrabFocus
		{
			get
			{
				return this.focusable;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003D9C File Offset: 0x00001F9C
		public virtual void Focus()
		{
			bool flag = this.focusController != null;
			if (flag)
			{
				bool canGrabFocus = this.canGrabFocus;
				if (canGrabFocus)
				{
					Focusable focusDelegate = this.GetFocusDelegate();
					this.focusController.SwitchFocus(focusDelegate);
				}
				else
				{
					this.focusController.SwitchFocus(null);
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003DEC File Offset: 0x00001FEC
		public virtual void Blur()
		{
			bool flag = this.focusController != null;
			if (flag)
			{
				bool flag2 = this.focusController.IsFocused(this);
				if (flag2)
				{
					this.focusController.SwitchFocus(null);
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003E28 File Offset: 0x00002028
		private Focusable GetFocusDelegate()
		{
			Focusable focusable = this;
			while (focusable != null && focusable.delegatesFocus)
			{
				focusable = Focusable.GetFirstFocusableChild(focusable as VisualElement);
			}
			return focusable;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003E60 File Offset: 0x00002060
		private static Focusable GetFirstFocusableChild(VisualElement ve)
		{
			foreach (VisualElement visualElement in ve.hierarchy.Children())
			{
				bool canGrabFocus = visualElement.canGrabFocus;
				if (canGrabFocus)
				{
					return visualElement;
				}
				bool flag = visualElement.hierarchy.parent != null && visualElement == visualElement.hierarchy.parent.contentContainer;
				bool flag2 = !visualElement.isCompositeRoot && !flag;
				if (flag2)
				{
					Focusable firstFocusableChild = Focusable.GetFirstFocusableChild(visualElement);
					bool flag3 = firstFocusableChild != null;
					if (flag3)
					{
						return firstFocusableChild;
					}
				}
			}
			return null;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003F28 File Offset: 0x00002128
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt != null && evt.target == evt.leafTarget;
			if (flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<MouseDownEvent>.TypeId();
				if (flag2)
				{
					this.Focus();
				}
				FocusController focusController = this.focusController;
				if (focusController != null)
				{
					focusController.SwitchFocusOnEvent(evt);
				}
			}
		}

		// Token: 0x04000047 RID: 71
		private bool m_DelegatesFocus;

		// Token: 0x04000048 RID: 72
		private bool m_ExcludeFromFocusRing;

		// Token: 0x04000049 RID: 73
		internal bool isIMGUIContainer = false;
	}
}
