using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	[NativeHeader("Modules/Animation/Motion.h")]
	public class Motion : Object
	{
		// Token: 0x06000277 RID: 631 RVA: 0x000039AF File Offset: 0x00001BAF
		protected Motion()
		{
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000278 RID: 632
		public extern float averageDuration
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000279 RID: 633
		public extern float averageAngularSpeed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000042F8 File Offset: 0x000024F8
		public Vector3 averageSpeed
		{
			get
			{
				Vector3 vector;
				this.get_averageSpeed_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600027B RID: 635
		public extern float apparentSpeed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600027C RID: 636
		public extern bool isLooping
		{
			[NativeMethod("IsLooping")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600027D RID: 637
		public extern bool legacy
		{
			[NativeMethod("IsLegacy")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600027E RID: 638
		public extern bool isHumanMotion
		{
			[NativeMethod("IsHumanMotion")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004310 File Offset: 0x00002510
		[Obsolete("ValidateIfRetargetable is not supported anymore, please use isHumanMotion instead.", true)]
		[EditorBrowsable(1)]
		public bool ValidateIfRetargetable(bool val)
		{
			return false;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00004323 File Offset: 0x00002523
		[Obsolete("isAnimatorMotion is not supported anymore, please use !legacy instead.", true)]
		[EditorBrowsable(1)]
		public bool isAnimatorMotion { get; }

		// Token: 0x06000281 RID: 641
		[MethodImpl(4096)]
		private extern void get_averageSpeed_Injected(out Vector3 ret);
	}
}
