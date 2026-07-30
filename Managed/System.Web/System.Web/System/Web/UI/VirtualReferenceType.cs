using System;

namespace System.Web.UI
{
	/// <summary>Specifies the type of resource referenced by a parsed virtual path.</summary>
	// Token: 0x0200024D RID: 589
	public enum VirtualReferenceType
	{
		/// <summary>The parsed virtual path references an ASP.NET page.</summary>
		// Token: 0x0400160E RID: 5646
		Page,
		/// <summary>The parsed virtual path references an ASP.NET user control.</summary>
		// Token: 0x0400160F RID: 5647
		UserControl,
		/// <summary>The parsed virtual path references a master page file.</summary>
		// Token: 0x04001610 RID: 5648
		Master,
		/// <summary>The parsed virtual path references a code file that is compiled using a specific language compiler.</summary>
		// Token: 0x04001611 RID: 5649
		SourceFile,
		/// <summary>The parsed virtual path references a resource that is not an ASP.NET page, master page, user control, or language-specific code file.</summary>
		// Token: 0x04001612 RID: 5650
		Other
	}
}
