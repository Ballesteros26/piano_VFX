using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the maximum length of array or string data allowed in a property.</summary>
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MaxLengthAttribute : ValidationAttribute
	{
		/// <summary>Gets the maximum allowable length of the array or string data.</summary>
		/// <returns>The maximum allowable length of the array or string data.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003739 File Offset: 0x00001939
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003741 File Offset: 0x00001941
		public int Length { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.MaxLengthAttribute" /> class based on the <paramref name="length" /> parameter.</summary>
		/// <param name="length">The maximum allowable length of array or string data.</param>
		// Token: 0x060000AF RID: 175 RVA: 0x0000374A File Offset: 0x0000194A
		public MaxLengthAttribute(int length)
			: base(() => MaxLengthAttribute.DefaultErrorMessageString)
		{
			this.Length = length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.MaxLengthAttribute" /> class.</summary>
		// Token: 0x060000B0 RID: 176 RVA: 0x00003778 File Offset: 0x00001978
		public MaxLengthAttribute()
			: base(() => MaxLengthAttribute.DefaultErrorMessageString)
		{
			this.Length = -1;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000037A6 File Offset: 0x000019A6
		private static string DefaultErrorMessageString
		{
			get
			{
				return "The field {0} must be a string or array type with a maximum length of '{1}'.";
			}
		}

		/// <summary>Determines whether a specified object is valid.</summary>
		/// <returns>true if the value is null, or if the value is less than or equal to the specified maximum length; otherwise, false.</returns>
		/// <param name="value">The object to validate.</param>
		/// <exception cref="Sytem.InvalidOperationException">Length is zero or less than negative one.</exception>
		// Token: 0x060000B2 RID: 178 RVA: 0x000037B0 File Offset: 0x000019B0
		public override bool IsValid(object value)
		{
			this.EnsureLegalLengths();
			if (value == null)
			{
				return true;
			}
			string text = value as string;
			int num;
			if (text != null)
			{
				num = text.Length;
			}
			else
			{
				num = ((Array)value).Length;
			}
			return -1 == this.Length || num <= this.Length;
		}

		/// <summary>Applies formatting to a specified error message.</summary>
		/// <returns>A localized string to describe the maximum acceptable length.</returns>
		/// <param name="name">The name to include in the formatted string.</param>
		// Token: 0x060000B3 RID: 179 RVA: 0x00003800 File Offset: 0x00001A00
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, this.Length);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000381E File Offset: 0x00001A1E
		private void EnsureLegalLengths()
		{
			if (this.Length == 0 || this.Length < -1)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MaxLengthAttribute must have a Length value that is greater than zero. Use MaxLength() without parameters to indicate that the string or array can have the maximum allowable length.", Array.Empty<object>()));
			}
		}

		// Token: 0x0400007D RID: 125
		private const int MaxAllowableLength = -1;
	}
}
