using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000FF RID: 255
	public sealed class PlaybackCurrentTimeWatcher : IDisposable, IClockDrivenObject
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000671 RID: 1649 RVA: 0x0001A3D0 File Offset: 0x000185D0
		// (remove) Token: 0x06000672 RID: 1650 RVA: 0x0001A408 File Offset: 0x00018608
		public event EventHandler<PlaybackCurrentTimeChangedEventArgs> CurrentTimeChanged;

		// Token: 0x06000673 RID: 1651 RVA: 0x0001A440 File Offset: 0x00018640
		private PlaybackCurrentTimeWatcher(MidiClockSettings clockSettings = null)
		{
			this._clockSettings = clockSettings ?? new MidiClockSettings();
			this.PollingInterval = PlaybackCurrentTimeWatcher.DefaultPollingInterval;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001A48F File Offset: 0x0001868F
		public static PlaybackCurrentTimeWatcher Instance
		{
			get
			{
				return PlaybackCurrentTimeWatcher._lazyInstance.Value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001A49B File Offset: 0x0001869B
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001A4A3 File Offset: 0x000186A3
		public TimeSpan PollingInterval
		{
			get
			{
				return this._pollingInterval;
			}
			set
			{
				this._pollingInterval = value;
				this.RecreateClock();
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001A4B4 File Offset: 0x000186B4
		public IEnumerable<Playback> Playbacks
		{
			get
			{
				object playbacksLock = this._playbacksLock;
				IEnumerable<Playback> keys;
				lock (playbacksLock)
				{
					keys = this._playbacks.Keys;
				}
				return keys;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001A4FC File Offset: 0x000186FC
		public bool IsWatching
		{
			get
			{
				MidiClock clock = this._clock;
				return clock != null && clock.IsRunning;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001A50F File Offset: 0x0001870F
		public void Start()
		{
			this.EnsureIsNotDisposed();
			this._clock.Start();
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001A522 File Offset: 0x00018722
		public void Stop()
		{
			this.EnsureIsNotDisposed();
			this._clock.Stop();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001A538 File Offset: 0x00018738
		public void AddPlayback(Playback playback, TimeSpanType timeType)
		{
			ThrowIfArgument.IsNull("playback", playback);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("timeType", timeType);
			this.EnsureIsNotDisposed();
			object playbacksLock = this._playbacksLock;
			lock (playbacksLock)
			{
				this._playbacks[playback] = timeType;
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001A59C File Offset: 0x0001879C
		public void RemovePlayback(Playback playback)
		{
			ThrowIfArgument.IsNull("playback", playback);
			this.EnsureIsNotDisposed();
			object playbacksLock = this._playbacksLock;
			lock (playbacksLock)
			{
				this._playbacks.Remove(playback);
				if (!this._playbacks.Any<KeyValuePair<Playback, TimeSpanType>>())
				{
					this.RecreateClock();
				}
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001A608 File Offset: 0x00018808
		public void RemoveAllPlaybacks()
		{
			this.EnsureIsNotDisposed();
			object playbacksLock = this._playbacksLock;
			lock (playbacksLock)
			{
				this._playbacks.Clear();
			}
			this.RecreateClock();
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001A65C File Offset: 0x0001885C
		private void OnTick(object sender, EventArgs e)
		{
			if (this._disposed || !this.IsWatching)
			{
				return;
			}
			List<PlaybackCurrentTime> list = new List<PlaybackCurrentTime>();
			object playbacksLock = this._playbacksLock;
			lock (playbacksLock)
			{
				foreach (KeyValuePair<Playback, TimeSpanType> keyValuePair in this._playbacks)
				{
					ITimeSpan currentTime = keyValuePair.Key.GetCurrentTime(keyValuePair.Value);
					list.Add(new PlaybackCurrentTime(keyValuePair.Key, currentTime));
				}
			}
			if (list.Any<PlaybackCurrentTime>())
			{
				this.OnCurrentTimeChanged(list);
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001A720 File Offset: 0x00018920
		private void OnCurrentTimeChanged(IEnumerable<PlaybackCurrentTime> times)
		{
			EventHandler<PlaybackCurrentTimeChangedEventArgs> currentTimeChanged = this.CurrentTimeChanged;
			if (currentTimeChanged == null)
			{
				return;
			}
			currentTimeChanged(this, new PlaybackCurrentTimeChangedEventArgs(times));
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001A739 File Offset: 0x00018939
		private void EnsureIsNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("Playback current time watcher is disposed.");
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001A74E File Offset: 0x0001894E
		private void DisposeClock()
		{
			if (this._clock == null)
			{
				return;
			}
			this._clock.Stop();
			this._clock.Ticked -= this.OnTick;
			this._clock.Dispose();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001A786 File Offset: 0x00018986
		private void CreateClock(TimeSpan pollingInterval)
		{
			this._clock = new MidiClock(true, this._clockSettings.CreateTickGeneratorCallback(pollingInterval));
			this._clock.Ticked += this.OnTick;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001A7BC File Offset: 0x000189BC
		private void RecreateClock()
		{
			bool isWatching = this.IsWatching;
			this.DisposeClock();
			this.CreateClock(this.PollingInterval);
			if (isWatching)
			{
				this.Start();
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001A7DE File Offset: 0x000189DE
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

		// Token: 0x06000685 RID: 1669 RVA: 0x0001A7F6 File Offset: 0x000189F6
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001A7FF File Offset: 0x000189FF
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this.DisposeClock();
			}
			this._disposed = true;
		}

		// Token: 0x040007E1 RID: 2017
		private static readonly TimeSpan DefaultPollingInterval = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x040007E3 RID: 2019
		private static readonly Lazy<PlaybackCurrentTimeWatcher> _lazyInstance = new Lazy<PlaybackCurrentTimeWatcher>(() => new PlaybackCurrentTimeWatcher(null));

		// Token: 0x040007E4 RID: 2020
		private readonly Dictionary<Playback, TimeSpanType> _playbacks = new Dictionary<Playback, TimeSpanType>();

		// Token: 0x040007E5 RID: 2021
		private readonly object _playbacksLock = new object();

		// Token: 0x040007E6 RID: 2022
		private readonly MidiClockSettings _clockSettings;

		// Token: 0x040007E7 RID: 2023
		private MidiClock _clock;

		// Token: 0x040007E8 RID: 2024
		private TimeSpan _pollingInterval = PlaybackCurrentTimeWatcher.DefaultPollingInterval;

		// Token: 0x040007E9 RID: 2025
		private bool _disposed;
	}
}
