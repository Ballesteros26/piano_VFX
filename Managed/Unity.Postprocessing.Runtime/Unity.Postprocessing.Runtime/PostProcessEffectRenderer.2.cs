using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000050 RID: 80
	public abstract class PostProcessEffectRenderer<T> : PostProcessEffectRenderer where T : PostProcessEffectSettings
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000A2F0 File Offset: 0x000084F0
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public T settings { get; internal set; }

		// Token: 0x06000111 RID: 273 RVA: 0x0000A301 File Offset: 0x00008501
		internal override void SetSettings(PostProcessEffectSettings settings)
		{
			this.settings = (T)((object)settings);
		}
	}
}
