using System;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DereferenceAlias" /> enumeration specifies how aliases are resolved. This enumeration provides values for the <see cref="P:System.DirectoryServices.DirectorySearcher.DerefAlias" /> property.          </summary>
	// Token: 0x0200008E RID: 142
	public enum DereferenceAlias
	{
		/// <summary>Dereferences aliases when both searching subordinates and locating the base objects of the search.</summary>
		// Token: 0x0400017E RID: 382
		Always = 3,
		/// <summary>Dereferences aliases when locating the base object of the search, but not when searching its subordinates.</summary>
		// Token: 0x0400017F RID: 383
		FindingBaseObject = 2,
		/// <summary>Dereferences aliases when searching subordinates of the base object, but not when locating the base itself.</summary>
		// Token: 0x04000180 RID: 384
		InSearching = 1,
		/// <summary>Indicates that the alias will not be dereferenced. If the <see cref="P:System.DirectoryServices.DirectorySearcher.DerefAlias" /> property is not set, the default value is <see cref="F:System.DirectoryServices.DereferenceAlias.Never" />.</summary>
		// Token: 0x04000181 RID: 385
		Never = 0
	}
}
