using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000054 RID: 84
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStream.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[RequiredByNativeCode]
	public struct AnimationStream
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00005F10 File Offset: 0x00004110
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00005F28 File Offset: 0x00004128
		public bool isValid
		{
			get
			{
				return this.m_AnimatorBindingsVersion >= 2U && this.constant != IntPtr.Zero && this.input != IntPtr.Zero && this.output != IntPtr.Zero && this.workspace != IntPtr.Zero && this.animationHandleBinder != IntPtr.Zero;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00005FA0 File Offset: 0x000041A0
		internal void CheckIsValid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new InvalidOperationException("The AnimationStream is invalid.");
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00005FC8 File Offset: 0x000041C8
		public float deltaTime
		{
			get
			{
				this.CheckIsValid();
				return this.GetDeltaTime();
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00005FE8 File Offset: 0x000041E8
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00006007 File Offset: 0x00004207
		public Vector3 velocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetVelocity(value);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000601C File Offset: 0x0000421C
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000603B File Offset: 0x0000423B
		public Vector3 angularVelocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetAngularVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetAngularVelocity(value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00006050 File Offset: 0x00004250
		public Vector3 rootMotionPosition
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionPosition();
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00006070 File Offset: 0x00004270
		public Quaternion rootMotionRotation
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionRotation();
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00006090 File Offset: 0x00004290
		public bool isHumanStream
		{
			get
			{
				this.CheckIsValid();
				return this.GetIsHumanStream();
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000060B0 File Offset: 0x000042B0
		public AnimationHumanStream AsHuman()
		{
			this.CheckIsValid();
			bool flag = !this.GetIsHumanStream();
			if (flag)
			{
				throw new InvalidOperationException("Cannot create an AnimationHumanStream for a generic rig.");
			}
			return this.GetHumanStream();
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000060E8 File Offset: 0x000042E8
		public int inputStreamCount
		{
			get
			{
				this.CheckIsValid();
				return this.GetInputStreamCount();
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00006108 File Offset: 0x00004308
		public AnimationStream GetInputStream(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputStream(index);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00006128 File Offset: 0x00004328
		public float GetInputWeight(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputWeight(index);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00006148 File Offset: 0x00004348
		private void ReadSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalReadSceneTransforms();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00006159 File Offset: 0x00004359
		private void WriteSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalWriteSceneTransforms();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000616A File Offset: 0x0000436A
		[NativeMethod(IsThreadSafe = true)]
		private float GetDeltaTime()
		{
			return AnimationStream.GetDeltaTime_Injected(ref this);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00006172 File Offset: 0x00004372
		[NativeMethod(IsThreadSafe = true)]
		private bool GetIsHumanStream()
		{
			return AnimationStream.GetIsHumanStream_Injected(ref this);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000617C File Offset: 0x0000437C
		[NativeMethod(Name = "AnimationStreamBindings::GetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetVelocity()
		{
			Vector3 vector;
			AnimationStream.GetVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00006192 File Offset: 0x00004392
		[NativeMethod(Name = "AnimationStreamBindings::SetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetVelocity(Vector3 velocity)
		{
			AnimationStream.SetVelocity_Injected(ref this, ref velocity);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000619C File Offset: 0x0000439C
		[NativeMethod(Name = "AnimationStreamBindings::GetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetAngularVelocity()
		{
			Vector3 vector;
			AnimationStream.GetAngularVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000061B2 File Offset: 0x000043B2
		[NativeMethod(Name = "AnimationStreamBindings::SetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetAngularVelocity(Vector3 velocity)
		{
			AnimationStream.SetAngularVelocity_Injected(ref this, ref velocity);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000061BC File Offset: 0x000043BC
		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRootMotionPosition()
		{
			Vector3 vector;
			AnimationStream.GetRootMotionPosition_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000061D4 File Offset: 0x000043D4
		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRootMotionRotation()
		{
			Quaternion quaternion;
			AnimationStream.GetRootMotionRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000061EA File Offset: 0x000043EA
		[NativeMethod(IsThreadSafe = true)]
		private int GetInputStreamCount()
		{
			return AnimationStream.GetInputStreamCount_Injected(ref this);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000061F4 File Offset: 0x000043F4
		[NativeMethod(Name = "GetInputStream", IsThreadSafe = true)]
		private AnimationStream InternalGetInputStream(int index)
		{
			AnimationStream animationStream;
			AnimationStream.InternalGetInputStream_Injected(ref this, index, out animationStream);
			return animationStream;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000620B File Offset: 0x0000440B
		[NativeMethod(Name = "GetInputWeight", IsThreadSafe = true)]
		private float InternalGetInputWeight(int index)
		{
			return AnimationStream.InternalGetInputWeight_Injected(ref this, index);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00006214 File Offset: 0x00004414
		[NativeMethod(IsThreadSafe = true)]
		private AnimationHumanStream GetHumanStream()
		{
			AnimationHumanStream animationHumanStream;
			AnimationStream.GetHumanStream_Injected(ref this, out animationHumanStream);
			return animationHumanStream;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000622A File Offset: 0x0000442A
		[NativeMethod(Name = "ReadSceneTransforms", IsThreadSafe = true)]
		private void InternalReadSceneTransforms()
		{
			AnimationStream.InternalReadSceneTransforms_Injected(ref this);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00006232 File Offset: 0x00004432
		[NativeMethod(Name = "WriteSceneTransforms", IsThreadSafe = true)]
		private void InternalWriteSceneTransforms()
		{
			AnimationStream.InternalWriteSceneTransforms_Injected(ref this);
		}

		// Token: 0x0600041D RID: 1053
		[MethodImpl(4096)]
		private static extern float GetDeltaTime_Injected(ref AnimationStream _unity_self);

		// Token: 0x0600041E RID: 1054
		[MethodImpl(4096)]
		private static extern bool GetIsHumanStream_Injected(ref AnimationStream _unity_self);

		// Token: 0x0600041F RID: 1055
		[MethodImpl(4096)]
		private static extern void GetVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x06000420 RID: 1056
		[MethodImpl(4096)]
		private static extern void SetVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		// Token: 0x06000421 RID: 1057
		[MethodImpl(4096)]
		private static extern void GetAngularVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x06000422 RID: 1058
		[MethodImpl(4096)]
		private static extern void SetAngularVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		// Token: 0x06000423 RID: 1059
		[MethodImpl(4096)]
		private static extern void GetRootMotionPosition_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x06000424 RID: 1060
		[MethodImpl(4096)]
		private static extern void GetRootMotionRotation_Injected(ref AnimationStream _unity_self, out Quaternion ret);

		// Token: 0x06000425 RID: 1061
		[MethodImpl(4096)]
		private static extern int GetInputStreamCount_Injected(ref AnimationStream _unity_self);

		// Token: 0x06000426 RID: 1062
		[MethodImpl(4096)]
		private static extern void InternalGetInputStream_Injected(ref AnimationStream _unity_self, int index, out AnimationStream ret);

		// Token: 0x06000427 RID: 1063
		[MethodImpl(4096)]
		private static extern float InternalGetInputWeight_Injected(ref AnimationStream _unity_self, int index);

		// Token: 0x06000428 RID: 1064
		[MethodImpl(4096)]
		private static extern void GetHumanStream_Injected(ref AnimationStream _unity_self, out AnimationHumanStream ret);

		// Token: 0x06000429 RID: 1065
		[MethodImpl(4096)]
		private static extern void InternalReadSceneTransforms_Injected(ref AnimationStream _unity_self);

		// Token: 0x0600042A RID: 1066
		[MethodImpl(4096)]
		private static extern void InternalWriteSceneTransforms_Injected(ref AnimationStream _unity_self);

		// Token: 0x0400015A RID: 346
		private uint m_AnimatorBindingsVersion;

		// Token: 0x0400015B RID: 347
		private IntPtr constant;

		// Token: 0x0400015C RID: 348
		private IntPtr input;

		// Token: 0x0400015D RID: 349
		private IntPtr output;

		// Token: 0x0400015E RID: 350
		private IntPtr workspace;

		// Token: 0x0400015F RID: 351
		private IntPtr inputStreamAccessor;

		// Token: 0x04000160 RID: 352
		private IntPtr animationHandleBinder;

		// Token: 0x04000161 RID: 353
		internal const int InvalidIndex = -1;
	}
}
