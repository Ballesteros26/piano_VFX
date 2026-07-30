using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000059 RID: 89
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	public struct PropertySceneHandle
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00006D90 File Offset: 0x00004F90
		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00006DAC File Offset: 0x00004FAC
		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex && this.HasValidTransform(ref stream);
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00006DE0 File Offset: 0x00004FE0
		private bool createdByNative
		{
			get
			{
				return this.valid > 0U;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00006DFC File Offset: 0x00004FFC
		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00006E1A File Offset: 0x0000501A
		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			this.ResolveInternal(ref stream);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00006E30 File Offset: 0x00005030
		public bool IsResolved(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsBound(ref stream);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00006E58 File Offset: 0x00005058
		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = !this.createdByNative || !this.hasHandleIndex;
			if (flag)
			{
				throw new InvalidOperationException("The PropertySceneHandle is invalid. Please use proper function to create the handle.");
			}
			bool flag2 = !this.HasValidTransform(ref stream);
			if (flag2)
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00006EAC File Offset: 0x000050AC
		public float GetFloat(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetFloatInternal(ref stream);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetFloat(AnimationStream stream, float value)
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00006ED0 File Offset: 0x000050D0
		public int GetInt(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetIntInternal(ref stream);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetInt(AnimationStream stream, int value)
		{
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00006EF4 File Offset: 0x000050F4
		public bool GetBool(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetBoolInternal(ref stream);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetBool(AnimationStream stream, bool value)
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00006F17 File Offset: 0x00005117
		[ThreadSafe]
		private bool HasValidTransform(ref AnimationStream stream)
		{
			return PropertySceneHandle.HasValidTransform_Injected(ref this, ref stream);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00006F20 File Offset: 0x00005120
		[ThreadSafe]
		private bool IsBound(ref AnimationStream stream)
		{
			return PropertySceneHandle.IsBound_Injected(ref this, ref stream);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00006F29 File Offset: 0x00005129
		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			PropertySceneHandle.ResolveInternal_Injected(ref this, ref stream);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00006F32 File Offset: 0x00005132
		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		private float GetFloatInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetFloatInternal_Injected(ref this, ref stream);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00006F3B File Offset: 0x0000513B
		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		private int GetIntInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetIntInternal_Injected(ref this, ref stream);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00006F44 File Offset: 0x00005144
		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		private bool GetBoolInternal(ref AnimationStream stream)
		{
			return PropertySceneHandle.GetBoolInternal_Injected(ref this, ref stream);
		}

		// Token: 0x060004C3 RID: 1219
		[MethodImpl(4096)]
		private static extern bool HasValidTransform_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004C4 RID: 1220
		[MethodImpl(4096)]
		private static extern bool IsBound_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004C5 RID: 1221
		[MethodImpl(4096)]
		private static extern void ResolveInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004C6 RID: 1222
		[MethodImpl(4096)]
		private static extern float GetFloatInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004C7 RID: 1223
		[MethodImpl(4096)]
		private static extern int GetIntInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x060004C8 RID: 1224
		[MethodImpl(4096)]
		private static extern bool GetBoolInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		// Token: 0x04000173 RID: 371
		private uint valid;

		// Token: 0x04000174 RID: 372
		private int handleIndex;
	}
}
