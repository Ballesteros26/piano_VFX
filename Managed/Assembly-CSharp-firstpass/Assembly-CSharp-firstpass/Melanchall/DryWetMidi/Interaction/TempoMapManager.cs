using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000AB RID: 171
	public sealed class TempoMapManager : IDisposable
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x000129AB File Offset: 0x00010BAB
		public TempoMapManager()
			: this(new TicksPerQuarterNoteTimeDivision())
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000129B8 File Offset: 0x00010BB8
		public TempoMapManager(TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			this.TempoMap = new TempoMap(timeDivision);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000129D8 File Offset: 0x00010BD8
		public TempoMapManager(TimeDivision timeDivision, IEnumerable<EventsCollection> eventsCollections)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			ThrowIfArgument.IsNull("eventsCollections", eventsCollections);
			ThrowIfArgument.IsEmptyCollection<EventsCollection>("eventsCollections", eventsCollections, "Collection of EventsCollection is empty.");
			this._timedEventsManagers = (from events in eventsCollections
				where events != null
				select events.ManageTimedEvents(null)).ToList<TimedEventsManager>();
			this.TempoMap = new TempoMap(timeDivision);
			this.CollectTimeSignatureChanges();
			this.CollectTempoChanges();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00012A7D File Offset: 0x00010C7D
		public TempoMap TempoMap { get; }

		// Token: 0x060003CB RID: 971 RVA: 0x00012A85 File Offset: 0x00010C85
		public void SetTimeSignature(long time, TimeSignature timeSignature)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			this.TempoMap.TimeSignature.SetValue(time, timeSignature);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012AAF File Offset: 0x00010CAF
		public void SetTimeSignature(ITimeSpan time, TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			this.SetTimeSignature(TimeConverter.ConvertFrom(time, this.TempoMap), timeSignature);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00012ADA File Offset: 0x00010CDA
		public void ClearTimeSignature(long startTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			this.TempoMap.TimeSignature.DeleteValues(startTime);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public void ClearTimeSignature(ITimeSpan startTime)
		{
			ThrowIfArgument.IsNull("startTime", startTime);
			this.ClearTimeSignature(TimeConverter.ConvertFrom(startTime, this.TempoMap));
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012B17 File Offset: 0x00010D17
		public void ClearTimeSignature(long startTime, long endTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			ThrowIfTimeArgument.EndIsNegative("endTime", endTime);
			this.TempoMap.TimeSignature.DeleteValues(startTime, endTime);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00012B41 File Offset: 0x00010D41
		public void ClearTimeSignature(ITimeSpan startTime, ITimeSpan endTime)
		{
			ThrowIfArgument.IsNull("startTime", startTime);
			ThrowIfArgument.IsNull("endTime", endTime);
			this.ClearTimeSignature(TimeConverter.ConvertFrom(startTime, this.TempoMap), TimeConverter.ConvertFrom(endTime, this.TempoMap));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012B77 File Offset: 0x00010D77
		public void SetTempo(long time, Tempo tempo)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempo", tempo);
			this.TempoMap.Tempo.SetValue(time, tempo);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00012BA1 File Offset: 0x00010DA1
		public void SetTempo(ITimeSpan time, Tempo tempo)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempo", tempo);
			this.SetTempo(TimeConverter.ConvertFrom(time, this.TempoMap), tempo);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00012BCC File Offset: 0x00010DCC
		public void ClearTempo(long startTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			this.TempoMap.Tempo.DeleteValues(startTime);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00012BEA File Offset: 0x00010DEA
		public void ClearTempo(ITimeSpan startTime)
		{
			ThrowIfArgument.IsNull("startTime", startTime);
			this.ClearTempo(TimeConverter.ConvertFrom(startTime, this.TempoMap));
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012C09 File Offset: 0x00010E09
		public void ClearTempo(long startTime, long endTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			ThrowIfTimeArgument.EndIsNegative("endTime", endTime);
			this.TempoMap.Tempo.DeleteValues(startTime, endTime);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00012C33 File Offset: 0x00010E33
		public void ClearTempo(ITimeSpan startTime, ITimeSpan endTime)
		{
			ThrowIfArgument.IsNull("startTime", startTime);
			ThrowIfArgument.IsNull("endTime", endTime);
			this.ClearTempo(TimeConverter.ConvertFrom(startTime, this.TempoMap), TimeConverter.ConvertFrom(endTime, this.TempoMap));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012C69 File Offset: 0x00010E69
		public void ClearTempoMap()
		{
			this.TempoMap.Tempo.Clear();
			this.TempoMap.TimeSignature.Clear();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012C8C File Offset: 0x00010E8C
		public void ReplaceTempoMap(TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			this.TempoMap.TimeDivision = tempoMap.TimeDivision.Clone();
			this.TempoMap.Tempo.ReplaceValues(tempoMap.Tempo);
			this.TempoMap.TimeSignature.ReplaceValues(tempoMap.TimeSignature);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012CE8 File Offset: 0x00010EE8
		public void SaveChanges()
		{
			if (this._timedEventsManagers == null)
			{
				return;
			}
			foreach (TimedEventsCollection timedEventsCollection in this._timedEventsManagers.Select((TimedEventsManager m) => m.Events))
			{
				timedEventsCollection.RemoveAll(new Predicate<TimedEvent>(TempoMapManager.IsTempoMapEvent));
			}
			TimedEventsCollection events = this._timedEventsManagers.First<TimedEventsManager>().Events;
			events.Add(this.TempoMap.Tempo.Select(new Func<ValueChange<Tempo>, TimedEvent>(TempoMapManager.GetSetTempoTimedEvent)));
			events.Add(this.TempoMap.TimeSignature.Select(new Func<ValueChange<TimeSignature>, TimedEvent>(TempoMapManager.GetTimeSignatureTimedEvent)));
			foreach (TimedEventsManager timedEventsManager in this._timedEventsManagers)
			{
				timedEventsManager.SaveChanges();
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00012DF8 File Offset: 0x00010FF8
		private IEnumerable<TimedEvent> GetTimedEvents(Func<TimedEvent, bool> predicate)
		{
			return this._timedEventsManagers.SelectMany((TimedEventsManager m) => m.Events).Where(predicate);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00012E2C File Offset: 0x0001102C
		private void CollectTimeSignatureChanges()
		{
			foreach (TimedEvent timedEvent in this.GetTimedEvents(new Func<TimedEvent, bool>(TempoMapManager.IsTimeSignatureEvent)))
			{
				TimeSignatureEvent timeSignatureEvent = timedEvent.Event as TimeSignatureEvent;
				if (timeSignatureEvent != null)
				{
					this.TempoMap.TimeSignature.SetValue(timedEvent.Time, new TimeSignature((int)timeSignatureEvent.Numerator, (int)timeSignatureEvent.Denominator));
				}
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00012EB4 File Offset: 0x000110B4
		private void CollectTempoChanges()
		{
			foreach (TimedEvent timedEvent in this.GetTimedEvents(new Func<TimedEvent, bool>(TempoMapManager.IsTempoEvent)))
			{
				SetTempoEvent setTempoEvent = timedEvent.Event as SetTempoEvent;
				if (setTempoEvent != null)
				{
					this.TempoMap.Tempo.SetValue(timedEvent.Time, new Tempo(setTempoEvent.MicrosecondsPerQuarterNote));
				}
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012F38 File Offset: 0x00011138
		private static bool IsTempoMapEvent(TimedEvent timedEvent)
		{
			return TempoMapManager.IsTempoEvent(timedEvent) || TempoMapManager.IsTimeSignatureEvent(timedEvent);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00012F4A File Offset: 0x0001114A
		private static bool IsTempoEvent(TimedEvent timedEvent)
		{
			return ((timedEvent != null) ? timedEvent.Event : null) is SetTempoEvent;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00012F60 File Offset: 0x00011160
		private static bool IsTimeSignatureEvent(TimedEvent timedEvent)
		{
			return ((timedEvent != null) ? timedEvent.Event : null) is TimeSignatureEvent;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012F76 File Offset: 0x00011176
		private static TimedEvent GetSetTempoTimedEvent(ValueChange<Tempo> tempoChange)
		{
			return new TimedEvent(new SetTempoEvent(tempoChange.Value.MicrosecondsPerQuarterNote), tempoChange.Time);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012F94 File Offset: 0x00011194
		private static TimedEvent GetTimeSignatureTimedEvent(ValueChange<TimeSignature> timeSignatureChange)
		{
			TimeSignature value = timeSignatureChange.Value;
			return new TimedEvent(new TimeSignatureEvent((byte)value.Numerator, (byte)value.Denominator), timeSignatureChange.Time);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00012FC6 File Offset: 0x000111C6
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012FCF File Offset: 0x000111CF
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.SaveChanges();
			}
			this._disposed = true;
		}

		// Token: 0x0400069C RID: 1692
		private readonly IEnumerable<TimedEventsManager> _timedEventsManagers;

		// Token: 0x0400069D RID: 1693
		private bool _disposed;
	}
}
