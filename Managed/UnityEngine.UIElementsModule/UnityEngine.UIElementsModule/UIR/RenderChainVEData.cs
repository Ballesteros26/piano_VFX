using System;
using System.Collections.Generic;
using UnityEngine.UIElements.UIR.Implementation;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200021E RID: 542
	internal struct RenderChainVEData
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0003D4F8 File Offset: 0x0003B6F8
		internal RenderChainCommand lastClosingOrLastCommand
		{
			get
			{
				return this.lastClosingCommand ?? this.lastCommand;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003D51C File Offset: 0x0003B71C
		internal static bool AllocatesID(BMPAlloc alloc)
		{
			return alloc.ownedState != OwnedState.Inherited && alloc.IsValid();
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003D540 File Offset: 0x0003B740
		internal static bool InheritsID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Inherited && alloc.IsValid();
		}

		// Token: 0x0400071C RID: 1820
		internal VisualElement prev;

		// Token: 0x0400071D RID: 1821
		internal VisualElement next;

		// Token: 0x0400071E RID: 1822
		internal VisualElement groupTransformAncestor;

		// Token: 0x0400071F RID: 1823
		internal VisualElement boneTransformAncestor;

		// Token: 0x04000720 RID: 1824
		internal VisualElement prevDirty;

		// Token: 0x04000721 RID: 1825
		internal VisualElement nextDirty;

		// Token: 0x04000722 RID: 1826
		internal int hierarchyDepth;

		// Token: 0x04000723 RID: 1827
		internal RenderDataDirtyTypes dirtiedValues;

		// Token: 0x04000724 RID: 1828
		internal uint dirtyID;

		// Token: 0x04000725 RID: 1829
		internal ClipMethod clipMethod;

		// Token: 0x04000726 RID: 1830
		internal RenderChainCommand firstCommand;

		// Token: 0x04000727 RID: 1831
		internal RenderChainCommand lastCommand;

		// Token: 0x04000728 RID: 1832
		internal RenderChainCommand firstClosingCommand;

		// Token: 0x04000729 RID: 1833
		internal RenderChainCommand lastClosingCommand;

		// Token: 0x0400072A RID: 1834
		internal bool isInChain;

		// Token: 0x0400072B RID: 1835
		internal bool isStencilClipped;

		// Token: 0x0400072C RID: 1836
		internal bool isHierarchyHidden;

		// Token: 0x0400072D RID: 1837
		internal bool usesAtlas;

		// Token: 0x0400072E RID: 1838
		internal bool disableNudging;

		// Token: 0x0400072F RID: 1839
		internal bool usesLegacyText;

		// Token: 0x04000730 RID: 1840
		internal MeshHandle data;

		// Token: 0x04000731 RID: 1841
		internal MeshHandle closingData;

		// Token: 0x04000732 RID: 1842
		internal Matrix4x4 verticesSpace;

		// Token: 0x04000733 RID: 1843
		internal int displacementUVStart;

		// Token: 0x04000734 RID: 1844
		internal int displacementUVEnd;

		// Token: 0x04000735 RID: 1845
		internal BMPAlloc transformID;

		// Token: 0x04000736 RID: 1846
		internal BMPAlloc clipRectID;

		// Token: 0x04000737 RID: 1847
		internal BMPAlloc opacityID;

		// Token: 0x04000738 RID: 1848
		internal float compositeOpacity;

		// Token: 0x04000739 RID: 1849
		internal VisualElement prevText;

		// Token: 0x0400073A RID: 1850
		internal VisualElement nextText;

		// Token: 0x0400073B RID: 1851
		internal List<RenderChainTextEntry> textEntries;
	}
}
