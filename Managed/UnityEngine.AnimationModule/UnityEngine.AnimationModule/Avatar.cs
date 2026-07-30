using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/Animation/Avatar.h")]
	[UsedByNativeCode]
	public class Avatar : Object
	{
		// Token: 0x06000200 RID: 512 RVA: 0x000039AF File Offset: 0x00001BAF
		private Avatar()
		{
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000201 RID: 513
		public extern bool isValid
		{
			[NativeMethod("IsValid")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000202 RID: 514
		public extern bool isHuman
		{
			[NativeMethod("IsHuman")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000039BC File Offset: 0x00001BBC
		public HumanDescription humanDescription
		{
			get
			{
				HumanDescription humanDescription;
				this.get_humanDescription_Injected(out humanDescription);
				return humanDescription;
			}
		}

		// Token: 0x06000204 RID: 516
		[MethodImpl(4096)]
		internal extern void SetMuscleMinMax(int muscleId, float min, float max);

		// Token: 0x06000205 RID: 517
		[MethodImpl(4096)]
		internal extern void SetParameter(int parameterId, float value);

		// Token: 0x06000206 RID: 518 RVA: 0x000039D4 File Offset: 0x00001BD4
		internal float GetAxisLength(int humanId)
		{
			return this.Internal_GetAxisLength(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000039F4 File Offset: 0x00001BF4
		internal Quaternion GetPreRotation(int humanId)
		{
			return this.Internal_GetPreRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00003A14 File Offset: 0x00001C14
		internal Quaternion GetPostRotation(int humanId)
		{
			return this.Internal_GetPostRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00003A34 File Offset: 0x00001C34
		internal Quaternion GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			return this.Internal_GetZYPostQ(HumanTrait.GetBoneIndexFromMono(humanId), parentQ, q);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00003A54 File Offset: 0x00001C54
		internal Quaternion GetZYRoll(int humanId, Vector3 uvw)
		{
			return this.Internal_GetZYRoll(HumanTrait.GetBoneIndexFromMono(humanId), uvw);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00003A74 File Offset: 0x00001C74
		internal Vector3 GetLimitSign(int humanId)
		{
			return this.Internal_GetLimitSign(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x0600020C RID: 524
		[NativeMethod("GetAxisLength")]
		[MethodImpl(4096)]
		internal extern float Internal_GetAxisLength(int humanId);

		// Token: 0x0600020D RID: 525 RVA: 0x00003A94 File Offset: 0x00001C94
		[NativeMethod("GetPreRotation")]
		internal Quaternion Internal_GetPreRotation(int humanId)
		{
			Quaternion quaternion;
			this.Internal_GetPreRotation_Injected(humanId, out quaternion);
			return quaternion;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00003AAC File Offset: 0x00001CAC
		[NativeMethod("GetPostRotation")]
		internal Quaternion Internal_GetPostRotation(int humanId)
		{
			Quaternion quaternion;
			this.Internal_GetPostRotation_Injected(humanId, out quaternion);
			return quaternion;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00003AC4 File Offset: 0x00001CC4
		[NativeMethod("GetZYPostQ")]
		internal Quaternion Internal_GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			Quaternion quaternion;
			this.Internal_GetZYPostQ_Injected(humanId, ref parentQ, ref q, out quaternion);
			return quaternion;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00003AE0 File Offset: 0x00001CE0
		[NativeMethod("GetZYRoll")]
		internal Quaternion Internal_GetZYRoll(int humanId, Vector3 uvw)
		{
			Quaternion quaternion;
			this.Internal_GetZYRoll_Injected(humanId, ref uvw, out quaternion);
			return quaternion;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00003AFC File Offset: 0x00001CFC
		[NativeMethod("GetLimitSign")]
		internal Vector3 Internal_GetLimitSign(int humanId)
		{
			Vector3 vector;
			this.Internal_GetLimitSign_Injected(humanId, out vector);
			return vector;
		}

		// Token: 0x06000212 RID: 530
		[MethodImpl(4096)]
		private extern void get_humanDescription_Injected(out HumanDescription ret);

		// Token: 0x06000213 RID: 531
		[MethodImpl(4096)]
		private extern void Internal_GetPreRotation_Injected(int humanId, out Quaternion ret);

		// Token: 0x06000214 RID: 532
		[MethodImpl(4096)]
		private extern void Internal_GetPostRotation_Injected(int humanId, out Quaternion ret);

		// Token: 0x06000215 RID: 533
		[MethodImpl(4096)]
		private extern void Internal_GetZYPostQ_Injected(int humanId, ref Quaternion parentQ, ref Quaternion q, out Quaternion ret);

		// Token: 0x06000216 RID: 534
		[MethodImpl(4096)]
		private extern void Internal_GetZYRoll_Injected(int humanId, ref Vector3 uvw, out Quaternion ret);

		// Token: 0x06000217 RID: 535
		[MethodImpl(4096)]
		private extern void Internal_GetLimitSign_Injected(int humanId, out Vector3 ret);
	}
}
