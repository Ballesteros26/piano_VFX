using System;

namespace System.Web.Compilation
{
	/// <summary>Represents an enumeration that specifies the target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</summary>
	// Token: 0x02000606 RID: 1542
	[Flags]
	public enum FolderLevelBuildProviderAppliesTo
	{
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object does not apply to any directory.</summary>
		// Token: 0x040023B2 RID: 9138
		None = 0,
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to a folder that contains code.</summary>
		// Token: 0x040023B3 RID: 9139
		Code = 1,
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the Web content directory.</summary>
		// Token: 0x040023B4 RID: 9140
		WebReferences = 2,
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the local resources directory.</summary>
		// Token: 0x040023B5 RID: 9141
		LocalResources = 4,
		/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the global resources directory.</summary>
		// Token: 0x040023B6 RID: 9142
		GlobalResources = 8
	}
}
