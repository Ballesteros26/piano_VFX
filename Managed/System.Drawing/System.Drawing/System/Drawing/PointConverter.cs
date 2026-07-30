using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Drawing
{
	/// <summary>Converts a <see cref="T:System.Drawing.Point" /> object from one data type to another. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007D RID: 125
	public class PointConverter : TypeConverter
	{
		/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <returns>true if this object can perform the conversion; otherwise, false.</returns>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="sourceType">The type you want to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000616 RID: 1558 RVA: 0x000065D8 File Offset: 0x000047D8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> object that represents the type you want to convert to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000617 RID: 1559 RVA: 0x00008ACC File Offset: 0x00006CCC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to a <see cref="T:System.Drawing.Point" /> object.</summary>
		/// <returns>The converted object. </returns>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">An object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000618 RID: 1560 RVA: 0x00011F84 File Offset: 0x00010184
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
				throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"x, y.\"");
			}
			return new Point(array2[0], array2[1]);
		}

		/// <summary>Converts the specified object to the specified type.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">An object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000619 RID: 1561 RVA: 0x00012028 File Offset: 0x00010228
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			if (value is Point)
			{
				Point point = (Point)value;
				if (destinationType == typeof(string))
				{
					return point.X.ToString(culture) + culture.TextInfo.ListSeparator + " " + point.Y.ToString(culture);
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					return new InstanceDescriptor(typeof(Point).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int)
					}), new object[] { point.X, point.Y });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an instance of this type given a set of property values for the object.</summary>
		/// <returns>The newly created object, or null if the object could not be created. The default implementation returns null.</returns>
		/// <param name="context">A type descriptor through which additional context can be provided. </param>
		/// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from <see cref="M:System.Drawing.PointConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600061A RID: 1562 RVA: 0x00012114 File Offset: 0x00010314
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object obj = propertyValues["X"];
			object obj2 = propertyValues["Y"];
			if (obj == null || obj2 == null)
			{
				throw new ArgumentException("propertyValues");
			}
			int num = (int)obj;
			int num2 = (int)obj2;
			return new Point(num, num2);
		}

		/// <summary>Determines if changing a value on this object should require a call to <see cref="M:System.Drawing.PointConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value.</summary>
		/// <returns>true if the <see cref="M:System.Drawing.PointConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method should be called when a change is made to one or more properties of this object; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600061B RID: 1563 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Retrieves the set of properties for this type. By default, a type does not return any properties. </summary>
		/// <returns>The set of properties that are exposed for this data type. If no properties are exposed, this method might return null. The default implementation always returns null.</returns>
		/// <param name="context">A type descriptor through which additional context can be provided. </param>
		/// <param name="value">The value of the object to get the properties for. </param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600061C RID: 1564 RVA: 0x00012160 File Offset: 0x00010360
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value is Point)
			{
				return TypeDescriptor.GetProperties(value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <summary>Determines if this object supports properties. By default, this is false.</summary>
		/// <returns>true if <see cref="M:System.Drawing.PointConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600061D RID: 1565 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
