using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000078 RID: 120
	internal static class ChordsNamesTable
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000C304 File Offset: 0x0000A504
		public static IList<string> GetChordNames(NoteName[] notesNames)
		{
			List<string> list = new List<string>();
			if (!notesNames.Any<NoteName>())
			{
				return list;
			}
			HashSet<string> hashSet = new HashSet<string>();
			foreach (NoteName[] array in MathUtilities.GetPermutations<NoteName>(notesNames))
			{
				string text = new string(array.Select((NoteName n) => (char)n).ToArray<char>());
				if (hashSet.Add(text))
				{
					list.AddRange(ChordsNamesTable.GetChordNamesByPermutation(array));
				}
			}
			return (from n in list.Distinct<string>()
				orderby n.Length
				select n).ToArray<string>();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		private static IList<string> GetChordNamesByPermutation(NoteName[] notesNames)
		{
			List<string> list = new List<string>(ChordsNamesTable.GetChordNamesInternal(notesNames));
			NoteName firstNoteName = notesNames.First<NoteName>();
			list.AddRange(from n in ChordsNamesTable.GetChordNamesInternal(notesNames.Skip(1).ToArray<NoteName>())
				select string.Format("{0}/{1}", n, firstNoteName));
			return list;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C42C File Offset: 0x0000A62C
		private static List<string> GetChordNamesInternal(ICollection<NoteName> notesNames)
		{
			List<string> list = new List<string>();
			if (!notesNames.Any<NoteName>())
			{
				return list;
			}
			NoteName rootNoteName = notesNames.First<NoteName>();
			int[] array = (from i in ChordUtilities.GetIntervalsFromRootNote(notesNames)
				select i.HalfSteps).ToArray<int>();
			Func<string, string> <>9__1;
			foreach (ChordsNamesTable.NameDefinition nameDefinition in ChordsNamesTable.NamesDefinitions)
			{
				bool flag = false;
				foreach (int[] array2 in nameDefinition.Intervals)
				{
					if (array2[0] == 0)
					{
						int[] array3 = new int[1].Concat(array).ToArray<int>();
						bool flag2 = array3.Length >= array2.Length;
						int num = 0;
						int num2 = 0;
						while (num2 < array2.Length && num2 < array3.Length && flag2)
						{
							int num3 = array2[num2];
							if (array3[num2] != num3 && !array3.Contains(num3 - 12) && !array3.Contains(num3 - 24))
							{
								flag2 = false;
							}
							num2++;
							num++;
						}
						while (num < array3.Length && flag2)
						{
							if (!array3.Contains(array3[num] - 12) && !array3.Contains(array3[num] - 24))
							{
								flag2 = false;
							}
							num++;
						}
						flag |= flag2 && num >= array3.Length;
						if (flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					List<string> list2 = list;
					IEnumerable<string> names = nameDefinition.Names;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (string n) => rootNoteName.ToString().Replace("Sharp", "#") + n);
					}
					list2.AddRange(names.Select(func));
					break;
				}
			}
			return list;
		}

		// Token: 0x04000509 RID: 1289
		private static readonly ChordsNamesTable.NameDefinition[] NamesDefinitions = new ChordsNamesTable.NameDefinition[]
		{
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 7 } }, new string[]
			{
				"maj",
				"M",
				string.Empty
			}),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 3, 7 } }, new string[] { "min", "m" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 5, 7 } }, new string[] { "sus4" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 2, 7 } }, new string[] { "sus2" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 6 } }, new string[] { "b5" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 3, 6 } }, new string[] { "dim" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 8 } }, new string[] { "aug" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 3, 7, 9 } }, new string[] { "min6", "m6" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 7, 9 } }, new string[] { "maj6", "M6", "6" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 7, 10 } }, new string[] { "7" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 5, 7, 10 } }, new string[] { "7sus4" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 2, 7, 10 } }, new string[] { "7sus2" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 3, 7, 10 } }, new string[] { "min7", "m7" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 3, 7, 10, 14 },
				new int[] { 0, 3, 10, 14 },
				new int[] { 3, 10, 14 },
				new int[] { 3, 7, 10, 14 }
			}, new string[] { "min9", "min7(9)", "m9", "m7(9)" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 3, 7, 10, 14, 17 },
				new int[] { 0, 3, 10, 14, 17 },
				new int[] { 3, 10, 14, 17 },
				new int[] { 3, 7, 10, 14, 17 }
			}, new string[] { "min11", "min7(9,11)", "m11", "m7(9,11)" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 7, 11 } }, new string[] { "maj7" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 4, 7, 11, 14 },
				new int[] { 0, 4, 11, 14 },
				new int[] { 4, 11, 14 },
				new int[] { 4, 7, 11, 14 }
			}, new string[] { "maj7(9)", "M7(9)" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 4, 7, 11, 14, 18 },
				new int[] { 0, 4, 11, 14, 18 },
				new int[] { 4, 11, 14, 18 },
				new int[] { 4, 7, 11, 14, 18 }
			}, new string[] { "maj7(#11)", "M7(#11)" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 4, 7, 11, 21 },
				new int[] { 0, 4, 11, 21 },
				new int[] { 4, 11, 21 },
				new int[] { 4, 7, 11, 21 }
			}, new string[] { "maj7(13)", "M7(13)" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 4, 7, 11, 14, 21 },
				new int[] { 0, 4, 11, 14, 21 },
				new int[] { 4, 11, 14, 21 },
				new int[] { 4, 7, 11, 14, 21 }
			}, new string[] { "maj7(9,13)", "M7(9,13)" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 8, 11 } }, new string[] { "maj7#5", "M7#5" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 4, 8, 11, 14 },
				new int[] { 4, 8, 11, 14 }
			}, new string[] { "maj7#5(9)", "M7#5(9)" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 3, 7, 11 } }, new string[] { "minMaj7", "mM7" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 3, 7, 11, 14 },
				new int[] { 0, 3, 11, 14 },
				new int[] { 3, 11, 14 },
				new int[] { 3, 7, 11, 14 }
			}, new string[] { "minMaj7(9)", "mM7(9)" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 7 } }, new string[] { "5" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 6, 10 } }, new string[] { "7b5", "dom7dim5", "7dim5" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 3, 6, 10 } }, new string[] { "ø", "ø7", "m7b5", "min7dim5", "m7dim5", "min7b5", "m7b5" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 8, 10 } }, new string[] { "aug7" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 3, 6, 9 } }, new string[] { "dim7" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 7, 14 } }, new string[] { "add9" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 3, 7, 14 } }, new string[] { "minAdd9", "mAdd9" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 4, 7, 9, 14 },
				new int[] { 4, 7, 9, 14 },
				new int[] { 0, 4, 9, 14 },
				new int[] { 4, 9, 14 }
			}, new string[] { "maj6(9)", "6(9)", "6/9", "M6/9", "M6(9)" }),
			new ChordsNamesTable.NameDefinition(new int[][]
			{
				new int[] { 0, 3, 7, 9, 14 },
				new int[] { 3, 7, 9, 14 },
				new int[] { 0, 3, 9, 14 },
				new int[] { 3, 9, 14 }
			}, new string[] { "min6(9)", "m6(9)", "m6/9", "min6/9" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 7, 10, 14 } }, new string[] { "9" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 2, 7, 10, 14 } }, new string[] { "9sus2" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 5, 7, 10, 14 } }, new string[] { "9sus4" }),
			new ChordsNamesTable.NameDefinition(new int[][] { new int[] { 0, 4, 7, 10, 14, 17 } }, new string[] { "11" })
		}.OrderByDescending((ChordsNamesTable.NameDefinition d) => d.Intervals.First<int[]>().Length).ToArray<ChordsNamesTable.NameDefinition>();

		// Token: 0x02000223 RID: 547
		private sealed class NameDefinition
		{
			// Token: 0x06000D3B RID: 3387 RVA: 0x00028B27 File Offset: 0x00026D27
			public NameDefinition(int[][] intervals, params string[] names)
			{
				this.Intervals = intervals;
				this.Names = names;
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00028B3D File Offset: 0x00026D3D
			public int[][] Intervals { get; }

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00028B45 File Offset: 0x00026D45
			public string[] Names { get; }
		}
	}
}
