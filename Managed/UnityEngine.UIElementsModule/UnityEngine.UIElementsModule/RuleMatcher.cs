using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000071 RID: 113
	internal struct RuleMatcher
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000A653 File Offset: 0x00008853
		public RuleMatcher(StyleSheet sheet, StyleComplexSelector complexSelector, int styleSheetIndexInStack)
		{
			this.sheet = sheet;
			this.complexSelector = complexSelector;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A664 File Offset: 0x00008864
		public override string ToString()
		{
			return this.complexSelector.ToString();
		}

		// Token: 0x0400015B RID: 347
		public StyleSheet sheet;

		// Token: 0x0400015C RID: 348
		public StyleComplexSelector complexSelector;
	}
}
