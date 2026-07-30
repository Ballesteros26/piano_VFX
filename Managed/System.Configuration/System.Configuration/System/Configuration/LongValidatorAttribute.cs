using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform long-integer validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000053 RID: 83
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class LongValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range defined by the <see cref="P:System.Configuration.LongValidatorAttribute.MinValue" /> and <see cref="P:System.Configuration.LongValidatorAttribute.MaxValue" /> property values.</summary>
		/// <returns>true if the value must be excluded; otherwise, false. The default is false.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000865A File Offset: 0x0000685A
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00008662 File Offset: 0x00006862
		public bool ExcludeRange
		{
			get
			{
				return this.excludeRange;
			}
			set
			{
				this.excludeRange = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the maximum value allowed for the property.</summary>
		/// <returns>A long integer that indicates the allowed maximum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.LongValidatorAttribute.MinValue" />.</exception>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00008672 File Offset: 0x00006872
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000867A File Offset: 0x0000687A
		public long MaxValue
		{
			get
			{
				return this.maxValue;
			}
			set
			{
				this.maxValue = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the minimum value allowed for the property.</summary>
		/// <returns>An integer that indicates the allowed minimum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.LongValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000868A File Offset: 0x0000688A
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00008692 File Offset: 0x00006892
		public long MinValue
		{
			get
			{
				return this.minValue;
			}
			set
			{
				this.minValue = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.LongValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000086A2 File Offset: 0x000068A2
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new LongValidator(this.minValue, this.maxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		// Token: 0x04000107 RID: 263
		private bool excludeRange;

		// Token: 0x04000108 RID: 264
		private long maxValue;

		// Token: 0x04000109 RID: 265
		private long minValue;

		// Token: 0x0400010A RID: 266
		private ConfigurationValidatorBase instance;
	}
}
