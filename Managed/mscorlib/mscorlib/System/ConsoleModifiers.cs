using System;

namespace System
{
	/// <summary>Represents the SHIFT, ALT, and CTRL modifier keys on a keyboard.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013F RID: 319
	[Flags]
	[Serializable]
	public enum ConsoleModifiers
	{
		/// <summary>The left or right ALT modifier key.</summary>
		// Token: 0x0400087D RID: 2173
		Alt = 1,
		/// <summary>The left or right SHIFT modifier key.</summary>
		// Token: 0x0400087E RID: 2174
		Shift = 2,
		/// <summary>The left or right CTRL modifier key.</summary>
		// Token: 0x0400087F RID: 2175
		Control = 4
	}
}
