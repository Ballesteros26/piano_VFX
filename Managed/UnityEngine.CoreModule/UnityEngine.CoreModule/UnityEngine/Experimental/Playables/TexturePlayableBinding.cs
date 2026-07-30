using System;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003CA RID: 970
	public static class TexturePlayableBinding
	{
		// Token: 0x060021BD RID: 8637 RVA: 0x00039470 File Offset: 0x00037670
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(RenderTexture), new PlayableBinding.CreateOutputMethod(TexturePlayableBinding.CreateTextureOutput));
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000394A0 File Offset: 0x000376A0
		private static PlayableOutput CreateTextureOutput(PlayableGraph graph, string name)
		{
			return TexturePlayableOutput.Create(graph, name, null);
		}
	}
}
