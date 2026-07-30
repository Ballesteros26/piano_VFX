using System;
using UnityEngine.Playables;
using UnityEngine.VFX;

// Token: 0x02000005 RID: 5
internal class VisualEffectActivationMixerBehaviour : PlayableBehaviour
{
	// Token: 0x0600000E RID: 14 RVA: 0x00002308 File Offset: 0x00000508
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		VisualEffect visualEffect = playerData as VisualEffect;
		if (!visualEffect)
		{
			return;
		}
		int inputCount = playable.GetInputCount<Playable>();
		for (int i = 0; i < inputCount; i++)
		{
			bool flag = playable.GetInputWeight(i) != 0f;
			VisualEffectActivationBehaviour behaviour = ((ScriptPlayable<T>)playable.GetInput(i)).GetBehaviour();
			if (this.enabledStates[i] != flag)
			{
				if (flag)
				{
					behaviour.SendEventEnter(visualEffect);
				}
				else
				{
					behaviour.SendEventExit(visualEffect);
				}
				this.enabledStates[i] = flag;
			}
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000238A File Offset: 0x0000058A
	public override void OnPlayableCreate(Playable playable)
	{
		this.enabledStates = new bool[playable.GetInputCount<Playable>()];
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000239D File Offset: 0x0000059D
	public override void OnPlayableDestroy(Playable playable)
	{
		this.enabledStates = null;
	}

	// Token: 0x04000009 RID: 9
	private bool[] enabledStates;
}
