using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A4 RID: 164
	internal class DebugUIHandlerPersistentCanvas : MonoBehaviour
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		internal void Toggle(DebugUI.Value widget)
		{
			int num = this.m_Items.FindIndex((DebugUIHandlerValue x) => x.GetWidget() == widget);
			if (num > -1)
			{
				CoreUtils.Destroy(this.m_Items[num].gameObject);
				this.m_Items.RemoveAt(num);
				return;
			}
			GameObject gameObject = Object.Instantiate<RectTransform>(this.valuePrefab, this.panel, false).gameObject;
			gameObject.name = widget.displayName;
			DebugUIHandlerValue component = gameObject.GetComponent<DebugUIHandlerValue>();
			component.SetWidget(widget);
			this.m_Items.Add(component);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000FD78 File Offset: 0x0000DF78
		internal void Clear()
		{
			if (this.m_Items == null)
			{
				return;
			}
			foreach (DebugUIHandlerValue debugUIHandlerValue in this.m_Items)
			{
				CoreUtils.Destroy(debugUIHandlerValue.gameObject);
			}
			this.m_Items.Clear();
		}

		// Token: 0x04000219 RID: 537
		public RectTransform panel;

		// Token: 0x0400021A RID: 538
		public RectTransform valuePrefab;

		// Token: 0x0400021B RID: 539
		private List<DebugUIHandlerValue> m_Items = new List<DebugUIHandlerValue>();
	}
}
