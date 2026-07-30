using System;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000159 RID: 345
	public sealed class EscapeSysExEvent : SysExEvent
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public EscapeSysExEvent()
			: base(MidiEventType.EscapeSysEx)
		{
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001FCB5 File Offset: 0x0001DEB5
		public EscapeSysExEvent(byte[] data)
			: this()
		{
			ThrowIfArgument.StartsWithInvalidValue<byte>("data", data, 247, string.Format("First data byte mustn't be {0} ({1:X2}) since it will be used automatically.", 247, 247));
			base.Data = data;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001FCF2 File Offset: 0x0001DEF2
		protected override MidiEvent CloneEvent()
		{
			byte[] data = base.Data;
			return new EscapeSysExEvent((data != null) ? data.ToArray<byte>() : null);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001FD0B File Offset: 0x0001DF0B
		public override string ToString()
		{
			return "Escape SysEx";
		}
	}
}
