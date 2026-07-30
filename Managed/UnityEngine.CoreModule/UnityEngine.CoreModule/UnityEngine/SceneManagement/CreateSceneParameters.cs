using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000275 RID: 629
	[Serializable]
	public struct CreateSceneParameters
	{
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0002B058 File Offset: 0x00029258
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x0002B070 File Offset: 0x00029270
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

		// Token: 0x06001A59 RID: 6745 RVA: 0x0002B070 File Offset: 0x00029270
		public CreateSceneParameters(LocalPhysicsMode physicsMode)
		{
			this.m_LocalPhysicsMode = physicsMode;
		}

		// Token: 0x0400080E RID: 2062
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
