using System;

namespace System.Configuration
{
	/// <summary>Provides validation of an <see cref="T:System.Int64" /> value.</summary>
	// Token: 0x02000052 RID: 82
	public class LongValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the long value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the long value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolution">An <see cref="T:System.Int64" /> value that specifies a specific value that must be matched.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolution" /> is equal to or less than 0.- or -<paramref name="maxValue" /> is less than <paramref name="minValue" />.</exception>
		// Token: 0x060002C0 RID: 704 RVA: 0x0000851D File Offset: 0x0000671D
		public LongValidator(long minValue, long maxValue, bool rangeIsExclusive, long resolution)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolution = resolution;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the long value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the long value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		// Token: 0x060002C1 RID: 705 RVA: 0x00008542 File Offset: 0x00006742
		public LongValidator(long minValue, long maxValue, bool rangeIsExclusive)
			: this(minValue, maxValue, rangeIsExclusive, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the long value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the long value.</param>
		// Token: 0x060002C2 RID: 706 RVA: 0x0000854F File Offset: 0x0000674F
		public LongValidator(long minValue, long maxValue)
			: this(minValue, maxValue, false, 0L)
		{
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches an <see cref="T:System.Int64" /> value; otherwise, false. </returns>
		/// <param name="type">The type of object.</param>
		// Token: 0x060002C3 RID: 707 RVA: 0x0000855C File Offset: 0x0000675C
		public override bool CanValidate(Type type)
		{
			return type == typeof(long);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		// Token: 0x060002C4 RID: 708 RVA: 0x00008570 File Offset: 0x00006770
		public override void Validate(object value)
		{
			long num = (long)value;
			if (!this.rangeIsExclusive)
			{
				if (num < this.minValue || num > this.maxValue)
				{
					throw new ArgumentException(string.Concat(new object[] { "The value must be in the range ", this.minValue, " - ", this.maxValue }));
				}
			}
			else if (num >= this.minValue && num <= this.maxValue)
			{
				throw new ArgumentException(string.Concat(new object[] { "The value must not be in the range ", this.minValue, " - ", this.maxValue }));
			}
			if (this.resolution != 0L && num % this.resolution != 0L)
			{
				throw new ArgumentException("The value must have a resolution of " + this.resolution);
			}
		}

		// Token: 0x04000103 RID: 259
		private bool rangeIsExclusive;

		// Token: 0x04000104 RID: 260
		private long minValue;

		// Token: 0x04000105 RID: 261
		private long maxValue;

		// Token: 0x04000106 RID: 262
		private long resolution;
	}
}
