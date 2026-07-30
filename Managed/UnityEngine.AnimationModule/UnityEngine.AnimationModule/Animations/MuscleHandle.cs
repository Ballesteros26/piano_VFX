using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000067 RID: 103
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/MuscleHandle.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	public struct MuscleHandle
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x000080CD File Offset: 0x000062CD
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x000080D5 File Offset: 0x000062D5
		public HumanPartDof humanPartDof { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x000080DE File Offset: 0x000062DE
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x000080E6 File Offset: 0x000062E6
		public int dof { get; private set; }

		// Token: 0x06000605 RID: 1541 RVA: 0x000080EF File Offset: 0x000062EF
		public MuscleHandle(BodyDof bodyDof)
		{
			this.humanPartDof = HumanPartDof.Body;
			this.dof = (int)bodyDof;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00008102 File Offset: 0x00006302
		public MuscleHandle(HeadDof headDof)
		{
			this.humanPartDof = HumanPartDof.Head;
			this.dof = (int)headDof;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00008118 File Offset: 0x00006318
		public MuscleHandle(HumanPartDof partDof, LegDof legDof)
		{
			bool flag = partDof != HumanPartDof.LeftLeg && partDof != HumanPartDof.RightLeg;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a leg, please use either HumanPartDof.LeftLeg or HumanPartDof.RightLeg.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)legDof;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00008154 File Offset: 0x00006354
		public MuscleHandle(HumanPartDof partDof, ArmDof armDof)
		{
			bool flag = partDof != HumanPartDof.LeftArm && partDof != HumanPartDof.RightArm;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for an arm, please use either HumanPartDof.LeftArm or HumanPartDof.RightArm.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)armDof;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00008190 File Offset: 0x00006390
		public MuscleHandle(HumanPartDof partDof, FingerDof fingerDof)
		{
			bool flag = partDof < HumanPartDof.LeftThumb || partDof > HumanPartDof.RightLittle;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a finger.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)fingerDof;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000081CC File Offset: 0x000063CC
		public string name
		{
			get
			{
				return this.GetName();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000081E4 File Offset: 0x000063E4
		public static int muscleHandleCount
		{
			get
			{
				return MuscleHandle.GetMuscleHandleCount();
			}
		}

		// Token: 0x0600060C RID: 1548
		[MethodImpl(4096)]
		public static extern void GetMuscleHandles([NotNull] [Out] MuscleHandle[] muscleHandles);

		// Token: 0x0600060D RID: 1549 RVA: 0x000081FB File Offset: 0x000063FB
		private string GetName()
		{
			return MuscleHandle.GetName_Injected(ref this);
		}

		// Token: 0x0600060E RID: 1550
		[MethodImpl(4096)]
		private static extern int GetMuscleHandleCount();

		// Token: 0x0600060F RID: 1551
		[MethodImpl(4096)]
		private static extern string GetName_Injected(ref MuscleHandle _unity_self);
	}
}
