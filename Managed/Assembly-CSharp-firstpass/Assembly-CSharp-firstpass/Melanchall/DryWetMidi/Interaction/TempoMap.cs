using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A9 RID: 169
	public sealed class TempoMap
	{
		// Token: 0x060003AE RID: 942 RVA: 0x00012608 File Offset: 0x00010808
		internal TempoMap(TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			this.TimeDivision = timeDivision;
			this.Tempo = new ValueLine<Tempo>(Melanchall.DryWetMidi.Interaction.Tempo.Default);
			this.TimeSignature = new ValueLine<TimeSignature>(Melanchall.DryWetMidi.Interaction.TimeSignature.Default);
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001265F File Offset: 0x0001085F
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x00012667 File Offset: 0x00010867
		public TimeDivision TimeDivision { get; internal set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00012670 File Offset: 0x00010870
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00012678 File Offset: 0x00010878
		public ValueLine<TimeSignature> TimeSignature
		{
			get
			{
				return this._timeSignature;
			}
			private set
			{
				if (this._timeSignature != null)
				{
					this._timeSignature.ValuesChanged -= this.OnTimeSignatureChanged;
				}
				this._timeSignature = value;
				this._timeSignature.ValuesChanged += this.OnTimeSignatureChanged;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000126B7 File Offset: 0x000108B7
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000126BF File Offset: 0x000108BF
		public ValueLine<Tempo> Tempo
		{
			get
			{
				return this._tempo;
			}
			private set
			{
				if (this._tempo != null)
				{
					this._tempo.ValuesChanged -= this.OnTempoChanged;
				}
				this._tempo = value;
				this._tempo.ValuesChanged += this.OnTempoChanged;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000126FE File Offset: 0x000108FE
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00012706 File Offset: 0x00010906
		internal bool IsTempoMapReady
		{
			get
			{
				return this._isTempoMapReady;
			}
			set
			{
				if (this._isTempoMapReady == value)
				{
					return;
				}
				this._isTempoMapReady = value;
				if (this._isTempoMapReady)
				{
					this.InvalidateCaches(TempoMapLine.Tempo);
					this.InvalidateCaches(TempoMapLine.TimeSignature);
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001272F File Offset: 0x0001092F
		public TempoMap Clone()
		{
			TempoMap tempoMap = new TempoMap(this.TimeDivision.Clone());
			tempoMap.Tempo.ReplaceValues(this.Tempo);
			tempoMap.TimeSignature.ReplaceValues(this.TimeSignature);
			return tempoMap;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00012763 File Offset: 0x00010963
		public static TempoMap Create(Tempo tempo, TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("tempo", tempo);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap tempoMap = TempoMap.Default.Clone();
			TempoMap.SetGlobalTempo(tempoMap, tempo);
			TempoMap.SetGlobalTimeSignature(tempoMap, timeSignature);
			return tempoMap;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012793 File Offset: 0x00010993
		public static TempoMap Create(Tempo tempo)
		{
			ThrowIfArgument.IsNull("tempo", tempo);
			TempoMap tempoMap = TempoMap.Default.Clone();
			TempoMap.SetGlobalTempo(tempoMap, tempo);
			return tempoMap;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000127B1 File Offset: 0x000109B1
		public static TempoMap Create(TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap tempoMap = TempoMap.Default.Clone();
			TempoMap.SetGlobalTimeSignature(tempoMap, timeSignature);
			return tempoMap;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000127CF File Offset: 0x000109CF
		public static TempoMap Create(TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			return new TempoMap(timeDivision);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000127E2 File Offset: 0x000109E2
		public static TempoMap Create(TimeDivision timeDivision, Tempo tempo)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			ThrowIfArgument.IsNull("tempo", tempo);
			TempoMap tempoMap = new TempoMap(timeDivision);
			TempoMap.SetGlobalTempo(tempoMap, tempo);
			return tempoMap;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00012807 File Offset: 0x00010A07
		public static TempoMap Create(TimeDivision timeDivision, TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap tempoMap = new TempoMap(timeDivision);
			TempoMap.SetGlobalTimeSignature(tempoMap, timeSignature);
			return tempoMap;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001282C File Offset: 0x00010A2C
		public static TempoMap Create(TimeDivision timeDivision, Tempo tempo, TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			ThrowIfArgument.IsNull("tempo", tempo);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap tempoMap = new TempoMap(timeDivision);
			TempoMap.SetGlobalTempo(tempoMap, tempo);
			TempoMap.SetGlobalTimeSignature(tempoMap, timeSignature);
			return tempoMap;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00012863 File Offset: 0x00010A63
		internal TempoMap Flip(long centerTime)
		{
			return new TempoMap(this.TimeDivision)
			{
				Tempo = this.Tempo.Reverse(centerTime),
				TimeSignature = this.TimeSignature.Reverse(centerTime)
			};
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00012894 File Offset: 0x00010A94
		internal TCache GetValuesCache<TCache>() where TCache : ITempoMapValuesCache, new()
		{
			TCache tcache = this._valuesCaches.OfType<TCache>().FirstOrDefault<TCache>();
			if (tcache == null)
			{
				this._valuesCaches.Add(tcache = new TCache());
				tcache.Invalidate(this);
			}
			return tcache;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000128E0 File Offset: 0x00010AE0
		private static void SetGlobalTempo(TempoMap tempoMap, Tempo tempo)
		{
			tempoMap.Tempo.SetValue(0L, tempo);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000128F0 File Offset: 0x00010AF0
		private static void SetGlobalTimeSignature(TempoMap tempoMap, TimeSignature timeSignature)
		{
			tempoMap.TimeSignature.SetValue(0L, timeSignature);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00012900 File Offset: 0x00010B00
		private void InvalidateCaches(TempoMapLine tempoMapLine)
		{
			if (!this.IsTempoMapReady)
			{
				return;
			}
			IEnumerable<ITempoMapValuesCache> valuesCaches = this._valuesCaches;
			Func<ITempoMapValuesCache, bool> <>9__0;
			Func<ITempoMapValuesCache, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = delegate(ITempoMapValuesCache c)
				{
					IEnumerable<TempoMapLine> invalidateOnLines = c.InvalidateOnLines;
					return invalidateOnLines != null && invalidateOnLines.Contains(tempoMapLine);
				});
			}
			foreach (ITempoMapValuesCache tempoMapValuesCache in valuesCaches.Where(func))
			{
				tempoMapValuesCache.Invalidate(this);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00012988 File Offset: 0x00010B88
		private void OnTimeSignatureChanged(object sender, EventArgs args)
		{
			this.InvalidateCaches(TempoMapLine.TimeSignature);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00012991 File Offset: 0x00010B91
		private void OnTempoChanged(object sender, EventArgs args)
		{
			this.InvalidateCaches(TempoMapLine.Tempo);
		}

		// Token: 0x04000693 RID: 1683
		public static readonly TempoMap Default = new TempoMap(new TicksPerQuarterNoteTimeDivision());

		// Token: 0x04000694 RID: 1684
		private ValueLine<TimeSignature> _timeSignature;

		// Token: 0x04000695 RID: 1685
		private ValueLine<Tempo> _tempo;

		// Token: 0x04000696 RID: 1686
		private readonly List<ITempoMapValuesCache> _valuesCaches = new List<ITempoMapValuesCache>();

		// Token: 0x04000697 RID: 1687
		private bool _isTempoMapReady = true;
	}
}
