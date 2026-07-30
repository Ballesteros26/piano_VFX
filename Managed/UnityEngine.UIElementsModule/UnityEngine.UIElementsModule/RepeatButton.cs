using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E3 RID: 227
	public class RepeatButton : TextElement
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x0001A894 File Offset: 0x00018A94
		public RepeatButton()
		{
			base.AddToClassList(RepeatButton.ussClassName);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001A8AA File Offset: 0x00018AAA
		public RepeatButton(Action clickEvent, long delay, long interval)
			: this()
		{
			this.SetAction(clickEvent, delay, interval);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001A8BE File Offset: 0x00018ABE
		public void SetAction(Action clickEvent, long delay, long interval)
		{
			this.RemoveManipulator(this.m_Clickable);
			this.m_Clickable = new PointerClickable(clickEvent, delay, interval);
			this.AddManipulator(this.m_Clickable);
		}

		// Token: 0x040002FC RID: 764
		private PointerClickable m_Clickable;

		// Token: 0x040002FD RID: 765
		public new static readonly string ussClassName = "unity-repeat-button";

		// Token: 0x020000E4 RID: 228
		public new class UxmlFactory : UxmlFactory<RepeatButton, RepeatButton.UxmlTraits>
		{
		}

		// Token: 0x020000E5 RID: 229
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x0600068B RID: 1675 RVA: 0x0001A900 File Offset: 0x00018B00
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RepeatButton repeatButton = (RepeatButton)ve;
				repeatButton.SetAction(null, this.m_Delay.GetValueFromBag(bag, cc), this.m_Interval.GetValueFromBag(bag, cc));
			}

			// Token: 0x040002FE RID: 766
			private UxmlLongAttributeDescription m_Delay = new UxmlLongAttributeDescription
			{
				name = "delay"
			};

			// Token: 0x040002FF RID: 767
			private UxmlLongAttributeDescription m_Interval = new UxmlLongAttributeDescription
			{
				name = "interval"
			};
		}
	}
}
