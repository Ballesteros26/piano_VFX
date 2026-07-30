using System;

namespace System.Data.Common
{
	/// <summary>Specifies what types of Transact-SQL join statements are supported by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000375 RID: 885
	[Flags]
	public enum SupportedJoinOperators
	{
		/// <summary>The data source does not support join queries.</summary>
		// Token: 0x04001967 RID: 6503
		None = 0,
		/// <summary>The data source supports inner joins.</summary>
		// Token: 0x04001968 RID: 6504
		Inner = 1,
		/// <summary>The data source supports left outer joins.</summary>
		// Token: 0x04001969 RID: 6505
		LeftOuter = 2,
		/// <summary>The data source supports right outer joins.</summary>
		// Token: 0x0400196A RID: 6506
		RightOuter = 4,
		/// <summary>The data source supports full outer joins.</summary>
		// Token: 0x0400196B RID: 6507
		FullOuter = 8
	}
}
