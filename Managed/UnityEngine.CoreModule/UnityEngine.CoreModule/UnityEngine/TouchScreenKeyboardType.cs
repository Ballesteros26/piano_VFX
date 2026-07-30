using System;

namespace UnityEngine
{
	// Token: 0x020001F1 RID: 497
	public enum TouchScreenKeyboardType
	{
		// Token: 0x040006CD RID: 1741
		Default,
		// Token: 0x040006CE RID: 1742
		ASCIICapable,
		// Token: 0x040006CF RID: 1743
		NumbersAndPunctuation,
		// Token: 0x040006D0 RID: 1744
		URL,
		// Token: 0x040006D1 RID: 1745
		NumberPad,
		// Token: 0x040006D2 RID: 1746
		PhonePad,
		// Token: 0x040006D3 RID: 1747
		NamePhonePad,
		// Token: 0x040006D4 RID: 1748
		EmailAddress,
		// Token: 0x040006D5 RID: 1749
		[Obsolete("Wii U is no longer supported as of Unity 2018.1.")]
		NintendoNetworkAccount,
		// Token: 0x040006D6 RID: 1750
		Social,
		// Token: 0x040006D7 RID: 1751
		Search,
		// Token: 0x040006D8 RID: 1752
		DecimalPad,
		// Token: 0x040006D9 RID: 1753
		OneTimeCode
	}
}
