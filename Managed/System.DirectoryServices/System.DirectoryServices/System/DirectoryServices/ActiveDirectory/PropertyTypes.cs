using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies the property types to select when calling the <see cref="M:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema.FindAllProperties(System.DirectoryServices.ActiveDirectory.PropertyTypes)" /> method.</summary>
	// Token: 0x02000068 RID: 104
	[Flags]
	public enum PropertyTypes
	{
		/// <summary>A property that is indexed.</summary>
		// Token: 0x04000132 RID: 306
		Indexed = 2,
		/// <summary>A property that is replicated in the global catalog.</summary>
		// Token: 0x04000133 RID: 307
		InGlobalCatalog = 4
	}
}
