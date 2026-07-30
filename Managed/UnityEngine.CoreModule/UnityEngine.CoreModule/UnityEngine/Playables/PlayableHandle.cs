using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x020003A8 RID: 936
	[UsedByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Export/Director/PlayableHandle.bindings.h")]
	public struct PlayableHandle : IEquatable<PlayableHandle>
	{
		// Token: 0x06002098 RID: 8344 RVA: 0x00037204 File Offset: 0x00035404
		internal T GetObject<T>() where T : class, IPlayableBehaviour
		{
			bool flag = !this.IsValid();
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				object scriptInstance = this.GetScriptInstance();
				bool flag2 = scriptInstance == null;
				if (flag2)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)scriptInstance);
				}
			}
			return t;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00037254 File Offset: 0x00035454
		[VisibleToOtherModules]
		internal bool IsPlayableOfType<T>()
		{
			return this.GetPlayableType() == typeof(T);
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x00037278 File Offset: 0x00035478
		public static PlayableHandle Null
		{
			get
			{
				return PlayableHandle.m_Null;
			}
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00037290 File Offset: 0x00035490
		internal Playable GetInput(int inputPort)
		{
			return new Playable(this.GetInputHandle(inputPort));
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000372B0 File Offset: 0x000354B0
		internal Playable GetOutput(int outputPort)
		{
			return new Playable(this.GetOutputHandle(outputPort));
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000372D0 File Offset: 0x000354D0
		internal bool SetInputWeight(int inputIndex, float weight)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			bool flag2;
			if (flag)
			{
				this.SetInputWeightFromIndex(inputIndex, weight);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000372FC File Offset: 0x000354FC
		internal float GetInputWeight(int inputIndex)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			float num;
			if (flag)
			{
				num = this.GetInputWeightFromIndex(inputIndex);
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0003732C File Offset: 0x0003552C
		internal void Destroy()
		{
			this.GetGraph().DestroyPlayable<Playable>(new Playable(this));
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00037354 File Offset: 0x00035554
		public static bool operator ==(PlayableHandle x, PlayableHandle y)
		{
			return PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00037370 File Offset: 0x00035570
		public static bool operator !=(PlayableHandle x, PlayableHandle y)
		{
			return !PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0003738C File Offset: 0x0003558C
		public override bool Equals(object p)
		{
			return p is PlayableHandle && this.Equals((PlayableHandle)p);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000373B8 File Offset: 0x000355B8
		public bool Equals(PlayableHandle other)
		{
			return PlayableHandle.CompareVersion(this, other);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000373D8 File Offset: 0x000355D8
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00037404 File Offset: 0x00035604
		internal static bool CompareVersion(PlayableHandle lhs, PlayableHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x0003743C File Offset: 0x0003563C
		internal bool CheckInputBounds(int inputIndex)
		{
			return this.CheckInputBounds(inputIndex, false);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00037458 File Offset: 0x00035658
		internal bool CheckInputBounds(int inputIndex, bool acceptAny)
		{
			bool flag = inputIndex == -1 && acceptAny;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = inputIndex < 0;
				if (flag3)
				{
					throw new IndexOutOfRangeException("Index must be greater than 0");
				}
				bool flag4 = this.GetInputCount() <= inputIndex;
				if (flag4)
				{
					throw new IndexOutOfRangeException(string.Concat(new object[]
					{
						"inputIndex ",
						inputIndex,
						" is greater than the number of available inputs (",
						this.GetInputCount(),
						")."
					}));
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000374DE File Offset: 0x000356DE
		[VisibleToOtherModules]
		internal bool IsNull()
		{
			return PlayableHandle.IsNull_Injected(ref this);
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000374E6 File Offset: 0x000356E6
		[VisibleToOtherModules]
		internal bool IsValid()
		{
			return PlayableHandle.IsValid_Injected(ref this);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000374EE File Offset: 0x000356EE
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetPlayableType", HasExplicitThis = true, ThrowsException = true)]
		internal Type GetPlayableType()
		{
			return PlayableHandle.GetPlayableType_Injected(ref this);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000374F6 File Offset: 0x000356F6
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetJobType", HasExplicitThis = true, ThrowsException = true)]
		internal Type GetJobType()
		{
			return PlayableHandle.GetJobType_Injected(ref this);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000374FE File Offset: 0x000356FE
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		internal void SetScriptInstance(object scriptInstance)
		{
			PlayableHandle.SetScriptInstance_Injected(ref this, scriptInstance);
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00037507 File Offset: 0x00035707
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::CanChangeInputs", HasExplicitThis = true, ThrowsException = true)]
		internal bool CanChangeInputs()
		{
			return PlayableHandle.CanChangeInputs_Injected(ref this);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0003750F File Offset: 0x0003570F
		[FreeFunction("PlayableHandleBindings::CanSetWeights", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool CanSetWeights()
		{
			return PlayableHandle.CanSetWeights_Injected(ref this);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00037517 File Offset: 0x00035717
		[FreeFunction("PlayableHandleBindings::CanDestroy", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool CanDestroy()
		{
			return PlayableHandle.CanDestroy_Injected(ref this);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0003751F File Offset: 0x0003571F
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetPlayState", HasExplicitThis = true, ThrowsException = true)]
		internal PlayState GetPlayState()
		{
			return PlayableHandle.GetPlayState_Injected(ref this);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00037527 File Offset: 0x00035727
		[FreeFunction("PlayableHandleBindings::Play", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void Play()
		{
			PlayableHandle.Play_Injected(ref this);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0003752F File Offset: 0x0003572F
		[FreeFunction("PlayableHandleBindings::Pause", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void Pause()
		{
			PlayableHandle.Pause_Injected(ref this);
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00037537 File Offset: 0x00035737
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetSpeed", HasExplicitThis = true, ThrowsException = true)]
		internal double GetSpeed()
		{
			return PlayableHandle.GetSpeed_Injected(ref this);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x0003753F File Offset: 0x0003573F
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetSpeed", HasExplicitThis = true, ThrowsException = true)]
		internal void SetSpeed(double value)
		{
			PlayableHandle.SetSpeed_Injected(ref this, value);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00037548 File Offset: 0x00035748
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTime", HasExplicitThis = true, ThrowsException = true)]
		internal double GetTime()
		{
			return PlayableHandle.GetTime_Injected(ref this);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00037550 File Offset: 0x00035750
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTime", HasExplicitThis = true, ThrowsException = true)]
		internal void SetTime(double value)
		{
			PlayableHandle.SetTime_Injected(ref this, value);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00037559 File Offset: 0x00035759
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::IsDone", HasExplicitThis = true, ThrowsException = true)]
		internal bool IsDone()
		{
			return PlayableHandle.IsDone_Injected(ref this);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00037561 File Offset: 0x00035761
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetDone", HasExplicitThis = true, ThrowsException = true)]
		internal void SetDone(bool value)
		{
			PlayableHandle.SetDone_Injected(ref this, value);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0003756A File Offset: 0x0003576A
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetDuration", HasExplicitThis = true, ThrowsException = true)]
		internal double GetDuration()
		{
			return PlayableHandle.GetDuration_Injected(ref this);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00037572 File Offset: 0x00035772
		[FreeFunction("PlayableHandleBindings::SetDuration", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetDuration(double value)
		{
			PlayableHandle.SetDuration_Injected(ref this, value);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0003757B File Offset: 0x0003577B
		[FreeFunction("PlayableHandleBindings::GetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool GetPropagateSetTime()
		{
			return PlayableHandle.GetPropagateSetTime_Injected(ref this);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00037583 File Offset: 0x00035783
		[FreeFunction("PlayableHandleBindings::SetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetPropagateSetTime(bool value)
		{
			PlayableHandle.SetPropagateSetTime_Injected(ref this, value);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0003758C File Offset: 0x0003578C
		[FreeFunction("PlayableHandleBindings::GetGraph", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal PlayableGraph GetGraph()
		{
			PlayableGraph playableGraph;
			PlayableHandle.GetGraph_Injected(ref this, out playableGraph);
			return playableGraph;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000375A2 File Offset: 0x000357A2
		[FreeFunction("PlayableHandleBindings::GetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal int GetInputCount()
		{
			return PlayableHandle.GetInputCount_Injected(ref this);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000375AA File Offset: 0x000357AA
		[FreeFunction("PlayableHandleBindings::SetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetInputCount(int value)
		{
			PlayableHandle.SetInputCount_Injected(ref this, value);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000375B3 File Offset: 0x000357B3
		[FreeFunction("PlayableHandleBindings::GetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal int GetOutputCount()
		{
			return PlayableHandle.GetOutputCount_Injected(ref this);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000375BB File Offset: 0x000357BB
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		internal void SetOutputCount(int value)
		{
			PlayableHandle.SetOutputCount_Injected(ref this, value);
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000375C4 File Offset: 0x000357C4
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetInputWeight", HasExplicitThis = true, ThrowsException = true)]
		internal void SetInputWeight(PlayableHandle input, float weight)
		{
			PlayableHandle.SetInputWeight_Injected(ref this, ref input, weight);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000375CF File Offset: 0x000357CF
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetDelay", HasExplicitThis = true, ThrowsException = true)]
		internal void SetDelay(double delay)
		{
			PlayableHandle.SetDelay_Injected(ref this, delay);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000375D8 File Offset: 0x000357D8
		[FreeFunction("PlayableHandleBindings::GetDelay", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal double GetDelay()
		{
			return PlayableHandle.GetDelay_Injected(ref this);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000375E0 File Offset: 0x000357E0
		[FreeFunction("PlayableHandleBindings::IsDelayed", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal bool IsDelayed()
		{
			return PlayableHandle.IsDelayed_Injected(ref this);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000375E8 File Offset: 0x000357E8
		[FreeFunction("PlayableHandleBindings::GetPreviousTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal double GetPreviousTime()
		{
			return PlayableHandle.GetPreviousTime_Injected(ref this);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000375F0 File Offset: 0x000357F0
		[FreeFunction("PlayableHandleBindings::SetLeadTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetLeadTime(float value)
		{
			PlayableHandle.SetLeadTime_Injected(ref this, value);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000375F9 File Offset: 0x000357F9
		[FreeFunction("PlayableHandleBindings::GetLeadTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal float GetLeadTime()
		{
			return PlayableHandle.GetLeadTime_Injected(ref this);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00037601 File Offset: 0x00035801
		[FreeFunction("PlayableHandleBindings::GetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal PlayableTraversalMode GetTraversalMode()
		{
			return PlayableHandle.GetTraversalMode_Injected(ref this);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00037609 File Offset: 0x00035809
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		internal void SetTraversalMode(PlayableTraversalMode mode)
		{
			PlayableHandle.SetTraversalMode_Injected(ref this, mode);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00037612 File Offset: 0x00035812
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetJobData", HasExplicitThis = true, ThrowsException = true)]
		internal IntPtr GetJobData()
		{
			return PlayableHandle.GetJobData_Injected(ref this);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0003761A File Offset: 0x0003581A
		[FreeFunction("PlayableHandleBindings::GetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal DirectorWrapMode GetTimeWrapMode()
		{
			return PlayableHandle.GetTimeWrapMode_Injected(ref this);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00037622 File Offset: 0x00035822
		[FreeFunction("PlayableHandleBindings::SetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		internal void SetTimeWrapMode(DirectorWrapMode mode)
		{
			PlayableHandle.SetTimeWrapMode_Injected(ref this, mode);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0003762B File Offset: 0x0003582B
		[FreeFunction("PlayableHandleBindings::GetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		private object GetScriptInstance()
		{
			return PlayableHandle.GetScriptInstance_Injected(ref this);
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x00037634 File Offset: 0x00035834
		[FreeFunction("PlayableHandleBindings::GetInputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetInputHandle(int index)
		{
			PlayableHandle playableHandle;
			PlayableHandle.GetInputHandle_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0003764C File Offset: 0x0003584C
		[FreeFunction("PlayableHandleBindings::GetOutputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetOutputHandle(int index)
		{
			PlayableHandle playableHandle;
			PlayableHandle.GetOutputHandle_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00037663 File Offset: 0x00035863
		[FreeFunction("PlayableHandleBindings::SetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		private void SetInputWeightFromIndex(int index, float weight)
		{
			PlayableHandle.SetInputWeightFromIndex_Injected(ref this, index, weight);
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0003766D File Offset: 0x0003586D
		[FreeFunction("PlayableHandleBindings::GetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		private float GetInputWeightFromIndex(int index)
		{
			return PlayableHandle.GetInputWeightFromIndex_Injected(ref this, index);
		}

		// Token: 0x060020D4 RID: 8404
		[MethodImpl(4096)]
		private static extern bool IsNull_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020D5 RID: 8405
		[MethodImpl(4096)]
		private static extern bool IsValid_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020D6 RID: 8406
		[MethodImpl(4096)]
		private static extern Type GetPlayableType_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020D7 RID: 8407
		[MethodImpl(4096)]
		private static extern Type GetJobType_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020D8 RID: 8408
		[MethodImpl(4096)]
		private static extern void SetScriptInstance_Injected(ref PlayableHandle _unity_self, object scriptInstance);

		// Token: 0x060020D9 RID: 8409
		[MethodImpl(4096)]
		private static extern bool CanChangeInputs_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020DA RID: 8410
		[MethodImpl(4096)]
		private static extern bool CanSetWeights_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020DB RID: 8411
		[MethodImpl(4096)]
		private static extern bool CanDestroy_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020DC RID: 8412
		[MethodImpl(4096)]
		private static extern PlayState GetPlayState_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020DD RID: 8413
		[MethodImpl(4096)]
		private static extern void Play_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020DE RID: 8414
		[MethodImpl(4096)]
		private static extern void Pause_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020DF RID: 8415
		[MethodImpl(4096)]
		private static extern double GetSpeed_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020E0 RID: 8416
		[MethodImpl(4096)]
		private static extern void SetSpeed_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x060020E1 RID: 8417
		[MethodImpl(4096)]
		private static extern double GetTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020E2 RID: 8418
		[MethodImpl(4096)]
		private static extern void SetTime_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x060020E3 RID: 8419
		[MethodImpl(4096)]
		private static extern bool IsDone_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020E4 RID: 8420
		[MethodImpl(4096)]
		private static extern void SetDone_Injected(ref PlayableHandle _unity_self, bool value);

		// Token: 0x060020E5 RID: 8421
		[MethodImpl(4096)]
		private static extern double GetDuration_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020E6 RID: 8422
		[MethodImpl(4096)]
		private static extern void SetDuration_Injected(ref PlayableHandle _unity_self, double value);

		// Token: 0x060020E7 RID: 8423
		[MethodImpl(4096)]
		private static extern bool GetPropagateSetTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020E8 RID: 8424
		[MethodImpl(4096)]
		private static extern void SetPropagateSetTime_Injected(ref PlayableHandle _unity_self, bool value);

		// Token: 0x060020E9 RID: 8425
		[MethodImpl(4096)]
		private static extern void GetGraph_Injected(ref PlayableHandle _unity_self, out PlayableGraph ret);

		// Token: 0x060020EA RID: 8426
		[MethodImpl(4096)]
		private static extern int GetInputCount_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020EB RID: 8427
		[MethodImpl(4096)]
		private static extern void SetInputCount_Injected(ref PlayableHandle _unity_self, int value);

		// Token: 0x060020EC RID: 8428
		[MethodImpl(4096)]
		private static extern int GetOutputCount_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020ED RID: 8429
		[MethodImpl(4096)]
		private static extern void SetOutputCount_Injected(ref PlayableHandle _unity_self, int value);

		// Token: 0x060020EE RID: 8430
		[MethodImpl(4096)]
		private static extern void SetInputWeight_Injected(ref PlayableHandle _unity_self, ref PlayableHandle input, float weight);

		// Token: 0x060020EF RID: 8431
		[MethodImpl(4096)]
		private static extern void SetDelay_Injected(ref PlayableHandle _unity_self, double delay);

		// Token: 0x060020F0 RID: 8432
		[MethodImpl(4096)]
		private static extern double GetDelay_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020F1 RID: 8433
		[MethodImpl(4096)]
		private static extern bool IsDelayed_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020F2 RID: 8434
		[MethodImpl(4096)]
		private static extern double GetPreviousTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020F3 RID: 8435
		[MethodImpl(4096)]
		private static extern void SetLeadTime_Injected(ref PlayableHandle _unity_self, float value);

		// Token: 0x060020F4 RID: 8436
		[MethodImpl(4096)]
		private static extern float GetLeadTime_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020F5 RID: 8437
		[MethodImpl(4096)]
		private static extern PlayableTraversalMode GetTraversalMode_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020F6 RID: 8438
		[MethodImpl(4096)]
		private static extern void SetTraversalMode_Injected(ref PlayableHandle _unity_self, PlayableTraversalMode mode);

		// Token: 0x060020F7 RID: 8439
		[MethodImpl(4096)]
		private static extern IntPtr GetJobData_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020F8 RID: 8440
		[MethodImpl(4096)]
		private static extern DirectorWrapMode GetTimeWrapMode_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020F9 RID: 8441
		[MethodImpl(4096)]
		private static extern void SetTimeWrapMode_Injected(ref PlayableHandle _unity_self, DirectorWrapMode mode);

		// Token: 0x060020FA RID: 8442
		[MethodImpl(4096)]
		private static extern object GetScriptInstance_Injected(ref PlayableHandle _unity_self);

		// Token: 0x060020FB RID: 8443
		[MethodImpl(4096)]
		private static extern void GetInputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x060020FC RID: 8444
		[MethodImpl(4096)]
		private static extern void GetOutputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x060020FD RID: 8445
		[MethodImpl(4096)]
		private static extern void SetInputWeightFromIndex_Injected(ref PlayableHandle _unity_self, int index, float weight);

		// Token: 0x060020FE RID: 8446
		[MethodImpl(4096)]
		private static extern float GetInputWeightFromIndex_Injected(ref PlayableHandle _unity_self, int index);

		// Token: 0x04000BAB RID: 2987
		internal IntPtr m_Handle;

		// Token: 0x04000BAC RID: 2988
		internal uint m_Version;

		// Token: 0x04000BAD RID: 2989
		private static readonly PlayableHandle m_Null = default(PlayableHandle);
	}
}
