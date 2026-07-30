using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[RequiredByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics/Collider.h")]
	public class Collider : Component
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F7 RID: 247
		// (set) Token: 0x060000F8 RID: 248
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000F9 RID: 249
		public extern Rigidbody attachedRigidbody
		{
			[NativeMethod("GetRigidbody")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000FA RID: 250
		// (set) Token: 0x060000FB RID: 251
		public extern bool isTrigger
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000FC RID: 252
		// (set) Token: 0x060000FD RID: 253
		public extern float contactOffset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002F3C File Offset: 0x0000113C
		public Vector3 ClosestPoint(Vector3 position)
		{
			Vector3 vector;
			this.ClosestPoint_Injected(ref position, out vector);
			return vector;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002F54 File Offset: 0x00001154
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.get_bounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000100 RID: 256
		// (set) Token: 0x06000101 RID: 257
		[NativeMethod("Material")]
		public extern PhysicMaterial sharedMaterial
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000102 RID: 258
		// (set) Token: 0x06000103 RID: 259
		public extern PhysicMaterial material
		{
			[NativeMethod("GetClonedMaterial")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetMaterial")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00002F6C File Offset: 0x0000116C
		private RaycastHit Raycast(Ray ray, float maxDistance, ref bool hasHit)
		{
			RaycastHit raycastHit;
			this.Raycast_Injected(ref ray, maxDistance, ref hasHit, out raycastHit);
			return raycastHit;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00002F88 File Offset: 0x00001188
		public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			bool flag = false;
			hitInfo = this.Raycast(ray, maxDistance, ref flag);
			return flag;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002FAD File Offset: 0x000011AD
		[NativeName("ClosestPointOnBounds")]
		private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
		{
			this.Internal_ClosestPointOnBounds_Injected(ref point, ref outPos, ref distance);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002FBC File Offset: 0x000011BC
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			float num = 0f;
			Vector3 zero = Vector3.zero;
			this.Internal_ClosestPointOnBounds(position, ref zero, ref num);
			return zero;
		}

		// Token: 0x06000109 RID: 265
		[MethodImpl(4096)]
		private extern void ClosestPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x0600010A RID: 266
		[MethodImpl(4096)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x0600010B RID: 267
		[MethodImpl(4096)]
		private extern void Raycast_Injected(ref Ray ray, float maxDistance, ref bool hasHit, out RaycastHit ret);

		// Token: 0x0600010C RID: 268
		[MethodImpl(4096)]
		private extern void Internal_ClosestPointOnBounds_Injected(ref Vector3 point, ref Vector3 outPos, ref float distance);
	}
}
