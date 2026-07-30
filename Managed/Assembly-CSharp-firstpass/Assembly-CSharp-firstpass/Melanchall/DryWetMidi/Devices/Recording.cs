using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x0200010D RID: 269
	public sealed class Recording : IDisposable
	{
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600071F RID: 1823 RVA: 0x0001C458 File Offset: 0x0001A658
		// (remove) Token: 0x06000720 RID: 1824 RVA: 0x0001C490 File Offset: 0x0001A690
		public event EventHandler Started;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000721 RID: 1825 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
		// (remove) Token: 0x06000722 RID: 1826 RVA: 0x0001C500 File Offset: 0x0001A700
		public event EventHandler Stopped;

		// Token: 0x06000723 RID: 1827 RVA: 0x0001C538 File Offset: 0x0001A738
		public Recording(TempoMap tempoMap, IInputDevice inputDevice)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("inputDevice", inputDevice);
			this.TempoMap = tempoMap;
			this.InputDevice = inputDevice;
			this.InputDevice.EventReceived += this.OnEventReceived;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001C59C File Offset: 0x0001A79C
		public TempoMap TempoMap { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		public IInputDevice InputDevice { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001C5AC File Offset: 0x0001A7AC
		public bool IsRunning
		{
			get
			{
				return this._stopwatch.IsRunning;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		public ITimeSpan GetDuration(TimeSpanType durationType)
		{
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("durationType", durationType);
			RecordingEvent recordingEvent = this._events.LastOrDefault<RecordingEvent>();
			TimeSpan? timeSpan = ((recordingEvent != null) ? new TimeSpan?(recordingEvent.Time) : null);
			return TimeConverter.ConvertTo(((timeSpan != null) ? timeSpan.GetValueOrDefault() : null) ?? new MetricTimeSpan(), durationType, this.TempoMap);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001C628 File Offset: 0x0001A828
		public TTimeSpan GetDuration<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			RecordingEvent recordingEvent = this._events.LastOrDefault<RecordingEvent>();
			TimeSpan? timeSpan = ((recordingEvent != null) ? new TimeSpan?(recordingEvent.Time) : null);
			return TimeConverter.ConvertTo<TTimeSpan>(((timeSpan != null) ? timeSpan.GetValueOrDefault() : null) ?? new MetricTimeSpan(), this.TempoMap);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001C686 File Offset: 0x0001A886
		public IReadOnlyList<TimedEvent> GetEvents()
		{
			return this._events.Select((RecordingEvent e) => new TimedEvent(e.Event, TimeConverter.ConvertFrom(e.Time, this.TempoMap))).ToList<TimedEvent>().AsReadOnly();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001C6A9 File Offset: 0x0001A8A9
		public void Start()
		{
			if (this.IsRunning)
			{
				return;
			}
			if (!this.InputDevice.IsListeningForEvents)
			{
				throw new InvalidOperationException("Input device is not listening for MIDI events. Call StartEventsListening prior to start recording.");
			}
			this._stopwatch.Start();
			this.OnStarted();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001C6DD File Offset: 0x0001A8DD
		public void Stop()
		{
			if (!this.IsRunning)
			{
				return;
			}
			this._stopwatch.Stop();
			this.OnStopped();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001C6F9 File Offset: 0x0001A8F9
		private void OnStarted()
		{
			EventHandler started = this.Started;
			if (started == null)
			{
				return;
			}
			started(this, EventArgs.Empty);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001C711 File Offset: 0x0001A911
		private void OnStopped()
		{
			EventHandler stopped = this.Stopped;
			if (stopped == null)
			{
				return;
			}
			stopped(this, EventArgs.Empty);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001C729 File Offset: 0x0001A929
		private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
		{
			if (!this.IsRunning)
			{
				return;
			}
			this._events.Add(new RecordingEvent(e.Event, this._stopwatch.Elapsed));
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001C755 File Offset: 0x0001A955
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001C75E File Offset: 0x0001A95E
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.Stop();
				this.InputDevice.EventReceived -= this.OnEventReceived;
			}
			this._disposed = true;
		}

		// Token: 0x0400081A RID: 2074
		private readonly List<RecordingEvent> _events = new List<RecordingEvent>();

		// Token: 0x0400081B RID: 2075
		private readonly Stopwatch _stopwatch = new Stopwatch();

		// Token: 0x0400081C RID: 2076
		private bool _disposed;
	}
}
