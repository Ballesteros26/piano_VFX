using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Modules/Physics/CapsuleCollider.h")]
	[RequiredByNativeCode]
	public class CapsuleCollider : Collider
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003080 File Offset: 0x00001280
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00003096 File Offset: 0x00001296
		public Vector3 center
		{
			get
			{
				Vector3 vector;
				this.get_center_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000139 RID: 313
		// (set) Token: 0x0600013A RID: 314
		public extern float radius
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600013B RID: 315
		// (set) Token: 0x0600013C RID: 316
		public extern float height
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600013D RID: 317
		// (set) Token: 0x0600013E RID: 318
		public extern int direction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000030A0 File Offset: 0x000012A0
		internal Vector2 GetGlobalExtents()
		{
			Vector2 vector;
			this.GetGlobalExtents_Injected(out vector);
			return vector;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000030B8 File Offset: 0x000012B8
		internal Matrix4x4 CalculateTransform()
		{
			Matrix4x4 matrix4x;
			this.CalculateTransform_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000142 RID: 322
		[MethodImpl(4096)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000143 RID: 323
		[MethodImpl(4096)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x06000144 RID: 324
		[MethodImpl(4096)]
		private extern void GetGlobalExtents_Injected(out Vector2 ret);

		// Token: 0x06000145 RID: 325
		[MethodImpl(4096)]
		private extern void CalculateTransform_Injected(out Matrix4x4 ret);
	}
}
