using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x0200007A RID: 122
	internal static class ChordProgressionParser
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000D19C File Offset: 0x0000B39C
		internal static ParsingResult TryParse(string input, Scale scale, out ChordProgression chordProgression)
		{
			chordProgression = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match[] array = ParsingUtilities.Matches(input, ChordProgressionParser.Patterns, false);
			if (array == null)
			{
				return ParsingResult.NotMatched;
			}
			List<Chord> list = new List<Chord>();
			foreach (Match match in array)
			{
				Group group = match.Groups["sd"];
				string text = group.Value.ToLower();
				if (!string.IsNullOrWhiteSpace(text))
				{
					int num = ChordProgressionParser.RomanToInteger(text);
					NoteName step = scale.GetStep(num - 1);
					string value = match.Value;
					int index = match.Index;
					int index2 = group.Index;
					Chord chord;
					ParsingResult parsingResult = ChordParser.TryParse(value.Substring(0, index2 - index) + step + value.Substring(index2 - index + group.Length), out chord);
					if (parsingResult.Status != ParsingStatus.Parsed)
					{
						return parsingResult;
					}
					list.Add(chord);
				}
			}
			chordProgression = new ChordProgression(list);
			return ParsingResult.Parsed;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		private static int RomanToInteger(string roman)
		{
			int num = 0;
			for (int i = 0; i < roman.Length; i++)
			{
				if (i + 1 < roman.Length && ChordProgressionParser.RomanMap[roman[i]] < ChordProgressionParser.RomanMap[roman[i + 1]])
				{
					num -= ChordProgressionParser.RomanMap[roman[i]];
				}
				else
				{
					num += ChordProgressionParser.RomanMap[roman[i]];
				}
			}
			return num;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D320 File Offset: 0x0000B520
		// Note: this type is marked as 'beforefieldinit'.
		static ChordProgressionParser()
		{
			Dictionary<char, int> dictionary = new Dictionary<char, int>();
			dictionary['i'] = 1;
			dictionary['v'] = 5;
			dictionary['x'] = 10;
			dictionary['l'] = 50;
			dictionary['c'] = 100;
			dictionary['d'] = 500;
			dictionary['m'] = 1000;
			ChordProgressionParser.RomanMap = dictionary;
		}

		// Token: 0x0400050B RID: 1291
		private const string ScaleDegreeGroupName = "sd";

		// Token: 0x0400050C RID: 1292
		private const string ChordCharacteristicsGroupName = "cc";

		// Token: 0x0400050D RID: 1293
		private static readonly string ScaleDegreeGroup = "(?<sd>(?i:M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})))";

		// Token: 0x0400050E RID: 1294
		private static readonly string ChordCharacteristicsGroup = "(?<cc>" + ChordParser.ChordCharacteristicsGroup + ")";

		// Token: 0x0400050F RID: 1295
		private static readonly string[] Patterns = new string[] { ChordProgressionParser.ScaleDegreeGroup + "\\s*" + ChordProgressionParser.ChordCharacteristicsGroup };

		// Token: 0x04000510 RID: 1296
		private static Dictionary<char, int> RomanMap;
	}
}
