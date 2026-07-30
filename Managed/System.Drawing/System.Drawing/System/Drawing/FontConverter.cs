using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Text;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Drawing
{
	/// <summary>Converts <see cref="T:System.Drawing.Font" /> objects from one data type to another. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000061 RID: 97
	public class FontConverter : TypeConverter
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00008AA4 File Offset: 0x00006CA4
		~FontConverter()
		{
		}

		/// <summary>Determines whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
		/// <returns>This method returns true if this object can perform the conversion.</returns>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="sourceType">The type you want to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000371 RID: 881 RVA: 0x000065D8 File Offset: 0x000047D8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>This method returns true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An ITypeDescriptorContext object that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> object that represents the type you want to convert to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000372 RID: 882 RVA: 0x00008ACC File Offset: 0x00006CCC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to another type. </summary>
		/// <returns>The converted object. </returns>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies the culture used to represent the object. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The data type to convert the object to. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion was not successful.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000373 RID: 883 RVA: 0x00008B00 File Offset: 0x00006D00
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is Font)
			{
				Font font = (Font)value;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(font.Name).Append(culture.TextInfo.ListSeparator[0].ToString() + " ");
				stringBuilder.Append(font.Size);
				switch (font.Unit)
				{
				case GraphicsUnit.World:
					stringBuilder.Append("world");
					break;
				case GraphicsUnit.Display:
					stringBuilder.Append("display");
					break;
				case GraphicsUnit.Pixel:
					stringBuilder.Append("px");
					break;
				case GraphicsUnit.Point:
					stringBuilder.Append("pt");
					break;
				case GraphicsUnit.Inch:
					stringBuilder.Append("in");
					break;
				case GraphicsUnit.Document:
					stringBuilder.Append("doc");
					break;
				case GraphicsUnit.Millimeter:
					stringBuilder.Append("mm");
					break;
				}
				if (font.Style != FontStyle.Regular)
				{
					stringBuilder.Append(culture.TextInfo.ListSeparator[0].ToString() + " style=").Append(font.Style);
				}
				return stringBuilder.ToString();
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Font)
			{
				Font font2 = (Font)value;
				return new InstanceDescriptor(typeof(Font).GetTypeInfo().GetConstructor(new Type[]
				{
					typeof(string),
					typeof(float),
					typeof(FontStyle),
					typeof(GraphicsUnit)
				}), new object[] { font2.Name, font2.Size, font2.Style, font2.Unit });
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Converts the specified object to the native type of the converter.</summary>
		/// <returns>The converted object. </returns>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">A CultureInfo object that specifies the culture used to represent the font. </param>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000374 RID: 884 RVA: 0x00008D20 File Offset: 0x00006F20
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			text = text.Trim();
			if (text.Length == 0)
			{
				return null;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string[] array = text.Split(new char[] { culture.TextInfo.ListSeparator[0] });
			if (array.Length < 1)
			{
				throw new ArgumentException("Failed to parse font format");
			}
			text = array[0];
			float num = 8f;
			string text2 = "px";
			GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
			if (array.Length > 1)
			{
				for (int i = 0; i < array[1].Length; i++)
				{
					if (char.IsLetter(array[1][i]))
					{
						num = (float)TypeDescriptor.GetConverter(typeof(float)).ConvertFromString(context, culture, array[1].Substring(0, i));
						text2 = array[1].Substring(i);
						break;
					}
				}
				if (text2 == "display")
				{
					graphicsUnit = GraphicsUnit.Display;
				}
				else if (text2 == "doc")
				{
					graphicsUnit = GraphicsUnit.Document;
				}
				else if (text2 == "pt")
				{
					graphicsUnit = GraphicsUnit.Point;
				}
				else if (text2 == "in")
				{
					graphicsUnit = GraphicsUnit.Inch;
				}
				else if (text2 == "mm")
				{
					graphicsUnit = GraphicsUnit.Millimeter;
				}
				else if (text2 == "px")
				{
					graphicsUnit = GraphicsUnit.Pixel;
				}
				else if (text2 == "world")
				{
					graphicsUnit = GraphicsUnit.World;
				}
			}
			FontStyle fontStyle = FontStyle.Regular;
			if (array.Length > 2)
			{
				for (int j = 2; j < array.Length; j++)
				{
					string text3 = array[j];
					if (text3.IndexOf("Regular") != -1)
					{
						fontStyle |= FontStyle.Regular;
					}
					if (text3.IndexOf("Bold") != -1)
					{
						fontStyle |= FontStyle.Bold;
					}
					if (text3.IndexOf("Italic") != -1)
					{
						fontStyle |= FontStyle.Italic;
					}
					if (text3.IndexOf("Strikeout") != -1)
					{
						fontStyle |= FontStyle.Strikeout;
					}
					if (text3.IndexOf("Underline") != -1)
					{
						fontStyle |= FontStyle.Underline;
					}
				}
			}
			return new Font(text, num, fontStyle, graphicsUnit);
		}

		/// <summary>Creates an object of this type by using a specified set of property values for the object. </summary>
		/// <returns>The newly created object, or null if the object could not be created. The default implementation returns null.<see cref="M:System.Drawing.FontConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> useful for creating non-changeable objects that have changeable properties.</returns>
		/// <param name="context">A type descriptor through which additional context can be provided. </param>
		/// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from the <see cref="Overload:System.Drawing.FontConverter.GetProperties" /> method. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000375 RID: 885 RVA: 0x00008F14 File Offset: 0x00007114
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			byte b = 1;
			float num = 8f;
			string text = null;
			bool flag = false;
			FontStyle fontStyle = FontStyle.Regular;
			FontFamily fontFamily = null;
			GraphicsUnit graphicsUnit = GraphicsUnit.Point;
			object obj;
			if ((obj = propertyValues["GdiCharSet"]) != null)
			{
				b = (byte)obj;
			}
			if ((obj = propertyValues["Size"]) != null)
			{
				num = (float)obj;
			}
			if ((obj = propertyValues["Unit"]) != null)
			{
				graphicsUnit = (GraphicsUnit)obj;
			}
			if ((obj = propertyValues["Name"]) != null)
			{
				text = (string)obj;
			}
			if ((obj = propertyValues["GdiVerticalFont"]) != null)
			{
				flag = (bool)obj;
			}
			if ((obj = propertyValues["Bold"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Bold;
			}
			if ((obj = propertyValues["Italic"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Italic;
			}
			if ((obj = propertyValues["Strikeout"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			if ((obj = propertyValues["Underline"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Underline;
			}
			if (text == null)
			{
				fontFamily = new FontFamily("Tahoma");
			}
			else
			{
				text = text.ToLower();
				foreach (FontFamily fontFamily2 in new InstalledFontCollection().Families)
				{
					if (text == fontFamily2.Name.ToLower())
					{
						fontFamily = fontFamily2;
						break;
					}
				}
				if (fontFamily == null)
				{
					foreach (FontFamily fontFamily3 in new PrivateFontCollection().Families)
					{
						if (text == fontFamily3.Name.ToLower())
						{
							fontFamily = fontFamily3;
							break;
						}
					}
				}
				if (fontFamily == null)
				{
					fontFamily = FontFamily.GenericSansSerif;
				}
			}
			return new Font(fontFamily, num, fontStyle, graphicsUnit, b, flag);
		}

		/// <summary>Determines whether changing a value on this object should require a call to the <see cref="Overload:System.Drawing.FontConverter.CreateInstance" /> method to create a new value.</summary>
		/// <returns>This method returns true if the CreateInstance object should be called when a change is made to one or more properties of this object; otherwise, false.</returns>
		/// <param name="context">A type descriptor through which additional context can be provided. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000376 RID: 886 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Retrieves the set of properties for this type. By default, a type does not have any properties to return. </summary>
		/// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return null. The default implementation always returns null.An easy implementation of this method can call the <see cref="Overload:System.ComponentModel.TypeConverter.GetProperties" /> method for the correct data type.</returns>
		/// <param name="context">A type descriptor through which additional context can be provided. </param>
		/// <param name="value">The value of the object to get the properties for. </param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000377 RID: 887 RVA: 0x000090D4 File Offset: 0x000072D4
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value is Font)
			{
				return TypeDescriptor.GetProperties(value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <summary>Determines whether this object supports properties. The default is false.</summary>
		/// <returns>This method returns true if the <see cref="M:System.Drawing.FontConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext)" /> method should be called to find the properties of this object; otherwise, false.</returns>
		/// <param name="context">A type descriptor through which additional context can be provided. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000378 RID: 888 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>
		///   <see cref="T:System.Drawing.FontConverter.FontNameConverter" /> is a type converter that is used to convert a font name to and from various other representations.</summary>
		// Token: 0x02000062 RID: 98
		public sealed class FontNameConverter : TypeConverter, IDisposable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.FontConverter.FontNameConverter" /> class. </summary>
			// Token: 0x06000379 RID: 889 RVA: 0x000090EF File Offset: 0x000072EF
			public FontNameConverter()
			{
				this.fonts = FontFamily.Families;
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x0600037A RID: 890 RVA: 0x00002CE2 File Offset: 0x00000EE2
			void IDisposable.Dispose()
			{
			}

			/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
			/// <returns>true if the converter can perform the conversion; otherwise, false. </returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may return null.</param>
			/// <param name="sourceType">The type you wish to convert from. </param>
			// Token: 0x0600037B RID: 891 RVA: 0x000065D8 File Offset: 0x000047D8
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			/// <summary>Converts the given object to the converter's native type.</summary>
			/// <returns>The converted object. </returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may return null. </param>
			/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to use to perform the conversion </param>
			/// <param name="value">The object to convert. </param>
			/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
			// Token: 0x0600037C RID: 892 RVA: 0x00009102 File Offset: 0x00007302
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return value;
				}
				return base.ConvertFrom(context, culture, value);
			}

			/// <summary>Retrieves a collection containing a set of standard values for the data type this converter is designed for. </summary>
			/// <returns>A collection containing a standard set of valid values, or null. The default is null.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may return null.</param>
			// Token: 0x0600037D RID: 893 RVA: 0x00009118 File Offset: 0x00007318
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				string[] array = new string[this.fonts.Length];
				int i = this.fonts.Length;
				while (i > 0)
				{
					i--;
					array[i] = this.fonts[i].Name;
				}
				return new TypeConverter.StandardValuesCollection(array);
			}

			/// <summary>Determines if the list of standard values returned from the <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" /> method is an exclusive list. </summary>
			/// <returns>true if the collection returned from <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" />is an exclusive list of possible values; otherwise, false. The default is false. </returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may return null.</param>
			// Token: 0x0600037E RID: 894 RVA: 0x0000915C File Offset: 0x0000735C
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			/// <summary>Determines if this object supports a standard set of values that can be picked from a list.</summary>
			/// <returns>true if <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may return null. </param>
			// Token: 0x0600037F RID: 895 RVA: 0x00006BA4 File Offset: 0x00004DA4
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x040003B9 RID: 953
			private FontFamily[] fonts;
		}

		/// <summary>Converts font units to and from other unit types.</summary>
		// Token: 0x02000063 RID: 99
		public class FontUnitConverter : EnumConverter
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.FontConverter.FontUnitConverter" /> class.</summary>
			// Token: 0x06000380 RID: 896 RVA: 0x0000915F File Offset: 0x0000735F
			public FontUnitConverter()
				: base(typeof(GraphicsUnit))
			{
			}

			/// <summary>Returns a collection of standard values valid for the <see cref="T:System.Drawing.Font" /> type.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			// Token: 0x06000381 RID: 897 RVA: 0x00009171 File Offset: 0x00007371
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				return base.GetStandardValues(context);
			}
		}
	}
}
