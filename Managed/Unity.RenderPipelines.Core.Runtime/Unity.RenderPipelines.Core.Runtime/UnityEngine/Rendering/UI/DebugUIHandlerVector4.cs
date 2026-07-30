using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000AC RID: 172
	public class DebugUIHandlerVector4 : DebugUIHandlerWidget
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x0001094C File Offset: 0x0000EB4C
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector4Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float x)
			{
				this.SetValue(x, true, false, false, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float x)
			{
				this.SetValue(x, false, true, false, false);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.fieldY.nextUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldY);
			this.fieldZ.getter = () => this.m_Field.GetValue().z;
			this.fieldZ.setter = delegate(float x)
			{
				this.SetValue(x, false, false, true, false);
			};
			this.fieldZ.previousUIHandler = this.fieldY;
			this.fieldZ.nextUIHandler = this.fieldW;
			this.SetupSettings(this.fieldZ);
			this.fieldW.getter = () => this.m_Field.GetValue().w;
			this.fieldW.setter = delegate(float x)
			{
				this.SetValue(x, false, false, false, true);
			};
			this.fieldW.previousUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldW);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010ADC File Offset: 0x0000ECDC
		private void SetValue(float v, bool x = false, bool y = false, bool z = false, bool w = false)
		{
			Vector4 value = this.m_Field.GetValue();
			if (x)
			{
				value.x = v;
			}
			if (y)
			{
				value.y = v;
			}
			if (z)
			{
				value.z = v;
			}
			if (w)
			{
				value.w = v;
			}
			this.m_Field.SetValue(value);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00010B30 File Offset: 0x0000ED30
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00010B80 File Offset: 0x0000ED80
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

		// Token: 0x06000452 RID: 1106 RVA: 0x00010BF7 File Offset: 0x0000EDF7
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00010C0A File Offset: 0x0000EE0A
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00010C18 File Offset: 0x0000EE18
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00010C26 File Offset: 0x0000EE26
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00010C44 File Offset: 0x0000EE44
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

		// Token: 0x04000237 RID: 567
		public Text nameLabel;

		// Token: 0x04000238 RID: 568
		public UIFoldout valueToggle;

		// Token: 0x04000239 RID: 569
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x0400023A RID: 570
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x0400023B RID: 571
		public DebugUIHandlerIndirectFloatField fieldZ;

		// Token: 0x0400023C RID: 572
		public DebugUIHandlerIndirectFloatField fieldW;

		// Token: 0x0400023D RID: 573
		private DebugUI.Vector4Field m_Field;

		// Token: 0x0400023E RID: 574
		private DebugUIHandlerContainer m_Container;
	}
}
