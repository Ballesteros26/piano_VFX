using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the order in which trace messages are displayed.</summary>
	// Token: 0x0200057B RID: 1403
	public enum TraceDisplayMode
	{
		/// <summary>Emit trace messages in the order they were processed.</summary>
		// Token: 0x04002071 RID: 8305
		SortByTime = 1,
		/// <summary>Emit trace messages alphabetically by category.</summary>
		// Token: 0x04002072 RID: 8306
		SortByCategory
	}
}
