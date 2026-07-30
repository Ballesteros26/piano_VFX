using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000AE RID: 174
	public sealed class TimeSignature
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x000132F0 File Offset: 0x000114F0
		public TimeSignature(int numerator, int denominator)
		{
			ThrowIfArgument.IsNonpositive("numerator", numerator, "Numerator is zero or negative.");
			ThrowIfArgument.IsNonpositive("denominator", denominator, "Denominator is zero or negative.");
			ThrowIfArgument.DoesntSatisfyCondition("denominator", denominator, new Predicate<int>(MathUtilities.IsPowerOfTwo), "Denominator is not a power of two.");
			this.Numerator = numerator;
			this.Denominator = denominator;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001334D File Offset: 0x0001154D
		public int Numerator { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00013355 File Offset: 0x00011555
		public int Denominator { get; }

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001335D File Offset: 0x0001155D
		public static bool operator ==(TimeSignature timeSignature1, TimeSignature timeSignature2)
		{
			return timeSignature1 == timeSignature2 || (timeSignature1 != null && timeSignature2 != null && timeSignature1.Numerator == timeSignature2.Numerator && timeSignature1.Denominator == timeSignature2.Denominator);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001338B File Offset: 0x0001158B
		public static bool operator !=(TimeSignature timeSignature1, TimeSignature timeSignature2)
		{
			return !(timeSignature1 == timeSignature2);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013397 File Offset: 0x00011597
		public override string ToString()
		{
			return string.Format("{0}/{1}", this.Numerator, this.Denominator);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000133B9 File Offset: 0x000115B9
		public override bool Equals(object obj)
		{
			return this == obj as TimeSignature;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000133C8 File Offset: 0x000115C8
		public override int GetHashCode()
		{
			return (17 * 23 + this.Numerator.GetHashCode()) * 23 + this.Denominator.GetHashCode();
		}

		// Token: 0x040006A1 RID: 1697
		public static readonly TimeSignature Default = new TimeSignature(4, 4);
	}
}
