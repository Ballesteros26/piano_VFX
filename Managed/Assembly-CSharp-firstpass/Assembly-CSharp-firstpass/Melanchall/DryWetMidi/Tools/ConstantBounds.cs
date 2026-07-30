using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000057 RID: 87
	public sealed class ConstantBounds : IBounds
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00009B73 File Offset: 0x00007D73
		public ConstantBounds(ITimeSpan size)
			: this(size, size)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009B7D File Offset: 0x00007D7D
		public ConstantBounds(ITimeSpan leftSize, ITimeSpan rightSize)
		{
			ThrowIfArgument.IsNull("leftSize", leftSize);
			ThrowIfArgument.IsNull("rightSize", rightSize);
			this.LeftSize = leftSize;
			this.RightSize = rightSize;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00009BA9 File Offset: 0x00007DA9
		public ITimeSpan LeftSize { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009BB1 File Offset: 0x00007DB1
		public ITimeSpan RightSize { get; }

		// Token: 0x060001E3 RID: 483 RVA: 0x00009BBC File Offset: 0x00007DBC
		private static long CalculateBoundaryTime(long time, ITimeSpan size, MathOperation operation, TempoMap tempoMap)
		{
			ITimeSpan timeSpan = (MidiTimeSpan)time;
			if (operation != MathOperation.Add)
			{
				if (operation == MathOperation.Subtract)
				{
					ITimeSpan timeSpan2;
					if (TimeConverter.ConvertFrom(size, tempoMap) <= time)
					{
						timeSpan2 = timeSpan.Subtract(size, TimeSpanMode.TimeLength);
					}
					else
					{
						ITimeSpan timeSpan3 = (MidiTimeSpan)0L;
						timeSpan2 = timeSpan3;
					}
					timeSpan = timeSpan2;
				}
			}
			else
			{
				timeSpan = timeSpan.Add(size, TimeSpanMode.TimeLength);
			}
			return TimeConverter.ConvertFrom(timeSpan, tempoMap);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009C09 File Offset: 0x00007E09
		public Tuple<long, long> GetBounds(long time, TempoMap tempoMap)
		{
			return Tuple.Create<long, long>(ConstantBounds.CalculateBoundaryTime(time, this.LeftSize, MathOperation.Subtract, tempoMap), ConstantBounds.CalculateBoundaryTime(time, this.RightSize, MathOperation.Add, tempoMap));
		}
	}
}
