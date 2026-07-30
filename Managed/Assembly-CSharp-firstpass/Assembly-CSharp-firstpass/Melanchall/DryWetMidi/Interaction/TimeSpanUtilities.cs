using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000CA RID: 202
	public static class TimeSpanUtilities
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x00016DFC File Offset: 0x00014FFC
		public static bool TryParse(string input, out ITimeSpan timeSpan)
		{
			timeSpan = null;
			foreach (Parsing<ITimeSpan> parsing in TimeSpanUtilities.Parsers.Values)
			{
				if (ParsingUtilities.TryParse<ITimeSpan>(input, parsing, out timeSpan))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00016E60 File Offset: 0x00015060
		public static bool TryParse(string input, TimeSpanType timeSpanType, out ITimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse<ITimeSpan>(input, TimeSpanUtilities.Parsers[timeSpanType], out timeSpan);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00016E74 File Offset: 0x00015074
		public static ITimeSpan Parse(string input)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("input", input, "Input string");
			foreach (Parsing<ITimeSpan> parsing in TimeSpanUtilities.Parsers.Values)
			{
				ITimeSpan timeSpan;
				ParsingResult parsingResult = parsing(input, out timeSpan);
				if (parsingResult.Status == ParsingStatus.Parsed)
				{
					return timeSpan;
				}
				if (parsingResult.Status == ParsingStatus.FormatError)
				{
					throw parsingResult.Exception;
				}
			}
			throw new FormatException("Time span has unknown format.");
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00016F04 File Offset: 0x00015104
		public static ITimeSpan GetMaxTimeSpan(TimeSpanType timeSpanType)
		{
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("timeSpanType", timeSpanType);
			return TimeSpanUtilities.MaximumTimeSpans[timeSpanType];
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00016F1C File Offset: 0x0001511C
		public static ITimeSpan GetZeroTimeSpan(TimeSpanType timeSpanType)
		{
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("timeSpanType", timeSpanType);
			return TimeSpanUtilities.ZeroTimeSpans[timeSpanType];
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00016F34 File Offset: 0x00015134
		public static TTimeSpan GetZeroTimeSpan<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			return (TTimeSpan)((object)TimeSpanUtilities.ZeroTimeSpans.Values.FirstOrDefault((ITimeSpan timeSpan) => timeSpan is TTimeSpan));
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00016F6C File Offset: 0x0001516C
		internal static double Divide(ITimeSpan timeSpan1, ITimeSpan timeSpan2)
		{
			MetricTimeSpan metricTimeSpan = timeSpan1 as MetricTimeSpan;
			if (metricTimeSpan != null)
			{
				return metricTimeSpan.Divide(timeSpan2 as MetricTimeSpan);
			}
			MidiTimeSpan midiTimeSpan = timeSpan1 as MidiTimeSpan;
			if (midiTimeSpan != null)
			{
				return midiTimeSpan.Divide(timeSpan2 as MidiTimeSpan);
			}
			MusicalTimeSpan musicalTimeSpan = timeSpan1 as MusicalTimeSpan;
			if (musicalTimeSpan != null)
			{
				return musicalTimeSpan.Divide(timeSpan2 as MusicalTimeSpan);
			}
			throw new NotSupportedException(string.Format("Dividing of time span of the '{0}' type is not supported.", timeSpan1.GetType()));
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00016FE5 File Offset: 0x000151E5
		internal static ITimeSpan Add(ITimeSpan timeSpan1, ITimeSpan timeSpan2, TimeSpanMode mode)
		{
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			if (mode == TimeSpanMode.TimeTime)
			{
				throw new ArgumentException("Times cannot be added.", "mode");
			}
			return new MathTimeSpan(timeSpan1, timeSpan2, MathOperation.Add, mode);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001700E File Offset: 0x0001520E
		internal static ITimeSpan Subtract(ITimeSpan timeSpan1, ITimeSpan timeSpan2, TimeSpanMode mode)
		{
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			return new MathTimeSpan(timeSpan1, timeSpan2, MathOperation.Subtract, mode);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017024 File Offset: 0x00015224
		private static Parsing<ITimeSpan> GetParsing<TTimeSpan>(Parsing<TTimeSpan> parsing) where TTimeSpan : ITimeSpan
		{
			return delegate(string input, out ITimeSpan timeSpan)
			{
				TTimeSpan ttimeSpan;
				ParsingResult parsingResult = parsing(input, out ttimeSpan);
				timeSpan = ttimeSpan;
				return parsingResult;
			};
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00017040 File Offset: 0x00015240
		// Note: this type is marked as 'beforefieldinit'.
		static TimeSpanUtilities()
		{
			Dictionary<TimeSpanType, Parsing<ITimeSpan>> dictionary = new Dictionary<TimeSpanType, Parsing<ITimeSpan>>();
			dictionary[TimeSpanType.Midi] = TimeSpanUtilities.GetParsing<MidiTimeSpan>(new Parsing<MidiTimeSpan>(MidiTimeSpanParser.TryParse));
			dictionary[TimeSpanType.BarBeatTicks] = TimeSpanUtilities.GetParsing<BarBeatTicksTimeSpan>(new Parsing<BarBeatTicksTimeSpan>(BarBeatTicksTimeSpanParser.TryParse));
			dictionary[TimeSpanType.BarBeatFraction] = TimeSpanUtilities.GetParsing<BarBeatFractionTimeSpan>(new Parsing<BarBeatFractionTimeSpan>(BarBeatFractionTimeSpanParser.TryParse));
			dictionary[TimeSpanType.Metric] = TimeSpanUtilities.GetParsing<MetricTimeSpan>(new Parsing<MetricTimeSpan>(MetricTimeSpanParser.TryParse));
			dictionary[TimeSpanType.Musical] = TimeSpanUtilities.GetParsing<MusicalTimeSpan>(new Parsing<MusicalTimeSpan>(MusicalTimeSpanParser.TryParse));
			TimeSpanUtilities.Parsers = dictionary;
			Dictionary<TimeSpanType, ITimeSpan> dictionary2 = new Dictionary<TimeSpanType, ITimeSpan>();
			dictionary2[TimeSpanType.Midi] = new MidiTimeSpan(long.MaxValue);
			dictionary2[TimeSpanType.Metric] = new MetricTimeSpan(TimeSpan.MaxValue);
			dictionary2[TimeSpanType.Musical] = new MusicalTimeSpan(long.MaxValue, 1L, true);
			dictionary2[TimeSpanType.BarBeatTicks] = new BarBeatTicksTimeSpan(long.MaxValue, long.MaxValue, long.MaxValue);
			dictionary2[TimeSpanType.BarBeatFraction] = new BarBeatFractionTimeSpan(long.MaxValue, double.MaxValue);
			TimeSpanUtilities.MaximumTimeSpans = dictionary2;
			Dictionary<TimeSpanType, ITimeSpan> dictionary3 = new Dictionary<TimeSpanType, ITimeSpan>();
			dictionary3[TimeSpanType.Midi] = new MidiTimeSpan();
			dictionary3[TimeSpanType.Metric] = new MetricTimeSpan();
			dictionary3[TimeSpanType.Musical] = new MusicalTimeSpan();
			dictionary3[TimeSpanType.BarBeatTicks] = new BarBeatTicksTimeSpan();
			dictionary3[TimeSpanType.BarBeatFraction] = new BarBeatFractionTimeSpan();
			TimeSpanUtilities.ZeroTimeSpans = dictionary3;
		}

		// Token: 0x0400071F RID: 1823
		private static readonly Dictionary<TimeSpanType, Parsing<ITimeSpan>> Parsers;

		// Token: 0x04000720 RID: 1824
		private static readonly Dictionary<TimeSpanType, ITimeSpan> MaximumTimeSpans;

		// Token: 0x04000721 RID: 1825
		private static readonly Dictionary<TimeSpanType, ITimeSpan> ZeroTimeSpans;
	}
}
