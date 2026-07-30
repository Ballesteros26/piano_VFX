using System;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Specifies the type of data-store query the design environment should construct.</summary>
	// Token: 0x02000172 RID: 370
	public enum QueryBuilderMode
	{
		/// <summary>The query being built is a Select query.</summary>
		// Token: 0x04000295 RID: 661
		Select,
		/// <summary>The query being built is an Update query.</summary>
		// Token: 0x04000296 RID: 662
		Update,
		/// <summary>The query being built is an Insert query.</summary>
		// Token: 0x04000297 RID: 663
		Insert,
		/// <summary>The query being built is a Delete query.</summary>
		// Token: 0x04000298 RID: 664
		Delete
	}
}
