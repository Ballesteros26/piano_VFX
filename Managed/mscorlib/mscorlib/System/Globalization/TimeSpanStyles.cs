using System;

namespace System.Globalization
{
	/// <summary>Defines the formatting options that customize string parsing for the <see cref="Overload:System.TimeSpan.ParseExact" /> and <see cref="Overload:System.TimeSpan.TryParseExact" /> methods.</summary>
	// Token: 0x0200042D RID: 1069
	[Flags]
	public enum TimeSpanStyles
	{
		/// <summary>Indicates that input is interpreted as a negative time interval only if a negative sign is present.</summary>
		// Token: 0x04001B29 RID: 6953
		None = 0,
		/// <summary>Indicates that input is always interpreted as a negative time interval.</summary>
		// Token: 0x04001B2A RID: 6954
		AssumeNegative = 1
	}
}
