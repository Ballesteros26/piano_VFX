using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Drawing
{
	/// <summary>Converts <see cref="T:System.Drawing.SizeF" /> objects from one type to another.</summary>
	// Token: 0x02000087 RID: 135
	public class SizeFConverter : TypeConverter
	{
		/// <summary>Returns a value indicating whether the converter can convert from the type specified to the <see cref="T:System.Drawing.SizeF" /> type, using the specified context.</summary>
		/// <returns>true to indicate the conversion can be performed; otherwise, false. </returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> the represents the type you wish to convert from.</param>
		// Token: 0x060006FC RID: 1788 RVA: 0x000065D8 File Offset: 0x000047D8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether the <see cref="T:System.Drawing.SizeFConverter" /> can convert a <see cref="T:System.Drawing.SizeF" /> to the specified type.</summary>
		/// <returns>true if this converter can perform the conversion otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		// Token: 0x060006FD RID: 1789 RVA: 0x00008ACC File Offset: 0x00006CCC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		// Token: 0x060006FE RID: 1790 RVA: 0x000141B0 File Offset: 0x000123B0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
			SingleConverter singleConverter = new SingleConverter();
			float[] array2 = new float[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (float)singleConverter.ConvertFromString(context, culture, array[i]);
			}
			if (array.Length != 2)
			{
				throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"Width,Height.\"");
			}
			return new SizeF(array2[0], array2[1]);
		}

		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
		// Token: 0x060006FF RID: 1791 RVA: 0x00014254 File Offset: 0x00012454
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is SizeF)
			{
				SizeF sizeF = (SizeF)value;
				if (destinationType == typeof(string))
				{
					return sizeF.Width.ToString(culture) + culture.TextInfo.ListSeparator + " " + sizeF.Height.ToString(culture);
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					return new InstanceDescriptor(typeof(SizeF).GetConstructor(new Type[]
					{
						typeof(float),
						typeof(float)
					}), new object[] { sizeF.Width, sizeF.Height });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an instance of a <see cref="T:System.Drawing.SizeF" /> with the specified property values using the specified context.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the new <see cref="T:System.Drawing.SizeF" />, or null if the object cannot be created.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> containing property names and values.</param>
		// Token: 0x06000700 RID: 1792 RVA: 0x00014334 File Offset: 0x00012534
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			float num = (float)propertyValues["Width"];
			float num2 = (float)propertyValues["Height"];
			return new SizeF(num, num2);
		}

		/// <summary>Returns a value indicating whether changing a value on this object requires a call to the <see cref="Overload:System.Drawing.SizeFConverter.CreateInstance" /> method to create a new value.</summary>
		/// <returns>Always returns true.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. This may be null.</param>
		// Token: 0x06000701 RID: 1793 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Retrieves a set of properties for the <see cref="T:System.Drawing.SizeF" /> type using the specified context and attributes.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to return properties for.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
		// Token: 0x06000702 RID: 1794 RVA: 0x0001436D File Offset: 0x0001256D
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value is SizeF)
			{
				return TypeDescriptor.GetProperties(value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <summary>Returns whether the <see cref="T:System.Drawing.SizeF" /> type supports properties.</summary>
		/// <returns>Always returns true.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		// Token: 0x06000703 RID: 1795 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
