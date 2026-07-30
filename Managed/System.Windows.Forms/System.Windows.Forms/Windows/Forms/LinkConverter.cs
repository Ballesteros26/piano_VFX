using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter for <see cref="T:System.Windows.Forms.LinkLabel.Link" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000209 RID: 521
	public class LinkConverter : TypeConverter
	{
		/// <summary>Retrieves a value that determines if this <see cref="T:System.Windows.Forms.LinkConverter" /> can convert an object having the specified context and source type to the native type of the <see cref="T:System.Windows.Forms.LinkConverter" />.</summary>
		/// <returns>true if this <see cref="T:System.Windows.Forms.LinkConverter" /> can convert the specified object; otherwise, false. </returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> providing contextual information about the object to be converted.</param>
		/// <param name="sourceType">The type of the object to be converted.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600200D RID: 8205 RVA: 0x00078294 File Offset: 0x00076494
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Retrieves a value that determines if this <see cref="T:System.Windows.Forms.LinkConverter" /> can convert an object having the specified context to the specified type.</summary>
		/// <returns>true if this <see cref="T:System.Windows.Forms.LinkConverter" /> can convert the specified object; otherwise, false. </returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> providing contextual information about the object to be converted.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600200E RID: 8206 RVA: 0x000782AC File Offset: 0x000764AC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to the native type of the <see cref="T:System.Windows.Forms.LinkConverter" />.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> providing contextual information about the object to be converted.</param>
		/// <param name="culture">Cultural attributes of the object to be converted. If this parameter is null, the <see cref="P:System.Globalization.CultureInfo.CurrentCulture" /> property value is used.</param>
		/// <param name="value">The object to be converted.</param>
		/// <exception cref="T:System.ArgumentException">The text of the object to be converted could not be parsed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600200F RID: 8207 RVA: 0x000782C8 File Offset: 0x000764C8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			if (((string)value).Length == 0)
			{
				return null;
			}
			string[] array = ((string)value).Split(culture.TextInfo.ListSeparator.ToCharArray());
			return new LinkLabel.Link(int.Parse(array[0].Trim()), int.Parse(array[1].Trim()));
		}

		/// <summary>Converts the specified object to another type.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> providing contextual information about the object to be converted.</param>
		/// <param name="culture">Cultural attributes of the object to be converted. If this parameter is null, the <see cref="P:System.Globalization.CultureInfo.CurrentCulture" /> property value is used.</param>
		/// <param name="value">The object to be converted.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <exception cref="T:System.NotSupportedException">The object cannot be converted to the destination type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002010 RID: 8208 RVA: 0x0007834C File Offset: 0x0007654C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || !(value is LinkLabel.Link) || destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			LinkLabel.Link link = (LinkLabel.Link)value;
			return string.Format("{0}{2} {1}", link.Start, link.Length, culture.TextInfo.ListSeparator);
		}
	}
}
