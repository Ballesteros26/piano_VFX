using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000126 RID: 294
	internal static class MidiFileEquality
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2, MidiFileEqualityCheckSettings settings, out string message)
		{
			message = null;
			if (midiFile1 == midiFile2)
			{
				return true;
			}
			if (midiFile1 == null || midiFile2 == null)
			{
				message = "One of files is null.";
				return false;
			}
			if (settings.CompareOriginalFormat)
			{
				ushort? originalFormat = midiFile1._originalFormat;
				ushort? originalFormat2 = midiFile2._originalFormat;
				ushort? num = originalFormat;
				int? num2 = ((num != null) ? new int?((int)num.GetValueOrDefault()) : null);
				num = originalFormat2;
				int? num3 = ((num != null) ? new int?((int)num.GetValueOrDefault()) : null);
				if (!((num2.GetValueOrDefault() == num3.GetValueOrDefault()) & (num2 != null == (num3 != null))))
				{
					message = string.Format("Original formats are different ({0} vs {1}).", originalFormat, originalFormat2);
					return false;
				}
			}
			ChunksCollection chunks = midiFile1.Chunks;
			ChunksCollection chunks2 = midiFile2.Chunks;
			if (chunks.Count != chunks2.Count)
			{
				message = string.Format("Counts of chunks are different ({0} vs {1}).", chunks.Count, chunks2.Count);
				return false;
			}
			for (int i = 0; i < chunks.Count; i++)
			{
				MidiChunk midiChunk = chunks[i];
				MidiChunk midiChunk2 = chunks2[i];
				string text;
				if (!MidiChunk.Equals(midiChunk, midiChunk2, settings.ChunkEqualityCheckSettings, out text))
				{
					message = string.Format("Chunks at position {0} are different. {1}", i, text);
					return false;
				}
			}
			return true;
		}
	}
}
