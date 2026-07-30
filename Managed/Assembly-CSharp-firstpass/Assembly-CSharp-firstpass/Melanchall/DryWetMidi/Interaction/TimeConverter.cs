using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B7 RID: 183
	public static class TimeConverter
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x000145B2 File Offset: 0x000127B2
		public static TTimeSpan ConvertTo<TTimeSpan>(long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(time, 0L, tempoMap);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000145D3 File Offset: 0x000127D3
		public static ITimeSpan ConvertTo(long time, TimeSpanType timeType, TempoMap tempoMap)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(time, timeType, 0L, tempoMap);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00014600 File Offset: 0x00012800
		public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(time, 0L, tempoMap);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00014621 File Offset: 0x00012821
		public static ITimeSpan ConvertTo(ITimeSpan time, TimeSpanType timeType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(time, timeType, 0L, tempoMap);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001464E File Offset: 0x0001284E
		public static ITimeSpan ConvertTo(ITimeSpan time, Type timeType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(time, timeType, 0L, tempoMap);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001467B File Offset: 0x0001287B
		public static long ConvertFrom(ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertFrom(time, 0L, tempoMap);
		}
	}
}
