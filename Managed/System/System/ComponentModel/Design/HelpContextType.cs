using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate information about the context in which a request for Help information originated.</summary>
	// Token: 0x02000321 RID: 801
	public enum HelpContextType
	{
		/// <summary>A general context.</summary>
		// Token: 0x04001473 RID: 5235
		Ambient,
		/// <summary>A window.</summary>
		// Token: 0x04001474 RID: 5236
		Window,
		/// <summary>A selection.</summary>
		// Token: 0x04001475 RID: 5237
		Selection,
		/// <summary>A tool window selection.</summary>
		// Token: 0x04001476 RID: 5238
		ToolWindowSelection
	}
}
