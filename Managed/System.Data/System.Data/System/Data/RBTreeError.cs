using System;

namespace System.Data
{
	// Token: 0x020000DD RID: 221
	internal enum RBTreeError
	{
		// Token: 0x040007F7 RID: 2039
		InvalidPageSize = 1,
		// Token: 0x040007F8 RID: 2040
		PagePositionInSlotInUse = 3,
		// Token: 0x040007F9 RID: 2041
		NoFreeSlots,
		// Token: 0x040007FA RID: 2042
		InvalidStateinInsert,
		// Token: 0x040007FB RID: 2043
		InvalidNextSizeInDelete = 7,
		// Token: 0x040007FC RID: 2044
		InvalidStateinDelete,
		// Token: 0x040007FD RID: 2045
		InvalidNodeSizeinDelete,
		// Token: 0x040007FE RID: 2046
		InvalidStateinEndDelete,
		// Token: 0x040007FF RID: 2047
		CannotRotateInvalidsuccessorNodeinDelete,
		// Token: 0x04000800 RID: 2048
		IndexOutOFRangeinGetNodeByIndex = 13,
		// Token: 0x04000801 RID: 2049
		RBDeleteFixup,
		// Token: 0x04000802 RID: 2050
		UnsupportedAccessMethod1,
		// Token: 0x04000803 RID: 2051
		UnsupportedAccessMethod2,
		// Token: 0x04000804 RID: 2052
		UnsupportedAccessMethodInNonNillRootSubtree,
		// Token: 0x04000805 RID: 2053
		AttachedNodeWithZerorbTreeNodeId,
		// Token: 0x04000806 RID: 2054
		CompareNodeInDataRowTree,
		// Token: 0x04000807 RID: 2055
		CompareSateliteTreeNodeInDataRowTree,
		// Token: 0x04000808 RID: 2056
		NestedSatelliteTreeEnumerator
	}
}
