using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x0200009C RID: 156
	public class DebugUIHandlerFloatField : DebugUIHandlerWidget
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.FloatField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000F31D File Offset: 0x0000D51D
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000F342 File Offset: 0x0000D542
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000F366 File Offset: 0x0000D566
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1f);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000F374 File Offset: 0x0000D574
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1f);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000F384 File Offset: 0x0000D584
		private void ChangeValue(bool fast, float multiplier)
		{
			float num = this.m_Field.GetValue();
			num += this.m_Field.incStep * (fast ? this.m_Field.incStepMult : 1f) * multiplier;
			this.m_Field.SetValue(num);
			this.UpdateValueLabel();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
		private void UpdateValueLabel()
		{
			this.valueLabel.text = this.m_Field.GetValue().ToString("N" + this.m_Field.decimals);
		}

		// Token: 0x040001F4 RID: 500
		public Text nameLabel;

		// Token: 0x040001F5 RID: 501
		public Text valueLabel;

		// Token: 0x040001F6 RID: 502
		private DebugUI.FloatField m_Field;
	}
}
