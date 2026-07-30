using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Standards
{
	// Token: 0x02000067 RID: 103
	public static class GeneralMidiUtilities
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000A18E File Offset: 0x0000838E
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidiProgram program)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidiProgram>("program", program);
			return (SevenBitNumber)((byte)program);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A1A1 File Offset: 0x000083A1
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidiPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidiPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public static MidiEvent GetProgramEvent(this GeneralMidiProgram program, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidiProgram>("program", program);
			return new ProgramChangeEvent(program.AsSevenBitNumber())
			{
				Channel = channel
			};
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A1D3 File Offset: 0x000083D3
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidiPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidiPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A1F3 File Offset: 0x000083F3
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidiPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidiPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}
	}
}
