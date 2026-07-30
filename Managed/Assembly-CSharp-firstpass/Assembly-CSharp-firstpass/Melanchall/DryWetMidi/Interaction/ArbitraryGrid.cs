using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x02000093 RID: 147
	public sealed class ArbitraryGrid : IGrid
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00010E7C File Offset: 0x0000F07C
		public ArbitraryGrid(IEnumerable<ITimeSpan> times)
		{
			ThrowIfArgument.IsNull("times", times);
			ThrowIfArgument.ContainsNull<ITimeSpan>("times", times);
			this.Times = times;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00010EA1 File Offset: 0x0000F0A1
		public ArbitraryGrid(params ITimeSpan[] times)
			: this(times)
		{
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00010EAA File Offset: 0x0000F0AA
		public IEnumerable<ITimeSpan> Times { get; }

		// Token: 0x06000320 RID: 800 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		public IEnumerable<long> GetTimes(TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return this.Times.Select((ITimeSpan t) => TimeConverter.ConvertFrom(t, tempoMap));
		}
	}
}
