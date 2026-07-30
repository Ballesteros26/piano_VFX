using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000058 RID: 88
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	public struct TransformSceneHandle
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x00006B4C File Offset: 0x00004D4C
		public bool IsValid(AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasTransformSceneHandleDefinitionIndex && this.HasValidTransform(ref stream);
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00006B84 File Offset: 0x00004D84
		private bool createdByNative
		{
			get
			{
				return this.valid > 0U;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00006BA0 File Offset: 0x00004DA0
		private bool hasTransformSceneHandleDefinitionIndex
		{
			get
			{
				return this.transformSceneHandleDefinitionIndex != -1;
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00006BC0 File Offset: 0x00004DC0
		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = !this.createdByNative || !this.hasTransformSceneHandleDefinitionIndex;
			if (flag)
			{
				throw new InvalidOperationException("The TransformSceneHandle is invalid. Please use proper function to create the handle.");
			}
			bool flag2 = !this.HasValidTransform(ref stream);
			if (flag2)
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00006C14 File Offset: 0x00004E14
		public Vector3 GetPosition(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetPositionInternal(ref stream);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetPosition(AnimationStream stream, Vector3 position)
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00006C38 File Offset: 0x00004E38
		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalPositionInternal(ref stream);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00006C5C File Offset: 0x00004E5C
		public Quaternion GetRotation(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetRotationInternal(ref stream);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00006C80 File Offset: 0x00004E80
		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalRotationInternal(ref stream);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public Vector3 GetLocalScale(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetLocalScaleInternal(ref stream);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00006CC7 File Offset: 0x00004EC7
		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			this.CheckIsValid(ref stream);
			this.GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00006CE0 File Offset: 0x00004EE0
		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			this.CheckIsValid(ref stream);
			this.GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00006CF7 File Offset: 0x00004EF7
		[ThreadSafe]
		private bool HasValidTransform(ref AnimationStream stream)
		{
			return TransformSceneHandle.HasValidTransform_Injected(ref this, ref stream);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00006D00 File Offset: 0x00004F00
		[NativeMethod(Name = "TransformSceneHandleBindings::GetPositionInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformSceneHandle.GetPositionInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00006D18 File Offset: 0x00004F18
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformSceneHandle.GetLocalPositionInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00006D30 File Offset: 0x00004F30
		[NativeMethod(Name = "TransformSceneHandleBindings::GetRotationInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			Quaternion quaternion;
			TransformSceneHandle.GetRotationInternal_Injected(ref this, ref stream, out quaternion);
			return quaternion;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00006D48 File Offset: 0x00004F48
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			Quaternion quaternion;
			TransformSceneHandle.GetLocalRotationInternal_Injected(ref this, ref stream, out quaternion);
			return quaternion;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00006D60 File Offset: 0x00004F60
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			Vector3 vector;
			TransformSceneHandle.GetLocalScaleInternal_Injected(ref this, ref stream, out vector);
			return vector;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00006D77 File Offset: 0x00004F77
		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			TransformSceneHandle.GetLocalTRSInternal_Injected(ref this, ref stream, out position, out rotation, out scale);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00006D84 File Offset: 0x00004F84
		[NativeMethod(Name = "TransformSceneHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			TransformSceneHandle.GetGlobalTRInternal_Injected(ref this, ref stream, out position, out rotation);
		}

		// Token: 0x060004A8 RID: 1192
		[MethodImpl(4096)]
		private static extern bool HasValidTransform_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004A9 RID: 1193
		[MethodImpl(4096)]
		private static extern void GetPositionInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060004AA RID: 1194
		[MethodImpl(4096)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060004AB RID: 1195
		[MethodImpl(4096)]
		private static extern void GetRotationInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x060004AC RID: 1196
		[MethodImpl(4096)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		// Token: 0x060004AD RID: 1197
		[MethodImpl(4096)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		// Token: 0x060004AE RID: 1198
		[MethodImpl(4096)]
		private static extern void GetLocalTRSInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		// Token: 0x060004AF RID: 1199
		[MethodImpl(4096)]
		private static extern void GetGlobalTRInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		// Token: 0x04000171 RID: 369
		private uint valid;

		// Token: 0x04000172 RID: 370
		private int transformSceneHandleDefinitionIndex;
	}
}
