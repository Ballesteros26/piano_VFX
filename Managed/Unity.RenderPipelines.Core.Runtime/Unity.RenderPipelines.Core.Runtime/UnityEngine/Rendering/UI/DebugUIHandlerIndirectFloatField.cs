using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A0 RID: 160
	public class DebugUIHandlerIndirectFloatField : DebugUIHandlerWidget
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x0000F843 File Offset: 0x0000DA43
		public void Init()
		{
			this.UpdateValueLabel();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000F84B File Offset: 0x0000DA4B
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000F870 File Offset: 0x0000DA70
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F894 File Offset: 0x0000DA94
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1f);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000F8A2 File Offset: 0x0000DAA2
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1f);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		private void ChangeValue(bool fast, float multiplier)
		{
			float num = this.getter();
			num += this.incStepGetter() * (fast ? this.incStepMultGetter() : 1f) * multiplier;
			this.setter(num);
			this.UpdateValueLabel();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F904 File Offset: 0x0000DB04
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.getter().ToString("N" + this.decimalsGetter());
			}
		}

		// Token: 0x04000202 RID: 514
		public Text nameLabel;

		// Token: 0x04000203 RID: 515
		public Text valueLabel;

		// Token: 0x04000204 RID: 516
		public Func<float> getter;

		// Token: 0x04000205 RID: 517
		public Action<float> setter;

		// Token: 0x04000206 RID: 518
		public Func<float> incStepGetter;

		// Token: 0x04000207 RID: 519
		public Func<float> incStepMultGetter;

		// Token: 0x04000208 RID: 520
		public Func<float> decimalsGetter;
	}
}
