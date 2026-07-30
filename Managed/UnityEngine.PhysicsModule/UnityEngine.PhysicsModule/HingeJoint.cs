using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[NativeClass("Unity::HingeJoint")]
	[NativeHeader("Modules/Physics/HingeJoint.h")]
	public class HingeJoint : Joint
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000032AC File Offset: 0x000014AC
		// (set) Token: 0x0600018A RID: 394 RVA: 0x000032C2 File Offset: 0x000014C2
		public JointMotor motor
		{
			get
			{
				JointMotor jointMotor;
				this.get_motor_Injected(out jointMotor);
				return jointMotor;
			}
			set
			{
				this.set_motor_Injected(ref value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000032CC File Offset: 0x000014CC
		// (set) Token: 0x0600018C RID: 396 RVA: 0x000032E2 File Offset: 0x000014E2
		public JointLimits limits
		{
			get
			{
				JointLimits jointLimits;
				this.get_limits_Injected(out jointLimits);
				return jointLimits;
			}
			set
			{
				this.set_limits_Injected(ref value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000032EC File Offset: 0x000014EC
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00003302 File Offset: 0x00001502
		public JointSpring spring
		{
			get
			{
				JointSpring jointSpring;
				this.get_spring_Injected(out jointSpring);
				return jointSpring;
			}
			set
			{
				this.set_spring_Injected(ref value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600018F RID: 399
		// (set) Token: 0x06000190 RID: 400
		public extern bool useMotor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000191 RID: 401
		// (set) Token: 0x06000192 RID: 402
		public extern bool useLimits
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000193 RID: 403
		// (set) Token: 0x06000194 RID: 404
		public extern bool useSpring
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000195 RID: 405
		public extern float velocity
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000196 RID: 406
		public extern float angle
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000198 RID: 408
		[MethodImpl(4096)]
		private extern void get_motor_Injected(out JointMotor ret);

		// Token: 0x06000199 RID: 409
		[MethodImpl(4096)]
		private extern void set_motor_Injected(ref JointMotor value);

		// Token: 0x0600019A RID: 410
		[MethodImpl(4096)]
		private extern void get_limits_Injected(out JointLimits ret);

		// Token: 0x0600019B RID: 411
		[MethodImpl(4096)]
		private extern void set_limits_Injected(ref JointLimits value);

		// Token: 0x0600019C RID: 412
		[MethodImpl(4096)]
		private extern void get_spring_Injected(out JointSpring ret);

		// Token: 0x0600019D RID: 413
		[MethodImpl(4096)]
		private extern void set_spring_Injected(ref JointSpring value);
	}
}
