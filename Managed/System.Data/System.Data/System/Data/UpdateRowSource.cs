using System;

namespace System.Data
{
	/// <summary>Specifies how query command results are applied to the row being updated.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FE RID: 254
	public enum UpdateRowSource
	{
		/// <summary>Any returned parameters or rows are ignored.</summary>
		// Token: 0x040008AE RID: 2222
		None,
		/// <summary>Output parameters are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x040008AF RID: 2223
		OutputParameters,
		/// <summary>The data in the first returned row is mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x040008B0 RID: 2224
		FirstReturnedRecord,
		/// <summary>Both the output parameters and the first returned row are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x040008B1 RID: 2225
		Both
	}
}
