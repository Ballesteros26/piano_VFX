using System;

namespace System.Web.Compilation
{
	/// <summary>Defines an attribute that specifies the scope where a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object should be applied when a resource is located. </summary>
	// Token: 0x02000607 RID: 1543
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class FolderLevelBuildProviderAppliesToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.FolderLevelBuildProviderAppliesToAttribute" /> class.</summary>
		/// <param name="appliesTo">The target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</param>
		// Token: 0x060042A7 RID: 17063 RVA: 0x000AFB6B File Offset: 0x000ADD6B
		public FolderLevelBuildProviderAppliesToAttribute(FolderLevelBuildProviderAppliesTo appliesTo)
		{
			this._appliesTo = appliesTo;
		}

		/// <summary>Gets or sets the target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</summary>
		/// <returns>The directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</returns>
		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x000AFB7A File Offset: 0x000ADD7A
		public FolderLevelBuildProviderAppliesTo AppliesTo
		{
			get
			{
				return this._appliesTo;
			}
		}

		// Token: 0x040023B7 RID: 9143
		private FolderLevelBuildProviderAppliesTo _appliesTo;
	}
}
