using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies which child controls to skip.</summary>
	// Token: 0x020001A0 RID: 416
	[Flags]
	public enum GetChildAtPointSkip
	{
		/// <summary>Does not skip any child windows.</summary>
		// Token: 0x04000F02 RID: 3842
		None = 0,
		/// <summary>Skips invisible child windows.</summary>
		// Token: 0x04000F03 RID: 3843
		Invisible = 1,
		/// <summary>Skips disabled child windows.</summary>
		// Token: 0x04000F04 RID: 3844
		Disabled = 2,
		/// <summary>Skips transparent child windows.</summary>
		// Token: 0x04000F05 RID: 3845
		Transparent = 4
	}
}
