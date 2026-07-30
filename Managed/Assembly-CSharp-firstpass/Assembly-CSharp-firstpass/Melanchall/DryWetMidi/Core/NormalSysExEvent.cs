using System;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200015A RID: 346
	public sealed class NormalSysExEvent : SysExEvent
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0001FD12 File Offset: 0x0001DF12
		public NormalSysExEvent()
			: base(MidiEventType.NormalSysEx)
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001FD1B File Offset: 0x0001DF1B
		public NormalSysExEvent(byte[] data)
			: this()
		{
			ThrowIfArgument.StartsWithInvalidValue<byte>("data", data, 240, string.Format("First data byte mustn't be {0} ({1:X2}) since it will be used automatically.", 240, 240));
			base.Data = data;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001FD58 File Offset: 0x0001DF58
		protected override MidiEvent CloneEvent()
		{
			byte[] data = base.Data;
			return new NormalSysExEvent((data != null) ? data.ToArray<byte>() : null);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001FD71 File Offset: 0x0001DF71
		public override string ToString()
		{
			return "Normal SysEx";
		}
	}
}
