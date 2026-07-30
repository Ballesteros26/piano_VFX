using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the numeric range constraints for the value of a data field. </summary>
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class RangeAttribute : ValidationAttribute
	{
		/// <summary>Gets the minimum allowed field value.</summary>
		/// <returns>The minimu value that is allowed for the data field.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003C48 File Offset: 0x00001E48
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003C50 File Offset: 0x00001E50
		public object Minimum { get; private set; }

		/// <summary>Gets the maximum allowed field value.</summary>
		/// <returns>The maximum value that is allowed for the data field.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003C59 File Offset: 0x00001E59
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003C61 File Offset: 0x00001E61
		public object Maximum { get; private set; }

		/// <summary>Gets the type of the data field whose value must be validated.</summary>
		/// <returns>The type of the data field whose value must be validated.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003C6A File Offset: 0x00001E6A
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003C72 File Offset: 0x00001E72
		public Type OperandType { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003C7B File Offset: 0x00001E7B
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003C83 File Offset: 0x00001E83
		private Func<object, object> Conversion { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.RangeAttribute" /> class by using the specified minimum and maximum values.</summary>
		/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
		/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
		// Token: 0x060000DB RID: 219 RVA: 0x00003C8C File Offset: 0x00001E8C
		public RangeAttribute(int minimum, int maximum)
			: this()
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
			this.OperandType = typeof(int);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.RangeAttribute" /> class by using the specified minimum and maximum values. </summary>
		/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
		/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
		// Token: 0x060000DC RID: 220 RVA: 0x00003CBC File Offset: 0x00001EBC
		public RangeAttribute(double minimum, double maximum)
			: this()
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
			this.OperandType = typeof(double);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.RangeAttribute" /> class by using the specified minimum and maximum values and the specific type.</summary>
		/// <param name="type">Specifies the type of the object to test.</param>
		/// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
		/// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060000DD RID: 221 RVA: 0x00003CEC File Offset: 0x00001EEC
		public RangeAttribute(Type type, string minimum, string maximum)
			: this()
		{
			this.OperandType = type;
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003D09 File Offset: 0x00001F09
		private RangeAttribute()
			: base(() => "The field {0} must be between {1} and {2}.")
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003D30 File Offset: 0x00001F30
		private void Initialize(IComparable minimum, IComparable maximum, Func<object, object> conversion)
		{
			if (minimum.CompareTo(maximum) > 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The maximum value '{0}' must be greater than or equal to the minimum value '{1}'.", maximum, minimum));
			}
			this.Minimum = minimum;
			this.Maximum = maximum;
			this.Conversion = conversion;
		}

		/// <summary>Checks that the value of the data field is in the specified range.</summary>
		/// <returns>true if the specified value is in the range; otherwise, false.</returns>
		/// <param name="value">The data field value to validate.</param>
		/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">The data field value was outside the allowed range.</exception>
		// Token: 0x060000E0 RID: 224 RVA: 0x00003D68 File Offset: 0x00001F68
		public override bool IsValid(object value)
		{
			this.SetupConversion();
			if (value == null)
			{
				return true;
			}
			string text = value as string;
			if (text != null && string.IsNullOrEmpty(text))
			{
				return true;
			}
			object obj = null;
			try
			{
				obj = this.Conversion(value);
			}
			catch (FormatException)
			{
				return false;
			}
			catch (InvalidCastException)
			{
				return false;
			}
			catch (NotSupportedException)
			{
				return false;
			}
			IComparable comparable = (IComparable)this.Minimum;
			IComparable comparable2 = (IComparable)this.Maximum;
			return comparable.CompareTo(obj) <= 0 && comparable2.CompareTo(obj) >= 0;
		}

		/// <summary>Formats the error message that is displayed when range validation fails.</summary>
		/// <returns>The formatted error message.</returns>
		/// <param name="name">The name of the field that caused the validation failure. </param>
		// Token: 0x060000E1 RID: 225 RVA: 0x00003E10 File Offset: 0x00002010
		public override string FormatErrorMessage(string name)
		{
			this.SetupConversion();
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, this.Minimum, this.Maximum);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003E38 File Offset: 0x00002038
		private void SetupConversion()
		{
			if (this.Conversion == null)
			{
				object minimum = this.Minimum;
				object maximum = this.Maximum;
				if (minimum == null || maximum == null)
				{
					throw new InvalidOperationException("The minimum and maximum values must be set.");
				}
				Type type2 = minimum.GetType();
				if (type2 == typeof(int))
				{
					this.Initialize((int)minimum, (int)maximum, (object v) => Convert.ToInt32(v, CultureInfo.InvariantCulture));
					return;
				}
				if (type2 == typeof(double))
				{
					this.Initialize((double)minimum, (double)maximum, (object v) => Convert.ToDouble(v, CultureInfo.InvariantCulture));
					return;
				}
				Type type = this.OperandType;
				if (type == null)
				{
					throw new InvalidOperationException("The OperandType must be set when strings are used for minimum and maximum values.");
				}
				Type typeFromHandle = typeof(IComparable);
				if (!typeFromHandle.IsAssignableFrom(type))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The type {0} must implement {1}.", type.FullName, typeFromHandle.FullName));
				}
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				IComparable comparable = (IComparable)converter.ConvertFromString((string)minimum);
				IComparable comparable2 = (IComparable)converter.ConvertFromString((string)maximum);
				Func<object, object> func = delegate(object value)
				{
					if (value == null || !(value.GetType() == type))
					{
						return converter.ConvertFrom(value);
					}
					return value;
				};
				this.Initialize(comparable, comparable2, func);
			}
		}
	}
}
