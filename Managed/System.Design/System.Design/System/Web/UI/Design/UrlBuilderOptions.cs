using System;

namespace System.Web.UI.Design
{
	/// <summary>Defines identifiers for settings of a <see cref="T:System.Web.UI.Design.UrlBuilder" />.</summary>
	// Token: 0x020000AF RID: 175
	[Flags]
	public enum UrlBuilderOptions
	{
		/// <summary>Use no additional options for the <see cref="T:System.Web.UI.Design.UrlBuilder" />.</summary>
		// Token: 0x0400013C RID: 316
		None = 0,
		/// <summary>Build a URL that references a path relative to the current path, rather than one that references a fully qualified, absolute path.</summary>
		// Token: 0x0400013D RID: 317
		NoAbsolute = 1
	}
}
