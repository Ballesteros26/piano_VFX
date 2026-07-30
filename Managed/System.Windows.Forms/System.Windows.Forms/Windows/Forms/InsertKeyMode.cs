using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the insertion mode used by text boxes.</summary>
	// Token: 0x020001E6 RID: 486
	public enum InsertKeyMode
	{
		/// <summary>Honors the current INSERT key mode of the keyboard.</summary>
		// Token: 0x0400100B RID: 4107
		Default,
		/// <summary>Indicates that the insertion mode is enabled regardless of the INSERT key mode of the keyboard.</summary>
		// Token: 0x0400100C RID: 4108
		Insert,
		/// <summary>Indicates that the overwrite mode is enabled regardless of the INSERT key mode of the keyboard.</summary>
		// Token: 0x0400100D RID: 4109
		Overwrite
	}
}
