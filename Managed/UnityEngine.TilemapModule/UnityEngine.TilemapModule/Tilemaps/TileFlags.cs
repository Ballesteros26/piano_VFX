using System;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000D RID: 13
	[Flags]
	public enum TileFlags
	{
		// Token: 0x04000028 RID: 40
		None = 0,
		// Token: 0x04000029 RID: 41
		LockColor = 1,
		// Token: 0x0400002A RID: 42
		LockTransform = 2,
		// Token: 0x0400002B RID: 43
		InstantiateGameObjectRuntimeOnly = 4,
		// Token: 0x0400002C RID: 44
		LockAll = 3
	}
}
