using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000267 RID: 615
	internal struct SelectorMatchRecord
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x00050BFA File Offset: 0x0004EDFA
		public SelectorMatchRecord(StyleSheet sheet, int styleSheetIndexInStack)
		{
			this = default(SelectorMatchRecord);
			this.sheet = sheet;
			this.styleSheetIndexInStack = styleSheetIndexInStack;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00050C14 File Offset: 0x0004EE14
		public static int Compare(SelectorMatchRecord a, SelectorMatchRecord b)
		{
			bool flag = a.sheet.isUnityStyleSheet != b.sheet.isUnityStyleSheet;
			int num;
			if (flag)
			{
				num = (a.sheet.isUnityStyleSheet ? (-1) : 1);
			}
			else
			{
				int num2 = a.complexSelector.specificity.CompareTo(b.complexSelector.specificity);
				bool flag2 = num2 == 0;
				if (flag2)
				{
					num2 = a.styleSheetIndexInStack.CompareTo(b.styleSheetIndexInStack);
				}
				bool flag3 = num2 == 0;
				if (flag3)
				{
					num2 = a.complexSelector.orderInStyleSheet.CompareTo(b.complexSelector.orderInStyleSheet);
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x04000912 RID: 2322
		public StyleSheet sheet;

		// Token: 0x04000913 RID: 2323
		public int styleSheetIndexInStack;

		// Token: 0x04000914 RID: 2324
		public StyleComplexSelector complexSelector;
	}
}
