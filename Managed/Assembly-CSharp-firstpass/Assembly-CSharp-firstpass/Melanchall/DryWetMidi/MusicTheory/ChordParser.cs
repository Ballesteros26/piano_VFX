using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000075 RID: 117
	internal static class ChordParser
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000B6F8 File Offset: 0x000098F8
		internal static ParsingResult TryParse(string input, out Chord chord)
		{
			chord = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, ChordParser.Patterns, false);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			NoteName noteName;
			ParsingResult parsingResult = NoteNameParser.TryParse(match.Groups["rn"].Value, out noteName);
			if (parsingResult.Status != ParsingStatus.Parsed)
			{
				return parsingResult;
			}
			if (match.Groups["cn"].Success)
			{
				return ChordParser.TryParseChordName(match, noteName, out chord);
			}
			if (match.Groups["ci"].Success)
			{
				return ChordParser.TryParseChordIntervals(match, noteName, out chord);
			}
			return ParsingResult.Parsed;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B798 File Offset: 0x00009998
		internal static ParsingResult TryParseChordName(Match match, NoteName rootNoteName, out Chord chord)
		{
			chord = null;
			ChordParser.Quality? quality = null;
			if (match.Groups["q"].Success)
			{
				quality = new ChordParser.Quality?(ChordParser.GroupsQualities.FirstOrDefault((KeyValuePair<string, ChordParser.Quality> gq) => match.Groups[gq.Key].Success).Value);
			}
			int num = -1;
			IntervalQuality? intervalQuality = null;
			Group group = match.Groups["ext"];
			if (group.Success && !string.IsNullOrWhiteSpace(group.Value))
			{
				if (match.Groups["eq"].Success)
				{
					intervalQuality = new IntervalQuality?(ChordParser.GroupsExtensionQualities.FirstOrDefault((KeyValuePair<string, IntervalQuality> gq) => match.Groups[gq.Key].Success).Value);
				}
				if (!ParsingUtilities.ParseInt(match, "en", -1, out num) || num < 5)
				{
					return ParsingResult.Error("Extension number is out of range.");
				}
			}
			ChordParser.Quality? quality2 = quality;
			ChordParser.Quality quality3 = ChordParser.Quality.HalfDiminished;
			if (!((quality2.GetValueOrDefault() == quality3) & (quality2 != null)))
			{
				quality2 = quality;
				quality3 = ChordParser.Quality.Dominant;
				if (!((quality2.GetValueOrDefault() == quality3) & (quality2 != null)))
				{
					goto IL_0147;
				}
			}
			if (num >= 0 && num != 7)
			{
				return ParsingResult.Error("Half-diminished or dominant chord is not seventh one.");
			}
			if (num < 0)
			{
				num = 7;
			}
			IL_0147:
			List<NoteName> list = new List<NoteName>();
			List<int> list2 = new List<int>();
			if (num >= 0)
			{
				IDictionary<NoteName, int> extensionNotes = ChordParser.GetExtensionNotes(quality, rootNoteName, num, intervalQuality);
				list.AddRange(extensionNotes.Keys);
				list2.AddRange(extensionNotes.Values);
			}
			quality2 = quality;
			quality3 = ChordParser.Quality.HalfDiminished;
			if ((quality2.GetValueOrDefault() == quality3) & (quality2 != null))
			{
				quality = new ChordParser.Quality?(ChordParser.Quality.Diminished);
			}
			else
			{
				quality2 = quality;
				quality3 = ChordParser.Quality.Dominant;
				if ((quality2.GetValueOrDefault() == quality3) & (quality2 != null))
				{
					quality = new ChordParser.Quality?(ChordParser.Quality.Major);
				}
			}
			if (quality == null)
			{
				quality = new ChordParser.Quality?(ChordParser.Quality.Major);
			}
			List<NoteName> list3 = new List<NoteName>(Chord.GetByTriad(rootNoteName, ChordParser.ChordQualities[quality.Value], Array.Empty<Interval>()).NotesNames);
			list3.AddRange(list);
			list2.InsertRange(0, new int[] { 1, 3, 5 });
			if (num == 5)
			{
				list3.Clear();
				list3.AddRange(new NoteName[]
				{
					rootNoteName,
					list.First<NoteName>()
				});
				list2.Clear();
				list2.AddRange(new int[] { 1, 5 });
			}
			if (match.Groups["alt"].Success)
			{
				int num2;
				if (!ParsingUtilities.ParseInt(match, "altn", -1, out num2))
				{
					return ParsingResult.Error("Altered tone number is out of range.");
				}
				int num3 = 0;
				string value = match.Groups["alta"].Value;
				if (!(value == "#") && !(value == "+"))
				{
					if (value == "b" || value == "-")
					{
						num3 = -1;
					}
				}
				else
				{
					num3 = 1;
				}
				int maxExtensionNumber = list2.Max();
				if (maxExtensionNumber < num2)
				{
					IEnumerable<KeyValuePair<NoteName, int>> enumerable = from kv in ChordParser.GetExtensionNotes(quality, rootNoteName, num2, null)
						where kv.Value > maxExtensionNumber
						select kv;
					list3.AddRange(enumerable.Select((KeyValuePair<NoteName, int> kv) => kv.Key));
					list2.AddRange(enumerable.Select((KeyValuePair<NoteName, int> kv) => kv.Value));
				}
				int num4 = list2.IndexOf(num2);
				if (num4 >= 0)
				{
					list3[num4] = list3[num4].Transpose(Interval.FromHalfSteps(num3));
				}
			}
			if (match.Groups["sus"].Success)
			{
				int num5;
				if (!ParsingUtilities.ParseInt(match, "susn", -1, out num5) || (num5 != 2 && num5 != 4))
				{
					return ParsingResult.Error("Suspended chord is not sus2 or sus4.");
				}
				Interval interval = ((num5 == 2) ? Interval.Get(IntervalQuality.Major, 2) : Interval.Get(IntervalQuality.Perfect, 4));
				list3[1] = rootNoteName.Transpose(interval);
			}
			if (match.Groups["add"].Success)
			{
				int num6;
				if (!ParsingUtilities.ParseInt(match, "addn", -1, out num6))
				{
					return ParsingResult.Error("Added tone number is out of range.");
				}
				Interval interval2 = (Interval.IsPerfect(num6) ? Interval.Get(IntervalQuality.Perfect, num6) : Interval.Get(IntervalQuality.Major, num6));
				list3.Add(rootNoteName.Transpose(interval2));
			}
			Group group2 = match.Groups["bn"];
			if (group2.Success)
			{
				NoteName noteName;
				ParsingResult parsingResult = NoteNameParser.TryParse(group2.Value, out noteName);
				if (parsingResult.Status != ParsingStatus.Parsed)
				{
					return parsingResult;
				}
				list3.Insert(0, noteName);
			}
			chord = new Chord(list3);
			return ParsingResult.Parsed;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		private static ParsingResult TryParseChordIntervals(Match match, NoteName rootNoteName, out Chord chord)
		{
			chord = null;
			var array = match.Groups["i"].Captures.OfType<Capture>().Select(delegate(Capture c)
			{
				Interval interval;
				ParsingResult parsingResult = IntervalParser.TryParse(c.Value, out interval);
				return new
				{
					Interval = interval,
					ParsingResult = parsingResult
				};
			}).ToArray();
			var <>f__AnonymousType = array.FirstOrDefault(r => r.ParsingResult.Status > ParsingStatus.Parsed);
			if (<>f__AnonymousType != null)
			{
				return <>f__AnonymousType.ParsingResult;
			}
			Interval[] array2 = array.Select(r => r.Interval).ToArray<Interval>();
			chord = new Chord(rootNoteName, array2);
			return ParsingResult.Parsed;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000BD68 File Offset: 0x00009F68
		private static IDictionary<NoteName, int> GetExtensionNotes(ChordParser.Quality? quality, NoteName rootNoteName, int extensionIntervalNumber, IntervalQuality? extensionIntervalQuality)
		{
			Dictionary<NoteName, int> dictionary = new Dictionary<NoteName, int>();
			IntervalQuality[] array = Enumerable.Range(0, extensionIntervalNumber + 1).Select(delegate(int i)
			{
				if (i <= 2 || !Interval.IsPerfect(i))
				{
					return IntervalQuality.Major;
				}
				return IntervalQuality.Perfect;
			}).ToArray<IntervalQuality>();
			if (extensionIntervalNumber >= 7)
			{
				if (extensionIntervalQuality == null && quality == null)
				{
					array[7] = IntervalQuality.Minor;
				}
				else if (extensionIntervalQuality != null)
				{
					array[7] = extensionIntervalQuality.Value;
					array[extensionIntervalNumber] = extensionIntervalQuality.Value;
				}
				else
				{
					IntervalQuality[] array2 = array;
					int num = 7;
					ChordParser.Quality? quality2 = quality;
					ChordParser.Quality quality3 = ChordParser.Quality.HalfDiminished;
					IntervalQuality intervalQuality;
					if (!((quality2.GetValueOrDefault() == quality3) & (quality2 != null)))
					{
						quality2 = quality;
						quality3 = ChordParser.Quality.Dominant;
						if (!((quality2.GetValueOrDefault() == quality3) & (quality2 != null)))
						{
							quality2 = quality;
							quality3 = ChordParser.Quality.Augmented;
							if (!((quality2.GetValueOrDefault() == quality3) & (quality2 != null)))
							{
								intervalQuality = ChordParser.ChordToIntervalQualities[quality.Value];
								goto IL_00D5;
							}
						}
					}
					intervalQuality = IntervalQuality.Minor;
					IL_00D5:
					array2[num] = intervalQuality;
				}
				for (int j = 7; j <= extensionIntervalNumber; j += 2)
				{
					dictionary.Add(rootNoteName.Transpose(Interval.Get(array[j], j)), j);
				}
			}
			else
			{
				if (extensionIntervalQuality != null)
				{
					array[extensionIntervalNumber] = extensionIntervalQuality.Value;
				}
				dictionary.Add(rootNoteName.Transpose(Interval.Get(array[extensionIntervalNumber], extensionIntervalNumber)), extensionIntervalNumber);
			}
			return dictionary;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		// Note: this type is marked as 'beforefieldinit'.
		static ChordParser()
		{
			Dictionary<string, ChordParser.Quality> dictionary = new Dictionary<string, ChordParser.Quality>();
			dictionary["maj"] = ChordParser.Quality.Major;
			dictionary["min"] = ChordParser.Quality.Minor;
			dictionary["dim"] = ChordParser.Quality.Diminished;
			dictionary["aug"] = ChordParser.Quality.Augmented;
			dictionary["hdim"] = ChordParser.Quality.HalfDiminished;
			dictionary["dom"] = ChordParser.Quality.Dominant;
			ChordParser.GroupsQualities = dictionary;
			Dictionary<string, IntervalQuality> dictionary2 = new Dictionary<string, IntervalQuality>();
			dictionary2["emaj"] = IntervalQuality.Major;
			dictionary2["emin"] = IntervalQuality.Minor;
			dictionary2["edim"] = IntervalQuality.Diminished;
			dictionary2["eaug"] = IntervalQuality.Augmented;
			ChordParser.GroupsExtensionQualities = dictionary2;
			Dictionary<ChordParser.Quality, ChordQuality> dictionary3 = new Dictionary<ChordParser.Quality, ChordQuality>();
			dictionary3[ChordParser.Quality.Major] = ChordQuality.Major;
			dictionary3[ChordParser.Quality.Minor] = ChordQuality.Minor;
			dictionary3[ChordParser.Quality.Diminished] = ChordQuality.Diminished;
			dictionary3[ChordParser.Quality.Augmented] = ChordQuality.Augmented;
			ChordParser.ChordQualities = dictionary3;
			Dictionary<ChordParser.Quality, IntervalQuality> dictionary4 = new Dictionary<ChordParser.Quality, IntervalQuality>();
			dictionary4[ChordParser.Quality.Major] = IntervalQuality.Major;
			dictionary4[ChordParser.Quality.Minor] = IntervalQuality.Minor;
			dictionary4[ChordParser.Quality.Diminished] = IntervalQuality.Diminished;
			dictionary4[ChordParser.Quality.Augmented] = IntervalQuality.Augmented;
			ChordParser.ChordToIntervalQualities = dictionary4;
		}

		// Token: 0x040004D6 RID: 1238
		private const string RootNoteNameGroupName = "rn";

		// Token: 0x040004D7 RID: 1239
		private const string IntervalGroupName = "i";

		// Token: 0x040004D8 RID: 1240
		private const string ChordQualityGroupName = "q";

		// Token: 0x040004D9 RID: 1241
		private const string MajorQualityGroupName = "maj";

		// Token: 0x040004DA RID: 1242
		private const string MinorQualityGroupName = "min";

		// Token: 0x040004DB RID: 1243
		private const string DiminishedQualityGroupName = "dim";

		// Token: 0x040004DC RID: 1244
		private const string AugmentedQualityGroupName = "aug";

		// Token: 0x040004DD RID: 1245
		private const string HalfDiminishedQualityGroupName = "hdim";

		// Token: 0x040004DE RID: 1246
		private const string DominantQualityGroupName = "dom";

		// Token: 0x040004DF RID: 1247
		private const string BassNoteNameGroupName = "bn";

		// Token: 0x040004E0 RID: 1248
		private const string ChordIntervalsGroupName = "ci";

		// Token: 0x040004E1 RID: 1249
		private const string ChordNameGroupName = "cn";

		// Token: 0x040004E2 RID: 1250
		private const string ExtensionQualityGroupName = "eq";

		// Token: 0x040004E3 RID: 1251
		private const string ExtensionMajorQualityGroupName = "emaj";

		// Token: 0x040004E4 RID: 1252
		private const string ExtensionMinorQualityGroupName = "emin";

		// Token: 0x040004E5 RID: 1253
		private const string ExtensionDiminishedQualityGroupName = "edim";

		// Token: 0x040004E6 RID: 1254
		private const string ExtensionAugmentedQualityGroupName = "eaug";

		// Token: 0x040004E7 RID: 1255
		private const string ExtensionGroupName = "ext";

		// Token: 0x040004E8 RID: 1256
		private const string ExtensionNumberGroupName = "en";

		// Token: 0x040004E9 RID: 1257
		private const string SuspendedNumberGroupName = "susn";

		// Token: 0x040004EA RID: 1258
		private const string SuspendedGroupName = "sus";

		// Token: 0x040004EB RID: 1259
		private const string AddedToneNumberGroupName = "addn";

		// Token: 0x040004EC RID: 1260
		private const string AddedToneGroupName = "add";

		// Token: 0x040004ED RID: 1261
		private const string AlteredToneNumberGroupName = "altn";

		// Token: 0x040004EE RID: 1262
		private const string AlteredToneAccidentalGroupName = "alta";

		// Token: 0x040004EF RID: 1263
		private const string AlteredToneGroupName = "alt";

		// Token: 0x040004F0 RID: 1264
		private static readonly string IntervalGroup = "(?<i>(" + string.Join("|", IntervalParser.GetPatterns()) + ")\\s*)+";

		// Token: 0x040004F1 RID: 1265
		private static readonly string RootNoteNameGroup = "(?<rn>" + string.Join("|", NoteNameParser.GetPatterns()) + ")";

		// Token: 0x040004F2 RID: 1266
		private static readonly string BassNoteNameGroup = "(?<bn>" + string.Join("|", NoteNameParser.GetPatterns()) + ")";

		// Token: 0x040004F3 RID: 1267
		private static readonly string ChordQualityGroup = "(?<q>(?<maj>(?i:maj)|M)|(?<min>(?i:min)|m)|(?<aug>(?i:aug)|\\+)|(?<dim>(?i:dim))|(?<hdim>ø)|(?<dom>(?i:dom)))";

		// Token: 0x040004F4 RID: 1268
		private static readonly string ChordExtensionQualityGroup = "(?<eq>(?<emaj>(?i:maj)|M)|(?<emin>(?i:min)|m)|(?<eaug>(?i:(aug|a)))|(?<edim>(?i:(dim|d))))";

		// Token: 0x040004F5 RID: 1269
		private static readonly string ChordExtensionGroup = "(?<ext>" + ChordParser.ChordExtensionQualityGroup + "?\\s*(?<en>\\d+))";

		// Token: 0x040004F6 RID: 1270
		private static readonly string SuspendedGroup = "(?<sus>(?i:sus)(?<susn>\\d))";

		// Token: 0x040004F7 RID: 1271
		private static readonly string AddedToneGroup = "(?<add>(?i:add)(?<addn>\\d+))";

		// Token: 0x040004F8 RID: 1272
		private static readonly string AlteredToneGroup = "(?<alt>(?<alta>#|\\+|b|\\-)(?<altn>\\d+))";

		// Token: 0x040004F9 RID: 1273
		internal static readonly string ChordCharacteristicsGroup = string.Concat(new string[]
		{
			ChordParser.ChordQualityGroup,
			"?\\s*(",
			ChordParser.ChordExtensionGroup,
			"|\\(\\s*",
			ChordParser.ChordExtensionGroup,
			"\\s*\\)|/\\s*",
			ChordParser.ChordExtensionGroup,
			")?\\s*",
			ChordParser.AlteredToneGroup,
			"?\\s*",
			ChordParser.SuspendedGroup,
			"?\\s*",
			ChordParser.AddedToneGroup,
			"?\\s*(/(?i:",
			ChordParser.BassNoteNameGroup,
			"))?"
		});

		// Token: 0x040004FA RID: 1274
		private static readonly string[] Patterns = new string[]
		{
			string.Concat(new string[]
			{
				"(?<cn>(?i:",
				ChordParser.RootNoteNameGroup,
				")\\s*",
				ChordParser.ChordCharacteristicsGroup,
				")"
			}),
			string.Concat(new string[]
			{
				"(?<ci>(?i:",
				ChordParser.RootNoteNameGroup,
				")\\s*(?i:",
				ChordParser.IntervalGroup,
				"))"
			})
		};

		// Token: 0x040004FB RID: 1275
		private static readonly Dictionary<string, ChordParser.Quality> GroupsQualities;

		// Token: 0x040004FC RID: 1276
		private static readonly Dictionary<string, IntervalQuality> GroupsExtensionQualities;

		// Token: 0x040004FD RID: 1277
		private static readonly Dictionary<ChordParser.Quality, ChordQuality> ChordQualities;

		// Token: 0x040004FE RID: 1278
		private static readonly Dictionary<ChordParser.Quality, IntervalQuality> ChordToIntervalQualities;

		// Token: 0x040004FF RID: 1279
		private const string ExtensionNumberIsOutOfRange = "Extension number is out of range.";

		// Token: 0x04000500 RID: 1280
		private const string HalfDiminishedOrDominantIsNotSeventh = "Half-diminished or dominant chord is not seventh one.";

		// Token: 0x04000501 RID: 1281
		private const string SuspensionNumberIsOutOfRange = "Suspended chord is not sus2 or sus4.";

		// Token: 0x04000502 RID: 1282
		private const string AddedToneNumberIsOutOfRange = "Added tone number is out of range.";

		// Token: 0x04000503 RID: 1283
		private const string AlteredToneNumberIsOutOfRange = "Altered tone number is out of range.";

		// Token: 0x0200021B RID: 539
		private enum Quality
		{
			// Token: 0x04000C35 RID: 3125
			Major,
			// Token: 0x04000C36 RID: 3126
			Minor,
			// Token: 0x04000C37 RID: 3127
			Diminished,
			// Token: 0x04000C38 RID: 3128
			HalfDiminished,
			// Token: 0x04000C39 RID: 3129
			Augmented,
			// Token: 0x04000C3A RID: 3130
			Dominant
		}
	}
}
