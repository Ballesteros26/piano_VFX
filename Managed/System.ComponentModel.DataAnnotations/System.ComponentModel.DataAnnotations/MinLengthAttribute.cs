using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the minimum length of array of string data allowed in a property.</summary>
	// Token: 0x02000023 RID: 35
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MinLengthAttribute : ValidationAttribute
	{
		/// <summary>Gets or sets the minimum allowable length of the array or string data.</summary>
		/// <returns>The minimum allowable length of the array or string data.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000396D File Offset: 0x00001B6D
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003975 File Offset: 0x00001B75
		public int Length { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.MinLengthAttribute" /> class.</summary>
		/// <param name="length">The length of the array or string data.</param>
		// Token: 0x060000C9 RID: 201 RVA: 0x0000397E File Offset: 0x00001B7E
		public MinLengthAttribute(int length)
			: base("The field {0} must be a string or array type with a minimum length of '{1}'.")
		{
			this.Length = length;
		}

		/// <summary>Determines whether a specified object is valid.</summary>
		/// <returns>true if the specified object is valid; otherwise, false.</returns>
		/// <param name="value">The object to validate.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x00003994 File Offset: 0x00001B94
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
			return num >= this.Length;
		}

		/// <summary>Applies formatting to a specified error message.</summary>
		/// <returns>A localized string to describe the minimum acceptable length.</returns>
		/// <param name="name">The name to include in the formatted string.</param>
		// Token: 0x060000CB RID: 203 RVA: 0x000039D9 File Offset: 0x00001BD9
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, this.Length);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000039F7 File Offset: 0x00001BF7
		private void EnsureLegalLengths()
		{
			if (this.Length < 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "MinLengthAttribute must have a Length value that is zero or greater.", Array.Empty<object>()));
			}
		}
	}
}
