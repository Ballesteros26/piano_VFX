using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000068 RID: 104
	public struct VolumeIsolationScope : IDisposable
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000CF88 File Offset: 0x0000B188
		public VolumeIsolationScope(bool unused)
		{
			VolumeManager.needIsolationFilteredByRenderer = true;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000CF90 File Offset: 0x0000B190
		void IDisposable.Dispose()
		{
			VolumeManager.needIsolationFilteredByRenderer = false;
		}
	}
}
