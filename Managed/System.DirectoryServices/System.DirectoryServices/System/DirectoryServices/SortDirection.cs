using System;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.SortDirection" /> enumeration specifies how to sort the results of an Active Directory Domain Services query.</summary>
	// Token: 0x02000030 RID: 48
	[Serializable]
	public enum SortDirection
	{
		/// <summary>Sort from smallest to largest. For example, A to Z.</summary>
		// Token: 0x040000AE RID: 174
		Ascending,
		/// <summary>Sort from largest to smallest. For example, Z to A.</summary>
		// Token: 0x040000AF RID: 175
		Descending
	}
}
