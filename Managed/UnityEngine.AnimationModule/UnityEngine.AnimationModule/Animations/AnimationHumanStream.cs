using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000048 RID: 72
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationHumanStream.bindings.h")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/Director/AnimationHumanStream.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct AnimationHumanStream
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00004A80 File Offset: 0x00002C80
		public bool isValid
		{
			get
			{
				return this.stream != IntPtr.Zero;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00004AA4 File Offset: 0x00002CA4
		private void ThrowIfInvalid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new InvalidOperationException("The AnimationHumanStream is invalid.");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00004ACC File Offset: 0x00002CCC
		public float humanScale
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetHumanScale();
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00004AEC File Offset: 0x00002CEC
		public float leftFootHeight
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetFootHeight(true);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00004B0C File Offset: 0x00002D0C
		public float rightFootHeight
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetFootHeight(false);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00004B2C File Offset: 0x00002D2C
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00004B4B File Offset: 0x00002D4B
		public Vector3 bodyLocalPosition
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyLocalPosition();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyLocalPosition(value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00004B60 File Offset: 0x00002D60
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00004B7F File Offset: 0x00002D7F
		public Quaternion bodyLocalRotation
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyLocalRotation();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyLocalRotation(value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00004B94 File Offset: 0x00002D94
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00004BB3 File Offset: 0x00002DB3
		public Vector3 bodyPosition
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyPosition();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyPosition(value);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00004BC8 File Offset: 0x00002DC8
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00004BE7 File Offset: 0x00002DE7
		public Quaternion bodyRotation
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyRotation();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyRotation(value);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00004BFC File Offset: 0x00002DFC
		public float GetMuscle(MuscleHandle muscle)
		{
			this.ThrowIfInvalid();
			return this.InternalGetMuscle(muscle);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00004C1C File Offset: 0x00002E1C
		public void SetMuscle(MuscleHandle muscle, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetMuscle(muscle, value);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00004C30 File Offset: 0x00002E30
		public Vector3 leftFootVelocity
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetLeftFootVelocity();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00004C50 File Offset: 0x00002E50
		public Vector3 rightFootVelocity
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetRightFootVelocity();
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00004C6F File Offset: 0x00002E6F
		public void ResetToStancePose()
		{
			this.ThrowIfInvalid();
			this.InternalResetToStancePose();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00004C80 File Offset: 0x00002E80
		public Vector3 GetGoalPositionFromPose(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalPositionFromPose(index);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public Quaternion GetGoalRotationFromPose(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalRotationFromPose(index);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public Vector3 GetGoalLocalPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalLocalPosition(index);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public void SetGoalLocalPosition(AvatarIKGoal index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalLocalPosition(index, pos);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public Quaternion GetGoalLocalRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalLocalRotation(index);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00004D14 File Offset: 0x00002F14
		public void SetGoalLocalRotation(AvatarIKGoal index, Quaternion rot)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalLocalRotation(index, rot);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00004D28 File Offset: 0x00002F28
		public Vector3 GetGoalPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalPosition(index);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00004D48 File Offset: 0x00002F48
		public void SetGoalPosition(AvatarIKGoal index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalPosition(index, pos);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00004D5C File Offset: 0x00002F5C
		public Quaternion GetGoalRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalRotation(index);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00004D7C File Offset: 0x00002F7C
		public void SetGoalRotation(AvatarIKGoal index, Quaternion rot)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalRotation(index, rot);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00004D8F File Offset: 0x00002F8F
		public void SetGoalWeightPosition(AvatarIKGoal index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalWeightPosition(index, value);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00004DA2 File Offset: 0x00002FA2
		public void SetGoalWeightRotation(AvatarIKGoal index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalWeightRotation(index, value);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public float GetGoalWeightPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalWeightPosition(index);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00004DD8 File Offset: 0x00002FD8
		public float GetGoalWeightRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalWeightRotation(index);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public Vector3 GetHintPosition(AvatarIKHint index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetHintPosition(index);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00004E18 File Offset: 0x00003018
		public void SetHintPosition(AvatarIKHint index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetHintPosition(index, pos);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00004E2B File Offset: 0x0000302B
		public void SetHintWeightPosition(AvatarIKHint index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetHintWeightPosition(index, value);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00004E40 File Offset: 0x00003040
		public float GetHintWeightPosition(AvatarIKHint index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetHintWeightPosition(index);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00004E60 File Offset: 0x00003060
		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtPosition(lookAtPosition);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00004E72 File Offset: 0x00003072
		public void SetLookAtClampWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtClampWeight(weight);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00004E84 File Offset: 0x00003084
		public void SetLookAtBodyWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtBodyWeight(weight);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00004E96 File Offset: 0x00003096
		public void SetLookAtHeadWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtHeadWeight(weight);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00004EA8 File Offset: 0x000030A8
		public void SetLookAtEyesWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtEyesWeight(weight);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00004EBA File Offset: 0x000030BA
		public void SolveIK()
		{
			this.ThrowIfInvalid();
			this.InternalSolveIK();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00004ECB File Offset: 0x000030CB
		[NativeMethod(IsThreadSafe = true)]
		private float GetHumanScale()
		{
			return AnimationHumanStream.GetHumanScale_Injected(ref this);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00004ED3 File Offset: 0x000030D3
		[NativeMethod(IsThreadSafe = true)]
		private float GetFootHeight(bool left)
		{
			return AnimationHumanStream.GetFootHeight_Injected(ref this, left);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00004EDC File Offset: 0x000030DC
		[NativeMethod(Name = "ResetToStancePose", IsThreadSafe = true)]
		private void InternalResetToStancePose()
		{
			AnimationHumanStream.InternalResetToStancePose_Injected(ref this);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00004EE4 File Offset: 0x000030E4
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalPositionFromPose", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalPositionFromPose(AvatarIKGoal index)
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetGoalPositionFromPose_Injected(ref this, index, out vector);
			return vector;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00004EFC File Offset: 0x000030FC
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalRotationFromPose", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalRotationFromPose(AvatarIKGoal index)
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetGoalRotationFromPose_Injected(ref this, index, out quaternion);
			return quaternion;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00004F14 File Offset: 0x00003114
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetBodyLocalPosition()
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetBodyLocalPosition_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00004F2A File Offset: 0x0000312A
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyLocalPosition(Vector3 value)
		{
			AnimationHumanStream.InternalSetBodyLocalPosition_Injected(ref this, ref value);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00004F34 File Offset: 0x00003134
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetBodyLocalRotation()
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetBodyLocalRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00004F4A File Offset: 0x0000314A
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyLocalRotation(Quaternion value)
		{
			AnimationHumanStream.InternalSetBodyLocalRotation_Injected(ref this, ref value);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00004F54 File Offset: 0x00003154
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetBodyPosition()
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetBodyPosition_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00004F6A File Offset: 0x0000316A
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyPosition(Vector3 value)
		{
			AnimationHumanStream.InternalSetBodyPosition_Injected(ref this, ref value);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00004F74 File Offset: 0x00003174
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetBodyRotation()
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetBodyRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00004F8A File Offset: 0x0000318A
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyRotation(Quaternion value)
		{
			AnimationHumanStream.InternalSetBodyRotation_Injected(ref this, ref value);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00004F94 File Offset: 0x00003194
		[NativeMethod(Name = "GetMuscle", IsThreadSafe = true)]
		private float InternalGetMuscle(MuscleHandle muscle)
		{
			return AnimationHumanStream.InternalGetMuscle_Injected(ref this, ref muscle);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00004F9E File Offset: 0x0000319E
		[NativeMethod(Name = "SetMuscle", IsThreadSafe = true)]
		private void InternalSetMuscle(MuscleHandle muscle, float value)
		{
			AnimationHumanStream.InternalSetMuscle_Injected(ref this, ref muscle, value);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00004FAC File Offset: 0x000031AC
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetLeftFootVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLeftFootVelocity()
		{
			Vector3 vector;
			AnimationHumanStream.GetLeftFootVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00004FC4 File Offset: 0x000031C4
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetRightFootVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRightFootVelocity()
		{
			Vector3 vector;
			AnimationHumanStream.GetRightFootVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00004FDC File Offset: 0x000031DC
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalLocalPosition(AvatarIKGoal index)
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetGoalLocalPosition_Injected(ref this, index, out vector);
			return vector;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00004FF3 File Offset: 0x000031F3
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalLocalPosition(AvatarIKGoal index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetGoalLocalPosition_Injected(ref this, index, ref pos);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00005000 File Offset: 0x00003200
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalLocalRotation(AvatarIKGoal index)
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetGoalLocalRotation_Injected(ref this, index, out quaternion);
			return quaternion;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00005017 File Offset: 0x00003217
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalLocalRotation(AvatarIKGoal index, Quaternion rot)
		{
			AnimationHumanStream.InternalSetGoalLocalRotation_Injected(ref this, index, ref rot);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00005024 File Offset: 0x00003224
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalPosition(AvatarIKGoal index)
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetGoalPosition_Injected(ref this, index, out vector);
			return vector;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000503B File Offset: 0x0000323B
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalPosition(AvatarIKGoal index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetGoalPosition_Injected(ref this, index, ref pos);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00005048 File Offset: 0x00003248
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalRotation(AvatarIKGoal index)
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetGoalRotation_Injected(ref this, index, out quaternion);
			return quaternion;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000505F File Offset: 0x0000325F
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalRotation(AvatarIKGoal index, Quaternion rot)
		{
			AnimationHumanStream.InternalSetGoalRotation_Injected(ref this, index, ref rot);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000506A File Offset: 0x0000326A
		[NativeMethod(Name = "SetGoalWeightPosition", IsThreadSafe = true)]
		private void InternalSetGoalWeightPosition(AvatarIKGoal index, float value)
		{
			AnimationHumanStream.InternalSetGoalWeightPosition_Injected(ref this, index, value);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00005074 File Offset: 0x00003274
		[NativeMethod(Name = "SetGoalWeightRotation", IsThreadSafe = true)]
		private void InternalSetGoalWeightRotation(AvatarIKGoal index, float value)
		{
			AnimationHumanStream.InternalSetGoalWeightRotation_Injected(ref this, index, value);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000507E File Offset: 0x0000327E
		[NativeMethod(Name = "GetGoalWeightPosition", IsThreadSafe = true)]
		private float InternalGetGoalWeightPosition(AvatarIKGoal index)
		{
			return AnimationHumanStream.InternalGetGoalWeightPosition_Injected(ref this, index);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00005087 File Offset: 0x00003287
		[NativeMethod(Name = "GetGoalWeightRotation", IsThreadSafe = true)]
		private float InternalGetGoalWeightRotation(AvatarIKGoal index)
		{
			return AnimationHumanStream.InternalGetGoalWeightRotation_Injected(ref this, index);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00005090 File Offset: 0x00003290
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetHintPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetHintPosition(AvatarIKHint index)
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetHintPosition_Injected(ref this, index, out vector);
			return vector;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000050A7 File Offset: 0x000032A7
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetHintPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetHintPosition(AvatarIKHint index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetHintPosition_Injected(ref this, index, ref pos);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000050B2 File Offset: 0x000032B2
		[NativeMethod(Name = "SetHintWeightPosition", IsThreadSafe = true)]
		private void InternalSetHintWeightPosition(AvatarIKHint index, float value)
		{
			AnimationHumanStream.InternalSetHintWeightPosition_Injected(ref this, index, value);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000050BC File Offset: 0x000032BC
		[NativeMethod(Name = "GetHintWeightPosition", IsThreadSafe = true)]
		private float InternalGetHintWeightPosition(AvatarIKHint index)
		{
			return AnimationHumanStream.InternalGetHintWeightPosition_Injected(ref this, index);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000050C5 File Offset: 0x000032C5
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetLookAtPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetLookAtPosition(Vector3 lookAtPosition)
		{
			AnimationHumanStream.InternalSetLookAtPosition_Injected(ref this, ref lookAtPosition);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000050CF File Offset: 0x000032CF
		[NativeMethod(Name = "SetLookAtClampWeight", IsThreadSafe = true)]
		private void InternalSetLookAtClampWeight(float weight)
		{
			AnimationHumanStream.InternalSetLookAtClampWeight_Injected(ref this, weight);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000050D8 File Offset: 0x000032D8
		[NativeMethod(Name = "SetLookAtBodyWeight", IsThreadSafe = true)]
		private void InternalSetLookAtBodyWeight(float weight)
		{
			AnimationHumanStream.InternalSetLookAtBodyWeight_Injected(ref this, weight);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000050E1 File Offset: 0x000032E1
		[NativeMethod(Name = "SetLookAtHeadWeight", IsThreadSafe = true)]
		private void InternalSetLookAtHeadWeight(float weight)
		{
			AnimationHumanStream.InternalSetLookAtHeadWeight_Injected(ref this, weight);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000050EA File Offset: 0x000032EA
		[NativeMethod(Name = "SetLookAtEyesWeight", IsThreadSafe = true)]
		private void InternalSetLookAtEyesWeight(float weight)
		{
			AnimationHumanStream.InternalSetLookAtEyesWeight_Injected(ref this, weight);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000050F3 File Offset: 0x000032F3
		[NativeMethod(Name = "SolveIK", IsThreadSafe = true)]
		private void InternalSolveIK()
		{
			AnimationHumanStream.InternalSolveIK_Injected(ref this);
		}

		// Token: 0x0600034C RID: 844
		[MethodImpl(4096)]
		private static extern float GetHumanScale_Injected(ref AnimationHumanStream _unity_self);

		// Token: 0x0600034D RID: 845
		[MethodImpl(4096)]
		private static extern float GetFootHeight_Injected(ref AnimationHumanStream _unity_self, bool left);

		// Token: 0x0600034E RID: 846
		[MethodImpl(4096)]
		private static extern void InternalResetToStancePose_Injected(ref AnimationHumanStream _unity_self);

		// Token: 0x0600034F RID: 847
		[MethodImpl(4096)]
		private static extern void InternalGetGoalPositionFromPose_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		// Token: 0x06000350 RID: 848
		[MethodImpl(4096)]
		private static extern void InternalGetGoalRotationFromPose_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		// Token: 0x06000351 RID: 849
		[MethodImpl(4096)]
		private static extern void InternalGetBodyLocalPosition_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		// Token: 0x06000352 RID: 850
		[MethodImpl(4096)]
		private static extern void InternalSetBodyLocalPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 value);

		// Token: 0x06000353 RID: 851
		[MethodImpl(4096)]
		private static extern void InternalGetBodyLocalRotation_Injected(ref AnimationHumanStream _unity_self, out Quaternion ret);

		// Token: 0x06000354 RID: 852
		[MethodImpl(4096)]
		private static extern void InternalSetBodyLocalRotation_Injected(ref AnimationHumanStream _unity_self, ref Quaternion value);

		// Token: 0x06000355 RID: 853
		[MethodImpl(4096)]
		private static extern void InternalGetBodyPosition_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		// Token: 0x06000356 RID: 854
		[MethodImpl(4096)]
		private static extern void InternalSetBodyPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 value);

		// Token: 0x06000357 RID: 855
		[MethodImpl(4096)]
		private static extern void InternalGetBodyRotation_Injected(ref AnimationHumanStream _unity_self, out Quaternion ret);

		// Token: 0x06000358 RID: 856
		[MethodImpl(4096)]
		private static extern void InternalSetBodyRotation_Injected(ref AnimationHumanStream _unity_self, ref Quaternion value);

		// Token: 0x06000359 RID: 857
		[MethodImpl(4096)]
		private static extern float InternalGetMuscle_Injected(ref AnimationHumanStream _unity_self, ref MuscleHandle muscle);

		// Token: 0x0600035A RID: 858
		[MethodImpl(4096)]
		private static extern void InternalSetMuscle_Injected(ref AnimationHumanStream _unity_self, ref MuscleHandle muscle, float value);

		// Token: 0x0600035B RID: 859
		[MethodImpl(4096)]
		private static extern void GetLeftFootVelocity_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		// Token: 0x0600035C RID: 860
		[MethodImpl(4096)]
		private static extern void GetRightFootVelocity_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		// Token: 0x0600035D RID: 861
		[MethodImpl(4096)]
		private static extern void InternalGetGoalLocalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		// Token: 0x0600035E RID: 862
		[MethodImpl(4096)]
		private static extern void InternalSetGoalLocalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Vector3 pos);

		// Token: 0x0600035F RID: 863
		[MethodImpl(4096)]
		private static extern void InternalGetGoalLocalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		// Token: 0x06000360 RID: 864
		[MethodImpl(4096)]
		private static extern void InternalSetGoalLocalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Quaternion rot);

		// Token: 0x06000361 RID: 865
		[MethodImpl(4096)]
		private static extern void InternalGetGoalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		// Token: 0x06000362 RID: 866
		[MethodImpl(4096)]
		private static extern void InternalSetGoalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Vector3 pos);

		// Token: 0x06000363 RID: 867
		[MethodImpl(4096)]
		private static extern void InternalGetGoalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		// Token: 0x06000364 RID: 868
		[MethodImpl(4096)]
		private static extern void InternalSetGoalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Quaternion rot);

		// Token: 0x06000365 RID: 869
		[MethodImpl(4096)]
		private static extern void InternalSetGoalWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, float value);

		// Token: 0x06000366 RID: 870
		[MethodImpl(4096)]
		private static extern void InternalSetGoalWeightRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, float value);

		// Token: 0x06000367 RID: 871
		[MethodImpl(4096)]
		private static extern float InternalGetGoalWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index);

		// Token: 0x06000368 RID: 872
		[MethodImpl(4096)]
		private static extern float InternalGetGoalWeightRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index);

		// Token: 0x06000369 RID: 873
		[MethodImpl(4096)]
		private static extern void InternalGetHintPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, out Vector3 ret);

		// Token: 0x0600036A RID: 874
		[MethodImpl(4096)]
		private static extern void InternalSetHintPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, ref Vector3 pos);

		// Token: 0x0600036B RID: 875
		[MethodImpl(4096)]
		private static extern void InternalSetHintWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, float value);

		// Token: 0x0600036C RID: 876
		[MethodImpl(4096)]
		private static extern float InternalGetHintWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index);

		// Token: 0x0600036D RID: 877
		[MethodImpl(4096)]
		private static extern void InternalSetLookAtPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 lookAtPosition);

		// Token: 0x0600036E RID: 878
		[MethodImpl(4096)]
		private static extern void InternalSetLookAtClampWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		// Token: 0x0600036F RID: 879
		[MethodImpl(4096)]
		private static extern void InternalSetLookAtBodyWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		// Token: 0x06000370 RID: 880
		[MethodImpl(4096)]
		private static extern void InternalSetLookAtHeadWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		// Token: 0x06000371 RID: 881
		[MethodImpl(4096)]
		private static extern void InternalSetLookAtEyesWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		// Token: 0x06000372 RID: 882
		[MethodImpl(4096)]
		private static extern void InternalSolveIK_Injected(ref AnimationHumanStream _unity_self);

		// Token: 0x04000146 RID: 326
		private IntPtr stream;
	}
}
