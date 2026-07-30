using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005CF RID: 1487
	internal static class PropertyHelper
	{
		// Token: 0x040022CE RID: 8910
		internal static WhiteSpaceTrimStringConverter WhiteSpaceTrimStringConverter = new WhiteSpaceTrimStringConverter();

		// Token: 0x040022CF RID: 8911
		internal static InfiniteTimeSpanConverter InfiniteTimeSpanConverter = new InfiniteTimeSpanConverter();

		// Token: 0x040022D0 RID: 8912
		internal static InfiniteIntConverter InfiniteIntConverter = new InfiniteIntConverter();

		// Token: 0x040022D1 RID: 8913
		internal static TimeSpanMinutesConverter TimeSpanMinutesConverter = new TimeSpanMinutesConverter();

		// Token: 0x040022D2 RID: 8914
		internal static TimeSpanSecondsOrInfiniteConverter TimeSpanSecondsOrInfiniteConverter = new TimeSpanSecondsOrInfiniteConverter();

		// Token: 0x040022D3 RID: 8915
		internal static TimeSpanSecondsConverter TimeSpanSecondsConverter = new TimeSpanSecondsConverter();

		// Token: 0x040022D4 RID: 8916
		internal static CommaDelimitedStringCollectionConverter CommaDelimitedStringCollectionConverter = new CommaDelimitedStringCollectionConverter();

		// Token: 0x040022D5 RID: 8917
		internal static DefaultValidator DefaultValidator = new DefaultValidator();

		// Token: 0x040022D6 RID: 8918
		internal static NullableStringValidator NonEmptyStringValidator = new NullableStringValidator(1);

		// Token: 0x040022D7 RID: 8919
		internal static PositiveTimeSpanValidator PositiveTimeSpanValidator = new PositiveTimeSpanValidator();

		// Token: 0x040022D8 RID: 8920
		internal static TimeSpanMinutesOrInfiniteConverter TimeSpanMinutesOrInfiniteConverter = new TimeSpanMinutesOrInfiniteConverter();

		// Token: 0x040022D9 RID: 8921
		internal static IntegerValidator IntFromZeroToMaxValidator = new IntegerValidator(0, int.MaxValue);

		// Token: 0x040022DA RID: 8922
		internal static IntegerValidator IntFromOneToMax_1Validator = new IntegerValidator(1, 2147483646);

		// Token: 0x040022DB RID: 8923
		internal static VersionConverter VersionConverter = new VersionConverter();
	}
}
