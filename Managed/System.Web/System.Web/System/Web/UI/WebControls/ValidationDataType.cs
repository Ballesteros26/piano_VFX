using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the validation data types used by the <see cref="T:System.Web.UI.WebControls.CompareValidator" /> and <see cref="T:System.Web.UI.WebControls.RangeValidator" /> controls.</summary>
	// Token: 0x02000325 RID: 805
	public enum ValidationDataType
	{
		/// <summary>A string data type. The value is treated as a <see cref="T:System.String" />.</summary>
		// Token: 0x040017C6 RID: 6086
		String,
		/// <summary>A 32-bit signed integer data type. The value is treated as a <see cref="T:System.Int32" />.</summary>
		// Token: 0x040017C7 RID: 6087
		Integer,
		/// <summary>A double precision floating point number data type. The value is treated as a <see cref="T:System.Double" />.</summary>
		// Token: 0x040017C8 RID: 6088
		Double,
		/// <summary>A date data type. Only numeric dates are allowed. The time portion cannot be specified.</summary>
		// Token: 0x040017C9 RID: 6089
		Date,
		/// <summary>A monetary data type. The value is treated as a <see cref="T:System.Decimal" />. However, currency and grouping symbols are still allowed.</summary>
		// Token: 0x040017CA RID: 6090
		Currency
	}
}
