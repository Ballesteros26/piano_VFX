using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000AD RID: 173
	internal class StyleMatchingContext
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x00013654 File Offset: 0x00011854
		public StyleMatchingContext(Action<VisualElement, MatchResultInfo> processResult)
		{
			this.styleSheetStack = new List<StyleSheet>();
			this.variableContext = StyleVariableContext.none;
			this.currentElement = null;
			this.processResult = processResult;
		}

		// Token: 0x04000222 RID: 546
		public List<StyleSheet> styleSheetStack;

		// Token: 0x04000223 RID: 547
		public StyleVariableContext variableContext;

		// Token: 0x04000224 RID: 548
		public VisualElement currentElement;

		// Token: 0x04000225 RID: 549
		public Action<VisualElement, MatchResultInfo> processResult;
	}
}
