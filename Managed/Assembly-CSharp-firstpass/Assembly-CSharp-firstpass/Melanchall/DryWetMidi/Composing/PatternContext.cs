using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B8 RID: 440
	internal sealed class PatternContext
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00023ABF File Offset: 0x00021CBF
		public PatternContext(TempoMap tempoMap, FourBitNumber channel)
		{
			this.TempoMap = tempoMap;
			this.Channel = channel;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00023AF6 File Offset: 0x00021CF6
		public TempoMap TempoMap { get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00023AFE File Offset: 0x00021CFE
		public FourBitNumber Channel { get; }

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00023B06 File Offset: 0x00021D06
		public void SaveTime(long time)
		{
			this._timeHistory.Push(time);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00023B14 File Offset: 0x00021D14
		public long? RestoreTime()
		{
			if (!this._timeHistory.Any<long>())
			{
				return null;
			}
			return new long?(this._timeHistory.Pop());
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00023B48 File Offset: 0x00021D48
		public void AnchorTime(object anchor, long time)
		{
			this.GetAnchorTimesList(anchor).Add(time);
			if (anchor != null)
			{
				this._anchorsList.Add(time);
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00023B66 File Offset: 0x00021D66
		public IReadOnlyList<long> GetAnchorTimes(object anchor)
		{
			return this.GetAnchorTimesList(anchor).AsReadOnly();
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00023B74 File Offset: 0x00021D74
		private List<long> GetAnchorTimesList(object anchor)
		{
			if (anchor == null)
			{
				return this._anchorsList;
			}
			List<long> list;
			if (!this._anchors.TryGetValue(anchor, out list))
			{
				this._anchors.Add(anchor, list = new List<long>());
			}
			return list;
		}

		// Token: 0x0400099D RID: 2461
		private readonly Stack<long> _timeHistory = new Stack<long>();

		// Token: 0x0400099E RID: 2462
		private readonly Dictionary<object, List<long>> _anchors = new Dictionary<object, List<long>>();

		// Token: 0x0400099F RID: 2463
		private readonly List<long> _anchorsList = new List<long>();
	}
}
