using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.SelectionRange" /> objects to and from various other types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002D9 RID: 729
	public class SelectionRangeConverter : TypeConverter
	{
		/// <summary>Determines if this converter can convert an object of the specified source type to the native type of the converter by querying the supplied type descriptor context.</summary>
		/// <returns>true if the converter can perform the specified conversion; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">The source <see cref="T:System.Type" /> to be converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003003 RID: 12291 RVA: 0x000B9A88 File Offset: 0x000B7C88
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the specified destination type by using the specified context.</summary>
		/// <returns>true if this converter can perform the specified conversion; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">The destination <see cref="T:System.Type" /> to convert into. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003004 RID: 12292 RVA: 0x000B9AA0 File Offset: 0x000B7CA0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		/// <summary>Converts the specified value to the converter's native type by using the specified locale.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />. </returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The locale information for the conversion. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of type <see cref="T:System.String" /> but could not be parsed into two strings representing dates.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> is of type <see cref="T:System.String" /> that was parsed into two strings, but the conversion of one or both into the <see cref="T:System.DateTime" /> type did not succeed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003005 RID: 12293 RVA: 0x000B9AB8 File Offset: 0x000B7CB8
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
			string[] array = ((string)value).Split(culture.TextInfo.ListSeparator.ToCharArray());
			DateTime dateTime = (DateTime)TypeDescriptor.GetConverter(typeof(DateTime)).ConvertFromString(context, culture, array[0]);
			DateTime dateTime2 = (DateTime)TypeDescriptor.GetConverter(typeof(DateTime)).ConvertFromString(context, culture, array[1]);
			return new SelectionRange(dateTime, dateTime2);
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.SelectionRangeConverter" /> object to another type by using the specified culture.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The locale information for the conversion. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The destination <see cref="T:System.Type" /> to convert into. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> cannot be converted to the <paramref name="destinationType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003006 RID: 12294 RVA: 0x000B9B50 File Offset: 0x000B7D50
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || !(value is SelectionRange) || destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			SelectionRange selectionRange = (SelectionRange)value;
			return selectionRange.Start.ToShortDateString() + culture.TextInfo.ListSeparator + selectionRange.End.ToShortDateString();
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.SelectionRange" /> object by using the specified type descriptor and set of property values for that object.</summary>
		/// <returns>If successful, the newly created <see cref="T:System.Windows.Forms.SelectionRange" />; otherwise, this method throws an exception.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> that contains the new property values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyValues" /> is null or its Start and End elements could not be converted into a <see cref="T:System.Windows.Forms.SelectionRange" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003007 RID: 12295 RVA: 0x000B9BCC File Offset: 0x000B7DCC
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new SelectionRange((DateTime)propertyValues["Start"], (DateTime)propertyValues["End"]);
		}

		/// <summary>Determines if changing a value on an instance should require a call to <see cref="Overload:System.Windows.Forms.SelectionRangeConverter.CreateInstance" /> to create a new value.</summary>
		/// <returns>true if <see cref="Overload:System.Windows.Forms.SelectionRangeConverter.CreateInstance" /> must be called to make a change to one or more properties; otherwise false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003008 RID: 12296 RVA: 0x000B9BF4 File Offset: 0x000B7DF4
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Returns the set of filtered properties for the <see cref="T:System.Windows.Forms.SelectionRange" /> type </summary>
		/// <returns>If successful, the set of properties that should be exposed for the <see cref="T:System.Windows.Forms.SelectionRange" /> type; otherwise, null.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003009 RID: 12297 RVA: 0x000B9BF8 File Offset: 0x000B7DF8
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(SelectionRange), attributes);
		}

		/// <summary>Determines whether the current object supports properties that use the specified type descriptor context.</summary>
		/// <returns>true if <see cref="Overload:System.Windows.Forms.SelectionRangeConverter.GetProperties" /> can be called to find the properties of a <see cref="T:System.Windows.Forms.SelectionRange" /> object; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600300A RID: 12298 RVA: 0x000B9C0C File Offset: 0x000B7E0C
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
