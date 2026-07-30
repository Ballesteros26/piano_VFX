using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x0200009E RID: 158
	public class DebugUIHandlerGroup : DebugUIHandlerWidget
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Container>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			if (string.IsNullOrEmpty(this.m_Field.displayName))
			{
				this.header.gameObject.SetActive(false);
				return;
			}
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000F738 File Offset: 0x0000D938
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (!fromNext && !this.m_Container.IsDirectChild(previous))
			{
				DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
				DebugManager.instance.ChangeSelection(lastItem, false);
				return true;
			}
			return false;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000F774 File Offset: 0x0000D974
		public override DebugUIHandlerWidget Next()
		{
			if (this.m_Container == null)
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

		// Token: 0x040001FD RID: 509
		public Text nameLabel;

		// Token: 0x040001FE RID: 510
		public Transform header;

		// Token: 0x040001FF RID: 511
		private DebugUI.Container m_Field;

		// Token: 0x04000200 RID: 512
		private DebugUIHandlerContainer m_Container;
	}
}
