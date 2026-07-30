using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C3 RID: 195
	[NativeType(Header = "Runtime/Geometry/AABB.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("AABB")]
	[NativeHeader("Runtime/Geometry/Intersection.h")]
	[NativeHeader("Runtime/Geometry/Ray.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[NativeHeader("Runtime/Geometry/AABB.h")]
	public struct Bounds : IEquatable<Bounds>, IFormattable
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00006E61 File Offset: 0x00005061
		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00006E7C File Offset: 0x0000507C
		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ (this.extents.GetHashCode() << 2);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00006EBC File Offset: 0x000050BC
		public override bool Equals(object other)
		{
			bool flag = !(other is Bounds);
			return !flag && this.Equals((Bounds)other);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00006EF0 File Offset: 0x000050F0
		public bool Equals(Bounds other)
		{
			return this.center.Equals(other.center) && this.extents.Equals(other.extents);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00006F34 File Offset: 0x00005134
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00006F4C File Offset: 0x0000514C
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00006F58 File Offset: 0x00005158
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00006F7A File Offset: 0x0000517A
		public Vector3 size
		{
			get
			{
				return this.m_Extents * 2f;
			}
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00006F90 File Offset: 0x00005190
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00006FA8 File Offset: 0x000051A8
		public Vector3 extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00006FB4 File Offset: 0x000051B4
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00006FD7 File Offset: 0x000051D7
		public Vector3 min
		{
			get
			{
				return this.center - this.extents;
			}
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00006FE8 File Offset: 0x000051E8
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0000700B File Offset: 0x0000520B
		public Vector3 max
		{
			get
			{
				return this.center + this.extents;
			}
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000701C File Offset: 0x0000521C
		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000705C File Offset: 0x0000525C
		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00007078 File Offset: 0x00005278
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000070A6 File Offset: 0x000052A6
		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000070C8 File Offset: 0x000052C8
		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000070FF File Offset: 0x000052FF
		public void Expand(float amount)
		{
			amount *= 0.5f;
			this.extents += new Vector3(amount, amount, amount);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00007125 File Offset: 0x00005325
		public void Expand(Vector3 amount)
		{
			this.extents += amount * 0.5f;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00007148 File Offset: 0x00005348
		public bool Intersects(Bounds bounds)
		{
			return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000071FC File Offset: 0x000053FC
		public bool IntersectRay(Ray ray)
		{
			float num;
			return Bounds.IntersectRayAABB(ray, this, out num);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000721C File Offset: 0x0000541C
		public bool IntersectRay(Ray ray, out float distance)
		{
			return Bounds.IntersectRayAABB(ray, this, out distance);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000723C File Offset: 0x0000543C
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00007260 File Offset: 0x00005460
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00007284 File Offset: 0x00005484
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F1";
			}
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center.ToString(format, formatProvider),
				this.m_Extents.ToString(format, formatProvider)
			});
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000072D7 File Offset: 0x000054D7
		[NativeMethod("IsInside", IsThreadSafe = true)]
		public bool Contains(Vector3 point)
		{
			return Bounds.Contains_Injected(ref this, ref point);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000072E1 File Offset: 0x000054E1
		[FreeFunction("BoundsScripting::SqrDistance", HasExplicitThis = true, IsThreadSafe = true)]
		public float SqrDistance(Vector3 point)
		{
			return Bounds.SqrDistance_Injected(ref this, ref point);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000072EB File Offset: 0x000054EB
		[FreeFunction("IntersectRayAABB", IsThreadSafe = true)]
		private static bool IntersectRayAABB(Ray ray, Bounds bounds, out float dist)
		{
			return Bounds.IntersectRayAABB_Injected(ref ray, ref bounds, out dist);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000072F8 File Offset: 0x000054F8
		[FreeFunction("BoundsScripting::ClosestPoint", HasExplicitThis = true, IsThreadSafe = true)]
		public Vector3 ClosestPoint(Vector3 point)
		{
			Vector3 vector;
			Bounds.ClosestPoint_Injected(ref this, ref point, out vector);
			return vector;
		}

		// Token: 0x060004C9 RID: 1225
		[MethodImpl(4096)]
		private static extern bool Contains_Injected(ref Bounds _unity_self, ref Vector3 point);

		// Token: 0x060004CA RID: 1226
		[MethodImpl(4096)]
		private static extern float SqrDistance_Injected(ref Bounds _unity_self, ref Vector3 point);

		// Token: 0x060004CB RID: 1227
		[MethodImpl(4096)]
		private static extern bool IntersectRayAABB_Injected(ref Ray ray, ref Bounds bounds, out float dist);

		// Token: 0x060004CC RID: 1228
		[MethodImpl(4096)]
		private static extern void ClosestPoint_Injected(ref Bounds _unity_self, ref Vector3 point, out Vector3 ret);

		// Token: 0x04000237 RID: 567
		private Vector3 m_Center;

		// Token: 0x04000238 RID: 568
		[NativeName("m_Extent")]
		private Vector3 m_Extents;
	}
}
