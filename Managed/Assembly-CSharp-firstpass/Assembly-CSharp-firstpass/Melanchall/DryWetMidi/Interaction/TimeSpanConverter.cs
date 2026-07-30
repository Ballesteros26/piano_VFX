using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B8 RID: 184
	internal static class TimeSpanConverter
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0001469C File Offset: 0x0001289C
		public static TTimeSpan ConvertTo<TTimeSpan>(long timeSpan, long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			return (TTimeSpan)((object)TimeSpanConverter.GetConverter<TTimeSpan>().ConvertTo(timeSpan, time, tempoMap));
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000146B0 File Offset: 0x000128B0
		public static ITimeSpan ConvertTo(long timeSpan, TimeSpanType timeSpanType, long time, TempoMap tempoMap)
		{
			return TimeSpanConverter.GetConverter(timeSpanType).ConvertTo(timeSpan, time, tempoMap);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000146C0 File Offset: 0x000128C0
		public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan timeSpan, long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			if (timeSpan is TTimeSpan)
			{
				return (TTimeSpan)((object)timeSpan.Clone());
			}
			return TimeSpanConverter.ConvertTo<TTimeSpan>(TimeSpanConverter.ConvertFrom(timeSpan, time, tempoMap), time, tempoMap);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000146E5 File Offset: 0x000128E5
		public static ITimeSpan ConvertTo(ITimeSpan timeSpan, TimeSpanType timeSpanType, long time, TempoMap tempoMap)
		{
			if (timeSpan.GetType() == TimeSpanConverter.TimeSpansTypes[timeSpanType])
			{
				return timeSpan.Clone();
			}
			return TimeSpanConverter.ConvertTo(TimeSpanConverter.ConvertFrom(timeSpan, time, tempoMap), timeSpanType, time, tempoMap);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00014716 File Offset: 0x00012916
		public static ITimeSpan ConvertTo(ITimeSpan timeSpan, Type timeSpanType, long time, TempoMap tempoMap)
		{
			if (timeSpan.GetType() == timeSpanType)
			{
				return timeSpan.Clone();
			}
			return TimeSpanConverter.GetConverter(timeSpanType).ConvertTo(TimeSpanConverter.ConvertFrom(timeSpan, time, tempoMap), time, tempoMap);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00014742 File Offset: 0x00012942
		public static long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			return TimeSpanConverter.GetConverter(timeSpan.GetType()).ConvertFrom(timeSpan, time, tempoMap);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00014757 File Offset: 0x00012957
		private static ITimeSpanConverter GetConverter<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			return TimeSpanConverter.GetConverter(typeof(TTimeSpan));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00014768 File Offset: 0x00012968
		private static ITimeSpanConverter GetConverter(TimeSpanType timeSpanType)
		{
			Type type;
			if (!TimeSpanConverter.TimeSpansTypes.TryGetValue(timeSpanType, out type))
			{
				throw new NotSupportedException(string.Format("Converter for {0} is not supported.", timeSpanType));
			}
			return TimeSpanConverter.GetConverter(type);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000147A0 File Offset: 0x000129A0
		private static ITimeSpanConverter GetConverter(Type timeSpanType)
		{
			ITimeSpanConverter timeSpanConverter;
			if (TimeSpanConverter.Converters.TryGetValue(timeSpanType, out timeSpanConverter))
			{
				return timeSpanConverter;
			}
			throw new NotSupportedException(string.Format("Converter for {0} is not supported.", timeSpanType));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000147D0 File Offset: 0x000129D0
		// Note: this type is marked as 'beforefieldinit'.
		static TimeSpanConverter()
		{
			Dictionary<TimeSpanType, Type> dictionary = new Dictionary<TimeSpanType, Type>();
			dictionary[TimeSpanType.Midi] = typeof(MidiTimeSpan);
			dictionary[TimeSpanType.Metric] = typeof(MetricTimeSpan);
			dictionary[TimeSpanType.Musical] = typeof(MusicalTimeSpan);
			dictionary[TimeSpanType.BarBeatTicks] = typeof(BarBeatTicksTimeSpan);
			dictionary[TimeSpanType.BarBeatFraction] = typeof(BarBeatFractionTimeSpan);
			TimeSpanConverter.TimeSpansTypes = dictionary;
			Dictionary<Type, ITimeSpanConverter> dictionary2 = new Dictionary<Type, ITimeSpanConverter>();
			Type typeFromHandle = typeof(MidiTimeSpan);
			dictionary2[typeFromHandle] = new MidiTimeSpanConverter();
			Type typeFromHandle2 = typeof(MetricTimeSpan);
			dictionary2[typeFromHandle2] = new MetricTimeSpanConverter();
			Type typeFromHandle3 = typeof(MusicalTimeSpan);
			dictionary2[typeFromHandle3] = new MusicalTimeSpanConverter();
			Type typeFromHandle4 = typeof(BarBeatTicksTimeSpan);
			dictionary2[typeFromHandle4] = new BarBeatTicksTimeSpanConverter();
			Type typeFromHandle5 = typeof(BarBeatFractionTimeSpan);
			dictionary2[typeFromHandle5] = new BarBeatFractionTimeSpanConverter();
			Type typeFromHandle6 = typeof(MathTimeSpan);
			dictionary2[typeFromHandle6] = new MathTimeSpanConverter();
			TimeSpanConverter.Converters = dictionary2;
		}

		// Token: 0x040006A5 RID: 1701
		private static readonly Dictionary<TimeSpanType, Type> TimeSpansTypes;

		// Token: 0x040006A6 RID: 1702
		private static readonly Dictionary<Type, ITimeSpanConverter> Converters;
	}
}
