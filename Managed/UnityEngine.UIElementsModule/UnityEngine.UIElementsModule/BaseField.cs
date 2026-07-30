using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B7 RID: 183
	public abstract class BaseField<TValueType> : BindableElement, INotifyValueChanged<TValueType>
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000144FC File Offset: 0x000126FC
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00014514 File Offset: 0x00012714
		internal VisualElement visualInput
		{
			get
			{
				return this.m_VisualInput;
			}
			set
			{
				bool flag = this.m_VisualInput != null;
				if (flag)
				{
					bool flag2 = this.m_VisualInput.parent == this;
					if (flag2)
					{
						this.m_VisualInput.RemoveFromHierarchy();
					}
					this.m_VisualInput = null;
				}
				bool flag3 = value != null;
				if (flag3)
				{
					this.m_VisualInput = value;
				}
				else
				{
					this.m_VisualInput = new VisualElement
					{
						pickingMode = PickingMode.Ignore
					};
				}
				this.m_VisualInput.focusable = true;
				this.m_VisualInput.AddToClassList(BaseField<TValueType>.inputUssClassName);
				base.Add(this.m_VisualInput);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x000145AC File Offset: 0x000127AC
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x000145C4 File Offset: 0x000127C4
		protected TValueType rawValue
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000145D0 File Offset: 0x000127D0
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x000145E8 File Offset: 0x000127E8
		public virtual TValueType value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_Value, value);
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(this.m_Value, value))
						{
							pooled.target = this;
							this.SetValueWithoutNotify(value);
							this.SendEvent(pooled);
						}
					}
					else
					{
						this.SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00014670 File Offset: 0x00012870
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00014678 File Offset: 0x00012878
		public Label labelElement { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00014684 File Offset: 0x00012884
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x000146A4 File Offset: 0x000128A4
		public string label
		{
			get
			{
				return this.labelElement.text;
			}
			set
			{
				bool flag = this.labelElement.text != value;
				if (flag)
				{
					this.labelElement.text = value;
					bool flag2 = string.IsNullOrEmpty(this.labelElement.text);
					if (flag2)
					{
						base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						this.labelElement.RemoveFromHierarchy();
					}
					else
					{
						bool flag3 = !base.Contains(this.labelElement);
						if (flag3)
						{
							base.Insert(0, this.labelElement);
							base.RemoveFromClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						}
					}
				}
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00014738 File Offset: 0x00012938
		internal BaseField(string label)
		{
			base.isCompositeRoot = true;
			base.focusable = true;
			base.tabIndex = 0;
			base.excludeFromFocusRing = true;
			base.delegatesFocus = true;
			base.AddToClassList(BaseField<TValueType>.ussClassName);
			this.labelElement = new Label
			{
				focusable = true,
				tabIndex = -1
			};
			this.labelElement.AddToClassList(BaseField<TValueType>.labelUssClassName);
			bool flag = label != null;
			if (flag)
			{
				this.label = label;
			}
			else
			{
				base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
			}
			this.m_VisualInput = null;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000147D7 File Offset: 0x000129D7
		protected BaseField(string label, VisualElement visualInput)
			: this(label)
		{
			this.visualInput = visualInput;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000147EC File Offset: 0x000129EC
		public virtual void SetValueWithoutNotify(TValueType newValue)
		{
			this.m_Value = newValue;
			bool flag = !string.IsNullOrEmpty(base.viewDataKey);
			if (flag)
			{
				base.SaveViewData();
			}
			base.MarkDirtyRepaint();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00014824 File Offset: 0x00012A24
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = this.m_VisualInput != null;
			if (flag)
			{
				string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
				TValueType value = this.m_Value;
				base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
				bool flag2 = !EqualityComparer<TValueType>.Default.Equals(value, this.m_Value);
				if (flag2)
				{
					using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(value, this.m_Value))
					{
						pooled.target = this;
						this.SetValueWithoutNotify(this.m_Value);
						this.SendEvent(pooled);
					}
				}
			}
		}

		// Token: 0x04000248 RID: 584
		public static readonly string ussClassName = "unity-base-field";

		// Token: 0x04000249 RID: 585
		public static readonly string labelUssClassName = BaseField<TValueType>.ussClassName + "__label";

		// Token: 0x0400024A RID: 586
		public static readonly string inputUssClassName = BaseField<TValueType>.ussClassName + "__input";

		// Token: 0x0400024B RID: 587
		public static readonly string noLabelVariantUssClassName = BaseField<TValueType>.ussClassName + "--no-label";

		// Token: 0x0400024C RID: 588
		public static readonly string labelDraggerVariantUssClassName = BaseField<TValueType>.labelUssClassName + "--with-dragger";

		// Token: 0x0400024D RID: 589
		private VisualElement m_VisualInput;

		// Token: 0x0400024E RID: 590
		[SerializeField]
		private TValueType m_Value;

		// Token: 0x020000B8 RID: 184
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000562 RID: 1378 RVA: 0x0001492F File Offset: 0x00012B2F
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			// Token: 0x06000563 RID: 1379 RVA: 0x0001496A File Offset: 0x00012B6A
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((BaseField<TValueType>)ve).label = this.m_Label.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000250 RID: 592
			private UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription
			{
				name = "label"
			};
		}
	}
}
