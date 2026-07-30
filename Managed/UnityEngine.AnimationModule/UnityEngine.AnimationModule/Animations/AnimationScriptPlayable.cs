using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000052 RID: 82
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationScriptPlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[StaticAccessor("AnimationScriptPlayableBindings", StaticAccessorType.DoubleColon)]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct AnimationScriptPlayable : IAnimationJobPlayable, IPlayable, IEquatable<AnimationScriptPlayable>
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public static AnimationScriptPlayable Null
		{
			get
			{
				return AnimationScriptPlayable.m_NullPlayable;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00005CD0 File Offset: 0x00003ED0
		public static AnimationScriptPlayable Create<T>(PlayableGraph graph, T jobData, int inputCount = 0) where T : struct, IAnimationJob
		{
			PlayableHandle playableHandle = AnimationScriptPlayable.CreateHandle<T>(graph, inputCount);
			AnimationScriptPlayable animationScriptPlayable = new AnimationScriptPlayable(playableHandle);
			animationScriptPlayable.SetJobData<T>(jobData);
			return animationScriptPlayable;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00005CFC File Offset: 0x00003EFC
		private static PlayableHandle CreateHandle<T>(PlayableGraph graph, int inputCount) where T : struct, IAnimationJob
		{
			IntPtr jobReflectionData = ProcessAnimationJobStruct<T>.GetJobReflectionData();
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationScriptPlayable.CreateHandleInternal(graph, ref @null, jobReflectionData);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00005D3C File Offset: 0x00003F3C
		internal AnimationScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationScriptPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationScriptPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00005D78 File Offset: 0x00003F78
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00005D90 File Offset: 0x00003F90
		private void CheckJobTypeValidity<T>()
		{
			Type jobType = this.GetHandle().GetJobType();
			bool flag = jobType != typeof(T);
			if (flag)
			{
				throw new ArgumentException(string.Format("Wrong type: the given job type ({0}) is different from the creation job type ({1}).", typeof(T).FullName, jobType.FullName));
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public unsafe T GetJobData<T>() where T : struct, IAnimationJob
		{
			this.CheckJobTypeValidity<T>();
			T t;
			UnsafeUtility.CopyPtrToStructure<T>((void*)this.GetHandle().GetJobData(), out t);
			return t;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00005E20 File Offset: 0x00004020
		public unsafe void SetJobData<T>(T jobData) where T : struct, IAnimationJob
		{
			this.CheckJobTypeValidity<T>();
			UnsafeUtility.CopyStructureToPtr<T>(ref jobData, (void*)this.GetHandle().GetJobData());
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00005E50 File Offset: 0x00004050
		public static implicit operator Playable(AnimationScriptPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00005E70 File Offset: 0x00004070
		public static explicit operator AnimationScriptPlayable(Playable playable)
		{
			return new AnimationScriptPlayable(playable.GetHandle());
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00005E90 File Offset: 0x00004090
		public bool Equals(AnimationScriptPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00005EB4 File Offset: 0x000040B4
		public void SetProcessInputs(bool value)
		{
			AnimationScriptPlayable.SetProcessInputsInternal(this.GetHandle(), value);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00005EC4 File Offset: 0x000040C4
		public bool GetProcessInputs()
		{
			return AnimationScriptPlayable.GetProcessInputsInternal(this.GetHandle());
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00005EE1 File Offset: 0x000040E1
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData)
		{
			return AnimationScriptPlayable.CreateHandleInternal_Injected(ref graph, ref handle, jobReflectionData);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00005EEC File Offset: 0x000040EC
		[NativeThrows]
		private static void SetProcessInputsInternal(PlayableHandle handle, bool value)
		{
			AnimationScriptPlayable.SetProcessInputsInternal_Injected(ref handle, value);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00005EF6 File Offset: 0x000040F6
		[NativeThrows]
		private static bool GetProcessInputsInternal(PlayableHandle handle)
		{
			return AnimationScriptPlayable.GetProcessInputsInternal_Injected(ref handle);
		}

		// Token: 0x060003FB RID: 1019
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData);

		// Token: 0x060003FC RID: 1020
		[MethodImpl(4096)]
		private static extern void SetProcessInputsInternal_Injected(ref PlayableHandle handle, bool value);

		// Token: 0x060003FD RID: 1021
		[MethodImpl(4096)]
		private static extern bool GetProcessInputsInternal_Injected(ref PlayableHandle handle);

		// Token: 0x04000154 RID: 340
		private PlayableHandle m_Handle;

		// Token: 0x04000155 RID: 341
		private static readonly AnimationScriptPlayable m_NullPlayable = new AnimationScriptPlayable(PlayableHandle.Null);
	}
}
