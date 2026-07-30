using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the validation comparison operators used by the <see cref="T:System.Web.UI.WebControls.CompareValidator" /> control.</summary>
	// Token: 0x02000324 RID: 804
	public enum ValidationCompareOperator
	{
		/// <summary>A comparison for equality.</summary>
		// Token: 0x040017BE RID: 6078
		Equal,
		/// <summary>A comparison for inequality.</summary>
		// Token: 0x040017BF RID: 6079
		NotEqual,
		/// <summary>A comparison for greater than.</summary>
		// Token: 0x040017C0 RID: 6080
		GreaterThan,
		/// <summary>A comparison for greater than or equal to.</summary>
		// Token: 0x040017C1 RID: 6081
		GreaterThanEqual,
		/// <summary>A comparison for less than.</summary>
		// Token: 0x040017C2 RID: 6082
		LessThan,
		/// <summary>A comparison for less than or equal to.</summary>
		// Token: 0x040017C3 RID: 6083
		LessThanEqual,
		/// <summary>A comparison for data type only.</summary>
		// Token: 0x040017C4 RID: 6084
		DataTypeCheck
	}
}
