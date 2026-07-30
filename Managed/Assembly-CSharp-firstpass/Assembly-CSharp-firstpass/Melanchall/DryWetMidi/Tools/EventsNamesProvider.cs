using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200001E RID: 30
	internal static class EventsNamesProvider
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00005E5D File Offset: 0x0000405D
		public static string[] Get(MidiFileCsvLayout layout)
		{
			return EventsNamesProvider.EventsNames[layout];
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005E6C File Offset: 0x0000406C
		private static string[] GetEventsNames(Type eventNamesClassType)
		{
			return (from fi in eventNamesClassType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
				where fi.IsLiteral && !fi.IsInitOnly
				select fi.GetValue(null).ToString()).ToArray<string>();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005ECE File Offset: 0x000040CE
		// Note: this type is marked as 'beforefieldinit'.
		static EventsNamesProvider()
		{
			Dictionary<MidiFileCsvLayout, string[]> dictionary = new Dictionary<MidiFileCsvLayout, string[]>();
			dictionary[MidiFileCsvLayout.DryWetMidi] = EventsNamesProvider.GetEventsNames(typeof(DryWetMidiRecordTypes.Events));
			dictionary[MidiFileCsvLayout.MidiCsv] = EventsNamesProvider.GetEventsNames(typeof(MidiCsvRecordTypes.Events));
			EventsNamesProvider.EventsNames = dictionary;
		}

		// Token: 0x0400008F RID: 143
		private static readonly Dictionary<MidiFileCsvLayout, string[]> EventsNames;
	}
}
