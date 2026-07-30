using System;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200000A RID: 10
	[RequireComponent(typeof(Renderer))]
	internal class VFXVisibilityEventBinder : VFXEventBinderBase
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002091 File Offset: 0x00000291
		protected override void SetEventAttribute(object[] parameters)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002652 File Offset: 0x00000852
		private void OnBecameVisible()
		{
			if (this.activation != VFXVisibilityEventBinder.Activation.OnBecameVisible)
			{
				return;
			}
			base.SendEventToVisualEffect(Array.Empty<object>());
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002668 File Offset: 0x00000868
		private void OnBecameInvisible()
		{
			if (this.activation != VFXVisibilityEventBinder.Activation.OnBecameInvisible)
			{
				return;
			}
			base.SendEventToVisualEffect(Array.Empty<object>());
		}

		// Token: 0x04000012 RID: 18
		public VFXVisibilityEventBinder.Activation activation;

		// Token: 0x0200002E RID: 46
		public enum Activation
		{
			// Token: 0x040000BB RID: 187
			OnBecameVisible,
			// Token: 0x040000BC RID: 188
			OnBecameInvisible
		}
	}
}
