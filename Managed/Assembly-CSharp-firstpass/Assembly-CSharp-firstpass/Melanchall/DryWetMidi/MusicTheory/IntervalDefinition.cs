using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x0200007C RID: 124
	public sealed class IntervalDefinition
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000DBAF File Offset: 0x0000BDAF
		public IntervalDefinition(int number, IntervalQuality quality)
		{
			ThrowIfArgument.IsLessThan("number", number, 1, "Interval number is less than 1.");
			ThrowIfArgument.IsInvalidEnumValue<IntervalQuality>("quality", quality);
			this.Number = number;
			this.Quality = quality;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000DBE1 File Offset: 0x0000BDE1
		public int Number { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000DBE9 File Offset: 0x0000BDE9
		public IntervalQuality Quality { get; }

		// Token: 0x0600027D RID: 637 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
		public static bool operator ==(IntervalDefinition intervalDefinition1, IntervalDefinition intervalDefinition2)
		{
			return intervalDefinition1 == intervalDefinition2 || (intervalDefinition1 != null && intervalDefinition2 != null && intervalDefinition1.Number == intervalDefinition2.Number && intervalDefinition1.Quality == intervalDefinition2.Quality);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000DC1F File Offset: 0x0000BE1F
		public static bool operator !=(IntervalDefinition intervalDefinition1, IntervalDefinition intervalDefinition2)
		{
			return !(intervalDefinition1 == intervalDefinition2);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000DC2B File Offset: 0x0000BE2B
		public override string ToString()
		{
			return string.Format("{0}{1}", IntervalDefinition.QualitiesSymbols[this.Quality], this.Number);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000DC57 File Offset: 0x0000BE57
		public override bool Equals(object obj)
		{
			return this == obj as IntervalDefinition;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000DC68 File Offset: 0x0000BE68
		public override int GetHashCode()
		{
			return (17 * 23 + this.Number.GetHashCode()) * 23 + this.Quality.GetHashCode();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DCA1 File Offset: 0x0000BEA1
		// Note: this type is marked as 'beforefieldinit'.
		static IntervalDefinition()
		{
			Dictionary<IntervalQuality, char> dictionary = new Dictionary<IntervalQuality, char>();
			dictionary[IntervalQuality.Perfect] = 'P';
			dictionary[IntervalQuality.Minor] = 'm';
			dictionary[IntervalQuality.Major] = 'M';
			dictionary[IntervalQuality.Augmented] = 'A';
			dictionary[IntervalQuality.Diminished] = 'd';
			IntervalDefinition.QualitiesSymbols = dictionary;
		}

		// Token: 0x04000526 RID: 1318
		private static readonly Dictionary<IntervalQuality, char> QualitiesSymbols;
	}
}
