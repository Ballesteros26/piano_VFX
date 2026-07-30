using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.FontUnit" /> to a string. It also converts a string to a <see cref="T:System.Web.UI.WebControls.FontUnit" />. </summary>
	// Token: 0x02000399 RID: 921
	public class FontUnitConverter : TypeConverter
	{
		/// <summary>Determines whether a data type can be converted to a <see cref="T:System.Web.UI.WebControls.FontUnit" />.</summary>
		/// <returns>true if the data type specified by the <paramref name="sourceType" /> parameter can be converted to a <see cref="T:System.Web.UI.WebControls.FontUnit" />; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> implemented object that provides information about the context of a type converter. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the data type to check. </param>
		// Token: 0x06002447 RID: 9287 RVA: 0x00035B25 File Offset: 0x00033D25
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether the converter can convert a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object to the specified type.</summary>
		/// <returns>true if the converter supports converting a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object to the destination type; otherwise, false.NoteThis method will return true only if the <paramref name="destinationType" /> is <see cref="T:System.String" />. The only type this converter can convert <see cref="T:System.Web.UI.WebControls.FontUnit" /> into is <see cref="T:System.String" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the object to convert.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the data type to convert to.</param>
		// Token: 0x06002448 RID: 9288 RVA: 0x0005E4A4 File Offset: 0x0005C6A4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts an object to a <see cref="T:System.Web.UI.WebControls.FontUnit" /> in a specific culture.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> implemented object that provides information about the context of a type converter. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the <see cref="T:System.Web.UI.WebControls.FontUnit" />, when it is expressed in points. </param>
		/// <param name="value">The object to convert to a <see cref="T:System.Web.UI.WebControls.FontUnit" />. </param>
		// Token: 0x06002449 RID: 9289 RVA: 0x0005E4C4 File Offset: 0x0005C6C4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			if (text == "")
			{
				return FontUnit.Empty;
			}
			return FontUnit.Parse(text, culture);
		}

		/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.FontUnit" /> to an object with a different data type.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> implemented object that provides information about the context of a type converter. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the <see cref="T:System.Web.UI.WebControls.FontUnit" />, when it is expressed in points. </param>
		/// <param name="value">A <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.FontUnit" /> to convert. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the data type to convert to. </param>
		// Token: 0x0600244A RID: 9290 RVA: 0x0005E51C File Offset: 0x0005C71C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || !(value is FontUnit) || destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			return ((FontUnit)value).ToString(culture);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing the standard values for a <see cref="T:System.Web.UI.WebControls.FontUnit" />, using the specified format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing the standard values for a <see cref="T:System.Web.UI.WebControls.FontUnit" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about the context of a type converter. </param>
		// Token: 0x0600244B RID: 9291 RVA: 0x0005E56C File Offset: 0x0005C76C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(FontUnit));
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < properties.Count; i++)
			{
				arrayList.Add(properties[i].GetValue(null));
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}

		/// <summary>Returns whether the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned by the <see cref="M:System.Web.UI.WebControls.FontUnitConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method is an exclusive list of values, using the specified format context.</summary>
		/// <returns>false for all cases.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about the context of a type converter. </param>
		// Token: 0x0600244C RID: 9292 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns whether the instance of the <see cref="T:System.Web.UI.WebControls.FontUnitConverter" /> class that this method is called from supports a standard set of values that can be picked from a list, using the specified format context.</summary>
		/// <returns>true for all cases.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about the context of a type converter. </param>
		// Token: 0x0600244D RID: 9293 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
