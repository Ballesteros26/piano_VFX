using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000274 RID: 628
	[Serializable]
	public struct LoadSceneParameters
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0002AFF0 File Offset: 0x000291F0
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x0002B008 File Offset: 0x00029208
		public LoadSceneMode loadSceneMode
		{
			get
			{
				return this.m_LoadSceneMode;
			}
			set
			{
				this.m_LoadSceneMode = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0002B014 File Offset: 0x00029214
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x0002B02C File Offset: 0x0002922C
		public LocalPhysicsMode localPhysicsMode
		{
			get
			{
				return this.m_LocalPhysicsMode;
			}
			set
			{
				this.m_LocalPhysicsMode = value;
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0002B036 File Offset: 0x00029236
		public LoadSceneParameters(LoadSceneMode mode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = LocalPhysicsMode.None;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0002B047 File Offset: 0x00029247
		public LoadSceneParameters(LoadSceneMode mode, LocalPhysicsMode physicsMode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = physicsMode;
		}

		// Token: 0x0400080C RID: 2060
		[SerializeField]
		private LoadSceneMode m_LoadSceneMode;

		// Token: 0x0400080D RID: 2061
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
