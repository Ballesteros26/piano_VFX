using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B9 RID: 185
	public class BaseFieldTraits<TValueType, TValueUxmlAttributeType> : BaseField<TValueType>.UxmlTraits where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x00014990 File Offset: 0x00012B90
		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
			base.Init(ve, bag, cc);
			((INotifyValueChanged<TValueType>)ve).SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000149BB File Offset: 0x00012BBB
		public BaseFieldTraits()
		{
			TValueUxmlAttributeType tvalueUxmlAttributeType = new TValueUxmlAttributeType();
			tvalueUxmlAttributeType.name = "value";
			this.m_Value = tvalueUxmlAttributeType;
			base..ctor();
		}

		// Token: 0x04000251 RID: 593
		private TValueUxmlAttributeType m_Value;
	}
}
