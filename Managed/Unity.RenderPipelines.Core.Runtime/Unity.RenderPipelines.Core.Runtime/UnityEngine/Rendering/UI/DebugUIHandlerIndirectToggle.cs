using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A1 RID: 161
	public class DebugUIHandlerIndirectToggle : DebugUIHandlerWidget
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000F957 File Offset: 0x0000DB57
		public void Init()
		{
			this.UpdateValueLabel();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000F95F File Offset: 0x0000DB5F
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.checkmarkImage.color = this.colorSelected;
			return true;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000F984 File Offset: 0x0000DB84
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.checkmarkImage.color = this.colorDefault;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		public override void OnAction()
		{
			bool flag = !this.getter(this.index);
			this.setter(this.index, flag);
			this.UpdateValueLabel();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000F9E2 File Offset: 0x0000DBE2
		internal void UpdateValueLabel()
		{
			if (this.valueToggle != null)
			{
				this.valueToggle.isOn = this.getter(this.index);
			}
		}

		// Token: 0x04000209 RID: 521
		public Text nameLabel;

		// Token: 0x0400020A RID: 522
		public Toggle valueToggle;

		// Token: 0x0400020B RID: 523
		public Image checkmarkImage;

		// Token: 0x0400020C RID: 524
		public Func<int, bool> getter;

		// Token: 0x0400020D RID: 525
		public Action<int, bool> setter;

		// Token: 0x0400020E RID: 526
		internal int index;
	}
}
