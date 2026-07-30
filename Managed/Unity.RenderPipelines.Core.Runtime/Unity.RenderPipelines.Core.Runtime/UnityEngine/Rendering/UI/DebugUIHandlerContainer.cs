using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000099 RID: 153
	public class DebugUIHandlerContainer : MonoBehaviour
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		internal DebugUIHandlerWidget GetFirstItem()
		{
			if (this.contentHolder.childCount == 0)
			{
				return null;
			}
			List<DebugUIHandlerWidget> activeChildren = this.GetActiveChildren();
			if (activeChildren.Count == 0)
			{
				return null;
			}
			return activeChildren[0];
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000EE14 File Offset: 0x0000D014
		internal DebugUIHandlerWidget GetLastItem()
		{
			if (this.contentHolder.childCount == 0)
			{
				return null;
			}
			List<DebugUIHandlerWidget> activeChildren = this.GetActiveChildren();
			if (activeChildren.Count == 0)
			{
				return null;
			}
			return activeChildren[activeChildren.Count - 1];
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000EE50 File Offset: 0x0000D050
		internal bool IsDirectChild(DebugUIHandlerWidget widget)
		{
			return this.contentHolder.childCount != 0 && this.GetActiveChildren().Count((DebugUIHandlerWidget x) => x == widget) > 0;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000EE94 File Offset: 0x0000D094
		private List<DebugUIHandlerWidget> GetActiveChildren()
		{
			List<DebugUIHandlerWidget> list = new List<DebugUIHandlerWidget>();
			foreach (object obj in this.contentHolder)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeInHierarchy)
				{
					DebugUIHandlerWidget component = transform.GetComponent<DebugUIHandlerWidget>();
					if (component != null)
					{
						list.Add(component);
					}
				}
			}
			return list;
		}

		// Token: 0x040001EE RID: 494
		[SerializeField]
		public RectTransform contentHolder;
	}
}
