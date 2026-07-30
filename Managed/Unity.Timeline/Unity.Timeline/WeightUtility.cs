using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000050 RID: 80
	internal static class WeightUtility
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000A5B0 File Offset: 0x000087B0
		public static float NormalizeMixer(Playable mixer)
		{
			if (!mixer.IsValid<Playable>())
			{
				return 0f;
			}
			int inputCount = mixer.GetInputCount<Playable>();
			float num = 0f;
			for (int i = 0; i < inputCount; i++)
			{
				num += mixer.GetInputWeight(i);
			}
			if (num > Mathf.Epsilon && num < 1f)
			{
				for (int j = 0; j < inputCount; j++)
				{
					mixer.SetInputWeight(j, mixer.GetInputWeight(j) / num);
				}
			}
			return Mathf.Clamp01(num);
		}
	}
}
