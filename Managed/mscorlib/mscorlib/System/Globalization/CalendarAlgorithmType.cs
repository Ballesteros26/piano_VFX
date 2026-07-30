using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Specifies whether a calendar is solar-based, lunar-based, or lunisolar-based.</summary>
	// Token: 0x020003FA RID: 1018
	[ComVisible(true)]
	public enum CalendarAlgorithmType
	{
		/// <summary>An unknown calendar basis.</summary>
		// Token: 0x040018EF RID: 6383
		Unknown,
		/// <summary>A solar-based calendar.</summary>
		// Token: 0x040018F0 RID: 6384
		SolarCalendar,
		/// <summary>A lunar-based calendar.</summary>
		// Token: 0x040018F1 RID: 6385
		LunarCalendar,
		/// <summary>A lunisolar-based calendar.</summary>
		// Token: 0x040018F2 RID: 6386
		LunisolarCalendar
	}
}
