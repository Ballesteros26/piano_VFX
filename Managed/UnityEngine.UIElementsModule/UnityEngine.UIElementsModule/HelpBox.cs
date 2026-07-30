using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C8 RID: 200
	public class HelpBox : VisualElement
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00015D00 File Offset: 0x00013F00
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00015D1D File Offset: 0x00013F1D
		public string text
		{
			get
			{
				return this.m_Label.text;
			}
			set
			{
				this.m_Label.text = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00015D30 File Offset: 0x00013F30
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00015D48 File Offset: 0x00013F48
		public HelpBoxMessageType messageType
		{
			get
			{
				return this.m_HelpBoxMessageType;
			}
			set
			{
				bool flag = value != this.m_HelpBoxMessageType;
				if (flag)
				{
					this.m_HelpBoxMessageType = value;
					this.UpdateIcon(value);
				}
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00015D77 File Offset: 0x00013F77
		public HelpBox()
			: this(string.Empty, HelpBoxMessageType.None)
		{
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00015D88 File Offset: 0x00013F88
		public HelpBox(string text, HelpBoxMessageType messageType)
		{
			base.AddToClassList(HelpBox.ussClassName);
			this.m_HelpBoxMessageType = messageType;
			this.m_Label = new Label(text);
			this.m_Label.AddToClassList(HelpBox.labelUssClassName);
			base.Add(this.m_Label);
			this.m_Icon = new VisualElement();
			this.m_Icon.AddToClassList(HelpBox.iconUssClassName);
			this.UpdateIcon(messageType);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00015E00 File Offset: 0x00014000
		private string GetIconClass(HelpBoxMessageType messageType)
		{
			string text;
			switch (messageType)
			{
			case HelpBoxMessageType.Info:
				text = HelpBox.iconInfoUssClassName;
				break;
			case HelpBoxMessageType.Warning:
				text = HelpBox.iconwarningUssClassName;
				break;
			case HelpBoxMessageType.Error:
				text = HelpBox.iconErrorUssClassName;
				break;
			default:
				text = null;
				break;
			}
			return text;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00015E44 File Offset: 0x00014044
		private void UpdateIcon(HelpBoxMessageType messageType)
		{
			bool flag = !string.IsNullOrEmpty(this.m_IconClass);
			if (flag)
			{
				this.m_Icon.RemoveFromClassList(this.m_IconClass);
			}
			this.m_IconClass = this.GetIconClass(messageType);
			bool flag2 = this.m_IconClass == null;
			if (flag2)
			{
				this.m_Icon.RemoveFromHierarchy();
			}
			else
			{
				this.m_Icon.AddToClassList(this.m_IconClass);
				bool flag3 = this.m_Icon.parent == null;
				if (flag3)
				{
					base.Insert(0, this.m_Icon);
				}
			}
		}

		// Token: 0x0400027A RID: 634
		public static readonly string ussClassName = "unity-help-box";

		// Token: 0x0400027B RID: 635
		public static readonly string labelUssClassName = HelpBox.ussClassName + "__label";

		// Token: 0x0400027C RID: 636
		public static readonly string iconUssClassName = HelpBox.ussClassName + "__icon";

		// Token: 0x0400027D RID: 637
		public static readonly string iconInfoUssClassName = HelpBox.iconUssClassName + "--info";

		// Token: 0x0400027E RID: 638
		public static readonly string iconwarningUssClassName = HelpBox.iconUssClassName + "--warning";

		// Token: 0x0400027F RID: 639
		public static readonly string iconErrorUssClassName = HelpBox.iconUssClassName + "--error";

		// Token: 0x04000280 RID: 640
		private HelpBoxMessageType m_HelpBoxMessageType;

		// Token: 0x04000281 RID: 641
		private VisualElement m_Icon;

		// Token: 0x04000282 RID: 642
		private string m_IconClass;

		// Token: 0x04000283 RID: 643
		private Label m_Label;

		// Token: 0x020000C9 RID: 201
		public new class UxmlFactory : UxmlFactory<HelpBox, HelpBox.UxmlTraits>
		{
		}

		// Token: 0x020000CA RID: 202
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x060005BB RID: 1467 RVA: 0x00015F5C File Offset: 0x0001415C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				HelpBox helpBox = ve as HelpBox;
				helpBox.text = this.m_Text.GetValueFromBag(bag, cc);
				helpBox.messageType = this.m_MessageType.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000284 RID: 644
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x04000285 RID: 645
			private UxmlEnumAttributeDescription<HelpBoxMessageType> m_MessageType = new UxmlEnumAttributeDescription<HelpBoxMessageType>
			{
				name = "message-type",
				defaultValue = HelpBoxMessageType.None
			};
		}
	}
}
