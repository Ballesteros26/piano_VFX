using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000098 RID: 152
	public class DebugUIHandlerColor : DebugUIHandlerWidget
	{
		// Token: 0x060003AE RID: 942 RVA: 0x0000E998 File Offset: 0x0000CB98
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.ColorField>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldR.getter = () => this.m_Field.GetValue().r;
			this.fieldR.setter = delegate(float x)
			{
				this.SetValue(x, true, false, false, false);
			};
			this.fieldR.nextUIHandler = this.fieldG;
			this.SetupSettings(this.fieldR);
			this.fieldG.getter = () => this.m_Field.GetValue().g;
			this.fieldG.setter = delegate(float x)
			{
				this.SetValue(x, false, true, false, false);
			};
			this.fieldG.previousUIHandler = this.fieldR;
			this.fieldG.nextUIHandler = this.fieldB;
			this.SetupSettings(this.fieldG);
			this.fieldB.getter = () => this.m_Field.GetValue().b;
			this.fieldB.setter = delegate(float x)
			{
				this.SetValue(x, false, false, true, false);
			};
			this.fieldB.previousUIHandler = this.fieldG;
			this.fieldB.nextUIHandler = (this.m_Field.showAlpha ? this.fieldA : null);
			this.SetupSettings(this.fieldB);
			this.fieldA.gameObject.SetActive(this.m_Field.showAlpha);
			this.fieldA.getter = () => this.m_Field.GetValue().a;
			this.fieldA.setter = delegate(float x)
			{
				this.SetValue(x, false, false, false, true);
			};
			this.fieldA.previousUIHandler = this.fieldB;
			this.SetupSettings(this.fieldA);
			this.UpdateColor();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000EB5C File Offset: 0x0000CD5C
		private void SetValue(float x, bool r = false, bool g = false, bool b = false, bool a = false)
		{
			Color value = this.m_Field.GetValue();
			if (r)
			{
				value.r = x;
			}
			if (g)
			{
				value.g = x;
			}
			if (b)
			{
				value.b = x;
			}
			if (a)
			{
				value.a = x;
			}
			this.m_Field.SetValue(value);
			this.UpdateColor();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000EC08 File Offset: 0x0000CE08
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

		// Token: 0x060003B2 RID: 946 RVA: 0x0000EC7F File Offset: 0x0000CE7F
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000EC92 File Offset: 0x0000CE92
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000ECAE File Offset: 0x0000CEAE
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000ECC9 File Offset: 0x0000CEC9
		private void UpdateColor()
		{
			if (this.colorImage != null)
			{
				this.colorImage.color = this.m_Field.GetValue();
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
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

		// Token: 0x040001E5 RID: 485
		public Text nameLabel;

		// Token: 0x040001E6 RID: 486
		public UIFoldout valueToggle;

		// Token: 0x040001E7 RID: 487
		public Image colorImage;

		// Token: 0x040001E8 RID: 488
		public DebugUIHandlerIndirectFloatField fieldR;

		// Token: 0x040001E9 RID: 489
		public DebugUIHandlerIndirectFloatField fieldG;

		// Token: 0x040001EA RID: 490
		public DebugUIHandlerIndirectFloatField fieldB;

		// Token: 0x040001EB RID: 491
		public DebugUIHandlerIndirectFloatField fieldA;

		// Token: 0x040001EC RID: 492
		private DebugUI.ColorField m_Field;

		// Token: 0x040001ED RID: 493
		private DebugUIHandlerContainer m_Container;
	}
}
