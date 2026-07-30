using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C1 RID: 193
	public class Button : TextElement
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00015790 File Offset: 0x00013990
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x000157A8 File Offset: 0x000139A8
		public Clickable clickable
		{
			get
			{
				return this.m_Clickable;
			}
			set
			{
				bool flag = this.m_Clickable != null && this.m_Clickable.target == this;
				if (flag)
				{
					this.RemoveManipulator(this.m_Clickable);
				}
				this.m_Clickable = value;
				bool flag2 = this.m_Clickable != null;
				if (flag2)
				{
					this.AddManipulator(this.m_Clickable);
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000599 RID: 1433 RVA: 0x00015805 File Offset: 0x00013A05
		// (remove) Token: 0x0600059A RID: 1434 RVA: 0x00015810 File Offset: 0x00013A10
		[Obsolete("onClick is obsolete. Use clicked instead (UnityUpgradable) -> clicked", true)]
		public event Action onClick
		{
			add
			{
				this.clicked += value;
			}
			remove
			{
				this.clicked -= value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600059B RID: 1435 RVA: 0x0001581C File Offset: 0x00013A1C
		// (remove) Token: 0x0600059C RID: 1436 RVA: 0x00015858 File Offset: 0x00013A58
		public event Action clicked
		{
			add
			{
				bool flag = this.m_Clickable == null;
				if (flag)
				{
					this.clickable = new PointerClickable(value);
				}
				else
				{
					this.m_Clickable.clicked += value;
				}
			}
			remove
			{
				bool flag = this.m_Clickable != null;
				if (flag)
				{
					this.m_Clickable.clicked -= value;
				}
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00015882 File Offset: 0x00013A82
		public Button()
			: this(null)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001588D File Offset: 0x00013A8D
		public Button(Action clickEvent)
		{
			base.AddToClassList(Button.ussClassName);
			this.clickable = new PointerClickable(clickEvent);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000158B0 File Offset: 0x00013AB0
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			string text = this.text;
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = Button.NonEmptyString;
			}
			return base.MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		// Token: 0x04000268 RID: 616
		public new static readonly string ussClassName = "unity-button";

		// Token: 0x04000269 RID: 617
		private Clickable m_Clickable;

		// Token: 0x0400026A RID: 618
		private static readonly string NonEmptyString = " ";

		// Token: 0x020000C2 RID: 194
		public new class UxmlFactory : UxmlFactory<Button, Button.UxmlTraits>
		{
		}

		// Token: 0x020000C3 RID: 195
		public new class UxmlTraits : TextElement.UxmlTraits
		{
		}
	}
}
