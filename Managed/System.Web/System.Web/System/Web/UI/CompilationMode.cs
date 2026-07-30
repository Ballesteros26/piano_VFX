using System;

namespace System.Web.UI
{
	/// <summary>Defines constants that specify how ASP.NET should compile .aspx pages and .ascx controls.</summary>
	// Token: 0x0200018F RID: 399
	public enum CompilationMode
	{
		/// <summary>ASP.NET will not compile the page, if possible.</summary>
		// Token: 0x0400131C RID: 4892
		Auto,
		/// <summary>The page or control should never be dynamically compiled.</summary>
		// Token: 0x0400131D RID: 4893
		Never,
		/// <summary>The page should always be compiled.</summary>
		// Token: 0x0400131E RID: 4894
		Always
	}
}
