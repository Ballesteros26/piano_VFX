using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000095 RID: 149
	public class DebugUIHandlerButton : DebugUIHandlerWidget
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0000E154 File Offset: 0x0000C354
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Button>();
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000E17F File Offset: 0x0000C37F
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000E193 File Offset: 0x0000C393
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000E1A6 File Offset: 0x0000C3A6
		public override void OnAction()
		{
			if (this.m_Field.action != null)
			{
				this.m_Field.action();
			}
		}

		// Token: 0x040001D9 RID: 473
		public Text nameLabel;

		// Token: 0x040001DA RID: 474
		private DebugUI.Button m_Field;
	}
}
