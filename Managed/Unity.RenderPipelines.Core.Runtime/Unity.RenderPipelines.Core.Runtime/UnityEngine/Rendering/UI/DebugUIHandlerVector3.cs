using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000AB RID: 171
	public class DebugUIHandlerVector3 : DebugUIHandlerWidget
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x000105EC File Offset: 0x0000E7EC
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector3Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float v)
			{
				this.SetValue(v, true, false, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float v)
			{
				this.SetValue(v, false, true, false);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.fieldY.nextUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldY);
			this.fieldZ.getter = () => this.m_Field.GetValue().z;
			this.fieldZ.setter = delegate(float v)
			{
				this.SetValue(v, false, false, true);
			};
			this.fieldZ.previousUIHandler = this.fieldY;
			this.SetupSettings(this.fieldZ);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00010720 File Offset: 0x0000E920
		private void SetValue(float v, bool x = false, bool y = false, bool z = false)
		{
			Vector3 value = this.m_Field.GetValue();
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
			this.m_Field.SetValue(value);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00010768 File Offset: 0x0000E968
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000107B8 File Offset: 0x0000E9B8
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

		// Token: 0x0600043F RID: 1087 RVA: 0x0001082F File Offset: 0x0000EA2F
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00010842 File Offset: 0x0000EA42
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00010850 File Offset: 0x0000EA50
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001085E File Offset: 0x0000EA5E
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001087C File Offset: 0x0000EA7C
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

		// Token: 0x04000230 RID: 560
		public Text nameLabel;

		// Token: 0x04000231 RID: 561
		public UIFoldout valueToggle;

		// Token: 0x04000232 RID: 562
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x04000233 RID: 563
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000234 RID: 564
		public DebugUIHandlerIndirectFloatField fieldZ;

		// Token: 0x04000235 RID: 565
		private DebugUI.Vector3Field m_Field;

		// Token: 0x04000236 RID: 566
		private DebugUIHandlerContainer m_Container;
	}
}
