using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000EF RID: 239
	public abstract class CustomPostProcessVolumeComponent : VolumeComponent
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0003915C File Offset: 0x0003735C
		public virtual CustomPostProcessInjectionPoint injectionPoint
		{
			get
			{
				return CustomPostProcessInjectionPoint.AfterPostProcess;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public virtual bool visibleInSceneView
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void Setup()
		{
		}

		// Token: 0x0600077E RID: 1918
		public abstract void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination);

		// Token: 0x0600077F RID: 1919 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void Cleanup()
		{
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0003915F File Offset: 0x0003735F
		protected override void OnDisable()
		{
			base.OnDisable();
			this.CleanupInternal();
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0003916D File Offset: 0x0003736D
		internal void CleanupInternal()
		{
			if (this.m_IsInitialized)
			{
				this.Cleanup();
			}
			this.m_IsInitialized = false;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00039184 File Offset: 0x00037384
		internal void SetupIfNeeded()
		{
			if (!this.m_IsInitialized)
			{
				this.Setup();
				this.m_IsInitialized = true;
			}
		}

		// Token: 0x040007FA RID: 2042
		private bool m_IsInitialized;
	}
}
