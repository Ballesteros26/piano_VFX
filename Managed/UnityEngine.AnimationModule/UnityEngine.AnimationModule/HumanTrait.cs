using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	[NativeHeader("Modules/Animation/HumanTrait.h")]
	public class HumanTrait
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000263 RID: 611
		public static extern int MuscleCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000264 RID: 612
		[MethodImpl(4096)]
		internal static extern int GetBoneIndexFromMono(int humanId);

		// Token: 0x06000265 RID: 613
		[MethodImpl(4096)]
		internal static extern int GetBoneIndexToMono(int boneIndex);

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000266 RID: 614
		public static extern string[] MuscleName
		{
			[NativeMethod("GetMuscleNames")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000267 RID: 615
		public static extern int BoneCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000268 RID: 616
		public static extern string[] BoneName
		{
			[NativeMethod("MonoBoneNames")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000424C File Offset: 0x0000244C
		public static int MuscleFromBone(int i, int dofIndex)
		{
			return HumanTrait.Internal_MuscleFromBone(HumanTrait.GetBoneIndexFromMono(i), dofIndex);
		}

		// Token: 0x0600026A RID: 618
		[NativeMethod("MuscleFromBone")]
		[MethodImpl(4096)]
		private static extern int Internal_MuscleFromBone(int i, int dofIndex);

		// Token: 0x0600026B RID: 619 RVA: 0x0000426C File Offset: 0x0000246C
		public static int BoneFromMuscle(int i)
		{
			return HumanTrait.GetBoneIndexToMono(HumanTrait.Internal_BoneFromMuscle(i));
		}

		// Token: 0x0600026C RID: 620
		[NativeMethod("BoneFromMuscle")]
		[MethodImpl(4096)]
		private static extern int Internal_BoneFromMuscle(int i);

		// Token: 0x0600026D RID: 621 RVA: 0x0000428C File Offset: 0x0000248C
		public static bool RequiredBone(int i)
		{
			return HumanTrait.Internal_RequiredBone(HumanTrait.GetBoneIndexFromMono(i));
		}

		// Token: 0x0600026E RID: 622
		[NativeMethod("RequiredBone")]
		[MethodImpl(4096)]
		private static extern bool Internal_RequiredBone(int i);

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600026F RID: 623
		public static extern int RequiredBoneCount
		{
			[NativeMethod("RequiredBoneCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000270 RID: 624
		[MethodImpl(4096)]
		public static extern float GetMuscleDefaultMin(int i);

		// Token: 0x06000271 RID: 625
		[MethodImpl(4096)]
		public static extern float GetMuscleDefaultMax(int i);

		// Token: 0x06000272 RID: 626 RVA: 0x000042AC File Offset: 0x000024AC
		public static float GetBoneDefaultHierarchyMass(int i)
		{
			return HumanTrait.Internal_GetBoneHierarchyMass(HumanTrait.GetBoneIndexFromMono(i));
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000042CC File Offset: 0x000024CC
		public static int GetParentBone(int i)
		{
			int num = HumanTrait.Internal_GetParent(HumanTrait.GetBoneIndexFromMono(i));
			return (num != -1) ? HumanTrait.GetBoneIndexToMono(num) : (-1);
		}

		// Token: 0x06000274 RID: 628
		[NativeMethod("GetBoneHierarchyMass")]
		[MethodImpl(4096)]
		private static extern float Internal_GetBoneHierarchyMass(int i);

		// Token: 0x06000275 RID: 629
		[NativeMethod("GetParent")]
		[MethodImpl(4096)]
		private static extern int Internal_GetParent(int i);
	}
}
