using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public struct ArticulationDrive
	{
		// Token: 0x04000082 RID: 130
		public float lowerLimit;

		// Token: 0x04000083 RID: 131
		public float upperLimit;

		// Token: 0x04000084 RID: 132
		public float stiffness;

		// Token: 0x04000085 RID: 133
		public float damping;

		// Token: 0x04000086 RID: 134
		public float forceLimit;

		// Token: 0x04000087 RID: 135
		public float target;

		// Token: 0x04000088 RID: 136
		public float targetVelocity;
	}
}
