using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000056 RID: 86
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct TransformStreamHandle
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000623C File Offset: 0x0000443C
		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00006258 File Offset: 0x00004458
		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00006284 File Offset: 0x00004484
		private bool createdByNative
		{
			get
			{
				return this.animatorBindingsVersion > 0U;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000062A0 File Offset: 0x000044A0
		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return this.animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000062C0 File Offset: 0x000044C0
		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000062E0 File Offset: 0x000044E0
		private bool hasSkeletonIndex
		{
			get
			{
				return this.skeletonIndex != -1;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00006308 File Offset: 0x00004508
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x000062FE File Offset: 0x000044FE
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
			private set
			{
				this.m_AnimatorBindingsVersion = value;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00006320 File Offset: 0x00004520
		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000632C File Offset: 0x0000452C
		public bool IsResolved(AnimationStream stream)
		{
			return this.IsResolvedInternal(ref stream);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00006348 File Offset: 0x00004548
		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsSameVersionAsStream(ref stream) && this.hasSkeletonIndex;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00006378 File Offset: 0x00004578
		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = this.IsResolvedInternal(ref stream);
			if (!flag)
			{
				bool flag2 = !this.createdByNative || !this.hasHandleIndex;
				if (flag2)
				{
					throw new InvalidOperationException("The TransformStreamHandle is invalid. Please use proper function to create the handle.");
				}
				bool flag3 = !this.IsSameVersionAsStream(ref stream) || (this.hasHandleIndex && !this.hasSkeletonIndex);
				if (flag3)
				{
					this.ResolveInternal(ref stream);
				}
				bool flag4 = this.hasHandleIndex && !this.hasSkeletonIndex;
				if (flag4)
				{
					throw new InvalidOperationException("The TransformStreamHandle cannot be resolved.");
				}
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00006410 File Offset: 0x00004610
		public Vector3 GetPosition(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetPositionInternal(ref stream);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00006433 File Offset: 0x00004633
		public void SetPosition(AnimationStream stream, Vector3 position)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetPositionInternal(ref stream, position);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000644C File Offset: 0x0000464C
		public Quaternion GetRotation(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetRotationInternal(ref stream);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000646F File Offset: 0x0000466F
		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetRotationInternal(ref stream, rotation);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00006488 File Offset: 0x00004688
		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalPositionInternal(ref stream);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000064AB File Offset: 0x000046AB
		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalPositionInternal(ref stream, position);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000064C4 File Offset: 0x000046C4
		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalRotationInternal(ref stream);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000064E7 File Offset: 0x000046E7
		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalRotationInternal(ref stream, rotation);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00006500 File Offset: 0x00004700
		public Vector3 GetLocalScale(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetLocalScaleInternal(ref stream);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00006523 File Offset: 0x00004723
		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalScaleInternal(ref stream, scale);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000653C File Offset: 0x0000473C
		public bool GetPositionReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetPositionReadMaskInternal(ref stream);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00006560 File Offset: 0x00004760
		public bool GetRotationReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetRotationReadMaskInternal(ref stream);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00006584 File Offset: 0x00004784
		public bool GetScaleReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetScaleReadMaskInternal(ref stream);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000065A7 File Offset: 0x000047A7
		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000065C0 File Offset: 0x000047C0
		public void SetLocalTRS(AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetLocalTRSInternal(ref stream, position, rotation, scale, useMask);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000065DB File Offset: 0x000047DB
		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000065F2 File Offset: 0x000047F2
		public void SetGlobalTR(AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			this.CheckIsValidAndResolve(ref stream);
			this.SetGlobalTRInternal(ref stream, position, rotation, useMask);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000660B File Offset: 0x0000480B
		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			TransformStreamHandle.ResolveInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00006614 File Offset: 0x00004814
		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformStreamHandle.GetPositionInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000662B File Offset: 0x0000482B
		[NativeMethod(Name = "TransformStreamHandleBindings::SetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			TransformStreamHandle.SetPositionInternal_Injected(ref this, ref stream, ref position);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00006638 File Offset: 0x00004838
		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			Quaternion quaternion;
			TransformStreamHandle.GetRotationInternal_Injected(ref this, ref stream, out quaternion);
			return quaternion;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000664F File Offset: 0x0000484F
		[NativeMethod(Name = "TransformStreamHandleBindings::SetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			TransformStreamHandle.SetRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000665C File Offset: 0x0000485C
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformStreamHandle.GetLocalPositionInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00006673 File Offset: 0x00004873
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			TransformStreamHandle.SetLocalPositionInternal_Injected(ref this, ref stream, ref position);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00006680 File Offset: 0x00004880
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			Quaternion quaternion;
			TransformStreamHandle.GetLocalRotationInternal_Injected(ref this, ref stream, out quaternion);
			return quaternion;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00006697 File Offset: 0x00004897
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			TransformStreamHandle.SetLocalRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000066A4 File Offset: 0x000048A4
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformStreamHandle.GetLocalScaleInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000066BB File Offset: 0x000048BB
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalScaleInternal(ref AnimationStream stream, Vector3 scale)
		{
			TransformStreamHandle.SetLocalScaleInternal_Injected(ref this, ref stream, ref scale);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000066C6 File Offset: 0x000048C6
		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetPositionReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetPositionReadMaskInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000066CF File Offset: 0x000048CF
		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetRotationReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetRotationReadMaskInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000066D8 File Offset: 0x000048D8
		[NativeMethod(Name = "TransformStreamHandleBindings::GetScaleReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetScaleReadMaskInternal(ref AnimationStream stream)
		{
			return TransformStreamHandle.GetScaleReadMaskInternal_Injected(ref this, ref stream);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000066E1 File Offset: 0x000048E1
		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			TransformStreamHandle.GetLocalTRSInternal_Injected(ref this, ref stream, out position, out rotation, out scale);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000066EE File Offset: 0x000048EE
		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalTRSInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			TransformStreamHandle.SetLocalTRSInternal_Injected(ref this, ref stream, ref position, ref rotation, ref scale, useMask);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000066FF File Offset: 0x000048FF
		[NativeMethod(Name = "TransformStreamHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			TransformStreamHandle.GetGlobalTRInternal_Injected(ref this, ref stream, out position, out rotation);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000670A File Offset: 0x0000490A
		[NativeMethod(Name = "TransformStreamHandleBindings::SetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetGlobalTRInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			TransformStreamHandle.SetGlobalTRInternal_Injected(ref this, ref stream, ref position, ref rotation, useMask);
		}

		// Token: 0x0600045A RID: 1114
		[MethodImpl(4096)]
		private static extern void ResolveInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x0600045B RID: 1115
		[MethodImpl(4096)]
		private static extern void GetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x0600045C RID: 1116
		[MethodImpl(4096)]
		private static extern void SetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position);

		// Token: 0x0600045D RID: 1117
		[MethodImpl(4096)]
		private static extern void GetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x0600045E RID: 1118
		[MethodImpl(4096)]
		private static extern void SetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Quaternion rotation);

		// Token: 0x0600045F RID: 1119
		[MethodImpl(4096)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x06000460 RID: 1120
		[MethodImpl(4096)]
		private static extern void SetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position);

		// Token: 0x06000461 RID: 1121
		[MethodImpl(4096)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x06000462 RID: 1122
		[MethodImpl(4096)]
		private static extern void SetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Quaternion rotation);

		// Token: 0x06000463 RID: 1123
		[MethodImpl(4096)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x06000464 RID: 1124
		[MethodImpl(4096)]
		private static extern void SetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 scale);

		// Token: 0x06000465 RID: 1125
		[MethodImpl(4096)]
		private static extern bool GetPositionReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x06000466 RID: 1126
		[MethodImpl(4096)]
		private static extern bool GetRotationReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x06000467 RID: 1127
		[MethodImpl(4096)]
		private static extern bool GetScaleReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		// Token: 0x06000468 RID: 1128
		[MethodImpl(4096)]
		private static extern void GetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		// Token: 0x06000469 RID: 1129
		[MethodImpl(4096)]
		private static extern void SetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position, ref Quaternion rotation, ref Vector3 scale, bool useMask);

		// Token: 0x0600046A RID: 1130
		[MethodImpl(4096)]
		private static extern void GetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		// Token: 0x0600046B RID: 1131
		[MethodImpl(4096)]
		private static extern void SetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position, ref Quaternion rotation, bool useMask);

		// Token: 0x0400016A RID: 362
		private uint m_AnimatorBindingsVersion;

		// Token: 0x0400016B RID: 363
		private int handleIndex;

		// Token: 0x0400016C RID: 364
		private int skeletonIndex;
	}
}
