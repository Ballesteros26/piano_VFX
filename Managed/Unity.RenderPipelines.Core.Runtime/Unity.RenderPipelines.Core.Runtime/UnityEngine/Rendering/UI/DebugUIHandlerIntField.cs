using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A2 RID: 162
	public class DebugUIHandlerIntField : DebugUIHandlerWidget
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000FA0E File Offset: 0x0000DC0E
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.IntField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000FA3F File Offset: 0x0000DC3F
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000FA92 File Offset: 0x0000DC92
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		private void ChangeValue(bool fast, int multiplier)
		{
			int num = this.m_Field.GetValue();
			num += this.m_Field.incStep * (fast ? this.m_Field.intStepMult : 1) * multiplier;
			this.m_Field.SetValue(num);
			this.UpdateValueLabel();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000FAEC File Offset: 0x0000DCEC
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString("N0");
			}
		}

		// Token: 0x0400020F RID: 527
		public Text nameLabel;

		// Token: 0x04000210 RID: 528
		public Text valueLabel;

		// Token: 0x04000211 RID: 529
		private DebugUI.IntField m_Field;
	}
}
