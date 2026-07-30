using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x0200007B RID: 123
	public sealed class Interval : IComparable<Interval>
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000D3C6 File Offset: 0x0000B5C6
		private Interval(SevenBitNumber size, IntervalDirection direction)
		{
			this.Size = size;
			this.Direction = direction;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public SevenBitNumber Size { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000D3E4 File Offset: 0x0000B5E4
		public IntervalDirection Direction { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000D3EC File Offset: 0x0000B5EC
		public int HalfSteps
		{
			get
			{
				if (this.Direction != IntervalDirection.Up)
				{
					return (int)(-(int)this.Size);
				}
				return (int)this.Size;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D40E File Offset: 0x0000B60E
		public Interval Up()
		{
			return Interval.Get(this.Size, IntervalDirection.Up);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D41C File Offset: 0x0000B61C
		public Interval Down()
		{
			return Interval.Get(this.Size, IntervalDirection.Down);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D42C File Offset: 0x0000B62C
		public IReadOnlyCollection<IntervalDefinition> GetIntervalDefinitions()
		{
			if (this._intervalDefinitions != null)
			{
				return this._intervalDefinitions;
			}
			List<IntervalDefinition> list = new List<IntervalDefinition>();
			IntervalQuality? intervalQuality = Interval.QualitiesPattern[(int)(this.Size % 12)];
			int num = (int)(7 * (this.Size / 12)) + Interval.IntervalNumbersOffsets[(int)(this.Size % 12)];
			if (intervalQuality != null)
			{
				list.Add(new IntervalDefinition(num, intervalQuality.Value));
				IntervalQuality intervalQuality2 = IntervalQuality.Augmented;
				switch (intervalQuality.Value)
				{
				case IntervalQuality.Perfect:
					if (num == 1)
					{
						intervalQuality2 = IntervalQuality.Diminished;
					}
					else
					{
						intervalQuality2 = Interval.AdditionalQualitiesPattern[num % 7];
					}
					if (num % 7 == 1)
					{
						if (num > 1)
						{
							list.Add(new IntervalDefinition(num - 1, IntervalQuality.Augmented));
						}
						list.Add(new IntervalDefinition(num + 1, IntervalQuality.Diminished));
						return this._intervalDefinitions = new ReadOnlyCollection<IntervalDefinition>(list);
					}
					break;
				case IntervalQuality.Major:
					intervalQuality2 = IntervalQuality.Diminished;
					break;
				case IntervalQuality.Minor:
					intervalQuality2 = IntervalQuality.Augmented;
					break;
				}
				if (intervalQuality2 != IntervalQuality.Augmented)
				{
					if (intervalQuality2 == IntervalQuality.Diminished)
					{
						list.Add(new IntervalDefinition(num + 1, IntervalQuality.Diminished));
					}
				}
				else
				{
					list.Add(new IntervalDefinition(num - 1, IntervalQuality.Augmented));
				}
			}
			else
			{
				list.Add(new IntervalDefinition(num, IntervalQuality.Diminished));
				list.Add(new IntervalDefinition(num - 1, IntervalQuality.Augmented));
			}
			return this._intervalDefinitions = new ReadOnlyCollection<IntervalDefinition>(list);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D57C File Offset: 0x0000B77C
		public static bool IsPerfect(int intervalNumber)
		{
			ThrowIfArgument.IsLessThan("intervalNumber", intervalNumber, 1, "Interval number is less than 1.");
			int num = intervalNumber % 7 - 1;
			return num == 0 || num == 3 || num == 4;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		public static bool IsQualityApplicable(IntervalQuality intervalQuality, int intervalNumber)
		{
			ThrowIfArgument.IsInvalidEnumValue<IntervalQuality>("intervalQuality", intervalQuality);
			ThrowIfArgument.IsLessThan("intervalNumber", intervalNumber, 1, "Interval number is less than 1.");
			switch (intervalQuality)
			{
			case IntervalQuality.Perfect:
				return Interval.IsPerfect(intervalNumber);
			case IntervalQuality.Major:
			case IntervalQuality.Minor:
				return !Interval.IsPerfect(intervalNumber);
			case IntervalQuality.Augmented:
				return true;
			case IntervalQuality.Diminished:
				return intervalNumber >= 2;
			default:
				return false;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D614 File Offset: 0x0000B814
		public static Interval Get(IntervalQuality intervalQuality, int intervalNumber)
		{
			ThrowIfArgument.IsInvalidEnumValue<IntervalQuality>("intervalQuality", intervalQuality);
			ThrowIfArgument.IsLessThan("intervalNumber", intervalNumber, 1, "Interval number is less than 1.");
			if (!Interval.IsQualityApplicable(intervalQuality, intervalNumber))
			{
				throw new ArgumentException(string.Format("{0} quality is not applicable to interval number of {1}.", intervalQuality, intervalNumber), "intervalQuality");
			}
			int num = 8;
			if (intervalQuality == IntervalQuality.Minor || intervalQuality == IntervalQuality.Major || intervalQuality == IntervalQuality.Augmented)
			{
				num = 7;
			}
			int num2 = ((intervalNumber > num) ? ((intervalNumber - 1) / 7 * 12) : 0);
			int num3 = intervalNumber;
			if (intervalNumber > num)
			{
				num3 = (intervalNumber - 1) % 7 + 1;
			}
			Dictionary<int, int> dictionary = Interval.IntervalsHalfTones[intervalQuality];
			return Interval.FromHalfSteps(num2 + dictionary[num3]);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		public static Interval Get(SevenBitNumber intervalSize, IntervalDirection direction)
		{
			ThrowIfArgument.IsInvalidEnumValue<IntervalDirection>("direction", direction);
			Dictionary<IntervalDirection, Interval> dictionary;
			if (!Interval.Cache.TryGetValue(intervalSize, out dictionary))
			{
				Interval.Cache.Add(intervalSize, dictionary = new Dictionary<IntervalDirection, Interval>());
			}
			Interval interval;
			if (!dictionary.TryGetValue(direction, out interval))
			{
				dictionary.Add(direction, interval = new Interval(intervalSize, direction));
			}
			return interval;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D705 File Offset: 0x0000B905
		public static Interval GetUp(SevenBitNumber intervalSize)
		{
			return Interval.Get(intervalSize, IntervalDirection.Up);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D70E File Offset: 0x0000B90E
		public static Interval GetDown(SevenBitNumber intervalSize)
		{
			return Interval.Get(intervalSize, IntervalDirection.Down);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D718 File Offset: 0x0000B918
		public static Interval FromHalfSteps(int halfSteps)
		{
			ThrowIfArgument.IsOutOfRange("halfSteps", halfSteps, (int)(-(int)SevenBitNumber.MaxValue), (int)SevenBitNumber.MaxValue, "Half steps number is out of range.");
			return Interval.Get((SevenBitNumber)((byte)Math.Abs(halfSteps)), (Math.Sign(halfSteps) < 0) ? IntervalDirection.Down : IntervalDirection.Up);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D768 File Offset: 0x0000B968
		public static Interval FromDefinition(IntervalDefinition intervalDefinition)
		{
			ThrowIfArgument.IsNull("intervalDefinition", intervalDefinition);
			return Interval.Get(intervalDefinition.Quality, intervalDefinition.Number);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D786 File Offset: 0x0000B986
		public static bool TryParse(string input, out Interval interval)
		{
			return ParsingUtilities.TryParse<Interval>(input, new Parsing<Interval>(IntervalParser.TryParse), out interval);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D79B File Offset: 0x0000B99B
		public static Interval Parse(string input)
		{
			return ParsingUtilities.Parse<Interval>(input, new Parsing<Interval>(IntervalParser.TryParse));
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D7AF File Offset: 0x0000B9AF
		public static implicit operator int(Interval interval)
		{
			return interval.HalfSteps;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D7B7 File Offset: 0x0000B9B7
		public static implicit operator Interval(SevenBitNumber interval)
		{
			return Interval.GetUp(interval);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D7BF File Offset: 0x0000B9BF
		public static bool operator ==(Interval interval1, Interval interval2)
		{
			return interval1 == interval2 || (interval1 != null && interval2 != null && interval1.HalfSteps == interval2.HalfSteps);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D7DD File Offset: 0x0000B9DD
		public static bool operator !=(Interval interval1, Interval interval2)
		{
			return !(interval1 == interval2);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000D7E9 File Offset: 0x0000B9E9
		public static Interval operator +(Interval interval, int halfSteps)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return Interval.FromHalfSteps(interval.HalfSteps + halfSteps);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D803 File Offset: 0x0000BA03
		public static Interval operator -(Interval interval, int halfSteps)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return Interval.FromHalfSteps(interval.HalfSteps - halfSteps);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D81D File Offset: 0x0000BA1D
		public static Interval operator *(Interval interval, int multiplier)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return Interval.FromHalfSteps(interval.HalfSteps * multiplier);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D837 File Offset: 0x0000BA37
		public static Interval operator /(Interval interval, int divisor)
		{
			ThrowIfArgument.IsNull("interval", interval);
			if (divisor == 0)
			{
				throw new ArgumentOutOfRangeException("divisor", divisor, "Divisor is zero.");
			}
			return Interval.FromHalfSteps(interval.HalfSteps / divisor);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D86A File Offset: 0x0000BA6A
		public static Interval operator +(Interval interval)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return interval.Up();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D87D File Offset: 0x0000BA7D
		public static Interval operator -(Interval interval)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return interval.Down();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D890 File Offset: 0x0000BA90
		public int CompareTo(Interval other)
		{
			return this.HalfSteps.CompareTo(other.HalfSteps);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D8B1 File Offset: 0x0000BAB1
		public override string ToString()
		{
			return string.Format("{0}{1}", (this.Direction == IntervalDirection.Up) ? "+" : "-", this.Size);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D8DC File Offset: 0x0000BADC
		public override bool Equals(object obj)
		{
			return this == obj as Interval;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		public override int GetHashCode()
		{
			return this.HalfSteps.GetHashCode();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D908 File Offset: 0x0000BB08
		// Note: this type is marked as 'beforefieldinit'.
		static Interval()
		{
			Dictionary<IntervalQuality, Dictionary<int, int>> dictionary = new Dictionary<IntervalQuality, Dictionary<int, int>>();
			IntervalQuality intervalQuality = IntervalQuality.Perfect;
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			dictionary2[1] = 0;
			dictionary2[4] = 5;
			dictionary2[5] = 7;
			dictionary2[8] = 12;
			dictionary[intervalQuality] = dictionary2;
			IntervalQuality intervalQuality2 = IntervalQuality.Minor;
			Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
			dictionary3[2] = 1;
			dictionary3[3] = 3;
			dictionary3[6] = 8;
			dictionary3[7] = 10;
			dictionary[intervalQuality2] = dictionary3;
			IntervalQuality intervalQuality3 = IntervalQuality.Major;
			Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
			dictionary4[2] = 2;
			dictionary4[3] = 4;
			dictionary4[6] = 9;
			dictionary4[7] = 11;
			dictionary[intervalQuality3] = dictionary4;
			IntervalQuality intervalQuality4 = IntervalQuality.Diminished;
			Dictionary<int, int> dictionary5 = new Dictionary<int, int>();
			dictionary5[1] = -1;
			dictionary5[2] = 0;
			dictionary5[3] = 2;
			dictionary5[4] = 4;
			dictionary5[5] = 6;
			dictionary5[6] = 7;
			dictionary5[7] = 9;
			dictionary5[8] = 11;
			dictionary[intervalQuality4] = dictionary5;
			IntervalQuality intervalQuality5 = IntervalQuality.Augmented;
			Dictionary<int, int> dictionary6 = new Dictionary<int, int>();
			dictionary6[1] = 1;
			dictionary6[2] = 3;
			dictionary6[3] = 5;
			dictionary6[4] = 6;
			dictionary6[5] = 8;
			dictionary6[6] = 10;
			dictionary6[7] = 12;
			dictionary[intervalQuality5] = dictionary6;
			Interval.IntervalsHalfTones = dictionary;
			Interval.QualitiesPattern = new IntervalQuality?[]
			{
				new IntervalQuality?(IntervalQuality.Perfect),
				new IntervalQuality?(IntervalQuality.Minor),
				new IntervalQuality?(IntervalQuality.Major),
				new IntervalQuality?(IntervalQuality.Minor),
				new IntervalQuality?(IntervalQuality.Major),
				new IntervalQuality?(IntervalQuality.Perfect),
				null,
				new IntervalQuality?(IntervalQuality.Perfect),
				new IntervalQuality?(IntervalQuality.Minor),
				new IntervalQuality?(IntervalQuality.Major),
				new IntervalQuality?(IntervalQuality.Minor),
				new IntervalQuality?(IntervalQuality.Major)
			};
			Dictionary<int, IntervalQuality> dictionary7 = new Dictionary<int, IntervalQuality>();
			dictionary7[1] = IntervalQuality.Augmented;
			dictionary7[4] = IntervalQuality.Augmented;
			dictionary7[5] = IntervalQuality.Diminished;
			Interval.AdditionalQualitiesPattern = dictionary7;
			Interval.IntervalNumbersOffsets = new int[]
			{
				1, 2, 2, 3, 3, 4, 5, 5, 6, 6,
				7, 7
			};
		}

		// Token: 0x04000511 RID: 1297
		private static readonly Dictionary<SevenBitNumber, Dictionary<IntervalDirection, Interval>> Cache = new Dictionary<SevenBitNumber, Dictionary<IntervalDirection, Interval>>();

		// Token: 0x04000512 RID: 1298
		private IReadOnlyCollection<IntervalDefinition> _intervalDefinitions;

		// Token: 0x04000513 RID: 1299
		public static readonly Interval Zero = Interval.FromHalfSteps(0);

		// Token: 0x04000514 RID: 1300
		public static readonly Interval One = Interval.FromHalfSteps(1);

		// Token: 0x04000515 RID: 1301
		public static readonly Interval Two = Interval.FromHalfSteps(2);

		// Token: 0x04000516 RID: 1302
		public static readonly Interval Three = Interval.FromHalfSteps(3);

		// Token: 0x04000517 RID: 1303
		public static readonly Interval Four = Interval.FromHalfSteps(4);

		// Token: 0x04000518 RID: 1304
		public static readonly Interval Five = Interval.FromHalfSteps(5);

		// Token: 0x04000519 RID: 1305
		public static readonly Interval Six = Interval.FromHalfSteps(6);

		// Token: 0x0400051A RID: 1306
		public static readonly Interval Seven = Interval.FromHalfSteps(7);

		// Token: 0x0400051B RID: 1307
		public static readonly Interval Eight = Interval.FromHalfSteps(8);

		// Token: 0x0400051C RID: 1308
		public static readonly Interval Nine = Interval.FromHalfSteps(9);

		// Token: 0x0400051D RID: 1309
		public static readonly Interval Ten = Interval.FromHalfSteps(10);

		// Token: 0x0400051E RID: 1310
		public static readonly Interval Eleven = Interval.FromHalfSteps(11);

		// Token: 0x0400051F RID: 1311
		public static readonly Interval Twelve = Interval.FromHalfSteps(12);

		// Token: 0x04000520 RID: 1312
		private static readonly Dictionary<IntervalQuality, Dictionary<int, int>> IntervalsHalfTones;

		// Token: 0x04000521 RID: 1313
		private static readonly IntervalQuality?[] QualitiesPattern;

		// Token: 0x04000522 RID: 1314
		private static readonly Dictionary<int, IntervalQuality> AdditionalQualitiesPattern;

		// Token: 0x04000523 RID: 1315
		private static readonly int[] IntervalNumbersOffsets;
	}
}
