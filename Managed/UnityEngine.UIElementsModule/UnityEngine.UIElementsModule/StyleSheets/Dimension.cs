using System;
using System.Globalization;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200025F RID: 607
	[Serializable]
	internal struct Dimension : IEquatable<Dimension>
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x0004FDD3 File Offset: 0x0004DFD3
		public Dimension(float value, Dimension.Unit unit)
		{
			this.unit = unit;
			this.value = value;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0004FDE4 File Offset: 0x0004DFE4
		public Length ToLength()
		{
			LengthUnit lengthUnit = ((this.unit == Dimension.Unit.Percent) ? LengthUnit.Percent : LengthUnit.Pixel);
			return new Length(this.value, lengthUnit);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0004FE10 File Offset: 0x0004E010
		public static bool operator ==(Dimension lhs, Dimension rhs)
		{
			return lhs.value == rhs.value && lhs.unit == rhs.unit;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0004FE44 File Offset: 0x0004E044
		public static bool operator !=(Dimension lhs, Dimension rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004FE60 File Offset: 0x0004E060
		public bool Equals(Dimension other)
		{
			return other == this;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0004FE80 File Offset: 0x0004E080
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Dimension);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Dimension dimension = (Dimension)obj;
				flag2 = dimension == this;
			}
			return flag2;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004FEBC File Offset: 0x0004E0BC
		public override int GetHashCode()
		{
			int num = -799583767;
			num = num * -1521134295 + this.unit.GetHashCode();
			return num * -1521134295 + this.value.GetHashCode();
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004FF04 File Offset: 0x0004E104
		public override string ToString()
		{
			string text = string.Empty;
			Dimension.Unit unit = this.unit;
			if (unit != Dimension.Unit.Pixel)
			{
				if (unit == Dimension.Unit.Percent)
				{
					text = "%";
				}
			}
			else
			{
				text = "px";
			}
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat) + text;
		}

		// Token: 0x040008F6 RID: 2294
		public Dimension.Unit unit;

		// Token: 0x040008F7 RID: 2295
		public float value;

		// Token: 0x02000260 RID: 608
		public enum Unit
		{
			// Token: 0x040008F9 RID: 2297
			Unitless,
			// Token: 0x040008FA RID: 2298
			Pixel,
			// Token: 0x040008FB RID: 2299
			Percent
		}
	}
}
