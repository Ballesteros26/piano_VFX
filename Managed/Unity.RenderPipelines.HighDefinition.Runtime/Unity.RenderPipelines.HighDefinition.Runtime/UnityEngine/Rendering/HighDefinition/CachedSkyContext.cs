using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000160 RID: 352
	internal struct CachedSkyContext
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x00050C13 File Offset: 0x0004EE13
		public void Reset()
		{
			this.hash = 0;
			this.refCount = 0;
			if (this.renderingContext != null)
			{
				this.renderingContext.ClearAmbientProbe();
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00050C36 File Offset: 0x0004EE36
		public void Cleanup()
		{
			this.Reset();
			if (this.renderingContext != null)
			{
				this.renderingContext.Cleanup();
				this.renderingContext = null;
			}
		}

		// Token: 0x04000FC1 RID: 4033
		public Type type;

		// Token: 0x04000FC2 RID: 4034
		public SkyRenderingContext renderingContext;

		// Token: 0x04000FC3 RID: 4035
		public int hash;

		// Token: 0x04000FC4 RID: 4036
		public int refCount;
	}
}
