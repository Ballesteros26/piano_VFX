using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers used to indicate the type of filter that a <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> uses.</summary>
	// Token: 0x020002D9 RID: 729
	public enum ToolboxItemFilterType
	{
		/// <summary>Indicates that a toolbox item filter string is allowed, but not required.</summary>
		// Token: 0x040013ED RID: 5101
		Allow,
		/// <summary>Indicates that custom processing is required to determine whether to use a toolbox item filter string. </summary>
		// Token: 0x040013EE RID: 5102
		Custom,
		/// <summary>Indicates that a toolbox item filter string is not allowed. </summary>
		// Token: 0x040013EF RID: 5103
		Prevent,
		/// <summary>Indicates that a toolbox item filter string must be present for a toolbox item to be enabled. </summary>
		// Token: 0x040013F0 RID: 5104
		Require
	}
}
