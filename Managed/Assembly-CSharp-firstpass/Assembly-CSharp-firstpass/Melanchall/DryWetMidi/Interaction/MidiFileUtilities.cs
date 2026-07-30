using System;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D6 RID: 214
	public static class MidiFileUtilities
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x00017D80 File Offset: 0x00015F80
		public static TTimeSpan GetDuration<TTimeSpan>(this MidiFile midiFile) where TTimeSpan : class, ITimeSpan
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			TempoMap tempoMap = midiFile.GetTempoMap();
			TimedEvent timedEvent = midiFile.GetTimedEvents().LastOrDefault<TimedEvent>();
			TTimeSpan ttimeSpan;
			if ((ttimeSpan = ((timedEvent != null) ? timedEvent.TimeAs(tempoMap) : default(TTimeSpan))) == null)
			{
				ttimeSpan = TimeSpanUtilities.GetZeroTimeSpan<TTimeSpan>();
			}
			return ttimeSpan;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00017DD0 File Offset: 0x00015FD0
		public static ITimeSpan GetDuration(this MidiFile midiFile, TimeSpanType durationType)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("durationType", durationType);
			TempoMap tempoMap = midiFile.GetTempoMap();
			TimedEvent timedEvent = midiFile.GetTimedEvents().LastOrDefault<TimedEvent>();
			return ((timedEvent != null) ? timedEvent.TimeAs(durationType, tempoMap) : null) ?? TimeSpanUtilities.GetZeroTimeSpan(durationType);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00017E1D File Offset: 0x0001601D
		public static bool IsEmpty(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return !midiFile.GetEvents().Any<MidiEvent>();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00017E38 File Offset: 0x00016038
		public static void ShiftEvents(this MidiFile midiFile, ITimeSpan distance)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("distance", distance);
			midiFile.GetTrackChunks().ShiftEvents(distance, midiFile.GetTempoMap());
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00017E64 File Offset: 0x00016064
		public static void Resize(this MidiFile midiFile, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("length", length);
			if (midiFile.IsEmpty())
			{
				return;
			}
			TempoMap tempoMap = midiFile.GetTempoMap();
			ITimeSpan timeSpan = TimeConverter.ConvertTo(midiFile.GetDuration<MidiTimeSpan>(), length.GetType(), tempoMap);
			double num = TimeSpanUtilities.Divide(length, timeSpan);
			MidiFileUtilities.ResizeByRatio(midiFile, num);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00017EB9 File Offset: 0x000160B9
		public static void Resize(this MidiFile midiFile, double ratio)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNegative("ratio", ratio, "Ratio is negative");
			MidiFileUtilities.ResizeByRatio(midiFile, ratio);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00017EE0 File Offset: 0x000160E0
		private static void ResizeByRatio(MidiFile midiFile, double ratio)
		{
			midiFile.ProcessTimedEvents(delegate(TimedEvent e)
			{
				e.Time = MathUtilities.RoundToLong((double)e.Time * ratio);
			}, null);
		}
	}
}
