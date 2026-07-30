using System;
using System.Globalization;

namespace UnityEngine
{
	// Token: 0x020000C9 RID: 201
	public struct Ray2D : IFormattable
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x0000841F File Offset: 0x0000661F
		public Ray2D(Vector2 origin, Vector2 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00008438 File Offset: 0x00006638
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00008450 File Offset: 0x00006650
		public Vector2 origin
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

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000845C File Offset: 0x0000665C
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00008474 File Offset: 0x00006674
		public Vector2 direction
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

		// Token: 0x0600052D RID: 1325 RVA: 0x00008484 File Offset: 0x00006684
		public Vector2 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000084B0 File Offset: 0x000066B0
		public override string ToString()
		{
			return this.ToString("F1", CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000084D8 File Offset: 0x000066D8
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000084FC File Offset: 0x000066FC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format, formatProvider),
				this.m_Direction.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000243 RID: 579
		private Vector2 m_Origin;

		// Token: 0x04000244 RID: 580
		private Vector2 m_Direction;
	}
}
