using System;

namespace UnityEngine.Playables
{
	// Token: 0x020003A4 RID: 932
	public static class PlayableExtensions
	{
		// Token: 0x0600202D RID: 8237 RVA: 0x00036874 File Offset: 0x00034A74
		public static bool IsNull<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsNull();
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0003689C File Offset: 0x00034A9C
		public static bool IsValid<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsValid();
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000368C4 File Offset: 0x00034AC4
		public static void Destroy<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Destroy();
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x000368E8 File Offset: 0x00034AE8
		public static PlayableGraph GetGraph<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetGraph();
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00036910 File Offset: 0x00034B10
		[Obsolete("SetPlayState() has been deprecated. Use Play(), Pause() or SetDelay() instead", false)]
		public static void SetPlayState<U>(this U playable, PlayState value) where U : struct, IPlayable
		{
			bool flag = value == PlayState.Delayed;
			if (flag)
			{
				throw new ArgumentException("Can't set Delayed: use SetDelay() instead");
			}
			if (value != PlayState.Paused)
			{
				if (value == PlayState.Playing)
				{
					playable.GetHandle().Play();
				}
			}
			else
			{
				playable.GetHandle().Pause();
			}
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00036970 File Offset: 0x00034B70
		public static PlayState GetPlayState<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPlayState();
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00036998 File Offset: 0x00034B98
		public static void Play<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Play();
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000369BC File Offset: 0x00034BBC
		public static void Pause<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Pause();
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000369E0 File Offset: 0x00034BE0
		public static void SetSpeed<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetSpeed(value);
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00036A08 File Offset: 0x00034C08
		public static double GetSpeed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetSpeed();
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00036A30 File Offset: 0x00034C30
		public static void SetDuration<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDuration(value);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00036A58 File Offset: 0x00034C58
		public static double GetDuration<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDuration();
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00036A80 File Offset: 0x00034C80
		public static void SetTime<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTime(value);
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00036AA8 File Offset: 0x00034CA8
		public static double GetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTime();
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00036AD0 File Offset: 0x00034CD0
		public static double GetPreviousTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPreviousTime();
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00036AF8 File Offset: 0x00034CF8
		public static void SetDone<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDone(value);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00036B20 File Offset: 0x00034D20
		public static bool IsDone<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDone();
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00036B48 File Offset: 0x00034D48
		public static void SetPropagateSetTime<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetPropagateSetTime(value);
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00036B70 File Offset: 0x00034D70
		public static bool GetPropagateSetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPropagateSetTime();
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x00036B98 File Offset: 0x00034D98
		public static bool CanChangeInputs<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanChangeInputs();
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00036BC0 File Offset: 0x00034DC0
		public static bool CanSetWeights<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanSetWeights();
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00036BE8 File Offset: 0x00034DE8
		public static bool CanDestroy<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanDestroy();
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00036C10 File Offset: 0x00034E10
		public static void SetInputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputCount(value);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00036C38 File Offset: 0x00034E38
		public static int GetInputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputCount();
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00036C60 File Offset: 0x00034E60
		public static void SetOutputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetOutputCount(value);
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00036C88 File Offset: 0x00034E88
		public static int GetOutputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutputCount();
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00036CB0 File Offset: 0x00034EB0
		public static Playable GetInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInput(inputPort);
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00036CD8 File Offset: 0x00034ED8
		public static Playable GetOutput<U>(this U playable, int outputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutput(outputPort);
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00036D00 File Offset: 0x00034F00
		public static void SetInputWeight<U>(this U playable, int inputIndex, float weight) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(inputIndex, weight);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00036D28 File Offset: 0x00034F28
		public static void SetInputWeight<U, V>(this U playable, V input, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(input.GetHandle(), weight);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00036D5C File Offset: 0x00034F5C
		public static float GetInputWeight<U>(this U playable, int inputIndex) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputWeight(inputIndex);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00036D84 File Offset: 0x00034F84
		public static void ConnectInput<U, V>(this U playable, int inputIndex, V sourcePlayable, int sourceOutputIndex) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.ConnectInput(inputIndex, sourcePlayable, sourceOutputIndex, 0f);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00036D98 File Offset: 0x00034F98
		public static void ConnectInput<U, V>(this U playable, int inputIndex, V sourcePlayable, int sourceOutputIndex, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetGraph<U>().Connect<V, U>(sourcePlayable, sourceOutputIndex, playable, inputIndex);
			playable.SetInputWeight(inputIndex, weight);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00036DC4 File Offset: 0x00034FC4
		public static void DisconnectInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			playable.GetGraph<U>().Disconnect<U>(playable, inputPort);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00036DE4 File Offset: 0x00034FE4
		public static int AddInput<U, V>(this U playable, V sourcePlayable, int sourceOutputIndex, float weight = 0f) where U : struct, IPlayable where V : struct, IPlayable
		{
			int inputCount = playable.GetInputCount<U>();
			playable.SetInputCount(inputCount + 1);
			playable.ConnectInput(inputCount, sourcePlayable, sourceOutputIndex, weight);
			return inputCount;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00036E14 File Offset: 0x00035014
		[Obsolete("SetDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static void SetDelay<U>(this U playable, double delay) where U : struct, IPlayable
		{
			playable.GetHandle().SetDelay(delay);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00036E3C File Offset: 0x0003503C
		[Obsolete("GetDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static double GetDelay<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDelay();
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00036E64 File Offset: 0x00035064
		[Obsolete("IsDelayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static bool IsDelayed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDelayed();
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00036E8C File Offset: 0x0003508C
		public static void SetLeadTime<U>(this U playable, float value) where U : struct, IPlayable
		{
			playable.GetHandle().SetLeadTime(value);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00036EB4 File Offset: 0x000350B4
		public static float GetLeadTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetLeadTime();
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00036EDC File Offset: 0x000350DC
		public static PlayableTraversalMode GetTraversalMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTraversalMode();
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00036F04 File Offset: 0x00035104
		public static void SetTraversalMode<U>(this U playable, PlayableTraversalMode mode) where U : struct, IPlayable
		{
			playable.GetHandle().SetTraversalMode(mode);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00036F2C File Offset: 0x0003512C
		internal static DirectorWrapMode GetTimeWrapMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTimeWrapMode();
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00036F54 File Offset: 0x00035154
		internal static void SetTimeWrapMode<U>(this U playable, DirectorWrapMode value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTimeWrapMode(value);
		}
	}
}
