using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000020 RID: 32
	public class FocusController
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003FEC File Offset: 0x000021EC
		public FocusController(IFocusRing focusRing)
		{
			this.focusRing = focusRing;
			this.imguiKeyboardControl = 0;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004010 File Offset: 0x00002210
		private IFocusRing focusRing { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004018 File Offset: 0x00002218
		public Focusable focusedElement
		{
			get
			{
				return this.GetRetargetedFocusedElement(null);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004024 File Offset: 0x00002224
		internal bool IsFocused(Focusable f)
		{
			foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
			{
				bool flag = focusedElement.m_FocusedElement == f;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000408C File Offset: 0x0000228C
		internal Focusable GetRetargetedFocusedElement(VisualElement retargetAgainst)
		{
			VisualElement visualElement = ((retargetAgainst != null) ? retargetAgainst.hierarchy.parent : null);
			bool flag = visualElement == null;
			if (flag)
			{
				bool flag2 = this.m_FocusedElements.Count > 0;
				if (flag2)
				{
					return this.m_FocusedElements[this.m_FocusedElements.Count - 1].m_FocusedElement;
				}
			}
			else
			{
				while (!visualElement.isCompositeRoot && visualElement.hierarchy.parent != null)
				{
					visualElement = visualElement.hierarchy.parent;
				}
				foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
				{
					bool flag3 = focusedElement.m_SubTreeRoot == visualElement;
					if (flag3)
					{
						return focusedElement.m_FocusedElement;
					}
				}
			}
			return null;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004194 File Offset: 0x00002394
		internal Focusable GetLeafFocusedElement()
		{
			bool flag = this.m_FocusedElements.Count > 0;
			Focusable focusable;
			if (flag)
			{
				focusable = this.m_FocusedElements[0].m_FocusedElement;
			}
			else
			{
				focusable = null;
			}
			return focusable;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000041D0 File Offset: 0x000023D0
		internal void SetFocusToLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				this.m_LastFocusedElement.Focus();
			}
			this.m_LastFocusedElement = null;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004214 File Offset: 0x00002414
		internal void BlurLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				Focusable lastFocusedElement = this.m_LastFocusedElement;
				this.m_LastFocusedElement.Blur();
				this.m_LastFocusedElement = lastFocusedElement;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004260 File Offset: 0x00002460
		internal void DoFocusChange(Focusable f)
		{
			this.m_FocusedElements.Clear();
			VisualElement visualElement = f as VisualElement;
			bool flag = !(f is IMGUIContainer);
			if (flag)
			{
				this.m_LastFocusedElement = f;
			}
			while (visualElement != null)
			{
				bool flag2 = visualElement.hierarchy.parent == null || visualElement.isCompositeRoot;
				if (flag2)
				{
					this.m_FocusedElements.Add(new FocusController.FocusedElement
					{
						m_SubTreeRoot = visualElement,
						m_FocusedElement = f
					});
					f = visualElement;
				}
				visualElement = visualElement.hierarchy.parent;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004300 File Offset: 0x00002500
		private void AboutToReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction)
		{
			using (FocusOutEvent pooled = FocusEventBase<FocusOutEvent>.GetPooled(focusable, willGiveFocusTo, direction, this))
			{
				focusable.SendEvent(pooled);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004340 File Offset: 0x00002540
		private void ReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction)
		{
			using (BlurEvent pooled = FocusEventBase<BlurEvent>.GetPooled(focusable, willGiveFocusTo, direction, this))
			{
				focusable.SendEvent(pooled);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004380 File Offset: 0x00002580
		private void AboutToGrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction)
		{
			using (FocusInEvent pooled = FocusEventBase<FocusInEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this))
			{
				focusable.SendEvent(pooled);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000043C0 File Offset: 0x000025C0
		private void GrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction)
		{
			using (FocusEvent pooled = FocusEventBase<FocusEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this))
			{
				focusable.SendEvent(pooled);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004400 File Offset: 0x00002600
		internal void SwitchFocus(Focusable newFocusedElement)
		{
			this.SwitchFocus(newFocusedElement, FocusChangeDirection.unspecified);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004410 File Offset: 0x00002610
		private void SwitchFocus(Focusable newFocusedElement, FocusChangeDirection direction)
		{
			bool flag = this.GetLeafFocusedElement() == newFocusedElement;
			if (!flag)
			{
				Focusable leafFocusedElement = this.GetLeafFocusedElement();
				bool flag2 = newFocusedElement == null || !newFocusedElement.canGrabFocus;
				if (flag2)
				{
					bool flag3 = leafFocusedElement != null;
					if (flag3)
					{
						this.AboutToReleaseFocus(leafFocusedElement, null, direction);
						this.ReleaseFocus(leafFocusedElement, null, direction);
					}
				}
				else
				{
					bool flag4 = newFocusedElement != leafFocusedElement;
					if (flag4)
					{
						VisualElement visualElement = newFocusedElement as VisualElement;
						VisualElement visualElement2 = ((visualElement != null) ? visualElement.RetargetElement(leafFocusedElement as VisualElement) : null);
						VisualElement visualElement3 = leafFocusedElement as VisualElement;
						VisualElement visualElement4 = ((visualElement3 != null) ? visualElement3.RetargetElement(newFocusedElement as VisualElement) : null);
						bool flag5 = leafFocusedElement != null;
						if (flag5)
						{
							this.AboutToReleaseFocus(leafFocusedElement, visualElement2, direction);
						}
						this.AboutToGrabFocus(newFocusedElement, visualElement4, direction);
						bool flag6 = leafFocusedElement != null;
						if (flag6)
						{
							this.ReleaseFocus(leafFocusedElement, visualElement2, direction);
						}
						this.GrabFocus(newFocusedElement, visualElement4, direction);
					}
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000044F8 File Offset: 0x000026F8
		internal Focusable SwitchFocusOnEvent(EventBase e)
		{
			FocusChangeDirection focusChangeDirection = this.focusRing.GetFocusChangeDirection(this.GetLeafFocusedElement(), e);
			bool flag = focusChangeDirection != FocusChangeDirection.none;
			Focusable focusable;
			if (flag)
			{
				Focusable nextFocusable = this.focusRing.GetNextFocusable(this.GetLeafFocusedElement(), focusChangeDirection);
				this.SwitchFocus(nextFocusable, focusChangeDirection);
				focusable = nextFocusable;
			}
			else
			{
				focusable = this.GetLeafFocusedElement();
			}
			return focusable;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004553 File Offset: 0x00002753
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000455B File Offset: 0x0000275B
		internal int imguiKeyboardControl { get; set; }

		// Token: 0x060000B3 RID: 179 RVA: 0x00004564 File Offset: 0x00002764
		internal void SyncIMGUIFocus(int imguiKeyboardControlID, Focusable imguiContainerHavingKeyboardControl, bool forceSwitch)
		{
			this.imguiKeyboardControl = imguiKeyboardControlID;
			bool flag = forceSwitch || this.imguiKeyboardControl != 0;
			if (flag)
			{
				this.SwitchFocus(imguiContainerHavingKeyboardControl, FocusChangeDirection.unspecified);
			}
			else
			{
				this.SwitchFocus(null, FocusChangeDirection.unspecified);
			}
		}

		// Token: 0x0400004F RID: 79
		private List<FocusController.FocusedElement> m_FocusedElements = new List<FocusController.FocusedElement>();

		// Token: 0x04000050 RID: 80
		private Focusable m_LastFocusedElement;

		// Token: 0x02000021 RID: 33
		private struct FocusedElement
		{
			// Token: 0x04000052 RID: 82
			public VisualElement m_SubTreeRoot;

			// Token: 0x04000053 RID: 83
			public Focusable m_FocusedElement;
		}
	}
}
