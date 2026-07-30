using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B2 RID: 178
	public static class LengthConverter
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x00013E3C File Offset: 0x0001203C
		public static TTimeSpan ConvertTo<TTimeSpan>(long length, long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfLengthArgument.IsNegative("length", length);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(length, time, tempoMap);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00013E67 File Offset: 0x00012067
		public static ITimeSpan ConvertTo(long length, TimeSpanType lengthType, long time, TempoMap tempoMap)
		{
			ThrowIfLengthArgument.IsNegative("length", length);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, time, tempoMap);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013E9E File Offset: 0x0001209E
		public static TTimeSpan ConvertTo<TTimeSpan>(long length, ITimeSpan time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfLengthArgument.IsNegative("length", length);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(length, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013ECF File Offset: 0x000120CF
		public static ITimeSpan ConvertTo(long length, TimeSpanType lengthType, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfLengthArgument.IsNegative("length", length);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00013F0C File Offset: 0x0001210C
		public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan length, long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(length, time, tempoMap);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00013F37 File Offset: 0x00012137
		public static ITimeSpan ConvertTo(ITimeSpan length, TimeSpanType lengthType, long time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, time, tempoMap);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00013F6E File Offset: 0x0001216E
		public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan length, ITimeSpan time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(length, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00013F9F File Offset: 0x0001219F
		public static ITimeSpan ConvertTo(ITimeSpan length, TimeSpanType lengthType, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("lengthType", lengthType);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00013FDC File Offset: 0x000121DC
		public static ITimeSpan ConvertTo(ITimeSpan length, Type lengthType, long time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("lengthType", lengthType);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, time, tempoMap);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00014013 File Offset: 0x00012213
		public static ITimeSpan ConvertTo(ITimeSpan length, Type lengthType, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("lengthType", lengthType);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00014050 File Offset: 0x00012250
		public static long ConvertFrom(ITimeSpan length, long time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertFrom(length, time, tempoMap);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001407B File Offset: 0x0001227B
		public static long ConvertFrom(ITimeSpan length, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertFrom(length, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}
	}
}
