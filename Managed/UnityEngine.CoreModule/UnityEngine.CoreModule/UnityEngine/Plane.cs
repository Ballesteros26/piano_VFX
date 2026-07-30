using System;
using System.Globalization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C7 RID: 199
	[UsedByNativeCode]
	public struct Plane : IFormattable
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00007FA4 File Offset: 0x000061A4
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00007FBC File Offset: 0x000061BC
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00007FC8 File Offset: 0x000061C8
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00007FE0 File Offset: 0x000061E0
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00007FEA File Offset: 0x000061EA
		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(this.m_Normal, inPoint);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000800C File Offset: 0x0000620C
		public Plane(Vector3 inNormal, float d)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = d;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00008022 File Offset: 0x00006222
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00008056 File Offset: 0x00006256
		public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(inNormal, inPoint);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00008022 File Offset: 0x00006222
		public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00008073 File Offset: 0x00006273
		public void Flip()
		{
			this.m_Normal = -this.m_Normal;
			this.m_Distance = -this.m_Distance;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00008094 File Offset: 0x00006294
		public Plane flipped
		{
			get
			{
				return new Plane(-this.m_Normal, -this.m_Distance);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000080BD File Offset: 0x000062BD
		public void Translate(Vector3 translation)
		{
			this.m_Distance += Vector3.Dot(this.m_Normal, translation);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000080DC File Offset: 0x000062DC
		public static Plane Translate(Plane plane, Vector3 translation)
		{
			return new Plane(plane.m_Normal, plane.m_Distance += Vector3.Dot(plane.m_Normal, translation));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00008114 File Offset: 0x00006314
		public Vector3 ClosestPointOnPlane(Vector3 point)
		{
			float num = Vector3.Dot(this.m_Normal, point) + this.m_Distance;
			return point - this.m_Normal * num;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000814C File Offset: 0x0000634C
		public float GetDistanceToPoint(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00008174 File Offset: 0x00006374
		public bool GetSide(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance > 0f;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000081A0 File Offset: 0x000063A0
		public bool SameSide(Vector3 inPt0, Vector3 inPt1)
		{
			float distanceToPoint = this.GetDistanceToPoint(inPt0);
			float distanceToPoint2 = this.GetDistanceToPoint(inPt1);
			return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000081EC File Offset: 0x000063EC
		public bool Raycast(Ray ray, out float enter)
		{
			float num = Vector3.Dot(ray.direction, this.m_Normal);
			float num2 = -Vector3.Dot(ray.origin, this.m_Normal) - this.m_Distance;
			bool flag = Mathf.Approximately(num, 0f);
			bool flag2;
			if (flag)
			{
				enter = 0f;
				flag2 = false;
			}
			else
			{
				enter = num2 / num;
				flag2 = enter > 0f;
			}
			return flag2;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00008258 File Offset: 0x00006458
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000827C File Offset: 0x0000647C
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000082A0 File Offset: 0x000064A0
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F1";
			}
			return UnityString.Format("(normal:{0}, distance:{1})", new object[]
			{
				this.m_Normal.ToString(format, formatProvider),
				this.m_Distance.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400023E RID: 574
		internal const int size = 16;

		// Token: 0x0400023F RID: 575
		private Vector3 m_Normal;

		// Token: 0x04000240 RID: 576
		private float m_Distance;
	}
}
