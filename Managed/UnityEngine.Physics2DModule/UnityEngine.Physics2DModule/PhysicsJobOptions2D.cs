using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[NativeHeader("Modules/Physics2D/Public/Physics2DSettings.h")]
	[NativeClass("PhysicsJobOptions2D", "struct PhysicsJobOptions2D;")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct PhysicsJobOptions2D
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00006020 File Offset: 0x00004220
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00006038 File Offset: 0x00004238
		public bool useMultithreading
		{
			get
			{
				return this.m_UseMultithreading;
			}
			set
			{
				this.m_UseMultithreading = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00006044 File Offset: 0x00004244
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000605C File Offset: 0x0000425C
		public bool useConsistencySorting
		{
			get
			{
				return this.m_UseConsistencySorting;
			}
			set
			{
				this.m_UseConsistencySorting = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006068 File Offset: 0x00004268
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00006080 File Offset: 0x00004280
		public int interpolationPosesPerJob
		{
			get
			{
				return this.m_InterpolationPosesPerJob;
			}
			set
			{
				this.m_InterpolationPosesPerJob = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000608C File Offset: 0x0000428C
		// (set) Token: 0x0600023A RID: 570 RVA: 0x000060A4 File Offset: 0x000042A4
		public int newContactsPerJob
		{
			get
			{
				return this.m_NewContactsPerJob;
			}
			set
			{
				this.m_NewContactsPerJob = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000060B0 File Offset: 0x000042B0
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000060C8 File Offset: 0x000042C8
		public int collideContactsPerJob
		{
			get
			{
				return this.m_CollideContactsPerJob;
			}
			set
			{
				this.m_CollideContactsPerJob = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000060D4 File Offset: 0x000042D4
		// (set) Token: 0x0600023E RID: 574 RVA: 0x000060EC File Offset: 0x000042EC
		public int clearFlagsPerJob
		{
			get
			{
				return this.m_ClearFlagsPerJob;
			}
			set
			{
				this.m_ClearFlagsPerJob = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000060F8 File Offset: 0x000042F8
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00006110 File Offset: 0x00004310
		public int clearBodyForcesPerJob
		{
			get
			{
				return this.m_ClearBodyForcesPerJob;
			}
			set
			{
				this.m_ClearBodyForcesPerJob = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000611C File Offset: 0x0000431C
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00006134 File Offset: 0x00004334
		public int syncDiscreteFixturesPerJob
		{
			get
			{
				return this.m_SyncDiscreteFixturesPerJob;
			}
			set
			{
				this.m_SyncDiscreteFixturesPerJob = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00006140 File Offset: 0x00004340
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00006158 File Offset: 0x00004358
		public int syncContinuousFixturesPerJob
		{
			get
			{
				return this.m_SyncContinuousFixturesPerJob;
			}
			set
			{
				this.m_SyncContinuousFixturesPerJob = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00006164 File Offset: 0x00004364
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000617C File Offset: 0x0000437C
		public int findNearestContactsPerJob
		{
			get
			{
				return this.m_FindNearestContactsPerJob;
			}
			set
			{
				this.m_FindNearestContactsPerJob = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00006188 File Offset: 0x00004388
		// (set) Token: 0x06000248 RID: 584 RVA: 0x000061A0 File Offset: 0x000043A0
		public int updateTriggerContactsPerJob
		{
			get
			{
				return this.m_UpdateTriggerContactsPerJob;
			}
			set
			{
				this.m_UpdateTriggerContactsPerJob = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000061AC File Offset: 0x000043AC
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000061C4 File Offset: 0x000043C4
		public int islandSolverCostThreshold
		{
			get
			{
				return this.m_IslandSolverCostThreshold;
			}
			set
			{
				this.m_IslandSolverCostThreshold = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000061D0 File Offset: 0x000043D0
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000061E8 File Offset: 0x000043E8
		public int islandSolverBodyCostScale
		{
			get
			{
				return this.m_IslandSolverBodyCostScale;
			}
			set
			{
				this.m_IslandSolverBodyCostScale = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000061F4 File Offset: 0x000043F4
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000620C File Offset: 0x0000440C
		public int islandSolverContactCostScale
		{
			get
			{
				return this.m_IslandSolverContactCostScale;
			}
			set
			{
				this.m_IslandSolverContactCostScale = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00006218 File Offset: 0x00004418
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00006230 File Offset: 0x00004430
		public int islandSolverJointCostScale
		{
			get
			{
				return this.m_IslandSolverJointCostScale;
			}
			set
			{
				this.m_IslandSolverJointCostScale = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000623C File Offset: 0x0000443C
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00006254 File Offset: 0x00004454
		public int islandSolverBodiesPerJob
		{
			get
			{
				return this.m_IslandSolverBodiesPerJob;
			}
			set
			{
				this.m_IslandSolverBodiesPerJob = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00006260 File Offset: 0x00004460
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00006278 File Offset: 0x00004478
		public int islandSolverContactsPerJob
		{
			get
			{
				return this.m_IslandSolverContactsPerJob;
			}
			set
			{
				this.m_IslandSolverContactsPerJob = value;
			}
		}

		// Token: 0x0400006B RID: 107
		private bool m_UseMultithreading;

		// Token: 0x0400006C RID: 108
		private bool m_UseConsistencySorting;

		// Token: 0x0400006D RID: 109
		private int m_InterpolationPosesPerJob;

		// Token: 0x0400006E RID: 110
		private int m_NewContactsPerJob;

		// Token: 0x0400006F RID: 111
		private int m_CollideContactsPerJob;

		// Token: 0x04000070 RID: 112
		private int m_ClearFlagsPerJob;

		// Token: 0x04000071 RID: 113
		private int m_ClearBodyForcesPerJob;

		// Token: 0x04000072 RID: 114
		private int m_SyncDiscreteFixturesPerJob;

		// Token: 0x04000073 RID: 115
		private int m_SyncContinuousFixturesPerJob;

		// Token: 0x04000074 RID: 116
		private int m_FindNearestContactsPerJob;

		// Token: 0x04000075 RID: 117
		private int m_UpdateTriggerContactsPerJob;

		// Token: 0x04000076 RID: 118
		private int m_IslandSolverCostThreshold;

		// Token: 0x04000077 RID: 119
		private int m_IslandSolverBodyCostScale;

		// Token: 0x04000078 RID: 120
		private int m_IslandSolverContactCostScale;

		// Token: 0x04000079 RID: 121
		private int m_IslandSolverJointCostScale;

		// Token: 0x0400007A RID: 122
		private int m_IslandSolverBodiesPerJob;

		// Token: 0x0400007B RID: 123
		private int m_IslandSolverContactsPerJob;
	}
}
