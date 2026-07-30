using System;

namespace System.Drawing.Printing
{
	/// <summary>Standard paper sources.</summary>
	// Token: 0x020000B9 RID: 185
	public enum PaperSourceKind
	{
		/// <summary>The upper bin of a printer (or the default bin, if the printer only has one bin).</summary>
		// Token: 0x040006CE RID: 1742
		Upper = 1,
		/// <summary>The lower bin of a printer.</summary>
		// Token: 0x040006CF RID: 1743
		Lower,
		/// <summary>The middle bin of a printer.</summary>
		// Token: 0x040006D0 RID: 1744
		Middle,
		/// <summary>Manually fed paper.</summary>
		// Token: 0x040006D1 RID: 1745
		Manual,
		/// <summary>An envelope.</summary>
		// Token: 0x040006D2 RID: 1746
		Envelope,
		/// <summary>Manually fed envelope.</summary>
		// Token: 0x040006D3 RID: 1747
		ManualFeed,
		/// <summary>Automatically fed paper.</summary>
		// Token: 0x040006D4 RID: 1748
		AutomaticFeed,
		/// <summary>A tractor feed.</summary>
		// Token: 0x040006D5 RID: 1749
		TractorFeed,
		/// <summary>Small-format paper.</summary>
		// Token: 0x040006D6 RID: 1750
		SmallFormat,
		/// <summary>Large-format paper.</summary>
		// Token: 0x040006D7 RID: 1751
		LargeFormat,
		/// <summary>The printer's large-capacity bin.</summary>
		// Token: 0x040006D8 RID: 1752
		LargeCapacity,
		/// <summary>A paper cassette.</summary>
		// Token: 0x040006D9 RID: 1753
		Cassette = 14,
		/// <summary>The printer's default input bin.</summary>
		// Token: 0x040006DA RID: 1754
		FormSource,
		/// <summary>A printer-specific paper source.</summary>
		// Token: 0x040006DB RID: 1755
		Custom = 257
	}
}
