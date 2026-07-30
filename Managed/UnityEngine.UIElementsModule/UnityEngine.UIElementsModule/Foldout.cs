using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C4 RID: 196
	public class Foldout : BindableElement, INotifyValueChanged<bool>
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00015910 File Offset: 0x00013B10
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Container;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00015928 File Offset: 0x00013B28
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00015945 File Offset: 0x00013B45
		public string text
		{
			get
			{
				return this.m_Toggle.text;
			}
			set
			{
				this.m_Toggle.text = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00015958 File Offset: 0x00013B58
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00015970 File Offset: 0x00013B70
		public bool value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = this.m_Value == value;
				if (!flag)
				{
					using (ChangeEvent<bool> pooled = ChangeEvent<bool>.GetPooled(this.m_Value, value))
					{
						pooled.target = this;
						this.SetValueWithoutNotify(value);
						this.SendEvent(pooled);
						base.SaveViewData();
					}
				}
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000159D8 File Offset: 0x00013BD8
		public void SetValueWithoutNotify(bool newValue)
		{
			this.m_Value = newValue;
			this.m_Toggle.value = this.m_Value;
			this.contentContainer.style.display = (newValue ? DisplayStyle.Flex : DisplayStyle.None);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00015A14 File Offset: 0x00013C14
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.SetValueWithoutNotify(this.m_Value);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00015A48 File Offset: 0x00013C48
		public Foldout()
		{
			this.m_Value = true;
			base.AddToClassList(Foldout.ussClassName);
			this.m_Toggle = new Toggle
			{
				value = true
			};
			this.m_Toggle.RegisterValueChangedCallback(delegate(ChangeEvent<bool> evt)
			{
				this.value = this.m_Toggle.value;
				evt.StopPropagation();
			});
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			base.hierarchy.Add(this.m_Toggle);
			this.m_Container = new VisualElement
			{
				name = "unity-content"
			};
			this.m_Container.AddToClassList(Foldout.contentUssClassName);
			base.hierarchy.Add(this.m_Container);
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00015B14 File Offset: 0x00013D14
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			int num = 0;
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + i);
			}
			base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + "max");
			bool flag = base.parent != null;
			if (flag)
			{
				for (VisualElement visualElement = base.parent; visualElement != null; visualElement = visualElement.parent)
				{
					bool flag2 = visualElement.GetType() == typeof(Foldout);
					if (flag2)
					{
						num++;
					}
				}
			}
			bool flag3 = num > Foldout.ussFoldoutMaxDepth;
			if (flag3)
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + "max");
			}
			else
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + num);
			}
		}

		// Token: 0x0400026B RID: 619
		internal static readonly string ussFoldoutDepthClassName = "unity-foldout--depth-";

		// Token: 0x0400026C RID: 620
		internal static readonly int ussFoldoutMaxDepth = 4;

		// Token: 0x0400026D RID: 621
		private Toggle m_Toggle;

		// Token: 0x0400026E RID: 622
		private VisualElement m_Container;

		// Token: 0x0400026F RID: 623
		[SerializeField]
		private bool m_Value;

		// Token: 0x04000270 RID: 624
		public static readonly string ussClassName = "unity-foldout";

		// Token: 0x04000271 RID: 625
		public static readonly string toggleUssClassName = Foldout.ussClassName + "__toggle";

		// Token: 0x04000272 RID: 626
		public static readonly string contentUssClassName = Foldout.ussClassName + "__content";

		// Token: 0x020000C5 RID: 197
		public new class UxmlFactory : UxmlFactory<Foldout, Foldout.UxmlTraits>
		{
		}

		// Token: 0x020000C6 RID: 198
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x060005AF RID: 1455 RVA: 0x00015C70 File Offset: 0x00013E70
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Foldout foldout = ve as Foldout;
				bool flag = foldout != null;
				if (flag)
				{
					foldout.text = this.m_Text.GetValueFromBag(bag, cc);
					foldout.SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
				}
			}

			// Token: 0x04000273 RID: 627
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x04000274 RID: 628
			private UxmlBoolAttributeDescription m_Value = new UxmlBoolAttributeDescription
			{
				name = "value",
				defaultValue = true
			};
		}
	}
}
