using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Drawing
{
	/// <summary>The <see cref="T:System.Drawing.SizeConverter" /> class is used to convert from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000085 RID: 133
	public class SizeConverter : TypeConverter
	{
		/// <summary>Determines whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
		/// <returns>This method returns true if this object can perform the conversion.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="sourceType">The type you want to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006DF RID: 1759 RVA: 0x000065D8 File Offset: 0x000047D8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>This method returns true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This can be null, so always check. Also, properties on the context object can return null.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E0 RID: 1760 RVA: 0x00008ACC File Offset: 0x00006CCC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to the converter's native type.</summary>
		/// <returns>The converted object. </returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E1 RID: 1761 RVA: 0x00013DEC File Offset: 0x00011FEC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
			Int32Converter int32Converter = new Int32Converter();
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (int)int32Converter.ConvertFromString(context, culture, array[i]);
			}
			if (array.Length != 2)
			{
				throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"Width,Height.\"");
			}
			return new Size(array2[0], array2[1]);
		}

		/// <summary>Converts the specified object to the specified type.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E2 RID: 1762 RVA: 0x00013E90 File Offset: 0x00012090
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			if (value is Size)
			{
				Size size = (Size)value;
				if (destinationType == typeof(string))
				{
					return size.Width.ToString(culture) + culture.TextInfo.ListSeparator + " " + size.Height.ToString(culture);
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					return new InstanceDescriptor(typeof(Size).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int)
					}), new object[] { size.Width, size.Height });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an object of this type by using a specified set of property values for the object. This is useful for creating non-changeable objects that have changeable properties.</summary>
		/// <returns>The newly created object, or null if the object could not be created. The default implementation returns null.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
		/// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E3 RID: 1763 RVA: 0x00013F7C File Offset: 0x0001217C
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object obj = propertyValues["Width"];
			object obj2 = propertyValues["Height"];
			if (obj == null || obj2 == null)
			{
				throw new ArgumentException("propertyValues");
			}
			int num = (int)obj;
			int num2 = (int)obj2;
			return new Size(num, num2);
		}

		/// <summary>Determines whether changing a value on this object should require a call to the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method to create a new value.</summary>
		/// <returns>true if the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> object should be called when a change is made to one or more properties of this object.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E4 RID: 1764 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Retrieves the set of properties for this type. By default, a type does not have any properties to return. </summary>
		/// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return null. The default implementation always returns null.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
		/// <param name="value">The value of the object to get the properties for. </param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E5 RID: 1765 RVA: 0x00013FC8 File Offset: 0x000121C8
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value is Size)
			{
				return TypeDescriptor.GetProperties(value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <summary>Determines whether this object supports properties. By default, this is false.</summary>
		/// <returns>true if the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method should be called to find the properties of this object.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006E6 RID: 1766 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
