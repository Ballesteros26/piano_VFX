using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AD RID: 429
	public struct Length : IEquatable<Length>
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x00031E64 File Offset: 0x00030064
		public static Length Percent(float value)
		{
			return new Length(value, LengthUnit.Percent);
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00031E80 File Offset: 0x00030080
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x00031E98 File Offset: 0x00030098
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00031EA4 File Offset: 0x000300A4
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00031EBC File Offset: 0x000300BC
		public LengthUnit unit
		{
			get
			{
				return this.m_Unit;
			}
			set
			{
				this.m_Unit = value;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00031EC6 File Offset: 0x000300C6
		public Length(float value)
		{
			this = new Length(value, LengthUnit.Pixel);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00031ED2 File Offset: 0x000300D2
		public Length(float value, LengthUnit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00031EE4 File Offset: 0x000300E4
		public static implicit operator Length(float value)
		{
			return new Length(value, LengthUnit.Pixel);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00031F00 File Offset: 0x00030100
		public static bool operator ==(Length lhs, Length rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00031F34 File Offset: 0x00030134
		public static bool operator !=(Length lhs, Length rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00031F50 File Offset: 0x00030150
		public bool Equals(Length other)
		{
			return other == this;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00031F70 File Offset: 0x00030170
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Length);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Length length = (Length)obj;
				flag2 = length == this;
			}
			return flag2;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00031FAC File Offset: 0x000301AC
		public override int GetHashCode()
		{
			int num = 851985039;
			num = num * -1521134295 + this.m_Value.GetHashCode();
			return num * -1521134295 + this.m_Unit.GetHashCode();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00031FF4 File Offset: 0x000301F4
		public override string ToString()
		{
			string text = string.Empty;
			LengthUnit unit = this.unit;
			if (unit != LengthUnit.Pixel)
			{
				if (unit == LengthUnit.Percent)
				{
					text = "%";
				}
			}
			else
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					text = "px";
				}
			}
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat) + text;
		}

		// Token: 0x0400052F RID: 1327
		private float m_Value;

		// Token: 0x04000530 RID: 1328
		private LengthUnit m_Unit;
	}
}
