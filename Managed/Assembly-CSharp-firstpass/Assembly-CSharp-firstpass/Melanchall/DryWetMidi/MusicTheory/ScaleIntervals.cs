using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x0200008A RID: 138
	public static class ScaleIntervals
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000EF30 File Offset: 0x0000D130
		public static IEnumerable<Interval> GetByName(string name)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("name", name, "Scale's name");
			foreach (FieldInfo fieldInfo in typeof(ScaleIntervals).GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				DisplayNameAttribute displayNameAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
				string text = ((displayNameAttribute != null) ? displayNameAttribute.Name : null);
				if (!string.IsNullOrWhiteSpace(text) && text.Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					IEnumerable<Interval> enumerable = fieldInfo.GetValue(null) as IEnumerable<Interval>;
					if (enumerable != null)
					{
						return enumerable;
					}
				}
			}
			return null;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000EFBB File Offset: 0x0000D1BB
		private static IEnumerable<Interval> GetIntervals(params int[] intervalsInHalfSteps)
		{
			return intervalsInHalfSteps.Select((int i) => Interval.FromHalfSteps(i)).ToArray<Interval>();
		}

		// Token: 0x040005EB RID: 1515
		[DisplayName("aeolian")]
		public static readonly IEnumerable<Interval> Aeolian = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 2, 1, 2, 2 });

		// Token: 0x040005EC RID: 1516
		[DisplayName("altered")]
		public static readonly IEnumerable<Interval> Altered = ScaleIntervals.GetIntervals(new int[] { 1, 2, 1, 2, 2, 2, 2 });

		// Token: 0x040005ED RID: 1517
		[DisplayName("arabian")]
		public static readonly IEnumerable<Interval> Arabian = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 1, 2, 2, 2 });

		// Token: 0x040005EE RID: 1518
		[DisplayName("augmented")]
		public static readonly IEnumerable<Interval> Augmented = ScaleIntervals.GetIntervals(new int[] { 3, 1, 3, 1, 3, 1 });

		// Token: 0x040005EF RID: 1519
		[DisplayName("augmented heptatonic")]
		public static readonly IEnumerable<Interval> AugmentedHeptatonic = ScaleIntervals.GetIntervals(new int[] { 3, 1, 1, 2, 1, 3, 1 });

		// Token: 0x040005F0 RID: 1520
		[DisplayName("balinese")]
		public static readonly IEnumerable<Interval> Balinese = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 2, 1, 3, 1 });

		// Token: 0x040005F1 RID: 1521
		[DisplayName("bebop")]
		public static readonly IEnumerable<Interval> Bebop = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 2, 1, 1, 1 });

		// Token: 0x040005F2 RID: 1522
		[DisplayName("bebop dominant")]
		public static readonly IEnumerable<Interval> BebopDominant = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 2, 1, 1, 1 });

		// Token: 0x040005F3 RID: 1523
		[DisplayName("bebop locrian")]
		public static readonly IEnumerable<Interval> BebopLocrian = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 1, 1, 1, 2, 2 });

		// Token: 0x040005F4 RID: 1524
		[DisplayName("bebop major")]
		public static readonly IEnumerable<Interval> BebopMajor = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 1, 1, 2, 1 });

		// Token: 0x040005F5 RID: 1525
		[DisplayName("bebop minor")]
		public static readonly IEnumerable<Interval> BebopMinor = ScaleIntervals.GetIntervals(new int[] { 2, 1, 1, 1, 2, 2, 1, 2 });

		// Token: 0x040005F6 RID: 1526
		[DisplayName("blues")]
		public static readonly IEnumerable<Interval> Blues = ScaleIntervals.GetIntervals(new int[] { 3, 2, 1, 1, 3, 2 });

		// Token: 0x040005F7 RID: 1527
		[DisplayName("chinese")]
		public static readonly IEnumerable<Interval> Chinese = ScaleIntervals.GetIntervals(new int[] { 4, 2, 1, 4, 1 });

		// Token: 0x040005F8 RID: 1528
		[DisplayName("chromatic")]
		public static readonly IEnumerable<Interval> Chromatic = ScaleIntervals.GetIntervals(new int[]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1
		});

		// Token: 0x040005F9 RID: 1529
		[DisplayName("composite blues")]
		public static readonly IEnumerable<Interval> CompositeBlues = ScaleIntervals.GetIntervals(new int[] { 2, 1, 1, 1, 1, 1, 2, 1, 2 });

		// Token: 0x040005FA RID: 1530
		[DisplayName("diminished")]
		public static readonly IEnumerable<Interval> Diminished = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 1, 2, 1, 2, 1 });

		// Token: 0x040005FB RID: 1531
		[DisplayName("diminished whole tone")]
		public static readonly IEnumerable<Interval> DiminishedWholeTone = ScaleIntervals.GetIntervals(new int[] { 1, 2, 1, 2, 2, 2, 2 });

		// Token: 0x040005FC RID: 1532
		[DisplayName("dominant")]
		public static readonly IEnumerable<Interval> Dominant = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 2, 1, 2 });

		// Token: 0x040005FD RID: 1533
		[DisplayName("dorian")]
		public static readonly IEnumerable<Interval> Dorian = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 2, 2, 1, 2 });

		// Token: 0x040005FE RID: 1534
		[DisplayName("dorian #4")]
		public static readonly IEnumerable<Interval> Dorian4 = ScaleIntervals.GetIntervals(new int[] { 2, 1, 3, 1, 2, 1, 2 });

		// Token: 0x040005FF RID: 1535
		[DisplayName("dorian b2")]
		public static readonly IEnumerable<Interval> DorianB2 = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 2, 2, 2, 1 });

		// Token: 0x04000600 RID: 1536
		[DisplayName("double harmonic lydian")]
		public static readonly IEnumerable<Interval> DoubleHarmonicLydian = ScaleIntervals.GetIntervals(new int[] { 1, 3, 2, 1, 1, 3, 1 });

		// Token: 0x04000601 RID: 1537
		[DisplayName("double harmonic major")]
		public static readonly IEnumerable<Interval> DoubleHarmonicMajor = ScaleIntervals.GetIntervals(new int[] { 1, 3, 1, 2, 1, 3, 1 });

		// Token: 0x04000602 RID: 1538
		[DisplayName("egyptian")]
		public static readonly IEnumerable<Interval> Egyptian = ScaleIntervals.GetIntervals(new int[] { 2, 3, 2, 3, 2 });

		// Token: 0x04000603 RID: 1539
		[DisplayName("enigmatic")]
		public static readonly IEnumerable<Interval> Enigmatic = ScaleIntervals.GetIntervals(new int[] { 1, 3, 2, 2, 2, 1, 1 });

		// Token: 0x04000604 RID: 1540
		[DisplayName("flamenco")]
		public static readonly IEnumerable<Interval> Flamenco = ScaleIntervals.GetIntervals(new int[] { 1, 2, 1, 2, 1, 3, 2 });

		// Token: 0x04000605 RID: 1541
		[DisplayName("flat six pentatonic")]
		public static readonly IEnumerable<Interval> FlatSixPentatonic = ScaleIntervals.GetIntervals(new int[] { 2, 2, 3, 1, 4 });

		// Token: 0x04000606 RID: 1542
		[DisplayName("flat three pentatonic")]
		public static readonly IEnumerable<Interval> FlatThreePentatonic = ScaleIntervals.GetIntervals(new int[] { 2, 1, 4, 2, 3 });

		// Token: 0x04000607 RID: 1543
		[DisplayName("gypsy")]
		public static readonly IEnumerable<Interval> Gypsy = ScaleIntervals.GetIntervals(new int[] { 1, 3, 1, 2, 1, 3, 1 });

		// Token: 0x04000608 RID: 1544
		[DisplayName("harmonic major")]
		public static readonly IEnumerable<Interval> HarmonicMajor = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 1, 3, 1 });

		// Token: 0x04000609 RID: 1545
		[DisplayName("harmonic minor")]
		public static readonly IEnumerable<Interval> HarmonicMinor = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 2, 1, 3, 1 });

		// Token: 0x0400060A RID: 1546
		[DisplayName("hindu")]
		public static readonly IEnumerable<Interval> Hindu = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 1, 2, 2 });

		// Token: 0x0400060B RID: 1547
		[DisplayName("hirajoshi")]
		public static readonly IEnumerable<Interval> Hirajoshi = ScaleIntervals.GetIntervals(new int[] { 2, 1, 4, 1, 4 });

		// Token: 0x0400060C RID: 1548
		[DisplayName("hungarian major")]
		public static readonly IEnumerable<Interval> HungarianMajor = ScaleIntervals.GetIntervals(new int[] { 3, 1, 2, 1, 2, 1, 2 });

		// Token: 0x0400060D RID: 1549
		[DisplayName("hungarian minor")]
		public static readonly IEnumerable<Interval> HungarianMinor = ScaleIntervals.GetIntervals(new int[] { 2, 1, 3, 1, 1, 3, 1 });

		// Token: 0x0400060E RID: 1550
		[DisplayName("ichikosucho")]
		public static readonly IEnumerable<Interval> Ichikosucho = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 1, 1, 2, 2, 1 });

		// Token: 0x0400060F RID: 1551
		[DisplayName("in-sen")]
		public static readonly IEnumerable<Interval> InSen = ScaleIntervals.GetIntervals(new int[] { 1, 4, 2, 3, 2 });

		// Token: 0x04000610 RID: 1552
		[DisplayName("indian")]
		public static readonly IEnumerable<Interval> Indian = ScaleIntervals.GetIntervals(new int[] { 4, 1, 2, 3, 2 });

		// Token: 0x04000611 RID: 1553
		[DisplayName("ionian")]
		public static readonly IEnumerable<Interval> Ionian = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 2, 2, 1 });

		// Token: 0x04000612 RID: 1554
		[DisplayName("ionian augmented")]
		public static readonly IEnumerable<Interval> IonianAugmented = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 3, 1, 2, 1 });

		// Token: 0x04000613 RID: 1555
		[DisplayName("ionian pentatonic")]
		public static readonly IEnumerable<Interval> IonianPentatonic = ScaleIntervals.GetIntervals(new int[] { 4, 1, 2, 4, 1 });

		// Token: 0x04000614 RID: 1556
		[DisplayName("iwato")]
		public static readonly IEnumerable<Interval> Iwato = ScaleIntervals.GetIntervals(new int[] { 1, 4, 1, 4, 2 });

		// Token: 0x04000615 RID: 1557
		[DisplayName("kafi raga")]
		public static readonly IEnumerable<Interval> KafiRaga = ScaleIntervals.GetIntervals(new int[] { 3, 1, 1, 2, 2, 1, 1, 1 });

		// Token: 0x04000616 RID: 1558
		[DisplayName("kumoi")]
		public static readonly IEnumerable<Interval> Kumoi = ScaleIntervals.GetIntervals(new int[] { 2, 1, 4, 2, 3 });

		// Token: 0x04000617 RID: 1559
		[DisplayName("kumoijoshi")]
		public static readonly IEnumerable<Interval> Kumoijoshi = ScaleIntervals.GetIntervals(new int[] { 1, 4, 2, 1, 4 });

		// Token: 0x04000618 RID: 1560
		[DisplayName("leading whole tone")]
		public static readonly IEnumerable<Interval> LeadingWholeTone = ScaleIntervals.GetIntervals(new int[] { 2, 2, 2, 2, 2, 1, 1 });

		// Token: 0x04000619 RID: 1561
		[DisplayName("locrian")]
		public static readonly IEnumerable<Interval> Locrian = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 1, 2, 2, 2 });

		// Token: 0x0400061A RID: 1562
		[DisplayName("locrian #2")]
		public static readonly IEnumerable<Interval> Locrian2 = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 1, 2, 2, 2 });

		// Token: 0x0400061B RID: 1563
		[DisplayName("locrian major")]
		public static readonly IEnumerable<Interval> LocrianMajor = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 1, 2, 2, 2 });

		// Token: 0x0400061C RID: 1564
		[DisplayName("locrian pentatonic")]
		public static readonly IEnumerable<Interval> LocrianPentatonic = ScaleIntervals.GetIntervals(new int[] { 3, 2, 1, 4, 2 });

		// Token: 0x0400061D RID: 1565
		[DisplayName("lydian")]
		public static readonly IEnumerable<Interval> Lydian = ScaleIntervals.GetIntervals(new int[] { 2, 2, 2, 1, 2, 2, 1 });

		// Token: 0x0400061E RID: 1566
		[DisplayName("lydian #5P pentatonic")]
		public static readonly IEnumerable<Interval> Lydian5PPentatonic = ScaleIntervals.GetIntervals(new int[] { 4, 2, 2, 3, 1 });

		// Token: 0x0400061F RID: 1567
		[DisplayName("lydian #9")]
		public static readonly IEnumerable<Interval> Lydian9 = ScaleIntervals.GetIntervals(new int[] { 1, 3, 2, 1, 2, 2, 1 });

		// Token: 0x04000620 RID: 1568
		[DisplayName("lydian augmented")]
		public static readonly IEnumerable<Interval> LydianAugmented = ScaleIntervals.GetIntervals(new int[] { 2, 2, 2, 2, 1, 2, 1 });

		// Token: 0x04000621 RID: 1569
		[DisplayName("lydian b7")]
		public static readonly IEnumerable<Interval> LydianB7 = ScaleIntervals.GetIntervals(new int[] { 2, 2, 2, 1, 2, 1, 2 });

		// Token: 0x04000622 RID: 1570
		[DisplayName("lydian diminished")]
		public static readonly IEnumerable<Interval> LydianDiminished = ScaleIntervals.GetIntervals(new int[] { 2, 1, 3, 1, 2, 2, 1 });

		// Token: 0x04000623 RID: 1571
		[DisplayName("lydian dominant")]
		public static readonly IEnumerable<Interval> LydianDominant = ScaleIntervals.GetIntervals(new int[] { 2, 2, 2, 1, 2, 1, 2 });

		// Token: 0x04000624 RID: 1572
		[DisplayName("lydian dominant pentatonic")]
		public static readonly IEnumerable<Interval> LydianDominantPentatonic = ScaleIntervals.GetIntervals(new int[] { 4, 2, 1, 3, 2 });

		// Token: 0x04000625 RID: 1573
		[DisplayName("lydian minor")]
		public static readonly IEnumerable<Interval> LydianMinor = ScaleIntervals.GetIntervals(new int[] { 2, 2, 2, 1, 1, 2, 2 });

		// Token: 0x04000626 RID: 1574
		[DisplayName("lydian pentatonic")]
		public static readonly IEnumerable<Interval> LydianPentatonic = ScaleIntervals.GetIntervals(new int[] { 4, 2, 1, 4, 1 });

		// Token: 0x04000627 RID: 1575
		[DisplayName("major")]
		public static readonly IEnumerable<Interval> Major = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 2, 2, 1 });

		// Token: 0x04000628 RID: 1576
		[DisplayName("major blues")]
		public static readonly IEnumerable<Interval> MajorBlues = ScaleIntervals.GetIntervals(new int[] { 2, 1, 1, 3, 2, 3 });

		// Token: 0x04000629 RID: 1577
		[DisplayName("major flat two pentatonic")]
		public static readonly IEnumerable<Interval> MajorFlatTwoPentatonic = ScaleIntervals.GetIntervals(new int[] { 1, 3, 3, 2, 3 });

		// Token: 0x0400062A RID: 1578
		[DisplayName("major pentatonic")]
		public static readonly IEnumerable<Interval> MajorPentatonic = ScaleIntervals.GetIntervals(new int[] { 2, 2, 3, 2, 3 });

		// Token: 0x0400062B RID: 1579
		[DisplayName("malkos raga")]
		public static readonly IEnumerable<Interval> MalkosRaga = ScaleIntervals.GetIntervals(new int[] { 3, 2, 3, 2, 2 });

		// Token: 0x0400062C RID: 1580
		[DisplayName("melodic minor")]
		public static readonly IEnumerable<Interval> MelodicMinor = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 2, 2, 2, 1 });

		// Token: 0x0400062D RID: 1581
		[DisplayName("melodic minor fifth mode")]
		public static readonly IEnumerable<Interval> MelodicMinorFifthMode = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 1, 2, 2 });

		// Token: 0x0400062E RID: 1582
		[DisplayName("melodic minor second mode")]
		public static readonly IEnumerable<Interval> MelodicMinorSecondMode = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 2, 2, 1, 2 });

		// Token: 0x0400062F RID: 1583
		[DisplayName("minor")]
		public static readonly IEnumerable<Interval> Minor = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 2, 1, 2, 2 });

		// Token: 0x04000630 RID: 1584
		[DisplayName("minor #7M pentatonic")]
		public static readonly IEnumerable<Interval> Minor7MPentatonic = ScaleIntervals.GetIntervals(new int[] { 3, 2, 2, 4, 1 });

		// Token: 0x04000631 RID: 1585
		[DisplayName("minor bebop")]
		public static readonly IEnumerable<Interval> MinorBebop = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 2, 1, 2, 1, 1 });

		// Token: 0x04000632 RID: 1586
		[DisplayName("minor blues")]
		public static readonly IEnumerable<Interval> MinorBlues = ScaleIntervals.GetIntervals(new int[] { 3, 2, 1, 1, 3, 2 });

		// Token: 0x04000633 RID: 1587
		[DisplayName("minor hexatonic")]
		public static readonly IEnumerable<Interval> MinorHexatonic = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 2, 4, 1 });

		// Token: 0x04000634 RID: 1588
		[DisplayName("minor pentatonic")]
		public static readonly IEnumerable<Interval> MinorPentatonic = ScaleIntervals.GetIntervals(new int[] { 3, 2, 2, 3, 2 });

		// Token: 0x04000635 RID: 1589
		[DisplayName("minor seven flat five pentatonic")]
		public static readonly IEnumerable<Interval> MinorSevenFlatFivePentatonic = ScaleIntervals.GetIntervals(new int[] { 3, 2, 1, 4, 2 });

		// Token: 0x04000636 RID: 1590
		[DisplayName("minor six diminished")]
		public static readonly IEnumerable<Interval> MinorSixDiminished = ScaleIntervals.GetIntervals(new int[] { 2, 1, 2, 2, 1, 1, 2, 1 });

		// Token: 0x04000637 RID: 1591
		[DisplayName("minor six pentatonic")]
		public static readonly IEnumerable<Interval> MinorSixPentatonic = ScaleIntervals.GetIntervals(new int[] { 3, 2, 2, 2, 3 });

		// Token: 0x04000638 RID: 1592
		[DisplayName("mixolydian")]
		public static readonly IEnumerable<Interval> Mixolydian = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 2, 1, 2 });

		// Token: 0x04000639 RID: 1593
		[DisplayName("mixolydian b6M")]
		public static readonly IEnumerable<Interval> MixolydianB6M = ScaleIntervals.GetIntervals(new int[] { 2, 2, 1, 2, 1, 2, 2 });

		// Token: 0x0400063A RID: 1594
		[DisplayName("mixolydian pentatonic")]
		public static readonly IEnumerable<Interval> MixolydianPentatonic = ScaleIntervals.GetIntervals(new int[] { 4, 1, 2, 3, 2 });

		// Token: 0x0400063B RID: 1595
		[DisplayName("mystery #1")]
		public static readonly IEnumerable<Interval> Mystery1 = ScaleIntervals.GetIntervals(new int[] { 1, 3, 2, 2, 2, 2 });

		// Token: 0x0400063C RID: 1596
		[DisplayName("neopolitan")]
		public static readonly IEnumerable<Interval> Neopolitan = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 2, 1, 3, 1 });

		// Token: 0x0400063D RID: 1597
		[DisplayName("neopolitan major")]
		public static readonly IEnumerable<Interval> NeopolitanMajor = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 2, 2, 2, 1 });

		// Token: 0x0400063E RID: 1598
		[DisplayName("neopolitan major pentatonic")]
		public static readonly IEnumerable<Interval> NeopolitanMajorPentatonic = ScaleIntervals.GetIntervals(new int[] { 4, 1, 1, 4, 2 });

		// Token: 0x0400063F RID: 1599
		[DisplayName("neopolitan minor")]
		public static readonly IEnumerable<Interval> NeopolitanMinor = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 2, 1, 3, 1 });

		// Token: 0x04000640 RID: 1600
		[DisplayName("oriental")]
		public static readonly IEnumerable<Interval> Oriental = ScaleIntervals.GetIntervals(new int[] { 1, 3, 1, 1, 3, 1, 2 });

		// Token: 0x04000641 RID: 1601
		[DisplayName("pelog")]
		public static readonly IEnumerable<Interval> Pelog = ScaleIntervals.GetIntervals(new int[] { 1, 2, 4, 1, 4 });

		// Token: 0x04000642 RID: 1602
		[DisplayName("pentatonic")]
		public static readonly IEnumerable<Interval> Pentatonic = ScaleIntervals.GetIntervals(new int[] { 2, 2, 3, 2, 3 });

		// Token: 0x04000643 RID: 1603
		[DisplayName("persian")]
		public static readonly IEnumerable<Interval> Persian = ScaleIntervals.GetIntervals(new int[] { 1, 3, 1, 1, 2, 3, 1 });

		// Token: 0x04000644 RID: 1604
		[DisplayName("phrygian")]
		public static readonly IEnumerable<Interval> Phrygian = ScaleIntervals.GetIntervals(new int[] { 1, 2, 2, 2, 1, 2, 2 });

		// Token: 0x04000645 RID: 1605
		[DisplayName("phrygian major")]
		public static readonly IEnumerable<Interval> PhrygianMajor = ScaleIntervals.GetIntervals(new int[] { 1, 3, 1, 2, 1, 2, 2 });

		// Token: 0x04000646 RID: 1606
		[DisplayName("piongio")]
		public static readonly IEnumerable<Interval> Piongio = ScaleIntervals.GetIntervals(new int[] { 2, 3, 2, 2, 1, 2 });

		// Token: 0x04000647 RID: 1607
		[DisplayName("pomeroy")]
		public static readonly IEnumerable<Interval> Pomeroy = ScaleIntervals.GetIntervals(new int[] { 1, 2, 1, 2, 2, 2, 2 });

		// Token: 0x04000648 RID: 1608
		[DisplayName("prometheus")]
		public static readonly IEnumerable<Interval> Prometheus = ScaleIntervals.GetIntervals(new int[] { 2, 2, 2, 3, 1, 2 });

		// Token: 0x04000649 RID: 1609
		[DisplayName("prometheus neopolitan")]
		public static readonly IEnumerable<Interval> PrometheusNeopolitan = ScaleIntervals.GetIntervals(new int[] { 1, 3, 2, 3, 1, 2 });

		// Token: 0x0400064A RID: 1610
		[DisplayName("purvi raga")]
		public static readonly IEnumerable<Interval> PurviRaga = ScaleIntervals.GetIntervals(new int[] { 1, 3, 1, 1, 1, 1, 3, 1 });

		// Token: 0x0400064B RID: 1611
		[DisplayName("ritusen")]
		public static readonly IEnumerable<Interval> Ritusen = ScaleIntervals.GetIntervals(new int[] { 2, 3, 2, 2, 3 });

		// Token: 0x0400064C RID: 1612
		[DisplayName("romanian minor")]
		public static readonly IEnumerable<Interval> RomanianMinor = ScaleIntervals.GetIntervals(new int[] { 2, 1, 3, 1, 2, 1, 2 });

		// Token: 0x0400064D RID: 1613
		[DisplayName("scriabin")]
		public static readonly IEnumerable<Interval> Scriabin = ScaleIntervals.GetIntervals(new int[] { 1, 3, 3, 2, 3 });

		// Token: 0x0400064E RID: 1614
		[DisplayName("six tone symmetric")]
		public static readonly IEnumerable<Interval> SixToneSymmetric = ScaleIntervals.GetIntervals(new int[] { 1, 3, 1, 3, 1, 3 });

		// Token: 0x0400064F RID: 1615
		[DisplayName("spanish")]
		public static readonly IEnumerable<Interval> Spanish = ScaleIntervals.GetIntervals(new int[] { 1, 3, 1, 2, 1, 2, 2 });

		// Token: 0x04000650 RID: 1616
		[DisplayName("spanish heptatonic")]
		public static readonly IEnumerable<Interval> SpanishHeptatonic = ScaleIntervals.GetIntervals(new int[] { 1, 2, 1, 1, 2, 1, 2, 2 });

		// Token: 0x04000651 RID: 1617
		[DisplayName("super locrian")]
		public static readonly IEnumerable<Interval> SuperLocrian = ScaleIntervals.GetIntervals(new int[] { 1, 2, 1, 2, 2, 2, 2 });

		// Token: 0x04000652 RID: 1618
		[DisplayName("super locrian pentatonic")]
		public static readonly IEnumerable<Interval> SuperLocrianPentatonic = ScaleIntervals.GetIntervals(new int[] { 3, 1, 2, 4, 2 });

		// Token: 0x04000653 RID: 1619
		[DisplayName("todi raga")]
		public static readonly IEnumerable<Interval> TodiRaga = ScaleIntervals.GetIntervals(new int[] { 1, 2, 3, 1, 1, 3, 1 });

		// Token: 0x04000654 RID: 1620
		[DisplayName("vietnamese 1")]
		public static readonly IEnumerable<Interval> Vietnamese1 = ScaleIntervals.GetIntervals(new int[] { 3, 2, 2, 1, 4 });

		// Token: 0x04000655 RID: 1621
		[DisplayName("vietnamese 2")]
		public static readonly IEnumerable<Interval> Vietnamese2 = ScaleIntervals.GetIntervals(new int[] { 3, 2, 2, 3, 2 });

		// Token: 0x04000656 RID: 1622
		[DisplayName("whole tone")]
		public static readonly IEnumerable<Interval> WholeTone = ScaleIntervals.GetIntervals(new int[] { 2, 2, 2, 2, 2, 2 });

		// Token: 0x04000657 RID: 1623
		[DisplayName("whole tone pentatonic")]
		public static readonly IEnumerable<Interval> WholeTonePentatonic = ScaleIntervals.GetIntervals(new int[] { 4, 2, 2, 2, 2 });
	}
}
