using System;

namespace System.Web
{
	/// <summary>Specifies in what order trace messages are emitted into the HTML output of a page.</summary>
	// Token: 0x020000E6 RID: 230
	public enum TraceMode
	{
		/// <summary>Emit trace messages in the order they were processed.</summary>
		// Token: 0x040010FA RID: 4346
		SortByTime,
		/// <summary>Emit trace messages alphabetically by category.</summary>
		// Token: 0x040010FB RID: 4347
		SortByCategory,
		/// <summary>Specifies the default value of the <see cref="P:System.Web.TraceContext.TraceMode" /> enumeration, which is <see cref="F:System.Web.TraceMode.SortByTime" />.</summary>
		// Token: 0x040010FC RID: 4348
		Default
	}
}
