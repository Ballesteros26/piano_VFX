using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200039E RID: 926
	[RequiredByNativeCode]
	[AssetFileNameExtension("playable", new string[] { })]
	[Serializable]
	public abstract class PlayableAsset : ScriptableObject, IPlayableAsset
	{
		// Token: 0x0600200B RID: 8203
		public abstract Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x000366AC File Offset: 0x000348AC
		public virtual double duration
		{
			get
			{
				return PlayableBinding.DefaultDuration;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x000366C4 File Offset: 0x000348C4
		public virtual IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000366DC File Offset: 0x000348DC
		[RequiredByNativeCode]
		internal unsafe static void Internal_CreatePlayable(PlayableAsset asset, PlayableGraph graph, GameObject go, IntPtr ptr)
		{
			bool flag = asset == null;
			Playable playable;
			if (flag)
			{
				playable = Playable.Null;
			}
			else
			{
				playable = asset.CreatePlayable(graph, go);
			}
			Playable* ptr2 = (Playable*)ptr.ToPointer();
			*ptr2 = playable;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00036718 File Offset: 0x00034918
		[RequiredByNativeCode]
		internal unsafe static void Internal_GetPlayableAssetDuration(PlayableAsset asset, IntPtr ptrToDouble)
		{
			double duration = asset.duration;
			double* ptr = (double*)ptrToDouble.ToPointer();
			*ptr = duration;
		}
	}
}
