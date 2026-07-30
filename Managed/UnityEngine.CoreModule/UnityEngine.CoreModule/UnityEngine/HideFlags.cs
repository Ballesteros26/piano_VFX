using System;

namespace UnityEngine
{
	// Token: 0x020001C4 RID: 452
	[Flags]
	public enum HideFlags
	{
		// Token: 0x0400066D RID: 1645
		None = 0,
		// Token: 0x0400066E RID: 1646
		HideInHierarchy = 1,
		// Token: 0x0400066F RID: 1647
		HideInInspector = 2,
		// Token: 0x04000670 RID: 1648
		DontSaveInEditor = 4,
		// Token: 0x04000671 RID: 1649
		NotEditable = 8,
		// Token: 0x04000672 RID: 1650
		DontSaveInBuild = 16,
		// Token: 0x04000673 RID: 1651
		DontUnloadUnusedAsset = 32,
		// Token: 0x04000674 RID: 1652
		DontSave = 52,
		// Token: 0x04000675 RID: 1653
		HideAndDontSave = 61
	}
}
