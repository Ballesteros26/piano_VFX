using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A5 RID: 165
	public class DebugUIHandlerToggle : DebugUIHandlerWidget
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000FDF7 File Offset: 0x0000DFF7
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.BoolField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000FE28 File Offset: 0x0000E028
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.checkmarkImage.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000FE4D File Offset: 0x0000E04D
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.checkmarkImage.color = this.colorDefault;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000FE74 File Offset: 0x0000E074
		public override void OnAction()
		{
			bool flag = !this.m_Field.GetValue();
			this.m_Field.SetValue(flag);
			this.UpdateValueLabel();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000FEA2 File Offset: 0x0000E0A2
		protected virtual void UpdateValueLabel()
		{
			if (this.valueToggle != null)
			{
				this.valueToggle.isOn = this.m_Field.GetValue();
			}
		}

		// Token: 0x0400021C RID: 540
		public Text nameLabel;

		// Token: 0x0400021D RID: 541
		public Toggle valueToggle;

		// Token: 0x0400021E RID: 542
		public Image checkmarkImage;

		// Token: 0x0400021F RID: 543
		protected internal DebugUI.BoolField m_Field;
	}
}
