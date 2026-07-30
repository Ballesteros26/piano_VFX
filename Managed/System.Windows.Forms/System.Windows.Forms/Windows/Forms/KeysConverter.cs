using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Provides a <see cref="T:System.ComponentModel.TypeConverter" /> to convert <see cref="T:System.Windows.Forms.Keys" /> objects to and from other representations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FB RID: 507
	public class KeysConverter : TypeConverter, IComparer
	{
		/// <summary>Returns a value indicating whether this converter can convert an object in the specified source type to the native type of the converter using the specified context.</summary>
		/// <returns>true if the conversion can be performed; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <param name="sourceType">The <see cref="T:System.Type" /> to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F1D RID: 7965 RVA: 0x000750E8 File Offset: 0x000732E8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Returns a value indicating whether this converter can convert an object in the specified source type to the native type of the converter using the specified context.</summary>
		/// <returns>true if the conversion can be performed; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert to. </param>
		// Token: 0x06001F1E RID: 7966 RVA: 0x00075100 File Offset: 0x00073300
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(Enum[]) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Compares two key values for equivalence.</summary>
		/// <returns>An integer indicating the relationship between the two parameters.Value Type Condition A negative integer. <paramref name="a" /> is less than <paramref name="b" />. zero <paramref name="a" /> equals <paramref name="b" />. A positive integer. <paramref name="a" /> is greater than <paramref name="b" />. </returns>
		/// <param name="a">An <see cref="T:System.Object" /> that represents the first key to compare. </param>
		/// <param name="b">An <see cref="T:System.Object" /> that represents the second key to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F1F RID: 7967 RVA: 0x0007511C File Offset: 0x0007331C
		public int Compare(object a, object b)
		{
			if (a is string && b is string)
			{
				return string.Compare((string)a, (string)b);
			}
			return string.Compare(a.ToString(), b.ToString());
		}

		/// <summary>Converts the specified object to the converter's native type.</summary>
		/// <returns>An object that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An ITypeDescriptorContext that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <param name="culture">A CultureInfo object to provide locale information. </param>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.FormatException">An invalid key combination was supplied.-or- An invalid key name was supplied. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F20 RID: 7968 RVA: 0x00075164 File Offset: 0x00073364
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string[] array = ((string)value).Split(new char[] { '+' });
				Keys keys = Keys.None;
				if (array.Length > 1)
				{
					for (int i = 0; i < array.Length - 1; i++)
					{
						if (array[i].Equals("Ctrl"))
						{
							keys |= Keys.Control;
						}
						else
						{
							keys |= (Keys)((int)Enum.Parse(typeof(Keys), array[i], true));
						}
					}
				}
				if (array[array.Length - 1].Equals("Ctrl"))
				{
					keys |= Keys.Control;
				}
				else
				{
					keys |= (Keys)((int)Enum.Parse(typeof(Keys), array[array.Length - 1], true));
				}
				return keys;
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to the specified destination type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to provide locale information. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the object to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F21 RID: 7969 RVA: 0x00075240 File Offset: 0x00073440
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				StringBuilder stringBuilder = new StringBuilder();
				Keys keys = (Keys)((int)value);
				if ((keys & Keys.Control) != Keys.None)
				{
					stringBuilder.Append("Ctrl+");
				}
				if ((keys & Keys.Alt) != Keys.None)
				{
					stringBuilder.Append("Alt+");
				}
				if ((keys & Keys.Shift) != Keys.None)
				{
					stringBuilder.Append("Shift+");
				}
				stringBuilder.Append(Enum.GetName(typeof(Keys), keys & Keys.KeyCode));
				return stringBuilder.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a collection of standard values for the data type that this type converter is designed for when provided with a format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, which can be empty if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F22 RID: 7970 RVA: 0x000752E8 File Offset: 0x000734E8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			Keys[] array = new Keys[]
			{
				Keys.D0,
				Keys.D1,
				Keys.D2,
				Keys.D3,
				Keys.D4,
				Keys.D5,
				Keys.D6,
				Keys.D7,
				Keys.D8,
				Keys.D9,
				Keys.Alt,
				Keys.Back,
				Keys.Control,
				Keys.Delete,
				Keys.End,
				Keys.Return,
				Keys.F1,
				Keys.F10,
				Keys.F11,
				Keys.F12,
				Keys.F2,
				Keys.F3,
				Keys.F4,
				Keys.F5,
				Keys.F6,
				Keys.F7,
				Keys.F8,
				Keys.F9,
				Keys.Home,
				Keys.Insert,
				Keys.PageDown,
				Keys.PageUp,
				Keys.Shift
			};
			return new TypeConverter.StandardValuesCollection(array);
		}

		/// <summary>Determines if the list of standard values returned from GetStandardValues is an exclusive list using the specified <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.</summary>
		/// <returns>true if the collection returned from <see cref="Overload:System.Windows.Forms.KeysConverter.GetStandardValues" /> is an exhaustive list of possible values; otherwise, false if other values are possible. The default implementation for this method always returns false. </returns>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F23 RID: 7971 RVA: 0x000753C8 File Offset: 0x000735C8
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>Always returns true.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F24 RID: 7972 RVA: 0x000753CC File Offset: 0x000735CC
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
