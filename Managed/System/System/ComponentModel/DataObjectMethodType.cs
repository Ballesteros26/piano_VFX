using System;

namespace System.ComponentModel
{
	/// <summary>Identifies the type of data operation performed by a method, as specified by the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> applied to the method.</summary>
	// Token: 0x02000254 RID: 596
	public enum DataObjectMethodType
	{
		/// <summary>Indicates that a method is used for a data operation that fills a <see cref="T:System.Data.DataSet" /> object.</summary>
		// Token: 0x040012A1 RID: 4769
		Fill,
		/// <summary>Indicates that a method is used for a data operation that retrieves data.</summary>
		// Token: 0x040012A2 RID: 4770
		Select,
		/// <summary>Indicates that a method is used for a data operation that updates data.</summary>
		// Token: 0x040012A3 RID: 4771
		Update,
		/// <summary>Indicates that a method is used for a data operation that inserts data.</summary>
		// Token: 0x040012A4 RID: 4772
		Insert,
		/// <summary>Indicates that a method is used for a data operation that deletes data.</summary>
		// Token: 0x040012A5 RID: 4773
		Delete
	}
}
