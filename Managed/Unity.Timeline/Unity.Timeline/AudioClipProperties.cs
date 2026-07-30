using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000012 RID: 18
	[NotKeyable]
	[Serializable]
	internal class AudioClipProperties : PlayableBehaviour
	{
		// Token: 0x04000085 RID: 133
		[Range(0f, 1f)]
		public float volume = 1f;
	}
}
