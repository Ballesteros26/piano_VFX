using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x020000C8 RID: 200
	public struct Ray : IFormattable
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x000082F3 File Offset: 0x000064F3
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000830C File Offset: 0x0000650C
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00008324 File Offset: 0x00006524
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00008330 File Offset: 0x00006530
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00008348 File Offset: 0x00006548
		public Vector3 direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value.normalized;
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00008358 File Offset: 0x00006558
		public Vector3 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00008384 File Offset: 0x00006584
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000083A8 File Offset: 0x000065A8
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000083CC File Offset: 0x000065CC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F1";
			}
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format, formatProvider),
				this.m_Direction.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000241 RID: 577
		private Vector3 m_Origin;

		// Token: 0x04000242 RID: 578
		private Vector3 m_Direction;
	}
}
