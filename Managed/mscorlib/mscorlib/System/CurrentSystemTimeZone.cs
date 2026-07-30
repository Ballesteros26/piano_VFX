using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200023B RID: 571
	[Serializable]
	internal class CurrentSystemTimeZone : TimeZone
	{
		// Token: 0x06001B24 RID: 6948 RVA: 0x00066EA3 File Offset: 0x000650A3
		internal CurrentSystemTimeZone()
		{
			this.LocalTimeZone = TimeZoneInfo.Local;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00066EB6 File Offset: 0x000650B6
		public override string DaylightName
		{
			get
			{
				return this.LocalTimeZone.DaylightName;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x00066EC3 File Offset: 0x000650C3
		public override string StandardName
		{
			get
			{
				return this.LocalTimeZone.StandardName;
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00066ED0 File Offset: 0x000650D0
		public override DaylightTime GetDaylightChanges(int year)
		{
			return this.LocalTimeZone.GetDaylightChanges(year);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00066EDE File Offset: 0x000650DE
		public override TimeSpan GetUtcOffset(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			return this.LocalTimeZone.GetUtcOffset(dateTime);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00066EFC File Offset: 0x000650FC
		public override bool IsDaylightSavingTime(DateTime dateTime)
		{
			return dateTime.Kind != DateTimeKind.Utc && this.LocalTimeZone.IsDaylightSavingTime(dateTime);
		}

		// Token: 0x06001B2A RID: 6954
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetTimeZoneData(int year, out long[] data, out string[] names, out bool daylight_inverted);

		// Token: 0x04000F3B RID: 3899
		private readonly TimeZoneInfo LocalTimeZone;
	}
}
