using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	internal class StyleComplexSelector
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00035B74 File Offset: 0x00033D74
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x00035B8C File Offset: 0x00033D8C
		public int specificity
		{
			get
			{
				return this.m_Specificity;
			}
			internal set
			{
				this.m_Specificity = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00035B96 File Offset: 0x00033D96
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x00035B9E File Offset: 0x00033D9E
		public StyleRule rule { get; internal set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x00035BA8 File Offset: 0x00033DA8
		public bool isSimple
		{
			get
			{
				return this.selectors.Length == 1;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00035BC8 File Offset: 0x00033DC8
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00035BE0 File Offset: 0x00033DE0
		public StyleSelector[] selectors
		{
			get
			{
				return this.m_Selectors;
			}
			internal set
			{
				this.m_Selectors = value;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00035BEC File Offset: 0x00033DEC
		internal void CachePseudoStateMasks()
		{
			bool flag = StyleComplexSelector.s_PseudoStates == null;
			if (flag)
			{
				StyleComplexSelector.s_PseudoStates = new Dictionary<string, StyleComplexSelector.PseudoStateData>();
				StyleComplexSelector.s_PseudoStates["active"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, false);
				StyleComplexSelector.s_PseudoStates["hover"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Hover, false);
				StyleComplexSelector.s_PseudoStates["checked"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["selected"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Checked, false);
				StyleComplexSelector.s_PseudoStates["disabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, false);
				StyleComplexSelector.s_PseudoStates["focus"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Focus, false);
				StyleComplexSelector.s_PseudoStates["root"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Root, false);
				StyleComplexSelector.s_PseudoStates["inactive"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Active, true);
				StyleComplexSelector.s_PseudoStates["enabled"] = new StyleComplexSelector.PseudoStateData(PseudoStates.Disabled, true);
			}
			int i = 0;
			int num = this.selectors.Length;
			while (i < num)
			{
				StyleSelector styleSelector = this.selectors[i];
				StyleSelectorPart[] parts = styleSelector.parts;
				PseudoStates pseudoStates = (PseudoStates)0;
				PseudoStates pseudoStates2 = (PseudoStates)0;
				for (int j = 0; j < styleSelector.parts.Length; j++)
				{
					bool flag2 = styleSelector.parts[j].type == StyleSelectorType.PseudoClass;
					if (flag2)
					{
						StyleComplexSelector.PseudoStateData pseudoStateData;
						bool flag3 = StyleComplexSelector.s_PseudoStates.TryGetValue(parts[j].value, ref pseudoStateData);
						if (flag3)
						{
							bool flag4 = !pseudoStateData.negate;
							if (flag4)
							{
								pseudoStates |= pseudoStateData.state;
							}
							else
							{
								pseudoStates2 |= pseudoStateData.state;
							}
						}
						else
						{
							Debug.LogWarningFormat("Unknown pseudo class \"{0}\"", new object[] { parts[j].value });
						}
					}
				}
				styleSelector.pseudoStateMask = (int)pseudoStates;
				styleSelector.negatedPseudoStateMask = (int)pseudoStates2;
				i++;
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00035DEC File Offset: 0x00033FEC
		public override string ToString()
		{
			return string.Format("[{0}]", string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<StyleSelector, string>(this.m_Selectors, (StyleSelector x) => x.ToString()))));
		}

		// Token: 0x04000589 RID: 1417
		[SerializeField]
		private int m_Specificity;

		// Token: 0x0400058B RID: 1419
		[SerializeField]
		private StyleSelector[] m_Selectors;

		// Token: 0x0400058C RID: 1420
		[SerializeField]
		internal int ruleIndex;

		// Token: 0x0400058D RID: 1421
		[NonSerialized]
		internal StyleComplexSelector nextInTable;

		// Token: 0x0400058E RID: 1422
		[NonSerialized]
		internal int orderInStyleSheet;

		// Token: 0x0400058F RID: 1423
		private static Dictionary<string, StyleComplexSelector.PseudoStateData> s_PseudoStates;

		// Token: 0x020001BF RID: 447
		private struct PseudoStateData
		{
			// Token: 0x06000E43 RID: 3651 RVA: 0x00035E41 File Offset: 0x00034041
			public PseudoStateData(PseudoStates state, bool negate)
			{
				this.state = state;
				this.negate = negate;
			}

			// Token: 0x04000590 RID: 1424
			public readonly PseudoStates state;

			// Token: 0x04000591 RID: 1425
			public readonly bool negate;
		}
	}
}
