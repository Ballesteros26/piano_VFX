using System;

namespace System.Drawing
{
	/// <summary>The <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration specifies how to substitute digits in a string according to a user's locale or language.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000034 RID: 52
	public enum StringDigitSubstitute
	{
		/// <summary>Specifies a user-defined substitution scheme.</summary>
		// Token: 0x0400029B RID: 667
		User,
		/// <summary>Specifies to disable substitutions.</summary>
		// Token: 0x0400029C RID: 668
		None,
		/// <summary>Specifies substitution digits that correspond with the official national language of the user's locale.</summary>
		// Token: 0x0400029D RID: 669
		National,
		/// <summary>Specifies substitution digits that correspond with the user's native script or language, which may be different from the official national language of the user's locale.</summary>
		// Token: 0x0400029E RID: 670
		Traditional
	}
}
