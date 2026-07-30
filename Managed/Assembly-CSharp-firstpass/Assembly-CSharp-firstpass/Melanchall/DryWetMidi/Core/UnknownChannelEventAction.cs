using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200018F RID: 399
	public sealed class UnknownChannelEventAction
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x00021B51 File Offset: 0x0001FD51
		private UnknownChannelEventAction(UnknownChannelEventInstruction instruction, int dataBytesToSkipCount)
		{
			this.Instruction = instruction;
			this.DataBytesToSkipCount = dataBytesToSkipCount;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00021B67 File Offset: 0x0001FD67
		public UnknownChannelEventInstruction Instruction { get; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00021B6F File Offset: 0x0001FD6F
		public int DataBytesToSkipCount { get; }

		// Token: 0x060009CC RID: 2508 RVA: 0x00021B77 File Offset: 0x0001FD77
		public static UnknownChannelEventAction SkipData(int dataBytesToSkipCount)
		{
			ThrowIfArgument.IsNegative("dataBytesToSkipCount", dataBytesToSkipCount, "Count of data bytes to skip is negative.");
			return new UnknownChannelEventAction(UnknownChannelEventInstruction.SkipData, dataBytesToSkipCount);
		}

		// Token: 0x04000936 RID: 2358
		public static readonly UnknownChannelEventAction Abort = new UnknownChannelEventAction(UnknownChannelEventInstruction.Abort, 0);
	}
}
