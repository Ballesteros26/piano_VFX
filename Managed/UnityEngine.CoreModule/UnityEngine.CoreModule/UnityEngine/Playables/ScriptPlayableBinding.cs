using System;

namespace UnityEngine.Playables
{
	// Token: 0x020003AD RID: 941
	public static class ScriptPlayableBinding
	{
		// Token: 0x06002151 RID: 8529 RVA: 0x00037EF0 File Offset: 0x000360F0
		public static PlayableBinding Create(string name, Object key, Type type)
		{
			return PlayableBinding.CreateInternal(name, key, type, new PlayableBinding.CreateOutputMethod(ScriptPlayableBinding.CreateScriptOutput));
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00037F18 File Offset: 0x00036118
		private static PlayableOutput CreateScriptOutput(PlayableGraph graph, string name)
		{
			return ScriptPlayableOutput.Create(graph, name);
		}
	}
}
