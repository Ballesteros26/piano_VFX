using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000109 RID: 265
	public sealed class PlaybackSnapping
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		internal PlaybackSnapping(IEnumerable<PlaybackEvent> playbackEvents, TempoMap tempoMap)
		{
			this._playbackEvents = playbackEvents;
			this._tempoMap = tempoMap;
			PlaybackEvent playbackEvent = this._playbackEvents.LastOrDefault<PlaybackEvent>();
			this._maxTime = ((playbackEvent != null) ? playbackEvent.Time : TimeSpan.Zero);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001C03D File Offset: 0x0001A23D
		public IEnumerable<SnapPoint> SnapPoints
		{
			get
			{
				return this._snapPoints.AsReadOnly();
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001C04C File Offset: 0x0001A24C
		public SnapPoint<TData> AddSnapPoint<TData>(ITimeSpan time, TData data)
		{
			ThrowIfArgument.IsNull("time", time);
			SnapPoint<TData> snapPoint = new SnapPoint<TData>(TimeConverter.ConvertTo<MetricTimeSpan>(time, this._tempoMap), data);
			this._snapPoints.Add(snapPoint);
			return snapPoint;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001C089 File Offset: 0x0001A289
		public SnapPoint<Guid> AddSnapPoint(ITimeSpan time)
		{
			ThrowIfArgument.IsNull("time", time);
			return this.AddSnapPoint<Guid>(time, Guid.NewGuid());
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001C0A2 File Offset: 0x0001A2A2
		public void RemoveSnapPoint<TData>(SnapPoint<TData> snapPoint)
		{
			ThrowIfArgument.IsNull("snapPoint", snapPoint);
			this._snapPoints.Remove(snapPoint);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		public void RemoveSnapPointsByData<TData>(Predicate<TData> predicate)
		{
			ThrowIfArgument.IsNull("predicate", predicate);
			this._snapPoints.RemoveAll(delegate(SnapPoint p)
			{
				SnapPoint<TData> snapPoint = p as SnapPoint<TData>;
				return snapPoint != null && predicate(snapPoint.Data);
			});
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001C100 File Offset: 0x0001A300
		public SnapPointsGroup SnapToGrid(IGrid grid)
		{
			ThrowIfArgument.IsNull("grid", grid);
			SnapPointsGroup snapPointsGroup = new SnapPointsGroup();
			foreach (long num in grid.GetTimes(this._tempoMap))
			{
				TimeSpan timeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(num, this._tempoMap);
				if (timeSpan > this._maxTime)
				{
					break;
				}
				this._snapPoints.Add(new SnapPoint(timeSpan)
				{
					SnapPointsGroup = snapPointsGroup
				});
			}
			return snapPointsGroup;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001C198 File Offset: 0x0001A398
		public SnapPointsGroup SnapToNotesStarts()
		{
			SnapPointsGroup snapPointsGroup;
			if ((snapPointsGroup = this._noteStartSnapPointsGroup) == null)
			{
				snapPointsGroup = (this._noteStartSnapPointsGroup = this.SnapToNoteEvents(true));
			}
			return snapPointsGroup;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
		public SnapPointsGroup SnapToNotesEnds()
		{
			SnapPointsGroup snapPointsGroup;
			if ((snapPointsGroup = this._noteEndSnapPointsGroup) == null)
			{
				snapPointsGroup = (this._noteEndSnapPointsGroup = this.SnapToNoteEvents(false));
			}
			return snapPointsGroup;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		internal SnapPoint GetNextSnapPoint(TimeSpan time, SnapPointsGroup snapPointsGroup)
		{
			return this.GetActiveSnapPoints(snapPointsGroup).SkipWhile((SnapPoint p) => p.Time <= time).FirstOrDefault<SnapPoint>();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001C220 File Offset: 0x0001A420
		internal SnapPoint GetNextSnapPoint(TimeSpan time)
		{
			return this.GetActiveSnapPoints().SkipWhile((SnapPoint p) => p.Time <= time).FirstOrDefault<SnapPoint>();
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001C258 File Offset: 0x0001A458
		internal SnapPoint GetPreviousSnapPoint(TimeSpan time, SnapPointsGroup snapPointsGroup)
		{
			return this.GetActiveSnapPoints(snapPointsGroup).TakeWhile((SnapPoint p) => p.Time < time).LastOrDefault<SnapPoint>();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001C290 File Offset: 0x0001A490
		internal SnapPoint GetPreviousSnapPoint(TimeSpan time)
		{
			return this.GetActiveSnapPoints().TakeWhile((SnapPoint p) => p.Time < time).LastOrDefault<SnapPoint>();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		private SnapPointsGroup SnapToNoteEvents(bool snapToNoteOn)
		{
			List<ITimeSpan> list = new List<ITimeSpan>();
			foreach (PlaybackEvent playbackEvent in this._playbackEvents)
			{
				if (playbackEvent.Metadata.Note != null && playbackEvent.Event is NoteOnEvent == snapToNoteOn)
				{
					list.Add(playbackEvent.Time);
				}
			}
			return this.SnapToGrid(new ArbitraryGrid(list));
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001C350 File Offset: 0x0001A550
		private IEnumerable<SnapPoint> GetActiveSnapPoints()
		{
			return from p in this._snapPoints.Where(delegate(SnapPoint p)
				{
					if (p.IsEnabled)
					{
						SnapPointsGroup snapPointsGroup = p.SnapPointsGroup;
						return snapPointsGroup == null || snapPointsGroup.IsEnabled;
					}
					return false;
				})
				orderby p.Time
				select p;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		private IEnumerable<SnapPoint> GetActiveSnapPoints(SnapPointsGroup snapPointsGroup)
		{
			return from p in this.GetActiveSnapPoints()
				where p.SnapPointsGroup == snapPointsGroup
				select p;
		}

		// Token: 0x0400080D RID: 2061
		private readonly List<SnapPoint> _snapPoints = new List<SnapPoint>();

		// Token: 0x0400080E RID: 2062
		private readonly IEnumerable<PlaybackEvent> _playbackEvents;

		// Token: 0x0400080F RID: 2063
		private readonly TempoMap _tempoMap;

		// Token: 0x04000810 RID: 2064
		private readonly TimeSpan _maxTime;

		// Token: 0x04000811 RID: 2065
		private SnapPointsGroup _noteStartSnapPointsGroup;

		// Token: 0x04000812 RID: 2066
		private SnapPointsGroup _noteEndSnapPointsGroup;
	}
}
