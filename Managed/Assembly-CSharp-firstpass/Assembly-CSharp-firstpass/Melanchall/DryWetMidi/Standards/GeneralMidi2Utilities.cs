using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Standards
{
	// Token: 0x0200006A RID: 106
	public static class GeneralMidi2Utilities
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000A214 File Offset: 0x00008414
		public static IEnumerable<MidiEvent> GetProgramEvents(this GeneralMidi2Program program, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2Program>("program", program);
			GeneralMidi2Utilities.GeneralMidi2ProgramData generalMidi2ProgramData = GeneralMidi2Utilities.ProgramsData[program];
			return new MidiEvent[]
			{
				ControlName.BankSelect.GetControlChangeEvent(generalMidi2ProgramData.BankMsb, channel),
				ControlName.LsbForBankSelect.GetControlChangeEvent(generalMidi2ProgramData.BankLsb, channel),
				generalMidi2ProgramData.GeneralMidiProgram.GetProgramEvent(channel)
			};
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A26E File Offset: 0x0000846E
		public static IEnumerable<MidiEvent> GetPercussionSetEvents(this GeneralMidi2PercussionSet percussionSet, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2PercussionSet>("percussionSet", percussionSet);
			return new MidiEvent[]
			{
				ControlName.BankSelect.GetControlChangeEvent((SevenBitNumber)120, channel),
				ControlName.LsbForBankSelect.GetControlChangeEvent((SevenBitNumber)0, channel),
				percussionSet.GetProgramEvent(channel)
			};
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A2AD File Offset: 0x000084AD
		public static MidiEvent GetProgramEvent(this GeneralMidi2PercussionSet percussionSet, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2PercussionSet>("percussionSet", percussionSet);
			return new ProgramChangeEvent(percussionSet.AsSevenBitNumber())
			{
				Channel = channel
			};
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A2CC File Offset: 0x000084CC
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2PercussionSet percussionSet)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2PercussionSet>("percussionSet", percussionSet);
			return (SevenBitNumber)((byte)percussionSet);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A2DF File Offset: 0x000084DF
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2AnalogPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2AnalogPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A2F2 File Offset: 0x000084F2
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2BrushPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2BrushPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000A305 File Offset: 0x00008505
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2ElectronicPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2ElectronicPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A318 File Offset: 0x00008518
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2JazzPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2JazzPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A32B File Offset: 0x0000852B
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2OrchestraPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2OrchestraPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A33E File Offset: 0x0000853E
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2PowerPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2PowerPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A351 File Offset: 0x00008551
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2RoomPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2RoomPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A364 File Offset: 0x00008564
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2SfxPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2SfxPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A377 File Offset: 0x00008577
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2StandardPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2StandardPercussion>("percussion", percussion);
			return (SevenBitNumber)((byte)percussion);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A38A File Offset: 0x0000858A
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2AnalogPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2AnalogPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A3AA File Offset: 0x000085AA
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2BrushPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2BrushPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A3CA File Offset: 0x000085CA
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2ElectronicPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2ElectronicPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A3EA File Offset: 0x000085EA
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2JazzPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2JazzPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A40A File Offset: 0x0000860A
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2OrchestraPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2OrchestraPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A42A File Offset: 0x0000862A
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2PowerPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2PowerPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A44A File Offset: 0x0000864A
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2RoomPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2RoomPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A46A File Offset: 0x0000866A
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2SfxPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2SfxPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A48A File Offset: 0x0000868A
		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2StandardPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2StandardPercussion>("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A4AA File Offset: 0x000086AA
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2AnalogPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2AnalogPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A4CA File Offset: 0x000086CA
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2BrushPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2BrushPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A4EA File Offset: 0x000086EA
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2ElectronicPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2ElectronicPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A50A File Offset: 0x0000870A
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2JazzPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2JazzPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A52A File Offset: 0x0000872A
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2OrchestraPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2OrchestraPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A54A File Offset: 0x0000874A
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2PowerPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2PowerPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A56A File Offset: 0x0000876A
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2RoomPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2RoomPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A58A File Offset: 0x0000878A
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2SfxPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2SfxPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A5AA File Offset: 0x000087AA
		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2StandardPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2StandardPercussion>("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A5CC File Offset: 0x000087CC
		private static IEnumerable<GeneralMidi2Utilities.GeneralMidi2ProgramData> GetProgramsData(GeneralMidiProgram generalMidiProgram, params GeneralMidi2Program[] programs)
		{
			return programs.Select((GeneralMidi2Program p, int i) => GeneralMidi2Utilities.GetProgramData(p, generalMidiProgram, 121, (byte)i));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A5F8 File Offset: 0x000087F8
		private static GeneralMidi2Utilities.GeneralMidi2ProgramData GetProgramData(GeneralMidi2Program generalMidi2Program, GeneralMidiProgram generalMidiProgram, byte bankMsb, byte bankLsb)
		{
			return new GeneralMidi2Utilities.GeneralMidi2ProgramData(generalMidi2Program, generalMidiProgram, (SevenBitNumber)bankMsb, (SevenBitNumber)bankLsb);
		}

		// Token: 0x040002B0 RID: 688
		private const byte MelodyChannelBankMsb = 121;

		// Token: 0x040002B1 RID: 689
		private const byte RhythmChannelBankMsb = 120;

		// Token: 0x040002B2 RID: 690
		private static readonly Dictionary<GeneralMidi2Program, GeneralMidi2Utilities.GeneralMidi2ProgramData> ProgramsData = new IEnumerable<GeneralMidi2Utilities.GeneralMidi2ProgramData>[]
		{
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.AcousticGrandPiano, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.AcousticGrandPiano,
				GeneralMidi2Program.AcousticGrandPianoWide,
				GeneralMidi2Program.AcousticGrandPianoDark
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.BrightAcousticPiano, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.BrightAcousticPiano,
				GeneralMidi2Program.BrightAcousticPianoWide
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ElectricGrandPiano, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ElectricGrandPiano,
				GeneralMidi2Program.ElectricGrandPianoWide
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.HonkyTonkPiano, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.HonkyTonkPiano,
				GeneralMidi2Program.HonkyTonkPianoWide
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ElectricPiano1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ElectricPiano1,
				GeneralMidi2Program.DetunedElectricPiano1,
				GeneralMidi2Program.ElectricPiano1VelocityMix,
				GeneralMidi2Program.SixtiesElectricPiano
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ElectricPiano2, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ElectricPiano2,
				GeneralMidi2Program.DetunedElectricPiano2,
				GeneralMidi2Program.ElectricPiano2VelocityMix,
				GeneralMidi2Program.EpLegend,
				GeneralMidi2Program.EpPhase
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Harpsichord, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Harpsichord,
				GeneralMidi2Program.HarpsichordOctaveMix,
				GeneralMidi2Program.HarpsichordWide,
				GeneralMidi2Program.HarpsichordWithKeyOff
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Clavi, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Clavi,
				GeneralMidi2Program.PulseClavi
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Celesta, new GeneralMidi2Program[] { GeneralMidi2Program.Celesta }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Glockenspiel, new GeneralMidi2Program[] { GeneralMidi2Program.Glockenspiel }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.MusicBox, new GeneralMidi2Program[] { GeneralMidi2Program.MusicBox }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Vibraphone, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Vibraphone,
				GeneralMidi2Program.VibraphoneWide
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Marimba, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Marimba,
				GeneralMidi2Program.MarimbaWide
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Xylophone, new GeneralMidi2Program[] { GeneralMidi2Program.Xylophone }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.TubularBells, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.TubularBells,
				GeneralMidi2Program.ChurchBell,
				GeneralMidi2Program.Carillon
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Dulcimer, new GeneralMidi2Program[] { GeneralMidi2Program.Dulcimer }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.DrawbarOrgan, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.DrawbarOrgan,
				GeneralMidi2Program.DetunedDrawbarOrgan,
				GeneralMidi2Program.ItalianSixtiesOrgan,
				GeneralMidi2Program.DrawbarOrgan2
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.PercussiveOrgan, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.PercussiveOrgan,
				GeneralMidi2Program.DetunedPercussiveOrgan,
				GeneralMidi2Program.PercussiveOrgan2
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.RockOrgan, new GeneralMidi2Program[] { GeneralMidi2Program.RockOrgan }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ChurchOrgan, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ChurchOrgan,
				GeneralMidi2Program.ChurchOrganOctaveMix,
				GeneralMidi2Program.DetunedChurchOrgan
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ReedOrgan, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ReedOrgan,
				GeneralMidi2Program.PuffOrgan
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Accordion, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Accordion,
				GeneralMidi2Program.Accordion2
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Harmonica, new GeneralMidi2Program[] { GeneralMidi2Program.Harmonica }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.TangoAccordion, new GeneralMidi2Program[] { GeneralMidi2Program.TangoAccordion }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.AcousticGuitar1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.AcousticGuitarNylon,
				GeneralMidi2Program.Ukulele,
				GeneralMidi2Program.AcousticGuitarNylonKeyOff,
				GeneralMidi2Program.AcousticGuitarNylon2
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.AcousticGuitar2, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.AcousticGuitarSteel,
				GeneralMidi2Program.TwelveStringsGuitar,
				GeneralMidi2Program.Mandolin,
				GeneralMidi2Program.SteelGuitarWithBodySound
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ElectricGuitar1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ElectricGuitarJazz,
				GeneralMidi2Program.ElectricGuitarPedalSteel
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ElectricGuitar2, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ElectricGuitarClean,
				GeneralMidi2Program.ElectricGuitarDetunedClean,
				GeneralMidi2Program.MidToneGuitar
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ElectricGuitar3, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ElectricGuitarMuted,
				GeneralMidi2Program.ElectricGuitarFunkyCutting,
				GeneralMidi2Program.ElectricGuitarMutedVeloSw,
				GeneralMidi2Program.JazzMan
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.OverdrivenGuitar, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.OverdrivenGuitar,
				GeneralMidi2Program.GuitarPinch
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.DistortionGuitar, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.DistortionGuitar,
				GeneralMidi2Program.DistortionGuitarWithFeedback,
				GeneralMidi2Program.DistortedRhythmGuitar
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.GuitarHarmonics, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.GuitarHarmonics,
				GeneralMidi2Program.GuitarFeedback
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.AcousticBass, new GeneralMidi2Program[] { GeneralMidi2Program.AcousticBass }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ElectricBass1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ElectricBassFinger,
				GeneralMidi2Program.FingerSlapBass
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ElectricBass2, new GeneralMidi2Program[] { GeneralMidi2Program.ElectricBassPick }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.FretlessBass, new GeneralMidi2Program[] { GeneralMidi2Program.FretlessBass }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SlapBass1, new GeneralMidi2Program[] { GeneralMidi2Program.SlapBass1 }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SlapBass2, new GeneralMidi2Program[] { GeneralMidi2Program.SlapBass2 }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SynthBass1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.SynthBass1,
				GeneralMidi2Program.SynthBassWarm,
				GeneralMidi2Program.SynthBass3Resonance,
				GeneralMidi2Program.ClaviBass,
				GeneralMidi2Program.Hammer
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SynthBass2, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.SynthBass2,
				GeneralMidi2Program.SynthBass4Attack,
				GeneralMidi2Program.SynthBassRubber,
				GeneralMidi2Program.AttackPulse
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Violin, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Violin,
				GeneralMidi2Program.ViolinSlowAttack
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Viola, new GeneralMidi2Program[] { GeneralMidi2Program.Viola }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Cello, new GeneralMidi2Program[] { GeneralMidi2Program.Cello }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Contrabass, new GeneralMidi2Program[] { GeneralMidi2Program.Contrabass }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.TremoloStrings, new GeneralMidi2Program[] { GeneralMidi2Program.TremoloStrings }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.PizzicatoStrings, new GeneralMidi2Program[] { GeneralMidi2Program.PizzicatoStrings }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.OrchestralHarp, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.OrchestralHarp,
				GeneralMidi2Program.YangChin
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Timpani, new GeneralMidi2Program[] { GeneralMidi2Program.Timpani }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.StringEnsemble1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.StringEnsembles1,
				GeneralMidi2Program.StringsAndBrass,
				GeneralMidi2Program.SixtiesStrings
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.StringEnsemble2, new GeneralMidi2Program[] { GeneralMidi2Program.StringEnsembles2 }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SynthStrings1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.SynthStrings1,
				GeneralMidi2Program.SynthStrings3
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SynthStrings2, new GeneralMidi2Program[] { GeneralMidi2Program.SynthStrings2 }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ChoirAahs, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.ChoirAahs,
				GeneralMidi2Program.ChoirAahs2
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.VoiceOohs, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.VoiceOohs,
				GeneralMidi2Program.Humming
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SynthVoice, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.SynthVoice,
				GeneralMidi2Program.AnalogVoice
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.OrchestraHit, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.OrchestraHit,
				GeneralMidi2Program.BassHitPlus,
				GeneralMidi2Program.SixthHit,
				GeneralMidi2Program.EuroHit
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Trumpet, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Trumpet,
				GeneralMidi2Program.DarkTrumpetSoft
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Trombone, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Trombone,
				GeneralMidi2Program.Trombone2,
				GeneralMidi2Program.BrightTrombone
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Tuba, new GeneralMidi2Program[] { GeneralMidi2Program.Tuba }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.MutedTrumpet, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.MutedTrumpet,
				GeneralMidi2Program.MutedTrumpet2
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.FrenchHorn, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.FrenchHorn,
				GeneralMidi2Program.FrenchHorn2Warm
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.BrassSection, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.BrassSection,
				GeneralMidi2Program.BrassSection2OctaveMix
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SynthBrass1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.SynthBrass1,
				GeneralMidi2Program.SynthBrass3,
				GeneralMidi2Program.AnalogSynthBrass1,
				GeneralMidi2Program.JumpBrass
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SynthBrass2, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.SynthBrass2,
				GeneralMidi2Program.SynthBrass4,
				GeneralMidi2Program.AnalogSynthBrass2
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SopranoSax, new GeneralMidi2Program[] { GeneralMidi2Program.SopranoSax }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.AltoSax, new GeneralMidi2Program[] { GeneralMidi2Program.AltoSax }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.TenorSax, new GeneralMidi2Program[] { GeneralMidi2Program.TenorSax }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.BaritoneSax, new GeneralMidi2Program[] { GeneralMidi2Program.BaritoneSax }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Oboe, new GeneralMidi2Program[] { GeneralMidi2Program.Oboe }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.EnglishHorn, new GeneralMidi2Program[] { GeneralMidi2Program.EnglishHorn }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Bassoon, new GeneralMidi2Program[] { GeneralMidi2Program.Bassoon }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Clarinet, new GeneralMidi2Program[] { GeneralMidi2Program.Clarinet }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Piccolo, new GeneralMidi2Program[] { GeneralMidi2Program.Piccolo }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Flute, new GeneralMidi2Program[] { GeneralMidi2Program.Flute }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Recorder, new GeneralMidi2Program[] { GeneralMidi2Program.Recorder }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.PanFlute, new GeneralMidi2Program[] { GeneralMidi2Program.PanFlute }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.BlownBottle, new GeneralMidi2Program[] { GeneralMidi2Program.BlownBottle }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Shakuhachi, new GeneralMidi2Program[] { GeneralMidi2Program.Shakuhachi }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Whistle, new GeneralMidi2Program[] { GeneralMidi2Program.Whistle }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Ocarina, new GeneralMidi2Program[] { GeneralMidi2Program.Ocarina }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Lead1, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Lead1Square,
				GeneralMidi2Program.Lead1ASquare2,
				GeneralMidi2Program.Lead1BSine
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Lead2, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Lead2Sawtooth,
				GeneralMidi2Program.Lead2ASawtooth2,
				GeneralMidi2Program.Lead2BSawPulse,
				GeneralMidi2Program.Lead2CDoubleSawtooth,
				GeneralMidi2Program.Lead2DSequencedAnalog
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Lead3, new GeneralMidi2Program[] { GeneralMidi2Program.Lead3Calliope }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Lead4, new GeneralMidi2Program[] { GeneralMidi2Program.Lead4Chiff }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Lead5, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Lead5Charang,
				GeneralMidi2Program.Lead5AWireLead
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Lead6, new GeneralMidi2Program[] { GeneralMidi2Program.Lead6Voice }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Lead7, new GeneralMidi2Program[] { GeneralMidi2Program.Lead7Fifths }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Lead8, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Lead8BassLead,
				GeneralMidi2Program.Lead8ASoftWrl
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Pad1, new GeneralMidi2Program[] { GeneralMidi2Program.Pad1NewAge }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Pad2, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Pad2Warm,
				GeneralMidi2Program.Pad2ASinePad
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Pad3, new GeneralMidi2Program[] { GeneralMidi2Program.Pad3Polysynth }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Pad4, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Pad4Choir,
				GeneralMidi2Program.Pad4AItopia
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Pad5, new GeneralMidi2Program[] { GeneralMidi2Program.Pad5Bowed }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Pad6, new GeneralMidi2Program[] { GeneralMidi2Program.Pad6Metallic }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Pad7, new GeneralMidi2Program[] { GeneralMidi2Program.Pad7Halo }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Pad8, new GeneralMidi2Program[] { GeneralMidi2Program.Pad8Sweep }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fx1, new GeneralMidi2Program[] { GeneralMidi2Program.Fx1Rain }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fx2, new GeneralMidi2Program[] { GeneralMidi2Program.Fx2Soundtrack }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fx3, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Fx3Crystal,
				GeneralMidi2Program.Fx3ASynthMallet
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fx4, new GeneralMidi2Program[] { GeneralMidi2Program.Fx4Atmosphere }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fx5, new GeneralMidi2Program[] { GeneralMidi2Program.Fx5Brightness }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fx6, new GeneralMidi2Program[] { GeneralMidi2Program.Fx6Goblins }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fx7, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Fx7Echoes,
				GeneralMidi2Program.Fx7AEchoBell,
				GeneralMidi2Program.Fx7BEchoPan
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fx8, new GeneralMidi2Program[] { GeneralMidi2Program.Fx8SciFi }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Sitar, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Sitar,
				GeneralMidi2Program.Sitar2Bend
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Banjo, new GeneralMidi2Program[] { GeneralMidi2Program.Banjo }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Shamisen, new GeneralMidi2Program[] { GeneralMidi2Program.Shamisen }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Koto, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Koto,
				GeneralMidi2Program.TaishoKoto
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Kalimba, new GeneralMidi2Program[] { GeneralMidi2Program.Kalimba }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.BagPipe, new GeneralMidi2Program[] { GeneralMidi2Program.BagPipe }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Fiddle, new GeneralMidi2Program[] { GeneralMidi2Program.Fiddle }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Shanai, new GeneralMidi2Program[] { GeneralMidi2Program.Shanai }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.TinkleBell, new GeneralMidi2Program[] { GeneralMidi2Program.TinkleBell }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Agogo, new GeneralMidi2Program[] { GeneralMidi2Program.Agogo }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SteelDrums, new GeneralMidi2Program[] { GeneralMidi2Program.SteelDrums }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Woodblock, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Woodblock,
				GeneralMidi2Program.Castanets
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.TaikoDrum, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.TaikoDrum,
				GeneralMidi2Program.ConcertBassDrum
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.MelodicTom, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.MelodicTom,
				GeneralMidi2Program.MelodicTom2Power
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.SynthDrum, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.SynthDrum,
				GeneralMidi2Program.RhythmBoxTom,
				GeneralMidi2Program.ElectricDrum
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.ReverseCymbal, new GeneralMidi2Program[] { GeneralMidi2Program.ReverseCymbal }),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.GuitarFretNoise, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.GuitarFretNoise,
				GeneralMidi2Program.GuitarCuttingNoise,
				GeneralMidi2Program.AcousticBassStringSlap
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.BreathNoise, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.BreathNoise,
				GeneralMidi2Program.FluteKeyClick
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Seashore, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Seashore,
				GeneralMidi2Program.Rain,
				GeneralMidi2Program.Thunder,
				GeneralMidi2Program.Wind,
				GeneralMidi2Program.Stream,
				GeneralMidi2Program.Bubble
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.BirdTweet, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.BirdTweet,
				GeneralMidi2Program.Dog,
				GeneralMidi2Program.HorseGallop,
				GeneralMidi2Program.BirdTweet2
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.TelephoneRing, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.TelephoneRing,
				GeneralMidi2Program.TelephoneRing2,
				GeneralMidi2Program.DoorCreaking,
				GeneralMidi2Program.Door,
				GeneralMidi2Program.Scratch,
				GeneralMidi2Program.WindChime
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Helicopter, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Helicopter,
				GeneralMidi2Program.CarEngine,
				GeneralMidi2Program.CarStop,
				GeneralMidi2Program.CarPass,
				GeneralMidi2Program.CarCrash,
				GeneralMidi2Program.Siren,
				GeneralMidi2Program.Train,
				GeneralMidi2Program.Jetplane,
				GeneralMidi2Program.Starship,
				GeneralMidi2Program.BurstNoise
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Applause, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Applause,
				GeneralMidi2Program.Laughing,
				GeneralMidi2Program.Screaming,
				GeneralMidi2Program.Punch,
				GeneralMidi2Program.HeartBeat,
				GeneralMidi2Program.Footsteps
			}),
			GeneralMidi2Utilities.GetProgramsData(GeneralMidiProgram.Gunshot, new GeneralMidi2Program[]
			{
				GeneralMidi2Program.Gunshot,
				GeneralMidi2Program.MachineGun,
				GeneralMidi2Program.Lasergun,
				GeneralMidi2Program.Explosion
			})
		}.SelectMany((IEnumerable<GeneralMidi2Utilities.GeneralMidi2ProgramData> d) => d).ToDictionary((GeneralMidi2Utilities.GeneralMidi2ProgramData d) => d.GeneralMidi2Program, (GeneralMidi2Utilities.GeneralMidi2ProgramData d) => d);

		// Token: 0x02000216 RID: 534
		private sealed class GeneralMidi2ProgramData
		{
			// Token: 0x06000D03 RID: 3331 RVA: 0x0002863B File Offset: 0x0002683B
			public GeneralMidi2ProgramData(GeneralMidi2Program generalMidi2Program, GeneralMidiProgram generalMidiProgram, SevenBitNumber bankMsb, SevenBitNumber bankLsb)
			{
				this.GeneralMidi2Program = generalMidi2Program;
				this.GeneralMidiProgram = generalMidiProgram;
				this.BankMsb = bankMsb;
				this.BankLsb = bankLsb;
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00028660 File Offset: 0x00026860
			public GeneralMidi2Program GeneralMidi2Program { get; }

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00028668 File Offset: 0x00026868
			public GeneralMidiProgram GeneralMidiProgram { get; }

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00028670 File Offset: 0x00026870
			public SevenBitNumber BankMsb { get; }

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00028678 File Offset: 0x00026878
			public SevenBitNumber BankLsb { get; }
		}
	}
}
