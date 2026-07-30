using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Provides display instructions for the debugger.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000A66 RID: 2662
	[ComVisible(true)]
	public enum DebuggerBrowsableState
	{
		/// <summary>Never show the element.</summary>
		// Token: 0x040030AB RID: 12459
		Never,
		/// <summary>Show the element as collapsed.</summary>
		// Token: 0x040030AC RID: 12460
		Collapsed = 2,
		/// <summary>Do not display the root element; display the child elements if the element is a collection or array of items.</summary>
		// Token: 0x040030AD RID: 12461
		RootHidden
	}
}
