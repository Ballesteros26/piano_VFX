using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x0200009F RID: 159
	public class DebugUIHandlerHBox : DebugUIHandlerWidget
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0000F7B3 File Offset: 0x0000D9B3
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
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

		// Token: 0x060003EC RID: 1004 RVA: 0x0000F804 File Offset: 0x0000DA04
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

		// Token: 0x04000201 RID: 513
		private DebugUIHandlerContainer m_Container;
	}
}
