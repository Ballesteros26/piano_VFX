using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[NativeType("Modules/Animation/AnimationClip.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClip.bindings.h")]
	public sealed class AnimationClip : Motion
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000259A File Offset: 0x0000079A
		public AnimationClip()
		{
			AnimationClip.Internal_CreateAnimationClip(this);
		}

		// Token: 0x06000084 RID: 132
		[FreeFunction("AnimationClipBindings::Internal_CreateAnimationClip")]
		[MethodImpl(4096)]
		private static extern void Internal_CreateAnimationClip([Writable] AnimationClip self);

		// Token: 0x06000085 RID: 133 RVA: 0x000025AB File Offset: 0x000007AB
		public void SampleAnimation(GameObject go, float time)
		{
			AnimationClip.SampleAnimation(go, this, time, this.wrapMode);
		}

		// Token: 0x06000086 RID: 134
		[NativeHeader("Modules/Animation/AnimationUtility.h")]
		[FreeFunction]
		[MethodImpl(4096)]
		internal static extern void SampleAnimation([NotNull] GameObject go, [NotNull] AnimationClip clip, float inTime, WrapMode wrapMode);

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000087 RID: 135
		[NativeProperty("Length", false, TargetType.Function)]
		public extern float length
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000088 RID: 136
		[NativeProperty("StartTime", false, TargetType.Function)]
		internal extern float startTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000089 RID: 137
		[NativeProperty("StopTime", false, TargetType.Function)]
		internal extern float stopTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008A RID: 138
		// (set) Token: 0x0600008B RID: 139
		[NativeProperty("SampleRate", false, TargetType.Function)]
		public extern float frameRate
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600008C RID: 140
		[FreeFunction("AnimationClipBindings::Internal_SetCurve", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetCurve([NotNull] string relativePath, [NotNull] Type type, [NotNull] string propertyName, AnimationCurve curve);

		// Token: 0x0600008D RID: 141
		[MethodImpl(4096)]
		public extern void EnsureQuaternionContinuity();

		// Token: 0x0600008E RID: 142
		[MethodImpl(4096)]
		public extern void ClearCurves();

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008F RID: 143
		// (set) Token: 0x06000090 RID: 144
		[NativeProperty("WrapMode", false, TargetType.Function)]
		public extern WrapMode wrapMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000025C0 File Offset: 0x000007C0
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000025D6 File Offset: 0x000007D6
		[NativeProperty("Bounds", false, TargetType.Function)]
		public Bounds localBounds
		{
			get
			{
				Bounds bounds;
				this.get_localBounds_Injected(out bounds);
				return bounds;
			}
			set
			{
				this.set_localBounds_Injected(ref value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000093 RID: 147
		// (set) Token: 0x06000094 RID: 148
		public new extern bool legacy
		{
			[NativeMethod("IsLegacy")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetLegacy")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000095 RID: 149
		public extern bool humanMotion
		{
			[NativeMethod("IsHumanMotion")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000096 RID: 150
		public extern bool empty
		{
			[NativeMethod("IsEmpty")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000097 RID: 151
		public extern bool hasGenericRootTransform
		{
			[NativeMethod("HasGenericRootTransform")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000098 RID: 152
		public extern bool hasMotionFloatCurves
		{
			[NativeMethod("HasMotionFloatCurves")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000099 RID: 153
		public extern bool hasMotionCurves
		{
			[NativeMethod("HasMotionCurves")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009A RID: 154
		public extern bool hasRootCurves
		{
			[NativeMethod("HasRootCurves")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009B RID: 155
		internal extern bool hasRootMotion
		{
			[FreeFunction(Name = "AnimationClipBindings::Internal_GetHasRootMotion", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000025E0 File Offset: 0x000007E0
		public void AddEvent(AnimationEvent evt)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			this.AddEventInternal(evt);
		}

		// Token: 0x0600009D RID: 157
		[FreeFunction(Name = "AnimationClipBindings::AddEventInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void AddEventInternal(object evt);

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000260C File Offset: 0x0000080C
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002629 File Offset: 0x00000829
		public AnimationEvent[] events
		{
			get
			{
				return (AnimationEvent[])this.GetEventsInternal();
			}
			set
			{
				this.SetEventsInternal(value);
			}
		}

		// Token: 0x060000A0 RID: 160
		[FreeFunction(Name = "AnimationClipBindings::SetEventsInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetEventsInternal(Array value);

		// Token: 0x060000A1 RID: 161
		[FreeFunction(Name = "AnimationClipBindings::GetEventsInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Array GetEventsInternal();

		// Token: 0x060000A2 RID: 162
		[MethodImpl(4096)]
		private extern void get_localBounds_Injected(out Bounds ret);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(4096)]
		private extern void set_localBounds_Injected(ref Bounds value);
	}
}
