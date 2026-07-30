using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000102 RID: 258
	public sealed class Playback : IDisposable, IClockDrivenObject
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600068C RID: 1676 RVA: 0x0001A878 File Offset: 0x00018A78
		// (remove) Token: 0x0600068D RID: 1677 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		public event EventHandler Started;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600068E RID: 1678 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		// (remove) Token: 0x0600068F RID: 1679 RVA: 0x0001A920 File Offset: 0x00018B20
		public event EventHandler Stopped;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000690 RID: 1680 RVA: 0x0001A958 File Offset: 0x00018B58
		// (remove) Token: 0x06000691 RID: 1681 RVA: 0x0001A990 File Offset: 0x00018B90
		public event EventHandler Finished;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000692 RID: 1682 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		// (remove) Token: 0x06000693 RID: 1683 RVA: 0x0001AA00 File Offset: 0x00018C00
		public event EventHandler<NotesEventArgs> NotesPlaybackStarted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000694 RID: 1684 RVA: 0x0001AA38 File Offset: 0x00018C38
		// (remove) Token: 0x06000695 RID: 1685 RVA: 0x0001AA70 File Offset: 0x00018C70
		public event EventHandler<NotesEventArgs> NotesPlaybackFinished;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000696 RID: 1686 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		// (remove) Token: 0x06000697 RID: 1687 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		public event EventHandler<MidiEventPlayedEventArgs> EventPlayed;

		// Token: 0x06000698 RID: 1688 RVA: 0x0001AB15 File Offset: 0x00018D15
		public Playback(IEnumerable<MidiEvent> events, TempoMap tempoMap, MidiClockSettings clockSettings = null)
			: this(new IEnumerable<MidiEvent>[] { events }, tempoMap, clockSettings)
		{
			ThrowIfArgument.IsNull("events", events);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001AB34 File Offset: 0x00018D34
		public Playback(IEnumerable<MidiEvent> events, TempoMap tempoMap, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
			: this(new IEnumerable<MidiEvent>[] { events }, tempoMap, outputDevice, clockSettings)
		{
			ThrowIfArgument.IsNull("events", events);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001AB55 File Offset: 0x00018D55
		public Playback(IEnumerable<IEnumerable<MidiEvent>> events, TempoMap tempoMap, MidiClockSettings clockSettings = null)
			: this(Playback.GetTimedObjects(events), tempoMap, clockSettings)
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001AB65 File Offset: 0x00018D65
		public Playback(IEnumerable<IEnumerable<MidiEvent>> events, TempoMap tempoMap, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
			: this(Playback.GetTimedObjects(events), tempoMap, outputDevice, clockSettings)
		{
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001AB78 File Offset: 0x00018D78
		public Playback(IEnumerable<ITimedObject> timedObjects, TempoMap tempoMap, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("timedObjects", timedObjects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ICollection<PlaybackEvent> playbackEvents = Playback.GetPlaybackEvents(timedObjects, tempoMap);
			this._eventsEnumerator = playbackEvents.GetEnumerator();
			this._eventsEnumerator.MoveNext();
			PlaybackEvent playbackEvent = playbackEvents.LastOrDefault<PlaybackEvent>();
			this._duration = ((playbackEvent != null) ? playbackEvent.Time : TimeSpan.Zero);
			this._durationInTicks = ((playbackEvent != null) ? playbackEvent.RawTime : 0L);
			this._notesMetadata = (from e in playbackEvents
				select e.Metadata.Note into m
				where m != null
				select m).ToList<NotePlaybackEventMetadata>();
			this._notesMetadata.Sort((NotePlaybackEventMetadata m1, NotePlaybackEventMetadata m2) => m1.StartTime.CompareTo(m2.StartTime));
			this.TempoMap = tempoMap;
			clockSettings = clockSettings ?? new MidiClockSettings();
			this._clock = new MidiClock(false, clockSettings.CreateTickGeneratorCallback(Playback.ClockInterval));
			this._clock.Ticked += this.OnClockTicked;
			this.Snapping = new PlaybackSnapping(playbackEvents, tempoMap);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001ACCD File Offset: 0x00018ECD
		public Playback(IEnumerable<ITimedObject> timedObjects, TempoMap tempoMap, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
			: this(timedObjects, tempoMap, clockSettings)
		{
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			this.OutputDevice = outputDevice;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001ACEC File Offset: 0x00018EEC
		~Playback()
		{
			this.Dispose(false);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001AD1C File Offset: 0x00018F1C
		public TempoMap TempoMap { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001AD24 File Offset: 0x00018F24
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001AD2C File Offset: 0x00018F2C
		public IOutputDevice OutputDevice { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001AD35 File Offset: 0x00018F35
		public bool IsRunning
		{
			get
			{
				return this._clock.IsRunning;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001AD42 File Offset: 0x00018F42
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001AD4A File Offset: 0x00018F4A
		public bool Loop { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001AD53 File Offset: 0x00018F53
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001AD5B File Offset: 0x00018F5B
		public bool InterruptNotesOnStop { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001AD64 File Offset: 0x00018F64
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001AD6C File Offset: 0x00018F6C
		public bool TrackNotes { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001AD75 File Offset: 0x00018F75
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001AD82 File Offset: 0x00018F82
		public double Speed
		{
			get
			{
				return this._clock.Speed;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Speed is zero or negative.");
				this.EnsureIsNotDisposed();
				this._clock.Speed = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001ADA6 File Offset: 0x00018FA6
		public PlaybackSnapping Snapping { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001ADAE File Offset: 0x00018FAE
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x0001ADB6 File Offset: 0x00018FB6
		public NoteCallback NoteCallback { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001ADBF File Offset: 0x00018FBF
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0001ADC7 File Offset: 0x00018FC7
		public EventCallback EventCallback { get; set; }

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001ADD0 File Offset: 0x00018FD0
		public ITimeSpan GetDuration(TimeSpanType durationType)
		{
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("durationType", durationType);
			return TimeConverter.ConvertTo(this._duration, durationType, this.TempoMap);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		public TTimeSpan GetDuration<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			return TimeConverter.ConvertTo<TTimeSpan>(this._duration, this.TempoMap);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001AE0C File Offset: 0x0001900C
		public ITimeSpan GetCurrentTime(TimeSpanType timeType)
		{
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("timeType", timeType);
			return TimeConverter.ConvertTo(this._clock.CurrentTime, timeType, this.TempoMap);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001AE35 File Offset: 0x00019035
		public TTimeSpan GetCurrentTime<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			return TimeConverter.ConvertTo<TTimeSpan>(this._clock.CurrentTime, this.TempoMap);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001AE52 File Offset: 0x00019052
		public void Start()
		{
			this.EnsureIsNotDisposed();
			if (this._clock.IsRunning)
			{
				return;
			}
			IOutputDevice outputDevice = this.OutputDevice;
			if (outputDevice != null)
			{
				outputDevice.PrepareForEventsSending();
			}
			this.StopStartNotes();
			this._clock.Start();
			this.OnStarted();
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001AE90 File Offset: 0x00019090
		public void Stop()
		{
			this.EnsureIsNotDisposed();
			if (!this.IsRunning)
			{
				return;
			}
			this._clock.Stop();
			if (this.InterruptNotesOnStop)
			{
				TimeSpan currentTime = this._clock.CurrentTime;
				List<Note> list = new List<Note>();
				foreach (NotePlaybackEventMetadata notePlaybackEventMetadata in this._activeNotesMetadata.ToArray<NotePlaybackEventMetadata>())
				{
					Note note;
					if (this.TryPlayNoteEvent(notePlaybackEventMetadata, false, currentTime, out note))
					{
						list.Add(note);
					}
				}
				this.OnNotesPlaybackFinished(list.ToArray());
				this._activeNotesMetadata.Clear();
			}
			this.OnStopped();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001AF23 File Offset: 0x00019123
		public void Play()
		{
			this.EnsureIsNotDisposed();
			this.Start();
			SpinWait.SpinUntil(() => !this._clock.IsRunning);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001AF42 File Offset: 0x00019142
		public void MoveToSnapPoint(SnapPoint snapPoint)
		{
			ThrowIfArgument.IsNull("snapPoint", snapPoint);
			this.EnsureIsNotDisposed();
			if (!snapPoint.IsEnabled)
			{
				return;
			}
			this.MoveToTime(snapPoint.Time);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001AF70 File Offset: 0x00019170
		public void MoveToPreviousSnapPoint(SnapPointsGroup snapPointsGroup)
		{
			ThrowIfArgument.IsNull("snapPointsGroup", snapPointsGroup);
			this.EnsureIsNotDisposed();
			SnapPoint previousSnapPoint = this.Snapping.GetPreviousSnapPoint(this._clock.CurrentTime, snapPointsGroup);
			if (previousSnapPoint != null)
			{
				this.MoveToTime(previousSnapPoint.Time);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001AFBC File Offset: 0x000191BC
		public void MoveToPreviousSnapPoint()
		{
			this.EnsureIsNotDisposed();
			SnapPoint previousSnapPoint = this.Snapping.GetPreviousSnapPoint(this._clock.CurrentTime);
			if (previousSnapPoint != null)
			{
				this.MoveToTime(previousSnapPoint.Time);
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001AFFC File Offset: 0x000191FC
		public void MoveToNextSnapPoint(SnapPointsGroup snapPointsGroup)
		{
			ThrowIfArgument.IsNull("snapPointsGroup", snapPointsGroup);
			this.EnsureIsNotDisposed();
			SnapPoint nextSnapPoint = this.Snapping.GetNextSnapPoint(this._clock.CurrentTime, snapPointsGroup);
			if (nextSnapPoint != null)
			{
				this.MoveToTime(nextSnapPoint.Time);
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001B048 File Offset: 0x00019248
		public void MoveToNextSnapPoint()
		{
			this.EnsureIsNotDisposed();
			SnapPoint nextSnapPoint = this.Snapping.GetNextSnapPoint(this._clock.CurrentTime);
			if (nextSnapPoint != null)
			{
				this.MoveToTime(nextSnapPoint.Time);
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001B086 File Offset: 0x00019286
		public void MoveToStart()
		{
			this.EnsureIsNotDisposed();
			this.MoveToTime(new MetricTimeSpan());
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001B09C File Offset: 0x0001929C
		public void MoveToTime(ITimeSpan time)
		{
			ThrowIfArgument.IsNull("time", time);
			this.EnsureIsNotDisposed();
			if (TimeConverter.ConvertFrom(time, this.TempoMap) > this._durationInTicks)
			{
				time = this._duration;
			}
			bool isRunning = this.IsRunning;
			this.SetStartTime(time);
			if (isRunning)
			{
				this.StopStartNotes();
				this._clock.Start();
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001B0FC File Offset: 0x000192FC
		public void MoveForward(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			this.EnsureIsNotDisposed();
			MetricTimeSpan metricTimeSpan = this._clock.CurrentTime;
			this.MoveToTime(metricTimeSpan.Add(step, TimeSpanMode.TimeLength));
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001B13C File Offset: 0x0001933C
		public void MoveBack(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			this.EnsureIsNotDisposed();
			MetricTimeSpan metricTimeSpan = this._clock.CurrentTime;
			ITimeSpan timeSpan;
			if (!(TimeConverter.ConvertTo<MetricTimeSpan>(step, this.TempoMap) > metricTimeSpan))
			{
				timeSpan = metricTimeSpan.Subtract(step, TimeSpanMode.TimeLength);
			}
			else
			{
				ITimeSpan timeSpan2 = new MetricTimeSpan();
				timeSpan = timeSpan2;
			}
			this.MoveToTime(timeSpan);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001B198 File Offset: 0x00019398
		private void StopStartNotes()
		{
			if (!this.TrackNotes)
			{
				return;
			}
			TimeSpan currentTime = this._clock.CurrentTime;
			NotePlaybackEventMetadata[] notesToPlay = (from m in this._notesMetadata.SkipWhile((NotePlaybackEventMetadata m) => m.EndTime <= currentTime).TakeWhile((NotePlaybackEventMetadata m) => m.StartTime < currentTime)
				where m.StartTime < currentTime && m.EndTime > currentTime
				select m).Distinct<NotePlaybackEventMetadata>().ToArray<NotePlaybackEventMetadata>();
			NotePlaybackEventMetadata[] array = notesToPlay.Where((NotePlaybackEventMetadata n) => !this._activeNotesMetadata.Contains(n)).ToArray<NotePlaybackEventMetadata>();
			NotePlaybackEventMetadata[] array2 = this._activeNotesMetadata.Where((NotePlaybackEventMetadata n) => !notesToPlay.Contains(n)).ToArray<NotePlaybackEventMetadata>();
			IOutputDevice outputDevice = this.OutputDevice;
			if (outputDevice != null)
			{
				outputDevice.PrepareForEventsSending();
			}
			List<Note> list = new List<Note>();
			foreach (NotePlaybackEventMetadata notePlaybackEventMetadata in array2)
			{
				Note note;
				this.TryPlayNoteEvent(notePlaybackEventMetadata, false, currentTime, out note);
				list.Add(note);
			}
			this.OnNotesPlaybackFinished(list.ToArray());
			list.Clear();
			foreach (NotePlaybackEventMetadata notePlaybackEventMetadata2 in array)
			{
				Note note;
				this.TryPlayNoteEvent(notePlaybackEventMetadata2, true, currentTime, out note);
				list.Add(note);
			}
			this.OnNotesPlaybackStarted(list.ToArray());
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001B2EE File Offset: 0x000194EE
		private void OnStarted()
		{
			EventHandler started = this.Started;
			if (started == null)
			{
				return;
			}
			started(this, EventArgs.Empty);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001B306 File Offset: 0x00019506
		private void OnStopped()
		{
			EventHandler stopped = this.Stopped;
			if (stopped == null)
			{
				return;
			}
			stopped(this, EventArgs.Empty);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001B31E File Offset: 0x0001951E
		private void OnFinished()
		{
			EventHandler finished = this.Finished;
			if (finished == null)
			{
				return;
			}
			finished(this, EventArgs.Empty);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001B336 File Offset: 0x00019536
		private void OnNotesPlaybackStarted(params Note[] notes)
		{
			EventHandler<NotesEventArgs> notesPlaybackStarted = this.NotesPlaybackStarted;
			if (notesPlaybackStarted == null)
			{
				return;
			}
			notesPlaybackStarted(this, new NotesEventArgs(notes));
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001B34F File Offset: 0x0001954F
		private void OnNotesPlaybackFinished(params Note[] notes)
		{
			EventHandler<NotesEventArgs> notesPlaybackFinished = this.NotesPlaybackFinished;
			if (notesPlaybackFinished == null)
			{
				return;
			}
			notesPlaybackFinished(this, new NotesEventArgs(notes));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001B368 File Offset: 0x00019568
		private void OnEventPlayed(MidiEvent midiEvent)
		{
			EventHandler<MidiEventPlayedEventArgs> eventPlayed = this.EventPlayed;
			if (eventPlayed == null)
			{
				return;
			}
			eventPlayed(this, new MidiEventPlayedEventArgs(midiEvent));
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001B384 File Offset: 0x00019584
		private void OnClockTicked(object sender, EventArgs e)
		{
			for (;;)
			{
				TimeSpan currentTime = this._clock.CurrentTime;
				PlaybackEvent playbackEvent = this._eventsEnumerator.Current;
				if (playbackEvent != null)
				{
					if (playbackEvent.Time > currentTime)
					{
						break;
					}
					MidiEvent midiEvent = playbackEvent.Event;
					if (midiEvent != null)
					{
						if (!this.IsRunning)
						{
							return;
						}
						Note note;
						if (this.TryPlayNoteEvent(playbackEvent, out note))
						{
							if (note != null)
							{
								if (playbackEvent.Event is NoteOnEvent)
								{
									this.OnNotesPlaybackStarted(new Note[] { note });
								}
								else
								{
									this.OnNotesPlaybackFinished(new Note[] { note });
								}
							}
						}
						else
						{
							EventCallback eventCallback = this.EventCallback;
							if (eventCallback != null)
							{
								midiEvent = eventCallback(midiEvent.Clone(), playbackEvent.RawTime, currentTime);
							}
							if (midiEvent != null)
							{
								this.SendEvent(midiEvent);
							}
						}
					}
				}
				if (!this._eventsEnumerator.MoveNext())
				{
					goto Block_9;
				}
			}
			return;
			Block_9:
			if (!this.Loop)
			{
				this._clock.Stop();
				this.OnFinished();
				return;
			}
			this._clock.Stop();
			this._clock.ResetCurrentTime();
			this._eventsEnumerator.Reset();
			this._eventsEnumerator.MoveNext();
			this._clock.Start();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001B49D File Offset: 0x0001969D
		private void EnsureIsNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("Playback is disposed.");
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001B4B4 File Offset: 0x000196B4
		private void SetStartTime(ITimeSpan time)
		{
			TimeSpan timeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(time, this.TempoMap);
			this._clock.SetCurrentTime(timeSpan);
			this._eventsEnumerator.Reset();
			do
			{
				this._eventsEnumerator.MoveNext();
			}
			while (this._eventsEnumerator.Current != null && this._eventsEnumerator.Current.Time < timeSpan);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001B51B File Offset: 0x0001971B
		private void SendEvent(MidiEvent midiEvent)
		{
			IOutputDevice outputDevice = this.OutputDevice;
			if (outputDevice != null)
			{
				outputDevice.SendEvent(midiEvent);
			}
			this.OnEventPlayed(midiEvent);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001B536 File Offset: 0x00019736
		private bool TryPlayNoteEvent(NotePlaybackEventMetadata noteMetadata, bool isNoteOnEvent, TimeSpan time, out Note note)
		{
			return this.TryPlayNoteEvent(noteMetadata, null, isNoteOnEvent, time, out note);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001B544 File Offset: 0x00019744
		private bool TryPlayNoteEvent(PlaybackEvent playbackEvent, out Note note)
		{
			return this.TryPlayNoteEvent(playbackEvent.Metadata.Note, playbackEvent.Event, playbackEvent.Event is NoteOnEvent, playbackEvent.Time, out note);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001B574 File Offset: 0x00019774
		private bool TryPlayNoteEvent(NotePlaybackEventMetadata noteMetadata, MidiEvent midiEvent, bool isNoteOnEvent, TimeSpan time, out Note note)
		{
			note = null;
			if (noteMetadata == null)
			{
				return false;
			}
			NotePlaybackData notePlaybackData = noteMetadata.NotePlaybackData;
			NoteCallback noteCallback = this.NoteCallback;
			if (noteCallback != null && midiEvent is NoteOnEvent)
			{
				notePlaybackData = noteCallback(noteMetadata.RawNotePlaybackData, noteMetadata.RawNote.Time, noteMetadata.RawNote.Length, time);
				noteMetadata.SetCustomNotePlaybackData(notePlaybackData);
			}
			note = noteMetadata.RawNote;
			if (noteMetadata.IsCustomNotePlaybackDataSet)
			{
				if (notePlaybackData == null || !notePlaybackData.PlayNote)
				{
					midiEvent = null;
				}
				else
				{
					note = noteMetadata.GetEffectiveNote();
					midiEvent = ((midiEvent is NoteOnEvent) ? notePlaybackData.GetNoteOnEvent() : notePlaybackData.GetNoteOffEvent());
				}
			}
			else if (midiEvent == null)
			{
				midiEvent = (isNoteOnEvent ? notePlaybackData.GetNoteOnEvent() : notePlaybackData.GetNoteOffEvent());
			}
			if (midiEvent != null)
			{
				this.SendEvent(midiEvent);
				if (midiEvent is NoteOnEvent)
				{
					this._activeNotesMetadata.Add(noteMetadata);
				}
				else
				{
					this._activeNotesMetadata.Remove(noteMetadata);
				}
			}
			else
			{
				note = null;
			}
			return true;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001B660 File Offset: 0x00019860
		private static ICollection<PlaybackEvent> GetPlaybackEvents(IEnumerable<ITimedObject> timedObjects, TempoMap tempoMap)
		{
			List<PlaybackEvent> list = new List<PlaybackEvent>();
			foreach (ITimedObject timedObject in timedObjects)
			{
				Chord chord = timedObject as Chord;
				if (chord != null)
				{
					list.AddRange(Playback.GetPlaybackEvents(chord, tempoMap));
				}
				else
				{
					Note note = timedObject as Note;
					if (note != null)
					{
						list.AddRange(Playback.GetPlaybackEvents(note, tempoMap));
					}
					else
					{
						TimedEvent timedEvent = timedObject as TimedEvent;
						if (timedEvent != null)
						{
							list.Add(new PlaybackEvent(timedEvent.Event, timedEvent.TimeAs(tempoMap), timedEvent.Time));
						}
					}
				}
			}
			return list.OrderBy((PlaybackEvent e) => e, new PlaybackEventsComparer()).ToList<PlaybackEvent>();
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001B740 File Offset: 0x00019940
		private static IEnumerable<PlaybackEvent> GetPlaybackEvents(Chord chord, TempoMap tempoMap)
		{
			foreach (Note note in chord.Notes)
			{
				foreach (PlaybackEvent playbackEvent in Playback.GetPlaybackEvents(note, tempoMap))
				{
					yield return playbackEvent;
				}
				IEnumerator<PlaybackEvent> enumerator2 = null;
			}
			IEnumerator<Note> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001B757 File Offset: 0x00019957
		private static IEnumerable<PlaybackEvent> GetPlaybackEvents(Note note, TempoMap tempoMap)
		{
			TimeSpan timeSpan = note.TimeAs(tempoMap);
			TimeSpan timeSpan2 = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time + note.Length, tempoMap);
			NotePlaybackEventMetadata noteMetadata = new NotePlaybackEventMetadata(note, timeSpan, timeSpan2);
			yield return Playback.GetPlaybackEventWithNoteMetadata(note.TimedNoteOnEvent, tempoMap, noteMetadata);
			yield return Playback.GetPlaybackEventWithNoteMetadata(note.TimedNoteOffEvent, tempoMap, noteMetadata);
			yield break;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001B76E File Offset: 0x0001996E
		private static PlaybackEvent GetPlaybackEventWithNoteMetadata(TimedEvent timedEvent, TempoMap tempoMap, NotePlaybackEventMetadata noteMetadata)
		{
			return new PlaybackEvent(timedEvent.Event, timedEvent.TimeAs(tempoMap), timedEvent.Time)
			{
				Metadata = 
				{
					Note = noteMetadata
				}
			};
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001B79C File Offset: 0x0001999C
		private static IEnumerable<ITimedObject> GetTimedObjects(IEnumerable<IEnumerable<MidiEvent>> events)
		{
			ThrowIfArgument.IsNull("events", events);
			return events.Where((IEnumerable<MidiEvent> e) => e != null).SelectMany((IEnumerable<MidiEvent> e) => (from timedEvent in e.Where((MidiEvent midiEvent) => midiEvent != null).GetTimedEvents()
				where !(timedEvent.Event is MetaEvent)
				select timedEvent).GetTimedEventsAndNotes());
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001B7FD File Offset: 0x000199FD
		public void TickClock()
		{
			this.EnsureIsNotDisposed();
			MidiClock clock = this._clock;
			if (clock == null)
			{
				return;
			}
			clock.Tick();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001B815 File Offset: 0x00019A15
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001B820 File Offset: 0x00019A20
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.Stop();
				this._clock.Ticked -= this.OnClockTicked;
				this._clock.Dispose();
				this._eventsEnumerator.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x040007EC RID: 2028
		private static readonly TimeSpan ClockInterval = TimeSpan.FromMilliseconds(1.0);

		// Token: 0x040007F3 RID: 2035
		private readonly IEnumerator<PlaybackEvent> _eventsEnumerator;

		// Token: 0x040007F4 RID: 2036
		private readonly TimeSpan _duration;

		// Token: 0x040007F5 RID: 2037
		private readonly long _durationInTicks;

		// Token: 0x040007F6 RID: 2038
		private readonly MidiClock _clock;

		// Token: 0x040007F7 RID: 2039
		private readonly HashSet<NotePlaybackEventMetadata> _activeNotesMetadata = new HashSet<NotePlaybackEventMetadata>();

		// Token: 0x040007F8 RID: 2040
		private readonly List<NotePlaybackEventMetadata> _notesMetadata;

		// Token: 0x040007F9 RID: 2041
		private bool _disposed;
	}
}
