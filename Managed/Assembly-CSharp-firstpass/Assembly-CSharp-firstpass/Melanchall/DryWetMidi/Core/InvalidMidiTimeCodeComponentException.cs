using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000171 RID: 369
	public sealed class InvalidMidiTimeCodeComponentException : MidiException
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x000206B2 File Offset: 0x0001E8B2
		internal InvalidMidiTimeCodeComponentException(byte midiTimeCodeComponent)
			: base(string.Format("Invalid MIDI Time Code component ({0}).", midiTimeCodeComponent))
		{
			this.MidiTimeCodeComponent = midiTimeCodeComponent;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000206D1 File Offset: 0x0001E8D1
		public byte MidiTimeCodeComponent { get; }
	}
}
