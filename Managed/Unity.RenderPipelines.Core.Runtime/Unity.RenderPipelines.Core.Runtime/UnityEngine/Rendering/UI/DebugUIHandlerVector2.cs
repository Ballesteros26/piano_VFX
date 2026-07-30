using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000AA RID: 170
	public class DebugUIHandlerVector2 : DebugUIHandlerWidget
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x00010314 File Offset: 0x0000E514
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector2Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float x)
			{
				this.SetValue(x, true, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float x)
			{
				this.SetValue(x, false, true);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.SetupSettings(this.fieldY);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000103EC File Offset: 0x0000E5EC
		private void SetValue(float v, bool x = false, bool y = false)
		{
			Vector2 value = this.m_Field.GetValue();
			if (x)
			{
				value.x = v;
			}
			if (y)
			{
				value.y = v;
			}
			this.m_Field.SetValue(value);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00010428 File Offset: 0x0000E628
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00010478 File Offset: 0x0000E678
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000104EF File Offset: 0x0000E6EF
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00010502 File Offset: 0x0000E702
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00010510 File Offset: 0x0000E710
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001051E File Offset: 0x0000E71E
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001053C File Offset: 0x0000E73C
		public override DebugUIHandlerWidget Next()
		{
			if (!this.valueToggle.isOn || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x0400022A RID: 554
		public Text nameLabel;

		// Token: 0x0400022B RID: 555
		public UIFoldout valueToggle;

		// Token: 0x0400022C RID: 556
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x0400022D RID: 557
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x0400022E RID: 558
		private DebugUI.Vector2Field m_Field;

		// Token: 0x0400022F RID: 559
		private DebugUIHandlerContainer m_Container;
	}
}
