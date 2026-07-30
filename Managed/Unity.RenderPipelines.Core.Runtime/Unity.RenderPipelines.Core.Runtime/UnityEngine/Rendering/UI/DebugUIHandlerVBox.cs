using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A8 RID: 168
	public class DebugUIHandlerVBox : DebugUIHandlerWidget
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x0001019A File Offset: 0x0000E39A
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000101B0 File Offset: 0x0000E3B0
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

		// Token: 0x06000422 RID: 1058 RVA: 0x000101EC File Offset: 0x0000E3EC
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

		// Token: 0x04000225 RID: 549
		private DebugUIHandlerContainer m_Container;
	}
}
