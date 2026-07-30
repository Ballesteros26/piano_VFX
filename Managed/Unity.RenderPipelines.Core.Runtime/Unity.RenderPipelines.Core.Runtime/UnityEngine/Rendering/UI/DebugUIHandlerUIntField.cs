using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A7 RID: 167
	public class DebugUIHandlerUIntField : DebugUIHandlerWidget
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0001006F File Offset: 0x0000E26F
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.UIntField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000100A0 File Offset: 0x0000E2A0
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000100C5 File Offset: 0x0000E2C5
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000100E9 File Offset: 0x0000E2E9
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000100F3 File Offset: 0x0000E2F3
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00010100 File Offset: 0x0000E300
		private void ChangeValue(bool fast, int multiplier)
		{
			long num = (long)((ulong)this.m_Field.GetValue());
			if (num == 0L && multiplier < 0)
			{
				return;
			}
			num += (long)((ulong)(this.m_Field.incStep * (fast ? this.m_Field.intStepMult : 1U)) * (ulong)((long)multiplier));
			this.m_Field.SetValue((uint)num);
			this.UpdateValueLabel();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001015C File Offset: 0x0000E35C
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString("N0");
			}
		}

		// Token: 0x04000222 RID: 546
		public Text nameLabel;

		// Token: 0x04000223 RID: 547
		public Text valueLabel;

		// Token: 0x04000224 RID: 548
		private DebugUI.UIntField m_Field;
	}
}
