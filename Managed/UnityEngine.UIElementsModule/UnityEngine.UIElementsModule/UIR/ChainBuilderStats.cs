using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000216 RID: 534
	internal struct ChainBuilderStats
	{
		// Token: 0x040006B9 RID: 1721
		public uint elementsAdded;

		// Token: 0x040006BA RID: 1722
		public uint elementsRemoved;

		// Token: 0x040006BB RID: 1723
		public uint recursiveClipUpdates;

		// Token: 0x040006BC RID: 1724
		public uint recursiveClipUpdatesExpanded;

		// Token: 0x040006BD RID: 1725
		public uint nonRecursiveClipUpdates;

		// Token: 0x040006BE RID: 1726
		public uint recursiveTransformUpdates;

		// Token: 0x040006BF RID: 1727
		public uint recursiveTransformUpdatesExpanded;

		// Token: 0x040006C0 RID: 1728
		public uint recursiveOpacityUpdates;

		// Token: 0x040006C1 RID: 1729
		public uint recursiveOpacityUpdatesExpanded;

		// Token: 0x040006C2 RID: 1730
		public uint recursiveVisualUpdates;

		// Token: 0x040006C3 RID: 1731
		public uint recursiveVisualUpdatesExpanded;

		// Token: 0x040006C4 RID: 1732
		public uint nonRecursiveVisualUpdates;

		// Token: 0x040006C5 RID: 1733
		public uint dirtyProcessed;

		// Token: 0x040006C6 RID: 1734
		public uint nudgeTransformed;

		// Token: 0x040006C7 RID: 1735
		public uint boneTransformed;

		// Token: 0x040006C8 RID: 1736
		public uint skipTransformed;

		// Token: 0x040006C9 RID: 1737
		public uint visualUpdateTransformed;

		// Token: 0x040006CA RID: 1738
		public uint updatedMeshAllocations;

		// Token: 0x040006CB RID: 1739
		public uint newMeshAllocations;

		// Token: 0x040006CC RID: 1740
		public uint groupTransformElementsChanged;

		// Token: 0x040006CD RID: 1741
		public uint immedateRenderersActive;

		// Token: 0x040006CE RID: 1742
		public uint textUpdates;
	}
}
