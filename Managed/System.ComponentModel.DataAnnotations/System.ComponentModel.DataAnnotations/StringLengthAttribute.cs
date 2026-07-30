using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the minimum and maximum length of characters that are allowed in a data field.</summary>
	// Token: 0x0200002E RID: 46
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class StringLengthAttribute : ValidationAttribute
	{
		/// <summary>Gets or sets the maximum length of a string.</summary>
		/// <returns>The maximum length a string. </returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000426F File Offset: 0x0000246F
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00004277 File Offset: 0x00002477
		public int MaximumLength { get; private set; }

		/// <summary>Gets or sets the minimum length of a string.</summary>
		/// <returns>The minimum length of a string.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004280 File Offset: 0x00002480
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00004288 File Offset: 0x00002488
		public int MinimumLength { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.StringLengthAttribute" /> class by using a specified maximum length.</summary>
		/// <param name="maximumLength">The maximum length of a string. </param>
		// Token: 0x06000109 RID: 265 RVA: 0x00004291 File Offset: 0x00002491
		public StringLengthAttribute(int maximumLength)
			: base(() => "The field {0} must be a string with a maximum length of {1}.")
		{
			this.MaximumLength = maximumLength;
		}

		/// <summary>Determines whether a specified object is valid.</summary>
		/// <returns>true if the specified object is valid; otherwise, false.</returns>
		/// <param name="value">The object to validate.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumLength" /> is negative.-or-<paramref name="maximumLength" /> is less than <see cref="P:System.ComponentModel.DataAnnotations.StringLengthAttribute.MinimumLength" />.</exception>
		// Token: 0x0600010A RID: 266 RVA: 0x000042C0 File Offset: 0x000024C0
		public override bool IsValid(object value)
		{
			this.EnsureLegalLengths();
			int num = ((value == null) ? 0 : ((string)value).Length);
			return value == null || (num >= this.MinimumLength && num <= this.MaximumLength);
		}

		/// <summary>Applies formatting to a specified error message.</summary>
		/// <returns>The formatted error message.</returns>
		/// <param name="name">The name of the field that caused the validation failure.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumLength" /> is negative. -or-<paramref name="maximumLength" /> is less than <paramref name="minimumLength" />.</exception>
		// Token: 0x0600010B RID: 267 RVA: 0x00004304 File Offset: 0x00002504
		public override string FormatErrorMessage(string name)
		{
			this.EnsureLegalLengths();
			string text = ((this.MinimumLength != 0 && !base.CustomErrorMessageSet) ? "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}." : base.ErrorMessageString);
			return string.Format(CultureInfo.CurrentCulture, text, name, this.MaximumLength, this.MinimumLength);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004360 File Offset: 0x00002560
		private void EnsureLegalLengths()
		{
			if (this.MaximumLength < 0)
			{
				throw new InvalidOperationException("The maximum length must be a nonnegative integer.");
			}
			if (this.MaximumLength < this.MinimumLength)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The maximum value '{0}' must be greater than or equal to the minimum value '{1}'.", this.MaximumLength, this.MinimumLength));
			}
		}
	}
}
